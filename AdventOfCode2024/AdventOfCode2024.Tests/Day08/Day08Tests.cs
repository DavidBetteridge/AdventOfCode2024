namespace AdventOfCode2024.Tests.Day08;

public class Day08Tests
{
    [Fact]
    public void Test_Part1_Sample_1()
    {
        var solver = new Solutions.Day08();
        Assert.Equal(2, solver.Part1("Day08/sample_1.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day08();
        Assert.Equal(359, solver.Part1("Day08/input.txt")); 
    }
    
    // [Fact]
    // public void Test_Part2_Sample()
    // {
    //     var solver = new Solutions.Day08();
    //     Assert.Equal(11387, solver.Part2("Day08/sample.txt"));
    // }
    //
    // [Fact]
    // public void Test_Part2()
    // {
    //     var solver = new Solutions.Day08();
    //     Assert.Equal(145397611085341, solver.Part2("Day08/input.txt"));
    // }

 }