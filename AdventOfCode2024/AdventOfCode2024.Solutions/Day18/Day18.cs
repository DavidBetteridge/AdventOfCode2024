
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
        distances[source] = 0;

        for (var row = 0; row < size; row++)
        {
            for (var col = 0; col < size; col++)
            {
                var v = ((row * size) + col);
                if (!memory[v])
                {
                    if (v != source)
                    {
                        distances[v] = int.MaxValue;
                    }
                }
                else
                {
                    distances[v] = -1;
                }
            }
        }


        while (queue.Count > 0)
        {
            var u = queue.Dequeue();
     //       if (distances[u] == int.MaxValue) break;

            var col = u % size;
            var row = (u - col) / size;

         //   if (col == size-1 && row == size-1) break;

            //North
            if (row > 0 && (!memory[(row - 1) * size + col]))
            {
                var v = ((row - 1) * size) + col;
                var alt = distances[u] + 1;
                if (alt < distances[v])
                {
                    distances[v] = alt;
                    queue.Remove(v, out _, out _);
                    queue.Enqueue(v, alt);
                }
            }

            //South
            if ((row+1) < size && (!memory[(row + 1) * size + col]))
            {
                var v = ((row + 1) * size) + col;
                var alt = distances[u] + 1;
                if (alt < distances[v])
                {
                    distances[v] = alt;
                    queue.Remove(v, out _, out _);
                    queue.Enqueue(v, alt);
                }
            }
            
            //West
            if (col > 0 && (!memory[(row) * size + (col-1)]))
            {
                var v = ((row) * size) + (col-1);
                var alt = distances[u] + 1;
                if (alt < distances[v])
                {
                    distances[v] = alt;
                    queue.Remove(v, out _, out _);
                    queue.Enqueue(v, alt);
                }
            }
            
            //East
            if ((col+1) < size && (!memory[(row) * size + (col+1)]))
            {
                var v = ((row) * size) + (col+1);
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
}