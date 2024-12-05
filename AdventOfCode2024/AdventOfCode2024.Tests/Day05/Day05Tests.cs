namespace AdventOfCode2024.Tests.Day05;

public class Day04Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Solutions.Day05();
        Assert.Equal(143, solver.Part1("Day05/sample_part1.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day05();
        Assert.Equal(6951, solver.Part1("Day05/input.txt"));
    }
    //
    // [Fact]
    // public void Test_Part2_Sample()
    // {
    //     var solver = new Solutions.Day04();
    //     Assert.Equal(9, solver.Part2("Day04/sample_part2.txt"));
    // }
    //
    // [Fact]
    // public void Test_Part2()
    // {
    //     var solver = new Solutions.Day04();
    //     Assert.Equal(2011, solver.Part2("Day04/input.txt"));
    // }
}