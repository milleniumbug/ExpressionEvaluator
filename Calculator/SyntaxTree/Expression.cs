namespace Calculator.SyntaxTree;

public abstract class Expression<T>
    where T : notnull
{
    public abstract Result<T, EvaluationFailure> Evaluate();
}