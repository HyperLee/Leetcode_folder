namespace leetcode_115;

class Program
{
    /// <summary>
    /// 115. Distinct Subsequences
    /// https://leetcode.com/problems/distinct-subsequences/description/
    /// 115. 不同的子序列
    /// https://leetcode.cn/problems/distinct-subsequences/description/
    /// English (Original):
    /// Given two strings s and t, return the number of distinct subsequences of s which equals t.
    /// The test cases are generated so that the answer fits in a 32-bit signed integer.
    ///
    /// 繁體中文：
    /// 給定兩個字串 s 和 t，請回傳 s 中等於 t 的不同子序列數量。
    /// 測試案例保證答案符合 32 位元有號整數的範圍。
    /// </summary>
    /// <remarks>
    /// 程式進入點不讀取命令列或主控台輸入，會執行六組固定案例，
    /// 並輸出每組案例的 PASS/FAIL 與最後的通過數量總結。
    /// </remarks>
    /// <param name="args">命令列參數；本程式不使用任何命令列輸入。</param>
    static void Main(string[] args)
    {
        Program solver = new Program();
        (string name, string s, string t, int expected)[] testCases =
        {
            ("官方範例 1", "rabbbit", "rabbit", 3),
            ("官方範例 2", "babgbag", "bag", 5),
            ("完全相同", "abc", "abc", 1),
            ("重複字元", "aaa", "aa", 3),
            ("空目標（DP 邊界）", "abc", "", 1),
            ("s 短於 t", "ab", "abc", 0)
        };

        Console.WriteLine("=== 115. Distinct Subsequences ===");

        int passedCount = 0;
        foreach ((string name, string s, string t, int expected) in testCases)
        {
            if (RunTestCase(solver, name, s, t, expected))
            {
                passedCount++;
            }
        }

        int totalCount = testCases.Length;
        Console.WriteLine($"總結：{passedCount}/{totalCount} 通過，{totalCount - passedCount} 個失敗。");
        Environment.ExitCode = passedCount == totalCount ? 0 : 1;
    }

    /// <summary>
    /// 執行一組固定測試案例，呼叫 NumDistinct 並列印輸入、預期結果、
    /// 實際結果與 PASS/FAIL。輸入是案例名稱、兩個字串與預期的不同子序列數量；
    /// 回傳實際結果是否等於預期值。
    /// </summary>
    /// <param name="solver">包含 NumDistinct 解法的 Program 實例。</param>
    /// <param name="name">測試案例名稱；僅供主控台輸出辨識。</param>
    /// <param name="s">來源字串；測試案例可包含官方限制內的字串或額外邊界案例。</param>
    /// <param name="t">目標字串；方法會計算它在 s 的不同子序列選法數量。</param>
    /// <param name="expected">案例預期的不同子序列數量。</param>
    /// <returns>實際結果與預期值相同時回傳 true，否則回傳 false。</returns>
    private static bool RunTestCase(Program solver, string name, string s, string t, int expected)
    {
        int actual = solver.NumDistinct(s, t);
        bool passed = actual == expected;

        Console.WriteLine(
            $"{name}：s = \"{s}\"，t = \"{t}\"，預期：{expected}，實際：{actual}，結果：{(passed ? "PASS" : "FAIL")}");

        return passed;
    }

    /// <summary>
    /// 使用動態規劃計算字串 <paramref name="s"/> 的所有子序列中，
    /// 等於字串 <paramref name="t"/> 的不同子序列數量。
    ///
    /// 定義 dp[i, j] 表示在 s[i:] 的子序列中，t[j:] 出現的次數。
    ///
    /// 邊界條件：
    /// 1. 當 j == t.Length 時，t[j:] 為空字串。
    ///    空字串是任何字串的子序列，因此 dp[i, t.Length] = 1。
    /// 2. 當 i == s.Length 且 j < t.Length 時，s[i:] 為空字串，
    ///    無法組成非空的 t[j:]，因此 dp[s.Length, j] = 0。
    ///
    /// 狀態轉移：
    /// 1. 當 s[i] == t[j] 時，可以選擇：
    ///    - 使用 s[i] 與 t[j] 配對：dp[i + 1, j + 1]
    ///    - 跳過 s[i]：dp[i + 1, j]
    ///    因此 dp[i, j] = dp[i + 1, j + 1] + dp[i + 1, j]。
    ///
    /// 2. 當 s[i] != t[j] 時，s[i] 無法與 t[j] 配對，只能跳過 s[i]，
    ///    因此 dp[i, j] = dp[i + 1, j]。
    ///
    /// 最終 dp[0, 0] 即為 t 在 s 的所有子序列中出現的總次數。
    ///
    /// 時間複雜度：O(m * n)。
    /// 空間複雜度：O(m * n)。
    /// 其中 m 為 s.Length，n 為 t.Length。
    /// </summary>
    /// <param name="s">來源字串，用來選擇子序列。</param>
    /// <param name="t">目標字串。</param>
    /// <returns>字串 <paramref name="s"/> 的子序列中，等於 <paramref name="t"/> 的不同子序列數量。</returns>
    public int NumDistinct(string s, string t)
    {
        int m = s.Length;
        int n = t.Length;

        // 來源字串比目標字串短時，沒有足夠字元可以完成 t，答案必定是 0。
        if(m < n)
        {
            return 0;
        }

        int[,] dp = new int[m + 1, n + 1];

        // dp[i, n] 代表從 s[i:] 組成空目標的方式；不選任何字元也是唯一一種方式。
        for(int i = 0; i <= m; i++)
        {
            dp[i, n] = 1;
        }

        // 由右下往左上填表，讓轉移所需的 dp[i + 1, j] 與 dp[i + 1, j + 1] 已先完成。
        for(int i = m - 1; i >= 0; i--)
        {
            char sChar = s[i];

            for(int j = n - 1; j >= 0; j--)
            {
                char tChar = t[j];

                if(sChar == tChar)
                {
                    // 字元相等時可使用 s[i] 配對，或跳過 s[i] 尋找其他配對位置。
                    dp[i, j] = dp[i + 1, j + 1] + dp[i + 1, j];
                }
                else
                {
                    // 字元不同時不能配對，只能跳過目前的來源字元。
                    dp[i, j] = dp[i + 1, j];
                }
            }
        }

        return dp[0, 0];
    }
}
