namespace Calculator.SyntaxTree;

public class UnaryExpression<TInput, T> : Expression<T>
    where TInput : notnull
    where T : notnull
{
    private readonly Expression<TInput> inputExpression;
    private readonly Func<TInput, Result<T, EvaluationFailure>> operation;

    public UnaryExpression(
        Expression<TInput> inputExpression,
        Func<TInput, Result<T, EvaluationFailure>> operation)
    {
        this.inputExpression = inputExpression;
        this.operation = operation;
    }

    public override Result<T, EvaluationFailure> Evaluate(EvaluationContext context)
    {
        var leftResult = this.inputExpression.Evaluate(context);
        if (leftResult.HasError)
        {
            return Result<T, EvaluationFailure>.OfError(leftResult.Error);
        }

        return this.operation(leftResult.Value);
    }
}