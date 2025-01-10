using Simulator.Maps;


namespace Simulator;

public class Simulation
{
    /// <summary>
    /// Simulation's map.
    /// </summary>
    public Map Map { get; }

    /// <summary>
    /// Creatures moving on the map.
    /// </summary>
    public List<IMappable> Mappables { get; }

    /// <summary>
    /// Starting positions of creatures.
    /// </summary>
    public List<Point> Positions { get; }

    /// <summary>
    /// Cyclic list of creatures moves. 
    /// Bad moves are ignored - use DirectionParser.
    /// First move is for first creature, second for second and so on.
    /// When all creatures make moves, 
    /// next move is again for first creature and so on.
    /// </summary>
    public string Moves { get; }

    /// <summary>
    /// Has all moves been done?
    /// </summary>
    public bool Finished { get; private set; } = false;

    /// <summary>
    /// Creature which will be moving current turn.
    /// </summary>
    /// 
    private int currentTurn = 0;
    private int currentMappableIndex = 0;
    public IMappable CurrentMappable
    {
        get
        {
            return Mappables[currentMappableIndex];
        }
    }

    /// <summary>
    /// Lowercase name of direction which will be used in current turn.
    /// </summary>


    public string CurrentMoveName
    {
        get
        {
            if (Finished) throw new InvalidOperationException("Simulation has finished.");
            return Moves[currentMappableIndex].ToString().ToLower();
        }

        /// <summary>
        /// Simulation constructor.
        /// Throw errors:
        /// if creatures' list is empty,
        /// if number of creatures differs from 
        /// number of starting positions.
        /// </summary>
        /// 

    }
    public Simulation(Map map, List<IMappable> mappables,
            List<Point> positions, string moves)
    {
        if (string.IsNullOrEmpty(moves))
            throw new ArgumentException("Moves cannot be null or empty.");

        if (positions == null || mappables.Count != positions.Count)
            throw new ArgumentException("The number of creatures must match the number of starting positions.");

        
       if (mappables == null || mappables.Count == 0)
            throw new ArgumentException("The list of creatures cannot be empty.");

        Map = map;
        Mappables = mappables;
        Positions = positions;
        Moves = moves;

        // assign creatures to their starting positions
        for (int i = 0; i < mappables.Count; i++)
        {
            mappables[i].AssignToMap((SmallMap)Map, positions[i]);
        }
    }


    /// <summary>
    /// Makes one move of current creature in current direction.
    /// Throw error if simulation is finished.
    /// </summary>
    public void Turn()
    {
        if (Finished)
            throw new InvalidOperationException("Simulation has finished.");


        var directions = DirectionParser.Parse(CurrentMoveName);

        foreach (var direction in directions)
        {
            try
            {

                CurrentMappable.Go(direction);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Invalid turn {ex.Message}");
            }
        }


        currentMappableIndex++;


        if (currentMappableIndex >= Mappables.Count)
        {
            currentMappableIndex = 0;
        }
    }
}



