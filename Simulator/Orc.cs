namespace Simulator;

public class Orc : Creature
{
    private int _rage=0;

    

    public int Rage
    {
        get => _rage;
        set => _rage = Validator.Limiter(value, 0, 10);
    }
    public Orc() { }
    public Orc(string name, int level = 1, int rage = 1) : base(name, level)
    {
        _rage = rage;
    }

    public override string Greeting() =>
     $"Hi, I'm {Name}, my level is {Level}, my rage is {Rage}.";
        

    public void Hunt()
    {
        if (++_rage % 2 == 0)
        {
            _rage = Math.Min(_rage + 1, 10);
        }
    }

    public override int Power => 7 * Level + 3 * _rage;
    public override string Info => $"{Name.ToUpper()[0] + Name[1..].ToLower()} [{Level}][{Rage}]";
}
