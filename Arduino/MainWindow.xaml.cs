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
                if (receivedData == "Свет включен")
                {
                    Answer.Text = "Свет включен";
                    Answer.Foreground=Brushes.Green;
                }
                else
                {
                    Answer.Text = "Свет выключен";
                    Answer.Foreground = Brushes.Red;
                }
            });
        }

        private void SendToArduino(string data)
        {
            serialPort.WriteLine(data);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            serialPort = new SerialPort(comPort.SelectedValue.ToString(), 9600);

            serialPort.DataReceived += SerialPort_DataReceived;
            try
            {
                serialPort.Open();
                serialPort.Encoding = Encoding.UTF8;
                this.Title = "Порт открыт";
                Label.Content = "Соединение установлено";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        private void Window_Closed(object sender, EventArgs e)
        {
            serialPort?.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Answer.Text = "";
        }


        private void OnOff_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (OnOff.IsChecked == true)
                {
                    OnOff.Content = "On";
                    SendToArduino("On");

                }
                else
                {
                    OnOff.Content = "Off";
                    SendToArduino("Off");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}