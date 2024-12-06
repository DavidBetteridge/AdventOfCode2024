using System.Buffers;
using System.Numerics;
using System.Numerics.Tensors;
using System.Reflection;

namespace AdventOfCode2024.Solutions;

public class Day01
{
    public int Part1(string filename)
    {
        var input = File.ReadAllBytes(filename).AsSpan();

        var lhs = new List<int>();
        var rhs = new List<int>();

        var i = 0;
        while (i < input.Length)
        {
            // 58789   28882
            
            // First number
            var l = input[i] - '0';
            i++;
            while (input[i] != ' ')
            {
                l = l * 10 + (input[i] - '0');
                i++;
            }

            lhs.Add(l);
            
            // Spaces
            while (input[i] == ' ')
                i++;
            
            // Second number
            var r = input[i] - '0';
            i++;
            while (i < input.Length && input[i] != '\n')
            {
                r = r * 10 + (input[i] - '0');
                i++;
            }
            rhs.Add(r);
            
            // Skip the newline
            i++;
        }
        
        lhs.Sort();
        rhs.Sort();

        var listType = typeof(List<int>);
        var itemsField = listType.GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance);
        var la = new Span<int>((int[])itemsField!.GetValue(lhs)!, 0, lhs.Count) ;
        var ra = new Span<int>((int[])itemsField.GetValue(rhs)!, 0, rhs.Count) ;
        
        TensorPrimitives.Subtract(la, ra, la);
        TensorPrimitives.Abs(la,la);
        return TensorPrimitives.Sum<int>(la);
    }
    
    public int Part2(string filename)
    {
        var input = File.ReadAllBytes(filename).AsSpan();

        var lhs = new List<int>();
        var rhs = new Dictionary<int, int>();

        var i = 0;
        while (i < input.Length)
        {
            // 58789   28882
            
            // First number
            var l = input[i] - '0';
            i++;
            while (input[i] != ' ')
            {
                l = l * 10 + (input[i] - '0');
                i++;
            }

            lhs.Add(l);
            
            // Spaces
            while (input[i] == ' ')
                i++;
            
            // Second number
            var r = input[i] - '0';
            i++;
            while (i < input.Length && input[i] != '\n')
            {
                r = r * 10 + (input[i] - '0');
                i++;
            }
            rhs[r] = rhs.GetValueOrDefault(r) + r;
            
            // Skip the newline
            i++;
        }
        
        return lhs.Sum(l => rhs.GetValueOrDefault(l));
    }
}