namespace AdventOfCode2024.Solutions;

public class Day15
{
    public int Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);

        var inMap = true;
        var robot = (-1, -1);
        var map = new List<char[]>();
        var width = lines[0].Length;
        var commands = "";
        for (var lineNumber = 0; lineNumber < lines.Length; lineNumber++)
        {
            var line = lines[lineNumber];
            if (line == "")
            {
                inMap = false;
                continue;
            }

            if (inMap)
            {
                // # wall,   .=space. O=box. @=robot
                map.Add(new char[width]);
                for (var col = 0; col < line.Length; col++)
                {
                    if (line[col] == '@')
                    {
                        robot = (col, lineNumber);
                        map[lineNumber][col] = '.';
                    }
                    else
                    {
                        map[lineNumber][col] = line[col];
                    }
                }
            }
            else
            {
                commands += line;
            }
        }

        foreach (var command in commands)
        {
            if (command == '<')
            {
                // Is there a space between us and the wall?
                var (x, y) = robot;
                var spaceFoundAt = -1;
                while (map[y][x] != '#')
                {
                    x--;
                    if (map[y][x] == '.')
                    {
                        spaceFoundAt = x;
                        break;
                    }                                        
                }

                if (spaceFoundAt != -1)
                {
                    // We can move everything between spaceFoundAt and robotX one position to the left
                    for (var m = spaceFoundAt; m < robot.Item1; m++)
                    {
                        map[y][m] = map[y][m + 1];
                    }
                    robot = (robot.Item1 - 1, robot.Item2);
                }
            }
            
            if (command == '^')
            {
                // Is there a space between us and the wall?
                var (x, y) = robot;
                var spaceFoundAt = -1;
                while (map[y][x] != '#')
                {
                    y--;
                    if (map[y][x] == '.')
                    {
                        spaceFoundAt = y;
                        break;
                    }                                        
                }

                if (spaceFoundAt != -1)
                {
                    // We can move everything between spaceFoundAt and robotX one position to the left
                    for (var m = spaceFoundAt; m < robot.Item2; m++)
                    {
                        map[m][x] = map[m+1][x];
                    }
                    robot = (robot.Item1, robot.Item2-1);
                }
            }
            
            if (command == '>')
            {
                // Is there a space between us and the wall?
                var (x, y) = robot;
                var spaceFoundAt = -1;
                while (map[y][x] != '#')
                {
                    x++;
                    if (map[y][x] == '.')
                    {
                        spaceFoundAt = x;
                        break;
                    }                                        
                }

                if (spaceFoundAt != -1)
                {
                    // We can move everything between spaceFoundAt and robotX one position to the right
                    for (var m = spaceFoundAt; m > robot.Item1; m--)
                    {
                        map[y][m] = map[y][m - 1];
                    }
                    robot = (robot.Item1 + 1, robot.Item2);
                }
            }
            
            if (command == 'v')
            {
                // Is there a space between us and the wall?
                var (x, y) = robot;
                var spaceFoundAt = -1;
                while (map[y][x] != '#')
                {
                    y++;
                    if (map[y][x] == '.')
                    {
                        spaceFoundAt = y;
                        break;
                    }                                        
                }

                if (spaceFoundAt != -1)
                {
                    // We can move everything between spaceFoundAt and robotX one position to the left
                    for (var m = spaceFoundAt; m > robot.Item2; m--)
                    {
                        map[m][x] = map[m-1][x];
                    }
                    robot = (robot.Item1, robot.Item2+1);
                }
            }
        }

        var score = 0;
        for (var rowNumber = 0; rowNumber < map.Count; rowNumber++)
        {
            for (var col = 0; col < width; col++)
            {
                if (map[rowNumber][col] == 'O')
                {
                    score += (rowNumber * 100 + col);
                }
            }
        }
        

        return score;
        
        void Display()
        {
            for (var rowNumber = 0; rowNumber < map.Count; rowNumber++)
            {
                Console.WriteLine();
                for (var colNumber = 0; colNumber < map[0].Length; colNumber++)
                {
                    if (robot == (colNumber, rowNumber))
                        Console.Write("@");
                    else
                        Console.Write(map[rowNumber][colNumber]);
                }
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
    
    public int Part2(string filename)
    {
        var lines = File.ReadAllLines(filename);

        var inMap = true;
        var robot = (-1, -1);
        var map = new List<char[]>();
        var width = lines[0].Length;
        var commands = "";
        for (var lineNumber = 0; lineNumber < lines.Length; lineNumber++)
        {
            var line = lines[lineNumber];
            if (line == "")
            {
                inMap = false;
                continue;
            }

            if (inMap)
            {
                // # wall,   .=space. O=box. @=robot
                map.Add(new char[width*2]);
                for (var col = 0; col < line.Length; col++)
                {
                    if (line[col] == '@')
                    {
                        robot = (col*2, lineNumber);
                        map[lineNumber][col*2] = '.';
                        map[lineNumber][col*2+1] = '.';
                    }
                    else if (line[col] == 'O')
                    {
                        map[lineNumber][col * 2] = '[';
                        map[lineNumber][col * 2 + 1] = ']';
                    }
                    else if (line[col] == '#')
                    {
                        map[lineNumber][col * 2] = '#';
                        map[lineNumber][col * 2 + 1] = '#';
                    }
                    else
                    {
                        map[lineNumber][col*2] = '.';
                        map[lineNumber][col*2+1] = '.';
                    }
                }
            }
            else
            {
                commands += line;
            }
        }

        foreach (var command in commands)
        {
            if (command == '<')
            {
                // Is there a space between us and the wall?
                var (x, y) = robot;
                var spaceFoundAt = -1;
                while (map[y][x] != '#')
                {
                    x--;
                    if (map[y][x] == '.')
                    {
                        spaceFoundAt = x;
                        break;
                    }                                        
                }

                if (spaceFoundAt != -1)
                {
                    // We can move everything between spaceFoundAt and robotX one position to the left
                    for (var m = spaceFoundAt; m < robot.Item1; m++)
                    {
                        map[y][m] = map[y][m + 1];
                    }
                    robot = (robot.Item1 - 1, robot.Item2);
                }
            }
            
            if (command == '^')
            {
                var (x, y) = robot;
                if (CanMoveUpInto(x, y - 1))
                {
                    MoveUpInto(x, y - 1, '.');
                    robot = (x, y - 1);
                }

                bool CanMoveUpInto(int newX, int newY)
                {
                    if (map[newY][newX] == '.') return true;
                    if (map[newY][newX] == '#') return false;
                    
                    if (map[newY][newX] == '[') return CanMoveUpInto(newX, newY-1) && CanMoveUpInto(newX+1, newY-1);
                    if (map[newY][newX] == ']') return CanMoveUpInto(newX, newY-1) && CanMoveUpInto(newX-1, newY-1);
                    return false;
                }
                
                void MoveUpInto(int newX, int newY, char thingToMove)
                {
                    if (map[newY][newX] == '[')
                    {
                        MoveUpInto(newX, newY - 1, '[');
                        MoveUpInto(newX+1, newY - 1, ']');
                        map[newY][newX+1] = '.';
                    }

                    if (map[newY][newX] == ']')
                    {
                        MoveUpInto(newX-1, newY - 1, '[');
                        MoveUpInto(newX, newY - 1, ']');
                        map[newY][newX-1] = '.';
                    }
                    
                    map[newY][newX] = thingToMove;
                }
            }
            
            if (command == '>')
            {
                // Is there a space between us and the wall?
                var (x, y) = robot;
                var spaceFoundAt = -1;
                while (map[y][x] != '#')
                {
                    x++;
                    if (map[y][x] == '.')
                    {
                        spaceFoundAt = x;
                        break;
                    }                                        
                }

                if (spaceFoundAt != -1)
                {
                    // We can move everything between spaceFoundAt and robotX one position to the right
                    for (var m = spaceFoundAt; m > robot.Item1; m--)
                    {
                        map[y][m] = map[y][m - 1];
                    }
                    robot = (robot.Item1 + 1, robot.Item2);
                }
            }
            
            if (command == 'v')
            {
                var (x, y) = robot;
                if (CanMoveDownInto(x, y + 1))
                {
                    MoveDownInto(x, y + 1, '.');
                    robot = (x, y + 1);
                }

                bool CanMoveDownInto(int newX, int newY)
                {
                    if (map[newY][newX] == '.') return true;
                    if (map[newY][newX] == '#') return false;
                    
                    if (map[newY][newX] == '[') return CanMoveDownInto(newX, newY+1) && CanMoveDownInto(newX+1, newY+1);
                    if (map[newY][newX] == ']') return CanMoveDownInto(newX, newY+1) && CanMoveDownInto(newX-1, newY+1);
                    return false;
                }
                
                void MoveDownInto(int newX, int newY, char thingToMove)
                {
                    if (map[newY][newX] == '[')
                    {
                        MoveDownInto(newX, newY + 1, '[');
                        MoveDownInto(newX+1, newY + 1, ']');
                        map[newY][newX+1] = '.';
                    }

                    if (map[newY][newX] == ']')
                    {
                        MoveDownInto(newX-1, newY + 1, '[');
                        map[newY][newX-1] = '.';
                        MoveDownInto(newX, newY + 1, ']');
                    }
                    
                    map[newY][newX] = thingToMove;
                }
            }
        }

        var score = 0;
        for (var rowNumber = 0; rowNumber < map.Count; rowNumber++)
        {
            for (var col = 0; col < width*2; col++)
            {
                if (map[rowNumber][col] == '[' )
                {
                    score += (rowNumber * 100 + col);
                }
            }
        }

        return score;
        
        void Display()
        {
            for (var rowNumber = 0; rowNumber < map.Count; rowNumber++)
            {
                Console.WriteLine();
                for (var colNumber = 0; colNumber < map[0].Length; colNumber++)
                {
                    if (robot == (colNumber, rowNumber))
                        Console.Write("@");
                    else
                        Console.Write(map[rowNumber][colNumber]);
                }
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}