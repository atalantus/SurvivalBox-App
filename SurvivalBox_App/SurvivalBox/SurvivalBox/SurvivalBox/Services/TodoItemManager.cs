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
        private static TodoItemManager _instance = null;

        public static TodoItemManager Instance => _instance ?? (_instance = new TodoItemManager());

        private TodoItemManager()
        {
            _client = ServerConnection.DefaultInstance.Client;
            ServerConnection.DefaultInstance.Store.DefineTable<TodoItem>();
            _todoTable = _client.GetSyncTable<TodoItem>();
        }

        private IMobileServiceSyncTable<TodoItem> _todoTable;

        private IMobileServiceClient _client;

        /// <summary>
        /// Adds or updates a <see cref="TodoItem"/> in the local store
        /// </summary>
        /// <param name="item">Item to add or update (Based on Id)</param>
        /// <returns></returns>
        public async Task AddItem(TodoItem item)
        {
            await SaveItemAsync(item);
        }

        private async Task SaveItemAsync(TodoItem item)
        {
            if (item.Id == null)
            {
                await _todoTable.InsertAsync(item);
            }
            else
            {
                await _todoTable.UpdateAsync(item);
            }
        }

        /// <summary>
        /// Gets all <see cref="TodoItem"/>
        /// </summary>
        /// <param name="syncItems">When true gets synced items from server, else only from local store</param>
        /// <returns>All <see cref="TodoItem"/>s</returns>
        public async Task<ObservableCollection<TodoItem>> GetTodoItemsAsync(bool syncItems = false)
        {
            try
            {
                if (syncItems)
                {
                    await SyncAsync();
                }
                var items = await _todoTable
                    .Where(todoItem => !todoItem.Done)
                    .ToEnumerableAsync();

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

        // Synchronize local store with Server
        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await _client.SyncContext.PushAsync();

                // The first parameter is a query name that is used internally by the client SDK to implement incremental sync.
                // Use a different query name for each unique query in your program.
                await _todoTable.PullAsync("allTodoItems", _todoTable.CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            // Simple error/conflict handling.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        // Update failed, revert to server's copy
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }
        }
    }
}