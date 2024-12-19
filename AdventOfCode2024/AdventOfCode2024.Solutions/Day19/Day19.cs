namespace AdventOfCode2024.Solutions;

public class Day19
{
    public int Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var towels = lines[0].Split(", ");

        var count = 0;
        foreach (var pattern in lines[2..])
        {
            if (IsValid(pattern))
                count++;
        }
        
        return count;
        
        bool IsValid(ReadOnlySpan<char> patternToCheck)
        {
            if (patternToCheck.Length == 0) return true;

            foreach (var towel in towels)
            {
                if (patternToCheck.StartsWith(towel))
                {
                    if (IsValid(patternToCheck[(towel.Length)..]))
                        return true;
                }
            }

            return false;
        }
    }
    
    public long Part2(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var towels = lines[0].Split(", ");

        var total = 0L;
        var cache = new Dictionary<string, long>();
        var lookup = cache.GetAlternateLookup<ReadOnlySpan<char>>();
        
        foreach (var pattern in lines[2..])
        {
            total+=ValidCombinations(pattern);
        }

        return total;
        
        long ValidCombinations(ReadOnlySpan<char> patternToCheck)
        {
            if (patternToCheck.Length == 0) return 1;
            
            if (lookup.TryGetValue(patternToCheck, out var combinations))
                return combinations;
            
            for (var towelNumber = 0; towelNumber < towels.Length; towelNumber++)
            {
                if (patternToCheck.StartsWith(towels[towelNumber]))
                {
                    var left = patternToCheck[(towels[towelNumber].Length)..];
                    combinations += ValidCombinations(left);
                }
            }

            lookup[patternToCheck] = combinations;
            return combinations;
        }
    }
}