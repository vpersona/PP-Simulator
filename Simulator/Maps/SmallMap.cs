namespace Simulator.Maps;

public abstract class SmallMap : Map
{
    private readonly List<Creature>?[,] tiles;
    public SmallMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
        if (sizeX > 20 || sizeY > 20)
        {
            throw new ArgumentOutOfRangeException("Maximum dimensions size : 20x20");

        }
        tiles = new List<Creature>[sizeX, sizeY];
    }

    public override void Add(Creature creature, Point position)
    {
        if (!Exist(position))
            throw new ArgumentOutOfRangeException("Position is out of map bounds.");

        if (tiles[position.X, position.Y] == null)
        {
            tiles[position.X, position.Y] = new List<Creature>();
        }
        tiles[position.X, position.Y].Add(creature);
    }
    


    public override void Remove(Creature creature, Point position)
    {
        var CreaturesAtPoint = tiles[position.X, position.Y];
        if (CreaturesAtPoint != null)
        {
            CreaturesAtPoint.Remove(creature);
        }
    }

    public override List<Creature> At(Point position)
    {


        return tiles[position.X, position.Y] ?? new List<Creature>();
    }

    public override List<Creature> At(int x, int y)
    {
        return At(new Point(x, y));
    }




    public override Point Next(Point p, Direction d)
    {
        throw new NotImplementedException();
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        throw new NotImplementedException();
    }

}