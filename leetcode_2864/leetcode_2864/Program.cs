using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2864
{
    internal class Program
    {
        /// <summary>
        /// 2864. Maximum Odd Binary Number
        /// https://leetcode.com/problems/maximum-odd-binary-number/description/?envType=daily-question&envId=2024-03-01
        /// 2864. 最大二进制奇数
        /// https://leetcode.cn/problems/maximum-odd-binary-number/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "010";
            Console.WriteLine(MaximumOddBinaryNumber(s));
            Console.ReadKey();

        }


        /// <summary>
        /// 重點就是 一個 1 要放在 最尾端 個數位
        /// 其餘的1要放在越前面越好(位數越大者)
        /// 這樣就能創造出 最大奇數
        /// 
        /// 題目說保底有一個1
        /// 所以 最後塞1
        /// 回傳是二進位 注意
        /// 
        /// 前面能塞多少1, 就要看輸入的字串有多少個1能用
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string MaximumOddBinaryNumber(string s)
        {
            // 統計幾個 1
            int ones = 0;
            // 統計幾個0
            int zeros = 0;
            int length = s.Length;

            for(int i = 0; i < length; i++)
            {
                if (s[i] == '1') 
                {
                    ones++;
                }
            }

            // 字串只有0跟1, 長度扣除1之後就是0數量
            zeros = length - ones;
            StringBuilder sb = new StringBuilder();

            // 先塞 1; 保底一個1要放到尾端, 其餘放前面
            for(int i = 0; i < ones - 1; i++)
            {
                sb.Append("1");
            }
            
            // 在塞 0
            for(int i = 0; i < zeros; i++)
            {
                sb.Append("0");
            }

            // 最後塞 1, 保底一個 1.
            sb.Append("1");

            return sb.ToString();

        }

    }
}
