namespace AdventOfCode2024.Solutions;

public class Day24_Part2
{
   private record Triple : IComparable
   {
      public string Original { get; set; }
      public string Lhs { get; set; }
      public string Rhs { get; set; }
      public string Op { get; set; }
      public string Target { get; set; }
      public string Comment { get; set; }
      
      public int CompareTo(object? obj)
      {
         var t = obj as Triple;
         var a = this.Lhs.CompareTo(t!.Lhs);
         if (a != 0) return a;
         
         var b = this.Rhs.CompareTo(t!.Rhs);
         if (b != 0) return b;
         
         return this.Op.CompareTo(t!.Op);
      }
   }
   
   public string Part2(string filename)
   {
      var lines = File.ReadAllLines(filename);
      var triples = new List<Triple>();
      foreach (var line in lines)
      {
         //x00 AND y00 -> z00
         if (line.Contains("->"))
         {
            var parts = line.Split(" -> ");
            var expression = parts[0].Split(' ');
            
            triples.Add(new Triple
            {
               Original = line,
               Lhs = String.Compare(expression[0], expression[2], StringComparison.Ordinal) < 0 ? expression[0] : expression[2],
               Rhs = String.Compare(expression[0], expression[2], StringComparison.Ordinal) > 0 ? expression[0] : expression[2],
               Target = parts[1],
               Op = expression[1]
            });
         }
      }
      triples.Sort();

      var answer = new List<string>();
      foreach (var triple in triples)
      {
         // Any output to a Z which isn't an XOR must be wrong.
         if (triple.Target.StartsWith('z') && triple.Op != "XOR")
         {
            Console.WriteLine(triple.Original);
            answer.Add(triple.Target);
         }
      }
      
      // wpb OR fbj -> z45
      // fsf OR nqs -> z12
      // jfk AND vkb -> z29
      // x37 AND y37 -> z37

      
      foreach (var triple in triples)
      {
         if (triple.Op == "XOR" && !triple.Lhs.StartsWith('x') && !triple.Target.StartsWith('z'))
         {
            Console.WriteLine(triple.Original);
            answer.Add(triple.Target);
         }
      }
      
      // bkj XOR fhq -> dtv
      // vkb XOR jfk -> mtj
      // jmr XOR qts -> fgc

      
      //INPUT ANDS should OUTPUT to a OR
      foreach (var triple in triples)
      {
         if (triple.Op == "AND" && triple.Lhs.StartsWith('x') && !triple.Lhs.StartsWith("x00"))
         {
            foreach (var triple2 in triples)
            {
               if (triple2.Lhs == triple.Target || triple2.Rhs == triple.Target)
               {
                  if (triple2.Op != "OR")
                  {
                     Console.WriteLine(triple.Original);
                     answer.Add(triple.Target);
                     break;
                  }
               }
            }
         }
      }

      answer.Sort();
      return string.Join(',', answer);
      
      foreach (var triple in triples)
      {
         if (triple.Lhs.StartsWith('x') && triple.Rhs.StartsWith('y'))
         {
            var lhs = int.Parse(triple.Lhs[1..]);
            var rhs = int.Parse(triple.Rhs[1..]);
            if (lhs != rhs || triple.Op == "OR") throw new Exception();
            triple.Comment = $"Adder for {lhs}";

            if (lhs == 0 && triple.Op == "AND")
            {
               // Rename triple.Target to c0 
               foreach (var triple2 in triples)
               {
                  if (triple2.Lhs == triple.Target)
                  {
                     triple2.Lhs = $"car{lhs}";
                  
                  }
                  else if (triple2.Rhs == triple.Target)
                  {
                     triple2.Rhs = triple2.Lhs;
                     triple2.Lhs = $"car{lhs}";
                  }
               }

               triple.Target = "car0";
            }

            if (lhs != 0 && triple.Op == "XOR")
            {
               // Rename triple.Target to sumN 
               foreach (var triple2 in triples)
               {
                  if (triple2.Lhs == triple.Target)
                  {
                     triple2.Lhs = triple2.Rhs;
                     triple2.Rhs = $"sum{lhs}";
                  }
                  else if (triple2.Rhs == triple.Target)
                     triple2.Rhs = $"sum{lhs}";
               }

               triple.Target = $"sum{lhs}";
            }

            if (lhs != 0 && triple.Op == "AND")
            {
               foreach (var triple2 in triples)
               {
                  if (triple2.Lhs == triple.Target)
                     triple2.Lhs = $"tcA{lhs}";
                  else if (triple2.Rhs == triple.Target)
                  {
                     triple2.Rhs = triple2.Lhs;
                     triple2.Lhs = $"tcA{lhs}";
                  }
               }

               triple.Target = $"tcA{lhs}";
            }

         }
      }

      for (int i = 0; i < 50; i++)
      {


         foreach (var triple in triples)
         {
            if (triple.Lhs.StartsWith("car") && triple.Rhs.StartsWith("sum"))
            {
               var lhs = int.Parse(triple.Lhs[3..]);
               var rhs = int.Parse(triple.Rhs[3..]);
               if (lhs + 1 != rhs || triple.Op == "OR") throw new Exception();

               if (triple.Op == "XOR")
               {
                  if (triple.Target != $"z{rhs:D2}")
                     Console.WriteLine(triple.Original);
                     //throw new Exception("Wrong target");
               }

               if (triple.Op == "AND")
               {
                  foreach (var triple2 in triples)
                  {
                     if (triple2.Lhs == triple.Target)
                     {
                        triple2.Lhs = triple2.Rhs;
                        triple2.Rhs = $"tcB{rhs}";
                     }
                     else if (triple2.Rhs == triple.Target)
                        triple2.Rhs = $"tcB{rhs}";
                  }

                  triple.Target = $"tcB{rhs}";
               }
            }

            if (triple.Lhs.StartsWith("tcA") && triple.Rhs.StartsWith("tcB"))
            {
               var lhs = int.Parse(triple.Lhs[3..]);
               var rhs = int.Parse(triple.Rhs[3..]);
               if (lhs != rhs || triple.Op != "OR") throw new Exception();

               // Rename triple.Target to sumN 
               foreach (var triple2 in triples)
               {
                  if (triple2.Lhs == triple.Target)
                  {
                     triple2.Lhs = $"car{lhs}";

                  }
                  else if (triple2.Rhs == triple.Target)
                  {
                     triple2.Rhs = triple2.Lhs;
                     triple2.Lhs = $"car{lhs}";
                  }
               }

               triple.Target = $"car{lhs}";
            }
         }
      }

      // Half Adder
      //    x00 ^ y00 ==> z00
      //    x00 & y00 ==> car0
      
      
      // Full Adders (n)...
      //    x01 ^ y01 ==> sum1
      //    x01 & y01 ==> tcA1
      
      //    car0 ^ sum1 ==> z01
      //    car0 & sum1 ==> tcB1
      
      //    tcA1 | tcB1 ==> c1   
      

      return "";
   }
    
}