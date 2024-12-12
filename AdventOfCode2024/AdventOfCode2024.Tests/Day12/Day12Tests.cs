namespace AdventOfCode2024.Tests.Day11;

public class Day12Tests
{
    [Fact]
    public void Test_Part1_Sample_1()
    {
        var solver = new Solutions.Day12();
        Assert.Equal(1930, solver.Part1("Day12/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day12();
        Assert.Equal(1533644, solver.Part1("Day12/input.txt"));
    }

 }