using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1027
{
    internal class Program
    {
        /// <summary>
        /// 1027. Longest Arithmetic Subsequence
        /// https://leetcode.com/problems/longest-arithmetic-subsequence/
        /// 1027. 最长等差数列
        /// https://leetcode.cn/problems/longest-arithmetic-subsequence/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = new int[] { 24, 13, 1, 100, 0, 94, 3, 0, 3 };
            Console.WriteLine(LongestArithSeqLength2(input));

            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/longest-arithmetic-subsequence/solution/1027-zui-chang-deng-chai-shu-lie-by-stor-okka/
        /// 很奇怪有些網路解法
        /// 實際上放到VS2022 上都會有錯誤不能跑
        /// 但是 LEETCODE卻能過?
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int LongestArithSeqLength(int[] nums)
        {
            /*
            int maxNum = nums.Max();
            int minNum = nums.Min();
            int maxDiff = maxNum - minNum;
            int maxLength = 1;
            int n = nums.Length;
            int[][] dp = new int[n][];

            for (int i = 0; i < n; i++)
            {
                dp[i] = new int[maxDiff * 2 + 1];
                Array.Fill(dp[i], 1);
                
            }

            for (int i = 1; i < n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    int d = nums[i] - nums[j] + maxDiff;
                    dp[i][d] = Math.Max(dp[i][d], dp[j][d] + 1);
                    maxLength = Math.Max(maxLength, dp[i][d]);
                }
            }

            return maxLength;
            */
            return 0;
        }

        /// <summary>
        /// 有錯誤
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>

        public static int LongestArithSeqLength2(int[] nums)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();

            Array.Sort(nums);

            // 統計全部有多少個 diff
            for(int i = 1; i < nums.Length; i++)
            {
                int diff = 0;
                diff = nums[i] - nums[i - 1];

                if(dic.ContainsKey(diff))
                {
                    dic[diff]++;
                }
                else
                {
                    dic.Add(diff, i);
                }
            }


            int maxlength = 0;
            // 把每一個diff 丟進去跑一輪 找出最長的
            foreach(var value in dic.Keys)
            {
                int count = 1;
                for (int i = 1; i < nums.Length; i++)
                {
                    int diff = value;
                    if (nums[i] - nums[i - 1] == diff)
                    {
                        count++;
                    }
                }

                maxlength = Math.Max(maxlength, count);
                count = 1;
            }

            return maxlength;
        }

    }
}
