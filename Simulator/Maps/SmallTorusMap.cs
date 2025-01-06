using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Maps
{
    public class SmallTorusMap : Map

    {
        public int Size { get; }
        public SmallTorusMap(int size)
        {
            if (size < 5 || size > 20)
            {
                throw new ArgumentOutOfRangeException("Size must be in range [5,20]");
            }
            Size = size;
        }
        public override bool Exist(Point p)
        {
            return p.X >= 0 && p.X < Size && p.Y >= 0 && p.Y < Size;
        }

        public override Point Next(Point p, Direction d)
        {
            var nextPoint = p.Next(d);
            return WrapAround(nextPoint);
        }

        public override Point NextDiagonal(Point p, Direction d)
        {
            var nextPoint = p.NextDiagonal(d);
            return WrapAround(nextPoint);
        }
        private Point WrapAround(Point p)
        {
            int wrappedX = (p.X + Size) % Size;
            int wrappedY = (p.Y + Size) % Size;
            return new Point(wrappedX, wrappedY);
        }
    }
}
