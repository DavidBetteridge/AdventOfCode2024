namespace AdventOfCode2024.Tests.Day15;

public class Day15Tests
{
    [Fact]
    public void Test_Part1_SmallSample()
    {
        var solver = new Solutions.Day15();
        Assert.Equal(2028, solver.Part1("Day15/small_sample.txt"));
    }
    
    [Fact]
    public void Test_Part1_LargeSample()
    {
        var solver = new Solutions.Day15();
        Assert.Equal(10092, solver.Part1("Day15/large_sample.txt"));
    }
    
    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day15();
        Assert.Equal(10092, solver.Part1("Day15/input.txt"));
    }
    
    // [Fact]
    // public void Test_Part2()
    // {
    //     var solver = new Solutions.Day15();
    //     Assert.Equal(7623, solver.Part2("Day15/input.txt"));
    // }
}