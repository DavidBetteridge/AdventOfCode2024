using System.Collections.Concurrent;
using System.Reflection;

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

            if (Solve(testValue, inputs[0], inputs[1..]))
                totals.Add(testValue);
        });

        return totals.Sum();
        
        bool Solve(long testValue, long valueSoFar, Span<int> remainingValues)
        {
            if (remainingValues.Length == 0 && valueSoFar == testValue) return true;
            if (remainingValues.Length == 0) return false;
            if (valueSoFar > testValue) return false;
        
            if (Solve(testValue, valueSoFar + remainingValues[0], remainingValues[1..]))
                return true;
        
            if (Solve(testValue, valueSoFar * remainingValues[0], remainingValues[1..]))
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

            if (Solve(testValue, inputs[0], inputs[1..]))
                totals.Add(testValue);
        });

        return totals.Sum();
        
        bool Solve(long testValue, long valueSoFar, Span<int> remainingValues)
        {
            if (remainingValues.Length == 0 && valueSoFar == testValue) return true;
            if (remainingValues.Length == 0) return false;
            if (valueSoFar > testValue) return false;
        
            if (Solve(testValue, valueSoFar + remainingValues[0], remainingValues[1..]))
                return true;
        
            if (Solve(testValue, valueSoFar * remainingValues[0], remainingValues[1..]))
                return true;

            if (Solve(testValue, (valueSoFar * (long)Math.Pow(10, 1+(int)Math.Log10(remainingValues[0]))) + remainingValues[0], remainingValues[1..]))
                return true;
            
            return false;
        }
    }
}