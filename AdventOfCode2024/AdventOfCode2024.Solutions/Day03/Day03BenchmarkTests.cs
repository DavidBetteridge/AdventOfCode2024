using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024.Solutions;

[MemoryDiagnoser(true)]
public class Day03BenchmarkTests
{
    // [Benchmark(Baseline = true)]
    // public void Day03_Part1()
    // {
    //     var solver = new Day03();
    //     var answer = solver.Part1("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day03/input.txt");
    //     if (answer != 179834255) throw new Exception("Wrong answer");
    // }

    
    [Benchmark(Baseline = true)]
    public void Day03_Part2()
    {
        var solver = new Day03();
        var answer = solver.Part2("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day03/input.txt");
        if (answer != 80570939) throw new Exception("Wrong answer");
    }

    [Benchmark(Baseline = false)]
    public void Day03_Part2b()
    {
        var solver = new Day03();
        var answer = solver.Part2b("/Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Tests/Day03/input.txt");
        if (answer != 80570939) throw new Exception("Wrong answer");
    }
    
}