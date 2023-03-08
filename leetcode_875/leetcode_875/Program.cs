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
        /// https://leetcode.cn/problems/koko-eating-bananas/solution/er-fen-cha-zhao-ding-wei-su-du-by-liweiwei1419/
        /// 
        /// low 最少一根
        /// high 取 香蕉堆裡面最大那一堆
        /// 
        /// 珂珂吃香蕉的速度越小，耗时越多。
        /// 反之，速度越大，耗时越少
        /// 
        /// 每堆香蕉吃完的耗时 = 这堆香蕉的数量 / 珂珂一小时吃香蕉的数量
        /// 根据题意，这里的 / 在不能整除的时候，需要 上取整。
        /// 
        /// 取整还可以这样写：sum += (pile + speed - 1) / speed;。
        /// 这是因为题目问的是「最小速度 」。
        /// </summary>
        /// <param name="piles"></param>
        /// <param name="h"></param>
        /// <returns></returns>

        public static int MinEatingSpeed2(int[] piles, int h)
        {
            // 速度最小的时候，耗时最长
            int low = 1;
            // 速度最大的时候，耗时最短
            int high = piles.Max();

            while (low < high)
            {
                int speed = low + (high - low) / 2;
                int totaltime = 0;

                for (int i = 0; i < piles.Length; i++)
                {
                    // 全部所需時間
                    totaltime += (piles[i] + speed - 1) / speed;
                }

                if (totaltime > h)
                {
                    // 耗时太多，说明速度太慢了
                    low = speed + 1;
                }
                else
                {
                    // 耗時小 速度快
                    high = speed;
                }
            }

            return low;
        }


    }
}
