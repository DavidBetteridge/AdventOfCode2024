// ReSharper disable UseSymbolAlias
namespace AdventOfCode2024.Solutions;
using Distance = int;
public class Day20_Part2
{
    public async Task<int> Part2(string filename, int savesAtLeast, int cheatTime)
    {
        var map = await File.ReadAllLinesAsync(filename);
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
        
        var reverseDistances = new Distance[height * width];
        var forwardCosts = new Distance[height * width];
        var forwardPath = new int[height * width];
        
        var t1 = Task.Run(() => CostForwardMap(start));
        var t2 = Task.Run(() => CostReverseMap(end));
        await Task.WhenAll([t1, t2]);

        
        var target = end.Item2 * width + end.Item1;
        var worstCaseCost = forwardCosts[target] - savesAtLeast;
        
        var temp = target;
        var source = start.Item2 * width + start.Item1;
        var path = new List<int>();
        while (true)
        {
            path.Add(temp);
            if (temp == source) break;
            temp = forwardPath[temp];
        }

        var goodCheats = 0;

        Parallel.ForEach(path, location =>
        {
            unchecked
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
                                        var cost = reverseDistances[newLoc] + forwardCosts[location] + offset;
                                        if (cost <= worstCaseCost)
                                            count++;

                                    }
                                }
                            }
                        }
                    }

                }

                Interlocked.Add(ref goodCheats, count);
            }
        });

        return goodCheats;


        void CostForwardMap((int, int) measureFrom)
        {
            var measureFromIndex = measureFrom.Item2 * width + measureFrom.Item1;
            var queue = new PriorityQueue<int, Distance>();
            queue.Enqueue(measureFromIndex,0);
            
            Array.Fill(forwardCosts, Distance.MaxValue);
            forwardCosts[measureFromIndex] = 0;

            while (queue.Count > 0)
            {
                var u = queue.Dequeue();

                var col = u % width;
                var row = u / width;

                //North
                var v = u - width;
                if (row > 0 && (!walls[v]))
                {
                    var alt = forwardCosts[u] + 1;
                    if (alt < forwardCosts[v])
                    {
                        forwardCosts[v] = alt;
                        forwardPath[v] = u;
                        queue.Remove(v, out _, out _);
                        queue.Enqueue(v, alt);
                    }
                }
                
                //South
                v = u + width;
                if ((row + 1) < height && (!walls[v]))
                {
                    var alt = forwardCosts[u] + 1;
                    if (alt < forwardCosts[v])
                    {
                        forwardCosts[v] = alt;
                        forwardPath[v] = u;
                        queue.Remove(v, out _, out _);
                        queue.Enqueue(v, alt);
                    }
                }

                //West
                v = u - 1;
                if (col > 0 && (!walls[v]))
                {
                    var alt = forwardCosts[u] + 1;
                    if (alt < forwardCosts[v])
                    {
                        forwardCosts[v] = alt;
                        forwardPath[v] = u;
                        queue.Remove(v, out _, out _);
                        queue.Enqueue(v, alt);
                    }
                }

                //East
                v = u + 1;
                if ((col + 1) < width && (!walls[v]))
                {
                    var alt = forwardCosts[u] + 1;
                    if (alt < forwardCosts[v])
                    {
                        forwardCosts[v] = alt;
                        forwardPath[v] = u;
                        queue.Remove(v, out _, out _);
                        queue.Enqueue(v, alt);
                    }
                }

            }
        }
        
        void CostReverseMap((int, int) measureFrom)
        {
            var queue = new PriorityQueue<int, Distance>();
            var measureFromIndex = measureFrom.Item2 * width + measureFrom.Item1;
            queue.Enqueue(measureFromIndex,0);
            
            Array.Fill(reverseDistances, Distance.MaxValue);
            reverseDistances[measureFromIndex] = 0;

            while (queue.Count > 0)
            {
                var u = queue.Dequeue();

                var col = u % width;
                var row = u / width;

                //North
                var v = u - width;
                if (row > 0 && (!walls[v]))
                {
                    var alt = reverseDistances[u] + 1;
                    if (alt < reverseDistances[v])
                    {
                        reverseDistances[v] = alt;
                        queue.Remove(v, out _, out _);
                        queue.Enqueue(v, alt);
                    }
                }
                
                //South
                v = u + width;
                if ((row + 1) < height && (!walls[v]))
                {
                    var alt = reverseDistances[u] + 1;
                    if (alt < reverseDistances[v])
                    {
                        reverseDistances[v] = alt;
                        queue.Remove(v, out _, out _);
                        queue.Enqueue(v, alt);
                    }
                }

                //West
                v = u - 1;
                if (col > 0 && (!walls[v]))
                {
                    var alt = reverseDistances[u] + 1;
                    if (alt < reverseDistances[v])
                    {
                        reverseDistances[v] = alt;
                        queue.Remove(v, out _, out _);
                        queue.Enqueue(v, alt);
                    }
                }

                //East
                v = u + 1;
                if ((col + 1) < width && (!walls[v]))
                {
                    var alt = reverseDistances[u] + 1;
                    if (alt < reverseDistances[v])
                    {
                        reverseDistances[v] = alt;
                        queue.Remove(v, out _, out _);
                        queue.Enqueue(v, alt);
                    }
                }

            }
        }
    }
    
}
