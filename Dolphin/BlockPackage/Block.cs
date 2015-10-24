using Dolphin.StatementPackage;
using Dolphin.VariablePackage;
using System;
using System.Collections.Generic;

namespace Dolphin.BlockPackage
{
    public class Block
    {
        Block superBlock;
        List<Block> subBlocks;
        List<Variable> variables;
        Class mainClass;

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
            foreach (Variable v in GetAllVariables())
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

        public List<Variable> GetAllVariables()
        {
            List<Variable> result = new List<Variable>();
            Block b = this;
            while (true)
            {
                foreach (Variable v in b.GetVariables())
                {
                    result.Add(v);
                }
                b = b.GetSuperBlock();
                if (b == null)
                    break;
            }
            return result;
        }

        public List<Variable> GetVariables()
        {
            return variables;
        }

        public bool VariableExists(string name)
        {
            foreach (Variable v in GetAllVariables())
            {
                if (v.GetName().Equals(name))
                    return true;
            }
            return false;
        }

        public void SetMainClass(Class mainClass)
        {
            this.mainClass = mainClass;
        }

        public Class GetMainClass()
        {
            return mainClass;
        }

        public virtual void Execute()
        {

        }
    }
}
