# HackTheTrafficBluetoothRadarServer
Open source server for BAT-433 devices.

## How-To:
```cs
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
```

## Team:
Shravan Rajinikanth [@shravan2x](https://github.com/shravan2x)<br>
Souvik Banerjee [@souvik1997](https://github.com/souvik1997)<br>
Scott Cao [@caoscott](https://github.com/caoscott)
