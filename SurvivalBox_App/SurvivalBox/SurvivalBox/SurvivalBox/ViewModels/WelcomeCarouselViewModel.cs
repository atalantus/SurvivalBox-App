using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Prism.Navigation;

namespace SurvivalBox.ViewModels
{
	public class WelcomeCarouselViewModel : BindableBase
	{
	    private readonly INavigationService _navigationService;

	    public DelegateCommand GetStartedCommand { get; set; }

        public WelcomeCarouselViewModel(INavigationService navigationService)
        {
            Debug.WriteLine("WelcomeCarousel");
            _navigationService = navigationService;

            GetStartedCommand = new DelegateCommand(GetStarted);
        }

	    private void GetStarted()
	    {
	        _navigationService.NavigateAsync("NavigationPage/RegKey", useModalNavigation:true);
	    }
	}
}
