namespace AdventOfCode2024.Solutions;

public class Day21_Part2
{
    public long Part2(string filename, int numberOfRobots)
    {
        var codes = File.ReadAllLines(filename);

        var cache = new Dictionary<string, long>[numberOfRobots+1];
        for (int i = 0; i <= numberOfRobots; i++)
            cache[i] = new Dictionary<string, long>();
        
        var total = 0L;
        foreach (var code in codes)
            total += SolveCode(code, numberOfRobots, cache);

        return total;
    }

    private long SolveCode(string code, int numberOfRobots, Dictionary<string, long>[] cache)
    {
        var numericCodes = new List<string>();
        SolveNumericalCode(code, "", 11, numericCodes);

        // Are all the possibilities, we want the cheapest one
        var shortestSequence = long.MaxValue;
        foreach (var numericCode in numericCodes)
        {
            var length = Cost(numericCode, numberOfRobots, cache);
            shortestSequence = Math.Min(shortestSequence, length);
        }

        var numericPart = 0;
        for (var i = 0; i < code.Length; i++)
        {
            if (char.IsDigit(code[i]))
                numericPart = numericPart * 10 + code[i] - '0';
        }


        return shortestSequence * numericPart;
    }
    
    private long Cost(string numericCode, int robotNumber, Dictionary<string, long>[] cache)
    {
        if (cache[robotNumber].TryGetValue(numericCode, out var c))
            return c;
        
        if (robotNumber == 1) return numericCode.Length;

        var cost = 0L;
        var currentlyAt = 3;
        for (var i = 0; i < numericCode.Length; i++)
        {
            var moveTo = numericCode[i] switch
            {
                '^' => 4,
                'A' => 3,
                '<' => 2,
                'V' => 1,
                '>' => 0,
                _ => 5
            };
            
            var map = mapping[currentlyAt][moveTo];
            var lowestCost = long.MaxValue;
            foreach (var possible in map)
            {
                lowestCost = Math.Min(lowestCost, Cost(possible, robotNumber-1, cache));
            }
            cost += lowestCost;
            currentlyAt = moveTo;
        }

        cache[robotNumber][numericCode] = cost;

        return cost;
    }

    private List<string>[][] mapping = new[]
    {
        //0
        new[]
        {
            new List<string> { "A" },
            new List<string> { "<A" },
            new List<string> { "<<A" },
            new List<string> { "^A" },
            new List<string> { "^<A", "<^A" }
        },

        //1
        new[]
        {
            new List<string> { ">A" },
            new List<string> { "A" },
            new List<string> { "<A" },
            new List<string> { ">^A", "^>A" },
            new List<string> { "^A" }
        },

        //2
        new[]
        {
            new List<string> { ">>A" },
            new List<string> { ">A" },
            new List<string> { "A" },
            new List<string> { ">>^A", ">^>A" },
            new List<string> { ">^A" }
        },

        //3
        new[]
        {
            new List<string> { "VA" },
            new List<string> { "V<A", "<VA" },
            new List<string> { "<V<A", "V<<A" },
            new List<string> { "A" },
            new List<string> { "<A" }
        },

        //4
        new[]
        {
            new List<string> { "V>A", ">VA" },
            new List<string> { "VA" },
            new List<string> { "V<A" },
            new List<string> { ">A" },
            new List<string> { "A" }
        }
    };

    private void LengthOfShortestSequence(string code, string path, int location,
        ref int shortestPathLength)
    {
        if (code == "")
        {
            shortestPathLength = path.Length;
            return;
        }

        if (path.Length >= shortestPathLength)
            return;

        var symbol = code[0] switch
        {
            '^' => 4,
            'A' => 3,
            '<' => 2,
            'V' => 1,
            '>' => 0,
            _ => 5
        };

        var map = mapping[location][symbol];
        foreach (var possible in map)
        {
            LengthOfShortestSequence(code[1..], path + possible, symbol, ref shortestPathLength);
        }
    }


    private void SolveDirectionalCode(string code, string path, int location, HashSet<string> paths)
    {
        if (code == "")
        {
            paths.Add(path);
            return;
        }

        var symbol = code[0] switch
        {
            '^' => 4,
            'A' => 3,
            '<' => 2,
            'V' => 1,
            '>' => 0,
            _ => 5
        };

        var map = mapping[location][symbol];
        foreach (var possible in map)
        {
            SolveDirectionalCode(code[1..], path + possible, symbol, paths);
        }
    }


