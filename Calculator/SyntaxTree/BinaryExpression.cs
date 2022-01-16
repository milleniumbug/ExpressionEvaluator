namespace Calculator.SyntaxTree;

public class BinaryExpression<TLeft, TRight, T> : Expression<T>
    where TLeft : notnull
    where TRight : notnull
    where T : notnull
{
    private readonly Expression<TLeft> leftExpression;
    private readonly Expression<TRight> rightExpression;
    private readonly Func<TLeft, TRight, Result<T, EvaluationFailure>> operation;

    public BinaryExpression(
        Expression<TLeft> leftExpression,
        Expression<TRight> rightExpression,
        Func<TLeft, TRight, Result<T, EvaluationFailure>> operation)
    {
        this.leftExpression = leftExpression;
        this.rightExpression = rightExpression;
        this.operation = operation;
    }


    public override Result<T, EvaluationFailure> Evaluate()
    {
        var leftResult = this.leftExpression.Evaluate();
        if (leftResult.HasError)
        {
            return Result<T, EvaluationFailure>.OfError(leftResult.Error);
        }
        
        var rightResult = this.rightExpression.Evaluate();
        if (rightResult.HasError)
        {
            return Result<T, EvaluationFailure>.OfError(rightResult.Error);
        }
        
        return this.operation(leftResult.Value, rightResult.Value);
    }
}