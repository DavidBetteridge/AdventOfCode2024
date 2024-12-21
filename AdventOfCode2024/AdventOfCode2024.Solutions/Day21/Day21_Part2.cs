namespace AdventOfCode2024.Solutions;

public class Day21_Part2
{
    public long Part2(string filename, int numberOfRobots)
    {
        var codes = File.ReadAllLines(filename);

        var cache = new Dictionary<string, long>[numberOfRobots + 1];
        for (var i = 0; i <= numberOfRobots; i++)
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

            var map = _mapping[currentlyAt][moveTo];
            var lowestCost = long.MaxValue;
            foreach (var possible in map)
            {
                lowestCost = Math.Min(lowestCost, Cost(possible, robotNumber - 1, cache));
            }

            cost += lowestCost;
            currentlyAt = moveTo;
        }

        cache[robotNumber][numericCode] = cost;

        return cost;
    }

    private readonly string[][][] _mapping =
    [
        //0
        [
            ["A"],
            ["<A"],
            ["<<A"],
            ["^A"],
            ["^<A", "<^A"]
        ],

        //1
        [
            [">A"],
            ["A"],
            ["<A"],
            [">^A", "^>A"],
            ["^A"]
        ],

        //2
        [
            [">>A"],
            [">A"],
            ["A"],
            [">>^A", ">^>A"],
            [">^A"]
        ],

        //3
        [
            ["VA"],
            ["V<A", "<VA"],
            ["<V<A", "V<<A"],
            ["A"],
            ["<A"]
        ],

        //4
        [
            ["V>A", ">VA"],
            ["VA"],
            ["V<A"],
            [">A"],
            ["A"]
        ]
    ];

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

        var map = _digitMapping[location][symbol];
        foreach (var possible in map)
        {
            SolveNumericalCode(code[1..], path + possible, symbol, paths);
        }
    }


    private readonly string[][][] _digitMapping =
    [
        // From 0
        [
            ["A"], // To: 0
            [">A"], // To: 1
            [">>A"], // To: 2
            ["VA"], // To: 3
            ["V>A", ">VA"], // To: 4
            ["V>>A", ">V>A", ">>VA"], // To: 5
            ["VVA"], // To: 6
            ["VV>A", "V>VA", ">VVA"], // To: 7
            ["VV>>A", "V>V>A", "V>>VA", ">VV>A", ">V>VA", ">>VVA"], // To: 8
            [], // To: 9
            ["VV>VA", "V>VVA", ">VVVA"], // To: 10
            ["VV>V>A", "VV>>VA", "V>VV>A", "V>V>VA", "V>>VVA", ">VVV>A", ">VV>VA", ">V>VVA", ">>VVVA"] // To: 11
        ],
// From 1
        [
            ["<A"], // To: 0
            ["A"], // To: 1
            [">A"], // To: 2
            ["V<A", "<VA"], // To: 3
            ["VA"], // To: 4
            ["V>A", ">VA"], // To: 5
            ["VV<A", "V<VA", "<VVA"], // To: 6
            ["VVA"], // To: 7
            ["VV>A", "V>VA", ">VVA"], // To: 8
            [], // To: 9
            ["VVVA"], // To: 10
            ["VVV>A", "VV>VA", "V>VVA", ">VVVA"] // To: 11
        ],
// From 2
        [
            ["<<A"], // To: 0
            ["<A"], // To: 1
            ["A"], // To: 2
            ["V<<A", "<V<A", "<<VA"], // To: 3
            ["V<A", "<VA"], // To: 4
            ["VA"], // To: 5
            ["VV<<A", "V<V<A", "V<<VA", "<VV<A", "<V<VA", "<<VVA"], // To: 6
            ["VV<A", "V<VA", "<VVA"], // To: 7
            ["VVA"], // To: 8
            [], // To: 9
            ["VVV<A", "VV<VA", "V<VVA", "<VVVA"], // To: 10
            ["VVVA"] // To: 11
        ],
// From 3
        [
            ["^A"], // To: 0
            ["^>A", ">^A"], // To: 1
            ["^>>A", ">^>A", ">>^A"], // To: 2
            ["A"], // To: 3
            [">A"], // To: 4
            [">>A"], // To: 5
            ["VA"], // To: 6
            ["V>A", ">VA"], // To: 7
            ["V>>A", ">V>A", ">>VA"], // To: 8
            [], // To: 9
            ["V>VA", ">VVA"], // To: 10
            ["V>V>A", "V>>VA", ">VV>A", ">V>VA", ">>VVA"] // To: 11
        ],
// From 4
        [
            ["^<A", "<^A"], // To: 0
            ["^A"], // To: 1
            ["^>A", ">^A"], // To: 2
            ["<A"], // To: 3
            ["A"], // To: 4
            [">A"], // To: 5
            ["V<A", "<VA"], // To: 6
            ["VA"], // To: 7
            ["V>A", ">VA"], // To: 8
            [], // To: 9
            ["VVA"], // To: 10
            ["VV>A", "V>VA", ">VVA"] // To: 11
        ],
// From 5
        [
            ["^<<A", "<^<A", "<<^A"], // To: 0
            ["^<A", "<^A"], // To: 1
            ["^A"], // To: 2
            ["<<A"], // To: 3
            ["<A"], // To: 4
            ["A"], // To: 5
            ["V<<A", "<V<A", "<<VA"], // To: 6
            ["V<A", "<VA"], // To: 7
            ["VA"], // To: 8
            [], // To: 9
            ["VV<A", "V<VA", "<VVA"], // To: 10
            ["VVA"] // To: 11
        ],
// From 6
        [
            ["^^A"], // To: 0
            ["^^>A", "^>^A", ">^^A"], // To: 1
            ["^^>>A", "^>^>A", "^>>^A", ">^^>A", ">^>^A", ">>^^A"], // To: 2
            ["^A"], // To: 3
            ["^>A", ">^A"], // To: 4
            ["^>>A", ">^>A", ">>^A"], // To: 5
            ["A"], // To: 6
            [">A"], // To: 7
            [">>A"], // To: 8
            [], // To: 9
            [">VA"], // To: 10
            [">V>A", ">>VA"] // To: 11
        ],
// From 7
        [
            ["^^<A", "^<^A", "<^^A"], // To: 0
            ["^^A"], // To: 1
            ["^^>A", "^>^A", ">^^A"], // To: 2
            ["^<A", "<^A"], // To: 3
            ["^A"], // To: 4
            ["^>A", ">^A"], // To: 5
            ["<A"], // To: 6
            ["A"], // To: 7
            [">A"], // To: 8
            [], // To: 9
            ["VA"], // To: 10
            ["V>A", ">VA"] // To: 11
        ],
// From 8
        [
            ["^^<<A", "^<^<A", "^<<^A", "<^^<A", "<^<^A", "<<^^A"], // To: 0
            ["^^<A", "^<^A", "<^^A"], // To: 1
            ["^^A"], // To: 2
            ["^<<A", "<^<A", "<<^A"], // To: 3
            ["^<A", "<^A"], // To: 4
            ["^A"], // To: 5
            ["<<A"], // To: 6
            ["<A"], // To: 7
            ["A"], // To: 8
            [], // To: 9
            ["V<A", "<VA"], // To: 10
            ["VA"] // To: 11
        ],
// From 9
        [
            [], // To: 0
            [], // To: 1
            [], // To: 2
            [], // To: 3
            [], // To: 4
            [], // To: 5
            [], // To: 6
            [], // To: 7
            [], // To: 8
            [], // To: 9
            [], // To: 10
            [] // To: 11
        ],
// From 10
        [
            ["^^^<A", "^^<^A", "^<^^A"], // To: 0
            ["^^^A"], // To: 1
            ["^^^>A", "^^>^A", "^>^^A", ">^^^A"], // To: 2
            ["^^<A", "^<^A"], // To: 3
            ["^^A"], // To: 4
            ["^^>A", "^>^A", ">^^A"], // To: 5
            ["^<A"], // To: 6
            ["^A"], // To: 7
            ["^>A", ">^A"], // To: 8
            [], // To: 9
            ["A"], // To: 10
            [">A"] // To: 11
        ],
// From 11
        [
            ["^^^<<A", "^^<^<A", "^^<<^A", "^<^^<A", "^<^<^A", "^<<^^A", "<^^^<A", "<^^<^A", "<^<^^A"], // To: 0
            ["^^^<A", "^^<^A", "^<^^A", "<^^^A"], // To: 1
            ["^^^A"], // To: 2
            ["^^<<A", "^<^<A", "^<<^A", "<^^<A", "<^<^A"], // To: 3
            ["^^<A", "^<^A", "<^^A"], // To: 4
            ["^^A"], // To: 5
            ["^<<A", "<^<A"], // To: 6
            ["^<A", "<^A"], // To: 7
            ["^A"], // To: 8
            [], // To: 9
            ["<A"], // To: 10
            ["A"] // To: 11
        ]
    ];
}