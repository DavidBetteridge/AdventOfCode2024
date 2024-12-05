using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024.Solutions;

[MemoryDiagnoser(true)]
public class Day05BenchmarkTests
{
    [Benchmark(Baseline = true)]
    public void Day05_Part1()
    {
        var solver = new Day05();
        var answer = solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day05/input.txt");
        if (answer != 6951) throw new Exception("Wrong answer");
    }

    [Benchmark(Baseline = false)]
    public void Day05_Part2()
    {
        var solver = new Day05();
        var answer = solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day05/input.txt");
        if (answer != 4121) throw new Exception("Wrong answer");
    }
    
}