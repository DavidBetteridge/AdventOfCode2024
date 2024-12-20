namespace AdventOfCode2024.Tests;

public class Day20Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Solutions.Day20();
        Assert.Equal(44, solver.Part1("Day20/sample.txt", 1));
    }
    
    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day20();
        Assert.Equal(1507, solver.Part1("Day20/input.txt", 100));  // 944 too low
    }
}