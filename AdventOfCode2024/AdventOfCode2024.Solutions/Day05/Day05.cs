
namespace AdventOfCode2024.Solutions;

public class Day05
{
    public int Part1(string filename)
    {
        var lines = File.ReadAllText(filename).AsSpan();
        var inRules = true;
        
        var rules = new Dictionary<int,List<int>>();
        
        var total = 0;
        var seen = new HashSet<int>();
        var done = new List<int>();
        
        
        foreach (var line in lines.Split('\n'))
        {
            if (lines[line].Length == 0)
            {
                inRules = false;
            }
            else
            {
                if (inRules)
                {
                    var key = lines[line][0] - '0';
                    var j = 1;
                    while (j < lines[line].Length && lines[line][j] != '|')
                    {
                        key = key * 10 + (lines[line][j] - '0');
                        j++;
                    }

                    // Eat the pipe
                    j++;
                    
                    var value = lines[line][j] - '0';
                    j++;
                    while (j < lines[line].Length)
                    {
                        value = value * 10 + (lines[line][j] - '0');
                        j++;
                    }

                    if (!rules.ContainsKey(key))
                        rules[key] = [];
                    rules[key].Add(value);
                }
                else
                {
                    // Solve line
                    var ok = true;
                    seen.Clear();
                    done.Clear();
                    var pageCount = 0;
                    var k = 0;
                    while (k < lines[line].Length)
                    {
                        var page = lines[line][k] - '0';
                        k++;
                        while (k < lines[line].Length && lines[line][k] != ',')
                        {
                            page = page * 10 + (lines[line][k] - '0');
                            k++;
                        }
            
                        k++;  //eat ,
                        
                        if (rules.TryGetValue(page, out var rulesForPage))
                        {
                            if (rulesForPage.Any(rule => seen.Contains(rule)))
                            {
                                ok = false;
                            }
                        }

                        if (!ok) break;
                        seen.Add(page);
                        done.Add(page);
                        pageCount++;
                    }

                    if (ok)
                        total += done[pageCount/2];
                        
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