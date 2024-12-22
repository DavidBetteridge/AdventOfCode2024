namespace AdventOfCode2024.Tests;

public class Day22Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Solutions.Day22();
        Assert.Equal(37327623, solver.Part1("Day22/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day22();
        Assert.Equal(17965282217, solver.Part1("Day22/input.txt"));
    }

    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Solutions.Day22_Part2();
        Assert.Equal(23, solver.Part2("Day22/sample2.txt", 2000));
    }
    
    [Fact]
    public void Test_Part2_Sample123()
    {
        var solver = new Solutions.Day22_Part2();
        Assert.Equal(6, solver.Part2("Day22/sample123.txt", 10));
    }
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Solutions.Day22_Part2();
        Assert.Equal(2152, solver.Part2("Day22/input.txt", 2000));
    }
}