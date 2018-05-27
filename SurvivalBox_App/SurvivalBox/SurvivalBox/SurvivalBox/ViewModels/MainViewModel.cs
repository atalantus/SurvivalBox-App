using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Prism.Navigation;
using SurvivalBox.Models;

namespace SurvivalBox.ViewModels
{
	public class MainViewModel : BindableBase
	{
	    private readonly INavigationService _navigationService;

	    public ObservableCollection<MenuItem> MenuItems { get; set; }

	    private MenuItem _selectedItem;
        public MenuItem SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public DelegateCommand ItemSelectedCommand { get; set; }

	    public MainViewModel(INavigationService navigationService)
	    {
	        _navigationService = navigationService;

	        MenuItems = new ObservableCollection<MenuItem>(new[]
	        {
	            new MenuItem { Id = 0, Title = "Todo Items", IconSource = "", ViewName = "MainTodoItem"},
	            new MenuItem { Id = 1, Title = "Sample", IconSource = "", ViewName = "MainSample"}
	        });

	        ItemSelectedCommand = new DelegateCommand(OnItemSelected);
	    }

	    private void OnItemSelected()
	    {
	        if (SelectedItem != null)
	        {
	            _navigationService.NavigateAsync($"NavigationPage/{SelectedItem.ViewName}");
	            SelectedItem = null;
	        }
	    }
    }
}
