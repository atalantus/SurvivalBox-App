using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Services;

namespace SurvivalBox.ViewModels
{
	public class PasswordResetViewModel : BindableBase
	{
	    private IPageDialogService _dialogService;

        private string _eMailValue = String.Empty;
        public string EMailValue
        {
            get => _eMailValue;
            set => SetProperty(ref _eMailValue, value);
        }

	    public DelegateCommand ResetPasswordCommand { get; }

        public PasswordResetViewModel(IPageDialogService dialogService)
        {
            _dialogService = dialogService;

            ResetPasswordCommand = new DelegateCommand(ResetPassword);
        }

	    private void ResetPassword()
	    {
            //TODO: Reset Password
	        _dialogService.DisplayAlertAsync("Password was resetted",
	            "We sent you an E-Mail with the new password! Log in to change your password again.", "OK");
	    }
	}
}
