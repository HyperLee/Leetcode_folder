namespace leetcode_1578;

/// <summary>
/// 程式入口與 LeetCode 1578 題目實作
/// 解題思路：對於每個連續同色段，只能保留一個氣球（任意相鄰不相同），
/// 因此貪心地保留該段中移除耗時最大的那個氣球，將其他氣球移除，
/// 等價於將 neededTime 的總和減去每段的最大值。
/// 時間複雜度：O(n)，一次掃描；空間複雜度：O(1)。
/// </summary>
class Program
{
    /// <summary>
    /// 1578. Minimum Time to Make Rope Colorful
    /// https://leetcode.com/problems/minimum-time-to-make-rope-colorful/description/?envType=daily-question&envId=2025-11-03
    /// 1578. 使绳子变成彩色的最短时间
    /// https://leetcode.cn/problems/minimum-time-to-make-rope-colorful/description/?envType=daily-question&envId=2025-11-03
    /// 
    /// 題目描述：
    /// Alice 有 n 個氣球排成一條繩子。給定一個 0-indexed 字串 colors，colors[i] 表示第 i 個氣球的顏色。
    /// Alice 希望繩子是彩色的，不希望有兩個相鄰的氣球顏色相同，因此她請 Bob 幫忙。Bob 可以移除一些氣球使繩子變得彩色。
    /// 給定一個 0-indexed 整數陣列 neededTime，neededTime[i] 表示 Bob 移除第 i 個氣球所需的時間（秒）。
    /// 請回傳 Bob 使繩子變彩色所需的最少時間。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var tests = new (string colors, int[] times, int expected)[]
        {
            ("aab", new int[] {1,2,3}, 1),              // 範例：移除 index 0 的 'a'，耗時 1
            ("abbba", new int[] {1,3,2,4,1}, 5),       // 中間 "bbb" 需移除 3+2 = 5
            ("aaaa", new int[] {1,1,1,1}, 3),          // 全部相同，保留最大值 1，移除其餘 3
            ("abc", new int[] {1,2,3}, 0),             // 全不相同，不需移除
            ("a", new int[] {5}, 0),                   // 單一氣球，不需移除
            ("aaabbb", new int[] {2,3,4,1,2,3}, 8),    // 多段測試
            ("", new int[] { }, 0)                     // 空輸入（保守處理）
        };

        var solver = new Program();
        foreach (var (colors, times, expected) in tests)
        {
            int ans = solver.MinCost(colors, times);
            string timesStr = times.Length == 0 ? "[]" : ("[" + string.Join(",", times) + "]");
            Console.WriteLine($"colors=\"{colors}\", neededTime={timesStr} => min cost: {ans} (expected {expected})");
        }
    }

    /// <summary>
    /// 計算讓繩子變彩色（相鄰顏色不得相同）所需的最少移除時間。
    /// 解法要點：
    /// - 把整個字串拆成數個連續同色的區段（segment）。
    /// - 對於每個區段，為了讓該區段只剩一個氣球，需移除區段內除了耗時最大的那個以外的所有氣球，
    ///   等價於該區段所有 neededTime 的總和減去該區段的最大值。
    /// - 因此整體答案 = neededTime 總和 - 所有區段最大值之和（或等價地在掃描時累加每段除最大值外的值）。
    ///
    /// 範例運算流程（colors = "aab", neededTime = [1,2,3]）：
    /// - 分段: "aa" (indices 0..1), "b" (index 2)
    /// - 第 1 段總和 = 1 + 2 = 3，最大值 = 2，需移除 = 3 - 2 = 1
    /// - 第 2 段只有一個氣球，不需移除
    /// - 答案 = 1
    ///
    /// 時間複雜度：O(n)，空間複雜度：O(1)
    /// </summary>
    /// <param name="colors">顏色字串，長度與 neededTime 相同</param>
    /// <param name="neededTime">移除每個氣球所需的時間</param>
    /// <returns>使繩子彩色的最少總時間</returns>
    public int MinCost(string colors, int[] neededTime)
    {
        int n = neededTime.Length;
        int res = 0;
        int maxTime = 0;

        for (int i = 0; i < n; i++)
        {
            int t = neededTime[i];
            // 我們先把每個氣球的耗時加入總和，之後在每個連續同色段結束時扣掉該段的最大耗時，
            // 等價於只保留該段最大耗時的那顆氣球。
            res += t;
            // track current segment's maximum removal time
            maxTime = Math.Max(maxTime, t);

            // 若到達字串末尾或下一個顏色不同，表示目前同色段結束
            if (i == n - 1 || colors[i] != colors[i + 1])
            {
                // 扣掉該段最大耗時 (代表保留該段耗時最大的氣球，不移除它)
                res -= maxTime;
                // 重置 maxTime，準備下一段
                maxTime = 0;
            }
        }
        return res;
    }
}
