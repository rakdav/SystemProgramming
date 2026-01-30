using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace WMI.Samples
{
    public class ComputerInfo
    {
        public void GetInfo(string name)
        {
            ConnectionOptions options = new ConnectionOptions();
            options.Impersonation=ImpersonationLevel.Impersonate;
            ManagementScope scope=new ManagementScope($"\\\\{name}\\ROOT\\cimv2",options);
            scope.Connect();
            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope,query);
            ManagementObjectCollection querryCollection=searcher.Get();
            foreach (ManagementObject querry in querryCollection)
            {
                Console.WriteLine($"Имя компьютера: {querry["csname"]}");
                Console.WriteLine($"Windows каталог: {querry["WindowsDirectory"]}");
                Console.WriteLine($"Операционная система: {querry["Caption"]}");
                Console.WriteLine($"Версия: {querry["Version"]}");
                Console.WriteLine($"Производитель: {querry["Manufacturer"]}");
            }
        }
    }
}
