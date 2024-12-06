namespace AdventOfCode2024.Solutions;

public class Day06
{
    public int Part1(string filename)
    {
        var input = File.ReadAllBytes(filename).AsSpan();
        var map = new bool[input.Length];
        var visited = new bool[input.Length];
        var visitedCount = 1;
        var guard = -1;
        var direction = 0; // 0 means N,  1=E, 2=S and 3=W
        var width = -1;
        var height = 0;
        var i = 0;
        while (i < input.Length)
        {
            while (i < input.Length && input[i] != '\n')
            {
                if (input[i] == '#')
                    map[i-height] = true;

                if (input[i] == '^')
                {
                    guard = i - height;
                    visited[guard]=true;
                }

                i++;
            }

            // End of the line
            if (width == -1)
                width = i;

            height++;
        
            i++; //eat the new line
        }
        
        while (true)
        {
            // Try to walk forward
            var next = direction switch
            {
                0 => guard - width,
                1 => guard + 1,
                2 => guard + width,
                3 => guard - 1,
                _ => throw new Exception("currentDirection out of range")
            };
            
            // Would that be out of bounds
            var ok = direction switch
            {
                0 => next >= 0,
                1 => next % width != 0,  //Walked right, so can't be at x=0
                2 => next < (width * height),
                3 => next % width != (width-1),   // Walked left so can't be width2-1
                _ => throw new Exception("currentDirection out of range")
            };
            
            if (!ok) break;
            if (map[next])
                direction = (direction + 1) % 4; // turn right
            else
            {
                guard = next;

                if (!visited[guard])
                {
                    visited[guard]=true;
                    visitedCount++;
                }
            }
        }
        
        return visitedCount;
    }

    
    public int Part2(string filename)
    {
        //************************************************
        // Part 1 - Parse the file
        var input = File.ReadAllBytes(filename).AsSpan();
        var map = new bool[input.Length].AsSpan();
        var visited = new bool[input.Length].AsSpan();
        var guard = -1;
        var direction = 0; // 0 means N,  1=E, 2=S and 3=W
        var width = -1;
        var height = 0;
        var i = 0;
        while (i < input.Length)
        {
            while (i < input.Length && input[i] != '\n')
            {
                if (input[i] == '#')
                    map[i-height] = true;

                if (input[i] == '^')
                {
                    guard = i - height;
                    visited[guard]=true;
                }

                i++;
            }

            // End of the line
            if (width == -1)
                width = i;

            height++;
        
            i++; //eat the new line
        }
        
        
        //************************************************
        // Part 2 - Find the basic path
        var initialLocation = guard;
        var routeTaken = new List<(int,int)>();
        routeTaken.Add((guard,direction));
        while (true)
        {
            // Try to walk forward
            var next = direction switch
            {
                0 => guard - width,
                1 => guard + 1,
                2 => guard + width,
                3 => guard - 1,
                _ => throw new Exception("currentDirection out of range")
            };
            
            // Would that be out of bounds
            var ok = direction switch
            {
                0 => next >= 0,
                1 => next % width != 0,  //Walked right, so can't be at x=0
                2 => next < (width * height),
                3 => next % width != (width-1),   // Walked left so can be width2-1
                _ => throw new Exception("currentDirection out of range")
            };
            
            if (!ok) break;
            if (map[next])
                direction = (direction + 1) % 4; // turn right
            else
            {
                guard = next;
                visited[guard]=true;
                routeTaken.Add((guard, direction));
            }
        }
        
        
        //************************************************
        // Part 3 - Work out positions for obstructions
        
        // Other than the initial location, we have to try adding an obstruction
        // in each location and seeing if it causes a loop.
        var positions = 0;
        var route = new bool[input.Length*4].AsSpan();
        var locationChecked = new bool[input.Length].AsSpan();
        
        for (var k = 1; k < routeTaken.Count; k++)
        {
            var (j, _) = routeTaken[k];
            
            if (visited[j] && !locationChecked[j])
            {
                var (previousLoc, dirAtTime) = routeTaken[k-1];
                locationChecked[j] = true;
                map[j] = true;
                guard = previousLoc;
                direction = dirAtTime;
                
                route.Clear();
                    
                while (true)
                {
                    // Try to walk forward
                    var next = direction switch
                    {
                        0 => guard - width,
                        1 => guard + 1,
                        2 => guard + width,
                        3 => guard - 1,
                        _ => throw new Exception("currentDirection out of range")
                    };
                    
                    // Would that be out of bounds
                    var ok = direction switch
                    {
                        0 => next >= 0,
                        1 => next % width != 0,  //Walked right, so can't be at x=0
                        2 => next < (width * height),
                        3 => next % width != (width-1),   // Walked left so can't be width2-1
                        _ => throw new Exception("currentDirection out of range")
                    };
                    
                    if (!ok) break;
                    
                    if (map[next])
                        direction = (direction + 1) % 4; // turn right
                    else
                    {
                        guard = next;
                        
                        if (route[(direction * input.Length) + next])
                        {
                            // Looping
                            positions++;
                            break;
                        }
                        route[(direction * input.Length) + next] = true;
                    }
                }
                map[j] = false;
            }
        }
       
        return positions;
    }
}