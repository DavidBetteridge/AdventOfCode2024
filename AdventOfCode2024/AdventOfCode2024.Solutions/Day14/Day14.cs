namespace AdventOfCode2024.Solutions;

public class Day14
{
    public int Part1(string filename, int width, int height)
    {
        var robots = File.ReadAllLines(filename);
        var q1 = 0;
        var q2 = 0;
        var q3 = 0;
        var q4 = 0;

        foreach (var line in robots)
        {
            var i = 2;
            
            var sign = line[i] == '-' ? -1 : 1;
            if (line[i] == '-') i++;
            var x = line[i] - '0';
            while ( line[i+1] != ',')
            {
                i++;
                x = (x * 10) + line[i] - '0';
            }
            x = sign * x;

            i+=2;
            sign = line[i] == '-' ? -1 : 1;
            if (line[i] == '-') i++;
            var y = line[i] - '0';
            while ( line[i+1] != ' ')
            {
                i++;
                y = y * 10 + line[i] - '0';
            }
            y = sign * y;

            i+=4;
            sign = line[i] == '-' ? -1 : 1;
            if (line[i] == '-') i++;
            var vx = line[i] - '0';
            while ( line[i+1] != ',')
            {
                i++;
                vx = vx * 10 + line[i] - '0';
            }
            vx = sign * vx;

            i+=2;
            sign = line[i] == '-' ? -1 : 1;
            if (line[i] == '-') i++;
            var vy = line[i] - '0';
            while ( i+1 < line.Length)
            {
                i++;
                vy = vy * 10 + line[i] - '0';
            }
            vy = sign * vy;

            if (line != $"p={x},{y} v={vx},{vy}")
            {
                Console.WriteLine("Parse error");
            }

            var midX = (width-1)/2;
            var midY = (height-1)/2;
            
            x = mod(x + (100 * vx), width);
            y = mod(y + (100 * vy), height);

            if (x < midX && y < midY)
                q1 ++;
            
            if (x > midX && y < midY)
                q2++;

            if (x < midX && y > midY)
                q3++;

            if (x > midX && y > midY)
                q4++;
            
        }
        

        return q1 * q2 * q3 * q4;
    }
    
    public int Part2(string filename)
    {
        var robots = File.ReadAllLines(filename);

        var xs = new int[robots.Length];
        var ys = new int[robots.Length];
        var vxs = new int[robots.Length];
        var vys = new int[robots.Length];

        var lineNumber = 0;
        foreach (var line in robots)
        {
            var i = 2;
            
            var sign = line[i] == '-' ? -1 : 1;
            if (line[i] == '-') i++;
            var x = line[i] - '0';
            while ( line[i+1] != ',')
            {
                i++;
                x = (x * 10) + line[i] - '0';
            }
            xs[lineNumber] = sign * x;

            i+=2;
            sign = line[i] == '-' ? -1 : 1;
            if (line[i] == '-') i++;
            var y = line[i] - '0';
            while ( line[i+1] != ' ')
            {
                i++;
                y = y * 10 + line[i] - '0';
            }
            ys[lineNumber] = sign * y;

            i+=4;
            sign = line[i] == '-' ? -1 : 1;
            if (line[i] == '-') i++;
            var vx = line[i] - '0';
            while ( line[i+1] != ',')
            {
                i++;
                vx = vx * 10 + line[i] - '0';
            }
            vxs[lineNumber] = sign * vx;

            i+=2;
            sign = line[i] == '-' ? -1 : 1;
            if (line[i] == '-') i++;
            var vy = line[i] - '0';
            while ( i+1 < line.Length)
            {
                i++;
                vy = vy * 10 + line[i] - '0';
            }
            vys[lineNumber] = sign * vy;

            lineNumber++;
        }

        const int width = 101;
        const int height = 103;
        // const int width = 11;
        // const int height = 7;
        
        var time = 0;
        while (true)
        {
            time++;
            var board = new bool[width, height];

            for (var robot = 0; robot < robots.Length; robot++)
            {
                var x = mod(xs[robot] + (time * vxs[robot]), width);
                var y = mod(ys[robot] + (time * vys[robot]), height);
                board[x, y] = true;
            }
            
            // Can we find space 5 space
            for (var row = 3; row < height; row++)
            {
                for (var column = 0; column < width-7; column++)
                {
                    //.XXXXX.
                    if ( !board[column, row] && board[column+1, row] && board[column+2, row] && board[column+3, row] && board[column+4, row] && board[column+5, row] && !board[column+6, row])
                    {
                        //..XXX..
                        if (!board[column, row - 1] && !board[column + 1, row - 1] && board[column + 2, row - 1] &&
                            board[column + 3, row - 1] && board[column + 4, row - 1] && !board[column + 5, row - 1] &&
                            !board[column + 6, row - 1])
                        {

                            //..X..
                            if (!board[column, row - 2] && !board[column + 1, row - 2] && !board[column + 2, row - 2] &&
                                board[column + 3, row - 2] && !board[column + 4, row - 2] &&
                                !board[column + 5, row - 2] &&
                                !board[column + 6, row - 2])
                            {
                                // Display(board);
                                return time;
                            }
                        }
                    }
                }
            }
        }

        void Display(bool[,] board)
        {
            for (var row = 0; row < height; row++)
            {
                Console.WriteLine();
                for (var column = 0; column < width; column++)
                {
                    Console.Write(board[column, row] ? 'X' : ' ');
                }
            }
        }
    }
    
    int mod(int x, int m) {
        return (x%m + m)%m;
    }
}