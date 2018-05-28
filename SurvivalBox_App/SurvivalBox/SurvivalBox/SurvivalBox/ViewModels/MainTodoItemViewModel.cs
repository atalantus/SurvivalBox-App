using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services;
using SurvivalBox.Models;
using SurvivalBox.Services;
using Xamarin.Forms;

namespace SurvivalBox.ViewModels
{
    public class MainTodoItemViewModel : BindableBase
    {
        #region Fields



        public DelegateCommand AppearingCommand { get; set; }
        public DelegateCommand AddItemCommand { get; set; }
        public DelegateCommand RefreshingCommand { get; set; }
        public DelegateCommand SyncCommand { get; set; }
        public DelegateCommand ItemSelectedCommand { get; set; }
        public DelegateCommand CompleteCommand { get; set; }

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

        private bool _activityIndicatorIsVisible;
        public bool ActivityIndicatorIsVisible
        {
            get => _activityIndicatorIsVisible;
            set => SetProperty(ref _activityIndicatorIsVisible, value);
        }

        private bool _activityIndicatorIsRunning;
        public bool ActivityIndicatorIsRunning
        {
            get => _activityIndicatorIsRunning;
            set => SetProperty(ref _activityIndicatorIsRunning, value);
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
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
            AppearingCommand = new DelegateCommand(OnAppearing);
            AddItemCommand = new DelegateCommand(OnAddItem);
            RefreshingCommand = new DelegateCommand(OnRefresh);
            SyncCommand = new DelegateCommand(SyncItems);
            ItemSelectedCommand = new DelegateCommand(OnItemSelected);
            CompleteCommand = new DelegateCommand(Complete);

            _manager = TodoItemManager.DefaultManager;
            _dialogService = dialogService;

            TodoItems = new ObservableCollection<TodoItem>();
        }

        private async void OnAppearing()
        {
            Debug.WriteLine("OnAppearing()");

            // Set syncItems to true in order to synchronize the data on startup when running in offline mode
            await RefreshItems(true, true);
        }

        private async void OnAddItem()
        {
            Debug.WriteLine("OnAddItem");

            if (!string.IsNullOrWhiteSpace(NewItemName))
            {
                var todo = new TodoItem { Name = NewItemName };
                await AddItem(todo);
            }

            NewItemName = string.Empty;
            //newItemName.Unfocus();
            //TODO: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/triggers#event
        }

        private async void OnRefresh()
        {
            Debug.WriteLine("OnRefresh");

            Exception error = null;
            try
            {
                await RefreshItems(false, true);
            }
            catch (Exception ex)
            {
                error = ex;
            }
            finally
            {
                IsRefreshing = false;
            }

            if (error != null)
            {
                await _dialogService.DisplayAlertAsync("Refresh Error", "Couldn't refresh data (" + error.Message + ")", "OK");
            }
        }

        private async void OnItemSelected()
        {
            Debug.WriteLine("OnItemSelected");

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

        private void Complete()
        {
            Debug.WriteLine("Complete");
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

        async void SyncItems()
        {
            await RefreshItems(true, true);
        }

        private async Task RefreshItems(bool showActivityIndicator, bool syncItems)
        {
            using (var scope = new ActivityIndicatorScope(this, showActivityIndicator))
            {
                TodoItems = await _manager.GetTodoItemsAsync(syncItems);
            }
        }



        #endregion

        private class ActivityIndicatorScope : IDisposable
        {
            private bool _showIndicator;
            private MainTodoItemViewModel _viewModel;
            private Task _indicatorDelay;

            public ActivityIndicatorScope(MainTodoItemViewModel viewModel, bool showIndicator)
            {
                _viewModel = viewModel;
                this._showIndicator = showIndicator;

                if (showIndicator)
                {
                    _indicatorDelay = Task.Delay(2000);
                    SetIndicatorActivity(true);
                }
                else
                {
                    _indicatorDelay = Task.FromResult(0);
                }
            }

            private void SetIndicatorActivity(bool isActive)
            {
                _viewModel.ActivityIndicatorIsRunning = _viewModel.ActivityIndicatorIsVisible = isActive;
            }

            public void Dispose()
            {
                if (_showIndicator)
                {
                    _indicatorDelay.ContinueWith(t => SetIndicatorActivity(false), TaskScheduler.FromCurrentSynchronizationContext());
                }
            }
        }
    }
}
