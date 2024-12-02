using System.Buffers;

namespace AdventOfCode2024.Solutions;

public class Day02
{
    public int Part1(string filename)
    {
        var lines = File.ReadAllText(filename).AsSpan();
        var result = 0;
        foreach (var lineRange in lines.Split('\n'))
        {
            var i = 0;
            var increasing = false;
            var previousValue = 0;
            result++;
            foreach (var valRange in lines[lineRange].Split(' '))
            {
                var currentVal = int.Parse(lines[lineRange][valRange]);

                if (i > 0)
                {
                    var diff = currentVal - previousValue;
                    if (diff == 0 || Math.Abs(diff) > 3)
                    {
                        result--;
                        break;
                    }

                    if (i == 1)
                        increasing = diff > 0;
                    else if (increasing && diff < 0)
                    {
                        result--;
                        break;
                    }
                    else if (!increasing && diff > 0)
                    {
                        result--;
                        break;
                    }
                }

                previousValue = currentVal;
                i++;
            }
        }

        return result;
    }
    
    public int Part2(string filename)
    {
        var lines = File.ReadAllText(filename).AsSpan();

        var level = new List<int>();
        var result = 0;
        foreach (var lineRange in lines.Split('\n'))
        {
            level.Clear();
            foreach (var valRange in lines[lineRange].Split(' '))
            {
                level.Add(int.Parse(lines[lineRange][valRange]));
            }
            
            if (IsSafe(level))
            {
                result++;
                continue;
            }
            
            for (var toRemove = 0; toRemove < level.Count; toRemove++)
            {
                if (IsSafe(level, toRemove))  
                {
                    result++;
                    break;
                }
            }
        }
        

        return result;
    }
    
    private bool IsSafe(List<int> levels)
    {
        var increasing = false;

        for (var i = 1; i < levels.Count; i++)
        {
            var diff = levels[i] - levels[i-1];
            if (Math.Abs(diff) > 3) return false;
            if (diff==0) return false;
            if (i == 1)
                increasing = diff > 0;
            else if (increasing && diff < 0)
                return false;
            else if (!increasing && diff > 0)
                return false;
        }

        return true;
    }
    
    
    private bool IsSafe(List<int> levels, int skip)
    {
        bool? increasing = null;

        for (var i = 1; i < levels.Count; i++)
        {
            if (i == skip) continue;
            if (i == 1 && skip == 0) continue;
            
            var diff = skip == i-1 ? levels[i] - levels[i-2] : levels[i] - levels[i-1];
            if (diff==0) return false;
            if (Math.Abs(diff) > 3) return false;
            
            if (increasing is null)
                increasing = diff > 0;
            else if (increasing.Value && diff < 0)
                return false;
            else if (!increasing.Value && diff > 0)
                return false;
        }

        return true;
    }
}