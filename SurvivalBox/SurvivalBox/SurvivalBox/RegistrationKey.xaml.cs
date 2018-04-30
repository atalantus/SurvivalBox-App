using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SurvivalBox
{
	public partial class RegistrationKey : ContentPage
	{
		public RegistrationKey()
		{
			InitializeComponent();
		}

        /// <summary>
        /// Clicked Event Handler when the user presses the Activate Button to activate his Registration Key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnActivate(object sender, EventArgs e)
        {
            Debug.Write("RegistrationKey | OnActivate | ");

            var key = registrationKey.Text;

            switch (CheckRegKey(key))
            {
                case "valid":
                    Debug.WriteLine("valid");
                    break;
                case "registered":
                    Debug.WriteLine("registered");
                    break;
                case "invalid":
                    Debug.WriteLine("invalid");
                    break;
                default:
                    Debug.WriteLine("error");
                    break;
            }
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
