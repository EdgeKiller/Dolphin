using Dolphin.BlockPackage;
using Dolphin.ValuePackage;

namespace Dolphin.VariablePackage
{
    public class Variable : Value
    {
        Block block;
        string name;

        public Variable(Block block, VariableType type, string name, object value) : base(type, value)
        {
            this.block = block;
            this.name = name;
        }

        public Block GetBlock()
        {
            return block;
        }

        public string GetName()
        {
            return name;
        }
    }
}
