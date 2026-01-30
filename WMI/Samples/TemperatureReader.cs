using ExtensionLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace WMI.Samples
{
    public class TemperatureReader
    {
        public void TemperatureReaderUsingWMI() {
            var scope = "root\\WMI";
            var query = "SELECT * FROM MSAcpi_ThermalZoneTemperature";
            var searcher=new ManagementObjectSearcher(scope, query);
            try
            {
                foreach (var item in searcher.Get())
                {
                    var obj = (ManagementObject)item;
                    var temperature = Convert.ToDouble(obj["CurrentTemperature"])/10-273.15;
                    $"CPU Temperature:{temperature:F2} C".Dump();
                }
            }
            catch(ManagementException)
            {
                "BIOS не поддерживает API".Dump(ConsoleColor.Red);
            }
        }
    }
}
