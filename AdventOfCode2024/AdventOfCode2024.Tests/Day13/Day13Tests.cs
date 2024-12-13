namespace AdventOfCode2024.Tests.Day13;

public class Day13Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Solutions.Day13();
        Assert.Equal(480, solver.Part1("Day13/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day13();
        Assert.Equal(28138, solver.Part1("Day13/input.txt"));
    }
 }