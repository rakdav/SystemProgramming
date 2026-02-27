using ExtensionLibrary;
using Microsoft.Win32;
using System.Security.AccessControl;

"Работа с реестром".Dump(ConsoleColor.Cyan);

var key = Registry.LocalMachine.CreateSubKey(@"Software\SystemsProgrammers\Usage");
var currentUser=Environment.UserName;
var securuty = new RegistrySecurity();
var rule = new RegistryAccessRule(
        currentUser,
        RegistryRights.FullControl,
        InheritanceFlags.None,
        PropagationFlags.None,
        AccessControlType.Allow
    );
securuty.AddAccessRule( rule );
key.SetAccessControl( securuty );
var retrievedKey = key.GetValue("FirstAccess");
if (retrievedKey == null)
{
    key.SetValue(name: "FirstAccess", value: DateTime.UtcNow.ToBinary(),
        valueKind:RegistryValueKind.QWord);
    "First access record now".Dump(ConsoleColor.Cyan);
}
else
{
    if(retrievedKey is long firstAccessAsString)
    {
        var retrivedFirstAccess = DateTime.FromBinary(firstAccessAsString);
        $"Retrived first access:{retrivedFirstAccess}".Dump(ConsoleColor.Cyan);
    }
}

