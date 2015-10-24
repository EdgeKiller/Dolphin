using Dolphin.VariablePackage;

namespace Dolphin.BlockPackage
{
    public class Parameter
    {
        string name;
        VariableType type;

        public Parameter(VariableType type, string name)
        {
            this.type = type;
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }

        public VariableType GetVariableType()
        {
            return type;
        }
    }
}
