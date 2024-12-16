namespace AdventOfCode2024.Solutions;

public class Day16
{
    private record Loc(int x, int y);
    private record Position(int x, int y, int dir, int score);
    public int Part1(string filename)
    {
        const int north=1;
        const int east=2;
        const int south=3;
        const int west=4;
        
        var file = File.ReadAllLines(filename);
        var height = file.Length;
        var width = file[0].Length;
        var walls = new bool[width * height];

        var reindeer = new Position(-1, -1, east, 0);
        var target = new Loc(-1, -1);
        for (var row = 0; row < height; row++)
        {
            for (var col = 0; col < width; col++)
            {
                if (file[row][col] == 'S')
                    reindeer = new Position(col, row, east, 0);
                else if (file[row][col] == 'E')
                    target = new Loc(col, row);
                else if (file[row][col] == '#')
                    walls[row * width + col] = true;
            }
        }

        var seen = new Dictionary<int,int>();  // (row * width + col << 2) + dir. containing lowest score
        seen.Add((((reindeer.y * width) + reindeer.x) << 2) + reindeer.dir,0);
        
        // X, Y, Dir, Lowest Score
        var routesToTry = new Stack<Position>();
        TryNorth(reindeer);
        TrySouth(reindeer);
        TryWest(reindeer);
        TryEast(reindeer);

        var bestScore = int.MaxValue;
        while (routesToTry.Count > 0)
        {
            var currentPosition = routesToTry.Pop();

            if (currentPosition.x == target.x && currentPosition.y == target.y)
            {
                bestScore = Math.Min(bestScore, currentPosition.score);
            }
            else
            {
                TryNorth(currentPosition);
                TrySouth(currentPosition);
                TryWest(currentPosition);
                TryEast(currentPosition);
            }
        }
        
        return bestScore;

        void TryNorth(Position current)
        {
            if (!walls[(current.y - 1) * width + current.x])
            {
               var cost = current.dir switch
                {
                    north => 1,
                    east => 1001,
                    west => 1001,
                    _ => -1
                };
                if (cost != -1)
                {
                    var currentCost = seen[(((current.y * width) + current.x) << 2) + current.dir];
                    var key = ((((current.y-1) * width) + current.x) << 2) + north;
                    if (seen.TryGetValue(key, out var previousScore))
                    {
                        if (currentCost + cost < previousScore)
                        {
                            routesToTry.Push(new Position(current.x, current.y - 1, north, current.score + cost));
                            seen[key] = currentCost + cost;
                        }
                    }
                    else
                    {
                        routesToTry.Push(new Position(current.x, current.y - 1, north, current.score + cost));
                        seen[key] = currentCost + cost;
                    }
                }
            }
        }
        
        void TrySouth(Position current)
        {
            if (!walls[(current.y + 1) * width + current.x])
            {
                var cost = current.dir switch
                {
                    east => 1001,
                    south => 1,
                    west => 1001,
                    _ => -1
                };
                if (cost != -1)
                {
                    var currentCost = seen[(((current.y * width) + current.x) << 2) + current.dir];
                    var key = ((((current.y+1) * width) + current.x) << 2) + south;
                    if (seen.TryGetValue(key, out var previousScore))
                    {
                        if (currentCost + cost < previousScore)
                        {
                            routesToTry.Push(new Position(current.x, current.y + 1, south, current.score + cost));
                            seen[key] = currentCost + cost;
                        }
                    }
                    else
                    {
                        routesToTry.Push(new Position(current.x, current.y + 1, south, current.score + cost));
                        seen[key] = currentCost + cost;
                    }
                }
            }
        }
        
        void TryWest(Position current)
        {
            if (!walls[(current.y) * width + current.x-1])
            {
                var cost = current.dir switch
                {
                    north => 1001,
                    south => 1001,
                    west => 1,
                    _ => -1
                };
                if (cost != -1)
                {
                    var currentCost = seen[(((current.y * width) + current.x) << 2) + current.dir];
                    var key = ((((current.y) * width) + (current.x-1) << 2) + west);
                    if (seen.TryGetValue(key, out var previousScore))
                    {
                        if (currentCost + cost < previousScore)
                        {
                            routesToTry.Push(new Position(current.x-1, current.y , west, current.score + cost));
                            seen[key] = currentCost + cost;
                        }
                    }
                    else
                    {
                        routesToTry.Push(new Position(current.x-1, current.y , west, current.score + cost));
                        seen[key] = currentCost + cost;
                    }
                }
            }
        }
        
        void TryEast(Position current)
        {
            if (!walls[(current.y) * width + current.x+1])
            {
                var cost = current.dir switch
                {
                    north => 1001,
                    east => 1,
                    south => 1001,
                    _ => -1
                };
                if (cost != -1)
                {
                    var currentCost = seen[(((current.y * width) + current.x) << 2) + current.dir];
                    var key = ((((current.y) * width) + (current.x+1) << 2) + east);
                    if (seen.TryGetValue(key, out var previousScore))
                    {
                        if (currentCost + cost < previousScore)
                        {
                            routesToTry.Push(new Position(current.x+1, current.y , east, current.score + cost));
                            seen[key] = currentCost + cost;
                        }
                    }
                    else
                    {
                        routesToTry.Push(new Position(current.x+1, current.y , east, current.score + cost));
                        seen[key] = currentCost + cost;
                    }
                }
            }
        }
    }
    
    
    public int Part2(string filename)
    {
        const int north=1;
        const int east=2;
        const int south=3;
        const int west=4;
        
        var file = File.ReadAllLines(filename);
        var height = file.Length;
        var width = file[0].Length;
        var walls = new bool[width * height];

        var reindeer = new Position(-1, -1, east, 0);
        var target = new Loc(-1, -1);
        for (var row = 0; row < height; row++)
        {
            for (var col = 0; col < width; col++)
            {
                if (file[row][col] == 'S')
                    reindeer = new Position(col, row, east, 0);
                else if (file[row][col] == 'E')
                    target = new Loc(col, row);
                else if (file[row][col] == '#')
                    walls[row * width + col] = true;
            }
        }

        var seen = new Dictionary<int,int>();  // (row * width + col << 2) + dir. containing lowest score
        seen.Add((((reindeer.y * width) + reindeer.x) << 2) + reindeer.dir,0);
        
        // X, Y, Dir, Lowest Score
        var routesToTry = new Stack<Position>();
        TryNorth(reindeer);
        TrySouth(reindeer);
        TryWest(reindeer);
        TryEast(reindeer);

        var bestScore = int.MaxValue;
        while (routesToTry.Count > 0)
        {
            var currentPosition = routesToTry.Pop();

            if (currentPosition.x == target.x && currentPosition.y == target.y)
            {
                bestScore = Math.Min(bestScore, currentPosition.score);
            }
            else
            {
                TryNorth(currentPosition);
                TrySouth(currentPosition);
                TryWest(currentPosition);
                TryEast(currentPosition);
            }
        }

        var seats = new HashSet<Loc>();
        seats.Add(new Loc(target.x, target.y));
        var toSpend = bestScore;

        var possibleRoutes = new Stack<Position>();
        if (seen.TryGetValue((((target.y * width) + target.x) << 2) + north, out var cost) && cost == toSpend)
            possibleRoutes.Push(new Position(target.x, target.y+1, north, toSpend));
        
        if (seen.TryGetValue((((target.y * width) + target.x) << 2) + south, out cost) && cost == toSpend)
            possibleRoutes.Push(new Position(target.x, target.y-1, south, toSpend));
        
        if (seen.TryGetValue((((target.y * width) + target.x) << 2) + west, out cost) && cost == toSpend)
            possibleRoutes.Push(new Position(target.x+1, target.y, west, toSpend));
        
        if (seen.TryGetValue((((target.y * width) + target.x) << 2) + east, out cost) && cost == toSpend)
            possibleRoutes.Push(new Position(target.x-1, target.y, east, toSpend));

        while (possibleRoutes.Count > 0)
        {
            var possibleRoute = possibleRoutes.Pop();

            var costFromSouth = seen.GetValueOrDefault((((possibleRoute.y * width) + possibleRoute.x) << 2) + north);
            var costFromNorth = seen.GetValueOrDefault((((possibleRoute.y * width) + possibleRoute.x) << 2) + south);
            var costFromWest = seen.GetValueOrDefault((((possibleRoute.y * width) + possibleRoute.x) << 2) + east);
            var costFromEast = seen.GetValueOrDefault((((possibleRoute.y * width) + possibleRoute.x) << 2) + west);

            if (possibleRoute.dir == north)
            {
                if (costFromSouth + 1 == possibleRoute.score)
                {
                    possibleRoutes.Push(new Position(possibleRoute.x, possibleRoute.y+1, north, costFromSouth));
                    seats.Add(new Loc(possibleRoute.x, possibleRoute.y));
                }
                if (costFromWest + 1001 == possibleRoute.score)
                {
                    possibleRoutes.Push(new Position(possibleRoute.x-1, possibleRoute.y, east, costFromWest));
                    seats.Add(new Loc(possibleRoute.x, possibleRoute.y));
                }
                if (costFromEast + 1001 == possibleRoute.score)
                {
                    possibleRoutes.Push(new Position(possibleRoute.x+1, possibleRoute.y, west, costFromEast));
                    seats.Add(new Loc(possibleRoute.x, possibleRoute.y));
                }
            }
            
            if (possibleRoute.dir == west)
            {
                if (costFromEast + 1 == possibleRoute.score)
                {
                    possibleRoutes.Push(new Position(possibleRoute.x+1, possibleRoute.y, west, costFromEast));
                    seats.Add(new Loc(possibleRoute.x, possibleRoute.y));
                }
                if (costFromNorth + 1001 == possibleRoute.score)
                {
                    possibleRoutes.Push(new Position(possibleRoute.x, possibleRoute.y-1, south, costFromNorth));
                    seats.Add(new Loc(possibleRoute.x, possibleRoute.y));
                }
                if (costFromSouth + 1001 == possibleRoute.score)
                {
                    possibleRoutes.Push(new Position(possibleRoute.x, possibleRoute.y+1, north, costFromSouth));
                    seats.Add(new Loc(possibleRoute.x, possibleRoute.y));
                }
            }

            if (possibleRoute.dir == south)
            {
                if (costFromNorth + 1 == possibleRoute.score)
                {
                    possibleRoutes.Push(new Position(possibleRoute.x, possibleRoute.y-1, south, costFromNorth));
                    seats.Add(new Loc(possibleRoute.x, possibleRoute.y));
                }
                if (costFromWest + 1001 == possibleRoute.score)
                {
                    possibleRoutes.Push(new Position(possibleRoute.x-1, possibleRoute.y, east, costFromWest));
                    seats.Add(new Loc(possibleRoute.x, possibleRoute.y));
                }
                if (costFromEast + 1001 == possibleRoute.score)
                {
                    possibleRoutes.Push(new Position(possibleRoute.x+1, possibleRoute.y, west, costFromEast));
                    seats.Add(new Loc(possibleRoute.x, possibleRoute.y));
                }
            }
            
            if (possibleRoute.dir == east)
            {
                if (costFromWest + 1 == possibleRoute.score)
                {
                    possibleRoutes.Push(new Position(possibleRoute.x-1, possibleRoute.y, east, costFromWest));
                    seats.Add(new Loc(possibleRoute.x, possibleRoute.y));
                }
                if (costFromNorth + 1001 == possibleRoute.score)
                {
                    possibleRoutes.Push(new Position(possibleRoute.x, possibleRoute.y-1, south, costFromNorth));
                    seats.Add(new Loc(possibleRoute.x, possibleRoute.y));
                }
                if (costFromSouth + 1001 == possibleRoute.score)
                {
                    possibleRoutes.Push(new Position(possibleRoute.x, possibleRoute.y+1, north, costFromSouth));
                    seats.Add(new Loc(possibleRoute.x, possibleRoute.y));
                }
            }
        }

        
        return seats.Count;

        void TryNorth(Position current)
        {
            if (!walls[(current.y - 1) * width + current.x])
            {
               var cost = current.dir switch
                {
                    north => 1,
                    east => 1001,
                    west => 1001,
                    _ => -1
                };
                if (cost != -1)
                {
                    var currentCost = seen[(((current.y * width) + current.x) << 2) + current.dir];
                    var key = ((((current.y-1) * width) + current.x) << 2) + north;
                    if (seen.TryGetValue(key, out var previousScore))
                    {
                        if (currentCost + cost < previousScore)
                        {
                            routesToTry.Push(new Position(current.x, current.y - 1, north, current.score + cost));
                            seen[key] = currentCost + cost;
                        }
                    }
                    else
                    {
                        routesToTry.Push(new Position(current.x, current.y - 1, north, current.score + cost));
                        seen[key] = currentCost + cost;
                    }
                }
            }
        }
        
        void TrySouth(Position current)
        {
            if (!walls[(current.y + 1) * width + current.x])
            {
                var cost = current.dir switch
                {
                    east => 1001,
                    south => 1,
                    west => 1001,
                    _ => -1
                };
                if (cost != -1)
                {
                    var currentCost = seen[(((current.y * width) + current.x) << 2) + current.dir];
                    var key = ((((current.y+1) * width) + current.x) << 2) + south;
                    if (seen.TryGetValue(key, out var previousScore))
                    {
                        if (currentCost + cost < previousScore)
                        {
                            routesToTry.Push(new Position(current.x, current.y + 1, south, current.score + cost));
                            seen[key] = currentCost + cost;
                        }
                    }
                    else
                    {
                        routesToTry.Push(new Position(current.x, current.y + 1, south, current.score + cost));
                        seen[key] = currentCost + cost;
                    }
                }
            }
        }
        
        void TryWest(Position current)
        {
            if (!walls[(current.y) * width + current.x-1])
            {
                var cost = current.dir switch
                {
                    north => 1001,
                    south => 1001,
                    west => 1,
                    _ => -1
                };
                if (cost != -1)
                {
                    var currentCost = seen[(((current.y * width) + current.x) << 2) + current.dir];
                    var key = ((((current.y) * width) + (current.x-1) << 2) + west);
                    if (seen.TryGetValue(key, out var previousScore))
                    {
                        if (currentCost + cost < previousScore)
                        {
                            routesToTry.Push(new Position(current.x-1, current.y , west, current.score + cost));
                            seen[key] = currentCost + cost;
                        }
                    }
                    else
                    {
                        routesToTry.Push(new Position(current.x-1, current.y , west, current.score + cost));
                        seen[key] = currentCost + cost;
                    }
                }
            }
        }
        
        void TryEast(Position current)
        {
            if (!walls[(current.y) * width + current.x+1])
            {
                var cost = current.dir switch
                {
                    north => 1001,
                    east => 1,
                    south => 1001,
                    _ => -1
                };
                if (cost != -1)
                {
                    var currentCost = seen[(((current.y * width) + current.x) << 2) + current.dir];
                    var key = ((((current.y) * width) + (current.x+1) << 2) + east);
                    if (seen.TryGetValue(key, out var previousScore))
                    {
                        if (currentCost + cost < previousScore)
                        {
                            routesToTry.Push(new Position(current.x+1, current.y , east, current.score + cost));
                            seen[key] = currentCost + cost;
                        }
                    }
                    else
                    {
                        routesToTry.Push(new Position(current.x+1, current.y , east, current.score + cost));
                        seen[key] = currentCost + cost;
                    }
                }
            }
        }
    }
}