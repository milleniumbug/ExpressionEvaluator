namespace Calculator.Tokenization;

public abstract class IntegralLiteral : Token
{
    public int Value { get; }

    public IntegralLiteral(int value)
    {
        Value = value;
    }
}