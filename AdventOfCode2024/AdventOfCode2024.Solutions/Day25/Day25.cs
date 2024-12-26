using System.Collections.Concurrent;
using System.Numerics.Tensors;

namespace AdventOfCode2024.Solutions;

public class Day25
{
    public int Part1(string filename)
    {
        var input = File.ReadAllBytes(filename);

        var keys = new List<uint>();
        var locks = new List<uint>();

        var i = 0;
        while (i < input.Length)
        {
            var isKey = input[i] == '.';
            i += 6;

            if (isKey)
            {
                uint key = 0;
                for (var r = 0; r < 5; r++)
                {
                    for (var c = 0; c < 5; c++)
                    {
                        key <<= 1;
                        if (input[i] == '#')
                        {
                            key ++;
                        }

                        i++;
                    }
                    i++; // New line
                }

                keys.Add(key);
            }
            else
            {
                uint loc = 0;
                for (var r = 0; r < 5; r++)
                {
                    for (var c = 0; c < 5; c++)
                    {
                        loc <<= 1;
                        if (input[i] == '#')
                        {
                            loc++;
                        }

                        i++;
                    }
                    i++; // New line
                }

                locks.Add(loc);
            }

            i += 7; // Trailer and blank line
        }


        const int BlockSize = 25;
      
        var la = locks.ToArray();
        var total =new ConcurrentBag<int>();
        var blockCount = keys.Count / BlockSize;
        Parallel.For(0, blockCount, block =>
        {
            var k = new uint[locks.Count];
            ReadOnlySpan<uint> l = la;

            foreach (var key in keys[(block*BlockSize)..(((block+1)*BlockSize))])
            {
                Array.Fill(k, key);
                TensorPrimitives.BitwiseAnd(l, k, k);
                TensorPrimitives.PopCount<uint>(k, k);
                TensorPrimitives.PopCount<uint>(k, k);
                TensorPrimitives.PopCount<uint>(k, k);
                TensorPrimitives.PopCount<uint>(k, k);
                total.Add((int)(TensorPrimitives.Sum<uint>(k)));
            }
        });
        
        if( keys.Count % BlockSize != 0)
        {
            var k = new uint[locks.Count];
            ReadOnlySpan<uint> l = la;

            for (var j = blockCount * BlockSize; j < k.Length; j++)
            {
                var key = keys[j];
                Array.Fill(k, key);
                TensorPrimitives.BitwiseAnd(l, k, k);
                TensorPrimitives.PopCount<uint>(k, k);
                TensorPrimitives.PopCount<uint>(k, k);
                TensorPrimitives.PopCount<uint>(k, k);
                TensorPrimitives.PopCount<uint>(k, k);
                total.Add((int)(TensorPrimitives.Sum<uint>(k)));
            }
        }
            
        return (locks.Count * keys.Count) - total.Sum();
    }
}