using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024.Solutions;

[MemoryDiagnoser(true)]
public class Day04BenchmarkTests
{
    [Benchmark(Baseline = true)]
    public void Day04_Part1()
    {
        var solver = new Day04();
        var answer = solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day04/input.txt");
        if (answer != 2618) throw new Exception("Wrong answer");
    }

    [Benchmark(Baseline = false)]
    public void Day04_Part2()
    {
        var solver = new Day04();
        var answer = solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day04/input.txt");
        if (answer != 2011) throw new Exception("Wrong answer");
    }
    
}