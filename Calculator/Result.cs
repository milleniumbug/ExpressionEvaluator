using System.Diagnostics.CodeAnalysis;

namespace Calculator;

public class Result<TValue, TError>
    where TValue : notnull
    where TError : notnull
{
    public TValue? Value { get; }
    
    public TError? Error { get; }
    
    [MemberNotNullWhen(true, nameof(Value))]
    [MemberNotNullWhen(false, nameof(Error))]
    public bool HasValue { get; }

    [MemberNotNullWhen(true, nameof(Error))]
    [MemberNotNullWhen(false, nameof(Value))]
    public bool HasError => !HasValue;

    private Result(bool hasValue, TValue? value, TError? error)
    {
        HasValue = hasValue;
        Value = value;
        Error = error;
    }

    public static Result<TValue, TError> Of(TValue value)
    {
        return new Result<TValue, TError>(true, value, default);
    }
    
    public static Result<TValue, TError> OfError(TError error)
    {
        return new Result<TValue, TError>(false, default, error);
    }

    public Result<TOutput, TError> Map<TOutput>(Func<TValue, TOutput> mapper)
        where TOutput : notnull
    {
        if (HasError)
        {
            return Result<TOutput, TError>.OfError(Error);
        }
        
        return Result<TOutput, TError>.Of(mapper(Value));
    }
}