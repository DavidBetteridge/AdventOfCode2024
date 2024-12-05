using System.Buffers;
using System.Numerics;
using System.Numerics.Tensors;
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
        
        Span<int> la = lhs.ToArray();
        Span<int> ra = rhs.ToArray();
        
        TensorPrimitives.Subtract(la, ra, la);
        TensorPrimitives.Abs(la,la);
        return TensorPrimitives.Sum<int>(la);
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