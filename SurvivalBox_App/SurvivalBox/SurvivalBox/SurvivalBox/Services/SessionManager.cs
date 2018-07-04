using System;
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
        private ServerConnection _serverConnection;
        private IMobileServiceSyncTable<Session> _sessionTable;

        public Session CurSession { get; set; }

        private SessionManager()
        {
            Debug.WriteLine("Initializing SessionManager");
            _serverConnection = ServerConnection.DefaultConnection;
            Debug.WriteLine("Referenced Server Connection");
            _serverConnection.LocalDatabase.DefineTable<Session>();
            Debug.WriteLine("Created Session table in local database");
            _serverConnection.Client.SyncContext.InitializeAsync(_serverConnection.LocalDatabase);
            Debug.WriteLine("Synced local database");
            _sessionTable = _serverConnection.Client.GetSyncTable<Session>();
        }

        public async Task CreateSession(Session session)
        {
            if (CurSession == null)
            {
                await _sessionTable.InsertAsync(session);
                CurSession = session;
            }
            else
                throw new Exception("Trying to create a new session while old one is still available!");
        }

        public async Task PauseSession(bool continueSession = false)
        {
            CurSession.CurState = (continueSession) ? Session.States.ACTIVE : Session.States.STOPPED;
            // TODO: Pause duration
            await _sessionTable.UpdateAsync(CurSession);
        }

        public async Task EndSession()
        {
            CurSession.CurState = Session.States.ENDED;
            CurSession.Done = true;
            await _sessionTable.UpdateAsync(CurSession);
            CurSession = null;
        }
    }
}