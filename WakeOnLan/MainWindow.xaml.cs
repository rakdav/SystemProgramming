using System.Globalization;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WakeOnLan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(int destIp, int srcIP, byte[] macAddr, ref uint physicalAddrLen);
        List<TableHost> _host = new List<TableHost>();
        string hostname = "";
        IPHostEntry entry;
        string[] ipToString = new string[4];
        string[] ipaddressText;
        string[] hostnameText;
        string[] macaddressText;
        public MainWindow()
        {
            InitializeComponent();
            string host = System.Net.Dns.GetHostName();
            System.Net.IPAddress ip = System.Net.Dns.GetHostByName(host).AddressList[3];
            label3.Content = ip.ToString();
            ipToString = ip.ToString().Split('.');
        }
        private void WakeFunction(string MAC_ADDRESS)
        {
            WOLClass client = new WOLClass();
            client.Connect(new IPAddress(0xffffffff), 0x2fff);
            client.SetClientToBrodcastMode();
            int counter = 0;
            byte[] bytes = new byte[1024];
            for (int y = 0; y < 6; y++)
                bytes[counter++] = 0xFF;
            for (int y = 0; y < 16; y++)
            {
                int i = 0;
                for (int z = 0; z < 6; z++)
                {
                    bytes[counter++] = byte.Parse(MAC_ADDRESS.Substring(i, 2), NumberStyles.HexNumber);
                    i += 2;
                }
            }
            int reterned_value = client.Send(bytes, 1024);
        }
        private void GetInform(string textName)
        {
            string IP_Address = "";
            string HostName = "";
            string MacAddress = "";
            try
            {
                entry = Dns.GetHostEntry(textName);
                foreach (IPAddress a in entry.AddressList)
                {
                    IP_Address = a.ToString();
                    break;
                }
                HostName = entry.HostName;
                IPAddress dst = IPAddress.Parse(textName);
                byte[] macAddr = new byte[6];
                uint macAddrLen = (uint)macAddr.Length;
                if(SendARP(BitConverter.ToInt32(dst.GetAddressBytes(), 0), 0, macAddr, ref macAddrLen) != 0)
                    throw new InvalidOperationException("SendARP failed.");
                string[] str = new string[(int)macAddrLen];
                for (int i = 0; i < macAddrLen; i++)
                    str[i] = macAddr[i].ToString("x2");
                MacAddress = string.Join(":", str);
                Dispatcher.Invoke(new Action(() =>
                {

                    _host.Add(new TableHost() { ipAdress = IP_Address, nameComputer = HostName, MacAdress = MacAddress });
                    listView1.ItemsSource = null;
                    listView1.ItemsSource = _host;
                }));
            }
            catch { }
        }

        private void Scan_Click(object sender, RoutedEventArgs e)
        {
            int i = int.Parse(ipToString[0]);
            int j = int.Parse(ipToString[1]);
            int k = 110;
            for (int m = 1; m < 255; m++)
            {
                Thread _thread = new Thread(() => GetInform(string.Format("{0}.{1}.{2}.{3}", i.ToString(), j.ToString(), k.ToString(), m.ToString())));
                _thread.Start();
             }
        }
    }
}