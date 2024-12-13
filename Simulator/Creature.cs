namespace Simulator;

public class Creature
{
    private string _name = "Unknown"; 
    private int _level = 1;   
    public string Name
    {
        get => _name;
        set
        {          
            value = value.Trim();          
            value = value.Length < 3 ? value.PadRight(3, '#') : value;
            value = value.Length > 25 ? value.Substring(0, 25).TrimEnd() : value;           
            value = char.IsLower(value[0]) ? char.ToUpper(value[0]) + value.Substring(1) : value;
            _name = value;
        }
    }

    public int Level
    {
        get => _level;
        set => _level = value < 1 ? 1 : value > 10 ? 10 : value;
    }



    public Creature(string name, int level = 1)
    {
        Name = name;
        Level = level;
    }

    public Creature() { }
    public void SayHi()
    {
        Console.WriteLine($"Hi, my name is {Name} and my level is {Level}.");
    }

 
    public void Upgrade()
    {
        if (_level < 10)
        {
            _level++;
        }
    }    
    public string Info => $"Creature: {Name}, Level: {Level}";
    public void Go(Direction direction)
    {
        Console.WriteLine($"{Name} goes {direction.ToString().ToLower()}.");
    }
    public void Go(Direction[] directions)
    {
        foreach (var direction in directions)
        {
            Go(direction);
        }
    }
    public void Go(string input)
    {
        var directions = DirectionParser.Parse(input);
        Go(directions);
    }
}
