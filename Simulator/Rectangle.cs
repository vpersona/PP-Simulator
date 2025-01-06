using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator;

public class Rectangle
{
   
    public int X1 { get; }
    public int Y1 { get; }
    public int X2 { get; }  
    public int Y2 { get; }

    public Rectangle(int x1, int y1, int x2, int y2)
    {
       if (x1 == x2 || y1 == y2)
        {
            throw new ArgumentException("We don't want slim rectangles");
        }
       if(x1>x2)
        {
            int p = x1;
            x1 = x2;
            x2 = p;
        }
        if (y1 > y2) 
        {
            int p = y1;
            y1 = y2;
            y2 = p;
        }


        X1 = x1;
        Y1 = y1;
        X2 = x2;
        Y2 = y2;
    }

    public Rectangle(Point p1, Point p2) : this(p1.X, p1.Y, p2.X, p2.Y)
    {

    }
    public bool Contains(Point point)
    {
        if (point.X<=X2 && point.X>=X1 && point.Y>=Y1 && point.Y<=Y2)
        {
            return true;
        }
        return false;
    }


    public override string ToString()
    {
        return $"({X1},{Y1}):({X2},{Y2})";
    }
}
