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
                var key = new int[5] { 5, 5, 5, 5, 5 };
                for (var r = 0; r < 5; r++)
                {
                    for (var c = 0; c < 5; c++)
                    {
                        if (input[i] == '.')
                            key[c]--;
                        i++;
                    }

                    i++; // New line
                }

                keys.Add(key);
            }
            else
            {
                var loc = new int[5] { 0,0,0,0,0 };
                for (var r = 0; r < 5; r++)
                {
                    for (var c = 0; c < 5; c++)
                    {
                        if (input[i] == '.')
                            loc[c]++;
                        i++;
                    }
                    i++; // New line
                }
                locks.Add(loc);
            }

            i += 7;  // Trailer and blank line
        }
        

        var total = 0;
        foreach (var key in keys)
        {
            foreach (var loc in locks)
            {
                var ok = true;
                for (var j = 0; j < 5; j++)
                {
                    if (key[j] > loc[j])
                    {
                        ok = false;
                        break;
                    }
                }

                if (ok)
                    total++;
            }
        }

        return total;
    }
}