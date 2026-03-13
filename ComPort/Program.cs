using System;
using System.IO.Ports;
using System.Text;
SerialPort serialPort=new SerialPort("COM3",9600);
serialPort.DataReceived += SerialPort_DataReceived;
try
{
    serialPort.Open();
    serialPort.Encoding=Encoding.UTF8;
    Console.WriteLine("COM port opened. Enter 'quit' to exit");
    while (true)
    {
        string input=Console.ReadLine()!;
        if (input.ToLower() == "quit") break;
        SendToArduino(input);
    }
}
catch(Exception ex)
{

}
finally
{
    serialPort.Close();
}
void SerialPort_DataReceived(object sender,SerialDataReceivedEventArgs e)
{
    string receivedData=serialPort.ReadLine();
    Console.WriteLine("Received from Arduino:"+ receivedData);
}
void SendToArduino(string data) 
{
    serialPort.WriteLine(data);
    Console.WriteLine("Send to Arduino:"+data);
}
