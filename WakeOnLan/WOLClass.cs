using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WakeOnLan
{
    public class WOLClass : UdpClient
    {
        public WOLClass():base(){}
        public void SetClientToBrodcastMode()
        {
            if (this.Active)
                this.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 0);
        }
    }
}
