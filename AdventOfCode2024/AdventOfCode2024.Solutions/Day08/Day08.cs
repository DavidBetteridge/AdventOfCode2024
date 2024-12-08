using System.Collections.Concurrent;

namespace AdventOfCode2024.Solutions;

public class Day08
{
    private sealed record Location(int X, int Y);
    
    public long Part1(string filename)
    {
        var input = File.ReadAllBytes(filename);
        var pos = 0;
        var height = 0;
        var width = 0;
        var antennas = new Dictionary<byte, List<Location>>();
        while (pos < input.Length)
        {
            //Consume line
            width = 0;
            while (pos < input.Length && input[pos] != '\n')
            {
                if (input[pos] != '.')
                {
                    if (!antennas.TryGetValue(input[pos], out List<Location>? value))
                    {
                        value = [];
                        antennas[input[pos]] = value;
                    }

                    value.Add(new Location(width, height));
                }
                width++;
                pos++;  // advance
            }

            pos++;  // Consume newline
            height++;
        }

        var antinodes = new HashSet<int>();
        
        // Naive solution
        foreach (var antenna in antennas)
        {
            var locations = antenna.Value;
            for (var i = 0; i < locations.Count-1 ; i++)
            {
                for (var j = i+1; j < locations.Count; j++)
                {
                    var xDiff = locations[j].X - locations[i].X;
                    var yDiff = locations[j].Y - locations[i].Y;
                    
                    var xTry = locations[i].X - xDiff;
                    var yTry = locations[i].Y - yDiff;
                    if (xTry >= 0 && yTry >= 0 && xTry < width && yTry < height)
                        antinodes.Add(yTry*width+xTry);
                    
                    xTry = locations[j].X + xDiff;
                    yTry = locations[j].Y + yDiff;
                    if (xTry >= 0 && yTry >= 0 && xTry < width && yTry < height)
                        antinodes.Add(yTry*width+xTry);
                    

                }
            }
        }
        
        
        return antinodes.Count;
    }
    
    public long Part2(string filename)
    {
        var input = File.ReadAllBytes(filename);
        var pos = 0;
        var height = 0;
        var width = 0;
        var antennas = new Dictionary<byte, List<Location>>();
        while (pos < input.Length)
        {
            //Consume line
            width = 0;
            while (pos < input.Length && input[pos] != '\n')
            {
                if (input[pos] != '.')
                {
                    if (!antennas.TryGetValue(input[pos], out List<Location>? value))
                    {
                        value = [];
                        antennas[input[pos]] = value;
                    }

                    value.Add(new Location(width, height));
                }
                width++;
                pos++;  // advance
            }

            pos++;  // Consume newline
            height++;
        }

        var antinodes = new HashSet<int>();
        
        // Naive solution
        foreach (var antenna in antennas)
        {
            var locations = antenna.Value;
            for (var i = 0; i < locations.Count-1 ; i++)
            {
                for (var j = i+1; j < locations.Count; j++)
                {
                    var xDiff = locations[j].X - locations[i].X;
                    var yDiff = locations[j].Y - locations[i].Y;
                    
                    var xTry = locations[i].X;
                    var yTry = locations[i].Y;
                    while (xTry >= 0 && yTry >= 0 && xTry < width && yTry < height)
                    {
                        antinodes.Add(yTry*width+xTry);
                        xTry -= xDiff;
                        yTry -= yDiff;
                    }

                    xTry = locations[j].X;
                    yTry = locations[j].Y;
                    while (xTry >= 0 && yTry >= 0 && xTry < width && yTry < height)
                    {
                        antinodes.Add(yTry*width+xTry);
                        xTry += xDiff;
                        yTry += yDiff;
                    }

                }
            }
        }
        
        
        return antinodes.Count;
    }
}