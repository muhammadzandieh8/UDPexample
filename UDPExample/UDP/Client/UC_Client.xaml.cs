using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Threading;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;

namespace UDPExample
{
    /// <summary>
    /// Interaction logic for UC_Client.xaml
    /// </summary>
    public partial class UC_Client : UserControl
    {
        public static UDPClient udpClient;

        //public static List<DataModel> DataModel_Li = new List<DataModel>();
        public ObservableCollection<DataModel> DataRec_List = new ObservableCollection<DataModel>();
        public ObservableCollection<DataModel> DataSend_List = new ObservableCollection<DataModel>();

        int _rownumberserver = 0;
        public byte[] Receivedata;
        public UC_Client()
        {
            Thread ReciveServer = new Thread(UpdateUI);
            ReciveServer.Start();

            InitializeComponent();
            datagrid_ReciveClient.ItemsSource = DataRec_List;
            datagrid_SendClient.ItemsSource = DataSend_List;
        }
        public void UpdateUI()
        {
            while (true)
            {
                #region Recive
                if (udpClient != null)
                {
                    if (udpClient.QReceivePacket.DataCount > 0)
                    {
                        Receivedata = new byte[udpClient.QReceivePacket.Data.Length];
                        Receivedata = udpClient.QReceivePacket.Data;
                        datagrid_ReciveClient.Dispatcher.Invoke((Action)delegate
                        {
                            DataRec_List.Add(new DataModel()
                            {
                                Id = _rownumberserver,
                                Data = BitConverter.ToString(Receivedata),
                                DateTime = DateTime.Now,
                                LenghtData = Receivedata.Length,
                            });
                            _rownumberserver++;
                        });
                    }
                }
                else
                {
                    Thread.Sleep(100);
                }
                #endregion
            }
        }
        private void Btn_ConnectionClient_Click(object sender, RoutedEventArgs e)
        {
            if (lbl_Connection_Client.Content.ToString() == "Connect")
            {
                //do some stuff
                IPAddress IPAddr = IPAddress.Parse(txt_ip.Text);
                udpClient = new UDPClient(IPAddr, Convert.ToInt32(txt_PortNumber.Text));
                lbl_Connection_Client.Content = "DisConnect";
                
            }
            else if (lbl_Connection_Client.Content.ToString() == "DisConnect")
            {
                //udpClient
                udpClient.udpClient.Close();
               
                //udpClient.udpClient.Client.Dispose();
                //udpClient.udpClient.Client.Close();

                lbl_Connection_Client.Content = "Connect";
            }


        }
        public static byte[] data;
        public int _rownumberSend = 0;

        private void Btn_SendClient_Click(object sender, RoutedEventArgs e)
        {
            string tempSendData = txt_Senddata.Text;
            char[] char_Packet = tempSendData.ToArray();
            data = new byte[char_Packet.Length];
            data = Encoding.ASCII.GetBytes(char_Packet);      
            udpClient.QSendPacket.Data = data;
            datagrid_SendClient.Dispatcher.Invoke((Action)delegate
            {
                DataSend_List.Add(new DataModel()
                {
                    Id = _rownumberSend,
                    Data = BitConverter.ToString(data),
                    DateTime = DateTime.Now,
                    LenghtData = data.Length,
                });
                _rownumberSend++;
            });

        }
    }
}
