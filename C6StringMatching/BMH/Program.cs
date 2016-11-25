using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMH
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(FindP("ABCABDABCC", "ABD"));
        }

        static int FindP(string s, string p)
        {
            var shift = GetShift(p);
            int pi, si = 0;
            while (si < s.Length - p.Length)
            {
                pi = p.Length - 1;
                while (p[pi] == s[si + pi])
                {
                    pi--;
                    if (pi < 0) return si;
                }
                si += Math.Max(1, shift[s[si + pi]] - (p.Length - 1 - pi));
            }
            return -1;
        }

        static int[] GetShift(string p)
        {
            var shift = new int[256];
            for (int i = 0; i < 256; i++)
                shift[i] = p.Length;
            for (int i = 0; i < p.Length - 1; i++)
                shift[p[i]] = p.Length - 1 - i;
            return shift;
        }
    }
}
