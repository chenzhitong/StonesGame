using System;
using System.Collections.Generic;
using System.Linq;

namespace StonesGame
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] initial = { 9, 7, 5 };
            int[] now = { 1, 0, 0 };
            List<string> all = new List<string>();
            List<string> firstFail = new List<string>();
            List<string> firstWin = new List<string>();

            for (int i = 0; i <= initial[0]; i++)
            {
                for (int j = 0; j <= initial[1]; j++)
                {
                    for (int k = 0; k <= initial[2]; k++)
                    {
                        var code = genDeadNum(new int[] { i, j, k });
                        if (!all.Contains(code))
                            all.Add(code);
                    }
                }
            }

            all = all.Distinct().OrderBy(p => p).ToList();
            all.Remove("0-0-0");
            firstFail.Add("1-0-0");
            while (firstFail.Count + firstWin.Count < all.Count)
            {
                foreach (var a in all)
                {
                    
                    var flag1 = false;
                    foreach (var f in firstFail)
                    {
                        if (OneStep(a, f) && !firstWin.Contains(a) && !firstFail.Contains(a))
                        {
                            firstWin.Add(a);
                            flag1 = true;
                        }
                    }
                    if (!flag1)
                    {
                        foreach (var w in firstWin)
                        {
                            if (OneStep(a, w) && !firstFail.Contains(a) && !firstWin.Contains(a))
                            {
                                firstFail.Add(a);
                            }
                        }
                    }
                }
            }
            firstFail.ForEach(p => Console.WriteLine($"先手必败 {p}"));
            Console.WriteLine($"先手必败 {firstFail.Count} 种情况，先手必胜 {firstWin.Count} 种情况");


            Console.ReadLine();
        }

        private static string genDeadNum(int[] input)  => string.Join('-', input.ToList().OrderByDescending(p => p));

        private static bool OneStep(string a, string b)
        {
            var oldA = a;
            var oldB = b;
            a = a.Replace("-", "");
            var sumA = 0;
            a.ToList().ForEach(p => sumA += (p - '0'));
            b = b.Replace("-", ""); 
            var sumB = 0;
            b.ToList().ForEach(p => sumB += (p - '0'));
            foreach (var i in a)
            {
                if (b.IndexOf(i) >= 0)
                {
                    b = b.Remove(b.IndexOf(i), 1);
                }
            }
            if (b.Length == 1 && sumA > sumB)
            {
                //Console.WriteLine($"{oldA} 一步 {oldB}");
                return true;
            }
            else
                return false;
        }
    }
}
