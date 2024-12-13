using System.Numerics;

namespace AdventOfCode2024.Solutions;

public class Day13
{
    public int Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var state = 0;
        
        var AX = 0;
        var AY = 0;
        var BX = 0;
        var BY = 0;
        var PX = 0;
        var PY = 0;
        var answer = 0;
        
        foreach (var line in lines)
        {
            if (state == 0)
            {
                // Button A: X+94, Y+34
                var rhs = line.Split(": ");
                var parts = rhs[1].Split(", ");
                AX = int.Parse(parts[0][2..]);
                AY = int.Parse(parts[1][2..]);
                state = 1;
            }
            else if (state == 1)
            {
                // Button B: X+22, Y+67
                var rhs = line.Split(": ");
                var parts = rhs[1].Split(", ");
                BX = int.Parse(parts[0][2..]);
                BY = int.Parse(parts[1][2..]);
                state = 2;
            }
            else if (state == 2)
            {
                // Prize: X=8400, Y=5400
                var rhs = line.Split(": ");
                var parts = rhs[1].Split(", ");
                PX = int.Parse(parts[0][2..]);
                PY = int.Parse(parts[1][2..]);
                state = 3;
                
                var bestScore = int.MaxValue;
                for (var ATimes = 0; ATimes < 100; ATimes++)
                {
                    var xLeft = PX - (ATimes * AX);
                    if (xLeft < 0) break;  // overshot
            
                    var yLeft = PY - (ATimes * AY);
                    if (yLeft < 0) break;  // overshot

                    var BTimes = xLeft / BX;
                    if (( xLeft % BX == 0) && ( yLeft == BTimes * BY))
                    {
                        bestScore = Math.Min(bestScore, (3 * ATimes) + BTimes);
                    }
                }

                if (bestScore < int.MaxValue)
                    answer += bestScore;

            }
            else
            {
                //blank line
                state = 0;
            }
        }

        return answer;
    }
    
      public long Part2(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var state = 0;
        
        var AX = 0;
        var AY = 0;
        var BX = 0;
        var BY = 0;
        var PX = 0L;
        var PY = 0L;
        var answer = 0L;
        
        foreach (var line in lines)
        {
            if (state == 0)
            {
                // Button A: X+94, Y+34
                var rhs = line.Split(": ");
                var parts = rhs[1].Split(", ");
                AX = int.Parse(parts[0][2..]);
                AY = int.Parse(parts[1][2..]);
                state = 1;
            }
            else if (state == 1)
            {
                // Button B: X+22, Y+67
                var rhs = line.Split(": ");
                var parts = rhs[1].Split(", ");
                BX = int.Parse(parts[0][2..]);
                BY = int.Parse(parts[1][2..]);
                state = 2;
            }
            else if (state == 2)
            {
                // Prize: X=8400, Y=5400
                var rhs = line.Split(": ");
                var parts = rhs[1].Split(", ");
                PX = int.Parse(parts[0][2..])+10000000000000;
                PY = int.Parse(parts[1][2..])+10000000000000;
                state = 3;

                var matrix = new double[2, 3];
                matrix[0, 0] = AX;
                matrix[0, 1] = BX;
                matrix[0, 2] = PX;
                matrix[1, 0] = AY;
                matrix[1, 1] = BY;
                matrix[1, 2] = PY;
                
                // Normalise the first row.  (1 x x)
                var multiplier = matrix[0, 0];
                matrix[0, 0] = matrix[0, 0] / multiplier;
                matrix[0, 1] = matrix[0, 1] / multiplier;
                matrix[0, 2] = matrix[0, 2] / multiplier;
 
                multiplier = matrix[1, 0];
                matrix[1, 0] = matrix[1, 0] - (matrix[0, 0] * multiplier);
                matrix[1, 1] = matrix[1, 1] - (matrix[0, 1] * multiplier);
                matrix[1, 2] = matrix[1, 2] - (matrix[0, 2] * multiplier);
                
                multiplier = matrix[1, 1];
                matrix[1, 0] = matrix[1, 0] / multiplier;
                matrix[1, 1] = matrix[1, 1] / multiplier;
                matrix[1, 2] = matrix[1, 2] / multiplier;
                
                multiplier = matrix[0, 1];
                matrix[0, 0] = matrix[0, 0] - (matrix[1, 0] * multiplier);
                matrix[0, 1] = matrix[0, 1] - (matrix[1, 1] * multiplier);
                matrix[0, 2] = matrix[0, 2] - (matrix[1, 2] * multiplier);


                var a = (long)Math.Round(matrix[0, 2], 0);
                var b = (long)Math.Round(matrix[1, 2], 0);

                if (((a * AX) + (b * BX) == PX) && ((a * AY) + (b * BY) == PY))
                {
                    var cost = (3 * a) + b;
                    answer += cost;
                }
            }
            else
            {
                //blank line
                state = 0;
            }
        }

        return answer;
    }
}