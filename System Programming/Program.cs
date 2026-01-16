using ExtensionLibrary;
using SystemProgramming;
"Введите название окна:".Dump();
string windowName=Console.ReadLine()!;
"Press enter to kill the other app".Dump();
Console.ReadLine();
WindowFinder.KillWindow(windowName);
"Verify if it is dead.".Dump();
"We are done".Dump();
