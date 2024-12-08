using System.Collections.Concurrent;

namespace AdventOfCode2024.Solutions;

public class Day07
{
    public long Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var totals = new ConcurrentBag<long>();
        Parallel.ForEach(lines, line =>
        {
            var parts1 = line.Split(':');
            var testValue = long.Parse(parts1[0]);
            var inputs = parts1[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .ToArray()
                .AsSpan();

            if (Solve(testValue, inputs))
                totals.Add(testValue);
        });

        return totals.Sum();
        
        bool Solve(long targetValue, Span<int> remainingValues)
        {
            if (remainingValues.Length == 1 && targetValue == remainingValues[0]) return true;
            if (remainingValues.Length == 1) return false;
            if (targetValue < remainingValues[0]) return false;

            if (targetValue % remainingValues[^1] == 0)
            {
                //It was a *
                if (Solve(targetValue / remainingValues[^1], remainingValues[..^1]))
                    return true;
            }

            if (Solve(targetValue - remainingValues[^1], remainingValues[..^1]))
                return true;
            
            return false;
        }
    }
    
    public long Part2(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var totals = new ConcurrentBag<long>();
        Parallel.ForEach(lines, line =>
        {
            var parts1 = line.Split(':');
            var testValue = long.Parse(parts1[0]);
            var inputs = parts1[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .ToArray()
                .AsSpan();

            if (Solve(testValue, inputs))
                totals.Add(testValue);
        });

        return totals.Sum();
        
        bool Solve(long targetValue, Span<int> remainingValues)
        {
            if (remainingValues.Length == 1 && targetValue == remainingValues[0]) return true;
            if (remainingValues.Length == 1) return false;
            if (targetValue < remainingValues[0]) return false;

            if (targetValue % remainingValues[^1] == 0)
            {
                //It was a *
                if (Solve(targetValue / remainingValues[^1], remainingValues[..^1]))
                    return true;
            }

            if (Solve(targetValue - remainingValues[^1], remainingValues[..^1]))
                return true;

            // If testValue ends with remainingValues[^1] then /
            var mag = (long)Math.Pow(10, 1 + (int)Math.Log10(remainingValues[^1]));
            if (targetValue % mag == remainingValues[^1])
            {
                if (Solve(targetValue / mag, remainingValues[..^1]))
                    return true;
            }

            return false;
        }
            
    }
}