## 2024 Results

|                     Day                      | Part 1 | Part 2 |
|:--------------------------------------------:| :---: | :---: |
| [Day 1](https://adventofcode.com/2024/day/1) | ⭐ | ⭐ |
| [Day 2](https://adventofcode.com/2024/day/1) | ⭐ | ⭐ |
| [Day 3](https://adventofcode.com/2024/day/1) | ⭐ | ⭐ |
| [Day 4](https://adventofcode.com/2024/day/1) | ⭐ | ⭐ |
| [Day 5](https://adventofcode.com/2024/day/1) | ⭐ | ⭐ |


| Method      | Categories | Mean     | Error   | StdDev  | Ratio | Gen0    | Gen1   | Allocated | Alloc Ratio |
|------------ |----------- |---------:|--------:|--------:|------:|--------:|-------:|----------:|------------:|
| Day01_Part1           | Part1      | 69.57 us | 1.323 us | 2.137 us |  1.00 |    0.04 | 13.4277 | 0.6104 | 110.76 KB |        1.00 |
| Day01_Part1_NoVectors | Part1      | 79.77 us | 1.545 us | 1.206 us |  1.15 |    0.04 | 10.1318 | 0.1221 |  83.56 KB |        0.75 |
|                       |            |          |          |          |       |         |         |        |           |             |
| Day01_Part2           | Part2      | 80.22 us | 0.455 us | 0.403 us |  1.00 |    0.01 | 13.3057 | 1.3428 | 108.96 KB |        1.00 |
|                       |            |          |          |          |       |         |         |        |           |             |
| Day02_Part1 | Part1      |  68.86 us | 0.542 us | 0.480 us |  1.00 | 11.3525 | 0.1221 |  93.13 KB |        1.00 |
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


### Run benchmarks

``` bash
cd /Users/davidbetteridge/Personal/AdventOfCode2024/AdventOfCode2024/AdventOfCode2024.Solutions
dotnet run -c Release
```


