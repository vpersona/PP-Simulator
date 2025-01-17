using Simulator.Maps;
using Simulator;

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
    public List<Point> Positions { get; private set; }
    public Simulation _simulation { get; set; }

    
    public void SetPositions(List<Point> newPositions)
    {
        if (newPositions == null || newPositions.Count != Positions.Count)
            throw new ArgumentException("The number of positions must match the number of creatures.");

        
        Positions = newPositions;
    }
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
    private int currentTurn = 0;
    private int currentMappableIndex = 0;

    
    private SimulationHistory _history;

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

        
        _history = new SimulationHistory(this);

        // assign creatures to the starting positions
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
                CurrentMappable.Go(direction);  // make a move
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