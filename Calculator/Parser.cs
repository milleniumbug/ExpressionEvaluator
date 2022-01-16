using Calculator.SyntaxTree;
using Calculator.Tokenization;

namespace Calculator;

public class Parser
{
    public Parser()
    {
        
    }

    public Result<Expression<string>, ParserError> Parse(Lexer lexer)
    {
        var tokens = new List<Token>();
        Result<Token, LexerError> lexerResult;
        while (true)
        {
            lexerResult = lexer.NextToken();
            if (lexerResult.HasError)
            {
                return Result<Expression<string>, ParserError>.OfError(new ParserError(lexerResult.Error,
                    $"invalid input at {lexerResult.Error.Position}"));
            }
            else if (lexerResult.Value is EndOfInputToken)
            {
                tokens.Add(lexerResult.Value);
                break;
            }
            else
            {
                tokens.Add(lexerResult.Value);
            }
        }

        var parsedResult = ParseFullExpression(tokens);
        if (parsedResult.HasError)
        {
            return Result<Expression<string>, ParserError>.OfError(parsedResult.Error);
        }

        var (expression, remainingTokens) = parsedResult.Value;
        /*if (remainingTokens.First() is not EndOfInputToken)
        {
            return Result<Expression<string>, ParserError>.OfError(new ParserError(null, "input left"));
        }*/

        return Result<Expression<string>, ParserError>.Of(expression);
    }
    
    private Result<(Expression<string> expression, IEnumerable<Token> remainingTokens), ParserError> ParseFullExpression(IEnumerable<Token> tokens)
    {
        var result = ParseExpression(tokens);
        if (result.HasError)
        {
            return Result<(Expression<string> expression, IEnumerable<Token> remainingTokens), ParserError>.OfError(result.Error);
        }

        var expression = new UnaryExpression<object, string>(
            result.Value.expression, o => Result<string, EvaluationFailure>.Of(o.ToString() ?? "null"));
        tokens = result.Value.remainingTokens;
        
        return Result<(Expression<string> expression, IEnumerable<Token> remainingTokens), ParserError>.Of(
            (expression, tokens));
    }
    
    private Result<(Expression<object> expression, IEnumerable<Token> remainingTokens), ParserError> ParseExpression(IEnumerable<Token> tokens)
    {
        tokens = SkipWhitespace(tokens);
        return ParseAdditiveExpression(tokens);
    }

    private IEnumerable<Token> SkipWhitespace(IEnumerable<Token> tokens)
    {
        return tokens.SkipWhile(token => token is WhitespaceToken);
    }
    
    private Result<(Expression<object> expression, IEnumerable<Token> remainingTokens), ParserError>
        ParseMultiplicativeExpression(IEnumerable<Token> tokens)
    {
        var leftResult = ParseUnaryExpression(tokens);
        if (leftResult.HasError)
        {
            return leftResult;
        }

        var leftExpression = leftResult.Value.expression;
        tokens = leftResult.Value.remainingTokens;

        tokens = SkipWhitespace(tokens);

        var operatorToken = tokens.First() as OperatorToken;
        var operatorKind = operatorToken?.Kind;

        if (operatorKind == null)
        {
            return leftResult;
        }

        if (operatorKind != "*" && operatorKind != "/")
        {
            return leftResult;
        }

        tokens = tokens.Take(1..);
        
        tokens = SkipWhitespace(tokens);
        
        var rightResult = ParseMultiplicativeExpression(tokens);
        if (rightResult.HasError)
        {
            return rightResult;
        }

        var rightExpression = rightResult.Value.expression;
        tokens = rightResult.Value.remainingTokens;

        if (operatorKind == "*")
        {
            var additiveExpression = new BinaryExpression<object, object, object>(
                leftExpression, rightExpression, Operations.MultiplicationOperation);
            return Result<(Expression<object> expression, IEnumerable<Token> remainingTokens), ParserError>.Of(
                (additiveExpression, tokens));
        }
        
        if (operatorKind == "/")
        {
            var additiveExpression = new BinaryExpression<object, object, object>(
                leftExpression, rightExpression, Operations.DivisionOperation);
            return Result<(Expression<object> expression, IEnumerable<Token> remainingTokens), ParserError>.Of(
                (additiveExpression, tokens));
        }

        throw new InvalidDataException();
    }
    
    private Result<(Expression<object> expression, IEnumerable<Token> remainingTokens), ParserError>
        ParseUnaryExpression(IEnumerable<Token> tokens)
    {
        var operatorToken = tokens.First() as OperatorToken;
        var operatorKind = operatorToken?.Kind;
        
        if (operatorKind == null)
        {
            return ParseLiteralExpression(tokens);
        }

        tokens = tokens.Take(1..);
        
        tokens = SkipWhitespace(tokens);
        
        
        var inputResult = ParseLiteralExpression(tokens);
        if (inputResult.HasError)
        {
            return inputResult;
        }

        var inputExpression = inputResult.Value.expression;
        tokens = inputResult.Value.remainingTokens;

        if (operatorKind != "-" && operatorKind != "factorial")
        {
            return inputResult;
        }

        if (operatorKind == "-")
        {
            var additiveExpression = new UnaryExpression<object ,object>(
                inputExpression, Operations.NegationOperation);
            return Result<(Expression<object> expression, IEnumerable<Token> remainingTokens), ParserError>.Of(
                (additiveExpression, tokens));
        }
        
        if (operatorKind == "factorial")
        {
            var additiveExpression = new UnaryExpression<object ,object>(
                inputExpression, Operations.FactorialOperation);
            return Result<(Expression<object> expression, IEnumerable<Token> remainingTokens), ParserError>.Of(
                (additiveExpression, tokens));
        }

        throw new InvalidDataException();
    }

