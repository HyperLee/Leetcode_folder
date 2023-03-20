using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_605
{
    internal class Program
    {
        /// <summary>
        /// leetcode_605
        /// https://leetcode.com/problems/can-place-flowers/
        /// 605. 种花问题
        /// https://leetcode.cn/problems/can-place-flowers/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] flow = new int[] { 1, 0, 0, 0, 1 };
            int n = 2;

            Console.WriteLine(CanPlaceFlowers(flow, n));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/can-place-flowers/solution/chong-hua-wen-ti-by-leetcode-solution-sojr/
        /// 
        /// </summary>
        /// <param name="flowerbed"></param>
        /// <param name="n"></param>
        /// <returns></returns>

        public static bool CanPlaceFlowers(int[] flowerbed, int n)
        {
            int count = 0;
            int m = flowerbed.Length;
            int prev = -1;

            for(int i = 0; i < m; i++)
            {
                if (flowerbed[i] == 1)
                {
                    if(prev < 0)
                    {
                        count += i / 2;
                    }
                    else
                    {
                        count += (i - prev - 2) / 2;
                    }

                    if(count >= n)
                    {
                        return true;
                    }
                    prev = i;
                }
            }

            if (prev < 0)
            {
                count += (m + 1) / 2;
            }
            else
            {
                count += (m - prev - 1) / 2;
            }

            return count >= n;

        }


    }
}
