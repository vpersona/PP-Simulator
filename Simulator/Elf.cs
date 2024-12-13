using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
namespace Simulator;


    public class Elf : Creature
    {
        private int _agility = 0;

        public int Agility
        {
            get => _agility;
            private set => _agility = value < 0 ? 0 : value > 10 ? 10 : value;
        }

        public Elf() { }

        public Elf(string name, int level = 1, int agility = 0) : base(name, level)
        {
            Agility = agility;
        }

    public override void SayHi() => Console.WriteLine(
    $"Hi, I'm {Name}, my level is {Level}, my agility is {Agility}."
        );

    public void Sing()
        {
            if (++_agility % 3 == 0)
            {
                Agility = Math.Min(Agility + 1, 10);
            }
        }

        public override int Power => 8 * Level + 2 * Agility;
    }

