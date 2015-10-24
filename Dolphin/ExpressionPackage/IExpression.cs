using Dolphin.ValuePackage;

namespace Dolphin.ExpressionPackage
{
    public interface IExpression
    {
        Value Evaluate();
    }
}
