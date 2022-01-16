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
    
    public static Result<object, EvaluationFailure> XorOperation(object left, object right)
    {
        {
            if (left is int x && right is int y)
            {
                return Result<object, EvaluationFailure>.Of(x ^ y);
            }
        }
        return Result<object, EvaluationFailure>.OfError(new EvaluationFailure("one of the inputs to the operator 'xor' is not an integral number", null));
    }
    
    public static Result<object, EvaluationFailure> OrOperation(object left, object right)
    {
        {
            if (left is int x && right is int y)
            {
                return Result<object, EvaluationFailure>.Of(x | y);
            }
        }
        return Result<object, EvaluationFailure>.OfError(new EvaluationFailure("one of the inputs to the operator 'or' is not an integral number", null));
    }
    
    public static Result<object, EvaluationFailure> AndOperation(object left, object right)
    {
        {
            if (left is int x && right is int y)
            {
                return Result<object, EvaluationFailure>.Of(x & y);
            }
        }
        return Result<object, EvaluationFailure>.OfError(new EvaluationFailure("one of the inputs to the operator 'and' is not an integral number", null));
    }
    
    public static Result<object, EvaluationFailure> NotOperation(object arg)
    {
        {
            if (arg is int x)
            {
                return Result<object, EvaluationFailure>.Of(~x);
            }
        }
        return Result<object, EvaluationFailure>.OfError(new EvaluationFailure("one of the inputs to the operator 'not' is not an integral number", null));
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
        if (arg is int x)
        {
            if (x < 0)
            {
                return Result<object, EvaluationFailure>.OfError(
                    new EvaluationFailure("the input to factorial is negative", null));
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

        return Result<object, EvaluationFailure>.OfError(new EvaluationFailure("the input to factorial is not an integer", null));
    }

    public static Result<object, EvaluationFailure> Logarithm10Operation(object arg)
    {
        double value;
        if (arg is int x)
        {
            value = x;
        }
        else if (arg is double y)
        {
            value = y;
        }
        else
        {
            return Result<object, EvaluationFailure>.OfError(
                new EvaluationFailure("the input to decimal logarithm is not a number", null));
        }

        if (value < 0)
        {
            return Result<object, EvaluationFailure>.OfError(
                new EvaluationFailure("the input to decimal logarithm is negative", null));
        }

        return Result<object, EvaluationFailure>.Of(Math.Log10(value));
    }
    
    public static Result<object, EvaluationFailure> NaturalLogarithmOperation(object arg)
    {
        double value;
        if (arg is int x)
        {
            value = x;
        }
        else if (arg is double y)
        {
            value = y;
        }
        else
        {
            return Result<object, EvaluationFailure>.OfError(
                new EvaluationFailure("the input to natural logarithm is not a number", null));
        }

        if (value < 0)
        {
            return Result<object, EvaluationFailure>.OfError(
                new EvaluationFailure("the input to natural logarithm is negative", null));
        }

        return Result<object, EvaluationFailure>.Of(Math.Log(value));
    }

    public static Result<object, EvaluationFailure> SquareRootOperation(object arg)
    {
        double value;
        if (arg is int x)
        {
            value = x;
        }
        else if (arg is double y)
        {
            value = y;
        }
        else
        {
            return Result<object, EvaluationFailure>.OfError(
                new EvaluationFailure("the input to square root function is not a number", null));
        }

        if (value < 0)
        {
            return Result<object, EvaluationFailure>.OfError(
                new EvaluationFailure("the input to square root function is negative", null));
        }

        return Result<object, EvaluationFailure>.Of(Math.Sqrt(value));
    }

    public static Result<object, EvaluationFailure> SineOperation(object arg)
    {
        double value;
        if (arg is int x)
        {
            value = x;
        }
        else if (arg is double y)
        {
            value = y;
        }
        else
        {
            return Result<object, EvaluationFailure>.OfError(
                new EvaluationFailure("the input to sine function is not a number", null));
        }

        return Result<object, EvaluationFailure>.Of(Math.Sin(value));
    }
    
    public static Result<object, EvaluationFailure> CosineOperation(object arg)
    {
        double value;
        if (arg is int x)
        {
            value = x;
        }
        else if (arg is double y)
        {
            value = y;
        }
        else
        {
            return Result<object, EvaluationFailure>.OfError(
                new EvaluationFailure("the input to cosine function is not a number", null));
        }

        return Result<object, EvaluationFailure>.Of(Math.Cos(value));
    }
    
    public static Result<object, EvaluationFailure> TangentOperation(object arg)
    {
        double value;
        if (arg is int x)
        {
            value = x;
        }
        else if (arg is double y)
        {
            value = y;
        }
        else
        {
            return Result<object, EvaluationFailure>.OfError(
                new EvaluationFailure("the input to tangent function is not a number", null));
        }

        return Result<object, EvaluationFailure>.Of(Math.Tan(value));
    }
    
    public static Result<object, EvaluationFailure> CotangentOperation(object arg)
    {
        double value;
        if (arg is int x)
        {
            value = x;
        }
        else if (arg is double y)
        {
            value = y;
        }
        else
        {
            return Result<object, EvaluationFailure>.OfError(
                new EvaluationFailure("the input to cotangent function is not a number", null));
        }

        var tan = Math.Tan(value);
        if (tan == 0.0)
        {
            return Result<object, EvaluationFailure>.OfError(
                new EvaluationFailure("the input to cotangent function is not defined", null));
        }

        return Result<object, EvaluationFailure>.Of(1/tan);
    }

    public static Result<object, EvaluationFailure> ExponentOperation(object left, object right)
    {
        {
            if (left is double x && right is double y)
            {
                return Result<object, EvaluationFailure>.Of(Math.Pow(x, y));
            }
        }
        {
            if (left is int x && right is double y)
            {
                return Result<object, EvaluationFailure>.Of(Math.Pow(x, y));
            }
        }
        {
            if (left is double x && right is int y)
            {
                return Result<object, EvaluationFailure>.Of(Math.Pow(x, y));
            }
        }
        {
            if (left is int x && right is int y)
            {
                return Result<object, EvaluationFailure>.Of((int)Math.Pow(x, y));
            }
        }
        return Result<object, EvaluationFailure>.OfError(new EvaluationFailure("one of the inputs to the operator '^' is not a number", null));
    }
}