namespace AdventOfCode2024.Tests.Day05;

public class Day05Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Solutions.Day05();
        Assert.Equal(143, solver.Part1("Day05/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day05();
        Assert.Equal(6951, solver.Part1("Day05/input.txt"));
    }
    
    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Solutions.Day05();
        Assert.Equal(123, solver.Part2("Day05/sample.txt"));
    }
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Solutions.Day05();
        Assert.Equal(4121, solver.Part2("Day05/input.txt"));
    }
}