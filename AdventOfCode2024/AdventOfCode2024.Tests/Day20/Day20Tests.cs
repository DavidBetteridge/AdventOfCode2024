namespace AdventOfCode2024.Tests;

public class Day20Tests
{
    [Fact]
    public async Task Test_Part1_Sample()
    {
        var solver = new Solutions.Day20_Part2();
        Assert.Equal(44,await solver.Part2("Day20/sample.txt", 1, 2));
    }
    
    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day20_Part1();
        Assert.Equal(1507, solver.Part1("Day20/input.txt", 100));
    }
    
    [Fact]
    public async Task Test_Part2_Sample()
    {
        var solver = new Solutions.Day20_Part2();
        Assert.Equal(41, await solver.Part2("Day20/sample.txt", 70, 20));
    }
    
    [Fact]
    public async Task Test_Part2()
    {
        var solver = new Solutions.Day20_Part2();
        Assert.Equal(1037936, await solver.Part2("Day20/input.txt", 100, 20));
    }
}