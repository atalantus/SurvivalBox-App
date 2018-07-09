using System;
using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using SurvivalBox.Models;

namespace SurvivalBox.Services
{
    public class ServerConnection
    {
        private static ServerConnection _defaultConnection;
        public static ServerConnection DefaultConnection => _defaultConnection ?? (_defaultConnection = new ServerConnection());

        public MobileServiceClient Client { get; }

        public MobileServiceSQLiteStore Database { get; set; }

        private ServerConnection()
        {
            Debug.WriteLine("Connecting to Server...");
            Client = new MobileServiceClient(Constants.ApplicationURL);
            Debug.WriteLine("Created Client");
        }

        public async void InitializeDatabaseAsync()
        {
            try
            {
                Database = new MobileServiceSQLiteStore("survivalBox.db");
                Debug.WriteLine("Created Database!");
                Database.DefineTable<Session>();
                Debug.WriteLine("Created Session table");
                Database.DefineTable<TodoItem>();
                Debug.WriteLine("Created TodoItem table");
                Database.DefineTable<GPSData>();
                Debug.WriteLine("Created GPSData table");
                Debug.WriteLine("Client: " + Client);
                Debug.WriteLine("Database: " + Database);
                await Client.SyncContext.InitializeAsync(Database);
                Debug.WriteLine("Synced database!");
            }
            catch (Exception e)
            {
                Debug.WriteLine("Initialize Database failed: {0}", e.Message);
            }
        }
    }
}