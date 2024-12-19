using System.Text;

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
    
    public long Part2(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var towels = lines[0].Split(", ");
        var towelsUsed = new int[towels.Length];

        var total = 0L;
        var possibleArrangements = new HashSet<string>();
        foreach (var pattern in lines[2..])
        {
            possibleArrangements.Clear();
            ValidCombinations(pattern);
            total+=possibleArrangements.Count;
        }

        return total;
        
        void ValidCombinations(string patternToCheck)
        {
            if (patternToCheck == "")
            {
                // We have constructed the pattern from the towels listed in towelsUsed
                var sb = new StringBuilder();
                for (var towelNumber = 0; towelNumber < towels.Length; towelNumber++)
                    sb.Append(towelsUsed[towelNumber] + ',');
                possibleArrangements.Add(sb.ToString());
            }
            
            for (var towelNumber = 0; towelNumber < towels.Length; towelNumber++)
            {
                if (patternToCheck.StartsWith(towels[towelNumber]))
                {
                    towelsUsed[towelNumber]++;
                    ValidCombinations(patternToCheck[(towels[towelNumber].Length)..]);
                    towelsUsed[towelNumber]--;
                }  
            }
       
        }
    }
}