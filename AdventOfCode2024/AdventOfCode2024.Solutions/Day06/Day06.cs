namespace AdventOfCode2024.Solutions;

public class Day06
{
    private record struct Location(int X, int Y);
    public int Part1(string filename)
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

        visited.Add(guard);
        while (true)
        {
            // Try to walk forward
            var next = NextLocation(guard, direction);
            if (obstructions.Contains(next))
                direction = (direction + 1) % 4; // turn right
            else
            {
                guard = next;
                if (!InLab()) break;
                visited.Add(guard);
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
        
        bool InLab()
        {
            return guard.X >= 0 && guard.X < width && guard.Y >= 0 && guard.Y < lab.Length;
        }
    }
}