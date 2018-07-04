using System.Diagnostics;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using SurvivalBox.Models;

namespace SurvivalBox.Services
{
    public class ServerConnection
    {
        private static ServerConnection _defaultConnection;
        public static ServerConnection DefaultConnection => _defaultConnection ?? (_defaultConnection = new ServerConnection());

        private MobileServiceClient _client;
        public MobileServiceClient Client => _client;
        public MobileServiceSQLiteStore LocalDatabase { get; set; }

        private ServerConnection()
        {
            Debug.WriteLine("Connecting to Server...");
            _client = new MobileServiceClient(Constants.ApplicationURL);
            Debug.WriteLine("Created Client");
            LocalDatabase = new MobileServiceSQLiteStore("survivalBox.db");
            Debug.WriteLine("Created local database");
            LocalDatabase.DefineTable<TodoItem>();
            Debug.WriteLine("Created TodoItem table");
            LocalDatabase.DefineTable<Session>();
            Debug.WriteLine("Created Session table");
        }
    }
}