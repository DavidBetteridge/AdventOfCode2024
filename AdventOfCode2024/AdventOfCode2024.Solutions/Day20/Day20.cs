// ReSharper disable UseSymbolAlias
namespace AdventOfCode2024.Solutions;
using Distance = int;
public class Day20
{
    public int Part1(string filename, int savesAtLeast)
    {
        var map = File.ReadAllLines(filename);
        var height = map.Length;
        var width = map[0].Length;
        
        var walls = new bool[height * width];

        var start = (0, 0);
        var end = (0, 0);
        for (var rowNumber = 0; rowNumber < height; rowNumber++)
        {
            for (var columnNumber = 0; columnNumber < width; columnNumber++)
            {
                if (map[rowNumber][columnNumber] == '#')
                    walls[rowNumber * width + columnNumber] = true;

                if (map[rowNumber][columnNumber] == 'S')
                    start = (columnNumber, rowNumber);
                
                if (map[rowNumber][columnNumber] == 'E')
                    end = (columnNumber, rowNumber);
            }
        }
        
       
        var initial = CostMap(height, width, start, walls);
        var distances = initial.Item2;
        var route = initial.Item1;
        var target = end.Item2 * width + end.Item1;
        var worstCaseCost = distances[target];
        var goodCheats = 0;

        // Walk back along the route
        var location = target;
        var source = start.Item2 * width + start.Item1;
        var tried = new HashSet<int>();
        while (location != source)
        {
            var columnNumber = location % width;
            var rowNumber = location / width;
            
            // Above
            var remove = location - width;
            if (rowNumber > 1 && walls[remove] && !tried.Contains(remove))
            {
                var n = distances[remove - width];
                var w = distances[remove - 1];
                var e = distances[remove + 1];
                if (n - distances[location] > savesAtLeast ||
                    w - distances[location] > savesAtLeast ||
                    e - distances[location] > savesAtLeast)
                {
                    walls[remove] = false;
                    var newCosts = CostMap(height, width, start, walls);
                    distances = newCosts.Item2;
                    var newCost = distances[target];
                    if (worstCaseCost - newCost >= savesAtLeast)
                        goodCheats++;
                    walls[remove] = true;
                    tried.Add(remove);
                }
            }
            
            // Below
            remove = location + width;
            if (rowNumber < (height-2) && walls[remove] && !tried.Contains(remove))
            {
                var s = distances[remove + width];
                var w = distances[remove - 1];
                var e = distances[remove + 1];
                if (s - distances[location] > savesAtLeast ||
                    w - distances[location] > savesAtLeast ||
                    e - distances[location] > savesAtLeast)
                {
                    walls[remove] = false;
                    var newCosts = CostMap(height, width, start, walls);
                    distances = newCosts.Item2;
                    var newCost = distances[target];
                    if (worstCaseCost - newCost >= savesAtLeast)
                        goodCheats++;
                    walls[remove] = true;
                    tried.Add(remove);
                }
            }
            
            // Left
            remove = location - 1;
            if (columnNumber > 1 && walls[remove] && !tried.Contains(remove))
            {
                var s = distances[remove + width];
                var w = distances[remove - 1];
                var n = distances[remove - width];
                if (s - distances[location] > savesAtLeast ||
                    w - distances[location] > savesAtLeast ||
                    n - distances[location] > savesAtLeast)
                {
                    walls[remove] = false;
                    var newCosts = CostMap(height, width, start, walls);
                    distances = newCosts.Item2;
                    var newCost = distances[target];
                    if (worstCaseCost - newCost >= savesAtLeast)
                        goodCheats++;
                    walls[remove] = true;
                    tried.Add(remove);
                }
            }
            
            // Right
            remove = location + 1;
            if (columnNumber < (width-2) && walls[remove] && !tried.Contains(remove))
            {
                var s = distances[remove + width];
                var e = distances[remove + 1];
                var n = distances[remove - width];
                if (s - distances[location] > savesAtLeast ||
                    e - distances[location] > savesAtLeast ||
                    n - distances[location] > savesAtLeast)
                {
                    walls[remove] = false;
                    var newCosts = CostMap(height, width, start, walls);
                    distances = newCosts.Item2;
                    var newCost = distances[target];
                    if (worstCaseCost - newCost >= savesAtLeast)
                        goodCheats++;
                    walls[remove] = true;
                    tried.Add(remove);
                }
            }
            
            location = route[location];
        }


        return goodCheats;
    }

    private static (int[], Distance[]) CostMap(int height, int width, (int, int) start, bool[] walls)
    {
        var distances = new Distance[height * width];
        var prev = new int[height * width];
        var queue = new PriorityQueue<int, Distance>();
        
        var source = start.Item2 * width + start.Item1;
        queue.Enqueue(source,0);

        Array.Fill(distances, Distance.MaxValue);
        distances[source] = 0;

        while (queue.Count > 0)
        {
            var u = queue.Dequeue();

            var col = u % width;
            var row = u / width;

            //North
            var v = u - width;
            if (row > 0 && (!walls[v]))
            {
                var alt = distances[u] + 1;
                if (alt < distances[v])
                {
                    distances[v] = alt;
                    prev[v] = u;
                    queue.Remove(v, out _, out _);
                    queue.Enqueue(v, alt);
                }
            }
            
            //South
            v = u + width;
            if ((row + 1) < height && (!walls[v]))
            {
                var alt = distances[u] + 1;
                if (alt < distances[v])
                {
                    distances[v] = alt;
                    prev[v] = u;
                    queue.Remove(v, out _, out _);
                    queue.Enqueue(v, alt);
                }
            }

            //West
            v = u - 1;
            if (col > 0 && (!walls[v]))
            {
                var alt = distances[u] + 1;
                if (alt < distances[v])
                {
                    distances[v] = alt;
                    prev[v] = u;
                    queue.Remove(v, out _, out _);
                    queue.Enqueue(v, alt);
                }
            }

            //East
            v = u + 1;
            if ((col + 1) < width && (!walls[v]))
            {
                var alt = distances[u] + 1;
                if (alt < distances[v])
                {
                    distances[v] = alt;
                    prev[v] = u;
                    queue.Remove(v, out _, out _);
                    queue.Enqueue(v, alt);
                }
            }

        }

        return (prev, distances);
    }
}