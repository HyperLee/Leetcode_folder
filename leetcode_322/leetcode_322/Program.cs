using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_322
{
    internal class Program
    {
        /// <summary>
        /// 322. Coin Change
        /// https://leetcode.com/problems/coin-change/?envType=study-plan-v2&envId=top-interview-150
        /// 322. 零钱兑换
        /// https://leetcode.cn/problems/coin-change/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
        }

        const int INFINITY = int.MaxValue / 2;
        /// <summary>
        /// https://leetcode.cn/problems/coin-change/solutions/2276775/322-ling-qian-dui-huan-by-stormsunshine-h9bx/
        /// 寫之前預計應該是 DP演算法
        /// 但是怎麼做才是問題
        /// 
        /// </summary>
        /// <param name="coins"></param>
        /// <param name="amount"></param>
        /// <returns></returns>

        public int CoinChange(int[] coins, int amount)
        {
            int n = coins.Length;
            int[][] dp = new int[n + 1][];

            for(int i = 0; i <= n; i++)
            {
                dp[i] = new int[amount + 1];
            }

            Array.Fill(dp[0], INFINITY);

            dp[0][0] = 0;

            for(int i = 1; i <= n; i++)
            {
                for(int j = 0; j <= amount; j++)
                {
                    int mincoins = INFINITY;

                    int maxcount = j / coins[i - 1];

                    for(int k = 0; k <= maxcount; k++)
                    {
                        mincoins = Math.Min(mincoins, dp[i - 1][j - coins[i - 1] * k] + k);
                    }

                    dp[i][j] = mincoins;
                }
            }

            return dp[n][amount] != INFINITY ? dp[n][amount] : -1;

        }


    }
}
