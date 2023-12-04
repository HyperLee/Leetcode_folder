using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2264
{
    internal class Program
    {
        /// <summary>
        /// 2264. Largest 3-Same-Digit Number in String
        /// https://leetcode.com/problems/largest-3-same-digit-number-in-string/?envType=daily-question&envId=2023-12-04
        /// 2264. 字符串中最大的 3 位相同数字
        /// https://leetcode.cn/problems/largest-3-same-digit-number-in-string/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string input = "42352338";
            Console.WriteLine(LargestGoodInteger(input));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/largest-3-same-digit-number-in-string/solutions/1538493/zi-fu-chuan-zhong-zui-da-de-3-wei-xiang-isykz/
        /// 官方解法
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>

        public static string LargestGoodInteger(string num)
        {
            int n = num.Length;
            int res = -1;

            // 取連續三個需要注意迴圈範圍
            for(int i = 0; i < n - 2; i++)
            {
                // 題目要求 連續三個一樣數字, 且是最大的
                if (num[i] == num[i + 1] && num[i + 1] == num[i + 2])
                {
                    res = Math.Max(res,int.Parse(num.Substring(i, 3)));
                }
            }

            if(res == 0)
            {
                // 如果是0 要回傳000. 因res宣告是int 但是回傳是字串
                return "000";
            }
            else if(res == -1)
            {
                // -1 代表沒找到, 就回傳空
                return "";
            }

            return res.ToString();
        }

    }
}
