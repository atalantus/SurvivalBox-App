using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Prism.Services;
using SurvivalBox.Models;
using SurvivalBox.Views;

namespace SurvivalBox.ViewModels
{
	public class MainWarningsViewModel : BindableBase
	{
	    private IPageDialogService _dialogService;

	    public ObservableCollection<Warning> Warnings => WarningManager.Instance.Warnings;

	    private Warning _selectedWarning;
        public Warning SelectedWarning
        {
            get => _selectedWarning;
            set => SetProperty(ref _selectedWarning, value);
        }

        public DelegateCommand WarningSelectedCommand { get; set; }
        public DelegateCommand<object> DeleteWarningCommand { get; set; }

        public MainWarningsViewModel(IPageDialogService dialogService)
	    {
	        _dialogService = dialogService;

            WarningSelectedCommand = new DelegateCommand(OnWarningSelected);
            DeleteWarningCommand = new DelegateCommand<object>(OnWarningDelete);
	    }

	    private void OnWarningSelected()
	    {
	        SelectedWarning = null;
	    }

	    private void OnWarningDelete(object item)
	    {
	        var warning = (item as Warning);

	        WarningManager.Instance.RemoveWarning(warning);
	    }
    }
}
