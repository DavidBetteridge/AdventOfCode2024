## 2024 Results

|                     Day                      | Part 1 | Part 2 |
|:--------------------------------------------:| :---: | :---: |
| [Day 1](https://adventofcode.com/2024/day/1) | ⭐ | ⭐ |
| [Day 2](https://adventofcode.com/2024/day/1) | ⭐ | ⭐ |
| [Day 3](https://adventofcode.com/2024/day/1) | ⭐ | ⭐ |
| [Day 4](https://adventofcode.com/2024/day/1) | ⭐ | ⭐ |
| [Day 5](https://adventofcode.com/2024/day/1) | ⭐ | ⭐ |
| [Day 6](https://adventofcode.com/2024/day/1) | ⭐ | ⭐ |
| [Day 7](https://adventofcode.com/2024/day/1) | ⭐ | ⭐ |

| Method      | Categories | Mean     | Error   | StdDev  | Ratio | Gen0    | Gen1   | Allocated | Alloc Ratio |
|------------ |----------- |---------:|--------:|--------:|------:|--------:|-------:|----------:|------------:|
| Day01_Part1 | Part1      | 27.17 us | 0.356 us | 0.333 us |  1.00 |    0.02 |  3.6621 | 0.1221 |  30.24 KB |        1.00 |
|                       |            |          |          |          |       |         |         |        |           |             |
| Day01_Part2 | Part2      | 34.84 us | 0.522 us | 0.488 us |  1.00 |    0.02 | 6.7749 | 0.7935 |  55.82 KB |        1.00 |
|                       |            |          |          |          |       |         |         |        |           |             |
| Day02_Part1 | Part1      |  22.81 us | 0.163 us | 0.136 us |  1.00 |  2.3193 |      - |  19.06 KB |        1.00 |
|             |            |           |          |          |       |         |        |           |             |
| Day02_Part2 | Part2      | 112.00 us | 0.344 us | 0.305 us |  1.00 | 11.3525 | 0.1221 |  93.13 KB |        1.00 |
|             |            |           |          |          |       |         |        |           |             |
| Day03_Part1_Regex        | Part1      | 130.82 us | 0.253 us | 0.224 us |  1.00 | 61.5234 | 23.9258 | 502.72 KB |        1.00 |
| Day03_Part1_Fast         | Part1      |  15.97 us | 0.136 us | 0.121 us |  0.12 |  2.3499 |       - |  19.37 KB |        0.04 |
|                          |            |           |          |          |       |         |         |           |             |
| Day03_Part_Regex         | Part2      | 125.80 us | 0.412 us | 0.322 us |  1.00 | 58.1055 | 19.2871 | 476.02 KB |        1.00 |
| Day03_Part2_Fast         | Part2      |  18.16 us | 0.287 us | 0.268 us |  0.14 |  2.3499 |       - |  19.37 KB |        0.04 |
| Day03_Part2_StateMachine | Part2      |  54.66 us | 0.412 us | 0.385 us |  0.43 | 11.4136 |  1.8311 |  93.75 KB |        0.20 |
|                          |            |           |          |          |       |         |         |           |             |
| Day04_Part1 | Part1      | 225.5 us | 4.01 us | 3.75 us |  1.00 |    0.02 | 6.5918 | 0.7324 |  55.35 KB |        1.00 |
| Day04_Part2 | Part2      | 127.4 us | 2.48 us | 2.95 us |  0.57 |    0.02 | 6.5918 | 0.9766 |  54.97 KB |        0.99 |
|                          |            |           |          |          |       |         |         |           |             |
| Day05_Part1 | Part1      | 258.0 us | 1.60 us | 1.42 us |  1.00 | 11.2305 | 0.4883 |  94.96 KB |        1.00 |
| Day05_Part2 | Part2      | 279.6 us | 1.54 us | 1.37 us |  1.00 | 10.7422 |      - |  93.59 KB |        1.00 |
|                          |            |           |          |          |       |         |         |           |             |
| Day06_Part1 | Part1      |      37.69 us |     0.319 us |     0.283 us |  1.00 | 6.1035 |  50.06 KB |        1.00 |
|             |            |              |             |             |       |         |         |         |         |           |             |
| Day06_Part2 | Part2      | 24,380.84 us | 42.090 us | 39.371 us |  1.00 | 31.2500 | 261.57 KB |        1.00 |
|             |            |              |             |             |       |         |         |         |         |           |             |
| Day07_Part1 | Part1      | 198.7 us | 18.52 us | 51.32 us | 179.5 us |  1.05 |    0.35 | 74.2188 | 23.4375 |      - | 629.11 KB |        1.00 |
|             |            |          |          |          |          |       |         |         |         |        |           |             |
| Day07_Part2 | Part2      | 186.8 us |  3.72 us |  3.29 us | 186.0 us |  1.00 |    0.02 | 77.6367 | 25.8789 | 1.4648 | 632.32 KB |        1.00 |


### Run benchmarks

``` bash
cd /Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Solutions
dotnet run -c Release
dotnet run -c Release --filter '*Day01BenchmarkTests*'
```


