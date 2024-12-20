// ReSharper disable UseSymbolAlias
namespace AdventOfCode2024.Solutions;
using Distance = int;
public class Day20_Part2
{
    public int Part2(string filename, int savesAtLeast, int cheatTime)
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
        
        var temp = target;
        var source = start.Item2 * width + start.Item1;
        var path = new List<int>();
        while (true)
        {
            path.Add(temp);
            if (temp == source) break;
            temp = forward.Path[temp];
        }

        var goodCheats = 0;

        Parallel.ForEach(path, location =>
        {
            var count = 0;
            var columnNumber = location % width;
            var rowNumber = location / width;

            // Is there a non-walled cell within 20 spaces in any direction
            // which cost from the end + distance from us + plus our distance
            // is 100 less than the worst case cost.
            for (var yOffset = -cheatTime; yOffset <= cheatTime; yOffset++)
            {
                var newY = rowNumber + yOffset;
                if (newY > 0 && newY < (height - 1))
                {
                    var remainingTime = cheatTime - Math.Abs(yOffset);
                    for (var xOffset = -remainingTime; xOffset <= remainingTime; xOffset++)
                    {
                        var offset = Math.Abs(yOffset) + Math.Abs(xOffset);
                        if (offset > 1)
                        {
                            var newX = columnNumber + xOffset;
                            if (newX > 0 && newX < (width - 1))
                            {
                                var newLoc = newY * width + newX;
                                if (!walls[newLoc] && newLoc != location)
                                {
                                    var cost = backwards[newLoc] + forward.Cost[location] + offset;
                                    if ((worstCaseCost - cost) >= savesAtLeast)
                                    {
                                        count++;
                                        
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Interlocked.Add(ref goodCheats, count);
        });

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