using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Prism.Navigation;
using Prism.Services;
using SurvivalBox.Models;

namespace SurvivalBox.ViewModels
{
	public class MainViewModel : BindableBase
	{
	    private readonly INavigationService _navigationService;
	    private readonly IPageDialogService _dialogService;

	    public ObservableCollection<MenuItem> MenuItems { get; set; }

	    private MenuItem _selectedItem;
        public MenuItem SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private string _warningsText = "0 Warnings!";
	    public string WarningsText
	    {
	        get => _warningsText;
	        set => SetProperty(ref _warningsText, value);
	    }

        public DelegateCommand ItemSelectedCommand { get; set; }
        public DelegateCommand ShowWarningsCommand { get; set; }

        public MainViewModel(INavigationService navigationService, IPageDialogService dialogService)
	    {
            _navigationService = navigationService;
	        _dialogService = dialogService;

	        MenuItems = new ObservableCollection<MenuItem>(new[]
	        {
                new MenuItem { Id = 0, Title = "Home", IconSource = "home_icon.png", ViewName = "MainHome"},
	            new MenuItem { Id = 1, Title = "Todo Items", IconSource = "location_icon.png", ViewName = "MainTodoItem"},
	            new MenuItem { Id = 2, Title = "Weather", IconSource = "weather_icon.png", ViewName = "MainWeather"},
                new MenuItem { Id = 3, Title = "Settings", IconSource = "settings_icon.png", ViewName = "MainSettings"}
	        });

	        ItemSelectedCommand = new DelegateCommand(OnItemSelected);
            ShowWarningsCommand = new DelegateCommand(ShowWarnings);

	        WarningManager.Instance.WarningsChanged += On_WarningsChanged;
	    }

	    private void OnItemSelected()
	    {
	        if (SelectedItem != null)
	        {
	            _navigationService.NavigateAsync($"NavigationPage/{SelectedItem.ViewName}");
	            SelectedItem = null;
	        }
	    }

	    private void ShowWarnings()
	    {
	        _navigationService.NavigateAsync("NavigationPage/MainWarnings");
	    }

	    private void On_WarningsChanged(WarningManager sender)
	    {
	        WarningsText = sender.Warnings.Count == 1 ? "1 Warning!" : $"{sender.Warnings.Count} Warnings!";
        }
    }
}
