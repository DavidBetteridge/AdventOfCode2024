using AdventOfCode2024.Solutions;
using BenchmarkDotNet.Running;
// BenchmarkRunner.Run<Day05BenchmarkTests>();


class Program
{
    static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
}
