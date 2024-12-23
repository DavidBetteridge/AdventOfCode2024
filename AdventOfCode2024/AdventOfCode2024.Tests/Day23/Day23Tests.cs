namespace AdventOfCode2024.Tests;

public class Day23Tests
{
    [Fact]
    public void Test_Part1_Sample()
    {
        var solver = new Solutions.Day23();
        Assert.Equal(7, solver.Part1("Day23/sample.txt"));
    }

    [Fact]
    public void Test_Part1()
    {
        var solver = new Solutions.Day23();
        Assert.Equal(1108, solver.Part1("Day23/input.txt"));
    }

    [Fact]
    public void Test_Part2_Sample()
    {
        var solver = new Solutions.Day23_Part2();
        Assert.Equal("co,de,ka,ta", solver.Part2("Day23/sample.txt"));
    }

    [Fact]
    public void Test_Part2()
    {
        var solver = new Solutions.Day23_Part2();
        Assert.Equal("ab,cp,ep,fj,fl,ij,in,ng,pl,qr,rx,va,vf", solver.Part2("Day23/input.txt")); 
    }
    
    
}