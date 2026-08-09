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
    /// <para>
    /// 1578. Minimum Time to Make Rope Colorful
    /// https://leetcode.com/problems/minimum-time-to-make-rope-colorful/description/
    ///
    /// Alice has n balloons arranged on a rope. You are given a 0-indexed string colors where colors[i] is the color of the
    /// i-th balloon. Alice wants the rope to be colorful: no two consecutive balloons may have the same color. Bob can remove
    /// some balloons. You are also given a 0-indexed integer array neededTime, where neededTime[i] is the number of seconds Bob
    /// needs to remove the i-th balloon. Return the minimum time Bob needs to make the rope colorful.
    ///
    /// Example 1:
    /// Image: https://assets.leetcode.com/uploads/2021/12/13/ballon1.jpg
    /// Input: colors = "abaac", neededTime = [1,2,3,4,5]
    /// Output: 3
    /// Explanation: In the image, 'a' is blue, 'b' is red and 'c' is green. Remove the blue balloon at index 2, taking
    /// 3 seconds. No two consecutive balloons then have the same color. Total time = 3.
    ///
    /// Example 2:
    /// Image: https://assets.leetcode.com/uploads/2021/12/13/balloon2.jpg
    /// Input: colors = "abc", neededTime = [1,2,3]
    /// Output: 0
    /// Explanation: The rope is already colorful, so Bob removes no balloons.
    ///
    /// Example 3:
    /// Image: https://assets.leetcode.com/uploads/2021/12/13/balloon3.jpg
    /// Input: colors = "aabaa", neededTime = [1,2,3,4,1]
    /// Output: 2
    /// Explanation: Remove balloons at indices 0 and 4. Each takes 1 second. Total time = 1 + 1 = 2.
    ///
    /// Constraints:
    /// - n == colors.length == neededTime.length
    /// - 1 &lt;= n &lt;= 10^5
    /// - 1 &lt;= neededTime[i] &lt;= 10^4
    /// - colors contains only lowercase English letters.
    /// </para>
    /// <para>
    /// 1578. 使繩子變成彩色的最短時間
    /// https://leetcode.cn/problems/minimum-time-to-make-rope-colorful/description/
    ///
    /// Alice 有 n 個氣球排列在一條繩子上。給定從 0 開始索引的字串 colors，其中 colors[i] 是第 i 個氣球
    /// 的顏色。Alice 希望繩子是彩色的，也就是任意兩個相鄰氣球都不能同色。Bob 可以移除一些氣球。
    /// 另給定從 0 開始索引的整數陣列 neededTime，其中 neededTime[i] 是 Bob 移除第 i 個氣球所需的秒數。
    /// 回傳 Bob 使繩子變彩色所需的最少時間。
    ///
    /// 範例 1：
    /// 圖片：https://assets.leetcode.com/uploads/2021/12/13/ballon1.jpg
    /// 輸入：colors = "abaac"，neededTime = [1,2,3,4,5]
    /// 輸出：3
    /// 解釋：圖中 'a' 為藍色、'b' 為紅色、'c' 為綠色。移除索引 2 的藍色氣球，耗時 3 秒；之後不再有
    /// 兩個相鄰氣球同色。總時間 = 3。
    ///
    /// 範例 2：
    /// 圖片：https://assets.leetcode.com/uploads/2021/12/13/balloon2.jpg
    /// 輸入：colors = "abc"，neededTime = [1,2,3]
    /// 輸出：0
    /// 解釋：繩子已經是彩色的，Bob 不需要移除任何氣球。
    ///
    /// 範例 3：
    /// 圖片：https://assets.leetcode.com/uploads/2021/12/13/balloon3.jpg
    /// 輸入：colors = "aabaa"，neededTime = [1,2,3,4,1]
    /// 輸出：2
    /// 解釋：移除索引 0 與 4 的氣球，每個耗時 1 秒；總時間 = 1 + 1 = 2。
    ///
    /// 限制條件：
    /// - n == colors.length == neededTime.length
    /// - 1 &lt;= n &lt;= 10^5
    /// - 1 &lt;= neededTime[i] &lt;= 10^4
    /// - colors 只包含小寫英文字母。
    /// </para>
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
