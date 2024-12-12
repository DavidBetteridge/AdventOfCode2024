
namespace AdventOfCode2024.Solutions;

public class Day11
{
    private long[] Divisors =
    {
        10,
        100,
        1000,
        10000,
        100000,
        1000000,
        10000000,
        100000000,
        1000000000,
        10000000000,
        100000000000,
        1000000000000,
        10000000000000,
    };
    
    public long Part1(string filename)
    {
        var stones = File.ReadAllText(filename).Split(' ').Select(long.Parse).ToArray();
        var cache = new Dictionary<long, long[]>();
        return stones.Select(stone => Blink(25, stone, cache, 26)).Sum();
    }

    public long Part2(string filename)
    {
        var stones = File.ReadAllText(filename).Split(' ').Select(long.Parse).ToArray();
        var cache = new Dictionary<long, long[]>();
        return stones.Select(stone => Blink(75, stone, cache, 76)).Sum();
    }

    private long Blink(int timesToBlink, long stone, Dictionary<long, long[]> cache, int cacheSize)
    {
        if (timesToBlink == 0) return 1;
        if (cache.TryGetValue(stone, out var x) && x[timesToBlink] != 0) return x[timesToBlink];

        long newStoneCount;
        if (stone == 0)
            newStoneCount = Blink(timesToBlink - 1, 1, cache, cacheSize);
        else
        {
            var f = (int)Math.Log10(stone);
            if (f % 2 == 1)
            {
                var e = Divisors[f / 2];
                newStoneCount = Blink(timesToBlink - 1, stone / e, cache, cacheSize) +
                                Blink(timesToBlink - 1, stone % e, cache, cacheSize);
            }
            else
                newStoneCount = Blink(timesToBlink - 1, stone * 2024, cache, cacheSize);
        }
        
        if (cache.TryGetValue(stone, out var y))
        {
            y[timesToBlink] = newStoneCount;
        }
        else
        {
            cache[stone] = new long[cacheSize];
            cache[stone][timesToBlink] = newStoneCount;
        }

        return newStoneCount;
    }
}