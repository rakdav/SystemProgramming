using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialMonitor
{
    public interface IAsyncSerial
    {
        bool IsOpen { get; }
        void Open(string portName,
            int baudRate = 9600,
            Parity parity = Parity.None,
            int dataBits = 8,
            StopBits stopBits = StopBits.One);
        void Close();
        Task<byte> ReadByteAsync(CancellationToken stoppingToken);
    }
}
