namespace AdventOfCode2024.Solutions;

public class Day01
{
    public int Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);

        var lhs = new List<int>();
        var rhs = new List<int>();

        foreach (var line in lines)
        {
            var parts = line.Split("   "); 
            lhs.Add(int.Parse(parts[0]));
            rhs.Add(int.Parse(parts[1]));
        }
        
        lhs.Sort();
        rhs.Sort();

        return lhs.Zip(rhs, (l, h) => Math.Abs(l - h)).Sum();
    }
    
    public int Part2(string filename)
    {
        var lines = File.ReadAllLines(filename);

        var lhs = new List<int>();
        var rhs = new List<int>();

        foreach (var line in lines)
        {
            var parts = line.Split("   "); 
            lhs.Add(int.Parse(parts[0]));
            rhs.Add(int.Parse(parts[1]));
        }
        
        return lhs.Sum(l => l * rhs.Count(r => r == l));
    }
}