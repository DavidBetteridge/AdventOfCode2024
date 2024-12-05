using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace AdventOfCode2024.Solutions;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory), CategoriesColumn]
[MemoryDiagnoser(true)]
[MarkdownExporterAttribute.GitHub]
public class Day01BenchmarkTests
{
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Part1")]
    public void Day01_Part1()
    {
        var solver = new Day01();
        var answer = solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day01/input.txt");
        if (answer != 1189304) throw new Exception("Wrong answer");
    }
   
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Part2")]
    public void Day01_Part2()
    {
        var solver = new Day01();
        var answer = solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day01/input.txt");
        if (answer != 24349736) throw new Exception("Wrong answer");
    }
}