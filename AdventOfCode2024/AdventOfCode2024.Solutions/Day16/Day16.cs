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

        var seen = new HashSet<int>();  // (row * width + col << 2) + dir
        seen.Add((((reindeer.y * width) + reindeer.x) << 2) + reindeer.dir);
        
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

        void TryNorth(Position currently)
        {
            if (!walls[(currently.y - 1) * width + currently.x] &&
                !seen.Contains(((((currently.y-1) * width) + currently.x) << 2) + currently.dir))
            {
                var cost = currently.dir switch
                {
                    north => 0,
                    east => 1000,
                    west => 1000,
                    _ => -1
                };
                if (cost != -1)
                {
                    routesToTry.Push(new Position(currently.x, currently.y - 1, north, currently.score + cost + 1));
                    seen.Add(((((currently.y-1) * width) + currently.x) << 2) + north);
                }
            }
        }
        
        void TrySouth(Position currently)
        {
            if (!walls[(currently.y + 1) * width + currently.x] &&
                !seen.Contains(((((currently.y+1) * width) + currently.x) << 2) + currently.dir))
            {
                var cost = currently.dir switch
                {
                    east => 1000,
                    south => 0,
                    west => 1000,
                    _ => -1
                };
                if (cost != -1)
                {
                    routesToTry.Push(new Position(currently.x, currently.y + 1, south, currently.score + cost + 1));
                    seen.Add(((((currently.y + 1) * width) + currently.x) << 2) + south);
                }
            }
        }
        
        void TryWest(Position currently)
        {
            if (!walls[(currently.y) * width + currently.x-1] &&
                !seen.Contains(((((currently.y) * width) + (currently.x-1)) << 2) + currently.dir))
            {
                var cost = currently.dir switch
                {
                    north => 1000,
                    south => 1000,
                    west => 0,
                    _ => -1
                };
                if (cost != -1)
                {
                    routesToTry.Push(new Position(currently.x - 1, currently.y, west, currently.score + cost + 1));
                    seen.Add(((((currently.y) * width) + (currently.x - 1)) << 2) + west);
                }
            }
        }
        
        void TryEast(Position currently)
        {
            if (!walls[(currently.y) * width + currently.x+1] &&
                !seen.Contains(((((currently.y) * width) + (currently.x+1)) << 2) + currently.dir))
            {
                var cost = currently.dir switch
                {
                    north => 1000,
                    east => 0,
                    south => 1000,
                    _ => -1
                };
                if (cost != -1)
                {
                    routesToTry.Push(new Position(currently.x + 1, currently.y, east, currently.score + cost + 1));
                    seen.Add(((((currently.y) * width) + (currently.x + 1)) << 2) + east);
                }
            }
        }
    }
}