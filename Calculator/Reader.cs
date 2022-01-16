using System.Text.RegularExpressions;

namespace Calculator;

public class Reader
{
    private readonly string line;
    
    public int Position { get; private set; }

    public int Peek()
    {
        if (this.Position == this.line.Length)
        {
            return -1;
        }

        return this.line[this.Position];
    }
    
    public int Get()
    {
        var result = Peek();
        if (result != -1)
        {
            this.Position++;
        }
        
        return result;
    }
    
    public void Skip()
    {
        Get();
    }

    public Match Match(Regex regex)
    {
        return regex.Match(line, Position);
    }

    public void Advance(int count)
    {
        if (count < 0)
        {
            throw new ArgumentException(nameof(count));
        }
        this.Position = Math.Clamp(this.Position + count, 0, this.line.Length);
    }
    
    public Reader(string line)
    {
        this.line = line;
        this.Position = 0;
    }
}