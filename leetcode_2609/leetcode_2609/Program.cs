using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2609
{
    internal class Program
    {
        /// <summary>
        /// 2609. Find the Longest Balanced Substring of a Binary String
        /// https://leetcode.com/problems/find-the-longest-balanced-substring-of-a-binary-string/
        /// 2609. 最长平衡子字符串
        /// https://leetcode.cn/problems/find-the-longest-balanced-substring-of-a-binary-string/?envType=daily-question&envId=Invalid%20Date
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "00111";
            Console.WriteLine(FindTheLongestBalancedSubstring(s));
            Console.ReadKey();
        }


        /// <summary>
        /// 官方:
        /// https://leetcode.cn/problems/find-the-longest-balanced-substring-of-a-binary-string/solutions/2515219/zui-chang-ping-heng-zi-zi-fu-chuan-by-le-yje7/?envType=daily-question&envId=Invalid+Date
        /// 
        /// https://leetcode.cn/problems/find-the-longest-balanced-substring-of-a-binary-string/solutions/2517437/gong-shui-san-xie-on-shi-jian-o1-kong-ji-i8e7/?envType=daily-question&envId=Invalid+Date
        /// 
        /// count[0] : 遇到0 就累加
        /// count[1] : 遇到1 就累加
        /// 
        /// 只需通过计算 2×min⁡(count[0],count[1]) 来计算这个平衡字符串的长度。
        /// 在 a 和 b 中取较小值，进行乘 222 操作，作为当前平衡子字符串的长度，
        /// 
        /// 一個0 + 一個1 惟一組所以長度會是偶數 才會乘以2
        /// 
        /// 這行是重點, 取小值類似 0和1之間取and 交集 意思
        /// Math.Min(count[0], count[1])
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int FindTheLongestBalancedSubstring(string s)
        {
            int res = 0;
            int n = s.Length;
            int[] count = new int[2];

            for(int i = 0; i < n; i++)
            {
                if (s[i] == '1')
                {
                    // 遇到1
                    count[1]++;
                    // 這行是重點, 取小值類似 0和1之間取and 交集 意思
                    int aa = Math.Min(count[0], count[1]);
                    res = Math.Max(res, 2 * Math.Min(count[0], count[1]) );
                }
                else if(i == 0 || s[i - 1] == '1')
                {
                    // 開頭0, 長度1
                    // 或是長度 >= 2,  當 0 前面有1 類似 10
                    count[0] = 1;
                    count[1] = 0;
                }
                else
                {
                    // 就是遇到 0
                    count[0]++;
                }
            }

            return res;

        }
    }
}
