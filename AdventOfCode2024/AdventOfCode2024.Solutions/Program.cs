using AdventOfCode2024.Solutions;
using BenchmarkDotNet.Running;
// BenchmarkRunner.Run<Day05BenchmarkTests>();


class Program
{
   // /*
   //  *  7(0)  8(1) 9(2)
   //  *  4(3)  5(4) 6(5)
   //  *  1(6)  2(7) 3(8)
   //  *   (9)  0(10). A(11)
   //  */


   static void Main(string[] args)
   {
      for (var from = 0; from < 12; from++)
      {
         var fromCol = from % 3;
         var fromRow = from / 3;
         
         Console.WriteLine($"// From {from}");
         Console.WriteLine("new[]");
         Console.WriteLine("{");
         for (var to = 0; to < 12; to++)
         {
            var toCol = to % 3;
            var toRow = to / 3;
            Console.Write($"  new List<string> {{ ");
            
            var left = Math.Max(0, fromCol - toCol);
            var right = Math.Max(0, toCol - fromCol);
            var up = Math.Max(0, fromRow - toRow);
            var down = Math.Max(0, toRow - fromRow);
            Combination("", left, right, down, up, from);
            
            Console.WriteLine($"}},   // To: {to}");

         }
         Console.WriteLine("},");
      }
   }

   public static void Combination(string path, int left, int right, int down, int up, int location)
   {
      if (location == 9) return;  //skip blank
      
      if (up > 0)
         Combination(path+"^", left, right, down, up-1, location - 3);
      if (down > 0)
         Combination(path+"V", left, right, down-1, up, location + 3);
      if (left > 0)
         Combination(path+"<", left-1, right, down, up, location - 1);
      if (right > 0)
         Combination(path+">", left, right-1, down, up, location + 1);

      if (up + down + left + right == 0)
      {
         path += 'A';
         Console.Write($"\"{path}\", ");
      }
   }
}
