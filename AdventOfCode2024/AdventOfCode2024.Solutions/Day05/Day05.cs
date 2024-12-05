
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
        var lines = File.ReadAllText(filename).AsSpan();
        var inRules = true;
        var rules = new Dictionary<int,List<int>>();
        var total = 0;
        var pages = new List<int>();
        
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
                    // Find all page numbers
                    pages.Clear();
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

                        pages.Add(page);

                        k++; //eat ,
                    }

                    var ok = false;
                    var corrected = false;

                    while (!ok)
                    {
                        ok = true;
                        for (var i = 1; i < pages.Count; i++)
                        {
                            var page = pages[i];
                            if (rules.TryGetValue(page, out var rulesForPage))
                            {
                                for (var j = 0; j < i; j++)
                                {
                                    var page2 = pages[j];
                                    if (rulesForPage.Contains(page2))
                                    {
                                        // We have a problem
                                        pages[i] = page2;
                                        pages[j] = page;
                                        page = pages[i];
                                        corrected = true;
                                        ok = false;
                                    }
                                }
                            }
                        }
                    }
                    
                    if (corrected)
                    {
                        total += pages[pages.Count / 2];                        
                    }
      
                }
            }
        }
        return total;
    }
    
    
}