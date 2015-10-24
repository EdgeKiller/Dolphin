using Dolphin.BlockPackage;
using Dolphin.ParserPackage;
using Dolphin.TokenPackage;
using System;
using System.Collections.Generic;

namespace Dolphin
{
    public class Dolphin
    {

        public static void Interpret(string content, string mainClass)
        {
            List<Token> tokens = Tokenizer.Tokenize(content);

            Parser parser = new Parser(tokens);

            Block result = parser.Parse();

            //Console.WriteLine((result.GetSubBlocks()[0].GetSubBlocks()[0] as Method).GetName());
            Console.WriteLine((result.GetSubBlocks()[0].GetSubBlocks()[0] as Method).GetName());
        }

    }
}
