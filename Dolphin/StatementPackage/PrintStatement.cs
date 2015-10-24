using Dolphin.BlockPackage;
using Dolphin.ExpressionPackage;
using System;

namespace Dolphin.StatementPackage
{
    public class PrintStatement : Block
    {
        IExpression expression;

        public PrintStatement(Block superBlock, IExpression expression) : base(null)
        {
            this.expression = expression;
        }

        public override void Execute()
        {
            Console.WriteLine(expression.Evaluate().ToString());
        }
    }
}
