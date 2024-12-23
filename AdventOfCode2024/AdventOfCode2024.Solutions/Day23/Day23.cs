namespace AdventOfCode2024.Solutions;

public class Day23
{
    public int Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);

        var names = new Dictionary<string, int>();
        var names2 = new Dictionary<int, string>();
        var network = new Dictionary<int, List<int>>();
        
        //de-ta
        foreach (var line in lines)
        {
            var parts = line.Split("-");
            if (!names.TryGetValue(parts[0], out var lhs))
            {
                lhs = names.Count;
                names.Add(parts[0], lhs);
                names2.Add(lhs, parts[0]);
            }

            if (!names.TryGetValue(parts[1], out var rhs))
            {
                rhs = names.Count;
                names.Add(parts[1], rhs);
                names2.Add(rhs, parts[1]);
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

        var solutions = new HashSet<ulong>();
        for (var n0 = 0; n0 < names.Count; n0++)
        {
            // For all pairs of children,  are they related
            var possibleNodes = network[n0];
            if (possibleNodes.Count >= 2)
            {
                for (var x = 0; x < possibleNodes.Count-1; x++)
                {
                    var b = possibleNodes[x];
                    
                    for (var y = x+1; y < possibleNodes.Count; y++)
                    {
                        var c = possibleNodes[y];
                        
                        // We know that n0, x and y are related.
                        // Are x and y related?
                        foreach (var n in network[c])
                        {
                            if (n == b && n != n0)
                            {
                                var a = n0;

                                if (names2[a].StartsWith('t') || names2[b].StartsWith('t') || names2[c].StartsWith('t'))
                                {
                                    if (a > c)
                                        (a, c) = (c, a);

                                    if (a > b)
                                        (a, b) = (b, a);

                                    if (b > c)
                                        (b, c) = (c, b);

                                    var sol = ((ulong)a << 22) + ((ulong)b << 11) + (ulong)c;
                                    solutions.Add(sol);
                                }
                            }
                        }
                    }
                }
            }
            
        }
        
        return solutions.Count;
    }
}