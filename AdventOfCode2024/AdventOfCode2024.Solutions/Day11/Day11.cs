namespace AdventOfCode2024.Solutions;

public class Day11
{
    public long Part1(string filename)
    {
        var stones = File.ReadAllText(filename).Split(' ').Select(long.Parse).ToArray();
        var cache = new Dictionary<(long, int), long>();
        return stones.Select(stone => Blink(25, stone, cache)).Sum();
    }

    public long Part2(string filename)
    {
        var stones = File.ReadAllText(filename).Split(' ').Select(long.Parse).ToArray();
        var cache = new Dictionary<(long, int), long>();
        return stones.Select(stone => Blink(75, stone, cache)).Sum();
    }
    
    private long Blink(int timesToBlink, long stone, Dictionary<(long,int),long> cache)
    {
        if (cache.TryGetValue(((stone, timesToBlink)), out var x)) return x;
        
        if (timesToBlink == 0) return 1;

        var newStoneCount = -1L;
        if (stone == 0)
            newStoneCount = Blink(timesToBlink - 1, 1, cache);
        else if (( Math.Floor(Math.Log10(stone))) % 2 == 1)
        {
            var e = (int)Math.Pow(10, (((int)Math.Log10(stone)) + 1) / 2);
            newStoneCount = Blink(timesToBlink - 1, stone / e, cache) +
                            Blink(timesToBlink - 1, stone % e, cache);
        }
        else
            newStoneCount = Blink(timesToBlink -1 , stone * 2024, cache);

        cache[(stone, timesToBlink)] = newStoneCount;
        
        return newStoneCount;
    }
}