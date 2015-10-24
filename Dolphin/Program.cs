using System;
using System.IO;

namespace Dolphin
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Dolphin Testing";

            Dolphin.Interpret(File.ReadAllText("file.do"), "firstClass");

            Console.Read();

        }
    }
}
