using System;
using System.Diagnostics;
using SurvivalBox.Models;
using Xamarin.Forms;

namespace SurvivalBox.Services
{
    public delegate void GPSDataReceivedEventHandler(GPSData data);

    public class BoxConnection
    {
        private static BoxConnection _instance;
        public static BoxConnection Instance => _instance ?? (_instance = new BoxConnection());

        public event GPSDataReceivedEventHandler GPSDataReceived;
        private bool _stopDataCollection;
        private readonly IBluetoothConnection _bluetoothConnection;

        private BoxConnection()
        {
            Debug.WriteLine("BoxConnection");

            try
            {
                _bluetoothConnection = DependencyService.Get<IBluetoothConnection>();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.InnerException?.Message);
                Debug.WriteLine(e.InnerException?.StackTrace);
            }

            Debug.WriteLine("Bluetooth service: " + _bluetoothConnection);

            if (_bluetoothConnection.IsConnected() != BluetoothStatus.CONNECTED)
                _bluetoothConnection.RequestBluetoothConnection();
        }

        public void StartDataCollection()
        {
            Device.StartTimer(TimeSpan.FromMinutes(2), () =>
            {
                if (_stopDataCollection)
                {
                    // Stop
                    _stopDataCollection = false;
                    return false;
                }

                // Get GPS Data from Box
                var rawData = _bluetoothConnection.GetGPSData();
                var gpsData = new GPSData(rawData.position.latitude.value, rawData.position.longitude.value, rawData.trueVelocity, rawData.time);

                // Call GPSDataReceived Event
                GPSDataReceived?.Invoke(gpsData);
                return true;
            });
        }

        public BluetoothStatus GetBluetoothStatus()
        {
            return _bluetoothConnection.IsConnected();
        }

        public void StopDataCollection()
        {
            _stopDataCollection = true;
        }
    }
}