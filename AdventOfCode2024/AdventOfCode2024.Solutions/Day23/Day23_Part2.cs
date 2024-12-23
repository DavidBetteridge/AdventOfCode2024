namespace AdventOfCode2024.Solutions;

public class Day23_Part2
{
    public string Part2(string filename)
    {
        var lines = File.ReadAllLines(filename);

        var names = new Dictionary<string, int>();
        var names2 = new List<string>();
        var network = new Dictionary<int, bool[]>();
        
        //de-ta
        foreach (var line in lines)
        {
            var parts = line.Split("-");
            if (!names.TryGetValue(parts[0], out var lhs))
            {
                lhs = names.Count;
                names.Add(parts[0], lhs);
                names2.Add(parts[0]);
            }

            if (!names.TryGetValue(parts[1], out var rhs))
            {
                rhs = names.Count;
                names.Add(parts[1], rhs);
                names2.Add(parts[1]);
            }

            if (!network.TryGetValue(lhs, out var leftNode))
            {
                leftNode = new bool[lines.Length];
                network[lhs] = leftNode;
            }
            
            if (!network.TryGetValue(rhs, out var rightNode))
            {
                rightNode = new bool[lines.Length];
                network[rhs] = rightNode;
            }

            leftNode[rhs] = true;
            rightNode[lhs] = true;
        }

        var excluded = new bool[lines.Length];
        for (var x = 0; x < names.Count; x++)
        {
            excluded[x] = true;

            for (var n0 = 0; n0 < names.Count; n0++)
            {
                var ok = true;
                for (var n1 = 0; n1 < names.Count; n1++)
                {
                    if (!excluded[n1] && network[n0][n1])
                    {
                        // So n0 is linked to n1, but we need them to share all the other links
                        for (var toTest = 0; toTest < names.Count; toTest++)
                        {
                            if (!excluded[toTest] && (network[n0][toTest] && !network[n1][toTest]) && toTest != n0 && toTest != n1)
                            {
                                ok = false;
                                break;
                            }
                        }
                    }
                    if (!ok)
                        break;  
                }

                if (ok)
                {
                    // Everything linked to n0,  excluding 
                    var sol = new List<string>();
                    for (var n1 = 0; n1 < names.Count; n1++)
                    {
                        if (!excluded[n1] && network[n0][n1])
                        {
                            sol.Add(names2[n1]);
                        }
                    }
                    sol.Add(names2[n0]);
                    sol.Sort();
                    return string.Join(',', sol);
                }
            }            
            excluded[x] = false;
        }

        return "";
    }
}