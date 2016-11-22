using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindKthElement
{
    class Program
    {
        // 1,13,15,29,37,42,46,55,69,105,61,69,113
        static void Main(string[] args)
        {
            int size = 10000;
            for (int i = 0; i < 10000; i++)
            {
                var A = GetNums(size);
                var copyA = (int[])A.Clone();
                Array.Sort(copyA);
                if (FindKth(A, size / 2) != copyA[size / 2 - 1])
                {
                    foreach (var item in A)
                    {
                        Console.Write($"{item},");
                    }
                    break;
                }
            }

            //var A = new int[] { 1, 13, 15, 29, 37, 42, 46, 55, 69, 105, 61, 69, 113 };
            //Console.WriteLine(FindKth(A, 6));
        }

        static int[] GetNums(int size)
        {
            var random = new Random();
            var nums = new int[size];

            for (int i = 0; i < size; i++)
            {
                nums[i] = random.Next(size * 10);
            }
            return nums;
        }

        static int FindKth(int[] A, int k)
        {
            //return Find(A, k, 0, A.Count() - 1);
            return A[FindMoM(A, k, 0, A.Count() - 1)];
        }

        // 使用5元取中法，返回第k大数的索引
        private static int FindMoM(int[] A, int k, int low, int up)
        {
            int n = up - low + 1;
            if (n <= 5)
            {
                int groupLeft = low;
                int groupRight = Math.Min(low + 4, up);
                InsertSort(A, groupLeft, groupRight);
                return low+k-1;
            }

            int groupCount = (int)Math.Ceiling(n / 5d);
            for (int i = 0; i < groupCount; i++)
            {
                int groupLeft = low + i * 5;
                int groupRight = Math.Min(low + i * 5 + 4, up);
                InsertSort(A, groupLeft, groupRight);
                swap(ref A[low + i], ref A[(groupLeft + groupRight) / 2]);
            }
            int pivotIndex = FindMoM(A, (int)Math.Ceiling(n / 10d), low, low + groupCount - 1);

            int index = MoMPartition(A, pivotIndex, low, up);
            int count = index - low + 1;
            if (k < count) return FindMoM(A, k, low, index - 1);
            else if (k > count) return FindMoM(A, k - count, index + 1, up);
            else return index;
        }

        private static void InsertSort(int[] A, int low, int up)
        {
            int n = up - low + 1;
            for (int j = 1; j < n; j++)
            {
                int i = low + j;
                int temp = A[low + j];
                while (i > low && A[i - 1] > temp)
                {
                    A[i] = A[i - 1];
                    i--;
                }
                A[i] = temp;
            }
        }

        private static int MoMPartition(int[] A, int pivotIndex, int low, int up)
        {
            int pivot = A[pivotIndex];
            swap(ref A[pivotIndex], ref A[up]);
            int l = low - 1;
            for (int r = low; r < up; r++)
            {
                if (A[r] < pivot)
                {
                    l++;
                    swap(ref A[l], ref A[r]);
                }
            }
            swap(ref A[++l], ref A[up]);
            return l;
        }

        // 使用随机取中法，返回第k大的数
        private static int Find(int[] A, int k, int low, int up)
        {
            if (low == up) return A[low];
            int index = RandomPartition(A, low, up);
            // count of items <= A[index]
            int count = index - low + 1;
            if (k < count) return Find(A, k, low, index - 1);
            else if (k > count) return Find(A, k - count, index + 1, up);
            else return A[index];
        }

        private static int RandomPartition(int[] A, int low, int up)
        {
            var random = new Random();
            int i = random.Next(low, up + 1);
            int pivot = A[i];
            swap(ref A[i], ref A[up]);
            int l = low - 1;
            for (int r = low; r < up; r++)
            {
                if (A[r] < pivot)
                {
                    l++;
                    swap(ref A[l], ref A[r]);
                }
            }
            swap(ref A[++l], ref A[up]);
            return l;
        }

        private static void swap(ref int v1, ref int v2)
        {
            int temp = v1;
            v1 = v2;
            v2 = temp;
        }
    }
}
