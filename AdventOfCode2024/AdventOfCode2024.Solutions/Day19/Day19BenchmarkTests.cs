using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace AdventOfCode2024.Solutions;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory), CategoriesColumn]
[MemoryDiagnoser(true)]
[MarkdownExporterAttribute.GitHub]
public class Day19BenchmarkTests
{
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Part1")]
    public void Day19_Part1()
    {
        var solver = new Day19();
        var answer = solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day19/input.txt");
        if (answer != 333) throw new Exception("Wrong answer");
    }
    
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Part2")]
    public void Day19_Part2()
    {
        var solver = new Day19();
        var answer = solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day19/input.txt");
        if (answer != 678536865274732) throw new Exception("Wrong answer");
    }
 }