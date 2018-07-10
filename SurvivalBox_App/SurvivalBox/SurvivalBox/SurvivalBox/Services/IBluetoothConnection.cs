using DataParser;
using SurvivalBox.Models;

namespace SurvivalBox.Services
{
    public interface IBluetoothConnection
    {
        BluetoothStatus IsConnected();
        void RequestBluetoothConnection();
        GPSDataRaw GetGPSData();
    }
}