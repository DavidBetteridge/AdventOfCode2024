namespace AdventOfCode2024.Solutions;

public class Day22
{
    public long Part1(string filename)
    {
        var nums = File.ReadAllLines(filename).Select(long.Parse).ToArray();
        var total = 0L;
        foreach (var num in nums)
        {
            var secretNumber = num;
            for (var i = 0; i < 2000; i++)
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
            }

            total += secretNumber;
        }
        


        return total;
    }
}