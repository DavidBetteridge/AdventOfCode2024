namespace AdventOfCode2024.Solutions;

public class Day05
{
    // Load rules into a dict<int,List<int>>
    // Parse line by line
    // Track seen digits into a hashset<int>
    // Get rules for current digit rules[current]
    // Make sure no rule refers to an entry in seen
    //
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
}