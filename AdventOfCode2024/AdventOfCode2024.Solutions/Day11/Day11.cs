using System.Collections.Concurrent;

namespace AdventOfCode2024.Solutions;

public class Day11
{
    public long Part1(string filename)
    {
        var stones = File.ReadAllText(filename).Split(' ').Select(long.Parse).ToArray();
        var cache = new Dictionary<long, long[]>();
        return stones.Select(stone => Blink(25, stone, cache)).Sum();
    }

    public long Part2(string filename)
    {
        var stones = File.ReadAllText(filename).Split(' ').Select(long.Parse).ToArray();
        var cache = new Dictionary<long, long[]>();
        return stones.Select(stone => Blink(75, stone, cache)).Sum();
    }

    private long Blink(int timesToBlink, long stone, Dictionary<long, long[]> cache)
    {
        if (timesToBlink == 0) return 1;
        if (cache.TryGetValue(stone, out var x) && x[timesToBlink] != 0) return x[timesToBlink];

        long newStoneCount;
        if (stone == 0)
            newStoneCount = Blink(timesToBlink - 1, 1, cache);
        else
        {
            var f = (int)Math.Log10(stone);
            if (f % 2 == 1)
            {
                var e = (int)Math.Pow(10, (f + 1) / 2.0);
                newStoneCount = Blink(timesToBlink - 1, stone / e, cache) +
                                Blink(timesToBlink - 1, stone % e, cache);
            }
            else
                newStoneCount = Blink(timesToBlink - 1, stone * 2024, cache);
        }
        
        if (cache.TryGetValue(stone, out var y))
        {
            y[timesToBlink] = newStoneCount;
        }
        else
        {
            cache[stone] = new long[76];
            cache[stone][timesToBlink] = newStoneCount;
        }

        return newStoneCount;
    }
}