    private Result<(Expression<object> expression, IEnumerable<Token> remainingTokens), ParserError>
        ParseAdditiveExpression(IEnumerable<Token> tokens)
    {
        var leftResult = ParseMultiplicativeExpression(tokens);
        if (leftResult.HasError)
        {
            return leftResult;
        }

        var leftExpression = leftResult.Value.expression;
        tokens = leftResult.Value.remainingTokens;

        tokens = SkipWhitespace(tokens);

        var operatorToken = tokens.First() as OperatorToken;
        var operatorKind = operatorToken?.Kind;

        if (operatorKind == null)
        {
            return leftResult;
        }

        if (operatorKind != "+" && operatorKind != "-")
        {
            return leftResult;
        }

        tokens = tokens.Take(1..);
        
        tokens = SkipWhitespace(tokens);
        
        var rightResult = ParseAdditiveExpression(tokens);
        if (rightResult.HasError)
        {
            return rightResult;
        }

        var rightExpression = rightResult.Value.expression;
        tokens = rightResult.Value.remainingTokens;

        if (operatorKind == "+")
        {
            var additiveExpression = new BinaryExpression<object, object, object>(
                leftExpression, rightExpression, Operations.AdditionOperation);
            return Result<(Expression<object> expression, IEnumerable<Token> remainingTokens), ParserError>.Of(
                (additiveExpression, tokens));
        }
        
        if (operatorKind == "-")
        {
            var additiveExpression = new BinaryExpression<object, object, object>(
                leftExpression, rightExpression, Operations.SubtractionOperation);
            return Result<(Expression<object> expression, IEnumerable<Token> remainingTokens), ParserError>.Of(
                (additiveExpression, tokens));
        }

        throw new InvalidDataException();
    }

    private Result<(Expression<object> expression, IEnumerable<Token> remainingTokens), ParserError>
        ParseLiteralExpression(IEnumerable<Token> tokens)
    {
        {
            var result = ParseIntegralLiteralExpression(tokens);
            if (result.HasValue)
            {
                return Result<(Expression<object> expression, IEnumerable<Token> remainingTokens), ParserError>.Of(
                    (new UnaryExpression<int, object>(result.Value.expression, o => Result<object, EvaluationFailure>.Of(o)),
                        result.Value.remainingTokens));
            }
        }
        
        {
            var result = ParseFloatingPointLiteralExpression(tokens);
            if (result.HasValue)
            {
                return Result<(Expression<object> expression, IEnumerable<Token> remainingTokens), ParserError>.Of(
                    (new UnaryExpression<double, object>(result.Value.expression, o => Result<object, EvaluationFailure>.Of(o)),
                        result.Value.remainingTokens));
            }
        }
        
        return Result<(Expression<object> expression, IEnumerable<Token> remainingTokens), ParserError>.OfError(new ParserError(null, "not a literal expression"));
    }

    private Result<(Expression<int> expression, IEnumerable<Token> remainingTokens), ParserError>
        ParseIntegralLiteralExpression(IEnumerable<Token> tokens)
    {
        if (tokens.First() is IntegralLiteral integralLiteral)
        {
            tokens = tokens.Take(1..);
            var expression = new LiteralExpression<int>(integralLiteral.Value);
            return Result<(Expression<int> expression, IEnumerable<Token> remainingTokens), ParserError>.Of(
                (expression, tokens));
        }

        return Result<(Expression<int> expression, IEnumerable<Token> remainingTokens), ParserError>.OfError(
            new ParserError(null, "not a literal expression"));
    }

    private Result<(Expression<double> expression, IEnumerable<Token> remainingTokens), ParserError>
        ParseFloatingPointLiteralExpression(IEnumerable<Token> tokens)
    {
        if (tokens.First() is FloatingPointLiteral floatingPointLiteral)
        {
            tokens = tokens.Take(1..);
            var expression = new LiteralExpression<double>(floatingPointLiteral.Value);
            return Result<(Expression<double> expression, IEnumerable<Token> remainingTokens), ParserError>.Of(
                (expression, tokens));
        }
        
        return Result<(Expression<double> expression, IEnumerable<Token> remainingTokens), ParserError>.OfError(
            new ParserError(null, "not a literal expression"));
    }
}

public class ParserError
{
    public LexerError? LexerError { get; }
    
    public string Reason { get; }

    public ParserError(LexerError? lexerError, string reason)
    {
        LexerError = lexerError;
        Reason = reason;
    }
}