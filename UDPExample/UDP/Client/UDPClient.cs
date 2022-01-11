using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace UDPExample
{
    public class UDPClient
    {
        private byte isAlive = 0xFF;

        public StorageOfArrayByte QSendPacket = new StorageOfArrayByte();
        public StorageOfArrayByte QReceivePacket = new StorageOfArrayByte();

        public bool stopThread = false;

        private IPAddress _ipAddress = IPAddress.Any;
        private int _portNum = 0;
        public UdpClient udpClient = null;
        IPEndPoint remoteEP;
        bool isConnected = false;

        public UDPClient(IPAddress ipAddress, int portNum)
        {
            QReceivePacket = new StorageOfArrayByte();
            QSendPacket = new StorageOfArrayByte();
            _portNum = portNum;
            _ipAddress = ipAddress;

            Thread ThreadCheckServerConnection = new Thread(checkServerConnection);
            ThreadCheckServerConnection.Start();
        }

        public void checkServerConnection()
        {
            int state = 0;
            while (true)
            {
                switch (state)
                {
                    case 0:
                        {
                            bool isDone = findExistConnection(_ipAddress);
                            if (isDone)
                            {
                                state = 1;
                            }
                            else
                            {
                                Thread.Sleep(1000);
                            }

                            if (_ipAddress.Address == IPAddress.Parse("127.0.0.1").Address)
                            {
                                state = 1;
                            }
                        }
                        break;
                    case 1:
                        {
                            Thread ThreadReceiveFunc = new Thread(ReceiveFunc);
                            ThreadReceiveFunc.Start();

                            Thread ThreadSendFunc = new Thread(SendFunc);
                            ThreadSendFunc.Start();

                            state = 2;
                        }
                        break;
                    case 2:
                        {
                            Thread.Sleep(10000);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        bool findExistConnection(IPAddress IPAddrServer1)
        {
            //string ipAddress = "";
            var ni = NetworkInterface.GetAllNetworkInterfaces();
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                return false;
            }
            foreach (NetworkInterface item in ni)
            {
                // Only display informatin for interfaces that support IPv4.
                if (item.Supports(NetworkInterfaceComponent.IPv4) == false)
                {
                    continue;
                }
                if (item.OperationalStatus == OperationalStatus.Up) //&& item.NetworkInterfaceType == ?
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork & !IPAddress.IsLoopback(ip.Address))
                        {
                            if ((ip.Address.Address & 0x00FFFFFF) == (IPAddrServer1.Address & 0x00FFFFFF))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        public void ReceiveFunc()
        {
            Stopwatch SW = new Stopwatch();
            SW.Restart();
            Stopwatch SWTimeOut = new Stopwatch();
            SWTimeOut.Restart();
            int state = 1;
            while (!stopThread)
            {
                switch (state)
                {
                    case 1:
                        {
                            if (udpClient != null)
                            {
                                udpClient.Client.Dispose();
                                udpClient.Close();
                            }
                            try
                            {
                                remoteEP = new IPEndPoint(_ipAddress, _portNum);
                                udpClient = new UdpClient();
                                udpClient.Connect(remoteEP);
                                udpClient.Client.ReceiveTimeout = 10000;
                                udpClient.Client.ReceiveBufferSize = 2000000000;

                                isConnected = true;
                                state = 2;
                            }
                            catch
                            {
                                isConnected = false;
                                state = 1;
                            }

                        }
                        break;
                    case 2:
                        {
                            if (udpClient.Available > 1)
                            {
                                SWTimeOut.Restart();

                                try
                                {
                                    var receivedData = udpClient.Receive(ref remoteEP);

                                    QReceivePacket.Data = receivedData;
                                    isConnected = true;
                                }
                                catch
                                {
                                    isConnected = false;
                                    state = 1;
                                }

                            }
                            else
                            {
                                Thread.Sleep(1);
                            }
                            if (SW.Elapsed.TotalSeconds > 1)
                            {
                                if (isConnected)
                                {
                                    QSendPacket.Data = new byte[] { isAlive };
                                }
                                SW.Restart();
                            }
                            if (SWTimeOut.Elapsed.TotalSeconds > 10)
                            {
                                state = 1;
                                SWTimeOut.Restart();
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        public void SendFunc()
        {
            byte[] data;
            while (!stopThread)
            {
                if (QSendPacket.DataCount > 0)
                {
                    data = QSendPacket.Data;
                    if (isConnected)
                    {
                        udpClient.Send(data, data.Length);
                    }
                }
                else
                {
                    Thread.Sleep(50);
                }
            }
        }
    }
}
