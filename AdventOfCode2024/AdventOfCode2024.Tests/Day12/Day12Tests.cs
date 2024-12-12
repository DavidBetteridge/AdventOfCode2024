namespace AdventOfCode2024.Tests.Day12;

public class Day12Tests
{
    [Fact]
    public void Test_Part1_Sample()
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

    [Fact]
    public void Test_Part2()
    {
        var solver = new Solutions.Day12();
        Assert.Equal(936718, solver.Part2("Day12/input.txt"));
    }
    
    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Solutions.Day12();
        Assert.Equal(1206, solver.Part2("Day12/sample.txt"));
    }
    
    [Fact]
    public void Test_Part2_SampleE()
    {
        var solver = new Solutions.Day12();
        Assert.Equal(236, solver.Part2("Day12/sample_e.txt"));
    }
    
    [Fact]
    public void Test_Part2_Sample80()
    {
        var solver = new Solutions.Day12();
        Assert.Equal(80, solver.Part2("Day12/sample_80.txt"));
    }
    
    [Fact]
    public void Test_Part2_Sample368()
    {
        var solver = new Solutions.Day12();
        Assert.Equal(368, solver.Part2("Day12/sample_368.txt"));
    }
 }