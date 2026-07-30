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
            Console.Write(RunSamples());
        }

        /// <summary>
        /// 執行內建的零錢兌換案例，逐筆比較預期結果與
        /// <see cref="CoinChange(int[], int)"/> 的實際結果。
        /// 案例涵蓋可兌換、不可兌換、金額為零、非排序面額、剛好兌換與重複面額；
        /// 方法不接收外部輸入，回傳每筆 PASS/FAIL 與通過總數的完整報告。
        /// </summary>
        /// <returns>包含所有案例結果與總結的多行文字。</returns>
        private static string RunSamples()
        {
            SampleCase[] samples =
            {
                new("官方範例一：可組成金額", new[] { 1, 2, 5 }, 11, 3),
                new("官方範例二：無法組成金額", new[] { 2 }, 3, -1),
                new("官方範例三：金額為零", new[] { 1 }, 0, 0),
                new("非排序面額的組合選擇", new[] { 2, 5, 10, 1 }, 27, 4),
                new("剛好使用兩枚相同面額", new[] { 3, 7 }, 14, 2),
                new("輸入包含重複面額", new[] { 1, 1, 2 }, 3, 2)
            };

            int passedCount = 0;
            List<string> outputLines = new List<string>();

            for (int i = 0; i < samples.Length; i++)
            {
                SampleCase sample = samples[i];
                int actual = CoinChange(sample.Coins, sample.Amount);
                bool passed = actual == sample.Expected;

                if (passed)
                {
                    passedCount++;
                }

                outputLines.Add($"案例 {i + 1}：{sample.Name}");
                outputLines.Add($"  硬幣：{FormatCoins(sample.Coins)}");
                outputLines.Add($"  金額：{sample.Amount}");
                outputLines.Add($"  預期：{sample.Expected}");
                outputLines.Add($"  實際：{actual} => {(passed ? "PASS" : "FAIL")}");
                outputLines.Add(string.Empty);
            }

            outputLines.Add($"總結：{passedCount}/{samples.Length} 筆驗證通過");
            return string.Join(Environment.NewLine, outputLines) + Environment.NewLine;
        }

        /// <summary>
        /// 將硬幣面額陣列格式化為方便閱讀的方括號表示。
        /// 輸入為案例使用的整數陣列，輸出依原順序保留所有面額，
        /// 並以逗號及空格分隔，例如 <c>[1, 2, 5]</c>。
        /// </summary>
        /// <param name="coins">要顯示的硬幣面額陣列。</param>
        /// <returns>保留輸入順序的陣列文字。</returns>
        private static string FormatCoins(int[] coins)
        {
            return $"[{string.Join(", ", coins)}]";
        }

        /// <summary>
        /// 計算以指定硬幣面額湊成目標金額所需的最少硬幣數。
        /// 解題概念是使用一維自底向上動態規劃，令 <c>dp[i]</c> 表示湊成金額
        /// <c>i</c> 的最少硬幣數，再由每個可用面額對應的較小金額轉移狀態。
        /// 輸入需符合題目條件：硬幣陣列長度為 1 到 12、面額皆為正整數，
        /// 且 <paramref name="amount"/> 介於 0 到 10,000；方法不會修改輸入陣列。
        /// 找到組合時回傳最少硬幣數，無法組成目標金額時回傳 <c>-1</c>。
        /// </summary>
        /// <param name="coins">可無限次使用的正整數硬幣面額。</param>
        /// <param name="amount">要湊成的非負目標金額。</param>
        /// <returns>最少硬幣數；無法湊成時為 <c>-1</c>。</returns>
        public static int CoinChange(int[] coins, int amount)
        {
            // 正面額下最多使用 amount 枚硬幣，因此 amount + 1 可安全表示尚不可達。
            int unreachable = amount + 1;
            int[] dp = new int[amount + 1];
            Array.Fill(dp, unreachable);
            dp[0] = 0;

            for (int currentAmount = 1; currentAmount <= amount; currentAmount++)
            {
                for (int j = 0; j < coins.Length; j++)
                {
                    if (coins[j] <= currentAmount)
                    {
                        // 使用目前硬幣後，問題縮小為已計算過的 currentAmount - coins[j]。
                        dp[currentAmount] = Math.Min(
                            dp[currentAmount],
                            dp[currentAmount - coins[j]] + 1);
                    }
                }
            }

            return dp[amount] == unreachable ? -1 : dp[amount];
        }

        /// <summary>
        /// 表示一筆可執行的零錢兌換驗證案例。
        /// 輸入包含案例名稱、正整數硬幣面額、非負目標金額與預期最少硬幣數；
        /// 建立後供案例驗證流程讀取，驗證過程不會修改其中的硬幣陣列。
        /// </summary>
        /// <param name="Name">案例名稱與涵蓋情境。</param>
        /// <param name="Coins">可用的硬幣面額。</param>
        /// <param name="Amount">要湊成的目標金額。</param>
        /// <param name="Expected">預期最少硬幣數；無法組成時為 <c>-1</c>。</param>
        private sealed record SampleCase(string Name, int[] Coins, int Amount, int Expected);
    }
}
