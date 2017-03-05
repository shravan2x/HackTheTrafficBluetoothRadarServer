using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace HackTheTrafficBluetoothRadarServer
{
    public class BluetoothRadarServer
    {
        private const int BluetoothRadarServerUdpPort = 11001;
        private readonly UdpClient _serverSocket;
        private volatile bool _runServer;

        public delegate void RadarEntryHandlerDelegate(RadarEntry radarEntry);
        public event RadarEntryHandlerDelegate RadarEntryHandler;

        public BluetoothRadarServer()
        {
            _serverSocket = new UdpClient(new IPEndPoint(IPAddress.Any, BluetoothRadarServerUdpPort));
        }

        public void Start()
        {
            if (_runServer)
                return;
            _runServer = true;

            Thread serverThread = new Thread(RunServer);
            serverThread.IsBackground = false;
            serverThread.Start();
        }

        public void Stop()
        {
            _runServer = false;
        }

        private void RunServer()
        {
            while (_runServer)
            {
                IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                byte[] buf = _serverSocket.Receive(ref sender);

                string dataString = Encoding.ASCII.GetString(buf, 0, buf.Length);
                string[] dataParts = dataString.Split(',');

                RadarEntry curRadarEntry = new RadarEntry { DetectionTime = DateTime.Parse(dataParts[0]), AntennaCode = dataParts[1], DeviceIdentifier = dataParts[2] };
                RadarEntryHandler?.Invoke(curRadarEntry);
            }
        }
    }

    public class RadarEntry
    {
        private static readonly DateTime EpochTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public DateTime DetectionTime { get; set; }
        public string AntennaCode { get; set; }
        public string DeviceIdentifier { get; set; }

        public ulong DetectionTimeAsUnixTimestamp => (ulong) (DetectionTime - EpochTime).TotalSeconds;
    }
}
