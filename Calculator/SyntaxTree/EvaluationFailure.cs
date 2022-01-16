namespace Calculator.SyntaxTree;

public class EvaluationFailure
{
    public string Reason { get; }
    
    public EvaluationFailure? InnerReason { get; }

    public EvaluationFailure(string reason, EvaluationFailure? innerReason)
    {
        Reason = reason;
        InnerReason = innerReason;
    }
}