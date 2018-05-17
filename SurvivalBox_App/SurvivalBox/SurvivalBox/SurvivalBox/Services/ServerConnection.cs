using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using SurvivalBox.Models;

namespace SurvivalBox.Services
{
    public class ServerConnection
    {
        private static ServerConnection _defaultInstance = null;
        public static ServerConnection DefaultInstance => _defaultInstance ?? (_defaultInstance = new ServerConnection());

        private readonly MobileServiceClient _client;
        public MobileServiceClient Client => _client;

        private readonly MobileServiceSQLiteStore _store;
        public MobileServiceSQLiteStore Store => _store;

        public ServerConnection()
        {
            _client = new MobileServiceClient(Constants.ApplicationURL);
            _store = new MobileServiceSQLiteStore("localstore.db");
            _client.SyncContext.InitializeAsync(_store);
        }
    }
}