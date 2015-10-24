using Dolphin.BlockPackage;
using Dolphin.ValuePackage;

namespace Dolphin.ExpressionPackage
{
    public class VariableExpression : IExpression
    {
        string name;
        Block block;

        public VariableExpression(string name, Block block)
        {
            this.name = name;
            this.block = block;
        }

        public Value Evaluate()
        {
            return block.GetVariable(name);
        }
    }
}
