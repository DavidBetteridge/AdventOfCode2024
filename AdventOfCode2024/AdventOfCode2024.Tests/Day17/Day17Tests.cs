namespace AdventOfCode2024.Tests.Day17;

public class Day17Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Solutions.Day17();
        Assert.Equal("4,6,3,5,6,3,5,2,1,0", solver.Part1("Day17/sample.txt"));
    }
    
    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day17();
        Assert.Equal("1,4,6,1,6,4,3,0,3", solver.Part1("Day17/input.txt"));
    }
   
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Solutions.Day17();
        Assert.Equal((ulong)265061364597659, solver.Part2());
    }

}