namespace Simulator;

public class Orc : Creature
{
    private int _rage=0;

    

    public int Rage
    {
        get => _rage;
        set => _rage = value < 0 ? 0 : value > 10 ? 10 : value;
    }
    public Orc() { }
    public Orc(string name, int level = 1, int rage = 1) : base(name, level)
    {
        _rage = rage < 0 ? 0 : rage > 10 ? 10 : rage;
    }

    public override void SayHi() => Console.WriteLine(
     $"Hi, I'm {Name}, my level is {Level}, my rage is {Rage}."
        );

    public void Hunt()
    {
        if (++_rage % 2 == 0)
        {
            _rage = Math.Min(_rage + 1, 10);
        }
    }

    public override int Power => 7 * Level + 3 * _rage;
}
