using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Calculator.Tokenization;

namespace Calculator;

public class LexerError
{
    public int Position { get; }

    public LexerError(int position)
    {
        this.Position = position;
    }
}

public class Lexer
{
    private readonly Reader reader;

    public Lexer(Reader reader)
    {
        this.reader = reader;
    }
    
    private static Regex floatingPointLiteralRegex = new Regex(@"\G[0-9]+\.[0-9]*");

    private static Regex decimalLiteralRegex = new Regex(@"\G[0-9]+");
    
    private static Regex binaryLiteralRegex = new Regex(@"\G0b[0-1]+");
    
    private static Regex octalLiteralRegex = new Regex(@"\G0o[0-7]+");
    
    private static Regex hexLiteralRegex = new Regex(@"\G0x[0-9A-Fa-f]+");
    
    private static Regex identifierRegex = new Regex(@"\G[A-Za-z][A-Za-z0-9]*");

    private static Regex operatorRegex = new Regex(@"\G(\+|\-|\*|\/|\^|and|or|not|log|ln|xor|sin|cos|tan|ctg|factorial)");
    
    private static Regex whitespaceRegex = new Regex(@"\G[\s]+");

    public Result<Token, LexerError> NextToken()
    {
        var current = reader.Peek();
        if (current == -1)
        {
            return Result<Token, LexerError>.Of(new EndOfInputToken());
        }

        if (current == '(')
        {
            this.reader.Skip();
            return Result<Token, LexerError>.Of(new OpenParen());
        }
        
        if (current == ')')
        {
            this.reader.Skip();
            return Result<Token, LexerError>.Of(new CloseParen());
        }
        
        if ('0' <= current && current <= '9')
        {
            {
                var match = reader.Match(hexLiteralRegex);
                if (match.Success)
                {
                    if (int.TryParse(match.Value.AsSpan().Slice(2), NumberStyles.HexNumber,
                            CultureInfo.InvariantCulture, out var number))
                    {
                        reader.Advance(match.Value.Length);
                        return Result<Token, LexerError>.Of(new HexadecimalIntegralLiteral(number));                        
                    }
                    else
                    {
                        return Result<Token, LexerError>.OfError(new LexerError(this.reader.Position));
                    }
                }
            }
            
            {
                var match = reader.Match(floatingPointLiteralRegex);
                if (match.Success)
                {
                    if (double.TryParse(match.Value, NumberStyles.Number | NumberStyles.AllowExponent,
                            CultureInfo.InvariantCulture, out var number))
                    {
                        reader.Advance(match.Value.Length);
                        return Result<Token, LexerError>.Of(new FloatingPointLiteral(number));                        
                    }
                    else
                    {
                        return Result<Token, LexerError>.OfError(new LexerError(this.reader.Position));
                    }
                }
            }
            
            {
                var match = reader.Match(decimalLiteralRegex);
                if (match.Success)
                {
                    if (int.TryParse(match.Value, NumberStyles.Number,
                            CultureInfo.InvariantCulture, out var number))
                    {
                        reader.Advance(match.Value.Length);
                        return Result<Token, LexerError>.Of(new DecimalIntegralLiteral(number));                        
                    }
                    else
                    {
                        return Result<Token, LexerError>.OfError(new LexerError(this.reader.Position));
                    }
                }
            }
        }
        
        var whitespaceMatch = reader.Match(whitespaceRegex);
        if (whitespaceMatch.Success)
        {
            reader.Advance(whitespaceMatch.Value.Length);
            return Result<Token, LexerError>.Of(new WhitespaceToken(whitespaceMatch.Value));
        }
        
        var operatorMatch = reader.Match(operatorRegex);
        if (operatorMatch.Success)
        {
            reader.Advance(operatorMatch.Value.Length);
            return Result<Token, LexerError>.Of(new OperatorToken(operatorMatch.Value));
        }

        var identifierMatch = reader.Match(identifierRegex);
        if (identifierMatch.Success)
        {
            reader.Advance(identifierMatch.Value.Length);
            return Result<Token, LexerError>.Of(new Identifier(identifierMatch.Value));
        }
        
        return Result<Token, LexerError>.OfError(new LexerError(reader.Position));
    }
}