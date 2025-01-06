
using System.Security.Cryptography.X509Certificates;

namespace Simulator;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting Simulator!\n");
        Lab5a();
    }


    static void Lab5a()
    {
        try
        {
           
            var rect1 = new Rectangle(new Point(50, 3), new Point(2, 22));
            var rect2 = new Rectangle(new Point(9, 12), new Point(10, 15));

            //Argument Exception 

            var rect3 = new Rectangle(new Point(12, 3), new Point(12, 7));


            Console.WriteLine("Rectangle 1: ");
            Console.WriteLine(rect1.ToString());
            Console.WriteLine("Rectangle 2");
            Console.WriteLine(rect2.ToString());
            var point1 = new Point(5, 6);
            Console.WriteLine($"Does point {point1} belong to the rectangle?: {rect1.Contains(point1)}");
            Console.WriteLine($"Does point {point1} belong to the rectangle 2?: {rect2.Contains(point1)}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        


    }
    static void Lab4a()
    {
        Console.WriteLine("HUNT TEST\n");
        var o = new Orc() { Name = "Gorbag", Rage = 7 };
        o.SayHi();
        for (int i = 0; i < 10; i++)
        {
            o.Hunt();
            o.SayHi();
        }

        Console.WriteLine("\nSING TEST\n");
        var e = new Elf("Legolas", agility: 2);
        e.SayHi();
        for (int i = 0; i < 10; i++)
        {
            e.Sing();
            e.SayHi();
        }

        Console.WriteLine("\nPOWER TEST\n");
        Creature[] creatures = {
        o,
        e,
        new Orc("Morgash", 3, 8),
        new Elf("Elandor", 5, 3)
    };
        foreach (Creature creature in creatures)
        {
            Console.WriteLine($"{creature.Name,-15}: {creature.Power}");
        }
        Creature c = new Elf("Elandor", 5, 3);
        Console.WriteLine(c);  
    }
   

    static void Lab4b()
    {
        object[] myObjects = {
        new Animals() { Description = "dogs" },
        new Birds { Description = "eagles", Size = 10 },
        new Elf("e", 15, -3),
        new Orc("morgash", 6, 4)
    };

        Console.WriteLine("\nMy objects:");
        foreach (var o in myObjects)
        {
            Console.WriteLine(o);
        }
        /*
            My objects:
            ANIMALS: Dogs <3>
            BIRDS: Eagles (fly+) <10>
            ELF: E## [10][0]
            ORC: Morgash [6][4]
        */
    }




}
