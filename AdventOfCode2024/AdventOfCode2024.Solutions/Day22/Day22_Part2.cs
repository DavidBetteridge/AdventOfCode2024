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

        var q = new BlockingCollection<Dictionary<long, uint>>();
        var totalByOffset = new Dictionary<long, uint>();
        var queueProcessor = Task.Run(() =>
        {
            for (var j = 0; j < nums.Count; j++)
            {
                var next = q.Take();
                foreach (var pair in next)
                    totalByOffset[pair.Key] = totalByOffset.GetValueOrDefault(pair.Key) + pair.Value;
            }
        });
        

        Parallel.ForEach(nums, num =>
        {
            var secretNumber = (uint)num;
            var previousPrice = secretNumber % 10;
            uint offsets = 0;
            var seenOffsets = new Dictionary<long, uint>();
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
                var offset = (price - previousPrice) + 9;

                // offset is -9 to 9.
                // Adding 9 brings it in the range 0 ... 18. ...5 bits so will fit in an integer
                offsets = (offsets << 8) + offset;
                previousPrice = price;

                seenOffsets.TryAdd(offsets, price);
            }
            q.Add(seenOffsets);
        });

        await queueProcessor;
        return totalByOffset.Values.Max();
    }
}