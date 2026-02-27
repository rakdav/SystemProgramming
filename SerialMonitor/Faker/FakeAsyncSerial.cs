using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialMonitor.Faker
{
    public class FakeAsyncSerial:IAsyncSerial
    {
        public bool IsOpen { get; private set; }
        public void Open(string portName, int baudRate = 9600, Parity parity = Parity.None, int dataBits = 8,
            StopBits stopBits = StopBits.One)
        {
            // Nothing
            IsOpen = true;
        }
        public void Close()
        {
            // Nothing
            IsOpen = false;
        }
        public Task<byte> ReadByteAsync(CancellationToken stoppingToken)
        {
            return IsOpen ? Task.FromResult((byte)1) : Task.FromResult((byte)0);
        }
    }
}
