using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SurvivalBox.Views
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
	        throw new NotImplementedException();
	    }

	    private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
	    {
	        throw new NotImplementedException();
	    }
	}
}