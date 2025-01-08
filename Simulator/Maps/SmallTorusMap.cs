
namespace Simulator.Maps;
  public class SmallTorusMap : SmallMap
{
    public SmallTorusMap(int size) : base(size, size) { }

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
        int wrappedX = (p.X + SizeX) % SizeX;
        int wrappedY = (p.Y + SizeY) % SizeY;
        return new Point(wrappedX, wrappedY);
    }
}