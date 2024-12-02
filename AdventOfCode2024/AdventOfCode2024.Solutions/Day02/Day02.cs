namespace AdventOfCode2024.Solutions;

public class Day02
{
    public int Part1(string filename)
    {
        var lines = File.ReadAllText(filename).AsSpan();
        var result = 0;
        foreach (var lineRange in lines.Split('\n'))
        {
            var i = 0;
            var increasing = false;
            var previousValue = 0;
            result++;
            foreach (var valRange in lines[lineRange].Split(' '))
            {
                var currentVal = int.Parse(lines[lineRange][valRange]);

                if (i > 0)
                {
                    var diff = currentVal - previousValue;
                    if (diff == 0 || Math.Abs(diff) > 3)
                    {
                        result--;
                        break;
                    }

                    if (i == 1)
                        increasing = diff > 0;
                    else if (increasing && diff < 0)
                    {
                        result--;
                        break;
                    }
                    else if (!increasing && diff > 0)
                    {
                        result--;
                        break;
                    }
                }

                previousValue = currentVal;
                i++;
            }
        }

        return result;
    }
    
    
    public int Part2(string filename)
    {
        var lines = File.ReadAllText(filename).AsSpan();
        var result = 0;
        foreach (var lineRange in lines.Split('\n'))
        {
            if (CountErrors(lines, lineRange, increasing: true) <= 1)
                result++;
            else if (CountErrors(lines, lineRange, increasing: false) <= 1)
                result++;
        }

        return result;
    }

    private static int CountErrors(ReadOnlySpan<char> lines, Range lineRange, bool increasing)
    {
        bool Ok(int left, int rhs)
        {
            if (increasing)
                return (rhs - left) is > 0 and <= 3;
            else
                return (left - rhs) is > 0 and <= 3;
        }
        
        var n1 = -1;
        var n2 = -1;
        var n3 = -1;
        var i = 0;
        var errorCount = 0;
        foreach (var valRange in lines[lineRange].Split(' '))
        {
            var n = int.Parse(lines[lineRange][valRange]);

            if (i >= 2)
            {
                // We have two previous values to check.
                if (!Ok(n2, n1))
                {
                    // We have a problem
                    if (errorCount > 0)
                    {
                        // We have already has a problem
                        errorCount++;
                        break;
                    }
                    else
                    {
                        // Can we remove either n2 or n1 and fix the problem
                        if (Ok(n2, n))
                        {
                            // We can remove n1
                            errorCount = 1;
                            n1 = n2;
                            n2 = n3;
                        }
                        else if ((n3 == -1 || Ok(n3, n1)) && Ok(n1, n))
                        {
                            // We can remove n2
                            errorCount = 1;
                            n2 = n3;
                        }

                        else
                        {
                            // Not fixable
                            errorCount = 2;
                            break;
                        }
                    }
                }
                    
            }
                
            n3 = n2;
            n2 = n1;
            n1 = n;
            i++;
        }

        if (!Ok(n2, n1))
            errorCount++;
        
        return errorCount;
    }

}