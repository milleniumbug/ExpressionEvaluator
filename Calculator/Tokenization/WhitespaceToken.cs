namespace Calculator.Tokenization;

public class WhitespaceToken : Token
{
    public string Whitespace { get; }

    public WhitespaceToken(string whitespace)
    {
        Whitespace = whitespace;
    }
}