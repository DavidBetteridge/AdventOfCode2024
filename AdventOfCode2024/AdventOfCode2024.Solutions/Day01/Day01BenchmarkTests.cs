using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024.Solutions;

[MemoryDiagnoser(true)]
public class Day01BenchmarkTests
{
    [Benchmark]
    public void Day01_Part1()
    {
        var solver = new Day01();
        solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024.Tests/Day01/input.txt");
    }

}