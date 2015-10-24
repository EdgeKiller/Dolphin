namespace Dolphin.TokenPackage
{
    public enum TokenType
    {
        IDENTIFIER, CLASS, FUNC, VOID, NUMBER = 0, STRING = 1, 
        NUMBER_LITERAL, STRING_LITERAL, OPEN_P, CLOSE_P,
        OPEN_B, CLOSE_B, EOL, ASSIGN, OPERATOR,
        SPECIAL_OP, COMPARISON_OP, SEMICOLON
    }
}
