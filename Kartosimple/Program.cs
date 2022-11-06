using Kartotrezor.Executor;
using Kartotrezor.Utils;

namespace Kartosimple
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Kartosimple.exe (DRAW | PRINT) [PATH_TO_INPUT_FILE]");
                Console.WriteLine("\tEx: Kartosimple DRAW MyFile.txt");
                Console.WriteLine("\tEx: Kartosimple PRINT MyFile.txt");
                Console.ReadKey();
                return;
            }

            var path = args[1];
            var lines = File.ReadAllLines(path);

            var map = new MapExecutor().ExecuteMap(new CommandParser().ParseCommands(lines));

            Console.WriteLine(args[0].ToUpper() == "DRAW" ? map.DrawMap() : map.PrintMap());
            Console.ReadKey();
        }
    }
}