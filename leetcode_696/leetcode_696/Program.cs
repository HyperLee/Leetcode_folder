namespace leetcode_696;

class Program
{
    /// <summary>
    /// 696. Count Binary Substrings
    /// https://leetcode.com/problems/count-binary-substrings/description/?envType=daily-question&envId=2026-02-19
    /// (LeetCode 中文頁面)
    /// https://leetcode.cn/problems/count-binary-substrings/description/?envType=daily-question&envId=2026-02-19
    ///
    /// English:
    /// Given a binary string s, return the number of non-empty substrings that have the same number of 0's and 1's,
    /// and all the 0's and all the 1's in these substrings are grouped consecutively.
    /// Substrings that occur multiple times are counted the number of times they occur.
    ///
    /// 繁體中文:
    /// 給定一個二元（binary）字串 `s`，回傳符合下列條件的非空子字串數量：
    /// - 子字串中 0 與 1 的數量相同
    /// - 在該子字串內，所有 0 與所有 1 各自連續成群（即不交錯）
    /// 相同字串若在不同位置出現多次，則每次都要計數。
    /// </summary>
    /// <param name="args">程式參數（未使用）</param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試案例 1：s = "00110011" → 預期輸出 6
        // 分組："00" | "11" | "00" | "11"
        // 相鄰分組對的 min：min(2,2)=2、min(2,2)=2、min(2,2)=2 → 共 6
        string s1 = "00110011";
        Console.WriteLine($"輸入: \"{s1}\" → 輸出: {solution.CountBinarySubstrings(s1)}"); // 預期: 6

        // 測試案例 2：s = "10101" → 預期輸出 4
        // 分組："1" | "0" | "1" | "0" | "1"
        // 相鄰分組對的 min：min(1,1)=1、min(1,1)=1、min(1,1)=1、min(1,1)=1 → 共 4
        string s2 = "10101";
        Console.WriteLine($"輸入: \"{s2}\" → 輸出: {solution.CountBinarySubstrings(s2)}"); // 預期: 4

        // 測試案例 3：s = "000011100" → 預期輸出 5
        // 分組："0000" | "111" | "00"
        // 相鄰分組對的 min：min(4,3)=3、min(3,2)=2 → 共 5
        string s3 = "000011100";
        Console.WriteLine($"輸入: \"{s3}\" → 輸出: {solution.CountBinarySubstrings(s3)}"); // 預期: 5
    }

    /// <summary>
    /// 計算二元字串中滿足條件的子字串數量。
    /// <para>
    /// 解題思路：
    /// 將字串 <paramref name="s"/> 按連續相同字元進行分組，例如
    /// "000011100" 分成 "0000"、"111"、"00" 三組。
    /// </para>
    /// <para>
    /// 關鍵觀察：符合條件的子字串必然橫跨相鄰的兩個分組。
    /// 若相鄰兩組長度分別為 x 與 y，則可形成 min(x, y) 個有效子字串
    /// （長度分別為 2、4、…、2×min(x,y)）。
    /// </para>
    /// <para>
    /// 實作策略：一次走訪字串，以 <c>prev</c> 記錄上一組的長度、
    /// <c>curr</c> 記錄當前組的長度。每當偵測到字元切換時，
    /// 將 min(prev, curr) 累加至答案，並更新 prev = curr，重置 curr = 1。
    /// 走訪結束後對最後一組補算一次，避免遺漏。
    /// </para>
    /// <example>
    /// <code>
    /// CountBinarySubstrings("00110011") // 回傳 6
    /// CountBinarySubstrings("10101")    // 回傳 4
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="s">僅含 '0' 與 '1' 的二元字串。</param>
    /// <returns>符合條件的非空子字串總數。</returns>
    public int CountBinarySubstrings(string s)
    {
        int count = 0;  // 累計有效子字串數量
        int prev = 0;   // 上一個連續字元群組的長度
        int curr = 1;   // 當前連續字元群組的長度（從第一個字元開始計 1）
        int length = s.Length;

        // 從索引 1 開始，避免 s[i-1] 越界
        for (int i = 1; i < length; i++)
        {
            if (s[i - 1] == s[i])
            {
                // 相同字元 → 當前群組長度加 1
                curr++;
            }
            else
            {
                // 字元切換 → 相鄰兩組可貢獻 min(prev, curr) 個有效子字串
                count += Math.Min(prev, curr);
                // 將當前組設為新的「上一組」，重置當前組長度
                prev = curr;
                curr = 1;
            }
        }

        // 補算最後一個群組（走訪結束後尚未觸發 else 分支）
        count += Math.Min(prev, curr);
        return count;
    }
}
