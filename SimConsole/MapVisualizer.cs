using Simulator;
using Simulator.Maps;
using System.Drawing;

namespace SimConsole;

public class MapVisualizer
{
    private readonly Map _map;


    public MapVisualizer(Map map)
    {
        _map = map;
    }



    public void Draw()
    {
        Console.Clear();
        int width = _map.SizeX;
        int height = _map.SizeY;

        // top line
        Console.Write(Box.TopLeft);
        for (int x = 0; x < width; x++)
        {
            Console.Write(Box.Horizontal);
            if (x < width - 1) Console.Write(Box.TopMid);
        }
        Console.WriteLine(Box.TopRight);

        // draw rows
        for (int y = 0; y < height; y++)
        {
            // content in rows 
            Console.Write(Box.Vertical);
            for (int x = 0; x < width; x++)
            {
                Simulator.Point point = new Simulator.Point(x, y);
                var objects = _map.At(point); 

                if (objects?.Count > 1)
                {                  
                    Console.Write("X");
                }
                else if (objects?.Count == 1)
                {                    
                    var symbol = objects[0] is IMappable mappable ? mappable.Symbol : " ";
                    Console.Write(symbol);
                }
                else
                {                 
                    Console.Write(" ");
                }
                // grid separator
                if (x < width - 1) Console.Write(Box.Vertical);
            }
            Console.WriteLine(Box.Vertical);

            // horizontal grid lines
            if (y < height - 1)
            {
                Console.Write(Box.MidLeft);
                for (int x = 0; x < width; x++)
                {
                    Console.Write(Box.Horizontal);
                    if (x < width - 1) Console.Write(Box.Cross);
                }
                Console.WriteLine(Box.MidRight);
            }
        }

        // bottom line
        Console.Write(Box.BottomLeft);
        for (int x = 0; x < width; x++)
        {
            Console.Write(Box.Horizontal);
            if (x < width - 1) Console.Write(Box.BottomMid); // grid separator
        }
        Console.WriteLine(Box.BottomRight);
    }
}

