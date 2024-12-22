namespace AdventOfCode2024.Tests;

public class Day22Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Solutions.Day22();
        Assert.Equal(37327623, solver.Part1("Day22/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day22();
        Assert.Equal(17965282217, solver.Part1("Day22/input.txt"));
    }

}