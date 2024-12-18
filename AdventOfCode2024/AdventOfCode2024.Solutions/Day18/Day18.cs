
namespace AdventOfCode2024.Solutions;
using Distance = int;
public class Day18
{
    public int Part1(string filename, int size, int numberOfDrops)
    {
        var memory = new bool[size * size];

        var input = File.ReadAllBytes(filename).AsSpan();
        var i = 0;
        while (i < input.Length && numberOfDrops > 0)
        {
            var lhs = input[i++] - '0';
            while (input[i] != ',')
                lhs = lhs * 10 + input[i++] - '0';
            i++;
            
            var rhs = input[i++] - '0';
            while (i < input.Length && input[i] != '\n')
                rhs = rhs * 10 + input[i++] - '0';
            i++;
            
            memory[ rhs * size + lhs ] = true;
            numberOfDrops--;
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
        
        var input = File.ReadAllBytes(filename).AsSpan();
        var i = 0;
        var drops = new List<int>();
        while (i < input.Length)
        {
            var lhs = input[i++] - '0';
            while (input[i] != ',')
                lhs = lhs * 10 + input[i++] - '0';
            i++;
            
            var rhs = input[i++] - '0';
            while (i < input.Length && input[i] != '\n')
                rhs = rhs * 10 + input[i++] - '0';
            i++;
            
            drops.Add(rhs * size + lhs);
        }
        

        var lowerBound = startFrom;
        var upperBound = drops.Count - 1;
        var lastWorked = startFrom;
        var lastFailed = drops.Count;
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