using Dolphin.BlockPackage;
using Dolphin.ExpressionPackage;
using Dolphin.ValuePackage;
using Dolphin.VariablePackage;
using System;

namespace Dolphin.StatementPackage
{
    public class AssignStatement : Block
    {
        string name, type;
        IExpression value;
        Block superBlock;

        public AssignStatement(Block superBlock, string name, string type, IExpression value) : base(superBlock)
        {
            this.name = name;
            this.type = type;
            this.value = value;
            this.superBlock = superBlock;
        }

        public override void Execute()
        {
            Block localBlock = superBlock;

            while(localBlock != null)
            {

                if (localBlock.VariableExists(name))
                    break;
                localBlock = localBlock.GetSuperBlock();
            }

            if (type == "=")
                localBlock.GetVariable(name).SetValue(value.Evaluate());
            else if (type == "+=")
            {
                if (localBlock.GetVariable(name).GetVariableType() == VariableType.NUMBER)
                    localBlock.GetVariable(name).SetValue(new Value(VariableType.NUMBER, localBlock.GetVariable(name).ToNumber() + value.Evaluate().ToNumber()));
                else
                    localBlock.GetVariable(name).SetValue(new Value(VariableType.STRING, localBlock.GetVariable(name).ToString() + value.Evaluate().ToString()));
            }
            else if (type == "-=")
            {
                localBlock.GetVariable(name).SetValue(new Value(VariableType.NUMBER, localBlock.GetVariable(name).ToNumber() - value.Evaluate().ToNumber()));
            }
            else if (type == "*=")
            {
                localBlock.GetVariable(name).SetValue(new Value(VariableType.NUMBER, localBlock.GetVariable(name).ToNumber() * value.Evaluate().ToNumber()));
            }
            else if (type == "/=")
            {
                localBlock.GetVariable(name).SetValue(new Value(VariableType.NUMBER, localBlock.GetVariable(name).ToNumber() / value.Evaluate().ToNumber()));
            }
            else if (type == "++")
            {
                localBlock.GetVariable(name).SetValue(new Value(VariableType.NUMBER, localBlock.GetVariable(name).ToNumber() + value.Evaluate().ToNumber()));
            }
            else if (type == "--")
            {
                localBlock.GetVariable(name).SetValue(new Value(VariableType.NUMBER, localBlock.GetVariable(name).ToNumber() - value.Evaluate().ToNumber()));
            }
        }
    }
}
