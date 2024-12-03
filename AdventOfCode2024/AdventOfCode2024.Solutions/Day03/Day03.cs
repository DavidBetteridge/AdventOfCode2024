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


    
    public int Part2b(string filename)
    {
        var text = File.ReadAllText(filename).AsSpan();
        
        // don't()
        //01234567
        
        // do()
        //01289
        
        //   m  u  l  (  [0  1  2  3  4  5  6  7  8  9]  ,  0  1  2  3  4  5  6  7  8  9 )
        //0 10 11 12 13   14 14 14 14 14 14 14 14 14 14 15 16 16 16 16 16 16 16 16 16 16 17

        var rules = new Dictionary<char, int>[]
        {
            //0
            new (){
                ['d'] = 1,
                ['m'] = 10,
            },
            
            //1
            new () {
                ['d'] = 1,
                ['m'] = 10,
                ['o'] = 2
            },
            
            //2
            new () {
                ['d'] = 1,
                ['m'] = 10,
                ['n'] = 3,
                ['('] = 8
            },
            
            //3
            new () {
                ['d'] = 1,
                ['m'] = 10,
                ['\''] = 4
            },
            
            new () {
                ['d'] = 1,
                ['m'] = 10,
                ['t'] = 5
            },
            
            new () {
                ['d'] = 1,
                ['m'] = 10,
                ['('] = 6
            },
            
            new (){
                ['d'] = 1,
                ['m'] = 10,
                [')'] = 7
            },
            
            new ()
            {
                ['d'] = 1,
                ['m'] = 10
            },
            
            new ()
            {
                ['d'] = 1,
                ['m'] = 10,
                [')'] = 9
            },
            
            new ()
            {
                ['d'] = 1,
                ['m'] = 10
            },
            
            new (){
                 ['d'] = 1,
                 ['m'] = 10,
                 ['u'] = 11
            },
            
            new () {
                 ['d'] = 1,
                 ['m'] = 10,
                 ['l'] = 12
            },
            
            new () {
                ['d'] = 1,
                ['m'] = 10,
                ['('] = 13
            },
            
            new () {
                ['d'] = 1,
                ['m'] = 10,
                ['0'] = 14,
                ['1'] = 14,
                ['2'] = 14,
                ['3'] = 14,
                ['4'] = 14,
                ['5'] = 14,
                ['6'] = 14,
                ['7'] = 14,
                ['8'] = 14,
                ['9'] = 14
            },
            
            new () {
                ['d'] = 1,
                ['m'] = 10,
                ['0'] = 14,
                ['1'] = 14,
                ['2'] = 14,
                ['3'] = 14,
                ['4'] = 14,
                ['5'] = 14,
                ['6'] = 14,
                ['7'] = 14,
                ['8'] = 14,
                ['9'] = 14,
                [','] = 15
            },
            
            new () {
                ['d'] = 1,
                ['m'] = 10,
                ['0'] = 16,
                ['1'] = 16,
                ['2'] = 16,
                ['3'] = 16,
                ['4'] = 16,
                ['5'] = 16,
                ['6'] = 16,
                ['7'] = 16,
                ['8'] = 16,
                ['9'] = 16
            },
            
            new () {
                ['d'] = 1,
                ['m'] = 10,
                ['0'] = 16,
                ['1'] = 16,
                ['2'] = 16,
                ['3'] = 16,
                ['4'] = 16,
                ['5'] = 16,
                ['6'] = 16,
                ['7'] = 16,
                ['8'] = 16,
                ['9'] = 16,
                [')'] = 17
            },
            
            new () {
                ['d'] = 1,
                ['m'] = 10
            },
        };
        
        var currentState = 0;
        var lhsStart = 0;
        var lhsEnd = 0;
        var rhsStart = 0;
        var enabled = true;
        var total = 0;
        for (var i = 0; i < text.Length; i++)
        {
            if (text[i] == 'd')
            {
                currentState = 1;
                continue;
            }
            if (text[i] == 'm')
            {
                currentState = 10;
                continue;
            }
            
            currentState = rules[currentState].GetValueOrDefault(text[i]);

            if (currentState == 7)
                enabled = false;
            else if (currentState == 9)
                enabled = true;
            else if (currentState == 17)
            {
                if (enabled)
                    total += int.Parse(text[lhsStart..lhsEnd]) * int.Parse(text[rhsStart..i]);
            }
            else if (currentState == 13)
                lhsStart = i + 1;

            else if (currentState == 15)
            {
                lhsEnd = i;
                rhsStart = i + 1;
            }
        }
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