    private void SolveNumericalCode(string code, string path, int location, List<string> paths)
    {
        if (code == "")
        {
            paths.Add(path);
            return;
        }

        // /*
        //  *  7(0)  8(1) 9(2)
        //  *  4(3)  5(4) 6(5)
        //  *  1(6)  2(7) 3(8)
        //  *   (9)  0(10). A(11)
        //  */
        
        var symbol = code[0] switch
        {
            'A' => 11,
            '0' => 10,
            '1' => 6,
            '2' => 7,
            '3' => 8,
            '4' => 3,
            '5' => 4,
            '6' => 5,
            '7' => 0,
            '8' => 1,
            '9' => 2,
            _ => 9
        };

        var map = digitMapping[location][symbol];
        foreach (var possible in map)
        {
            SolveNumericalCode(code[1..], path + possible, symbol, paths);
        }
    }


    private string[][][] digitMapping = new[]
    {
// From 0
new[]
{
  new string[] { "A", },   // To: 0
  new string[] { ">A", },   // To: 1
  new string[] { ">>A", },   // To: 2
  new string[] { "VA", },   // To: 3
  new string[] { "V>A", ">VA", },   // To: 4
  new string[] { "V>>A", ">V>A", ">>VA", },   // To: 5
  new string[] { "VVA", },   // To: 6
  new string[] { "VV>A", "V>VA", ">VVA", },   // To: 7
  new string[] { "VV>>A", "V>V>A", "V>>VA", ">VV>A", ">V>VA", ">>VVA", },   // To: 8
  new string[] { },   // To: 9
  new string[] { "VV>VA", "V>VVA", ">VVVA", },   // To: 10
  new string[] { "VV>V>A", "VV>>VA", "V>VV>A", "V>V>VA", "V>>VVA", ">VVV>A", ">VV>VA", ">V>VVA", ">>VVVA", },   // To: 11
},
// From 1
new[]
{
  new string[] { "<A", },   // To: 0
  new string[] { "A", },   // To: 1
  new string[] { ">A", },   // To: 2
  new string[] { "V<A", "<VA", },   // To: 3
  new string[] { "VA", },   // To: 4
  new string[] { "V>A", ">VA", },   // To: 5
  new string[] { "VV<A", "V<VA", "<VVA", },   // To: 6
  new string[] { "VVA", },   // To: 7
  new string[] { "VV>A", "V>VA", ">VVA", },   // To: 8
  new string[] { },   // To: 9
  new string[] { "VVVA", },   // To: 10
  new string[] { "VVV>A", "VV>VA", "V>VVA", ">VVVA", },   // To: 11
},
// From 2
new[]
{
  new string[] { "<<A", },   // To: 0
  new string[] { "<A", },   // To: 1
  new string[] { "A", },   // To: 2
  new string[] { "V<<A", "<V<A", "<<VA", },   // To: 3
  new string[] { "V<A", "<VA", },   // To: 4
  new string[] { "VA", },   // To: 5
  new string[] { "VV<<A", "V<V<A", "V<<VA", "<VV<A", "<V<VA", "<<VVA", },   // To: 6
  new string[] { "VV<A", "V<VA", "<VVA", },   // To: 7
  new string[] { "VVA", },   // To: 8
  new string[] { },   // To: 9
  new string[] { "VVV<A", "VV<VA", "V<VVA", "<VVVA", },   // To: 10
  new string[] { "VVVA", },   // To: 11
},
// From 3
new[]
{
  new string[] { "^A", },   // To: 0
  new string[] { "^>A", ">^A", },   // To: 1
  new string[] { "^>>A", ">^>A", ">>^A", },   // To: 2
  new string[] { "A", },   // To: 3
  new string[] { ">A", },   // To: 4
  new string[] { ">>A", },   // To: 5
  new string[] { "VA", },   // To: 6
  new string[] { "V>A", ">VA", },   // To: 7
  new string[] { "V>>A", ">V>A", ">>VA", },   // To: 8
  new string[] { },   // To: 9
  new string[] { "V>VA", ">VVA", },   // To: 10
  new string[] { "V>V>A", "V>>VA", ">VV>A", ">V>VA", ">>VVA", },   // To: 11
},
// From 4
new[]
{
  new string[] { "^<A", "<^A", },   // To: 0
  new string[] { "^A", },   // To: 1
  new string[] { "^>A", ">^A", },   // To: 2
  new string[] { "<A", },   // To: 3
  new string[] { "A", },   // To: 4
  new string[] { ">A", },   // To: 5
  new string[] { "V<A", "<VA", },   // To: 6
  new string[] { "VA", },   // To: 7
  new string[] { "V>A", ">VA", },   // To: 8
  new string[] { },   // To: 9
  new string[] { "VVA", },   // To: 10
  new string[] { "VV>A", "V>VA", ">VVA", },   // To: 11
},
// From 5
new[]
{
  new string[] { "^<<A", "<^<A", "<<^A", },   // To: 0
  new string[] { "^<A", "<^A", },   // To: 1
  new string[] { "^A", },   // To: 2
  new string[] { "<<A", },   // To: 3
  new string[] { "<A", },   // To: 4
  new string[] { "A", },   // To: 5
  new string[] { "V<<A", "<V<A", "<<VA", },   // To: 6
  new string[] { "V<A", "<VA", },   // To: 7
  new string[] { "VA", },   // To: 8
  new string[] { },   // To: 9
  new string[] { "VV<A", "V<VA", "<VVA", },   // To: 10
  new string[] { "VVA", },   // To: 11
},
// From 6
new[]
{
  new string[] { "^^A", },   // To: 0
  new string[] { "^^>A", "^>^A", ">^^A", },   // To: 1
  new string[] { "^^>>A", "^>^>A", "^>>^A", ">^^>A", ">^>^A", ">>^^A", },   // To: 2
  new string[] { "^A", },   // To: 3
  new string[] { "^>A", ">^A", },   // To: 4
  new string[] { "^>>A", ">^>A", ">>^A", },   // To: 5
  new string[] { "A", },   // To: 6
  new string[] { ">A", },   // To: 7
  new string[] { ">>A", },   // To: 8
  new string[] { },   // To: 9
  new string[] { ">VA", },   // To: 10
  new string[] { ">V>A", ">>VA", },   // To: 11
},
// From 7
new[]
{
  new string[] { "^^<A", "^<^A", "<^^A", },   // To: 0
  new string[] { "^^A", },   // To: 1
  new string[] { "^^>A", "^>^A", ">^^A", },   // To: 2
  new string[] { "^<A", "<^A", },   // To: 3
  new string[] { "^A", },   // To: 4
  new string[] { "^>A", ">^A", },   // To: 5
  new string[] { "<A", },   // To: 6
  new string[] { "A", },   // To: 7
  new string[] { ">A", },   // To: 8
  new string[] { },   // To: 9
  new string[] { "VA", },   // To: 10
  new string[] { "V>A", ">VA", },   // To: 11
},
// From 8
new[]
{
  new string[] { "^^<<A", "^<^<A", "^<<^A", "<^^<A", "<^<^A", "<<^^A", },   // To: 0
  new string[] { "^^<A", "^<^A", "<^^A", },   // To: 1
  new string[] { "^^A", },   // To: 2
  new string[] { "^<<A", "<^<A", "<<^A", },   // To: 3
  new string[] { "^<A", "<^A", },   // To: 4
  new string[] { "^A", },   // To: 5
  new string[] { "<<A", },   // To: 6
  new string[] { "<A", },   // To: 7
  new string[] { "A", },   // To: 8
  new string[] { },   // To: 9
  new string[] { "V<A", "<VA", },   // To: 10
  new string[] { "VA", },   // To: 11
},
// From 9
new[]
{
  new string[] { },   // To: 0
  new string[] { },   // To: 1
  new string[] { },   // To: 2
  new string[] { },   // To: 3
  new string[] { },   // To: 4
  new string[] { },   // To: 5
  new string[] { },   // To: 6
  new string[] { },   // To: 7
  new string[] { },   // To: 8
  new string[] { },   // To: 9
  new string[] { },   // To: 10
  new string[] { },   // To: 11
},
// From 10
new[]
{
  new string[] { "^^^<A", "^^<^A", "^<^^A", },   // To: 0
  new string[] { "^^^A", },   // To: 1
  new string[] { "^^^>A", "^^>^A", "^>^^A", ">^^^A", },   // To: 2
  new string[] { "^^<A", "^<^A", },   // To: 3
  new string[] { "^^A", },   // To: 4
  new string[] { "^^>A", "^>^A", ">^^A", },   // To: 5
  new string[] { "^<A", },   // To: 6
  new string[] { "^A", },   // To: 7
  new string[] { "^>A", ">^A", },   // To: 8
  new string[] { },   // To: 9
  new string[] { "A", },   // To: 10
  new string[] { ">A", },   // To: 11
},
// From 11
new[]
{
  new string[] { "^^^<<A", "^^<^<A", "^^<<^A", "^<^^<A", "^<^<^A", "^<<^^A", "<^^^<A", "<^^<^A", "<^<^^A", },   // To: 0
  new string[] { "^^^<A", "^^<^A", "^<^^A", "<^^^A", },   // To: 1
  new string[] { "^^^A", },   // To: 2
  new string[] { "^^<<A", "^<^<A", "^<<^A", "<^^<A", "<^<^A", },   // To: 3
  new string[] { "^^<A", "^<^A", "<^^A", },   // To: 4
  new string[] { "^^A", },   // To: 5
  new string[] { "^<<A", "<^<A", },   // To: 6
  new string[] { "^<A", "<^A", },   // To: 7
  new string[] { "^A", },   // To: 8
  new string[] { },   // To: 9
  new string[] { "<A", },   // To: 10
  new string[] { "A", },   // To: 11
},


    };
}