namespace AdventOfCode2024.Tests.Day02;

public class Day02Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Solutions.Day02();
        Assert.Equal(2, solver.Part1("Day02/sample.txt"));
    }
    
    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day02();
        Assert.Equal(334, solver.Part1("Day02/input.txt"));
    }
    
    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Solutions.Day02();
        Assert.Equal(4, solver.Part2("Day02/sample.txt"));
    }
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Solutions.Day02();
        Assert.Equal(400, solver.Part2("Day02/input.txt"));
    }
}