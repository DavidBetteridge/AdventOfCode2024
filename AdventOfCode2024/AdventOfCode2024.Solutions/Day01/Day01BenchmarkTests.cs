using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024.Solutions;

[MemoryDiagnoser(true)]
public class Day01BenchmarkTests
{
    [Benchmark(Baseline = true)]
    public void Day01_Part1()
    {
        var solver = new Day01();
        var answer = solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day01/input.txt");
        if (answer != 1189304) throw new Exception("Wrong answer");
    }

    [Benchmark(Baseline = false)]
    public void Day01_Part1_NoVec()
    {
        var solver = new Day01();
        var answer = solver.Part1_NoVec("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day01/input.txt");
        if (answer != 1189304) throw new Exception("Wrong answer");
    }
    
    [Benchmark]
    public void Day01_Part2()
    {
        var solver = new Day01();
        var answer = solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day01/input.txt");
        if (answer != 24349736) throw new Exception("Wrong answer");
    }
}