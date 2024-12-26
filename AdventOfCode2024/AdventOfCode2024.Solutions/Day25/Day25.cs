using System.Collections.Concurrent;

namespace AdventOfCode2024.Solutions;

public class Day25
{
    public int Part1(string filename)
    {
        var input = File.ReadAllBytes(filename);

        var keys = new List<int[]>();
        var locks = new List<int[]>();

        var i = 0;
        while (i < input.Length)
        {
            var isKey = input[i] == '.';
            i += 6;

            if (isKey)
            {
                var key = new int[6] { 5, 5, 5, 5, 5, 25 };
                for (var r = 0; r < 5; r++)
                {
                    for (var c = 0; c < 5; c++)
                    {
                        if (input[i] == '.')
                        {
                            key[c]--;
                        }

                        i++;
                    }

                    key[5] = key[0] + key[1] + key[2] + key[3] + key[4];

                    i++; // New line
                }

                keys.Add(key);
            }
            else
            {
                var loc = new int[6] { 0, 0, 0, 0, 0, 0 };
                for (var r = 0; r < 5; r++)
                {
                    for (var c = 0; c < 5; c++)
                    {
                        if (input[i] == '.')
                        {
                            loc[c]++;
                        }

                        i++;
                    }

                    loc[5] = loc[0] + loc[1] + loc[2] + loc[3] + loc[4];
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
                var ok = loc[5] >= key[5];
                ok &= loc[0] >= key[0];
                ok &= loc[1] >= key[1];
                ok &= loc[2] >= key[2];
                ok &= loc[3] >= key[3];
                ok &= loc[4] >= key[4];

                if (ok)
                    total.Add(1);
            }
        });

        return total.Count;
    }
}