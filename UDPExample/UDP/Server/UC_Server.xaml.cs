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
    /// Interaction logic for UC_Server.xaml
    /// </summary>

    public partial class UC_Server : UserControl
    {
        public static UDPServer udpServer;
        //public static List<DataModel> DataModel_Li = new List<DataModel>();
        public ObservableCollection<DataModel> DataRec_List = new ObservableCollection<DataModel>();
        public ObservableCollection<DataModel> DataSend_List = new ObservableCollection<DataModel>();

        int _rownumberserver = 0;
        public byte[] Receivedata;
        public UC_Server()
        {
            Thread ReciveServer = new Thread(UpdateUI);
            ReciveServer.Start();

            InitializeComponent();

            datagrid_ReciveServer.ItemsSource = DataRec_List;
            datagrid_SendServer.ItemsSource = DataSend_List;
        }
        public void UpdateUI()
        {
            while (true)
            {
                #region Recive
                if (udpServer != null)
                {
                    if (udpServer.QReceivePacket.DataCount > 0)
                    {
                        Receivedata = new byte[udpServer.QReceivePacket.Data.Length];
                        Receivedata = udpServer.QReceivePacket.Data;
                        datagrid_ReciveServer.Dispatcher.Invoke((Action)delegate
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
        public int _rownumberSend = 0;
        private void Btn_ConnectionServer_Click(object sender, RoutedEventArgs e)
        {
            if (txt_ip.Text != "" && txt_PortNumber.Text != "")
            {

                if (lbl_Connection_Server.Content.ToString() == "Connect")
                {
                    IPAddress IPAddr = IPAddress.Parse(txt_ip.Text);
                    udpServer = new UDPServer(IPAddr, Convert.ToInt32(txt_PortNumber.Text));
                    lbl_Connection_Server.Content = "DisConnect";

                }
                else if (lbl_Connection_Server.Content.ToString() == "DisConnect")
                {
                    udpServer.Close();
                    lbl_Connection_Server.Content = "Connect";
                }
            }
            else
            {
                MessageBox.Show("Enter Params...");
            }
        }
        public static byte[] data;
        private void Btn_SendServer_Click(object sender, RoutedEventArgs e)
        {
            if (udpServer != null)
            {
            string tempSendData = txt_Senddata.Text;
            char[] char_Packet = tempSendData.ToArray();
            data = new byte[char_Packet.Length];
            data = Encoding.ASCII.GetBytes(char_Packet);
            datagrid_SendServer.Dispatcher.Invoke((Action)delegate
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
                udpServer.QSendPacket.Data = data;
            }
        }
    }
}
