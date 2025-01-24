namespace leetcode_322
{
    internal class Program
    {
        /// <summary>
        /// 322. Coin Change
        /// https://leetcode.com/problems/coin-change/description/
        /// 
        /// 322. 零钱兑换
        /// https://leetcode.cn/problems/coin-change/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] coins = { 1, 2, 5 };
            int amount = 11;

            Console.WriteLine("res: " + CoinChange(coins, amount));
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/coin-change/solutions/2276775/322-ling-qian-dui-huan-by-stormsunshine-h9bx/
        /// https://leetcode.cn/problems/coin-change/solutions/132979/322-ling-qian-dui-huan-by-leetcode-solution/
        /// https://leetcode.cn/problems/coin-change/solutions/2119065/jiao-ni-yi-bu-bu-si-kao-dong-tai-gui-hua-21m5/
        /// 
        /// 採用動態規劃 官方解法
        /// 建議看連結說明 比較理解
        /// 
        /// ex:
        /// coins = [1, 2, 5], amount = 11
        /// 三種硬幣提供組合, 加總為 11
        /// 要使用最少硬幣數量
        /// 注意 i == 0 時, dp[i] = 0因為不需要硬幣
        /// 目前金額 i, 可以由 i - 1, i - 2, i - 5 組成
        /// 
        /// F(i)	最小硬币数量
        /// F(0)	0 //金额为0不能由硬币组成
        /// F(1)	1 //F(1)=min(F(1−1),F(1−2),F(1−5))+1=1
        /// F(2)	1 //F(2)=min(F(2−1),F(2−2),F(2−5))+1=1
        /// F(3)	2 //F(3)=min(F(3−1),F(3−2),F(3−5))+1=2
        /// F(4)	2 //F(4)=min(F(4−1),F(4−2),F(4−5))+1=2
        /// ...	...
        /// F(11)	3 //F(11)=min(F(11−1),F(11−2),F(11−5))+1=3
        /// 
        /// Copilot 解法說明
        /// 1. 定義硬幣面額的陣列 coins 和目標金額 amount。
        /// 2. 初始化一個 dp 陣列，大小為 amount + 1，並將所有元素設置為 amount + 1（表示無限大）。
        /// 3. 初始化 dp, 填充 max;設置 dp[0] 為 0，因為湊出 0 元不需要任何硬幣。
        /// 4. 使用兩層迴圈來填充 dp 陣列。外層迴圈遍歷每個金額，內層迴圈遍歷每個硬幣面額，並更新 dp 陣列中的值。
        /// 5. 最後，返回 dp[amount] 的值。如果 dp[amount] 大於 amount，則返回 -1，表示無法湊出目標金額；否則，返回 dp[amount] 的值。
        /// </summary>
        /// <param name="coins">提供數種硬幣</param>
        /// <param name="amount">金幣總額</param>
        /// <returns></returns>
        public static int CoinChange(int[] coins, int amount)
        {
            // max 表示最大值; 0 開始所以 + 1
            int max = amount + 1;
            // dp[i] 表示兑换 i 元所需的最少硬币数量;初始化一個 dp 陣列，大小為 amount + 1，並將所有元素設置為 amount + 1（表示無限大）。
            int[] dp = new int[amount + 1];
            // 初始化 dp, 填充 max
            Array.Fill(dp, max);
            // 兑换 0 元所需的最少硬币数量为 0
            dp[0] = 0;
            // 遍历 [1, amount] 所有金额
            for (int i = 1; i <= amount; i++)
            {
                // 遍历所有硬币
                for (int j = 0; j < coins.Length; j++)
                {
                    // 如果硬币面值小于等于 i
                    if (coins[j] <= i)
                    {
                        // dp[i] 表示兑换 i 元所需的最少硬币数量
                        dp[i] = Math.Min(dp[i], dp[i - coins[j]] + 1);
                    }
                }
            }

            // 如果 dp[amount] > amount 说明无法兑换
            return dp[amount] > amount ? -1 : dp[amount];
        }
    }
}
