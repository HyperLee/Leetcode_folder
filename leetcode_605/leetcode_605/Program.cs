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

            Console.WriteLine(CanPlaceFlowers2(flow, n));
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


        /// <summary>
        /// https://leetcode.cn/problems/can-place-flowers/solution/tu-jie-leetcode-chong-hua-wen-ti-bian-ch-1gdu/
        /// 個人偏好此方法
        /// 比較好懂
        /// 
        /// 因陣列只有0,1兩種數字
        /// 故可以區分下列幾種相鄰方式
        /// 00, 01, 10, 11. ==> 11不存在因不能連續種花
        /// 故排除11.
        /// 
        /// 当 flowerbed[i] = 0 时：
        /// 若 flowerbed[i + 1] = 0，即为情况“00”，则当前位置 flowerbed[i] = 0 可以种花，根据规则 flowerbed[i + 1] 则不可以种花。
        /// 若 flowerbed[i + 1] = 1，即为情况“01”，则当前位置 flowerbed[i] = 0 不可以种花，根据规则flowerbed[i + 2] 也不可以种花。
        /// 
        /// 当 flowerbed[i] = 1 时：
        /// 若 flowerbed[i + 1] = 0，即为情况“10”，根据规则 flowerbed[i + 1] 不可以种花。
        /// 若 flowerbed[i + 1] = 1，即为情况“11”，根据规则不存在这种情况。
        /// </summary>
        /// <param name="flowerbed"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool CanPlaceFlowers2(int[] flowerbed, int n)
        {
            int length = flowerbed.Length;
            int i = 0;

            while (i < length)
            {
                // 如果当前地块没种花
                if (flowerbed[i] == 0)
                {
                    // 边界条件，如果当前为最后一个且没种花，直接种
                    if (i == length - 1)
                    {
                        n--;
                        break;
                    }

                    // 如果下一个位置没种花
                    // 即 00 的情况
                    if (flowerbed[i + 1] == 0)
                    {
                        // 则当前位置可以种花
                        n--;

                        // 根据规则，此时下一个位置就没法种花，直接跳到下下个位置
                        i += 2;
                    }
                    else if (flowerbed[i + 1] == 1)
                    {
                        // 如果下一个位置种花了
                        // 即 010 的情况

                        // 根据规则，当前位置不能种花，下一个位置的下一个位置也不能种花，直接跳到下下下个位置
                        i += 3;
                    }

                }
                else if (flowerbed[i] == 1)
                {
                    // 如果当前地块种花
                    // 即 10 的情况

                    // 根据规则，下个位置不能种花，直接跳到下下个位置
                    i += 2;
                }
            }

            if (n > 0)
            {
                return false;
            }

            return true;
        }


    }
}
