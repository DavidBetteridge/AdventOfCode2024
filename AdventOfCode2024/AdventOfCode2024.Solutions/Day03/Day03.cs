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


    
    public int Part2_StateMachine(string filename)
    {
        var text = File.ReadAllText(filename).AsSpan();
        
        // don't()
        //01234567
        
        // do()
        //01289
        
        //   m  u  l  (  [0  1  2  3  4  5  6  7  8  9]  ,  0  1  2  3  4  5  6  7  8  9 )
        //0 10 11 12 13   14 14 14 14 14 14 14 14 14 14 15 16 16 16 16 16 16 16 16 16 16 17

        var fns = new Func<char, int>[]
        {
            //0
            (c) => c switch {
                'd' => 1,
                'm' => 10,
                _ => 0
            },
            
            //1
            (c) => c switch {
                'd' => 1,
                'm' => 10,
                'o' => 2,
                _ => 0
            },
            
            //2
            (c) => c switch {
                'd' => 1,
                'm' => 10,
                'n' => 3,
                '(' => 8,
                _ => 0
            },
            
            //3
            (c) => c switch {
                'd' => 1,
                'm' => 10,
                '\'' => 4,
                _ => 0
            },
            
            (c) => c switch {
                'd' => 1,
                'm' => 10,
                't' => 5,
                _ => 0
            },
            
            (c) => c switch {
                'd' => 1,
                'm' => 10,
                '(' => 6,
                _ => 0
            },
            
            (c) => c switch {
                'd' => 1,
                'm' => 10,
                ')' => 7,
                _ => 0
            },
            
            (c) => c switch {
                'd' => 1,
                'm' => 10,
                _ => 0
            },
            
            (c) => c switch {
                'd' => 1,
                'm' => 10,
                ')' => 9,
                _ => 0
            },
            
            (c) => c switch {
                'd' => 1,
                'm' => 10,
                _ => 0
            },
            
            (c) => c switch {
                 'd' => 1,
                 'm' => 10,
                 'u' => 11,
                 _ => 0
            },
            
            (c) => c switch {
                 'd' => 1,
                 'm' => 10,
                 'l' => 12,
                 _ => 0
            },
            
            (c) => c switch {
                'd' => 1,
                'm' => 10,
                '(' => 13,
                _ => 0
            },
            
            (c) => char.IsDigit(c) ? 14 : c switch {
                'd' => 1,
                'm' => 10,
                _ => 0
            },
            
            (c) => char.IsDigit(c) ? 14 : c switch {
                'd' => 1,
                'm' => 10,
                ',' => 15,
                _ => 0
            },
            
            (c) => char.IsDigit(c) ? 16 : c switch {
                'd' => 1,
                'm' => 10,
                _ => 0
            },
            
            (c) => char.IsDigit(c) ? 16 : c switch {
                'd' => 1,
                'm' => 10,
                ')' => 17,
                _ => 0
            },
            
            (c) => c switch {
                'd' => 1,
                'm' => 10,
                _ => 0
            },
        };
        
        var currentState = 0;
        var enabled = true;
        var total = 0;
        var lhs = 0;
        var rhs = 0;
        var shortLen = text.Length - 1;
        for (var i = 0; i < text.Length; i++)
        {
            currentState = fns[currentState](text[i]);

            switch (currentState)
            {
                case 14:
                {
                    lhs = text[i] - '0';
                    var next = i < shortLen ? text[i + 1] - '0' : -1;
                    while (next is >= 0 and <= 9 )
                    {
                        i++;
                        lhs = (lhs * 10) + next;
                        next = i < shortLen ? text[i + 1] - '0' : -1;
                    }

                    break;
                }
                case 16:
                {
                    rhs = text[i] - '0';
                    var next = i < shortLen ? text[i + 1] - '0' : -1;
                    while (next is >= 0 and <= 9 )
                    {
                        i++;
                        rhs = (rhs * 10) + next;
                        next = i < shortLen ? text[i + 1] - '0' : -1;
                    }

                    break;
                }
                case 7:
                    enabled = false;
                    break;
                case 9:
                    enabled = true;
                    break;
                case 17:
                {
                    if (enabled)
                        total += (lhs * rhs);
                    break;
                }
            }
        }
        return total;
    }

    public int Part2_Regex(string filename)
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