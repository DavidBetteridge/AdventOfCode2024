using System.Collections.Concurrent;

namespace AdventOfCode2024.Solutions;

public class Day25
{
    public int Part1(string filename)
    {
        var input = File.ReadAllBytes(filename);

        var keys = new List<uint>();
        var locks = new List<uint>();

        var i = 0;
        while (i < input.Length)
        {
            var isKey = input[i] == '.';
            i += 6;

            if (isKey)
            {
                uint key = 0;
                for (var r = 0; r < 5; r++)
                {
                    for (var c = 0; c < 5; c++)
                    {
                        key <<= 1;
                        if (input[i] == '#')
                        {
                            key ++;
                        }

                        i++;
                    }
                    i++; // New line
                }

                keys.Add(key);
            }
            else
            {
                uint loc = 0;
                for (var r = 0; r < 5; r++)
                {
                    for (var c = 0; c < 5; c++)
                    {
                        loc <<= 1;
                        if (input[i] == '#')
                        {
                            loc++;
                        }

                        i++;
                    }
                    i++; // New line
                }

                locks.Add(loc);
            }

            i += 7; // Trailer and blank line
        }


        var total = new ConcurrentBag<int>();

        Parallel.ForEach(keys, key =>
        {
            foreach (var loc in locks)
            {
                if ((loc & key) == 0)
                    total.Add(1);
            }
        });

        return total.Count;
    }
}