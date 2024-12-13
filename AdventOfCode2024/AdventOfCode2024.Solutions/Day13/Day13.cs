namespace AdventOfCode2024.Solutions;

public class Day13
{
    public double Part1(string filename)
    {
        var input = File.ReadAllBytes(filename).AsSpan();
        var answer = 0D;

        var i = 0;
        while (i < input.Length)
        {

            //Button A: X+94, Y+34
            i += 12;
            double AX = input[i] - '0';
            while (input[i+1] != ',')
            {
                i++;
                AX = AX * 10 + input[i] - '0';
            }

            i += 5;
            double AY = input[i] - '0';
            while (input[i+1] != '\n')
            {
                i++;
                AY = AY * 10 + input[i] - '0';
            }
            
            // Button B: X+22, Y+67
            i += 14;
            double BX = input[i] - '0';
            while (input[i+1] != ',')
            {
                i++;
                BX = BX * 10 + input[i] - '0';
            }

            i += 5;
            double BY = input[i] - '0';
            while (input[i+1] != '\n')
            {
                i++;
                BY = BY * 10 + input[i] - '0';
            }
            
            // Prize: X=8400, Y=5400
            i += 11;
            double PX = input[i] - '0';
            while (input[i+1] != ',')
            {
                i++;
                PX = PX * 10 + input[i] - '0';
            }

            i += 5;
            double PY = input[i] - '0';
            while (i+1 < input.Length && input[i+1] != '\n')
            {
                i++;
                PY = PY * 10 + input[i] - '0';
            }

            i += 3;  // Skip blank line
            
            var a = Math.Round(PX / AX - (PY - PX / AX * AY) / (BY - BX / AX * AY) * (BX / AX), 0);

            var b = (PX - (a * AX)) / BX;
               

            if (((a * AX) + (b * BX) == PX) && ((a * AY) + (b * BY) == PY))
            {
                var cost = (3 * a) + b;
                answer += cost;
            }
        }

        return answer;
    }
    
    public double Part2(string filename)
    {
        var input = File.ReadAllBytes(filename).AsSpan();
        var answer = 0D;

        var i = 0;
        while (i < input.Length)
        {

            //Button A: X+94, Y+34
            i += 12;
            double AX = input[i] - '0';
            while (input[i+1] != ',')
            {
                i++;
                AX = AX * 10 + input[i] - '0';
            }

            i += 5;
            double AY = input[i] - '0';
            while (input[i+1] != '\n')
            {
                i++;
                AY = AY * 10 + input[i] - '0';
            }
            
            // Button B: X+22, Y+67
            i += 14;
            double BX = input[i] - '0';
            while (input[i+1] != ',')
            {
                i++;
                BX = BX * 10 + input[i] - '0';
            }

            i += 5;
            double BY = input[i] - '0';
            while (input[i+1] != '\n')
            {
                i++;
                BY = BY * 10 + input[i] - '0';
            }
            
            // Prize: X=8400, Y=5400
            i += 11;
            double PX = input[i] - '0';
            while (input[i+1] != ',')
            {
                i++;
                PX = PX * 10 + input[i] - '0';
            }
            PX += 10000000000000;

            i += 5;
            double PY = input[i] - '0';
            while (i+1 < input.Length && input[i+1] != '\n')
            {
                i++;
                PY = PY * 10 + input[i] - '0';
            }
            PY += 10000000000000;

            i += 3;  // Skip blank line
            
            var a = Math.Round(PX / AX - (PY - PX / AX * AY) / (BY - BX / AX * AY) * (BX / AX), 0);

            var b = Math.Round((PX - (a * AX)) / BX, 0);
               

            if (((a * AX) + (b * BX) == PX) && ((a * AY) + (b * BY) == PY))
            {
                var cost = (3 * a) + b;
                answer += cost;
            }
        }

        return answer;
    }
}