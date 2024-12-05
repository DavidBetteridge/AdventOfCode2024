namespace AdventOfCode2024.Solutions;

public class Day05
{
    public int Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var inRules = true;
        var rules = new Dictionary<int,List<int>>();
        var total = 0;
        foreach (var line in lines)
        {
            if (line == "")
            {
                inRules = false;
            }
            else
            {
                if (inRules)
                {
                    // Store rule
                    var parts = line.Split('|').Select(int.Parse).ToArray();
                    var key = parts[0];
                    var value = parts[1];
                    if (!rules.ContainsKey(key))
                        rules[key] = [];
                    rules[key].Add(value);
                }
                else
                {
                    // Solve line
                    var pages = line.Split(',').Select(int.Parse).ToArray();
                    var ok = true;
                    var seen = new HashSet<int>();
                    foreach (var page in pages)
                    {
                        if (rules.TryGetValue(page, out var rulesForPage))
                        {
                            foreach (var rule in rulesForPage)
                            {
                                if (seen.Contains(rule))
                                {
                                    // Wrong order
                                    ok = false;
                                    break;
                                }
                            }    
                        }

                        if (!ok) break;
                        seen.Add(page);
                    }

                    if (ok)
                        total += pages[pages.Length / 2];
                }
            }
        }

        return total;
    }
    
    public int Part2(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var inRules = true;
        var rules = new Dictionary<int,List<int>>();
        var total = 0;
        foreach (var line in lines)
        {
            if (line == "")
            {
                inRules = false;
            }
            else
            {
                if (inRules)
                {
                    // Store rule
                    var parts = line.Split('|').Select(int.Parse).ToArray();
                    var key = parts[0];
                    var value = parts[1];
                    if (!rules.ContainsKey(key))
                        rules[key] = [];
                    rules[key].Add(value);
                }
                else
                {
                    // Solve line
                    var pages = line.Split(',').Select(int.Parse).ToArray();
                    var ok = false;
                    var corrected = false;

                    while (!ok)
                    {
                        ok = true;
                        var seen = new Dictionary<int, int>();
                        for (var i = 0; i < pages.Length; i++)
                        {
                            var page = pages[i];
                            if (rules.TryGetValue(page, out var rulesForPage))
                            {
                                foreach (var rule in rulesForPage)
                                {
                                    if (seen.TryGetValue(rule, out var location))
                                    {
                                        // Swap
                                        pages[location] = pages[i];
                                        pages[i] = rule;
                                        corrected = true;
                                        ok = false;
                                        break;
                                    }
                                }
                            }
                            if (!ok) break;
                            seen.Add(page, i);
                        }
                    }

                    if (corrected)
                    {
                        total += pages[pages.Length / 2];                        
                    }

                }
            }
        }

        return total;
    }
}