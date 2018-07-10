using System.Diagnostics;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using DataParser;
using SurvivalBox.Droid;
using SurvivalBox.Models;
using SurvivalBox.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(BluetoothConnection_Android))]
namespace SurvivalBox.Droid
{
    public class BluetoothConnection_Android : Activity, IBluetoothConnection
    {
        private readonly MainActivity activity;

        public BluetoothConnection_Android()
        {
            Debug.WriteLine("Initialize Bluetooth Connection Android");
            Debug.WriteLine(Android.App.Application.Context);
            var context = Android.App.Application.Context;
            var activity01 = (Activity) context;
            activity = (MainActivity) Android.App.Application.Context;
        }

        public void RequestBluetoothConnection()
        {
            activity.RequestBluetoothConnection();
        }

        public BluetoothStatus IsConnected()
        {
            if (activity.BluetoothAdapter == null)
                return BluetoothStatus.NOT_SUPPORTED;
            else if (!activity.BluetoothEnabled)
                return BluetoothStatus.NOT_ENABLED;
            else
                return BluetoothStatus.CONNECTED;
        }

        public GPSDataRaw GetGPSData()
        {
            if (IsConnected() != BluetoothStatus.CONNECTED) return null;

            throw new System.NotImplementedException();
        }
    }
}