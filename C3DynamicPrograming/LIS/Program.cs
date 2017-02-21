using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIS
{
    // 最长（不严格）递增子序列
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(FindLIS_CH(new int[] { 1, 2, 3, 4, 5, 6, 7, 3 }));
        }

        static int FindLIS(int[] A)
        {
            int n = A.Count();
            var dp = new int[n];
            for (int i = 0; i < n; i++) dp[i] = 1;
            for (int i = 1; i < n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (A[i] >= A[j])
                    {
                        dp[i] = Math.Max(dp[i], dp[j] + 1);
                    }
                }
            }
            return dp.Max();
        }
    }
}
