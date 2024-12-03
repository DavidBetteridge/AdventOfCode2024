using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solutions;

public class Day03
{
    public int Part1(string filename)
    {
        var text = File.ReadAllText(filename);

        var matches = Day3Matcher.Part1().Matches(text);
        var total = 0;
        foreach (Match match in matches)
            total+= (int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));

        return total;
    }
    
    public int Part2(string filename)
    {
        var text = File.ReadAllText(filename);

        var matches = Day3Matcher.Part2().Matches(text);
        var enabled = true;
        var total = 0;

        foreach (Match match in matches)
        {
            switch (match.Groups[0].Value)
            {
                case "do()":
                    enabled = true;
                    break;
                
                case "don't()":
                    enabled = false;
                    break;

                default:
                {
                    if (enabled)
                        total+= (int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));

                    break;
                }
            }
        }
        
        return total;
    }
}

public partial class Day3Matcher
{
    [GeneratedRegex(@"mul\((?'lhs'\d+),(?'rhs'\d+)\)")]
    public static partial Regex Part1();
    
    [GeneratedRegex(@"mul\((?'lhs'\d+),(?'rhs'\d+)\)|do\(\)|don't\(\)")]
    public static partial Regex Part2();
}