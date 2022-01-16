namespace Calculator.SyntaxTree;

public class LiteralExpression<T> : Expression<T>
    where T : notnull
{
    private readonly T value;

    public LiteralExpression(T value)
    {
        this.value = value;
    }

    public override Result<T, EvaluationFailure> Evaluate()
    {
        return Result<T, EvaluationFailure>.Of(value);
    }
}