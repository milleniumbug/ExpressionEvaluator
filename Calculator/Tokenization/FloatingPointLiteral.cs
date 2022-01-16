namespace Calculator.Tokenization;

public class FloatingPointLiteral : Token
{
    public double Value { get; }

    public FloatingPointLiteral(double value)
    {
        Value = value;
    }
}