namespace AdventOfCode2024.Solutions;

public class Day02
{
    public int Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return lines.Count(l => IsSafe(l.Split(' ').Select(int.Parse).ToList()));
    
    }

    public int Part2(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return lines.Count(IsSafeWithTolerance);
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
    
    private bool IsSafeWithTolerance(string line)
    {
        var level = line.Split(' ').Select(int.Parse).ToList();
        if (IsSafe(level)) return true;

        for (var toRemove = 0; toRemove < level.Count; toRemove++)
        {
            if (IsSafe(level, toRemove)) return true;
        }

        return false;
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