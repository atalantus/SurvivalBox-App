using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SurvivalBox.Views;
using Xamarin.Forms;

namespace SurvivalBox
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            //TODO: Check for saved login data
			MainPage = new NavigationPage(new RegistrationKey());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
