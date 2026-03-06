namespace leetcode_1784;

class Program
{
    /// <summary>
    /// 1784. Check if Binary String Has at Most One Segment of Ones
    /// Given a binary string s without leading zeros, return true if s contains at most one contiguous segment of ones. Otherwise, return false.
    ///
    /// 1784. 檢查二進位字串是否至多包含一個連續的 1 段
    /// 給定一個不含前導零的二進位字串 s，如果 s 至多包含一個連續的 1 段，回傳 true；否則回傳 false。
    ///
    /// English and Traditional Chinese versions added as requested.
    ///
    /// https://leetcode.com/problems/check-if-binary-string-has-at-most-one-segment-of-ones/description/?envType=daily-question&envId=2026-03-06
    /// https://leetcode.cn/problems/check-if-binary-string-has-at-most-one-segment-of-ones/description/?envType=daily-question&envId=2026-03-06
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();

        // 測試案例：涵蓋全零、單段1、多段1、邊界值等情境
        (string input, bool expected)[] testCases =
        [
            ("1",       true),   // 最短輸入，只有一個 1
            ("110",     true),   // 單段 1 後接 0
            ("1110000", true),   // 多個 1 後接多個 0
            ("1000110", false),  // 兩段 1，中間有 0
            ("10",      true),   // 單個 1 後接 0
            ("11",      true),   // 兩個連續 1
            ("101",     false),  // 中間斷開的兩段 1
        ];

        Console.WriteLine("=== LeetCode 1784 測試結果 ===");
        Console.WriteLine($"{"Input",-12} {"Expected",-10} {"Method1",-10} {"Method2",-10} {"Pass?"}");
        Console.WriteLine(new string('-', 55));

        foreach ((string s, bool expected) in testCases)
        {
            bool r1 = solution.CheckOnesSegment(s);
            bool r2 = solution.CheckOnesSegment2(s);
            string pass = (r1 == expected && r2 == expected) ? "PASS" : "FAIL";
            Console.WriteLine($"{s,-12} {expected,-10} {r1,-10} {r2,-10} {pass}");
        }
    }

    /// <summary>
    /// 方法一：逐字元狀態機（迴圈遍歷）
    ///
    /// 解題概念：
    ///   使用兩個布林旗標追蹤「目前是否在 1 段中」與「是否已離開過一段 1」。
    ///   當我們偵測到「離開過 1 段之後又遇到新的 1」，即確認存在兩段以上，回傳 false。
    ///
    /// 解題步驟：
    ///   1. 遇到第一個 '1' → 設定 inSegment = true（進入 1 段）。
    ///   2. 在 1 段中遇到 '0' → 設定 leftSegment = true（已離開第一段 1）。
    ///   3. 若 leftSegment == true 卻再次遇到 '1' → 第二段出現，直接回傳 false。
    ///   4. 整串走完未觸發第 3 步 → 回傳 true。
    ///
    /// 時間複雜度：O(n)，n 為字串長度，只需一次線性掃描。
    /// 空間複雜度：O(1)，只使用常數個輔助變數。
    /// </summary>
    /// <param name="s">不含前導零的二進位字串。</param>
    /// <returns>字串中至多只有一段連續 1 時回傳 true，否則回傳 false。</returns>
    public bool CheckOnesSegment(string s)
    {
        bool inSegment = false;   // 目前是否正在 1 段內
        bool leftSegment = false; // 是否已曾離開過一段 1

        foreach (char c in s)
        {
            if (c == '1')
            {
                // 已離開過 1 段卻又見到 1，代表第二段開始
                if (leftSegment)
                {
                    return false;
                }
                inSegment = true; // 進入（或持續在）1 段
            }
            else
            {
                // 遇到 0 時，若當前在 1 段內，標記為已離開
                if (inSegment)
                {
                    leftSegment = true;
                }
                inSegment = false;
            }
        }

        return true;
    }

    /// <summary>
    /// 方法二：尋找子字串 "01"（一行解）
    ///
    /// 解題概念：
    ///   由於輸入字串不含前導零，符合條件的字串形態只有兩種：
    ///     - 全零串：00⋯0
    ///     - 一段 1 後接零或多個 0：1⋯10⋯0
    ///   這兩種情況的共同特徵是：字串中絕對不會出現子字串 "01"。
    ///   反之，一旦出現 "01"，必定代表某段 1 之後出現了 0，而 0 之後還有 1，
    ///   意味著存在至少兩段不連續的 1。
    ///
    /// 關鍵觀察：
    ///   「不含前導零」保證字串開頭若有 1，就是第一段的起點。
    ///   若偵測到 "01"，代表 1 段在某處結束後，後面仍有 1，違反題意。
    ///
    /// 時間複雜度：O(n)，字串搜尋為線性。
    /// 空間複雜度：O(1)，不需要額外空間。
    /// </summary>
    /// <param name="s">不含前導零的二進位字串。</param>
    /// <returns>字串中至多只有一段連續 1 時回傳 true，否則回傳 false。</returns>
    public bool CheckOnesSegment2(string s)
    {
        // 若出現 "01" 子字串，代表 1 段結束後再度出現 1（第二段），不符合條件
        return !s.Contains("01");
    }
}
