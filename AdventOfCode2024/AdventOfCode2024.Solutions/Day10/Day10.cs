using System.Runtime.ExceptionServices;

namespace AdventOfCode2024.Solutions;

public class Day10
{
    public long Part1(string filename)
    {
        var map = File.ReadAllLines(filename)
                     .Select(line => line.ToCharArray().Select(c => c - '0').ToArray()).ToArray();
        var score = 0;
        var height = map.Length;
        var width = map[0].Length;

        var seen = new HashSet<int>();
        for (var rowNumber = 0; rowNumber < height; rowNumber++)
        {
            for (var columnNumber = 0; columnNumber < width; columnNumber++)
            {
                seen.Clear();
                score += CountPaths(rowNumber, columnNumber, 0);
            }
        }
        
        return score;
        
        int CountPaths(int rowNumber, int columnNumber, int currentValue)
        {
            if (rowNumber < 0 || rowNumber >= height || columnNumber < 0 || columnNumber >= width) return 0;
            
            if (map[rowNumber][columnNumber] != currentValue) return 0;
            if (map[rowNumber][columnNumber] == 9)
            {
                if (seen.Contains((columnNumber * width) + rowNumber)) return 0;
                seen.Add((columnNumber * width) + rowNumber);
                return 1;
            }
                
            return CountPaths(rowNumber, columnNumber+1 , currentValue + 1) +
                   CountPaths(rowNumber, columnNumber-1, currentValue + 1) +
                   CountPaths(rowNumber+1, columnNumber, currentValue + 1) +
                   CountPaths(rowNumber-1, columnNumber, currentValue + 1);
        }
    }

    public long Part2(string filename)
    {
        var map = File.ReadAllLines(filename)
            .Select(line => line.Select(c => c - '0').ToArray()).ToArray();
        var height = map.Length;
        var width = map[0].Length;

        var temp = new Dictionary<(int, int), int>();
        var temp2 = new Dictionary<(int, int), int>();
        
        for (var rowNumber = 0; rowNumber < height; rowNumber++)
        {
            for (var columnNumber = 0; columnNumber < width; columnNumber++)
            {
                if (map[rowNumber][columnNumber] == 9)
                    temp.Add((columnNumber, rowNumber),1);
            }
        }

        for (var target = 8; target >= 0; target--)
        {
            var previousValues = target % 2 == 0 ? temp : temp2;
            var futureValues = target % 2 == 1 ? temp : temp2;
            
            futureValues.Clear();
            
            foreach (var previous in previousValues)
            {
                var (x, y) = previous.Key;
                if (x > 0 && map[y][x - 1] == target)
                    futureValues[(x - 1, y)] = futureValues.GetValueOrDefault((x - 1, y)) + previous.Value;
                if (x + 1 < width && map[y][x + 1] == target)
                    futureValues[(x + 1, y)] = futureValues.GetValueOrDefault((x + 1, y)) + previous.Value;
                if (y > 0 && map[y - 1][x] == target)
                    futureValues[(x, y - 1)] = futureValues.GetValueOrDefault((x, y - 1)) + previous.Value;
                if (y + 1 < height && map[y + 1][x] == target)
                    futureValues[(x, y + 1)] = futureValues.GetValueOrDefault((x, y + 1)) + previous.Value;            
            }
        }
        
        return temp2.Values.Sum();
    }
}