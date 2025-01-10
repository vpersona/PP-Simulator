
namespace Simulator.Maps
{
    public interface IMappable
    {
        Point CurrentPosition { get; }
        void AssignToMap(Map map, Point position);
        void Go(Direction direction);
        string Symbol { get; }

    }
    
}
