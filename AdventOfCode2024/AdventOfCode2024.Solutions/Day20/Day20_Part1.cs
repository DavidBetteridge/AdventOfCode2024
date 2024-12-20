// ReSharper disable UseSymbolAlias
namespace AdventOfCode2024.Solutions;
using Distance = int;
public class Day20_Part1
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
        
       
        var forward = CostMap(height, width, start, walls);
        var backwards = CostMap(height, width, end, walls).Cost;
        
        var target = end.Item2 * width + end.Item1;
        var worstCaseCost = forward.Cost[target];
        
        var location = target;
        var source = start.Item2 * width + start.Item1;
        var tried = new HashSet<int>();
        var goodCheats = 0;
        while (true)
        {
            var columnNumber = location % width;
            var rowNumber = location / width;

            // Above
            var remove = location - width;
            if (rowNumber > 1 && walls[remove] && !tried.Contains(remove))
            {
                var n = backwards[remove - width];
                var w = backwards[remove - 1];
                var e = backwards[remove + 1];
                
                if (worstCaseCost - (n + forward.Cost[location] + 1) > savesAtLeast ||
                    worstCaseCost - (w + forward.Cost[location] + 1) > savesAtLeast ||
                    worstCaseCost - (e + forward.Cost[location] + 1) > savesAtLeast)
                {
                    goodCheats++;
                    tried.Add(remove);
                }
            }
            
            // Below
            remove = location + width;
            if (rowNumber < (height-2) && walls[remove] && !tried.Contains(remove))
            {
                var s = backwards[remove + width];
                var w = backwards[remove - 1];
                var e = backwards[remove + 1];
                if (worstCaseCost - (s + forward.Cost[location] + 1) > savesAtLeast ||
                    worstCaseCost - (w + forward.Cost[location] + 1) > savesAtLeast ||
                    worstCaseCost - (e + forward.Cost[location] + 1) > savesAtLeast)
                {
                    goodCheats++;
                    tried.Add(remove);
                }
            }
            
            // Left
            remove = location - 1;
            if (columnNumber > 1 && walls[remove] && !tried.Contains(remove))
            {
                var s = backwards[remove + width];
                var w = backwards[remove - 1];
                var n = backwards[remove - width];
                if (worstCaseCost - (s + forward.Cost[location] + 1) > savesAtLeast ||
                    worstCaseCost - (w + forward.Cost[location] + 1) > savesAtLeast ||
                    worstCaseCost - (n + forward.Cost[location] + 1) > savesAtLeast)
                {
                    goodCheats++;
                    tried.Add(remove);
                }
            }
            
            // Right
            remove = location + 1;
            if (columnNumber < (width-2) && walls[remove] && !tried.Contains(remove))
            {
                var s = backwards[remove + width];
                var e = backwards[remove + 1];
                var n = backwards[remove - width];
                if (worstCaseCost - (s + forward.Cost[location] + 1) > savesAtLeast ||
                    worstCaseCost - (n + forward.Cost[location] + 1) > savesAtLeast ||
                    worstCaseCost - (e + forward.Cost[location] + 1) > savesAtLeast)
                {
                    goodCheats++;
                    tried.Add(remove);
                }
            }

            if (location == source) break;
            location = forward.Path[location];
        }

        return goodCheats;

    }

    private static (int[] Path, Distance[] Cost) CostMap(int height, int width, (int, int) start, bool[] walls)
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