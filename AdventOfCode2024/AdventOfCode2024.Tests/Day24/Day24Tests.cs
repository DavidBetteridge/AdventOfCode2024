namespace AdventOfCode2024.Tests;

public class Day24Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Solutions.Day24();
        Assert.Equal((ulong)4, solver.Part1("Day24/sample.txt"));
    }

    [Fact]
    public void Test_Part1_Sample_Larger()
    {
        var solver = new Solutions.Day24();
        Assert.Equal((ulong)2024, solver.Part1("Day24/sample_large.txt"));
    }
    
    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day24();
        Assert.Equal((ulong)65635066541798, solver.Part1("Day24/input.txt"));
    }
   
    
}