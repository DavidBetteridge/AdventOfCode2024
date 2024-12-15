using Microsoft.CodeAnalysis.CSharp;

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

        Console.WriteLine("Initial State");
        Display();
        
        foreach (var command in commands)
        {
            Console.WriteLine($"Move {command}:");

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

            
         //   Display();
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
}