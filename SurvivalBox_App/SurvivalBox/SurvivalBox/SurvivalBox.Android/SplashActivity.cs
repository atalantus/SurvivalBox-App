using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SurvivalBox.Droid
{
    [Activity(Theme = "@style/MainTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            var startupWork = new Task(SimulateStartup);
            startupWork.Start();
        }

        // Background work that happens behind the splash screen
        private void SimulateStartup()
        {
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}