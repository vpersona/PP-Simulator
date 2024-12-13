namespace Simulator
{
    public class Birds : Animals
    {
        public bool CanFly { get; private set; } = true;

        public Birds() { }

        public Birds(string description, uint size = 3, bool canFly = true)
        {
            Description = description;
            Size = size;
            CanFly = canFly;
        }

        public override string Info => $"{Description} ({(CanFly ? "fly+" : "fly-")}) <{Size}>";

        public override string ToString()
        {
            return $"{GetType().Name.ToUpper()}: {Info}";
        }
    }
}
