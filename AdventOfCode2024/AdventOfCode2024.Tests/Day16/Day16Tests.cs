namespace AdventOfCode2024.Tests.Day16;

public class Day16Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Solutions.Day16();
        Assert.Equal(7036, solver.Part1("Day16/sample.txt"));
    }
    
    [Fact]
    public void Test_Part1_Sample2()
    {
        var solver = new Solutions.Day16();
        Assert.Equal(11048, solver.Part1("Day16/sample2.txt"));
    }
    
    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day16();
        Assert.Equal(90460, solver.Part1("Day16/input.txt"));
    }
    
    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Solutions.Day16();
        Assert.Equal(45, solver.Part2("Day16/sample.txt"));
    }
    
    [Fact]
    public void Test_Part2_Sample2()
    {
        var solver = new Solutions.Day16();
        Assert.Equal(64, solver.Part2("Day16/sample2.txt"));
    }
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Solutions.Day16();
        Assert.Equal(575, solver.Part2("Day16/input.txt"));
    }
}