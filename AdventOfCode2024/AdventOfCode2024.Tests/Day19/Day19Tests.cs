namespace AdventOfCode2024.Tests.Day19;

public class Day19Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Solutions.Day19();
        Assert.Equal(6, solver.Part1("Day19/sample.txt"));
    }
    
    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day19();
        Assert.Equal(333, solver.Part1("Day19/input.txt"));
    }
    
    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Solutions.Day19();
        Assert.Equal(16, solver.Part2("Day19/sample.txt"));
    }
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Solutions.Day19();
        Assert.Equal(0, solver.Part2("Day19/input.txt"));
    }
}