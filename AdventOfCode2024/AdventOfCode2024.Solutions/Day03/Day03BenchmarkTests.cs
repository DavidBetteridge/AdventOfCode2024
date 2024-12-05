using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace AdventOfCode2024.Solutions;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory), CategoriesColumn]
[MemoryDiagnoser(true)]
public class Day03BenchmarkTests
{
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Part1")]
    public void Day03_Part1_Regex()
    {
        var solver = new Day03();
        var answer = solver.Part1_Regex("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day03/input.txt");
        if (answer != 179834255) throw new Exception("Wrong answer");
    }
    
    [Benchmark(Baseline = false)]
    [BenchmarkCategory("Part1")]
    public void Day03_Part1_Fast()
    {
        var solver = new Day03();
        var answer = solver.Part1_Fast("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day03/input.txt");
        if (answer != 179834255) throw new Exception("Wrong answer");
    }
    
    
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Part2")]
    public void Day03_Part_Regex()
    {
        var solver = new Day03();
        var answer = solver.Part2_Regex("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day03/input.txt");
        if (answer != 80570939) throw new Exception("Wrong answer");
    }
    
    [Benchmark(Baseline = false)]
    [BenchmarkCategory("Part2")]
    public void Day03_Part2_Fast()
    {
        var solver = new Day03();
        var answer = solver.Part2_Fast("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day03/input.txt");
        if (answer != 80570939) throw new Exception("Wrong answer");
    }
        
    [Benchmark(Baseline = false)]
    [BenchmarkCategory("Part2")]
    public void Day03_Part2_StateMachine()
    {
        var solver = new Day03();
        var answer = solver.Part2_StateMachine("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day03/input.txt");
        if (answer != 80570939) throw new Exception("Wrong answer");
    }
    
}