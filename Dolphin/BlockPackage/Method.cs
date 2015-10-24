using Dolphin.ValuePackage;
using Dolphin.VariablePackage;
using System;

namespace Dolphin.BlockPackage
{
    public class Method : Block
    {
        string name, returnType;
        Parameter[] parameters;
        Value returnValue;

        public Method(Block superBlock, string name, string returnType, Parameter[] parameters) : base(superBlock)
        {
            this.name = name;
            this.returnType = returnType;
            this.parameters = parameters;
        }

        public string GetName()
        {
            return name;
        }

        public string GetReturnType()
        {
            return returnType;
        }

        public Parameter[] GetParameters()
        {
            return parameters;
        }

        public override void Execute()
        {
            Invoke();
        }

        public void SetReturnValue(Value value)
        {
            returnValue = value;
        }

        public Value Invoke(params Value[] values)
        {
            if (values.Length != parameters.Length)
                throw new Exception("Wrong number of values for parameters.");

            for (int i = 0; i < values.Length && i < parameters.Length; i++)
            {
                Parameter p = parameters[i];
                Value v = values[i];

                if (p.GetVariableType() != v.GetVariableType())
                {
                    throw new Exception("Parameter " + p.GetName() + " should be " + p.GetVariableType() + ". Got " + v.GetVariableType());
                }

                AddVariable(new Variable(this, p.GetVariableType(), p.GetName(), v.GetValue()));
            }

            foreach(Block b in GetSubBlocks())
            {
                b.Execute();

                if (returnValue != null)
                    break;
            }

            if (returnValue == null && returnType != "void")
                throw new Exception("No return value");

            Value localReturnValue = returnValue;
            returnValue = null;
            return localReturnValue;
        }
    }
}
