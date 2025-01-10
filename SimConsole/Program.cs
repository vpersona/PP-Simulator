using Simulator.Maps;
using Simulator;
using System.Text;

namespace SimConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int turn = 1;
            Console.OutputEncoding = Encoding.UTF8;
            SmallSquareMap map = new(8,6);

            
            List<IMappable> mappables = [

                new Orc("Gorbag"), 
                new Elf("Elandor"), 
                new Animals { Description = "Eagles", Size = 3, IsFlying = true}, 
                new Animals { Description = "Bunnies", Size = 3 }, 
                new Animals {Description = "Ostriches", Size = 3, IsBird=true }

                ];

            List<Point> points = [
                new(2,2), 
                new(3, 1),
                new(2,0),
                new(3,1),
                new(0,0)
                ];
            string moves = "dlrludlurduluru";

            Simulation simulation = new(map, mappables, points, moves);
            MapVisualizer mapVisualizer = new(simulation.Map);

            mapVisualizer.Draw();

            while (!simulation.Finished)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();  

                Console.WriteLine($"\nTurn: {turn}");
                Console.WriteLine($"{simulation.CurrentMappable} {simulation.CurrentMappable.CurrentPosition} goes {simulation.CurrentMoveName}:");             
                simulation.Turn();
                
                mapVisualizer.Draw();                
                turn++;

            }
        }
    }
}