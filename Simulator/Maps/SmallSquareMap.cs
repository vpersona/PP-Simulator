using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Maps;

public class SmallSquareMap : Map
{
    public int Size { get; }
    private readonly Rectangle bounds;
    public SmallSquareMap(int size)
    {
        if (size < 5 || size>20)
        {
            throw new ArgumentOutOfRangeException("Invalid size");
        }
        Size = size;

        bounds = new Rectangle(new Point(0, 0), new Point(size - 1, size - 1));
    }
    public override bool Exist(Point p)
    {
        return bounds.Contains(p);
    }

    public override Point Next(Point p, Direction d)
    {
        var nextPoint = p.Next(d);
        return Exist(nextPoint) ? nextPoint : p;
    }


    public override Point NextDiagonal(Point p, Direction d)
    {
        var nextPoint = p.NextDiagonal(d);

        return Exist(nextPoint) ? nextPoint : p;
    }
  

}

