using System;
using System.Diagnostics;
using System.IO;

namespace Dolphin
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Dolphin Interpreter";

            Stopwatch sw = new Stopwatch();

            if (args.Length < 1)
                Console.WriteLine("You need to specify a file to execute");
            else
            {
                if (File.Exists(args[0]))
                {
                    sw.Start();
                    Dolphin.Interpret(File.ReadAllText(args[0]));
                    sw.Stop();
                    Console.WriteLine("Execution time : " + sw.ElapsedMilliseconds + " ms");
                }
                else
                    Console.WriteLine("File not found : " + args[0]);
            }

            Console.Read();
        }
    }
}
