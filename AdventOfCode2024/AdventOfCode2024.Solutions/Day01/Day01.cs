using System.Buffers;

namespace AdventOfCode2024.Solutions;

public class Day01
{
    public int Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);

        var lhs = new List<int>();
        var rhs = new List<int>();

        foreach (var line in lines)
        {
            var parts = line.Split("   "); 
            lhs.Add(int.Parse(parts[0]));
            rhs.Add(int.Parse(parts[1]));
        }
        
        lhs.Sort();
        rhs.Sort();

        return lhs.Zip(rhs, (l, h) => Math.Abs(l - h)).Sum();
    }
    
    public int Part2(string filename)
    {
        var input = File.ReadAllText(filename).AsSpan();

        var lhs = new List<int>();
        var rhs = new Dictionary<int, int>();

        var sv = SearchValues.Create(" \n");

        var state = 0;
        foreach (var bit in input.SplitAny(sv))
        {
            if (state == 0)
            {
                lhs.Add(int.Parse(input[bit]));
                state = 1;
            }
            else if (state == 3)
            {
                var r = int.Parse(input[bit]);
                rhs[r] = rhs.GetValueOrDefault(r) + 1;
                state = 0;
            }
            else
            {
                state++;
            }
        }
        
        return lhs.Sum(l => l * rhs.GetValueOrDefault(l));
    }
}