
using Simulator.Maps;

namespace Simulator
{
    public class Animals : IMappable
    {
        public bool IsFlying { get; set; } = false;
        public bool IsBird { get; set; } = false;
        public required string Description { get; init; }
        public uint Size { get; set; } = 3;

        public virtual string Info => $"{Description} <{Size}>";
        public Point CurrentPosition { get; private set; }
        public Map? CurrentMap { get; private set; }

        public virtual string Symbol
        {
            get
            {
                if (IsFlying)
                {
                    return "B"; 
                }
                else if (!IsFlying && IsBird)
                {
                    return "b"; 
                }
                else
                {
                    return "A";
                }
            }
        }

        public void AssignToMap(Map map, Point position)
        {
            if (map == null) throw new ArgumentNullException(nameof(map));
            if (!map.Exist(position)) throw new ArgumentOutOfRangeException("Position is out of map bounds.");

            CurrentMap = map;
            CurrentPosition = position;
            map.Add(this, position);
        }

        public void Go(Direction direction)
        {
            if (CurrentMap == null)
            {
                throw new InvalidOperationException("Animal is not assigned to a map or position.");
            }


            
            if (IsFlying)
            {
                // If it's a bird, move two steps in the given direction
                Point newPosition = CurrentMap.Next(CurrentMap.Next(CurrentPosition, direction), direction);
                MoveTo(newPosition);
            }
            else
            {
                // If it's NOT a bird, move diagonally  by one tile
                Point newPosition = CurrentMap.NextDiagonal(CurrentPosition, direction);
                MoveTo(newPosition);
            }


           
        }

        private void MoveTo(Point newPosition)
        {
            if (CurrentMap == null)
                throw new InvalidOperationException("Animal is not assigned to a map or position.");

            CurrentMap.Remove(this, CurrentPosition);
            CurrentMap.Add(this, newPosition);
            CurrentPosition = newPosition;
        }
        public override string ToString()
        {
            return $"{GetType().Name.ToUpper()}: {Info}";
        }
    }
}


