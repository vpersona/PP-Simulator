using Simulator;

public class SimulationHistory
{
    public int _currentTurn { get; private set; } = 0;
    public int SizeX { get; }
    public int SizeY { get; }
    public List<SimulationTurnLog> _turnLogs { get; } = new List<SimulationTurnLog>();
    public Simulation _simulation;

    public SimulationHistory(Simulation simulation)
    {
        _simulation = simulation ?? throw new ArgumentNullException(nameof(simulation));
        SizeX = _simulation.Map.SizeX;
        SizeY = _simulation.Map.SizeY;

        var startingPosDict = new Dictionary<Point, List<char>>();

        for (int i = 0; i < _simulation.Mappables.Count; i++)
        {
            var point = _simulation.Positions[i];
            var symbol = _simulation.Mappables[i].Symbol[0];

            if (!startingPosDict.ContainsKey(point))
            {
                startingPosDict[point] = new List<char>(); 
            }

            startingPosDict[point].Add(symbol); 
        }

        _turnLogs.Add(new SimulationTurnLog
        {
            Mappable = "Pozycje startowe",
            Move = "Pozycje startowe",
            Symbols = startingPosDict.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.First()) 
        });


    }
    public void AddTurnLog(SimulationTurnLog log)
    {
        _turnLogs.Add(log);
    }

    public int GetTurnLogCount()
    {
        return _turnLogs.Count;
    }

  
    public SimulationTurnLog GetTurnLog(int turnIndex)
    {
        if (turnIndex >= 0 && turnIndex < _turnLogs.Count)
        {
            return _turnLogs[turnIndex];
        }
        return null;  
    }

    public void PreviousTurn()
    {
        if (_simulation.Finished || _currentTurn == 0)
            return;

        _currentTurn--;

        // restore symbols from previous tour
        var prevTurn = _turnLogs[_currentTurn-1];
        var previousSymbols = prevTurn.Symbols;

        // restore objects on map
        foreach (var point in previousSymbols.Keys)
        {
            var symbol = previousSymbols[point];
            var mappable = _simulation.Map.At(point).FirstOrDefault(m => m.Symbol[0] == symbol);
            if (mappable != null)
            {
                _simulation.Map.At(point).Clear();
                _simulation.Map.At(point).Add(mappable);
            }
        }

      

    }


    
    private void Run()
    {
        while (!_simulation.Finished)
        {
            var currentMappable = _simulation.CurrentMappable;
            var currentMove = _simulation.CurrentMoveName;
            var symbolsPos = new Dictionary<Point, char>();

            _simulation.Turn();

            for (int row = 0; row < SizeY; row++)
            {
                for (int col = 0; col < SizeX; col++)
                {
                    if (_simulation.Map.At(col, row).Count > 1)
                    {
                        symbolsPos.Add(new Point(col, row), 'X');
                    }
                    else if (_simulation.Map.At(col, row).Count == 1)
                    {
                        symbolsPos.Add(new Point(col, row), _simulation.Map.At(col, row)[0].Symbol[0]);
                    }
                }
            }
            _turnLogs.Add(new SimulationTurnLog { Mappable = currentMappable.ToString(), Move = currentMove, Symbols = symbolsPos });
        }
    }
}
