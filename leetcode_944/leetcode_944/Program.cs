using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_944
{
    internal class Program
    {
        /// <summary>
        /// leetcode 944
        /// https://leetcode.com/problems/delete-columns-to-make-sorted/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] strs = { "cba", "daf", "ghi" };

            Console.WriteLine(MinDeletionSize(strs));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/delete-columns-to-make-sorted/solution/shan-lie-zao-xu-by-leetcode-solution-bqyy/
        /// 题目要求删除不是按字典序升序排列的列，由于每个字符串的长度都相等，我们可以逐列访问字符串数组，
        /// 统计不是按字典序升序排列的列。
        /// 对于第 j 列的字符串，我们需要检测所有相邻字符是否均满足 strs[i−1][j]≤strs[i][j]。
        /// 
        /// 上到下 的英文字母排序是 由小到大
        /// 只要違反這原則 就要刪除
        /// 
        /// ex:
        /// strs = ["abc", "bce", "cae"]
        /// abc
        /// bce
        /// cae
        /// 
        /// 第一列(bce)不再原則 所以要刪除
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static int MinDeletionSize(string[] strs)
        {
            int row = strs.Length; // 橫
            int col = strs[0].Length; // 直
            int ans = 0;

            // 由左至右, 由上至下 全部找過一次
            for (int j = 0; j < col; ++j)
            {
                for (int i = 1; i < row; ++i)
                {
                    if (strs[i - 1][j] > strs[i][j])
                    {
                        ans++;
                        break;
                    }
                }
            }
            return ans;
        }

    }
}
