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
            var smaller = level[..toRemove].Concat(level[(toRemove + 1)..]);
            if (IsSafe(smaller.ToList())) return true;
        }

        return false;
    }
}