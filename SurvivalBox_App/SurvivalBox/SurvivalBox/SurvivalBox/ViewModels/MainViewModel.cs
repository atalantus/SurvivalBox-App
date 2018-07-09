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
using Xamarin.Forms;
using MenuItem = SurvivalBox.Models.MenuItem;

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

	    private Color _warningsBackgroundColor = Color.FromHex("#a5d6a7");
        public Color WarningsBackgroundColor
        {
            get => _warningsBackgroundColor;
            set => SetProperty(ref _warningsBackgroundColor, value);
        }

	    private Color _warningsTextColor = Color.Black;
	    public Color WarningsTextColor
        {
	        get => _warningsTextColor;
	        set => SetProperty(ref _warningsTextColor, value);
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
	            new MenuItem { Id = 2, Title = "GPS-Tracker", IconSource = "location_icon.png", ViewName = "MainTracker"},
                new MenuItem { Id = 3, Title = "Todo Items", IconSource = "location_icon.png", ViewName = "MainTodoItem"},
	            new MenuItem { Id = 4, Title = "Weather", IconSource = "weather_icon.png", ViewName = "MainWeather"},
                new MenuItem { Id = 5, Title = "Settings", IconSource = "settings_icon.png", ViewName = "MainSettings"}
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
	        var dangerLevel = sender.Warnings.Select(w => w.Level).Sum();
	        switch (dangerLevel)
	        {
                case 0:
                    WarningsBackgroundColor = Color.FromHex("#a5d6a7");
                    WarningsTextColor = Color.Black;
                    break;
	            case 1:
	                WarningsBackgroundColor = Color.FromHex("#ffebee");
	                WarningsTextColor = Color.Black;
                    break;
	            case 2:
	                WarningsBackgroundColor = Color.FromHex("#ffcdd2");
	                WarningsTextColor = Color.Black;
                    break;
	            case 3:
	                WarningsBackgroundColor = Color.FromHex("#ef9a9a");
	                WarningsTextColor = Color.Black;
                    break;
	            case 4:
	                WarningsBackgroundColor = Color.FromHex("#e57373");
	                WarningsTextColor = Color.Black;
                    break;
	            case 5:
	                WarningsBackgroundColor = Color.FromHex("#ef5350");
	                WarningsTextColor = Color.Black;
                    break;
	            case 6:
	                WarningsBackgroundColor = Color.FromHex("#f44336");
	                WarningsTextColor = Color.Black;
                    break;
	            case 7:
	                WarningsBackgroundColor = Color.FromHex("#e53935");
	                WarningsTextColor = Color.Black;
                    break;
	            case 8:
	                WarningsBackgroundColor = Color.FromHex("#d32f2f");
	                WarningsTextColor = Color.White;
                    break;
	            case 9:
	                WarningsBackgroundColor = Color.FromHex("#c62828");
	                WarningsTextColor = Color.White;
                    break;
	            case 10:
	                WarningsBackgroundColor = Color.FromHex("#b71c1c");
	                WarningsTextColor = Color.White;
                    break;
            }
	    }
    }
}
