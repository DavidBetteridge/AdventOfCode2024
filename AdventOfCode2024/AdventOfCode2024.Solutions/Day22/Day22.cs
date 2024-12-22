using System.Collections.Concurrent;

namespace AdventOfCode2024.Solutions;

public class Day22
{
    public long Part1(string filename)
    {
        //var nums = File.ReadAllLines(filename).Select(long.Parse).ToArray();

        var input = File.ReadAllBytes(filename);
        var i = 0;
        var nums = new List<long>();
        while (i < input.Length)
        {
            var num = input[i++] - '0';
            while (i < input.Length && input[i] != '\n')
                num = num * 10 + input[i++] - '0';
            i++;
            nums.Add(num);
        }
        
        var totals = new ConcurrentBag<long>();
        Parallel.ForEach(nums, num =>
        {
            var secretNumber = num;
            for (var iteration = 0; iteration < 2000; iteration++)
            {
                var nextNumber =secretNumber << 6;
                secretNumber = nextNumber ^ secretNumber;
                secretNumber = secretNumber % 16777216;

                nextNumber = (long)(secretNumber >> 5);
                secretNumber = nextNumber ^ secretNumber;
                secretNumber = secretNumber % 16777216;

                nextNumber = secretNumber << 11;
                secretNumber = nextNumber ^ secretNumber;
                secretNumber = secretNumber % 16777216;
            }

            totals.Add(secretNumber);
        });
        return totals.Sum();
    }
}