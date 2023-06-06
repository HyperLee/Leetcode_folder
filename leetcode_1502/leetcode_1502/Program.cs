using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1502
{
    internal class Program
    {
        /// <summary>
        /// 1502. Can Make Arithmetic Progression From Sequence
        /// https://leetcode.com/problems/can-make-arithmetic-progression-from-sequence/
        /// 1502. 判断能否形成等差数列
        /// https://leetcode.cn/problems/can-make-arithmetic-progression-from-sequence/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = new int[] { 3, 5, 1 };
            Console.WriteLine(CanMakeArithmeticProgression(input));
            Console.ReadKey();

        }


        /// <summary>
        /// 求出該輸入是不是 等差數列
        /// 
        /// 
        /// 等差數列，又名算術數列（英語：Arithmetic sequence[註 1]），是數列的一種。
        /// 在等差數列中，任何相鄰兩項的差相等，該差值稱為公差（common difference）。 
        /// https://zh.wikipedia.org/zh-tw/%E7%AD%89%E5%B7%AE%E6%95%B0%E5%88%97
        /// 
        /// 解題概念
        /// 1. 把輸入的array排序
        /// 2. 把第0與第1的值拿出來相減, 稱之為 diff
        /// 3. 從i = 2開始往後兩兩相鄰的差距 都要與diff 相同
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static bool CanMakeArithmeticProgression(int[] arr)
        {
            Array.Sort(arr);

            int diff = arr[1] - arr[0];

            for(int i = 2; i < arr.Length; i++)
            {
                if (arr[i] - arr[i - 1] != diff)
                {
                    return false;
                }
            }

            return true;
        }

    }
}
