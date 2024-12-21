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
    
    
    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Solutions.Day21_Part2();
        Assert.Equal(126384, solver.Part2("Day21/sample.txt", 2 + 1));
    }

    [Fact]
    public void Test_Part2()
    {
        var solver = new Solutions.Day21_Part2();
        Assert.Equal(209268004868246, solver.Part2("Day21/input.txt", 25 + 1));
    }
}