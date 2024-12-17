namespace AdventOfCode2024.Solutions;

public class Day17
{

    public string Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);

        var regA = int.Parse(lines[0].Split(": ")[1]);
        var regB = int.Parse(lines[1].Split(": ")[1]);
        var regC = int.Parse(lines[2].Split(": ")[1]);
        var program = lines[4].Split(": ")[1].Split(',').Select(int.Parse).ToArray().AsSpan();
        var ip = 0;
        var output = new List<string>();
        while (ip < program.Length)
        {
            var opCode = program[ip];
            var operand = program[ip + 1];

            var combo = operand switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                3 => 3,
                4 => regA,
                5 => regB,
                6 => regC,
                _ => -1 //throw new Exception($"Invalid combo {operand}")
            };
            ip += 2;

            switch (opCode)
            {
                case 0:
                {
                    // adv ( A = A / 2^combo) 
                    regA = (int)(regA / (Math.Pow(2, combo)));
                    break;
                }

                case 1:
                {
                    //bxl
                    regB = regB ^ operand;
                    break;
                }

                case 2:
                {
                    //bst
                    regB = combo % 8;
                    break;
                }

                case 3:
                {
                    if (regA != 0)
                        ip = operand;
                    break;
                }

                case 4:
                {
                    //bxc
                    regB = regB ^ regC;
                    break;
                }


                case 5:
                    output.Add((combo % 8).ToString());
                    break;

                case 7:
                {
                    // cdv ( C = A / 2^combo) 
                    regC = (int)(regA / (Math.Pow(2, combo)));
                    break;
                }

                default:
                    throw new Exception($@"Unknown opcode {opCode}");
            }


        }

        return string.Join(',', output);
    }


    public ulong Part2()
    {
        var program = "2,4,1,7,7,5,1,7,4,6,0,3,5,5,3,0";
        ulong start = 8;
            
        while (true) 
        {
            var value = Try(start++);
            if (value != 0) return value;
        }

        ulong Try(ulong toTry)
        {
            var output = Run(toTry);

            if (output == program)
            {
                return toTry;
            }

            if (program.EndsWith(output))
            {
                for (ulong i = 0; i < 8; i++)
                {
                    var next = i | (toTry << 3);
                    var value = Try(next);
                    if (value != 0)
                        return value;
                }
            }

            return 0;
        }
    }
    
    public string Run(ulong regA)
    {
        var output = new List<string>();

        // Program: 2,4,1,7,7,5,1,7,4,6,0,3,5,5,3,0
        do
        {
            // 2,4
            var regB = regA % 8; // Take last 3 bits

            // 1,7
            var shiftBy = regB ^ 7; // and not them

            // 7,5
            var regC = regA >> (int)shiftBy; // lose between 0 and 7 bits

            // 4,6
            regB = regB ^ regC;

            // 5,5
            output.Add((regB % 8).ToString());

            // 0,3
            regA = regA >> 3;
        } while (regA != 0);

        return string.Join(',', output);
    }
}