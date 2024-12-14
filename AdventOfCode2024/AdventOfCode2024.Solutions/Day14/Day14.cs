using System.Runtime.CompilerServices;

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
        var details = new int[robots.Length*4];
        
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
            details[lineNumber] = sign * x;

            i+=2;
            sign = line[i] == '-' ? -1 : 1;
            if (line[i] == '-') i++;
            var y = line[i] - '0';
            while ( line[i+1] != ' ')
            {
                i++;
                y = y * 10 + line[i] - '0';
            }
            details[lineNumber+2] = sign * y;

            i+=4;
            sign = line[i] == '-' ? -1 : 1;
            if (line[i] == '-') i++;
            var vx = line[i] - '0';
            while ( line[i+1] != ',')
            {
                i++;
                vx = vx * 10 + line[i] - '0';
            }
            details[lineNumber+1] = sign * vx;

            i+=2;
            sign = line[i] == '-' ? -1 : 1;
            if (line[i] == '-') i++;
            var vy = line[i] - '0';
            while ( i+1 < line.Length)
            {
                i++;
                vy = vy * 10 + line[i] - '0';
            }
            details[lineNumber+3] = sign * vy;

            lineNumber += 4;
        }

        const int width = 101;
        const int height = 103;
       
        var time = 0;
        var board = new bool[width * height];
        var highestEntropy = 0;
        var bestTime = 0;
        while (time < (width * height))
        {
            time++;
            Array.Clear(board);

            var i = 0;
            for (var robot = 0; robot < robots.Length; robot++)
            {
                var x = mod(details[i] + (time * details[i+1]), width);
                var y = mod(details[i+2] + (time * details[i+3]), height);
                board[(y * width) + x] = true;
                i += 4;
            }

            var entropy = 0;
            for (var j = 0; j < (width * height)-1; j++)
            {
                if (board[j] && board[j + 1])
                    entropy++;
            }

            if (entropy > highestEntropy)
            {
                highestEntropy = entropy;
                bestTime = time;
            }
        }
        return bestTime;
        
        void Display()
        {
            for (var row = 0; row < height; row++)
            {
                Console.WriteLine();
                for (var column = 0; column < width; column++)
                {
                    Console.Write(board[(row*width)+column] ? 'X' : ' ');
                }
            }
        }
        
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    int mod(int x, int m) {
        return (x%m + m)%m;
    }
    

}