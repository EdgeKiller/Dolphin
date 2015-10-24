using Dolphin.BlockPackage;
using Dolphin.VariablePackage;

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
            superBlock.AddVariable(new Variable(superBlock, type, name, null));
        }
    }
}
