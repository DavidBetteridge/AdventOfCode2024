namespace AdventOfCode2024.Tests.Day07;

public class Day07Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Solutions.Day07();
        Assert.Equal(3749, solver.Part1("Day07/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day07();
        Assert.Equal(1620690235709, solver.Part1("Day07/input.txt")); 
    }
    
    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Solutions.Day07();
        Assert.Equal(11387, solver.Part2("Day07/sample.txt"));
    }
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Solutions.Day07();
        Assert.Equal(145397611075341, solver.Part2("Day07/input.txt"));
    }

 }