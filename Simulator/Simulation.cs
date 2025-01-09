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
    public List<Creature> Creatures { get; }

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
    public Creature CurrentCreature
    {
        get
        {
            return Creatures[currentTurn % Creatures.Count];
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
            return Moves[currentTurn % Moves.Length].ToString().ToLower();
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
    public Simulation(Map map, List<Creature> creatures,
            List<Point> positions, string moves)
    {
        if (string.IsNullOrEmpty(moves))
            throw new ArgumentException("Moves cannot be null or empty.");

        if (positions == null || creatures.Count != positions.Count)
            throw new ArgumentException("The number of creatures must match the number of starting positions.");

        
       if (creatures == null || creatures.Count == 0)
            throw new ArgumentException("The list of creatures cannot be empty.");

        Map = map ?? throw new ArgumentNullException(nameof(map));
        Creatures = new List<Creature>(creatures);
        Positions = new List<Point>(positions);
        Moves = moves.ToLower();

        // assign creatures to their starting positions
        for (int i = 0; i < creatures.Count; i++)
        {
            creatures[i].AssignToMap((SmallMap)Map, positions[i]);
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

        Creature creature = CurrentCreature;
        string moveName = CurrentMoveName;

        // getting direction form parser
        Direction? direction = DirectionParser.Parse(moveName).FirstOrDefault();

       
        if (!direction.HasValue)
        {
            currentTurn++;
            return;
        }

        // creature current position
        Point currentPosition = creature.Position.Value;

        // next creature position
        Point nextPosition = Map.Next(currentPosition, direction.Value);

        // make a move on the map
        if (Map.Exist(nextPosition))
        {
            ((SmallMap)Map).Move(creature, nextPosition);
            creature.Position = nextPosition;
        }

        
        currentTurn++;

        // check if all the moves are finished 
        if (currentTurn >= Moves.Length * Creatures.Count)
        {
            Finished = true;
        }
    }

}

