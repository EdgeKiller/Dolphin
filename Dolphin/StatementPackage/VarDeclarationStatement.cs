using Dolphin.BlockPackage;
using Dolphin.VariablePackage;
using System;

namespace Dolphin.StatementPackage
{
    public class VarDeclarationStatement : Block
    {
        string name;
        VariableType type;
        Block superBlock;

        public VarDeclarationStatement(Block superBlock, string name, VariableType type) : base(superBlock)
        {
            this.superBlock = superBlock;
            this.name = name;
            this.type = type;
        }

        public override void Execute()
        {
            if (superBlock.VariableExists(name))
                throw new Exception("Variable already exists with this name");
            superBlock.AddVariable(new Variable(superBlock, type, name, null));
        }
    }
}
