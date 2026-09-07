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
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
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
        if(m < n)
        {
            return 0;
        }

        int[,] dp = new int[m + 1, n + 1];

        for(int i = 0; i <= m; i++)
        {
            dp[i, n] = 1;
        }

        for(int i = m - 1; i >= 0; i--)
        {
            char sChar = s[i];

            for(int j = n - 1; j >= 0; j--)
            {
                char tChar = t[j];

                if(sChar == tChar)
                {
                    dp[i, j] = dp[i + 1, j + 1] + dp[i + 1, j];
                }
                else
                {
                    dp[i, j] = dp[i + 1, j];
                }
            }
        }

        return dp[0, 0];
    }
}
