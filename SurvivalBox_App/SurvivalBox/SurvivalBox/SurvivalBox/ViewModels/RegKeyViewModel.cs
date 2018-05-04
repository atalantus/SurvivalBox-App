using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using Prism.Services;

namespace SurvivalBox.ViewModels
{
	public class RegKeyViewModel : BindableBase
	{
	    private readonly INavigationService _navigationService;
	    private readonly IPageDialogService _dialogService;

	    public string Title { get; } = "Account Creation";

        private string _regKeyValue = String.Empty;
        public string RegKeyValue
        {
            get => _regKeyValue;
            set => SetProperty(ref _regKeyValue, value);
        }

        public DelegateCommand ActivateRegKeyCommand { get; }
        public DelegateCommand LogInCommand { get; }

        public RegKeyViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            ActivateRegKeyCommand = new DelegateCommand(ActivateRegKey);
            LogInCommand = new DelegateCommand(LogIn);
        }

	    private void ActivateRegKey()
	    {
            //TODO: Registration Key mit Datenbank ueberpruefen

            // WENN (Registration Key frei)
            ////// WENN (Benutzer bereits registriert)
            //////////  _navigationService.NavigateAsync("Login");
            ////// SONST
                    _navigationService.NavigateAsync("CreateAccount");
	        // SONST
	        ////// _dialogService.DisplayAlertAsync("Ooops", "Looks like this key is invalid!", "OK");
	    }

        private void LogIn()
	    {
	        _navigationService.NavigateAsync("Login");
	    }
	}
}
