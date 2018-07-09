using System.Diagnostics;
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
    }
}