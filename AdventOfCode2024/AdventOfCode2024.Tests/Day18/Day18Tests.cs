namespace AdventOfCode2024.Tests.Day18;

public class Day18Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Solutions.Day18();
        Assert.Equal(22, solver.Part1("Day18/sample.txt", 7, 12));
    }
    
    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day18();
        Assert.Equal(322, solver.Part1("Day18/input.txt", 71, 1024));
    }
    
}