using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace AdventOfCode2024.Solutions;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory), CategoriesColumn]
[MemoryDiagnoser(true)]
[MarkdownExporterAttribute.GitHub]
public class Day02BenchmarkTests
{
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Part1")]
    public void Day02_Part1()
    {
        var solver = new Day02();
        var answer = solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day02/input.txt");
        if (answer != 334) throw new Exception("Wrong answer");
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Part2")]
    public void Day02_Part2()
    {
        var solver = new Day02();
        var answer = solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day02/input.txt");
        if (answer != 400) throw new Exception("Wrong answer");
    }
    
}