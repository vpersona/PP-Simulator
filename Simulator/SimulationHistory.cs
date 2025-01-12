using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator;

public class SimulationHistory
{
    private Simulation _simulation { get; }
    public int SizeX { get; }
    public int SizeY { get; }
    public List<SimulationTurnLog> TurnLogs { get; } = [];
    // store starting positions at index 0

    public SimulationHistory(Simulation simulation)
    {
        _simulation = simulation ??
            throw new ArgumentNullException(nameof(simulation));
        SizeX = _simulation.Map.SizeX;
        SizeY = _simulation.Map.SizeY;
        Run();
    }

    private void Run()
    {
        {
            // initial positions of all mappables
            var initialSymbols = new Dictionary<Point, char>();
            for (int i = 0; i < _simulation.Mappables.Count; i++)
            {
                var mappable = _simulation.Mappables[i];
                var position = _simulation.Positions[i];
                initialSymbols[position] = mappable.Symbol[0];
            }

            //adding the initial state to the logs

            TurnLogs.Add(new SimulationTurnLog
            {
                Mappable = "Starting Positions",
                Move = "N/A",
                Symbols = new Dictionary<Point, char>(initialSymbols)
            });

            while (!_simulation.Finished)
            {
                var currentSymbols = new Dictionary<Point, char>();

                // the current state of the map

                for (int x = 0; x < SizeX; x++)
                {
                    for (int y = 0; y < SizeY; y++)
                    {
                        var point = new Point(x, y);
                        var mappables = _simulation.Map.At(point);

                        foreach (var mappable in mappables)
                        {
                            currentSymbols[point] = mappable.Symbol[0];
                        }
                    }
                }
                // creating a log for the current turn and adding it to the collection
                var turnLog = new SimulationTurnLog
                {
                    Mappable = _simulation.CurrentMappable.ToString(),
                    Move = _simulation.CurrentMoveName,
                    Symbols = currentSymbols
                };

                TurnLogs.Add(turnLog);
                _simulation.Turn();
            }
        }
    }
}
