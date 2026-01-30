using WMI.Samples;

BiosReader reader = new BiosReader();
reader.ReadBiosDetails();

TemperatureReader temperatureReader = new TemperatureReader();
temperatureReader.TemperatureReaderUsingWMI();

Console.Write("Введите имя компьютера:");
string name = Console.ReadLine()!;
ComputerInfo computerInfo = new ComputerInfo();
computerInfo.GetInfo(name);