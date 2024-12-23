namespace AdventOfCode2024.Solutions;

public class Day23
{
    public int Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);

        var names = new Dictionary<string, int>();
        var network = new Dictionary<int, List<int>>();
        
        //de-ta
        foreach (var line in lines)
        {
            var parts = line.Split("-");
            if (!names.TryGetValue(parts[0], out var lhs))
            {
                lhs = names.Count;
                names.Add(parts[0], lhs);
            }

            if (!names.TryGetValue(parts[1], out var rhs))
            {
                rhs = names.Count;
                names.Add(parts[1], rhs);
            }

            if (!network.TryGetValue(lhs, out var leftNode))
            {
                leftNode = new List<int>();
                network[lhs] = leftNode;
            }
            
            if (!network.TryGetValue(rhs, out var rightNode))
            {
                rightNode = new List<int>();
                network[rhs] = rightNode;
            }
            
            leftNode.Add(rhs);
            rightNode.Add(lhs);
        }

        // Sets of three containing the letter 't'
        var count = 0;
        for (var n0 = 0; n0 < names.Count; n0++)
        {
            var other = network[n0];
            if (other.Count >= 2)
            {
                var n1 = network[other[0]];
                var n2 = network[other[1]];

                if (n1.Count >= 2 && n2.Count >= 2)
                {
                    // n1 must contain n0 and n2
                    var ok = false;
                    foreach (var n in n1)
                    {
                        if (n == other[1])
                        {
                            ok = true;
                            break;
                        }
                    }

                    if (ok)
                    {
                        ok = false;
                        foreach (var n in n2)
                        {
                            if (n == other[0])
                            {
                                ok = true;
                                break;
                            }
                        }
                    }
                    if (ok)
                        count++;
                }
            }
        }
        
        return count;
    }
}