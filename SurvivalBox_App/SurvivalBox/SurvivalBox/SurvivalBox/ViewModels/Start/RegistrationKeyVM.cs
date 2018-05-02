using System.Windows.Input;
using Xamarin.Forms;

namespace SurvivalBox.ViewModels.Start
{
    public class RegistrationKeyVM : VMBase
    {
        public ICommand ValidateRegKeyCmd { get; }
        public ICommand LogInCmd { get; }
        
        public RegistrationKeyVM()
        {
            Title = "Registration Key";

            ValidateRegKeyCmd = new Command(ValidateRegKey);
            LogInCmd = new Command(LogIn);
        }

        private void ValidateRegKey()
        {
        
        }

        private void LogIn()
        {

        }
    }
}