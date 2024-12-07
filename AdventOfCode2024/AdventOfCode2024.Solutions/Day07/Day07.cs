namespace AdventOfCode2024.Solutions;

public class Day07
{
    public ulong Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);
        ulong total = 0;

        foreach (var line in lines)
        {
            var parts1 = line.Split(':');
            var testValue = ulong.Parse(parts1[0]);
            var inputs = parts1[1]
                            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                            .Select(int.Parse)
                            .ToList();

            if (Solve(testValue, (ulong)inputs[0], inputs[1..]))
                total += testValue;

        }
        

        return total;
    }

    private bool Solve(ulong testValue, ulong valueSoFar, List<int> remainingValues)
    {
        if (remainingValues.Count == 0 && valueSoFar == testValue) return true;
        if (remainingValues.Count == 0) return false;
        if (valueSoFar > testValue) return false;
        
        if (Solve(testValue, valueSoFar + (ulong)remainingValues[0], remainingValues[1..]))
            return true;
        
        if (Solve(testValue, valueSoFar * (ulong)remainingValues[0], remainingValues[1..]))
            return true;

        return false;
    }
}