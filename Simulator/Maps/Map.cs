namespace Simulator.Maps
{
    public abstract class Map
    {
        public int SizeX { get; }
        public int SizeY { get; }
       

        protected Map(int sizeX, int sizeY)
        {
            if (sizeX < 5 || sizeY < 5)
            {
                throw new ArgumentOutOfRangeException("Minimum dimention size: 5x5");
            }

            SizeX = sizeX;
            SizeY = sizeY;
        }

        public virtual bool Exist(Point p)
        {
            return p.X >= 0 && p.X < SizeX && p.Y >= 0 && p.Y < SizeY; // (0,0) to (SizeX - 1, SizeY -1)
        }

        public abstract Point Next(Point p, Direction d);
        public abstract Point NextDiagonal(Point p, Direction d);

        
    }
}