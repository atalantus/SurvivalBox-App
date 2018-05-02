using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SurvivalBox.Views.Start
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent ();
		}

	    private void LoginBtn_OnClicked(object sender, EventArgs e)
	    {
            //TODO: Check Login
	        throw new NotImplementedException();
	    }

	    private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
	    {
	        Navigation.PushAsync(new PasswordReset());
	    }
	}
}