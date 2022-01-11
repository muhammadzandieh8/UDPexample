using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace UDPExample
{
    #region StorageOfArrayByte
    public class StorageOfArrayByte
    {
        volatile Queue<byte[]> StorageArrayByte;
        public StorageOfArrayByte()
        {
            StorageArrayByte = new Queue<byte[]>();
        }
        public byte[] Data
        {
            get
            {
                lock (this)
                {
                    while (StorageArrayByte.Count == 0) Monitor.Wait(this);
                    return StorageArrayByte.Dequeue();
                }
            }
            set
            {
                lock (this)
                {
                    StorageArrayByte.Enqueue(value);
                    Monitor.PulseAll(this);
                }
            }
        }
        public int DataCount { get { return StorageArrayByte.Count; } }
        public void DataReallocate() { StorageArrayByte.TrimExcess(); }
        public void Clear() { StorageArrayByte.Clear(); }
    }
    #endregion
    public class UDPServer
    {
        private byte isAlive = 0xFF;
        private IPAddress IPAddr = IPAddress.Any;
        private int portNum = 0;
        public bool stopThread = false;
        UdpClient udpServer;
        IPEndPoint remoteEP;
        public StorageOfArrayByte QReceivePacket;
        public StorageOfArrayByte QSendPacket;
        bool isConnected = false;
        public UDPServer(IPAddress iIPAddr, int iPortNum)
        {
            QReceivePacket = new StorageOfArrayByte();
            QSendPacket = new StorageOfArrayByte();
            this.portNum = iPortNum;
            this.IPAddr = iIPAddr;

            Thread thread_receiveFunc = new Thread(ReceiveFunc);
            thread_receiveFunc.Start();

            Thread thread_sendFunc = new Thread(SendFunc);
            thread_sendFunc.Start();
        }
        private void ReceiveFunc()
        {
            udpServer = new UdpClient(portNum);
            remoteEP = new IPEndPoint(IPAddr, portNum);
            Stopwatch SW = new Stopwatch();
            SW.Restart();
            while (!stopThread)
            {
                try
                {
                    byte[] data = udpServer.Receive(ref remoteEP); // listen on port 11000

                    QReceivePacket.Data = data;

                    if (data.Length > 0)
                    {
                        isConnected = true;
                        if (SW.Elapsed.TotalSeconds > 1)
                        {
                            QSendPacket.Data = new byte[] { isAlive };
                            SW.Restart();
                        }
                    }
                    else
                    {
                        isConnected = false;
                    }
                    
                }
                catch
                {
                    isConnected = false;
                }
            }
        }
        private void SendFunc()
        {
            byte[] data;
            while (!stopThread)
            {
                if (QSendPacket.DataCount > 0)
                {
                    data = QSendPacket.Data;
                    if (isConnected)
                    {
                        udpServer.Send(data, data.Length, remoteEP);
                    }
                }
                else
                {
                    Thread.Sleep(10);
                }
            }
        }
    }
}
