using Calculator.Tokenization;
using Xunit;

namespace Calculator.Tests;

public class LexerTests
{
    [Fact]
    public void Test1()
    {
        var lexer = new Lexer(new Reader("(23 + 0xF0 + 0o10 + 0b1101)"));
        Result<Token, LexerError> r;
        r = lexer.NextToken();
        Assert.True(r.HasValue);
        Assert.IsType<OpenParen>(r.Value);
        r = lexer.NextToken();
        Assert.True(r.HasValue);
        Assert.IsType<DecimalIntegralLiteral>(r.Value);
        Assert.Equal(23, ((DecimalIntegralLiteral)r.Value).Value);
        r = lexer.NextToken();
        Assert.True(r.HasValue);
        Assert.IsType<WhitespaceToken>(r.Value);
        r = lexer.NextToken();
        Assert.True(r.HasValue);
        Assert.IsType<OperatorToken>(r.Value);
        Assert.Equal("+", ((OperatorToken)r.Value).Kind);
        r = lexer.NextToken();
        Assert.True(r.HasValue);
        Assert.IsType<WhitespaceToken>(r.Value);
        r = lexer.NextToken();
        Assert.True(r.HasValue);
        Assert.IsType<HexadecimalIntegralLiteral>(r.Value);
        Assert.Equal(15 * 16 + 0, ((HexadecimalIntegralLiteral)r.Value).Value);
        r = lexer.NextToken();
        Assert.True(r.HasValue);
        Assert.IsType<WhitespaceToken>(r.Value);
        r = lexer.NextToken();
        Assert.True(r.HasValue);
        Assert.IsType<OperatorToken>(r.Value);
        Assert.Equal("+", ((OperatorToken)r.Value).Kind);
        r = lexer.NextToken();
        Assert.True(r.HasValue);
        Assert.IsType<WhitespaceToken>(r.Value);
        r = lexer.NextToken();
        Assert.True(r.HasValue);
        Assert.IsType<OctalIntegralLiteral>(r.Value);
        Assert.Equal(8, ((OctalIntegralLiteral)r.Value).Value);
        r = lexer.NextToken();
        Assert.True(r.HasValue);
        Assert.IsType<WhitespaceToken>(r.Value);
        r = lexer.NextToken();
        Assert.True(r.HasValue);
        Assert.IsType<OperatorToken>(r.Value);
        Assert.Equal("+", ((OperatorToken)r.Value).Kind);
        r = lexer.NextToken();
        Assert.True(r.HasValue);
        Assert.IsType<WhitespaceToken>(r.Value);
        r = lexer.NextToken();
        Assert.True(r.HasValue);
        Assert.IsType<BinaryIntegralLiteral>(r.Value);
        Assert.Equal(13, ((BinaryIntegralLiteral)r.Value).Value);
        r = lexer.NextToken();
        Assert.True(r.HasValue);
        Assert.IsType<CloseParen>(r.Value);
        r = lexer.NextToken();
        Assert.True(r.HasValue);
        Assert.IsType<EndOfInputToken>(r.Value);
    }
}