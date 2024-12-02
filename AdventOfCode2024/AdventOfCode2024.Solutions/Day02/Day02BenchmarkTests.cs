using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024.Solutions;

[MemoryDiagnoser(true)]
public class Day02BenchmarkTests
{
    [Benchmark(Baseline = true)]
    public void Day02_Part1()
    {
        var solver = new Day02();
        var answer = solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day02/input.txt");
        if (answer != 334) throw new Exception("Wrong answer");
    }
}