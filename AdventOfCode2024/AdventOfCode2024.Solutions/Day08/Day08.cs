namespace AdventOfCode2024.Solutions;

public class Day08
{
    private sealed record Location(int X, int Y);
    
    public long Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var height = lines.Length;
        var width = lines[0].Length;
        var antennas = new Dictionary<char, List<Location>>();

        for (var rowNumber = 0; rowNumber < width; rowNumber++)
        {
            for (var columnNumber = 0; columnNumber < height; columnNumber++)
            {
                var cell = lines[rowNumber][columnNumber];
                if (cell != '.')
                {
                    if (!antennas.ContainsKey(cell))
                    {
                        antennas[cell] = [];
                    }
                    antennas[cell].Add(new Location(columnNumber, rowNumber));
                }
            }
        }

        var antinodes = new HashSet<Location>();
        
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
                        antinodes.Add(new Location(xTry, yTry));
                    
                    xTry = locations[j].X + xDiff;
                    yTry = locations[j].Y + yDiff;
                    if (xTry >= 0 && yTry >= 0 && xTry < width && yTry < height)
                        antinodes.Add(new Location(xTry, yTry));
                    

                }
            }
        }
        
        
        return antinodes.Count;
    }
}