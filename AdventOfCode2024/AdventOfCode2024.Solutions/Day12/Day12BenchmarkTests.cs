using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace AdventOfCode2024.Solutions;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory), CategoriesColumn]
[MemoryDiagnoser(true)]
[MarkdownExporterAttribute.GitHub]
public class Day12BenchmarkTests
{
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Part1")]
    public void Day12_Part1()
    {
        var solver = new Day12();
        var answer = solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day12/input.txt");
        if (answer != 1533644) throw new Exception("Wrong answer");
    }
    
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Part2")]
    public void Day12_Part2()
    {
        var solver = new Day12();
        var answer = solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day12/input.txt");
        if (answer != 936718) throw new Exception("Wrong answer");
    }
 }