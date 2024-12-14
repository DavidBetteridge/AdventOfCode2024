namespace AdventOfCode2024.Tests.Day14;

public class Day14Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Solutions.Day14();
        Assert.Equal(12, solver.Part1("Day14/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day14();
        Assert.Equal(222208000, solver.Part1("Day14/input.txt"));
    }
}