using ExtensionLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace WMI.Samples
{
    public class BiosReader
    { 
        public void ReadBiosDetails() {
            ManagementScope scope=new ManagementScope("\\\\.\\ROOT\\cimv2");
            scope.Connect();
            ObjectQuery query=new ObjectQuery("SELECT * FROM Win32_BIOS");
            using ManagementObjectSearcher searcher=new ManagementObjectSearcher(scope,query);
            foreach (var o in searcher.Get())
            {
                var queryObj = (ManagementObject)o;
                "______________________".Dump(ConsoleColor.Yellow);
                "BIOS Infornmation".Dump(ConsoleColor.Yellow);
                "______________________".Dump(ConsoleColor.Yellow);
                $"Manufactorer:{queryObj["Manufacturer"]}".Dump(ConsoleColor.Yellow);
                $"Name:{queryObj["Name"]}".Dump(ConsoleColor.Yellow);
                $"Version:{queryObj["Version"]}".Dump(ConsoleColor.Yellow);
            }
        }
    }
}
