using Dolphin.ExpressionPackage;
using Dolphin.VariablePackage;
using System;

namespace Dolphin.ValuePackage
{
    public class Value : IExpression
    {
        VariableType type;
        object value;

        public Value(VariableType type, object value)
        {
            this.type = type;
            this.value = value;
        }

        public VariableType GetVariableType()
        {
            return type;
        }

        public override string ToString()
        {
            return Convert.ToString(value);
        }

        public double ToNumber()
        {
            return Convert.ToDouble(value.ToString());
        }

        public object GetValue()
        {
            return value;
        }

        public void SetValue(object value)
        {
            this.value = value;
        }

        public Value Evaluate()
        {
            return this;
        }
    }
}
