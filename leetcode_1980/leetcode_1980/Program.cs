using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1980
{
    internal class Program
    {
        /// <summary>
        /// 1980. Find Unique Binary String
        /// https://leetcode.com/problems/find-unique-binary-string/?envType=daily-question&envId=2023-11-16
        /// 1980. 找出不同的二进制字符串
        /// https://leetcode.cn/problems/find-unique-binary-string/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] nums = { "111", "011", "001" };
            Console.WriteLine(FindDifferentBinaryString(nums));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.com/problems/find-unique-binary-string/solutions/4292885/c-solution-for-find-unique-binary-string-problem/?envType=daily-question&envId=2023-11-16
        /// https://leetcode.cn/problems/find-unique-binary-string/solutions/951216/kang-tuo-dui-jiao-xian-by-seedjyh-wr2s/
        /// https://sweetkikibaby.pixnet.net/blog/post/191310453
        /// 
        /// nums = ["01","10"]
        /// 型別[] 陣列名稱 = new 型別[列數,行數];
        /// nums[0, 0] = 0; nums[0, 1] = 1;
        /// nums[1, 0] = 1; nums[1, 1] = 0;
        /// 
        /// 有人說這是 康托对角线 
        /// 只要和第i个串下标i的字符nums[i][i]不同，构造出来的串就和所有的串都不同。
        /// 只限于串数不超过串长的情况。
        /// 
        /// 之後再參考一下,
        /// 但是好像不是萬能使用
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>

        public static string FindDifferentBinaryString(string[] nums)
        {
            string res = "";

            int n = nums.Length;

            for (int i = 0; i < n; i++)
            {
                string a = nums[i][i].ToString();
                if (nums[i][i] == '0')
                {
                    res += '1';
                }
                else
                {
                    res += '0';
                }
            }

            return res;
        }


    }
}
