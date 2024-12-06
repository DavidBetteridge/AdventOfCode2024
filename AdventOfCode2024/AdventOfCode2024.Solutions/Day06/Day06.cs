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
                3 => next % width != (width-1),   // Walked left so can be width2-1
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
        
        var initialLocation = guard;
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
            }
        }
        
        
        // Other than the initial location, we have to try adding an obstruction
        // in each location and seeing if it causes a loop.
        var positions = 0;
        var n = new bool[input.Length].AsSpan();
        var s = new bool[input.Length].AsSpan();
        var w = new bool[input.Length].AsSpan();
        var e = new bool[input.Length].AsSpan();
        
        for (var j = 0; j < input.Length; j++)
        {
            if (visited[j] && j != initialLocation)
            {
                map[j] = true;
                guard = initialLocation;
                direction = 0;
                
                n.Clear();
                s.Clear();
                w.Clear();
                e.Clear();
                    
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
                        
                        if (
                            (direction == 0 && n[next]) ||
                            (direction == 1 && e[next]) ||
                            (direction == 2 && s[next]) ||
                            (direction == 3 && w[next])
                            )
                        {
                            // Looping
                            positions++;
                            break;
                        }

                        if (direction == 0)
                            n[next] = true;
                        else if (direction == 1)
                            e[next] = true;
                        else if (direction == 2)
                            s[next] = true;
                        else if (direction == 3)
                            w[next] = true;
                    }
                }
                map[j] = false;
            }
        }
       
        return positions;
    }
}