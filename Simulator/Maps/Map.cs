namespace Simulator.Maps
{
    public abstract class Map
    {
        public int SizeX { get; }
        public int SizeY { get; }
        public readonly Rectangle mapArea;
        public abstract void Remove(Creature creature, Point point);
        public abstract void Add(Creature creature, Point point);
        protected Map(int sizeX, int sizeY)
        {
            if (sizeX < 5 || sizeY < 5)
            {
                throw new ArgumentOutOfRangeException("Minimum dimention size: 5x5");
            }

            SizeX = sizeX;
            SizeY = sizeY;

            mapArea = new Rectangle(0, 0, SizeX - 1, SizeY - 1);
        }
        public void Move(Creature creature, Point position, Point nextposition)
        {
            if (!mapArea.Contains(position) || !mapArea.Contains(nextposition))
            {
                throw new ArgumentOutOfRangeException("Point must be inside of the map.");
            }


            Remove(creature, position);
            Add(creature, nextposition);
        }

        public abstract List<Creature> At(Point point);

        public abstract List<Creature> At(int x, int y);
        public virtual bool Exist(Point p)
        {
            return mapArea.Contains(p); 
        }

        public abstract Point Next(Point p, Direction d);
        public abstract Point NextDiagonal(Point p, Direction d);


    }
}