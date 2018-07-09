using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using SurvivalBox.Models;

namespace SurvivalBox.Services
{
    public class SessionManager
    {
        private static SessionManager _instance;
        public static SessionManager Instance => _instance ?? (_instance = new SessionManager());
        private readonly ServerConnection _serverConnection;
        private readonly IMobileServiceSyncTable<Session> _sessionTable;

        public Session CurSession { get; set; }

        private SessionManager()
        {
            Debug.WriteLine("Initializing SessionManager");
            _serverConnection = ServerConnection.DefaultConnection;
            _sessionTable = _serverConnection.Client.GetSyncTable<Session>();
            Debug.WriteLine(_sessionTable.TableName);
        }

        public async Task CreateSession(Session session)
        {
            if (CurSession == null)
            {
                Debug.WriteLine("Create Session: " + session.Name);
                CurSession = session;
                session.StartDate = DateTime.UtcNow;
                session.StartSessionTimer();
                Debug.WriteLine("Started Session");
                await _sessionTable.InsertAsync(session);
                Debug.WriteLine("Inserted session into table");
                await SyncAsync();
                Debug.WriteLine("Synced table");
            }
            else
                throw new Exception("Trying to create a new session while old one is still available!");
        }

        public async Task UpdateSession(Session session)
        {
            if (session.Id != null)
                await _sessionTable.UpdateAsync(session);
        }

        public async Task EndSession()
        {
            CurSession.CancelTimer = true;
            CurSession.CurState = Session.States.ENDED;
            CurSession.EndDate = DateTime.UtcNow;
            CurSession.Done = true;
            await _sessionTable.UpdateAsync(CurSession);
            CurSession = null;
            await SyncAsync();
        }

        public async Task<ObservableCollection<Session>> GetSessionsAsync(bool doneSessions = true, bool syncItems = false)
        {
            try
            {
                if (syncItems)
                {
                    await this.SyncAsync();
                }

                IEnumerable<Session> items = await _sessionTable
                    .Where(session => session.Done == doneSessions)
                    .ToEnumerableAsync();

                //var items = await _sessionTable.ToEnumerableAsync();

                return new ObservableCollection<Session>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);

                IEnumerable<Session> items = await _sessionTable
                    .Where(session => session.Done == doneSessions)
                    .ToEnumerableAsync();

                return new ObservableCollection<Session>(items);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);

                IEnumerable<Session> items = await _sessionTable
                    .Where(session => session.Done == doneSessions)
                    .ToEnumerableAsync();

                return new ObservableCollection<Session>(items);
            }
        }

        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await _serverConnection.Client.SyncContext.PushAsync();

                await _sessionTable.PullAsync(
                    //The first parameter is a query name that is used internally by the client SDK to implement incremental sync.
                    //Use a different query name for each unique query in your program
                    "allSessionItems",
                    _sessionTable.CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            // Simple error/conflict handling. A real application would handle the various errors like network conditions,
            // server conflicts and others via the IMobileServiceSyncHandler.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }
        }
    }
}