namespace Simulator.Maps;

public abstract class SmallMap : Map
{
    private readonly Dictionary<Point, List<Creature>> occupants = new();
    protected SmallMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
        if (sizeX > 20 || sizeY > 20)
        {
            throw new ArgumentOutOfRangeException("Maximum dimensions size : 20x20");

        }
    }

        public void Add(Creature creature, Point position)
        {
            if (!Exist(position))
                throw new ArgumentOutOfRangeException("Position is out of map bounds.");

            if (!occupants.ContainsKey(position))
                occupants[position] = new List<Creature>();

            occupants[position].Add(creature);
        }

        public void Move(Creature creature, Point newPosition)
        {
            if (creature.Position == null || !Exist(newPosition))
                throw new ArgumentOutOfRangeException("Invalid movement.");

            var currentPos = creature.Position.Value;
            occupants[currentPos].Remove(creature);
            if (occupants[currentPos].Count == 0)
                occupants.Remove(currentPos);

            Add(creature, newPosition);
        }


        public void Remove(Creature creature, Point position)
        {
            if (!Exist(position))
                throw new ArgumentOutOfRangeException("Position is out of map bounds.");

            occupants[position].Remove(creature);
        }

        public List<Creature> At(Point position)
        {
            if (!Exist(position))
                throw new ArgumentOutOfRangeException("Position is out of map bounds.");

            return new List<Creature>(occupants[position]);
        }

        public List<Creature> At(int x, int y)
        {
            return At(new Point(x, y));
        }

}