namespace AdventOfCode2024.Solutions;

public class Day22_Part2
{
    public long Part2(string filename, int numberOfIterations)
    {
        // 16,777,216 needs 24 bits, plus 1 bits for a sign
        // Need 4 offsets,  so 100bits
        
        var nums = File.ReadAllLines(filename).Select(long.Parse).ToArray();
        var totalByOffset = new Dictionary<Int128, int>();
        var seenOffsets = new HashSet<Int128>();
        var mask = (Int128)(Math.Pow(2, 100)) - 1;
        foreach (var num in nums)
        {
            var secretNumber = num;
            var previousPrice = secretNumber%10;
            System.Int128 offsets = 0;
            seenOffsets.Clear();
            for (var i = 0; i < numberOfIterations; i++)
            {
                var nextNumber =secretNumber* 64;
                secretNumber = nextNumber ^ secretNumber;
                secretNumber = secretNumber % 16777216;

                nextNumber = (long)(secretNumber / 32);
                secretNumber = nextNumber ^ secretNumber;
                secretNumber = secretNumber % 16777216;

                nextNumber =secretNumber* 2048;
                secretNumber = nextNumber ^ secretNumber;
                secretNumber = secretNumber % 16777216;

                var price = (int)secretNumber % 10;
                var offset = price - previousPrice;
                offsets = (offsets << 25) + (offset + 16777216);
                offsets &= mask;
                previousPrice = price;

                if (!seenOffsets.Contains(offsets))
                {
                    seenOffsets.Add(offsets);
                    totalByOffset[offsets] = totalByOffset.GetValueOrDefault(offsets) + price;
                }
            }
        }

        return totalByOffset.Values.Max();
    }
}