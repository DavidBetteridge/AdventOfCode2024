using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace AdventOfCode2024.Solutions;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory), CategoriesColumn]
[MemoryDiagnoser(true)]
[MarkdownExporterAttribute.GitHub]
public class Day25BenchmarkTests
{
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Part1")]
    public void Day25_Part1()
    {
        var solver = new Day25();
        var answer = solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day25/input.txt");
        if (answer != 3525) throw new Exception("Wrong answer");
    }
  }