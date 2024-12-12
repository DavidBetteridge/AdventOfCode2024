namespace AdventOfCode2024.Solutions;

public class Day12
{
    public long Part1(string filename)
    {
        var map = File.ReadAllLines(filename);
        var total = 0;
        var height = map.Length;
        var width = map[0].Length;
        var seen = new bool[height * width];
        
        for (var rowNumber = 0; rowNumber < height; rowNumber++)
        {
            for (var columnNumber = 0; columnNumber < width; columnNumber++)
            {
                var key = (rowNumber * width) + columnNumber;
                if (!seen[key])
                {
                    var next = Fill(columnNumber, rowNumber, map[rowNumber][columnNumber]);
                    total += next!.Value.Item1 * next.Value.Item2;

                    seen[key] = true;
                }
            }
        }
        
        return total;
        
        (int,int)? Fill(int column, int row, char region)
        {
            if (column < 0 || column >= width || row < 0 || row >= height)
                return null;
            if (map[row][column] != region)
                return null;
            var key = (row * width) + column;
            if (seen[key])
                return (0,0);

            seen[key] = true;

            var area = 1;
            var perimeter = 0;
            
            var n = Fill(column, row-1, region);
            if (n is null)
                perimeter += 1;
            else
            {
                area += n.Value.Item1;
                perimeter += n.Value.Item2;
            }

            var s = Fill(column, row+1, region);
            if (s is null)
                perimeter += 1;
            else
            {
                area += s.Value.Item1;
                perimeter += s.Value.Item2;
            }
            
            var w = Fill(column-1, row, region);
            if (w is null)
                perimeter += 1;
            else
            {
                area += w.Value.Item1;
                perimeter += w.Value.Item2;
            }
            
            var e = Fill(column+1, row, region);
            if (e is null)
                perimeter += 1;
            else
            {
                area += e.Value.Item1;
                perimeter += e.Value.Item2;
            }
            
            return (area, perimeter);
        }
    }
}