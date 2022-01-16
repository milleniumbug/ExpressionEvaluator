namespace Calculator.SyntaxTree;

public class VariableExpression<T> : Expression<T>
    where T : notnull
{
    private readonly string variableName;

    public VariableExpression(string variableName)
    {
        this.variableName = variableName;
    }

    public override Result<T, EvaluationFailure> Evaluate(EvaluationContext context)
    {
        var value = context[this.variableName];
        if (value == null)
        {
            return Result<T, EvaluationFailure>.OfError(new EvaluationFailure("variable does not exist", null));
        }
        if (value is not T x)
        {
            return Result<T, EvaluationFailure>.OfError(new EvaluationFailure("variable is not of expected type", null));
        }

        return Result<T, EvaluationFailure>.Of(x);
    }
}