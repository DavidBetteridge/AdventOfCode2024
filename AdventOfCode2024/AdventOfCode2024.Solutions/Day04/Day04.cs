namespace AdventOfCode2024.Solutions;

public class Day04
{
    private record Offset(int X, int Y);
    
    public int Part1(string filename)
    {
        var grid = File.ReadAllLines(filename);
        var rowWidth = grid[0].Length;
        var total = 0;

        var directions = new Offset[][]
        {
            [new Offset(1,0),new Offset(2,0),new Offset(3,0)],     //  L -> R
            [new Offset(-1,0),new Offset(-2,0),new Offset(-3,0)],  //  R --> L
            [new Offset(0,1),new Offset(0,2),new Offset(0, 3)],    //  T --> D
            [new Offset(0,-1),new Offset(0,-2),new Offset(0, -3)],  //  D --> T
            
            [new Offset(1,1),new Offset(2,2),new Offset(3,3)], 
            [new Offset(1,-1),new Offset(2,-2),new Offset(3,-3)], 
            [new Offset(-1,1),new Offset(-2,2),new Offset(-3,3)], 
            [new Offset(-1,-1),new Offset(-2,-2),new Offset(-3,-3)], 
        };

        for (var rowNumber = 0; rowNumber <grid.Length; rowNumber++)
        {
            for (var columnNumber = 0; columnNumber < rowWidth; columnNumber++)
            {
                if (grid[rowNumber][columnNumber] == 'X')
                {
                    foreach (var direction in directions)
                    {
                        if (Read(rowNumber, columnNumber, direction[0]) == 'M' && 
                            Read(rowNumber, columnNumber, direction[1]) == 'A' && 
                            Read(rowNumber, columnNumber, direction[2]) == 'S')
                        {
                            total++;
                        }
                    }
                }
            }
        }
        
        return total;
        
        char Read(int rowNumber, int columnNumber, Offset offset)
        {
            var x = columnNumber + offset.X;
            var y = rowNumber + offset.Y;
            if (x < 0 || x + 1 > rowWidth) return ' ';
            if (y < 0 || y + 1 > grid.Length) return ' ';
            return grid[y][x];
        }
    }

    public int Part2(string filename)
    {
        var grid = File.ReadAllLines(filename);
        var rowWidth = grid[0].Length;
        var total = 0;

        var directions = new Offset[][]
        {
            
            // M.S
            // .A.
            // M.S
            
            // M             M                    S                S
            [new Offset(-1,-1),new Offset(-1,1),new Offset(1,-1),new Offset(1,1)],    

            // S.S
            // .A.
            // M.M
            
            // M             M                    S                S
            [new Offset(-1,1),new Offset(1,1),new Offset(1,-1),new Offset(-1,-1)],    
            
            // S.M
            // .A.
            // S.M
            
            // M             M                    S                S
            [new Offset(1,-1),new Offset(1,1),new Offset(-1,1),new Offset(-1,-1)],    
            
            // M.M
            // .A.
            // S.S
            
            // M             M                    S                S
            [new Offset(-1,-1),new Offset(1,-1),new Offset(-1,1),new Offset(1,1)],   
            
        };

        for (var rowNumber = 0; rowNumber <grid.Length; rowNumber++)
        {
            for (var columnNumber = 0; columnNumber < rowWidth; columnNumber++)
            {
                if (grid[rowNumber][columnNumber] == 'A')
                {
                    foreach (var direction in directions)
                    {
                        if (Read(rowNumber, columnNumber, direction[0]) == 'M' && 
                            Read(rowNumber, columnNumber, direction[1]) == 'M' && 
                            Read(rowNumber, columnNumber, direction[2]) == 'S' && 
                            Read(rowNumber, columnNumber, direction[2]) == 'S')
                        {
                            total++;
                        }
                    }
                }
            }
        }
        
        return total;
        
        char Read(int rowNumber, int columnNumber, Offset offset)
        {
            var x = columnNumber + offset.X;
            var y = rowNumber + offset.Y;
            if (x < 0 || x + 1 > rowWidth) return ' ';
            if (y < 0 || y + 1 > grid.Length) return ' ';
            return grid[y][x];
        }
    }
}