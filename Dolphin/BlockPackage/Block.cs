using Dolphin.VariablePackage;
using System.Collections.Generic;

namespace Dolphin.BlockPackage
{
    public class Block
    {
        Block superBlock;
        List<Block> subBlocks;
        List<Variable> variables;
        Method mainMethod;

        public Block(Block superBlock)
        {
            this.superBlock = superBlock;
            subBlocks = new List<Block>();
            variables = new List<Variable>();
        }

        public Block GetSuperBlock()
        {
            return superBlock;
        }

        public List<Block> GetSubBlocks()
        {
            return subBlocks;
        }

        public void AddBlock(Block block)
        {
            subBlocks.Add(block);
        }

        public Variable GetVariable(string name)
        {
            foreach(Variable v in variables)
            {
                if (v.GetName().Equals(name))
                    return v;
            }
            return null;
        }

        public void AddVariable(Variable v)
        {
            variables.Add(v);
        }

        public bool VariableExists(string name)
        {
            foreach (Variable v in variables)
            {
                if (v.GetName().Equals(name))
                    return true;
            }
            return false;
        }

        public void SetMainMethod(Method mainMethod)
        {
            this.mainMethod = mainMethod;
        }

        public Method GetMainMethod()
        {
            return mainMethod;
        }

        public virtual void Execute()
        {

        }
    }
}
