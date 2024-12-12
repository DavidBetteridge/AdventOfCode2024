namespace AdventOfCode2024.Tests.Day09;

public class Day09Tests
{
    [Fact]
    public void Test_Part1_Sample_1()
    {
        var solver = new Solutions.Day09();
        Assert.Equal(1928, solver.Part1("Day09/sample.txt"));
    }

    [Fact]
    public void Test_Part1_Sample_12345()
    {
        var solver = new Solutions.Day09();
        Assert.Equal(60, solver.Part1("Day09/sample_12345.txt"));
    }
    
    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day09();
        Assert.Equal(6341711060162, solver.Part1("Day09/input.txt")); 
    }
    
    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Solutions.Day09();
        Assert.Equal(2858, solver.Part2("Day09/sample.txt"));
    }
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Solutions.Day09();
        Assert.Equal(6377400869326, solver.Part2("Day09/input.txt"));
    }
 }