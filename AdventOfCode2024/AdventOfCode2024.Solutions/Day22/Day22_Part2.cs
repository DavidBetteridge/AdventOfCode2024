using System.Collections.Concurrent;

namespace AdventOfCode2024.Solutions;

public class Day22_Part2
{
    public async Task<long> Part2(string filename, int numberOfIterations)
    {
        var input = File.ReadAllBytes(filename);
        var i = 0;
        var nums = new List<int>();
        while (i < input.Length)
        {
            var num = input[i++] - '0';
            while (i < input.Length && input[i] != '\n')
                num = num * 10 + input[i++] - '0';
            i++;
            nums.Add(num);
        }

        var q = new BlockingCollection<Dictionary<Int128, uint>>();
        var totalByOffset = new Dictionary<Int128, uint>();
        var queueProcessor = Task.Run(() =>
        {
            for (int j = 0; j < nums.Count; j++)
            {
                var next = q.Take();
                foreach (var pair in next)
                    totalByOffset[pair.Key] = totalByOffset.GetValueOrDefault(pair.Key) + pair.Value;
            }
        });
        
        var mask = (Int128)(Math.Pow(2, 100)) - 1;

        Parallel.ForEach(nums, num =>
        {
            var secretNumber = (uint)num;
            var previousPrice = secretNumber % 10;
            System.Int128 offsets = 0;
            var seenOffsets = new Dictionary<Int128, uint>();
            for (var iteration = 0; iteration < numberOfIterations; iteration++)
            {
                var nextNumber = secretNumber << 6;
                secretNumber = nextNumber ^ secretNumber;
                secretNumber = secretNumber % 16777216;

                nextNumber = (uint)(secretNumber >> 5);
                secretNumber = nextNumber ^ secretNumber;
                secretNumber = secretNumber % 16777216;

                nextNumber = secretNumber << 11;
                secretNumber = nextNumber ^ secretNumber;
                secretNumber = secretNumber % 16777216;

                var price = secretNumber % 10;
                var offset = price - previousPrice;
                offsets = (offsets << 25) + (offset + 16777216);
                offsets &= mask;
                previousPrice = price;

                seenOffsets.TryAdd(offsets, price);
            }
            q.Add(seenOffsets);
        });

        await queueProcessor;
        return totalByOffset.Values.Max();
    }
}