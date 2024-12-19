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
        
        bool IsValid(string patternToCheck)
        {
            if (patternToCheck == "") return true;

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
}