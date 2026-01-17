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
        string[] ipadressText;
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

            }
            catch { }
        }
    }
}