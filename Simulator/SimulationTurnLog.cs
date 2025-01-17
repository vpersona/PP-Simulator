using Simulator.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator;

/// <summary>
/// State of map after single simulation turn.
/// </summary>
public class SimulationTurnLog
{
    public List<Point> Positions { get; set; } 
    public Dictionary<Point, List<IMappable>> CreaturePositions { get; set; } = new();
    public int TurnId { get; set; }
    public required string Mappable { get; init; }
    public required string Move { get; init; }
    public Dictionary<Point, char> Symbols { get; init; }
}
