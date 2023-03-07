using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2187
{
    internal class Program
    {
        /// <summary>
        /// leetcode 2187
        /// https://leetcode.com/problems/minimum-time-to-complete-trips/description/
        /// 2187. 完成旅途的最少时间
        /// https://leetcode.cn/problems/minimum-time-to-complete-trips/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = new int[] { 1, 2, 3 };
            int totaltrips = 5;

            Console.WriteLine(MinimumTime2(input, totaltrips));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/minimum-time-to-complete-trips/solution/wan-cheng-lu-tu-de-zui-shao-shi-jian-by-uxyrp/
        /// 
        /// https://leetcode.com/problems/minimum-time-to-complete-trips/solutions/1817102/c-binary-search/?q=c%23&orderBy=most_relevant
        /// </summary>
        /// <param name="time"></param>
        /// <param name="totalTrips"></param>
        /// <returns></returns>
        public static long MinimumTime(int[] time, int totalTrips)
        {
            long left = 1;
            // 假設 單趟次最大 * totalTrips 為最大, 也可以直接寫 long.MaxValue; 即可 設一個最大 當作右邊界
            long right = ((long)time.Max() * totalTrips);

            while (left < right)
            {
                var middle = left + (right - left) / 2;
                if (check(time, middle, totalTrips) < totalTrips)
                    left = middle + 1;
                else
                    right = middle;
            }

            return left;
        }

        private static long check(int[] busTime, long totalTime, long target)
        {
            long sum = 0;
            var i = 0;
            while (i < busTime.Length && sum <= target)
            {
                sum += totalTime / busTime[i++];
            }
            return sum;
        }


        /// <summary>
        /// https://leetcode.com/problems/minimum-time-to-complete-trips/solutions/3266797/c-binary-search/?q=c%23&orderBy=most_relevant
        /// https://leetcode.cn/problems/minimum-time-to-complete-trips/solution/zhou-sai-di-282chang-zhou-sai-pu-tao-che-s4eo/
        /// 二分法
        /// 
        /// 左邊界取1, 因最少需要一趟次
        /// 右邊界 可以取最大值 
        /// 或者是 ((long)time.Max() * totalTrips) 耗時最久的一趟次 完成totalTrips需要時間
        /// 
        /// count 统计所有公交车完成旅途数量的总和; 记录当前时间能完成的趟数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="totalTrips"></param>
        /// <returns></returns>
        public static long MinimumTime2(int[] time, int totalTrips)
        {
            long result = long.MaxValue;
            long left = 1; 
            long right = ((long)time.Max() * totalTrips);

            while (left <= right)
            {
                long mid = left + (right - left) / 2;

                long count = 0;

                for (int i = 0; i < time.Length; i++)
                {
                    count += mid / time[i];
                }

                if (count >= totalTrips)
                {
                    result = Math.Min(result, mid);
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }

            }

            return result;
        }


    }
}
