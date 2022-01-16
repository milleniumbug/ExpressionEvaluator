using Calculator;
using Calculator.SyntaxTree;

public static class Operations
{
    public static Result<object, EvaluationFailure> AdditionOperation(object left, object right)
    {
        {
            if (left is double x && right is double y)
            {
                return Result<object, EvaluationFailure>.Of(x + y);
            }
        }
        {
            if (left is int x && right is double y)
            {
                return Result<object, EvaluationFailure>.Of(x + y);
            }
        }
        {
            if (left is double x && right is int y)
            {
                return Result<object, EvaluationFailure>.Of(x + y);
            }
        }
        {
            if (left is int x && right is int y)
            {
                try
                {
                    return Result<object, EvaluationFailure>.Of(checked(x + y));
                }
                catch (OverflowException)
                {
                    return Result<object, EvaluationFailure>.OfError(new EvaluationFailure("overflow", null));                    
                }
            }
        }
        return Result<object, EvaluationFailure>.OfError(new EvaluationFailure("one of the inputs to the operator '+' is not a number", null));
    }
    
    public static Result<object, EvaluationFailure> MultiplicationOperation(object left, object right)
    {
        {
            if (left is double x && right is double y)
            {
                return Result<object, EvaluationFailure>.Of(x * y);
            }
        }
        {
            if (left is int x && right is double y)
            {
                return Result<object, EvaluationFailure>.Of(x * y);
            }
        }
        {
            if (left is double x && right is int y)
            {
                return Result<object, EvaluationFailure>.Of(x * y);
            }
        }
        {
            if (left is int x && right is int y)
            {
                try
                {
                    return Result<object, EvaluationFailure>.Of(checked(x * y));
                }
                catch (OverflowException)
                {
                    return Result<object, EvaluationFailure>.OfError(new EvaluationFailure("overflow", null));                    
                }
            }
        }
        return Result<object, EvaluationFailure>.OfError(new EvaluationFailure("one of the inputs to the operator '*' is not a number", null));
    }
    
    public static Result<object, EvaluationFailure> DivisionOperation(object left, object right)
    {
        {
            if (right.Equals(0))
            {
                return Result<object, EvaluationFailure>.OfError(new EvaluationFailure(
                    "cannot divide by 0", null));
            }
            if (right.Equals(0.0))
            {
                return Result<object, EvaluationFailure>.OfError(new EvaluationFailure(
                    "cannot divide by 0", null));
            }
        }
        {
            if (left is double x && right is double y)
            {
                return Result<object, EvaluationFailure>.Of(x / y);
            }
        }
        {
            if (left is int x && right is double y)
            {
                return Result<object, EvaluationFailure>.Of(x / y);
            }
        }
        {
            if (left is double x && right is int y)
            {
                return Result<object, EvaluationFailure>.Of(x / y);
            }
        }
        {
            if (left is int x && right is int y)
            {
                try
                {
                    return Result<object, EvaluationFailure>.Of(checked(x / y));
                }
                catch (OverflowException)
                {
                    return Result<object, EvaluationFailure>.OfError(new EvaluationFailure("overflow", null));                    
                }
            }
        }
        return Result<object, EvaluationFailure>.OfError(new EvaluationFailure("one of the inputs to the operator '/' is not a number", null));
    }
    
    public static Result<object, EvaluationFailure> SubtractionOperation(object left, object right)
    {
        {
            if (left is double x && right is double y)
            {
                return Result<object, EvaluationFailure>.Of(x - y);
            }
        }
        {
            if (left is int x && right is double y)
            {
                return Result<object, EvaluationFailure>.Of(x - y);
            }
        }
        {
            if (left is double x && right is int y)
            {
                return Result<object, EvaluationFailure>.Of(x - y);
            }
        }
        {
            if (left is int x && right is int y)
            {
                try
                {
                    return Result<object, EvaluationFailure>.Of(checked(x - y));
                }
                catch (OverflowException)
                {
                    return Result<object, EvaluationFailure>.OfError(new EvaluationFailure("overflow", null));                    
                }
            }
        }
        return Result<object, EvaluationFailure>.OfError(new EvaluationFailure("one of the inputs to the operator '-' is not a number", null));
    }

    public static Result<object, EvaluationFailure> NegationOperation(object arg)
    {
        {
            if (arg is int x)
            {
                return Result<object, EvaluationFailure>.Of(-x);
            }
        }
        {
            if (arg is double x)
            {
                return Result<object, EvaluationFailure>.Of(-x);
            }
        }
        
        return Result<object, EvaluationFailure>.OfError(new EvaluationFailure("the input to the unary operator '-' is not a number", null));
    }

    public static Result<object, EvaluationFailure> FactorialOperation(object arg)
    {
        {
            if (arg is int x)
            {
                if (x < 0)
                {
                    return Result<object, EvaluationFailure>.OfError(new EvaluationFailure("the input to factorial is negative", null));
                }
                
                if (x == 0)
                {
                    return Result<object, EvaluationFailure>.Of(1);
                }
                
                try
                {
                    int result = 1;
                    for (int i = 1; i <= x; i++)
                    {
                        result = checked(result * i);
                    }
                    
                    return Result<object, EvaluationFailure>.Of(result);
                }
                catch (OverflowException)
                {
                    return Result<object, EvaluationFailure>.OfError(new EvaluationFailure("factorial overflowed", null));                    
                }
                
            }
        }
        
        return Result<object, EvaluationFailure>.OfError(new EvaluationFailure("the input to factorial is not an integer", null));
    }
}