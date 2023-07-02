using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1010
{
    internal class Program
    {
        /// <summary>
        /// 1010. Pairs of Songs With Total Durations Divisible by 60
        /// https://leetcode.com/problems/pairs-of-songs-with-total-durations-divisible-by-60/
        /// 1010. 总持续时间可被 60 整除的歌曲
        /// https://leetcode.cn/problems/pairs-of-songs-with-total-durations-divisible-by-60/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 30, 20, 150, 100, 40 };
            Console.WriteLine(NumPairsDivisibleBy602(input));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/pairs-of-songs-with-total-durations-divisible-by-60/solution/qu-mo-bian-li-by-bentlalala/
        /// 
        /// 對每個輸入時間 取 mod 60
        /// 遍歷同時作加總
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int NumPairsDivisibleBy60(int[] time)
        {
            int res = 0;
            int[] counts = new int[60];

            for (int i = 0; i < time.Length; i++)
            {
                time[i] %= 60;

                int mod = time[i] % 60;
                int tempa = time[i];

                if (time[i] != 0)
                {
                    // 找counts裡面 有幾個相符
                    res += counts[60 - time[i]];
                }
                else
                {
                    res += counts[time[i]];
                }

                // 統計 time[i] % 60 之後 數值 有多少個
                counts[time[i]]++;
            }

            return res;
        }


        /// <summary>
        /// https://leetcode.cn/problems/pairs-of-songs-with-total-durations-divisible-by-60/solution/can-kao-ti-jie-zong-chi-xu-shi-jian-ke-b-nrf5/
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int NumPairsDivisibleBy602(int[] time)
        {
            int ans = 0;
            int size = time.Length;
            int[] map = new int[60];

            for(int i = 0; i < size; i++)
            {
                int j = time[i] % 60;
                ans += map[(60 - j) % 60];
                map[j]++;

            }
            return ans;


        }

    }
}
