using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace AdventOfCode2024.Solutions;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory), CategoriesColumn]
[MemoryDiagnoser(true)]
[MarkdownExporterAttribute.GitHub]
public class Day17BenchmarkTests
{
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Part1")]
    public void Day17_Part1()
    {
        var solver = new Day17();
        var answer = solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day17/input.txt");
        if (answer != "1,4,6,1,6,4,3,0,3") throw new Exception("Wrong answer");
    }
    
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Part2")]
    public void Day17_Part2()
    {
        var solver = new Day17();
        var answer = solver.Part2();
        if (answer != 265061364597659) throw new Exception("Wrong answer");
    }
 }