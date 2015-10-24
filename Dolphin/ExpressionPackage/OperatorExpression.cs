using Dolphin.ValuePackage;
using Dolphin.VariablePackage;
using System;

namespace Dolphin.ExpressionPackage
{
    public class OperatorExpression
    {
        IExpression left, right;
        string op;

        public OperatorExpression(IExpression left, string op, IExpression right)
        {
            this.left = left;
            this.right = right;
            this.op = op;
        }

        public Value Evaluate()
        {
            Value leftVal = left.Evaluate();
            Value rightVal = right.Evaluate();

            switch (op)
            {
                case "==":
                    if (leftVal.GetVariableType() == VariableType.NUMBER && rightVal.GetVariableType() == VariableType.NUMBER)
                        return new Value(VariableType.NUMBER, (leftVal.ToNumber() == rightVal.ToNumber()) ? 1 : 0);
                    else
                        return new Value(VariableType.NUMBER, leftVal.ToString().Equals(rightVal.ToString()) ? 1 : 0);


                case "+":
                    if (leftVal.GetVariableType() == VariableType.NUMBER && rightVal.GetVariableType() == VariableType.NUMBER)
                        return new Value(VariableType.NUMBER, leftVal.ToNumber() + rightVal.ToNumber());
                    else
                        return new Value(VariableType.STRING, leftVal.ToString() + rightVal.ToString());


                case "-":
                    if (leftVal.GetVariableType() == VariableType.NUMBER && rightVal.GetVariableType() == VariableType.NUMBER)
                        return new Value(VariableType.NUMBER, leftVal.ToNumber() - rightVal.ToNumber());
                    else
                        throw new Exception("Cannot apply this operator for this variable type.");


                case "*":
                    if (leftVal.GetVariableType() == VariableType.NUMBER && rightVal.GetVariableType() == VariableType.NUMBER)
                        return new Value(VariableType.NUMBER, leftVal.ToNumber() * rightVal.ToNumber());
                    else
                        throw new Exception("Cannot apply this operator for this variable type.");


                case "/":
                    if (leftVal.GetVariableType() == VariableType.NUMBER && rightVal.GetVariableType() == VariableType.NUMBER)
                        return new Value(VariableType.NUMBER, leftVal.ToNumber() / rightVal.ToNumber());
                    else
                        throw new Exception("Cannot apply this operator for this variable type.");


                case "++":
                    if (leftVal.GetVariableType() == VariableType.NUMBER && rightVal.GetVariableType() == VariableType.NUMBER)
                        return new Value(VariableType.NUMBER, leftVal.ToNumber() + rightVal.ToNumber());
                    else
                        throw new Exception("Cannot apply this operator for this variable type.");


                case "--":
                    if (leftVal.GetVariableType() == VariableType.NUMBER && rightVal.GetVariableType() == VariableType.NUMBER)
                        return new Value(VariableType.NUMBER, leftVal.ToNumber() - rightVal.ToNumber());
                    else
                        throw new Exception("Cannot apply this operator for this variable type.");


                case "<":
                    if (leftVal.GetVariableType() == VariableType.NUMBER && rightVal.GetVariableType() == VariableType.NUMBER)
                        return new Value(VariableType.NUMBER, (leftVal.ToNumber() < rightVal.ToNumber()) ? 1 : 0);
                    else
                        throw new Exception("Cannot apply this operator for this variable type.");


                case ">":
                    if (leftVal.GetVariableType() == VariableType.NUMBER && rightVal.GetVariableType() == VariableType.NUMBER)
                        return new Value(VariableType.NUMBER, (leftVal.ToNumber() > rightVal.ToNumber()) ? 1 : 0);
                    else
                        throw new Exception("Cannot apply this operator for this variable type.");


                case "<=":
                    if (leftVal.GetVariableType() == VariableType.NUMBER && rightVal.GetVariableType() == VariableType.NUMBER)
                        return new Value(VariableType.NUMBER, (leftVal.ToNumber() <= rightVal.ToNumber()) ? 1 : 0);
                    else
                        throw new Exception("Cannot apply this operator for this variable type.");


                case ">=":
                    if (leftVal.GetVariableType() == VariableType.NUMBER && rightVal.GetVariableType() == VariableType.NUMBER)
                        return new Value(VariableType.NUMBER, (leftVal.ToNumber() >= rightVal.ToNumber()) ? 1 : 0);
                    else
                        throw new Exception("Cannot apply this operator for this variable type.");


                case "!=":
                    if (leftVal.GetVariableType() == VariableType.NUMBER && rightVal.GetVariableType() == VariableType.NUMBER)
                        return new Value(VariableType.NUMBER, (leftVal.ToNumber() != rightVal.ToNumber()) ? 1 : 0);
                    else
                        return new Value(VariableType.NUMBER, leftVal.ToString().Equals(rightVal.ToString()) ? 0 : 1);
            }
            throw new Exception("Unknown operator : " + op);
        }

    }
}
