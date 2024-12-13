
namespace Simulator
{
    public class Animals
    {
        public required string Description { get; init; }
        public uint Size { get; set; } = 3;

        public virtual string Info => $"{Description} <{Size}>";

        public override string ToString()
        {
            return $"{GetType().Name.ToUpper()}: {Info}";
        }
    }
}


