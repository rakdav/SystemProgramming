using static System.Threading.Thread;
namespace ExtensionLibrary
{
    public static class StringExtensions
    {
        public static string? Dump(this string? value, ConsoleColor printColor = ConsoleColor.Cyan)
        {
            lock(new object())
            {
                var oldColor=Console.ForegroundColor;
                Console.ForegroundColor = printColor;
                Console.WriteLine($"({CurrentThread.ManagedThreadId}):{value}");
                Console.ForegroundColor = oldColor;
                return value;
            }
        }
    }
}
