namespace AdventOfCode2024.Tests;

public class Day21Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Solutions.Day21();
        Assert.Equal(126384, solver.Part1("Day21/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day21();
        Assert.Equal(171596, solver.Part1("Day21/input.txt"));
    }
}