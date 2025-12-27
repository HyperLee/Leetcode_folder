namespace leetcode_1143;

class Program
{
    /// <summary>
    /// 1143. Longest Common Subsequence
    /// https://leetcode.com/problems/longest-common-subsequence/description/
    /// 1143. 最長公共子序列
    /// https://leetcode.cn/problems/longest-common-subsequence/description/
    ///
    /// 題目（繁體中文）：
    /// 給定兩個字串 text1 和 text2，回傳它們最長公共子序列的長度。
    /// 如果沒有公共子序列，則回傳 0。
    ///
    /// 子序列是從原字串刪除某些字元（可以不刪除任何字元）且不改變其餘字元的相對順序所產生的新字串。
    /// 例如，"ace" 是 "abcde" 的一個子序列。
    /// 公共子序列是同時為兩個字串的子序列的字串。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試案例 1：基本測試
        string text1_1 = "abcde";
        string text2_1 = "ace";
        int result1 = solution.LongestCommonSubsequence(text1_1, text2_1);
        Console.WriteLine($"測試案例 1: text1=\"{text1_1}\", text2=\"{text2_1}\"");
        Console.WriteLine($"結果: {result1} (預期: 3，LCS 為 \"ace\")");
        Console.WriteLine();

        // 測試案例 2：完全不同的字串
        string text1_2 = "abc";
        string text2_2 = "def";
        int result2 = solution.LongestCommonSubsequence(text1_2, text2_2);
        Console.WriteLine($"測試案例 2: text1=\"{text1_2}\", text2=\"{text2_2}\"");
        Console.WriteLine($"結果: {result2} (預期: 0，無公共子序列)");
        Console.WriteLine();

        // 測試案例 3：有重複字元
        string text1_3 = "bl";
        string text2_3 = "yby";
        int result3 = solution.LongestCommonSubsequence(text1_3, text2_3);
        Console.WriteLine($"測試案例 3: text1=\"{text1_3}\", text2=\"{text2_3}\"");
        Console.WriteLine($"結果: {result3} (預期: 1，LCS 為 \"b\")");
        Console.WriteLine();

        // 測試案例 4：較長字串
        string text1_4 = "AGGTAB";
        string text2_4 = "GXTXAYB";
        int result4 = solution.LongestCommonSubsequence(text1_4, text2_4);
        Console.WriteLine($"測試案例 4: text1=\"{text1_4}\", text2=\"{text2_4}\"");
        Console.WriteLine($"結果: {result4} (預期: 4，LCS 為 \"GTAB\")");
        Console.WriteLine();

        // 測試案例 5：相同字串
        string text1_5 = "abc";
        string text2_5 = "abc";
        int result5 = solution.LongestCommonSubsequence(text1_5, text2_5);
        Console.WriteLine($"測試案例 5: text1=\"{text1_5}\", text2=\"{text2_5}\"");
        Console.WriteLine($"結果: {result5} (預期: 3，LCS 為 \"abc\")");
    }

    /// <summary>
    /// 計算兩個字串的最長公共子序列 (Longest Common Subsequence, LCS) 長度。
    /// 
    /// <para><b>解題思路：</b></para>
    /// <para>使用動態規劃 (Dynamic Programming) 方法，建立一個二維 DP 陣列來儲存子問題的解。</para>
    /// 
    /// <para><b>狀態定義：</b></para>
    /// <para>dp[i, j] 表示 text1 前 i 個字元與 text2 前 j 個字元的最長公共子序列長度。</para>
    /// 
    /// <para><b>狀態轉移方程式：</b></para>
    /// <list type="bullet">
    ///   <item>
    ///     <description>若 text1[i-1] == text2[j-1]，則 dp[i,j] = dp[i-1,j-1] + 1（找到一個公共字元）</description>
    ///   </item>
    ///   <item>
    ///     <description>若 text1[i-1] != text2[j-1]，則 dp[i,j] = max(dp[i-1,j], dp[i,j-1])（取兩種情況的最大值）</description>
    ///   </item>
    /// </list>
    /// 
    /// <para><b>時間複雜度：</b>O(m × n)，其中 m 和 n 分別為兩字串的長度。</para>
    /// <para><b>空間複雜度：</b>O(m × n)，用於儲存 DP 陣列。</para>
    /// </summary>
    /// <param name="text1">第一個輸入字串</param>
    /// <param name="text2">第二個輸入字串</param>
    /// <returns>兩字串的最長公共子序列長度</returns>
    /// <example>
    /// <code>
    /// var solution = new Program();
    /// int result = solution.LongestCommonSubsequence("abcde", "ace");
    /// // result = 3，因為 "ace" 是最長公共子序列
    /// </code>
    /// </example>
    public int LongestCommonSubsequence(string text1, string text2)
    {
        // 建立 DP 陣列，大小為 (m+1) x (n+1)，多出的一行一列用於處理邊界條件（空字串情況）
        int[,] dp = new int[text1.Length + 1, text2.Length + 1];

        // 遍歷 text1 的每個字元（從索引 1 開始，因為索引 0 代表空字串）
        for (int i = 1; i <= text1.Length; i++)
        {
            // 遍歷 text2 的每個字元
            for (int j = 1; j <= text2.Length; j++)
            {
                // 情況一：當前字元相同
                // text1[i-1] 與 text2[j-1] 匹配，LCS 長度 = 左上角的值 + 1
                if (text1[i - 1] == text2[j - 1])
                {
                    dp[i, j] = dp[i - 1, j - 1] + 1;
                }
                // 情況二：當前字元不同
                // 取「text1 少一個字元」或「text2 少一個字元」兩種情況的最大值
                else
                {
                    dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                }
            }
        }

        // 回傳右下角的值，即為完整字串的 LCS 長度
        return dp[text1.Length, text2.Length];
    }
}
