namespace AdventOfCode2024.Tests.Day06;

public class Day06Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Solutions.Day06();
        Assert.Equal(41, solver.Part1("Day06/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day06();
        Assert.Equal(5095, solver.Part1("Day06/input.txt")); 
    }
   
    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Solutions.Day06();
        Assert.Equal(6, solver.Part2("Day06/sample.txt"));
    }
    
    [Fact]
    public void Test_Part2()
    {
        var solver = new Solutions.Day06();
        Assert.Equal(1933, solver.Part2("Day06/input.txt"));
    }

 }