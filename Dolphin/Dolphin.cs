using Dolphin.BlockPackage;
using Dolphin.ParserPackage;
using Dolphin.TokenPackage;
using Dolphin.VariablePackage;
using System;
using System.Collections.Generic;

namespace Dolphin
{
    public class Dolphin
    {

        public static void Interpret(string content)
        {
            List<Token> tokens = Tokenizer.Tokenize(content);

            Parser parser = new Parser(tokens);

            Block result = parser.Parse();

            result.GetMainClass().Execute();

            //Console.WriteLine(result.GetMainClass().GetVariable("test").GetValue());
            //Console.WriteLine(result.GetMainClass().GetSubBlocks()[1].GetVariable("test").GetValue());
            //Console.WriteLine((result.GetSubBlocks()[0].GetSubBlocks()[0] as Method).GetVariable("test").GetVariableType());
            //Console.WriteLine((result.GetSubBlocks()[0] as Class).GetVariable("test").GetVariableType());
            //Console.WriteLine((result.GetSubBlocks()[0] as Class).GetVariable("test"));
            //Console.WriteLine(result.GetMainClass().GetVariable("test").GetValue());
            //Console.WriteLine((result.GetSubBlocks()[0].GetSubBlocks()[0] as Method).GetName());
        }

    }
}
