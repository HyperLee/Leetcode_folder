using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1822
{
    internal class Program
    {
        /// <summary>
        /// leetcode 1822 Sign of the Product of an Array
        /// https://leetcode.com/problems/sign-of-the-product-of-an-array/
        /// 数组元素积的符号
        /// https://leetcode.cn/problems/sign-of-the-product-of-an-array/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = new int[] { 9, 72, 34, 29, -49, -22, -77, -17, -66, -75, -44, -30, -24 };
            Console.WriteLine(ArraySign(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 本題目  重點是判斷 正負號 以及 0
        /// 所以不需要實際乘法之後 取出數值
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int ArraySign(int[] nums)
        {
            // 沒有資料回傳0
            if(nums.Length == 0)
            {
                return 0;
            }

            // array中有0 回傳 0
            if(nums.Contains(0) == true)
            {
                return 0;
            }

            // 統計 負數 數量
            int sign = 0;
            for(int i = 0; i < nums.Length; i++)
            {
                if (nums[i] < 0)
                {
                    sign++;
                }
            }

            if(sign % 2 == 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }

            /*
            long res = 1;
            
            for(int i = 0; i < nums.Length; i++)
            {
                res = res * nums[i];
            }

            Console.WriteLine(res);

            if(res > 0)
            {
                return 1;
            }
            else if(res == 0)
            {
                return 0;
            }
            else
            {
                return -1;
            }

            */

        }


        /// <summary>
        /// 方法2
        /// 
        /// https://leetcode.cn/problems/sign-of-the-product-of-an-array/solution/by-ac_oier-qy0n/
        /// https://leetcode.cn/problems/sign-of-the-product-of-an-array/solution/shu-zu-yuan-su-ji-de-fu-hao-by-leetcode-f4uuj/
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int ArraySign2(int[] nums)
        {
            int sign = 1;
            for(int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 0)
                {
                    return 0;
                }
                else if (nums[i] < 0)
                {
                    sign = -sign;
                }

            }

            return sign;
        }


    }
}
