namespace Dolphin.TokenPackage
{
    public class Token
    {
        TokenType type;
        string value;

        public Token(TokenType type, string value)
        {
            this.type = type;
            this.value = value;
        }

        public TokenType GetTokenType()
        {
            return type;
        }

        public string GetValue()
        {
            return value;
        }

        public override string ToString()
        {
            if (value == "\n")
                value = string.Empty;
            return "[" + type + "] " + value;
        }
    }
}
