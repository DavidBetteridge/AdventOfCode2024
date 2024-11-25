namespace AdventOfCode2024.Tests.Day01;

public class Day01Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Solutions.Day01();
        Assert.Equal(5, solver.Part1("Day01/sample.txt"));
    }
}