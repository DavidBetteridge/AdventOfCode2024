using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace AdventOfCode2024.Solutions;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory), CategoriesColumn]
[MemoryDiagnoser(true)]
[MarkdownExporterAttribute.GitHub]
public class Day23BenchmarkTests
{
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Part1")]
    public void Day23_Part1()
    {
        var solver = new Day23();
        var answer = solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day23/input.txt");
        if (answer != 1108) throw new Exception("Wrong answer");
    }
    
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Part2")]
    public void Day23_Part2()
    {
        var solver = new Day23_Part2();
        var answer = solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day23/input.txt");
        if (answer != "ab,cp,ep,fj,fl,ij,in,ng,pl,qr,rx,va,vf") throw new Exception("Wrong answer");
    }
 }