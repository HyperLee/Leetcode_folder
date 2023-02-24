using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1143
{
    internal class Program
    {
        /// <summary>
        /// leetcode 1143
        /// 動態規劃 題目
        /// https://leetcode.com/problems/longest-common-subsequence/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string text1 = "3";
            string text2 = "123";

            Console.WriteLine(LongestCommonSubsequence(text1, text2));

            Console.ReadKey();

        }



        /// <summary>
        /// https://leetcode.cn/problems/longest-common-subsequence/solution/zi-xu-lie-zi-shu-zu-du-shi-dpjing-dian-w-ih1i/
        /// 
        /// 本題需要推理 得出解題公式
        /// 請看官方 畫圖 推倒
        /// https://leetcode.cn/problems/longest-common-subsequence/solution/zui-chang-gong-gong-zi-xu-lie-by-leetcod-y7u0/
        /// </summary>
        /// <param name="text1"></param>
        /// <param name="text2"></param>
        /// <returns></returns>
        public static int LongestCommonSubsequence(string text1, string text2)
        {
            int[,] dp = new int[text1.Length + 1, text2.Length + 1];
            for (int i = 1; i <= text1.Length; i++)
            {
                for (int j = 1; j <= text2.Length; j++)
                {
                    if (text1[i - 1] == text2[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        // 這邊是動態規劃重點 
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                    }
                }
            }
            return dp[text1.Length, text2.Length];
        }


    }
}
