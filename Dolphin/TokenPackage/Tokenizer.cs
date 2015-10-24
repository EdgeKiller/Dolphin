using System.Collections.Generic;

namespace Dolphin.TokenPackage
{
    public static class Tokenizer
    {

        public static List<Token> Tokenize(string content)
        {
            List<Token> tokens = new List<Token>();

            TokenizeState state = TokenizeState.DEFAULT;

            string token = string.Empty;

            string charTokens = "=+-*/<>(){};";//"\n=+-*/<>(){};";
            TokenType[] tokenTypes =
            {
                /*TokenType.EOL,*/ TokenType.ASSIGN,
                TokenType.OPERATOR, TokenType.OPERATOR,
                TokenType.OPERATOR, TokenType.OPERATOR,
                TokenType.COMPARISON_OP, TokenType.COMPARISON_OP,
                TokenType.OPEN_P, TokenType.CLOSE_P,
                TokenType.OPEN_B, TokenType.CLOSE_B,
                TokenType.SEMICOLON
            };

            char c = '\0', pc = '\0', nc = '\0';

            for (int i = 0; i < content.Length; i++)
            {
                //Actual char
                c = content[i];

                //Previous char
                if (i > 0)
                    pc = content[i - 1];
                else
                    pc = '\0';

                //Next char
                if (i < content.Length - 1)
                    nc = content[i + 1];
                else
                    nc = '\0';

                switch (state)
                {
                    case TokenizeState.DEFAULT:
                        int indexC = charTokens.IndexOf(c);

                        if (indexC != -1)
                        {
                            if ((c == '=' || c == '>' || c == '<' || c == '!') && nc == '=' )
                            {
                                tokens.Add(new Token(TokenType.COMPARISON_OP, c.ToString() + nc.ToString()));
                                i++;
                            }
                            else if ((c == '+' || c == '-' || c == '/' || c == '*') && nc == '=')
                            {
                                tokens.Add(new Token(TokenType.ASSIGN, c.ToString() + nc.ToString()));
                                i++;
                            }
                            else if ((c == '+' && nc == '+') || (c == '-' && nc == '-'))
                            {
                                tokens.Add(new Token(TokenType.SPECIAL_OP, c.ToString() + nc.ToString()));
                                i++;
                            }
                            else
                            {
                                tokens.Add(new Token(tokenTypes[charTokens.IndexOf(c)], char.ToString(c)));
                            }
                        }
                        else if (char.IsLetter(c))
                        {
                            token += c;
                            state = TokenizeState.IDENTIFIER;
                        }
                        else if (char.IsDigit(c))
                        {
                            token += c;
                            state = TokenizeState.NUMBER;
                        }
                        else if (c == '"')
                            state = TokenizeState.STRING;
                        else if (c == '\'')
                            state = TokenizeState.COMMENT;
                        break;

                    case TokenizeState.IDENTIFIER:
                        if (char.IsLetterOrDigit(c))
                            token += c;
                        else
                        {
                            TokenType localType = TokenType.IDENTIFIER;
                            switch (token)
                            {
                                case "class":
                                    localType = TokenType.CLASS;
                                    break;
                                case "func":
                                    localType = TokenType.FUNC;
                                    break;
                                case "number":
                                    localType = TokenType.NUMBER;
                                    break;
                                case "string":
                                    localType = TokenType.STRING;
                                    break;
                            }
                            tokens.Add(new Token(localType, token));
                            token = "";
                            state = TokenizeState.DEFAULT;
                            i--;
                        }
                        break;

                    case TokenizeState.NUMBER:
                        if (char.IsDigit(c) || (c == ',' && !token.Contains(",")))
                            token += c;
                        else
                        {
                            tokens.Add(new Token(TokenType.NUMBER_LITERAL, token));
                            token = "";
                            state = TokenizeState.DEFAULT;
                            i--;
                        }
                        break;

                    case TokenizeState.STRING:
                        if (c == '"' && pc != '\\')
                        {
                            tokens.Add(new Token(TokenType.STRING_LITERAL, token));
                            token = "";
                            state = TokenizeState.DEFAULT;
                        }
                        else
                        {
                            if (pc == '\\')
                                token = token.Remove(token.Length - 1);
                            token += c;
                        }
                        break;

                    case TokenizeState.COMMENT:
                        if (c == '\n' || c == '\'')
                            state = TokenizeState.DEFAULT;
                        break;
                }
            }
            return tokens;
        }

    }
}
