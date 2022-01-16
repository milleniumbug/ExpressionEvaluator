namespace Calculator.SyntaxTree;

public class SetVariableExpression<T> : Expression<T>
    where T : notnull
{
    private readonly string variableName;
    private readonly Expression<T> rightExpression;

    public SetVariableExpression(string variableName, Expression<T> rightExpression)
    {
        this.variableName = variableName;
        this.rightExpression = rightExpression;
    }

    public override Result<T, EvaluationFailure> Evaluate(EvaluationContext context)
    {
        var evalResult = this.rightExpression.Evaluate(context);
        if (evalResult.HasError)
        {
            return evalResult;
        }

        context[variableName] = evalResult.Value;

        return evalResult;
    }
}