namespace AdventOfCode2024.Tests.Day01;

public class Day01Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Solutions.Day01();
        Assert.Equal(11, solver.Part1("Day01/sample.txt"));
    }
    
    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day01();
        Assert.Equal(1189304, solver.Part1("Day01/input.txt"));
    }
   
   
    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Solutions.Day01();
        Assert.Equal(31, solver.Part2("Day01/sample.txt"));
    }

    [Fact]
    public void Test_Part2()
    {
        var solver = new Solutions.Day01();
        Assert.Equal(24349736, solver.Part2("Day01/input.txt"));
    }
}