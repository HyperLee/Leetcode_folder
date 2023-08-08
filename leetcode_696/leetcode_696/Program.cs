using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_696
{
    internal class Program
    {
        /// <summary>
        /// 696. Count Binary Substrings
        /// https://leetcode.com/problems/count-binary-substrings/
        /// 696. 计数二进制子串
        /// https://leetcode.cn/problems/count-binary-substrings/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "001100";
            Console.WriteLine(CountBinarySubstrings(s));
            Console.ReadKey();
        }



        /// <summary>
        /// https://leetcode.cn/problems/count-binary-substrings/solution/696-ji-shu-er-jin-zhi-zi-chuan-by-storms-ttqv/
        /// https://leetcode.cn/problems/count-binary-substrings/solution/696-ji-shu-er-jin-zhi-zi-chuan-by-shen-d-wzef/
        /// 
        /// 记录和当前位置数字相同且连续的长度cur，以及其之前连续的不同数字的 长度pre
        /// 如果当前字符和前一个字符相等，则cur++
        /// 如果不相等，则取pre和cur的最小值，此最小值，就是可以拼成满足条件的字串个数
        /// 
        /// 
        /// 跨越两个相邻的分组的子串数量是这两个分组的大小中的较小值。
        /// 基于上述结论，可以得到如下解法：遍历字符串 ss 并得到每个分组的大小
        /// ，然后遍历每一对相邻的分组
        /// ，将其中的较小的分组大小加到答案中，遍历结束后即可得到答案。
        /// 
        /// 
        /// 有看沒懂
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int CountBinarySubstrings(string s)
        {
            int count = 0;
            int prev = 0;
            int curr = 1;
            int length = s.Length;

            for(int i = 1; i < length; i++)
            {
                if (s[i - 1] == s[i])
                {
                    curr++;
                }
                else
                {
                    count += Math.Min(prev, curr);
                    prev = curr;
                    curr = 1;
                }
            }

            count += Math.Min(prev, curr);
            return count;

        }


    }
}
