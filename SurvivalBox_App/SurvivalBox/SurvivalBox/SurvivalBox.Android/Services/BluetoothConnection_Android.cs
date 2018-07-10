using System.Diagnostics;
using Android.App;
using DataParser;
using SurvivalBox.Droid.Services;
using SurvivalBox.Models;
using SurvivalBox.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(BluetoothConnection_Android))]
namespace SurvivalBox.Droid.Services
{
    public class BluetoothConnection_Android : Activity, IBluetoothConnection
    {
        private readonly MainActivity _activity;

        public BluetoothConnection_Android()
        {
            Debug.WriteLine("Initialize Bluetooth Connection Android");
            // TODO: Reference MainActivity
        }

        public void RequestBluetoothConnection()
        {
            _activity.RequestBluetoothConnection();
        }

        public BluetoothStatus IsConnected()
        {
            if (_activity.BluetoothAdapter == null)
                return BluetoothStatus.NOT_SUPPORTED;
            else if (!_activity.BluetoothEnabled)
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