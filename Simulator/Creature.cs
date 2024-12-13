namespace Simulator;

public abstract class Creature
{
    private string _name = "Unknown";
    private int _level = 1;
    public string Name
    {
        get => _name;
        set => _name = Validator.Shortener(value, 3, 25);
    }

    public int Level
    {
        get => _level;
        set => _level = Validator.Limiter(value, 1, 10);
    }



    protected Creature(string name, int level = 1)
    {
        Name = name;
        Level = level;
    }

    public Creature() { }
    public abstract void SayHi();
    public abstract int Power { get; }
   
    public abstract string Info {  get; }   

    public void Upgrade()
    {
        if (_level < 10)
        {
            _level++;
        }
    }
   
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

    public override string ToString()
    {
        return $"{GetType().Name.ToUpper()}: {Info}";
    }


}