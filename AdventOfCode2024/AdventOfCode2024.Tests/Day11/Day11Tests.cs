namespace AdventOfCode2024.Tests.Day11;

public class Day11Tests
{
    [Fact]
    public void Test_Part1_Sample_1()
    {
        var solver = new Solutions.Day11();
        Assert.Equal(55312, solver.Part1("Day11/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day11();
        Assert.Equal(203953, solver.Part1("Day11/input.txt"));
    }
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Solutions.Day11();
        Assert.Equal(242090118578155, solver.Part2("Day11/input.txt"));
    }
    
    // [Fact]
    // public void Test_Part2B()
    // {
    //     var solver = new Solutions.Day11();
    //     Assert.Equal(242090118578155, solver.Part2B("Day11/input.txt"));
    // }
 }