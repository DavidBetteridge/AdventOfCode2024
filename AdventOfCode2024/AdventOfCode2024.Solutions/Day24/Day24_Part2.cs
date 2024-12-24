namespace AdventOfCode2024.Solutions;

public class Day24_Part2
{
   private record Triple
   {
      public string Original { get; set; }
      public string Lhs { get; set; }
      public string Rhs { get; set; }
      public string Op { get; set; }
      public string Target { get; set; }
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

      var answer = new HashSet<string>();
      foreach (var triple in triples)
      {
         if (triple.Target.StartsWith('z') && triple.Op != "XOR" && triple.Target != "z45")
         {
            answer.Add(triple.Target);
            Console.WriteLine("Only XORs can target zs - not " + triple.Original);
         }
         
         // Stage2 XORs must target Zs
         if (!triple.Target.StartsWith('z') && triple.Op == "XOR" && !triple.Lhs.StartsWith("x"))
         {
            answer.Add(triple.Target);
            Console.WriteLine("Stage2 XORs must target Zs - not " + triple.Original);
         }
         
         // Adds must feed ORs
         if (triple.Op == "AND" && triple.Lhs != "x00" )
         {
            var feeds = triples.Where(t => t.Lhs == triple.Target || t.Rhs == triple.Target).ToList();
            foreach (var fed in feeds)
            {
               if (fed.Op != "OR")
               {
                  answer.Add(triple.Target);
                  Console.WriteLine("ANDS must target ORS - not " + triple.Original);
                  break;
               }
            }
         }
         
         // ORs must be targeted by ANDS
         if (triple.Op == "OR" )
         {
            var LHSFeeds = triples.Single(t => t.Target == triple.Lhs);
            if (LHSFeeds.Op != "AND")
            {
               answer.Add(LHSFeeds.Target);
               Console.WriteLine("ORS must be targeted by ANDS - not " + LHSFeeds.Original);
            }
            
            var RHSFeeds = triples.Single(t => t.Target == triple.Rhs);
            if (RHSFeeds.Op != "AND")
            {
               answer.Add(RHSFeeds.Target);
               Console.WriteLine("ORS must be targeted by ANDS - not " + RHSFeeds.Original);
            }
         }
      }

      var a = answer.ToList();
      a.Sort();
      return string.Join(',', a);
   }
    
}