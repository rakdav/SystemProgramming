using ExtensionLibrary;
using SystemProgramming;
Console.WriteLine("Введите название окна:");
string windowName=Console.ReadLine()!;
"Press enter to kill the other app".Dump();
Console.ReadLine();
WindowFinder.KillWindow(windowName);
"Verify if it is dead.".Dump();
"We are done".Dump();
