
using Simulator.Maps;
using System.Security.Cryptography.X509Certificates;

namespace Simulator;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting Simulator!\n");
       
        Lab5b();
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
    static void Lab5b()
    {
       
        try
            {
                var map = new SmallSquareMap(7);
                var point1 = new Point(5, 5);
                var point2 = new Point(-1, -1);
                var nextPoint = map.Next(point1, Direction.Up);
                var outOfBoundsPoint = map.Next(new Point(0, 0), Direction.Down);

                Console.WriteLine($"Map Size: {map.Size}");

                
                Console.WriteLine($"Point {point1} exists on map: {map.Exist(point1)}");

               
                Console.WriteLine($"Point {point2} exists on map: {map.Exist(point2)}");

                
                Console.WriteLine($"Point after moving up: {nextPoint}");

                
                Console.WriteLine($"Point after moving down from (0,0): {outOfBoundsPoint}");
            }


        catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        

    }




}
