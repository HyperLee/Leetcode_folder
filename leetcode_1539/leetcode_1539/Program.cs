using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1539
{
    internal class Program
    {
        /// <summary>
        /// leetcode 1539 Kth Missing Positive Number
        /// https://leetcode.com/problems/kth-missing-positive-number/description/
        /// 1539. 第 k 个缺失的正整数
        /// https://leetcode.cn/problems/kth-missing-positive-number/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = new int[] { 2, 3, 4, 7, 11 };
            int k = 5;

            Console.WriteLine(FindKthPositive(input, k));
            Console.ReadKey();
        }


        /// <summary>
        /// 方法一：枚举
        /// https://leetcode.cn/problems/kth-missing-positive-number/solution/di-k-ge-que-shi-de-zheng-zheng-shu-by-leetcode-sol/
        /// 
        /// arr[] 遞增順序, 從1 開始往上遞增
        /// 故 current 預設1, 此參數代表當前數值,
        /// 每輪迴圈中 current 與 ptr 對比
        /// 數值相等ptr往後, 
        /// 用一个指针 ptr 指向数组中没有匹配的第一个元素
        /// misscount 代表 遺失的數值 第幾個 數值
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int FindKthPositive(int[] arr, int k)
        {
            int missCount = 0, lastMiss = -1, current = 1, ptr = 0;

            if(arr == null || arr.Length == 0)
            {
                return 0;
            }

            for (missCount = 0; missCount < k; current++)
            {
                if (current == arr[ptr])
                {
                    ptr = (ptr + 1 < arr.Length) ? ptr + 1 : ptr;
                }
                else
                {
                    missCount++;
                    lastMiss = current;
                }
            }

            return lastMiss;
        }

    }
}
