
namespace AdventOfCode2024.Solutions;
using Distance = int;
public class Day18
{
    public int Part1(string filename, int size, int numberOfDrops)
    {
        var memory = new bool[size * size];
        var drops = File.ReadAllLines(filename);

        foreach (var drop in drops[..numberOfDrops])
        {
            // x,y
            var parts = drop.Split(',');
            memory[ int.Parse(parts[1]) * size + int.Parse(parts[0]) ] = true;
        }

        var distances = new Distance[size*size];
        var queue = new PriorityQueue<int, Distance>();
        
        var source = 0;
        queue.Enqueue(source,0);
        
        Array.Fill(distances, Distance.MaxValue);
        distances[source] = 0;

        while (queue.Count > 0)
        {
            var u = queue.Dequeue();

            var col = u % size;
            var row = u / size;

            //North
            var v = u - size;
            if (row > 0 && (!memory[v]))
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
            v = u + size;
            if ((row + 1) < size && (!memory[v]))
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
            if (col > 0 && (!memory[v]))
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
            if ((col + 1) < size && (!memory[v]))
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
        
        return distances[^1];
    }
    
    
    public string Part2(string filename, int size, int startFrom)
    {
        var memory = new bool[size * size];
        var drops = File.ReadAllLines(filename).
                Select(line =>
                {
                    var parts = line.Split(',');
                    return int.Parse(parts[1]) * size + int.Parse(parts[0]);
                }).ToArray();

        var lowerBound = startFrom;
        var upperBound = drops.Length - 1;
        var lastWorked = startFrom;
        var lastFailed = drops.Length;
        var distances = new Distance[size * size];
        var queue = new PriorityQueue<int, Distance>();
        
        while (true)
        {
            var numberOfDrops = (int)(upperBound + lowerBound) / 2;
            Array.Clear(memory);

            foreach (var drop in drops[..numberOfDrops])
                memory[drop] = true;
            
            queue.Clear();
            var source = 0;
            queue.Enqueue(source, 0);
            
            Array.Fill(distances, Distance.MaxValue);
            distances[source] = 0;

            while (queue.Count > 0)
            {
                var u = queue.Dequeue();
                var col = u % size;
                var row = u / size;

                //North
                var v = u - size;
                if (row > 0 && (!memory[v]))
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
                v = u + size;
                if ((row + 1) < size && (!memory[v]))
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
                if (col > 0 && (!memory[v]))
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
                if ((col + 1) < size && (!memory[v]))
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
            
            if (distances[^1] == int.MaxValue)
            {
                upperBound = numberOfDrops - 1;
                lastFailed = Math.Min(lastFailed, numberOfDrops);
            }
            else
            {
                lowerBound = numberOfDrops + 1;
                lastWorked = Math.Max(lastWorked, numberOfDrops);
            }

            if (lastWorked == lastFailed - 1)
            {
                var col = drops[lastWorked] % size;
                var row = (drops[lastWorked] - col) / size;
                return $"{col},{row}";
            }
            
        }
    }
}