namespace Calculator;

public class EvaluationContext
{
    private Dictionary<string, object?> variables = new Dictionary<string, object?>();

    public object? this[string name]
    {
        get
        {
            if(variables.TryGetValue(name, out var value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }
        set
        {
            variables[name] = value;
        }
    }
}