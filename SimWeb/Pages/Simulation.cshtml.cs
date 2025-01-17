using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimConsole;
using Simulator;
using Simulator.Maps;
using System.Diagnostics.Metrics;
using System.Text;

namespace SimWeb.Pages
{
    public class SimulationModel : PageModel
    {
        public int _currentTurn { get; private set; } 
        public SimulationHistory _history;
        public readonly Simulation _simulation;
        public string MapDisplay { get; private set; } = string.Empty;
        public SimulationTurnLog _currentLog { get; private set; }
       
        public void SimInit()
        {
            SmallSquareMap map = new(8, 6);
            List<IMappable> mappables = new()
                {
                    new Orc("Gorbag"),
                    new Elf("Elandor"),
                    new Animals { Description = "Eagles", Size = 3, IsFlying = true },
                    new Animals { Description = "Bunnies", Size = 3 },
                    new Animals { Description = "Ostriches", Size = 3, IsBird = true }
                };
            List<Point> points = new()
                {
                    new(2, 2),
                    new(3, 1),
                    new(2, 0),
                    new(3, 1),
                    new(0, 0)
                };
            string moves = "dlrludlurduluru";
            Simulation simulation = new(map, mappables, points, moves);
            _history = new SimulationHistory(_simulation);
            _currentLog = _history._turnLogs[_currentTurn];
        }

        public SimulationModel(Simulation simulation)
        {
            _simulation = simulation;
            _history = new SimulationHistory(simulation);
        }

        public void OnGet()
        {
            _currentTurn = HttpContext.Session.GetInt32("Counter") ?? 1;
            UpdateMap();
        }

        public IActionResult OnPostNextTurn()
        {
            if (!_simulation.Finished)
            {
                _simulation.Turn();
                // make a turn
                _currentTurn = HttpContext.Session.GetInt32("Counter") ?? 1;
                _currentTurn++;
                HttpContext.Session.SetInt32("Counter", _currentTurn);

                
                
                // update the map if needed
                UpdateMap();
                
            }

            return Page();
        }





        public IActionResult OnPostPreviousTurn()
        {
            _currentTurn = HttpContext.Session.GetInt32("Counter") ?? 1;

            
            if (_history == null)
            {
                SimInit();  // initialize history if  history is null
            }

            // chceck if you can go to the previous turn
            if (_currentTurn > 1 && _history._turnLogs.Count > _currentTurn - 1)
            {
                _currentTurn--;  
                HttpContext.Session.SetInt32("Counter", _currentTurn);  // save updated turn to session

                // get log from previous turn
                _currentLog = _history._turnLogs[_currentTurn];

                
                RestoreMapState(_currentLog.CreaturePositions);

            }

            return Page();
        }

        // restoring map to previous state
        private void RestoreMapState(Dictionary<Point, List<IMappable>> previousState)
        {
            
            foreach (var kvp in previousState)
            {
                var point = kvp.Key; 
                var mappables = kvp.Value;  

                
                var currentCell = _simulation.Map.At(point.X, point.Y);

                
                currentCell.Clear();  
                foreach (var mappable in mappables)
                {
                    currentCell.Add(mappable);  
                }
            }
        }





       
        public string GetCellContent(int x, int y)
        {
            var cellContent = _simulation.Map.At(x, y); 
            if (cellContent != null && cellContent.Count > 0)
            {
                //if more than one object on tiledisplay X image
                if (cellContent.Count > 1)
                {
                    return "<img src='/images/X.png' alt='Multiple objects' style='width: 100%; height: 100%; object-fit: contain;' />";
                }

                // if only one creature display it's image 
                var firstObject = cellContent.First();
                string imageSrc = GetImageSourceForObject(firstObject);
                return $"<img src='{imageSrc}' alt='{firstObject.Symbol}' style='width: 100%; height: 100%; object-fit: contain;' />";
            }
            return ""; 
        }

        
        private string GetImageSourceForObject(IMappable obj)
        {
            return obj.Symbol switch
            {
                "O" => "/images/orc.png",    
                "E" => "/images/elf.png",    
                "A" => "/images/rabbit.png", 
                "b" => "/images/bird.png",
                "B" => "/images/ostrich.png",
                "X" => "/images/x.png",
                _ => "/images/default.png"  
            };
        }

        private void UpdateMap()
        {
            var visualizer = new MapVisualizer(_simulation.Map);
            var mapString = new StringBuilder();

            for (int y = 0; y < _simulation.Map.SizeY; y++)
            {
                for (int x = 0; x < _simulation.Map.SizeX; x++)
                {
                    var cellContent = _simulation.Map.At(x, y);
                    string cellClass = "cell empty"; 

                    if (cellContent != null && cellContent.Count > 0)
                    {
                        var firstObject = cellContent.First(); 
                        cellClass = firstObject.Symbol switch
                        {
                            "O" => "cell orc",
                            "E" => "cell elf",
                            "A" => "cell animal",
                            _ => "cell occupied"
                        };
                    }

                    mapString.Append($"<div class='{cellClass}'>");

                    if (cellContent != null && cellContent.Count > 0)
                    {
                        mapString.Append(cellContent.First().Symbol);
                    }

                    mapString.Append("</div>");
                }
            }
            MapDisplay = mapString.ToString();  
        }
    }
}
