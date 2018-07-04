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
    public class TodoItemManager
    {
        private static TodoItemManager _instance;
        public static TodoItemManager Instance => _instance ?? (_instance = new TodoItemManager());
        private ServerConnection _serverConnection;
        private IMobileServiceSyncTable<TodoItem> _todoTable;

        private TodoItemManager()
        {  
            Debug.WriteLine("Initializing TodoItemManager");
            _serverConnection = ServerConnection.DefaultConnection;
            Debug.WriteLine("Referenced Server Connection");
            _serverConnection.Client.SyncContext.InitializeAsync(_serverConnection.LocalDatabase);
            Debug.WriteLine("Synced local database");
            _todoTable = _serverConnection.Client.GetSyncTable<TodoItem>();
        }

        public async Task<ObservableCollection<TodoItem>> GetTodoItemsAsync(bool syncItems = false)
        {
            try
            {
                if (syncItems)
                {
                    await this.SyncAsync();
                }
                IEnumerable<TodoItem> items = await _todoTable
                                    .Where(todoItem => !todoItem.Done)
                                    .ToEnumerableAsync();

                Debug.WriteLine(new List<TodoItem>(items).Count);
                return new ObservableCollection<TodoItem>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

        public async Task SaveTaskAsync(TodoItem item)
        {
            if (item.Id == null)
            {
                Debug.WriteLine("New Item");
                await _todoTable.InsertAsync(item);
            }
            else
            {
                Debug.WriteLine("Update Item");
                await _todoTable.UpdateAsync(item);
            }
        }

        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await _serverConnection.Client.SyncContext.PushAsync();

                await _todoTable.PullAsync(
                    //The first parameter is a query name that is used internally by the client SDK to implement incremental sync.
                    //Use a different query name for each unique query in your program
                    "allTodoItems",
                    _todoTable.CreateQuery());
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