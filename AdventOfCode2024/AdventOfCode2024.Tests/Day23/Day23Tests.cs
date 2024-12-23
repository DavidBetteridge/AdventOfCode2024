namespace AdventOfCode2024.Tests;

public class Day23Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Solutions.Day23();
        Assert.Equal(7, solver.Part1("Day23/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day23();
        Assert.Equal(1108, solver.Part1("Day23/input.txt"));  // 1982 too high,  1097 too low
    }

}