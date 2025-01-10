using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
namespace Simulator;


    public class Elf : Creature

    {

        public override string Symbol => "E"; 
        private int _agility = 0;

        public int Agility
        {
            get => _agility;
        set => _agility = Validator.Limiter(value, 0, 10);
        }

        public Elf() { }

        public Elf(string name, int level = 1, int agility = 0) : base(name, level)
        {
            Agility = agility;
        }

    public override string Greeting() =>
    $"Hi, I'm {Name}, my level is {Level}, my agility is {Agility}.";
       

    public void Sing()
        {
            if (++_agility % 3 == 0)
            {
                Agility = Math.Min(Agility + 1, 10);
            }
        }

        public override int Power => 8 * Level + 2 * Agility;
    public override string Info => $"{Name.ToUpper()[0] + Name[1..].ToLower()} [{Level}][{Agility}]";
}

