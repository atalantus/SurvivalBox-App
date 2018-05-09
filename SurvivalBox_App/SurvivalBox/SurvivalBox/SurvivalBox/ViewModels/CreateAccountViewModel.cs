using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using Prism.Services;

namespace SurvivalBox.ViewModels
{
	public class CreateAccountViewModel : BindableBase
	{
	    private readonly INavigationService _navigationService;
	    private readonly IPageDialogService _dialogService;

	    private string _eMailText = string.Empty;
        public string EMailText
        {
            get => _eMailText;
            set => SetProperty(ref _eMailText, value);
        }

	    private string _passwordText = string.Empty;
	    public string PasswordText
	    {
	        get => _passwordText;
	        set => SetProperty(ref _passwordText, value);
	    }

	    private string _passwordRepeatText = string.Empty;
	    public string PasswordRepeatText
        {
	        get => _passwordRepeatText;
	        set => SetProperty(ref _passwordRepeatText, value);
	    }

	    private string _nameText = string.Empty;
	    public string NameText
        {
	        get => _nameText;
	        set => SetProperty(ref _nameText, value);
	    }

        public DelegateCommand CreateAccountCommand { get; }

        public CreateAccountViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            CreateAccountCommand = new DelegateCommand(CreateAccount);
        }

	    private void CreateAccount()
	    {
	        _dialogService.DisplayAlertAsync("Error", "Your Account could not be created!", "OK");
	    }
	}
}
