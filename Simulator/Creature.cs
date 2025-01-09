using Simulator.Maps;
using System.ComponentModel.DataAnnotations;

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

    public Point? Position { get;  set; }
    public SmallMap? CurrentMap { get; private set; }

    public void AssignToMap(SmallMap map, Point position)
    {
        if (map == null) throw new ArgumentNullException(nameof(map));
        if (!map.Exist(position)) throw new ArgumentOutOfRangeException("Position is out of map bounds.");

        CurrentMap = map;
        Position = position;
        map.Add(this, position);
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

    public void Go(Direction direction)
    {
        if (CurrentMap == null || Position == null)
        {
            throw new InvalidOperationException("Creature is not assigned to a map or position.");
        }

        var newPosition = Position.Value.Next(direction);
        CurrentMap.Move(this, newPosition);
        Position = newPosition;
    }


    public override string ToString()
    {
        return $"{GetType().Name.ToUpper()}: {Info}";
    }


}