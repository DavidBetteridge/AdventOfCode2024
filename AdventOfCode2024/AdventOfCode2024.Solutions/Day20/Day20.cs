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
        
       
        var distances = CostMap(height, width, start, walls);
        var target = end.Item2 * width + end.Item1;
        var worstCaseCost = distances[target];
        var goodCheats = 0;
        
        for (var rowNumber = 0; rowNumber < height; rowNumber++)
        {
            for (var columnNumber = 0; columnNumber < width; columnNumber++)
            {
                if (walls[rowNumber * width + columnNumber])
                {
                    walls[rowNumber * width + columnNumber] = false;
                    distances = CostMap(height, width, start, walls);
                    var newCost = distances[target];
                    if (worstCaseCost - newCost >= savesAtLeast)
                        goodCheats++;
                    walls[rowNumber * width + columnNumber] = true;
                }
            }
        }

        return goodCheats;
    }

    private static Distance[] CostMap(int height, int width, (int, int) start, bool[] walls)
    {
        var distances = new Distance[height * width];
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
                    queue.Remove(v, out _, out _);
                    queue.Enqueue(v, alt);
                }
            }

        }

        return distances;
    }
}