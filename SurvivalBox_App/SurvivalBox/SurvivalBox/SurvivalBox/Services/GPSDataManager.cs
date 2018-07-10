using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using SurvivalBox.Models;

namespace SurvivalBox.Services
{
    public class GPSDataManager
    {
        private static GPSDataManager _instance;
        public static GPSDataManager Instance => _instance ?? (_instance = new GPSDataManager());
        private readonly ServerConnection _serverConnection;
        private readonly IMobileServiceSyncTable<GPSData> _gpsDataTable;

        private GPSDataManager()
        {
            Debug.WriteLine("Initializing GPSDataManager");
            _serverConnection = ServerConnection.DefaultConnection;
            _gpsDataTable = _serverConnection.Client.GetSyncTable<GPSData>();
            Debug.WriteLine(_gpsDataTable.TableName);
        }

        public async Task SaveGPSDataAsync(GPSData data)
        {
            if (data.Id == null)
            {
                Debug.WriteLine("New GPSData");
                await _gpsDataTable.InsertAsync(data);
                await SyncAsync();
            }
            else
            {
                Debug.WriteLine("Update GPSData");
                await _gpsDataTable.UpdateAsync(data);
                await SyncAsync();
            }
        }

        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await _serverConnection.Client.SyncContext.PushAsync();
                await _gpsDataTable.PullAsync(
                    //The first parameter is a query name that is used internally by the client SDK to implement incremental sync.
                    //Use a different query name for each unique query in your program
                    "allGPSData",
                    _gpsDataTable.CreateQuery());
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