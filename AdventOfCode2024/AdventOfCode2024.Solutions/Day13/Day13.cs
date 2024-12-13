namespace AdventOfCode2024.Solutions;

public class Day13
{
    public int Part1(string filename)
    {

        var lines = File.ReadAllLines(filename);
        var state = 0;
        
        int AX = 0;
        int AY = 0;
        int BX = 0;
        int BY = 0;
        int PX = 0;
        int PY = 0;
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
                        Console.WriteLine($"{ATimes}, {BTimes}");

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
}