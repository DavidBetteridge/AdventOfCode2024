namespace AdventOfCode2024.Tests;

public class Day25Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Solutions.Day25();
        Assert.Equal(3, solver.Part1("Day25/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day25();
        Assert.Equal(3525, solver.Part1("Day25/input.txt"));
    }
}