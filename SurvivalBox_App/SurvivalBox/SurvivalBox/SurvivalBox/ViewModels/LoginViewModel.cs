using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using Prism.Services;

namespace SurvivalBox.ViewModels
{
	public class LoginViewModel : BindableBase
	{
        #region Properties
        private INavigationService _navigationService;
	    private IPageDialogService _dialogService;

        private string _eMailText = string.Empty;
        public string EMailText
        {
            get => _eMailText;
            set => SetProperty(ref _eMailText, value);
        }

        private string _eMailFeedbackText = string.Empty;
        public string EMailFeedbackText
        {
            get => _eMailFeedbackText;
            set => SetProperty(ref _eMailFeedbackText, value);
        }

	    private string _passwordText = string.Empty;
        public string PasswordText
        {
            get => _passwordText;
            set => SetProperty(ref _passwordText, value);
        }

	    private string _passwordFeedbackText = string.Empty;
        public string PasswordFeedbackText
        {
            get => _passwordFeedbackText;
            set => SetProperty(ref _passwordFeedbackText, value);
        }

        public DelegateCommand LoginCommand { get; }
        public DelegateCommand ResetPasswordCommand { get; }
        #endregion

        public LoginViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            LoginCommand = new DelegateCommand(Login);
            ResetPasswordCommand = new DelegateCommand(ResetPassword);
        }

	    private void Login()
	    {
            //TODO: Login

	        _dialogService.DisplayAlertAsync("Error", "Your E-Mail adress or password is wrong!", "OK");
	    }

	    private void ResetPassword()
	    {
	        _navigationService.NavigateAsync("PasswordReset");
	    }
	}
}
