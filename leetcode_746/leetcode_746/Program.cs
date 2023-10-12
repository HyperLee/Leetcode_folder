using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_746
{
    internal class Program
    {
        /// <summary>
        /// 746. Min Cost Climbing Stairs
        /// https://leetcode.com/problems/min-cost-climbing-stairs/?envType=study-plan-v2&envId=leetcode-75
        /// 746. 使用最小花费爬楼梯
        /// https://leetcode.cn/problems/min-cost-climbing-stairs/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] cost = { 10, 15, 20 };
            Console.WriteLine("method1: " + MinCostClimbingStairs(cost));
            Console.WriteLine("method2: " + MinCostClimbingStairs2(cost));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/min-cost-climbing-stairs/solutions/528955/shi-yong-zui-xiao-hua-fei-pa-lou-ti-by-l-ncf8/
        /// 
        /// dp[i]取位置, cost[i]取花費
        /// 計算出到達dp[i]位置所需要之最小花費
        /// 
        /// 未優化解法
        /// </summary>
        /// <param name="cost"></param>
        /// <returns></returns>
        public static int MinCostClimbingStairs(int[] cost)
        {
            int n = cost.Length;
            int[] dp = new int[n + 1];
            dp[0] = dp[1] = 0;

            for(int i = 2; i <= n; i++)
            {
                // dp[i]取位置, cost[i]取花費
                dp[i] = Math.Min(dp[i - 1] + cost[i - 1], dp[i - 2] + cost[i - 2]);
            }

            return dp[n];
        }


        /// <summary>
        /// 優化解法
        /// 取出前一個計算值使用
        /// 不用每次都從頭累加
        /// 
        /// 
        /// 節省計算量
        /// </summary>
        /// <param name="cost"></param>
        /// <returns></returns>
        public static int MinCostClimbingStairs2(int[] cost)
        {
            int n = cost.Length;
            int prev = 0, cur = 0;
            for(int i = 2; i <= n;i++) 
            {
                int next = Math.Min(cur + cost[i - 1], prev + cost[i - 2]);
                prev = cur;
                cur = next;
            }

            return cur;
        }

    }
}
