using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Prism;
using Prism.Ioc;
using SurvivalBox.Services;
using Debug = System.Diagnostics.Debug;

namespace SurvivalBox.Droid
{
    [Activity(Label = "SurvivalBox", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private static readonly int REQUEST_ENABLE_BT = 1;
        public BluetoothAdapter BluetoothAdapter;
        public bool BluetoothEnabled;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }

        public void RequestBluetoothConnection()
        {
            Debug.WriteLine("RequestBluetooth Android");
            BluetoothAdapter = BluetoothAdapter.DefaultAdapter;

            if (!BluetoothAdapter.IsEnabled)
            {
                Debug.WriteLine("Request Bluetooth from user");
                var enableBtIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
                StartActivityForResult(enableBtIntent, REQUEST_ENABLE_BT);
            }
            else
            {
                Debug.WriteLine("Request Bluetooth: Bluetooth enabled");
                BluetoothEnabled = true;
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == REQUEST_ENABLE_BT)
            {
                // Bluetooth enable request handler
                BluetoothEnabled = resultCode == Result.Ok;
                Debug.WriteLine("Bluetooth enabled: " + BluetoothEnabled);
            }
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {
            // Register any platform specific implementations
            container.Register<IBluetoothConnection, BluetoothConnection_Android>();
        }
    }
}

