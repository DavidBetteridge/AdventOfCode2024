namespace AdventOfCode2024.Solutions;

public class Day24
{
    private record Gate
    {
        public string Name { get; set; }
        public string Operator { get; set; }
        public bool? V1 { get; set; }
        public bool? V2 { get; set; }
        public bool? Value { get; set; }
        public List<string> Outputs = new();
    }
    
    public ulong Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);

        var gates = new Dictionary<string, Gate>();
        var q = new Queue<Gate>();
        var outputSize = 0;
        foreach (var line in lines)
        {
            if (line.Contains(':'))
            {
                var parts = line.Split(": ");

                if (!gates.TryGetValue(parts[0], out var gate))
                {
                    gate = new Gate
                    {
                        Name = parts[0],
                    };
                    gates.Add(gate.Name, gate);  
                }
                gate.Value = parts[1] == "1";                
                q.Enqueue(gate);
                
            }
            else if (line.Contains("->"))
            {
                var parts = line.Split(" -> ");
                var expression = parts[0].Split(' ');
                
                if (!gates.TryGetValue(parts[1], out var gate))
                {
                    gate = new Gate
                    {
                        Name = parts[1],
                    };
                    gates.Add(gate.Name, gate);  
                }
                gate.Operator = expression[1];

                if (gate.Name.StartsWith('z'))
                {
                    var gateNumber = int.Parse(gate.Name[1..]);
                    outputSize = Math.Max(outputSize, gateNumber);
                }
                
                if (!gates.TryGetValue(expression[0], out var g1))
                {
                    g1 = new Gate
                    {
                        Name = expression[0],
                    };
                    gates.Add(g1.Name, g1);
                }
                g1.Outputs.Add(gate.Name);
                
                if (!gates.TryGetValue(expression[2], out var g2))
                {
                    g2 = new Gate
                    {
                        Name = expression[2],
                    };
                    gates.Add(g2.Name, g2);
                }
                g2.Outputs.Add(gate.Name);
            }
        }

        while (q.Count > 0)
        {
            var gate = q.Dequeue();

            foreach (var child in gate.Outputs)
            {
                var childGate = gates[child];
                if (childGate.V1 is null)
                    childGate.V1 = gate.Value;
                else
                {
                    childGate.V2 = gate.Value;
                    childGate.Value = childGate.Operator switch
                    {
                        "AND" => childGate.V1 & childGate.V2,
                        "OR" => childGate.V1 | childGate.V2,
                        "XOR" => childGate.V1 ^ childGate.V2
                    };
                    q.Enqueue(childGate);
                }
            }
            
        }

        ulong output = 0;
        for (int i = 0; i <= outputSize; i++)
        {
            output = output + ((gates[$"z{i:D2}"].Value!.Value ? (ulong)1 : 0) << i);
        }
        
        return output;
    }
    
}