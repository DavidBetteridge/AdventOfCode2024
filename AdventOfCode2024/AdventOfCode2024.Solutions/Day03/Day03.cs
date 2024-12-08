using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solutions;

public class Day03
{
    public int Part1_Regex(string filename)
    {
        var text = File.ReadAllText(filename);

        var matches = Day3Matcher.Part1().Matches(text);
        var total = 0;
        foreach (Match match in matches)
            total += (int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));

        return total;
    }

    public int Part1(string filename)
    {
        var text = File.ReadAllBytes(filename).AsSpan();
        var total = 0;
        var i = 0;
        try
        {
            while (i < text.Length - 7)
            {
                if (text[i] == 'm' && text[i + 1] == 'u' && text[i + 2] == 'l' && text[i + 3] == '(')
                {
                    i += 4;

                    if (text[i] >= 48 && text[i] <= 57)
                    {
                        var lhs = text[i] - 48;
                        i++;

                        // Keep advancing whilst we have a number
                        while (text[i] >= 48 && text[i] <= 57)
                        {
                            lhs = (lhs * 10) + text[i] - 48;
                            i++;
                        }

                        // Eat the comma
                        if (text[i] == ',')
                        {
                            i++;

                            // Get the RHS
                            if (text[i] >= 48 && text[i] <= 57)
                            {
                                var rhs = text[i] - 48;
                                i++;

                                // Keep advancing whilst we have a number
                                while (text[i] >= 48 && text[i] <= 57)
                                {
                                    rhs = (rhs * 10) + text[i] - 48;
                                    i++;
                                }

                                // Eat the bracket
                                if (text[i] == ')')
                                {
                                    i++;
                                    total += (lhs * rhs);
                                }
                            }
                        }
                    }
                }
                else
                {
                    i++;
                }
            }
        }
        catch
        {
            //
        }

        return total;
    }

    public int Part2(string filename)
    {
        var text = File.ReadAllBytes(filename).AsSpan();
        var total = 0;
        var i = 0;
        var enabled = true;

        try
        {
            while (i < text.Length - 7)
            {
                if (text[i] == 'd' && text[i + 1] == 'o' && text[i + 2] == '(' && text[i + 3] == ')')
                {
                    enabled = true;
                    i += 4;
                    continue;
                }

                if (text[i] == 'd' && text[i + 1] == 'o' && text[i + 2] == 'n' && text[i + 3] == '\'' &&
                    text[i + 4] == 't' && text[i + 5] == '(' && text[i + 6] == ')')
                {
                    enabled = false;
                    i += 7;
                    continue;
                }

                if (text[i] == 'm' && text[i + 1] == 'u' && text[i + 2] == 'l' && text[i + 3] == '(')
                {
                    i += 4;

                    if (enabled && text[i] >= 48 && text[i] <= 57)
                    {
                        var lhs = text[i] - 48;
                        i++;

                        // Keep advancing whilst we have a number
                        while (text[i] >= 48 && text[i] <= 57)
                        {
                            lhs = (lhs * 10) + text[i] - 48;
                            i++;
                        }

                        // Eat the comma
                        if (text[i] == ',')
                        {
                            i++;

                            // Get the RHS
                            if (text[i] >= 48 && text[i] <= 57)
                            {
                                var rhs = text[i] - 48;
                                i++;

                                // Keep advancing whilst we have a number
                                while (text[i] >= 48 && text[i] <= 57)
                                {
                                    rhs = (rhs * 10) + text[i] - 48;
                                    i++;
                                }

                                // Eat the bracket
                                if (text[i] == ')')
                                {
                                    i++;
                                    total += (lhs * rhs);
                                }
                            }
                        }
                    }
                }
                else
                {
                    i++;
                }
            }
        }
        catch
        {
            //
        }

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

        var currentState = 0;
        var enabled = true;
        var total = 0;
        var lhs = 0;
        var rhs = 0;
        var shortLen = text.Length - 1;
        for (var i = 0; i < text.Length; i++)
        {
            //  currentState = fns[currentState](text[i]);

            var c = text[i];
            currentState = currentState switch
            {
                0 => c switch
                {
                    'd' => 1,
                    'm' => 10,
                    _ => 0
                },

                //1
                1 => c switch
                {
                    'd' => 1,
                    'm' => 10,
                    'o' => 2,
                    _ => 0
                },

                //2
                2 => c switch
                {
                    'd' => 1,
                    'm' => 10,
                    'n' => 3,
                    '(' => 8,
                    _ => 0
                },

                //3
                3 => c switch
                {
                    'd' => 1,
                    'm' => 10,
                    '\'' => 4,
                    _ => 0
                },

                4 => c switch
                {
                    'd' => 1,
                    'm' => 10,
                    't' => 5,
                    _ => 0
                },

                5 => c switch
                {
                    'd' => 1,
                    'm' => 10,
                    '(' => 6,
                    _ => 0
                },

                6 => c switch
                {
                    'd' => 1,
                    'm' => 10,
                    ')' => 7,
                    _ => 0
                },

                7 => c switch
                {
                    'd' => 1,
                    'm' => 10,
                    _ => 0
                },

                8 => c switch
                {
                    'd' => 1,
                    'm' => 10,
                    ')' => 9,
                    _ => 0
                },

                9 => c switch
                {
                    'd' => 1,
                    'm' => 10,
                    _ => 0
                },

                10 => c switch
                {
                    'd' => 1,
                    'm' => 10,
                    'u' => 11,
                    _ => 0
                },

                11 => c switch
                {
                    'd' => 1,
                    'm' => 10,
                    'l' => 12,
                    _ => 0
                },

                12 => c switch
                {
                    'd' => 1,
                    'm' => 10,
                    '(' => 13,
                    _ => 0
                },

                13 => char.IsDigit(c)
                    ? 14
                    : c switch
                    {
                        'd' => 1,
                        'm' => 10,
                        _ => 0
                    },

                14 => char.IsDigit(c)
                    ? 14
                    : c switch
                    {
                        'd' => 1,
                        'm' => 10,
                        ',' => 15,
                        _ => 0
                    },

                15 => char.IsDigit(c)
                    ? 16
                    : c switch
                    {
                        'd' => 1,
                        'm' => 10,
                        _ => 0
                    },

                16 => char.IsDigit(c)
                    ? 16
                    : c switch
                    {
                        'd' => 1,
                        'm' => 10,
                        ')' => 17,
                        _ => 0
                    },

                17 => c switch
                {
                    'd' => 1,
                    'm' => 10,
                    _ => 0
                },
            };


            switch (currentState)
            {
                case 14:
                {
                    if (enabled)
                    {
                        lhs = text[i] - '0';
                        var next = i < shortLen ? text[i + 1] - '0' : -1;
                        while (next is >= 0 and <= 9)
                        {
                            i++;
                            lhs = (lhs * 10) + next;
                            next = i < shortLen ? text[i + 1] - '0' : -1;
                        }
                    }
                    else
                    {
                        while (i < shortLen && char.IsDigit(text[i + 1]))
                            i++;
                    }

                    break;
                }
                case 16:
                {
                    if (enabled)
                    {
                        rhs = text[i] - '0';
                        var next = i < shortLen ? text[i + 1] - '0' : -1;
                        while (next is >= 0 and <= 9)
                        {
                            i++;
                            rhs = (rhs * 10) + next;
                            next = i < shortLen ? text[i + 1] - '0' : -1;
                        }
                    }
                    else
                    {
                        while (i < shortLen && char.IsDigit(text[i + 1]))
                            i++;
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
                        total += (int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));

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