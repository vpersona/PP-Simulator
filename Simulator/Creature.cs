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
    public abstract string Greeting();
    public abstract int Power { get; }
   
    public abstract string Info {  get; }   

    public void Upgrade()
    {
        if (_level < 10)
        {
            _level++;
        }
    }

    public string Go(Direction direction) => $"{Name} goes {direction.ToString().ToLower()}.";



    public string[] Go(Direction[] directions)
    {
        List<string> results = new List<string>();

        foreach (var direction in directions)
        {

            string result = Go(direction);
            results.Add(result);
        }
        return results.ToArray();
    }
    public string Go(string input)
    {
        var directions = DirectionParser.Parse(input); 
        var result = new List<string>();

       
        foreach (var direction in directions)
        {
            
            var point = Go(direction); 
            result.Add(point.ToString()); 
        }

        return string.Join(", ", result); 
    }


    public override string ToString()
    {
        return $"{GetType().Name.ToUpper()}: {Info}";
    }


}