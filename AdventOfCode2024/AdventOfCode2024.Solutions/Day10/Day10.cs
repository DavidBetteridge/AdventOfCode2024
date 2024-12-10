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

}