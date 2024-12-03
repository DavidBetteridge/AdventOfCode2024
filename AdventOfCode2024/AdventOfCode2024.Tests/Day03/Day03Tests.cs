namespace AdventOfCode2024.Tests.Day03;

public class Day03Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Solutions.Day03();
        Assert.Equal(161, solver.Part1("Day03/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day03();
        Assert.Equal(179834255, solver.Part1("Day03/input.txt"));
    }
    
}