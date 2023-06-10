using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1802
{
    internal class Program
    {
        /// <summary>
        /// 1802. Maximum Value at a Given Index in a Bounded Array
        /// https://leetcode.com/problems/maximum-value-at-a-given-index-in-a-bounded-array/
        /// 
        /// 1802. 有界数组中指定下标处的最大值
        /// https://leetcode.cn/problems/maximum-value-at-a-given-index-in-a-bounded-array/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int n = 4, index = 2, maxSum = 6;

            Console.WriteLine(MaxValue(n, index, maxSum));
            Console.ReadKey();
        }

        /// <summary>
        /// 官方解法
        /// 方法一：贪心 + 二分查找
        /// https://leetcode.cn/problems/maximum-value-at-a-given-index-in-a-bounded-array/solution/you-jie-shu-zu-zhong-zhi-ding-xia-biao-c-aav4/
        /// 
        /// 有點納悶
        /// 左邊界從一開始
        /// mid算法 竟然還要 + 1 ?
        /// 
        /// 和以往的不同
        /// 左邊界改成從0開始
        /// mid改成 (right - left) / 2 + left 
        /// 這組合會超時
        /// 
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <param name="index"></param>
        /// <param name="maxSum"></param>
        /// <returns></returns>

        public static int MaxValue(int n, int index, int maxSum)
        {
            int left = 1, right = maxSum;

            while (left < right)
            {
                int mid = (left + right + 1) / 2;

                if (Valid(mid, n, index, maxSum))
                {
                    left = mid;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return left;
        }


        /// <summary>
        /// 函数 valid用来判断当前的 nums[index] 对应的 numsSum 是否满足条件。
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="n"></param>
        /// <param name="index"></param>
        /// <param name="maxSum"></param>
        /// <returns></returns>

        public static bool Valid(int mid, int n, int index, int maxSum)
        {
            int left = index;
            int right = n - index - 1;
            return mid + Cal(mid, left) + Cal(mid, right) <= maxSum;

        }


        /// <summary>
        /// cal 用来计算单边的元素和，需要考虑边界元素是否早已下降到 1 的情况。
        /// </summary>
        /// <param name="big"></param>
        /// <param name="length"></param>
        /// <returns></returns>

        public static long Cal(int big, int length)
        {
            if (length + 1 < big)
            {
                int small = big - length;

                return (long)(big - 1 + small) * length / 2;
            }
            else
            {
                int ones = length - (big - 1);

                return (long)big * (big - 1) / 2 + ones;
            }

        }


    }
}
