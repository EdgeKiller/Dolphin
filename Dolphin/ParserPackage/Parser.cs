using Dolphin.BlockPackage;
using Dolphin.ExpressionPackage;
using Dolphin.StatementPackage;
using Dolphin.TokenPackage;
using Dolphin.ValuePackage;
using Dolphin.VariablePackage;
using System;
using System.Collections.Generic;

namespace Dolphin.ParserPackage
{
    public class Parser
    {
        List<Token> tokens = new List<Token>();
        int Position;

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;

            Position = 0;
        }

        Block baseBlock = new Block(null);

        public Block Parse()
        {
            Token t, lt;
            Block actualBlock = baseBlock;

            while (HasNextToken())
            {
                t = NextToken();
                lt = null;

                #region Class Parser

                if (t.GetTokenType() == TokenType.CLASS)
                {
                    if (NextToken().GetTokenType() == TokenType.IDENTIFIER)
                    {
                        if (NextToken().GetTokenType() == TokenType.OPEN_B)
                        {
                            actualBlock.AddBlock(actualBlock = new Class(PreviousToken(2).GetValue()));
                            if (PreviousToken(2).GetValue() == "program")
                                baseBlock.SetMainClass(actualBlock as Class);

                            continue;
                        }
                        else
                            throw new Exception("You have to open bracket after class declaration");
                    }
                    else
                        throw new Exception("You need to set a name for your class");
                }

                #endregion

                #region Function Parser

                else if (t.GetTokenType() == TokenType.FUNC)
                {
                    lt = NextToken();
                    if (lt.GetTokenType() == TokenType.VOID || lt.GetTokenType() == TokenType.NUMBER || lt.GetTokenType() == TokenType.STRING)
                    {
                        if (NextToken().GetTokenType() == TokenType.IDENTIFIER)
                        {
                            if (NextToken().GetTokenType() == TokenType.OPEN_P)
                            {
                                List<Parameter> parameters = new List<Parameter>();
                                while (NextToken().GetTokenType() != TokenType.CLOSE_P)
                                {
                                    parameters.Add(new Parameter((VariableType)PreviousToken(1).GetTokenType(), NextToken().GetValue()));
                                }

                                if (NextToken().GetTokenType() == TokenType.OPEN_B)
                                {
                                    actualBlock.AddBlock(actualBlock = new Method(actualBlock, PreviousToken(parameters.Count * 2 + 4).GetValue(), PreviousToken(parameters.Count * 2 + 5).GetValue(), parameters.ToArray()));
                                    continue;
                                }
                                else
                                    throw new Exception("You have to open bracket after function declaration");
                            }
                            else
                                throw new Exception(""); //TODO
                        }
                        else
                            throw new Exception("You need to set a name for your function");
                    }
                    else
                        throw new Exception(""); //TODO

                }

                #endregion

                else if (t.GetTokenType() == TokenType.STRING || t.GetTokenType() == TokenType.NUMBER)
                {
                    if (NextToken().GetTokenType() == TokenType.IDENTIFIER)
                    { 
                        actualBlock.AddBlock(new VarDeclarationStatement(actualBlock, PreviousToken(1).GetValue(), (VariableType)t.GetTokenType()));

                        if (NextToken().GetTokenType() == TokenType.ASSIGN)
                        {
                            actualBlock.AddBlock(new AssignStatement(actualBlock, PreviousToken(2).GetValue(), "=", Exp(actualBlock)));
                        }
                        else
                        {
                            Position--;
                        }
                        continue;
                    }
                    else
                        throw new Exception("You need to set a name for your variable");
                }

                else if (t.GetTokenType() == TokenType.IDENTIFIER)
                {
                    t = NextToken();
                    if (t.GetTokenType() == TokenType.ASSIGN)
                    {
                        actualBlock.AddBlock(new AssignStatement(actualBlock, PreviousToken(2).GetValue(), "=", Exp(actualBlock)));
                    }
                    else if (PreviousToken(2).GetValue().Equals("print") && t.GetTokenType() == TokenType.OPEN_P)
                    {
                        actualBlock.AddBlock(new PrintStatement(actualBlock, Exp(actualBlock)));
                    }
                }

                else if (t.GetTokenType() == TokenType.CLOSE_B)
                    actualBlock = actualBlock.GetSuperBlock();
            }

            return baseBlock;
        }

        bool HasNextToken()
        {
            return Position < tokens.Count;
        }

        Token PreviousToken(int offset)
        {
            return tokens[Position - offset];
        }

        Token NextToken()
        {
            Token t;
            if (Position < tokens.Count)
            {
                t = tokens[Position];
                Position++;
                return t;
            }
            else
                return null;
        }

        IExpression Exp(Block block)
        {
            IExpression expression = Atomic(block);

            while (Match(TokenType.OPERATOR) || Match(TokenType.COMPARISON_OP))
            {
                string op = Last(1).GetValue();
                IExpression right;
                if (op == "++" || op == "--")
                    right = new Value(VariableType.NUMBER, 1);
                else
                    right = Atomic(block);
                expression = new OperatorExpression(expression, op, right);
            }

            return expression;
        }

        IExpression Atomic(Block block)
        {
            if (Match(TokenType.IDENTIFIER))
            {
                return new VariableExpression(Last(1).GetValue(), block);
            }
            else if (Match(TokenType.NUMBER_LITERAL))
            {
                return new Value(VariableType.NUMBER, double.Parse(Last(1).GetValue()));
            }
            else if (Match(TokenType.STRING_LITERAL))
            {
                return new Value(VariableType.STRING, Last(1).GetValue());
            }
            else if (Match(TokenType.OPEN_P))
            {
                IExpression expression = Exp(block);
                Consume(TokenType.CLOSE_P);
                return expression;
            }

            throw new Exception("Couldn't parse.");
        }

        bool Match(TokenType type)
        {
            if (Get(0).GetTokenType() != type) return false;
            Position++;
            return true;
        }

        bool Match(TokenType type1, TokenType type2)
        {
            if (Get(0).GetTokenType() != type1) return false;
            if (Get(1).GetTokenType() != type2) return false;
            Position += 2;
            return true;
        }

        bool Match(string name)
        {
            if (Get(0).GetTokenType() != TokenType.IDENTIFIER) return false;
            if (!Get(0).GetValue().Equals(name)) return false;
            Position++;
            return true;
        }

        Token Consume(TokenType type)
        {
            if (Get(0).GetTokenType() != type) throw new Exception("Expected " + type + ".");
            return tokens[Position++];
        }

        Token Consume(string name)
        {
            if (!Match(name)) throw new Exception("Expected " + name + ".");
            return Last(1);
        }

        Token Last(int offset)
        {
            if (Position - offset < 0) return new Token(TokenType.NULL, "\n");
            return tokens[Position - offset];
        }

        Token Get(int offset)
        {
            if (Position + offset >= tokens.Count) return new Token(TokenType.EOF, "");
            return tokens[Position + offset];
        }

    }
}
