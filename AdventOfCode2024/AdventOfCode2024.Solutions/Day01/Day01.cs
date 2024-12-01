using System.Buffers;
using System.Numerics;

namespace AdventOfCode2024.Solutions;

public class Day01
{
    public int Part1(string filename)
    {
        var input = File.ReadAllText(filename).AsSpan();

        var lhs = new List<int>();
        var rhs = new List<int>();

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
                rhs.Add(int.Parse(input[bit]));
                state = 0;
            }
            else
            {
                state++;
            }
        }
        
        lhs.Sort();
        rhs.Sort();
        var la = lhs.ToArray();
        var ra = rhs.ToArray();

        var size = Vector<int>.Count;
        var runningTotal = new Vector<int>();
        var i = 0;
        while ((i+3) < lhs.Count)
        {
            var l = new Vector<int>(la[i..(i+size)]);
            var r = new Vector<int>(ra[i..(i+size)]);
            var diffs = Vector.Abs(l - r);
            runningTotal += diffs;
            i += size;
        }
        
        var result = 0;
        for (var j = i; j < lhs.Count; j++)
            result += Math.Abs(la[j] - ra[j]);
        
        for (var j = 0; j < size; j++)
            result += runningTotal[j];
        return result;
    }
    
    public int Part1_NoVec(string filename)
    {
        var input = File.ReadAllText(filename).AsSpan();

        var lhs = new List<int>();
        var rhs = new List<int>();

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
                rhs.Add(int.Parse(input[bit]));
                state = 0;
            }
            else
            {
                state++;
            }
        }
        
        lhs.Sort();
        rhs.Sort();

        return lhs.Select((t, j) => Math.Abs(t - rhs[j])).Sum();
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