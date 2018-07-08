using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Prism.Navigation;
using Prism.Services;
using SurvivalBox.Models;
using SurvivalBox.Services;

namespace SurvivalBox.ViewModels
{
	public class CreateSessionViewModel : ActivityIndicatorViewModelBase
	{
	    private readonly INavigationService _navigationService;
	    private readonly IPageDialogService _dialogService;

	    private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

	    public DelegateCommand CreateSessionCommand { get; set; }

        public CreateSessionViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            CreateSessionCommand = new DelegateCommand(CreateSession);
        }

	    private async void CreateSession()
	    {
            Debug.WriteLine("Create Session!");

	        if (!Regex.IsMatch(Name, @"[0-9a-zA-Z]"))
	        {
	            await _dialogService.DisplayAlertAsync("Error", "Please enter a valid name for your Session!", "OK");
	            return;
	        }

	        using (new ActivityIndicatorScope(this, true))
	        {
                Debug.WriteLine("Creating Session: " + Name);
	            var newSession = new Session()
	            {
	                Name = Name
	            };
	            await SessionManager.Instance.CreateSession(newSession);
	            await _navigationService.NavigateAsync("Main", useModalNavigation: true);
            }
        }

	    protected override void OnRefresh()
	    {
	        throw new NotImplementedException();
	    }
	}
}
