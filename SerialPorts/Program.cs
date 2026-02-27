using System.IO.Ports;

using var serialPort=new SerialPort("COM1",9600,Parity.None,8,StopBits.One);
serialPort.Open();
try
{
    serialPort.Write([42], 0, 1);
}
finally
{
    serialPort.Close();
}
