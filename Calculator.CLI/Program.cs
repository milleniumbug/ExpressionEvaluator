using Calculator;

var evaluationContext = new EvaluationContext();
var parser = new Parser();

while (true)
{
    string? line = Console.ReadLine();
    if (line == null)
    {
        break;
    }

    var lexer = new Lexer(new Reader(line));
    var parseResult = parser.Parse(lexer);
    if (parseResult.HasError)
    {
        Console.WriteLine($"Error: {parseResult.Error.Reason}");
        continue;
    }

    var evaluationResult = parseResult.Value.Evaluate(evaluationContext);
    if (evaluationResult.HasError)
    {
        Console.WriteLine($"Error: {evaluationResult.Error.Reason}");
        continue;
    }
    
    Console.WriteLine($"Result: {evaluationResult.Value}");
}
