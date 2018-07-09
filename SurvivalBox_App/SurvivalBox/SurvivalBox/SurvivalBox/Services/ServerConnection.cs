using System.Collections;
using System.Diagnostics;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using SurvivalBox.Models;

namespace SurvivalBox.Services
{
    public class ServerConnection
    {
        private enum Services
        {
            TODO_ITEM,
            SESSION,
            ACCOUNT
        }

        private static ServerConnection _sessionConnection;
        private static ServerConnection _todoItemConnection;
        public static ServerConnection SessionConnection => _sessionConnection ?? (_sessionConnection = new ServerConnection(Services.SESSION));
        public static ServerConnection TodoItemConnection => _todoItemConnection ?? (_todoItemConnection = new ServerConnection(Services.TODO_ITEM));

        private MobileServiceClient _client;
        public MobileServiceClient Client => _client;
        public MobileServiceSQLiteStore LocalDatabase { get; set; }

        private ServerConnection(Services service)
        {
            Debug.WriteLine("Connecting to Server...");
            _client = new MobileServiceClient(Constants.ApplicationURL);
            Debug.WriteLine("Created Client");

            switch (service)
            {
                case Services.SESSION:
                    LocalDatabase = new MobileServiceSQLiteStore("survivalBox_session.db");
                    LocalDatabase.DefineTable<Session>();
                    break;
                case Services.TODO_ITEM:
                    LocalDatabase = new MobileServiceSQLiteStore("survivalBox_todoItem.db");
                    LocalDatabase.DefineTable<TodoItem>();
                    break;
                case Services.ACCOUNT:
                    LocalDatabase = new MobileServiceSQLiteStore("survivalBox_account.db");
                    break;
            }

            Debug.WriteLine("Created local database");           
            Debug.WriteLine("Created TodoItem table");            
            Debug.WriteLine("Created Session table");
        }
    }
}