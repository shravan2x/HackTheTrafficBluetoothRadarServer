using System;

namespace HackTheTrafficBluetoothRadarServer
{
    internal class Program
    {
        public static void Main()
        {
            BluetoothRadarServer radarServer = new BluetoothRadarServer();
            radarServer.RadarEntryHandler += HandleNewRadarEntry;
            radarServer.Start();
        }

        public static void HandleNewRadarEntry(RadarEntry radarEntry)
        {
            // Do whatever you want with the radar entries here; maybe put them in a DB.
            Console.WriteLine($"{radarEntry.DetectionTimeAsUnixTimestamp},{radarEntry.AntennaCode},{radarEntry.DeviceIdentifier}");
        }
    }
}
