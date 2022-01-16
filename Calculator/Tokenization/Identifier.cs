namespace Calculator.Tokenization;

public class Identifier : Token
{
    public string Name { get; }
    
    public Identifier(string name)
    {
        Name = name;
    }
}