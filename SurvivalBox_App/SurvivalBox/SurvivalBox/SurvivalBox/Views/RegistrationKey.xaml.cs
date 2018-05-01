using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SurvivalBox.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegistrationKey : ContentPage
	{
		public RegistrationKey ()
		{
			InitializeComponent ();
		}

	    private async void ActivateBtn_OnClicked(object sender, EventArgs e)
	    {
	        var key = RegKey.Text;

	        switch (CheckRegKey(key))
	        {
	            case "valid":
	                await Navigation.PushAsync(new CreateAccount());
	                break;
	            case "registered":
	                await Navigation.PushAsync(new Login());
	                break;
	            case "invalid":
	                await DisplayAlert("Ooops", "Looks like this key is invalid!", "OK");
	                break;
	            default:
	                await DisplayAlert("Error", "Something unexpected happened!\nTry again in a few minutes.", "OK");
	                break;
	        }
        }

	    private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new Login());
	    }

        /// <summary>
        /// Checks if the Registration Key is valid
        /// </summary>
        /// <param name="key">The Registration Key to check</param>
        /// <returns>Result as String</returns>
        private string CheckRegKey(string key)
	    {
	        //TODO: Registration Key mit Datenbank ueberpruefen

	        // WENN (Registration Key frei)
	            // WENN (Benutzer bereits registriert)
	                // RETURN "registered"
	            // SONST
	                // RETURN "valid"
	        // SONST
	            // RETURN "invalid"

	        return "valid";
	    }
	}
}