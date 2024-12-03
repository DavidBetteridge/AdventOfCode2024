using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solutions;

public class Day03
{
    public int Part1(string filename)
    {
        var text = File.ReadAllText(filename);

        var matches = Day3Matcher.Commands().Matches(text);
        var products = matches.Select(m => int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value));
        
        return products.Sum();
    }
}

public partial class Day3Matcher
{
    [GeneratedRegex(@"mul\((?'lhs'\d+),(?'rhs'\d+)\)")]
    public static partial Regex Commands();
}