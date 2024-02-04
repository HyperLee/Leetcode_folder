using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1043
{
    internal class Program
    {
        /// <summary>
        /// 1043. Partition Array for Maximum Sum
        /// https://leetcode.com/problems/partition-array-for-maximum-sum/description/?envType=daily-question&envId=2024-02-03
        /// 1043. 分隔数组以得到最大和
        /// https://leetcode.cn/problems/partition-array-for-maximum-sum/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 1, 15, 7, 9, 2, 5, 10 };
            int k = 3;

            Console.WriteLine(MaxSumAfterPartitioning(input, k));
            Console.ReadKey();
        }


        /// <summary>
        /// 官方: 往下拉有其他人解釋
        /// https://leetcode.cn/problems/partition-array-for-maximum-sum/solutions/2233208/fen-ge-shu-zu-yi-de-dao-zui-da-he-by-lee-mydv/
        /// 
        /// https://leetcode.cn/problems/partition-array-for-maximum-sum/solutions/2252362/1043-fen-ge-shu-zu-yi-de-dao-zui-da-he-b-2drw/
        /// 
        /// 
        /// d[j] + maxvalue * (i - j)
        /// 目前位置數值 + 目前範圍內最大值 * (長度)
        /// 
        /// 長度輸入最大是k,但是我們可以枚舉 1 ~ k 這個長度
        /// 
        /// 
        /// 因為很難把全部子字串組合都切割組合出來
        /// 所以把最大值乘上長度 比較快
        /// 
        /// 倒序遍历 j 的过程中可以顺便维护区间最大值
        /// 
        /// 動態規劃 dp
        /// 
        /// 用 maxVal 表示数组 arr 的下标范围 [j,i−1] 中的最大元素
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int MaxSumAfterPartitioning(int[] arr, int k)
        {
            int n = arr.Length;
            // arr 中前 i 個元素總和最大值
            int[] dp = new int[n + 1];

            for(int i = 1; i <= n; i++)
            {
                int maxvalue = arr[i - 1];

                // 倒序遍历 j; i範圍內往前推找出最大值
                for (int j = i - 1; j >= 0 && j >= i - k; j--)
                {
                    dp[i] = Math.Max(dp[i], dp[j] + maxvalue * (i - j));

                    if(j > 0)
                    {
                        maxvalue = Math.Max(maxvalue, arr[j - 1]);
                    }
                }
            }

            return dp[n];
        }

    }
}
