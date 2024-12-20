using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace AdventOfCode2024.Solutions;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory), CategoriesColumn]
[MemoryDiagnoser(true)]
[MarkdownExporterAttribute.GitHub]
public class Day20BenchmarkTests
{
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Part1")]
    public async Task Day20_Part1()
    {
        var solver = new Day20_Part2();
        var answer = await solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day20/input.txt", 100, 2);
        if (answer != 1507) throw new Exception("Wrong answer");
    }
    
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Part2")]
    public async Task Day20_Part2()
    {
        var solver = new Day20_Part2();
        var answer = await solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day20/input.txt", 100, 20);
        if (answer != 1037936) throw new Exception("Wrong answer");
    }
 }