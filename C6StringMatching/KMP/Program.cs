using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(FindP("BBC ABCDAB ABCDABCDABDE", "ABCDABD"));
        }

        static int FindP(string s, string p)
        {
            var next = GetNext(p);
            int pi = 0, si = 0;
            while (si < s.Length)
            {
                if (pi == -1 || p[pi] == s[si])
                {
                    pi++;
                    si++;
                    if (pi == p.Length) return si - p.Length;
                }
                else pi = next[pi];
            }
            return -1;
        }

        static int[] GetNext(string p)
        {
            var next = new int[p.Length];
            next[0] = -1;
            int k = -1, i = 0;
            while (i < p.Length - 1)
            {
                if (k == -1 || p[k] == p[i])
                {
                    next[++i] = ++k;
                    if (p[i] == p[next[i]]) next[i] = next[next[i]];
                }
                else k = next[k];
            }
            return next;
        }
    }
}
