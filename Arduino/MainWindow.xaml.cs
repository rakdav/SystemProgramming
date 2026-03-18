using System.IO.Ports;
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

namespace Arduino
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SerialPort serialPort;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                string receivedData = serialPort.ReadLine();
                Answer.Text += "Received from Arduino" + receivedData;
            });
        }

        private void SendToArduino(string data)
        {
            serialPort.WriteLine(data);
            Answer.Text += "Send to Arduino:" + data;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            serialPort = new SerialPort(comPort.SelectedValue.ToString(), 9600);

            serialPort.DataReceived += SerialPort_DataReceived;
            try
            {
                serialPort.Open();
                serialPort.Encoding = Encoding.UTF8;
            }
            catch (Exception ex)
            {

            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                SendToArduino(Message.Text);
            }
            catch (Exception ex) 
            {

            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            serialPort?.Close();
        }
    }
}