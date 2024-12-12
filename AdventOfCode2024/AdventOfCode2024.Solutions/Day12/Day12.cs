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
    
     public long Part2(string filename)
    {
        var map = File.ReadAllLines(filename);
        var total = 0;
        var height = map.Length;
        var width = map[0].Length;
        var seen = new bool[height * width];
        var sideCounted = new HashSet<(int,int)>();  // Loc,Dir
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
        
        (int, int)? Fill(int column, int row, char region)
        {
            if (!IsValid(column, row))
                return null;
            if (map[row][column] != region)
                return null;
            var key = (row * width) + column;
            if (seen[key])
                return (0,0);

            seen[key] = true;
            var area = 1;
            var sides = 0;
            
            var n = Fill(column, row-1, region);
            if (n is null)
            {
                // We only count a side if the square to left/right hasn't been counted
                const int north = 1;
                if (!sideCounted.Contains((key, north)))
                {
                    // We are on an edge, which hasn't yet been counted.
                    sides += 1;
                    Console.WriteLine($"{region} :: {column}, {row} NORTH");
                    
                    // Mark both ourselves, and all touching regions as an edge
                    var c = column;
                    while (c >= 0 && map[row][c] == region && IsEdge(c,row-1,region))
                    {
                        sideCounted.Add(((row * width) + c , north));
                        c--;
                    }

                    c = column + 1;
                    while (c < width && map[row][c] == region && IsEdge(c,row-1,region))
                    {
                        sideCounted.Add(((row * width) + c , north));
                        c++;
                    }
                }
            }
            else
            {
                area += n.Value.Item1;
                sides += n.Value.Item2;
            }

            var s = Fill(column, row+1, region);
            if (s is null)
            {
                // We only count a side if the square to left/right hasn't been counted
                const int south = 2;
                if (!sideCounted.Contains((key, south)))
                {
                    // We are on an edge, which hasn't yet been counted.
                    sides += 1;
                    Console.WriteLine($"{region} :: {column}, {row} SOUTH");
                    
                    // Mark both ourselves, and all touching regions as an edge
                    var c = column;
                    while (c >= 0 && map[row][c] == region && IsEdge(c,row+1,region))
                    {
                        sideCounted.Add(((row * width) + c , south));
                        c--;
                    }

                    c = column + 1;
                    while (c < width && map[row][c] == region && IsEdge(c,row+1,region))
                    {
                        sideCounted.Add(((row * width) + c , south));
                        c++;
                    }
                }
            }
            else
            {
                area += s.Value.Item1;
                sides += s.Value.Item2;
            }
            
            var w = Fill(column-1, row, region);
            if (w is null)
            {
                // We only count a side if the square to above/below hasn't been counted
                const int west = 3;
                if (!sideCounted.Contains((key, west)))
                {
                    // We are on an edge, which hasn't yet been counted.
                    sides += 1;
                    Console.WriteLine($"{region} :: {column}, {row} WEST");
                    
                    // Mark both ourselves, and all touching regions as an edge
                    var r = row;
                    while (r >= 0 && map[r][column] == region && IsEdge(column-1,r,region))
                    {
                        sideCounted.Add(((r * width) + column , west));
                        r--;
                    }

                    r = row+1;
                    while (r < height && map[r][column] == region && IsEdge(column-1,r,region))
                    {
                        sideCounted.Add(((r * width) + column , west));
                        r++;
                    }
                }
            }
            else
            {
                area += w.Value.Item1;
                sides += w.Value.Item2;
            }
            
            var e = Fill(column+1, row, region);
            if (e is null)
            {
                // We only count a side if the square to above/below hasn't been counted
                const int east = 4;
                if (!sideCounted.Contains((key, east)))
                {
                    // We are on an edge, which hasn't yet been counted.
                    sides += 1;
                    Console.WriteLine($"{region} :: {column}, {row} EAST");
                    
                    // Mark both ourselves, and all touching regions as an edge
                    var r = row;
                    while (r >= 0 && map[r][column] == region && IsEdge(column+1,r,region))
                    {
                        sideCounted.Add(((r * width) + column , east));
                        r--;
                    }

                    r = row+1;
                    while (r < height && map[r][column] == region && IsEdge(column+1,r,region))
                    {
                        sideCounted.Add(((r * width) + column , east));
                        r++;
                    }
                }
            }
            else
            {
                area += e.Value.Item1;
                sides += e.Value.Item2;
            }
            
            return (area, sides);
        }
        
        bool IsValid(int column, int row)
        {
            return (column >= 0 && column < width && row >= 0 && row < height);
        }

        bool IsEdge(int column, int row, char region)
        {
            if (!IsValid(column, row))
                return true;
            if (map[row][column] != region)
                return true;
            return false;
        }
    }
}