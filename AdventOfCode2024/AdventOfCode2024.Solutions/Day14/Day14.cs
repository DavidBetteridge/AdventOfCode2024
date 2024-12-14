namespace AdventOfCode2024.Solutions;

public class Day14
{
    public int Part1(string filename)
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
            
            // const int width = 11;
            // const int height = 7;
            // const int midX = 5;
            // const int midY = 3;
            
            const int width = 101;
            const int height = 103;
            const int midX = 50;
            const int midY = 51;
            
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
    
    int mod(int x, int m) {
        return (x%m + m)%m;
    }
}