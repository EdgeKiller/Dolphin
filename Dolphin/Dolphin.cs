using Dolphin.BlockPackage;
using Dolphin.ParserPackage;
using Dolphin.TokenPackage;
using System;
using System.Collections.Generic;

namespace Dolphin
{
    public class Dolphin
    {

        public static void Interpret(string content)
        {
            List<Token> tokens = Tokenizer.Tokenize(content);

            /*foreach(Token t in tokens)
            {
                Console.WriteLine(t);
            }*/

            Parser parser = new Parser(tokens);

            Block result = parser.Parse();

            result.GetMainMethod().Execute();

            Console.WriteLine((result.GetSubBlocks()[0].GetSubBlocks()[0] as Method).GetVariable("var").GetVariableType());
            //Console.WriteLine((result.GetSubBlocks()[0].GetSubBlocks()[0] as Method).GetName());
        }

    }
}
