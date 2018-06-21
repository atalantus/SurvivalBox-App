using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services;
using SurvivalBox.Models;
using SurvivalBox.Services;
using Xamarin.Forms;

namespace SurvivalBox.ViewModels
{
    public class MainTodoItemViewModel : ActivityIndicatorViewModelBase
    {
        #region Fields

        public DelegateCommand AddItemCommand { get; set; }
        public DelegateCommand ItemSelectedCommand { get; set; }
        public DelegateCommand<object> CompleteCommand { get; set; }

        private string _newItemName;
        public string NewItemName
        {
            get => _newItemName;
            set => SetProperty(ref _newItemName, value);
        }

        private TodoItem _selectedItem;
        public TodoItem SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private ObservableCollection<TodoItem> _todoItems;
        public ObservableCollection<TodoItem> TodoItems
        {
            get => _todoItems;
            set => SetProperty(ref _todoItems, value);
        }

        private TodoItemManager _manager;
        private IPageDialogService _dialogService;

        #endregion

        #region Methods

        public MainTodoItemViewModel(IPageDialogService dialogService)
        {
            AddItemCommand = new DelegateCommand(OnAddItem);
            ItemSelectedCommand = new DelegateCommand(OnItemSelected);
            CompleteCommand = new DelegateCommand<object>(Complete);

            _manager = TodoItemManager.DefaultManager;
            _dialogService = dialogService;
        }

        private async void OnAddItem()
        {
            if (!string.IsNullOrWhiteSpace(NewItemName))
            {
                var todo = new TodoItem { Name = NewItemName };
                await AddItem(todo);
            }

            NewItemName = string.Empty;
            //newItemName.Unfocus();
            //TODO: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/triggers#event
        }

        protected override void OnRefresh()
        {
            RefreshItems(false, true);
        }

        private async void OnItemSelected()
        {
            if (Device.RuntimePlatform != Device.iOS && SelectedItem != null)
            {
                // Not iOS - the swipe-to-delete is discoverable there
                if (Device.RuntimePlatform == Device.Android)
                {
                    await _dialogService.DisplayAlertAsync(SelectedItem.Name,
                        "Press-and-hold to complete task " + SelectedItem.Name, "OK");
                }
                else
                {
                    // Windows, not all platforms support the Context Actions yet
                    if (await _dialogService.DisplayAlertAsync("Mark completed?", "Do you wish to complete " + SelectedItem.Name + "?", "Complete", "Cancel"))
                    {
                        await CompleteItem(SelectedItem);
                    }
                }
            }

            // prevents background getting highlighted
            SelectedItem = null;
        }

        private async void Complete(object item)
        {
            if (item is TodoItem todoItem)
            {
                await CompleteItem(todoItem);
            }
        }

        // Data methods
        async Task AddItem(TodoItem item)
        {
            await _manager.SaveTaskAsync(item);
            TodoItems = await _manager.GetTodoItemsAsync();
        }

        async Task CompleteItem(TodoItem item)
        {
            item.Done = true;
            await _manager.SaveTaskAsync(item);
            TodoItems = await _manager.GetTodoItemsAsync();
        }

        private async void RefreshItems(bool showActivityIndicator, bool syncItems)
        {
            try
            {
                await LoadItems(showActivityIndicator, syncItems);
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e);
                await _dialogService.DisplayAlertAsync("Refresh Error",
                    "Make sure you have a working network connection!", "OK");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                await _dialogService.DisplayAlertAsync("Refresh Error", "Couldn't refresh data (" + e.Message + ")", "OK");
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        public async Task LoadItems(bool showActivityIndicator, bool syncItems)
        {
            using (new ActivityIndicatorScope(this, showActivityIndicator))
            {
                TodoItems = await _manager.GetTodoItemsAsync(syncItems);
            }
        }

        #endregion
    }
}
