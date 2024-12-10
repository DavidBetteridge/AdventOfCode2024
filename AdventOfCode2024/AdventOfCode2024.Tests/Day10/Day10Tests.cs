namespace AdventOfCode2024.Tests.Day10;

public class Day10Tests
{
    [Fact]
    public void Test_Part1_Sample_1()
    {
        var solver = new Solutions.Day10();
        Assert.Equal(36, solver.Part1("Day10/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day10();
        Assert.Equal(489, solver.Part1("Day10/input.txt")); 
    }
    
    // [Fact]
    // public void Test_Part2_Sample()
    // {
    //     var solver = new Solutions.Day10();
    //     Assert.Equal(2858, solver.Part2("Day10/sample.txt"));
    // }
    //
    // [Fact]
    // public void Test_Part2()
    // {
    //     var solver = new Solutions.Day10();
    //     Assert.Equal(6377400869326, solver.Part2("Day10/input.txt"));
    // }

 }