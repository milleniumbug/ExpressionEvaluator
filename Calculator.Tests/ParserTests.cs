using Xunit;

namespace Calculator.Tests;

public class ParserTests
{
    [Theory]
    [InlineData("24", "24")]
    [InlineData("2 + 5", "7")]
    [InlineData("23+1", "24")]
    [InlineData("23 -         1", "22")]
    [InlineData("1 + 2 + 3", "6")]
    [InlineData("3 + 2 - 1", "4")]
    [InlineData("3 * 2", "6")]
    [InlineData("2 + 2 * 2", "6")]
    [InlineData("-    6", "-6")]
    [InlineData("-    6 + 2", "-4")]
    [InlineData("0.5 + 0.25", "0.75")]
    [InlineData("factorial 5", "120")]
    public void Success(string expression, string expectedEvaluationResult)
    {
        var lexer = new Lexer(new Reader(expression));
        var parser = new Parser();
        var result = parser.Parse(lexer);
        Assert.True(result.HasValue);
        var evaluationResult = result.Value.Evaluate();
        Assert.Equal(expectedEvaluationResult, evaluationResult.Value);
    }
    
    [Theory]
    [InlineData("2 / 0", "cannot divide by 0")]
    [InlineData("2147483647 + 1", "overflow")]
    [InlineData("2147483647 * 2", "overflow")]
    [InlineData("-2147483648 / -1", "overflow")]
    public void Failure(string expression, string expectedFailureReason)
    {
        var lexer = new Lexer(new Reader(expression));
        var parser = new Parser();
        var result = parser.Parse(lexer);
        Assert.True(result.HasValue);
        var evaluationResult = result.Value.Evaluate();
        Assert.Equal(expectedFailureReason, evaluationResult.Error.Reason);
    }
}