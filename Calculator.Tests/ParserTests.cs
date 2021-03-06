using System;
using Xunit;

namespace Calculator.Tests;

public class ParserTests
{
    [Theory]
    [InlineData("24", "24")]
    [InlineData("2 + 5", "7")]
    [InlineData("23+1", "24")]
    [InlineData("23 -         1", "22")]
    [InlineData("-1 + 2 + 3", "4")]
    [InlineData("-1 + -2 + 3", "0")]
    [InlineData("-2 / -1", "2")]
    [InlineData("-1 * -2", "2")]
    [InlineData("1 + 2 + 3", "6")]
    [InlineData("3 + 2 - 1", "4")]
    [InlineData("3 * 2", "6")]
    [InlineData("2 * 2 + 2", "6")]
    [InlineData("2 + 2 * 2", "6")]
    [InlineData("2 + (2 * 2)", "6")]
    [InlineData("(2 + 3) * 2", "10")]
    [InlineData("(2 + 2) * 2", "8")]
    [InlineData("-    6", "-6")]
    [InlineData("-    6 + 2", "-4")]
    [InlineData("0.5 + 0.25", "0.75")]
    [InlineData("factorial 5", "120")]
    [InlineData("factorial 5 * 2", "240")]
    [InlineData("sqrt 4", "2")]
    [InlineData("log 1000", "3")]
    [InlineData("ln 1000        ", "6.907755278982137")]
    [InlineData("0 and 0", "0")]
    [InlineData("0 and 1", "0")]
    [InlineData("1 and 0", "0")]
    [InlineData("1 and 1", "1")]
    [InlineData("0 or 0", "0")]
    [InlineData("0 or 1", "1")]
    [InlineData("1 or 0", "1")]
    [InlineData("1 or 1", "1")]
    [InlineData("1 and (0 or 1)", "1")]
    [InlineData("sin 0", "0")]
    [InlineData("cos 0", "1")]
    [InlineData("10^4*2+1", "20001")]
    [InlineData("(10^4)*2+1", "20001")]
    [InlineData("0b00010 or 0b11011", "27")]
    public void Success(string expression, string expectedEvaluationResult)
    {
        var evaluationContext = new EvaluationContext();
        var lexer = new Lexer(new Reader(expression));
        var parser = new Parser();
        var result = parser.Parse(lexer);
        Assert.True(result.HasValue);
        var evaluationResult = result.Value.Evaluate(evaluationContext);
        Assert.Equal(expectedEvaluationResult, evaluationResult.Value);
    }
    
    [Theory]
    [InlineData("2 / 0", "cannot divide by 0")]
    [InlineData("2147483647 + 1", "overflow")]
    [InlineData("2147483647 * 2", "overflow")]
    [InlineData("(-2147483647 - 1) / (- 1)", "overflow")]
    public void Failure(string expression, string expectedFailureReason)
    {
        var evaluationContext = new EvaluationContext();
        var lexer = new Lexer(new Reader(expression));
        var parser = new Parser();
        var result = parser.Parse(lexer);
        Assert.True(result.HasValue);
        var evaluationResult = result.Value.Evaluate(evaluationContext);
        Assert.Equal(expectedFailureReason, evaluationResult.Error.Reason);
    }
    
    [Theory]
    [InlineData("a <- 42", "a / 2 ", "21")]
    [InlineData("a <- factorial 5", "a", "120")]
    public void VariableTest(string expression, string secondExpression, string expectedEvaluationResult)
    {
        var evaluationContext = new EvaluationContext();
        var parser = new Parser();

        {
            var lexer = new Lexer(new Reader(expression));
            var result = parser.Parse(lexer);
            Assert.True(result.HasValue);
            var evaluationResult = result.Value.Evaluate(evaluationContext);
        }
        {
            var lexer = new Lexer(new Reader(secondExpression));
            var result = parser.Parse(lexer);
            Assert.True(result.HasValue);
            var evaluationResult = result.Value.Evaluate(evaluationContext);
            Assert.Equal(expectedEvaluationResult, evaluationResult.Value);
        }
    }
}