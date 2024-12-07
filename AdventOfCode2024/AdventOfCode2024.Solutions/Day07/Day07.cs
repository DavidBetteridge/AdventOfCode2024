using System.Reflection;

namespace AdventOfCode2024.Solutions;

public class Day07
{
    public ulong Part1(string filename)
    {
        var input = File.ReadAllBytes(filename).AsSpan();
        var i = 0;
        var inputs = new List<int>();
        ulong total = 0;

        while (i < input.Length)
        {
            inputs.Clear();
            
            // Eat the testValue
            var testValue = (ulong)(input[i] - '0');
            i++;
            while (i < input.Length && input[i] != ':')
            {
                testValue = testValue * 10 + (ulong)(input[i] - '0');
                i++;
            }
            // Eat : and the space
            i += 2;
            
            // Eat the input values
            while (i < input.Length && input[i] != '\n')
            {
                // Eat the input value
                var inputValue = (input[i] - '0');
                i++;
                while (i < input.Length && input[i] != ' ' && input[i] != '\n')
                {
                    inputValue = inputValue * 10 + (input[i] - '0');
                    i++;
                }
                inputs.Add(inputValue);
                
                //Eat the space
                if (i < input.Length && input[i] == ' ')
                    i++;
            }
            
            // Eat the new line
            i++;

            var listType = typeof(List<int>);
            var itemsField = listType.GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance);
            var inputsSpan = new Span<int>((int[])itemsField!.GetValue(inputs)!, 0, inputs.Count) ;
            
            if (Solve((ulong)testValue, (ulong)inputsSpan[0], inputsSpan[1..]))
                total += (ulong)testValue;
        }

        return total;
        
        bool Solve(ulong testValue, ulong valueSoFar, Span<int> remainingValues)
        {
            if (remainingValues.Length == 0 && valueSoFar == testValue) return true;
            if (remainingValues.Length == 0) return false;
            if (valueSoFar > testValue) return false;
        
            if (Solve(testValue, valueSoFar + (ulong)remainingValues[0], remainingValues[1..]))
                return true;
        
            if (Solve(testValue, valueSoFar * (ulong)remainingValues[0], remainingValues[1..]))
                return true;

            return false;
        }
    }
    
    public ulong Part2(string filename)
    {
        var input = File.ReadAllBytes(filename).AsSpan();
        var i = 0;
        var inputs = new List<int>();
        ulong total = 0;

        while (i < input.Length)
        {
            inputs.Clear();
            
            // Eat the testValue
            var testValue = (ulong)(input[i] - '0');
            i++;
            while (i < input.Length && input[i] != ':')
            {
                testValue = testValue * 10 + (ulong)(input[i] - '0');
                i++;
            }
            // Eat : and the space
            i += 2;
            
            // Eat the input values
            while (i < input.Length && input[i] != '\n')
            {
                // Eat the input value
                var inputValue = (input[i] - '0');
                i++;
                var multiplier = 10;
                while (i < input.Length && input[i] != ' ' && input[i] != '\n')
                {
                    inputValue = inputValue * 10 + (input[i] - '0');
                    i++;
                    multiplier *= 10;
                }
                inputs.Add(inputValue);
                
                //Eat the space
                if (i < input.Length && input[i] == ' ')
                    i++;
            }
            
            // Eat the new line
            i++;

            var listType = typeof(List<int>);
            var itemsField = listType.GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance);
            var inputsSpan = new Span<int>((int[])itemsField!.GetValue(inputs)!, 0, inputs.Count) ;
            
            if (Solve((ulong)testValue, (ulong)inputsSpan[0], inputsSpan[1..]))
                total += (ulong)testValue;
        }

        return total;
        
        bool Solve(ulong testValue, ulong valueSoFar, Span<int> remainingValues)
        {
            if (remainingValues.Length == 0 && valueSoFar == testValue) return true;
            if (remainingValues.Length == 0) return false;
            if (valueSoFar > testValue) return false;
        
            if (Solve(testValue, valueSoFar + (ulong)remainingValues[0], remainingValues[1..]))
                return true;
        
            if (Solve(testValue, valueSoFar * (ulong)remainingValues[0], remainingValues[1..]))
                return true;
            
            if (Solve(testValue, (valueSoFar * (ulong)Math.Pow(10, 1+(int)Math.Log10(remainingValues[0]))) + (ulong)remainingValues[0], remainingValues[1..]))
                return true;

            return false;
        }
    }
}