namespace Calculator.Tokenization;

public class OperatorToken : Token
{
    public string Kind { get; }

    public OperatorToken(string kind)
    {
        Kind = kind;
    }
}