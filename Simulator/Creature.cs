using Simulator.Maps;
using System.ComponentModel.DataAnnotations;

namespace Simulator;

public abstract class Creature : IMappable
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

    public Point CurrentPosition { get; private set; }
    public Map? CurrentMap { get; private set; }

    public void AssignToMap(Map map, Point position)
    {
        if (map == null) throw new ArgumentNullException(nameof(map));
        if (!map.Exist(position)) throw new ArgumentOutOfRangeException("Position is out of map bounds.");

        CurrentMap = map;
        CurrentPosition = position;
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

    public abstract string Info { get; }

    public void Upgrade()
    {
        if (_level < 10)
        {
            _level++;
        }
    }

    public void Go(Direction direction)
    {
        if (CurrentMap == null)
        {
            throw new InvalidOperationException("Creature is not assigned to a map or position.");
        }

        Point newPosition = CurrentMap.Next(CurrentPosition, direction);
        MoveTo(newPosition);
    }

    private void MoveTo(Point newPosition)
    {
        if (CurrentMap == null)
            throw new InvalidOperationException("Creature is not assigned to a map or position.");

        CurrentMap.Remove(this, CurrentPosition);

        CurrentMap.Add(this, newPosition);

        CurrentPosition = newPosition;
    }


    public override string ToString()
    {
        return $"{GetType().Name.ToUpper()}: {Info}";
    }


}