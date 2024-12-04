namespace AdventOfCode2024.Tests.Day04;

public class Day04Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Solutions.Day04();
        Assert.Equal(18, solver.Part1("Day04/sample_part1.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day04();
        Assert.Equal(2618, solver.Part1("Day04/input.txt"));
    }
  
    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Solutions.Day04();
        Assert.Equal(9, solver.Part2("Day04/sample_part2.txt"));
    }

    [Fact]
    public void Test_Part2()
    {
        var solver = new Solutions.Day04();
        Assert.Equal(3046, solver.Part2("Day04/input.txt"));  //3046
    }
}