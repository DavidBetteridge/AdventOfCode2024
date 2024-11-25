namespace AdventOfCode2024.Solutions;

public class Day01
{
    public int Part1(string filename)
    {
        return File.ReadAllText(filename).Length;
    }
}