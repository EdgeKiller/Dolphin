using Dolphin.BlockPackage;
using Dolphin.StatementPackage;
using Dolphin.TokenPackage;
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
        //public Method mainMethod;

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

                else if(t.GetTokenType() == TokenType.FUNC)
                {
                    lt = NextToken();
                    if (lt.GetTokenType() == TokenType.VOID || lt.GetTokenType() == TokenType.NUMBER || lt.GetTokenType() == TokenType.STRING)
                    {
                        if(NextToken().GetTokenType() == TokenType.IDENTIFIER)
                        {
                            if(NextToken().GetTokenType() == TokenType.OPEN_P)
                            {
                                List<Parameter> parameters = new List<Parameter>();
                                while(NextToken().GetTokenType() != TokenType.CLOSE_P)
                                {
                                    parameters.Add(new Parameter((VariableType)PreviousToken(1).GetTokenType(), NextToken().GetValue()));
                                }

                                if (NextToken().GetTokenType() == TokenType.OPEN_B)
                                {
                                    actualBlock.AddBlock(actualBlock = new Method(actualBlock, PreviousToken(parameters.Count * 2 + 4).GetValue(), PreviousToken(parameters.Count * 2 + 5).GetValue(), parameters.ToArray()));
                                    if ((actualBlock as Method).GetName().Equals("main") && (actualBlock.GetSuperBlock() as Class).GetName().Equals("program"))
                                        baseBlock.SetMainMethod(actualBlock as Method);
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
                        lt = NextToken();
                        if (lt.GetTokenType() == TokenType.SEMICOLON)
                        {
                            actualBlock.AddBlock(new VarDeclarationStatement(actualBlock, PreviousToken(1).GetValue(), (VariableType)t.GetTokenType()));
                            continue;
                        }
                        else if (lt.GetTokenType() == TokenType.ASSIGN)
                        {

                        }
                    }
                    else
                        throw new Exception("You need to set a name for your variable");
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

    }
}
