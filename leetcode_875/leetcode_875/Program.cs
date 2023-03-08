using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_875
{
    internal class Program
    {
        /// <summary>
        /// leetcode 875
        /// https://leetcode.com/problems/koko-eating-bananas/
        /// 875. 爱吃香蕉的珂珂
        /// https://leetcode.cn/problems/koko-eating-bananas/
        /// 
        /// 本題類似 leetcode 2187
        /// https://leetcode.com/problems/minimum-time-to-complete-trips/
        /// 都採二分法
        /// 
        /// 本題偏好 方法2, 沒額外寫 function
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = new int[] { 3, 6, 7, 11 };
            int h = 8;

            Console.WriteLine(MinEatingSpeed2(input, h));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/koko-eating-bananas/solution/ai-chi-xiang-jiao-de-ke-ke-by-leetcode-s-z4rt/
        /// 
        /// low 最少一根
        /// high 取 香蕉堆裡面最大那一堆
        /// 
        /// 
        /// </summary>
        /// <param name="piles"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        public static int MinEatingSpeed(int[] piles, int h)
        {
            int low = 1;
            int high = piles.Max();
            int k = high;
            
            while (low < high)
            {
                int speed = low + (high - low) / 2;
                long time = GetTime(piles, speed);
                if (time <= h)
                {
                    k = speed;
                    high = speed;
                }
                else
                {
                    low = speed + 1;
                }
            }
            return k;

        }


        /// <summary>
        /// https://leetcode.cn/problems/koko-eating-bananas/solution/ai-chi-xiang-jiao-de-ke-ke-by-leetcode-s-z4rt/
        /// 
        /// </summary>
        /// <param name="piles"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public static long GetTime(int[] piles, int speed)
        {
            long time = 0;
            foreach (int pile in piles)
            {
                int curTime = (pile + speed - 1) / speed;
                time += curTime;
            }
            return time;
        }


        /// <summary>
        /// https://leetcode.cn/problems/koko-eating-bananas/solution/c-by-yym_nuaa-apnm/
        /// 
        /// 本題類似 leetcode 2187
        /// https://leetcode.com/problems/minimum-time-to-complete-trips/
        /// 都採二分法
        /// 
        /// low 最少一根
        /// high 取 香蕉堆裡面最大那一堆
        /// </summary>
        /// <param name="piles"></param>
        /// <param name="h"></param>
        /// <returns></returns>

        public static int MinEatingSpeed2(int[] piles, int h)
        {
            int low = 1;
            int high = piles.Max();

            while (low < high)
            {
                int speed = low + (high - low) / 2;
                int k = 0;

                for (int i = 0; i < piles.Length; i++)
                {
                    k += (piles[i] + speed - 1) / speed;
                }

                if (k > h)
                {
                    low = speed + 1;
                }
                else
                {
                    high = speed;
                }
            }

            return low;
        }


    }
}
