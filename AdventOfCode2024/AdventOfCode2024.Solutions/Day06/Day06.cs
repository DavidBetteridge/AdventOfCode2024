namespace AdventOfCode2024.Solutions;

public class Day06
{
    private record struct Location(int X, int Y);
    public int Part1(string filename)
    {
        var input = File.ReadAllBytes(filename).AsSpan();
        var map = new bool[input.Length];
        var visited = new HashSet<int>();
        var guard = new Location(0, 0);
        var direction = 0; // 0 means N,  1=E, 2=S and 3=W
        var i = 0;
        var width2 = -1;
        var height2 = 0;
        while (i < input.Length)
        {
            while (i < input.Length && input[i] != '\n')
            {
                if (input[i] == '#')
                    map[i-height2] = true;

                if (input[i] == '^')
                {
                    guard = new Location((i - height2) - (width2 * height2), height2);
                    visited.Add(i-height2);
                }

                i++;
            }

            // End of the line
            if (width2 == -1)
                width2 = i;

            height2++;
        
            i++; //eat the new line
        }
        
        while (true)
        {
            // Try to walk forward
            var next = NextLocation(guard, direction);
            if (next.X < 0 || next.X >= width2 || next.Y < 0 || next.Y >= height2) break;
            if (map[(next.Y * width2) + next.X])
                direction = (direction + 1) % 4; // turn right
            else
            {
                guard = next;
                visited.Add((next.Y * width2) + next.X);
            }
        }
        
        return visited.Count;

        Location NextLocation(Location currentLocation, int currentDirection)
        {
            return currentDirection switch
            {
                0 => currentLocation with { Y = currentLocation.Y - 1 },
                1 => currentLocation with { X = currentLocation.X + 1 },
                2 => currentLocation with { Y = currentLocation.Y + 1 },
                3 => currentLocation with { X = currentLocation.X - 1 },
                _ => throw new Exception("currentDirection out of range")
            };
        }
    }
    
    
    public int Part2(string filename)
    {
        var lab = File.ReadAllLines(filename);
        var guard = new Location(0,0);
        var direction = 0;  // 0 means N,  1=E, 2=S and 3=W
        
        var obstructions = new HashSet<Location>();
        var visited = new HashSet<Location>();

        var width = lab[0].Length;
        for (var rowNumber = 0; rowNumber < lab.Length; rowNumber++)
        {
            for (var columnNumber = 0; columnNumber < width; columnNumber++)
            {
                if (lab[rowNumber][columnNumber] == '#')
                    obstructions.Add(new Location(columnNumber, rowNumber));
                else if (lab[rowNumber][columnNumber] == '^')
                    guard = new Location(columnNumber, rowNumber);
            }
        }

        var initialLocation = guard;
        
        visited.Add(guard);
        while (true)
        {
            // Try to walk forward
            var next = NextLocation(guard, direction);
            if (next.X < 0 || next.X >= width || next.Y < 0 || next.Y >= lab.Length) break;
            
            if (obstructions.Contains(next))
                direction = (direction + 1) % 4; // turn right
            else
            {
                guard = next;
                visited.Add(guard);
            }
        }
        
        // Other than the initial location, we have to try adding an obstruction
        // in each location and seeing if it causes a loop.
        var positions = 0;
        var route = new HashSet<(Location, int)>();
        foreach (var possible in visited)
        {
            if (possible != initialLocation)
            {
                obstructions.Add(possible);
                guard = initialLocation;
                direction = 0;
                route.Clear();
                while (true)
                {
                    // Try to walk forward
                    var next = NextLocation(guard, direction);
                    if (obstructions.Contains(next))
                        direction = (direction + 1) % 4; // turn right
                    else
                    {
                        guard = next;
                        if (guard.X < 0 || guard.X == width || guard.Y < 0 || guard.Y == lab.Length) break;
                        if (route.Contains((guard, direction)))
                        {
                            // Looping
                            positions++;
                            break;
                        }
                        route.Add((next, direction));
                    }
                }
                obstructions.Remove(possible);
            }
        }
        
        return positions;

        Location NextLocation(Location currentLocation, int currentDirection)
        {
            return currentDirection switch
            {
                0 => currentLocation with { Y = currentLocation.Y - 1 },
                1 => currentLocation with { X = currentLocation.X + 1 },
                2 => currentLocation with { Y = currentLocation.Y + 1 },
                3 => currentLocation with { X = currentLocation.X - 1 },
                _ => throw new Exception("currentDirection out of range")
            };
        }
   
    }
}