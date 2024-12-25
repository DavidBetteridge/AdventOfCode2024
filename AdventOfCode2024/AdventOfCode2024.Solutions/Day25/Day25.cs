namespace AdventOfCode2024.Solutions;

public class Day25
{
    public int Part1(string filename)
    {
        var input = File.ReadAllText(filename).Split("\n\n");

        var keys = new List<int[]>();
        var locks = new List<int[]>();
        foreach (var block in input)
        {
            var lines = block.Split('\n');
            var isKey = lines[0] == ".....";

            if (isKey)
            {
                var key = new int[5] { 5, 5, 5, 5, 5 };
                for (var r = 1; r < 6; r++)
                {
                    for (var c = 0; c < 5; c++)
                    {
                        if (lines[r][c] == '.')
                            key[c]--;
                    }
                }
                keys.Add(key);
            }
            else
            {
                var loc = new int[5] { 0,0,0,0,0 };
                for (var r = 1; r < 6; r++)
                {
                    for (var c = 0; c < 5; c++)
                    {
                        if (lines[r][c] == '.')
                            loc[c]++;
                    }
                }
                locks.Add(loc);    
            }
            
        }

        var total = 0;
        foreach (var key in keys)
        {
            foreach (var loc in locks)
            {
                var ok = true;
                for (var i = 0; i < 5; i++)
                {
                    if (key[i] > loc[i])
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