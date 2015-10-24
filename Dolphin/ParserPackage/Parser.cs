using Dolphin.BlockPackage;
using Dolphin.TokenPackage;
using Dolphin.VariablePackage;
using System;
using System.Collections.Generic;

namespace Dolphin.ParserPackage
{
    public class Parser
    {
        List<Token> tokens;
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

            #region OLD
            /*
            for(int i = 0; i < tokens.Count; i++)
            {
                //Actual token
                t = tokens[i];

                //Next token
                if (i < tokens.Count - 1)
                    t1 = tokens[i + 1];
                else
                    t1 = null;

                //Next next token
                if (i < tokens.Count - 2)
                    t2 = tokens[i + 2];
                else
                    t2 = null;

                //Next next next token
                if (i < tokens.Count - 3)
                    t3 = tokens[i + 3];
                else
                    t3 = null;

                //Next next next next token
                if (i < tokens.Count - 4)
                    t4 = tokens[i + 4];
                else
                    t4 = null;

                //Next next next next next token
                if (i < tokens.Count - 5)
                    t5 = tokens[i + 5];
                else
                    t5 = null;

                #region Class Parser

                if (t.GetTokenType() == TokenType.CLASS)
                {
                    if(t1.GetTokenType() == TokenType.IDENTIFIER)
                    {
                        if(t2.GetTokenType() == TokenType.OPEN_B)
                        {
                            actualBlock.AddBlock(actualBlock = new Class(t1.GetValue()));
                            i += 2;
                        }
                        else
                        {
                            throw new Exception("You have to open bracket after class declaration");
                        }
                    }
                    else
                    {
                        throw new Exception("You need to set a name for your class");
                    }
                }

                #endregion

                #region Method Parser

                if(t.GetTokenType() == TokenType.FUNC)
                {
                    List<Parameter> parameters = new List<Parameter>();

                    if (t1.GetTokenType() == TokenType.VOID || t1.GetTokenType() == TokenType.STRING || t1.GetTokenType() == TokenType.NUMBER)
                    {
                        if (t2.GetTokenType() == TokenType.IDENTIFIER)
                        {
                            if (t3.GetTokenType() == TokenType.OPEN_P)
                            {
                                
                                if (tokens[i + 5].GetTokenType() == TokenType.OPEN_B)
                                {
                                    actualBlock.AddBlock(actualBlock = new Method(actualBlock, t2.GetValue(), t1.GetValue(), parameters.ToArray()));
                                    i += 2;
                                }
                                else
                                {
                                    throw new Exception("You have to open bracket after function declaration");
                                }
                            }
                            else
                            {
                                //TODO
                                throw new Exception("");
                            }


 


                        }
                        else
                        {
                            throw new Exception("You need to set a name for your function");
                        }
                    }
                    else
                    {
                        throw new Exception("You need to define return type for your function");
                    }
                }

                #endregion

                if (t.GetTokenType() == TokenType.CLOSE_B)
                    actualBlock = actualBlock.GetSuperBlock();
            }*/
            #endregion

            while(HasNextToken())
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

                if(t.GetTokenType() == TokenType.FUNC)
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

                if (t.GetTokenType() == TokenType.CLOSE_B)
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
