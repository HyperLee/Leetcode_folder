namespace leetcode_1717;

class Program
{
    /// <summary>
    /// 1717. Maximum Score From Removing Substrings
    /// https://leetcode.com/problems/maximum-score-from-removing-substrings/description/?envType=daily-question&envId=2025-07-23
    /// 1717. 删除子字符串的最大得分
    /// https://leetcode.cn/problems/maximum-score-from-removing-substrings/description/?envType=daily-question&envId=2025-07-23
    /// 
    /// 這是題目描述：
    /// 給你一個字串 s 和兩個整數 x 和 y。你可以執行以下兩種操作任意次數：
    /// 1. 移除子字串 "ab" 並獲得 x 分。例如，從 "cabxbae" 移除 "ab" 會變成 "cxbae"。
    /// 2. 移除子字串 "ba" 並獲得 y 分。例如，從 "cabxbae" 移除 "ba" 會變成 "cabxe"。
    /// 請回傳在對 s 執行上述操作後，所能獲得的最大分數。
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var prog = new Program();
        // 測試資料 1
        string s1 = "cdbcbbaaabab";
        int x1 = 4, y1 = 5;
        Console.WriteLine($"Input: s=\"{s1}\", x={x1}, y={y1}  => Output: {prog.MaximumGain(s1, x1, y1)} (預期: 19)");

        // 測試資料 2
        string s2 = "aabbaaxybbaabb";
        int x2 = 5, y2 = 4;
        Console.WriteLine($"Input: s=\"{s2}\", x={x2}, y={y2}  => Output: {prog.MaximumGain(s2, x2, y2)}");

        // 測試資料 3
        string s3 = "ababbab";
        int x3 = 10, y3 = 1;
        Console.WriteLine($"Input: s=\"{s3}\", x={x3}, y={y3}  => Output: {prog.MaximumGain(s3, x3, y3)}");

        // 測試資料 4
        string s4 = "bbaaababbab";
        int x4 = 2, y4 = 10;
        Console.WriteLine($"Input: s=\"{s4}\", x={x4}, y={y4}  => Output: {prog.MaximumGain(s4, x4, y4)}");

        // 邊界測試
        string s5 = "";
        int x5 = 1, y5 = 1;
        Console.WriteLine($"Input: s=\"{s5}\", x={x5}, y={y5}  => Output: {prog.MaximumGain(s5, x5, y5)} (預期: 0)");
    }

    /// <summary>
    /// 計算從字串 s 中移除子字串 "ab" 與 "ba" 所能獲得的最大分數。
    /// 
    /// 解題思路：
    /// 本題採用貪心策略，優先移除分數較高的子字串（假設 x >= y，則優先移除 "ab"，否則交換 x, y 並將 a, b 互換，問題等價）。
    /// 每次移除 "ab" 或 "ba" 都會使 a、b 各減一，剩餘字母相對順序不變，總刪除次數不變，因此應盡量先做高分操作。
    /// 具體做法：
    /// 1. 若 x < y，則交換 x, y 並將 s 中 a, b 互換，這樣只需考慮一種情況。
    /// 2. 依序遍歷 s，遇到 a 累加計數，遇到 b 且前面有 a 則消去一組 ab 並加分，否則累加 b 計數。
    /// 3. 最後剩下的 a, b 可組成 ba，取 min(cntA, cntB) 組，每組加 y 分。
    /// 
    /// <example>
    /// <code>
    /// MaximumGain("cdbcbbaaabab", 4, 5) // return 19
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="s">輸入字串</param>
    /// <param name="x">移除 "ab" 的分數</param>
    /// <param name="y">移除 "ba" 的分數</param>
    /// <returns>最大總分</returns>
    public int MaximumGain(string s, int x, int y)
    {
        // 若移除 "ba" 分數更高，則交換 x, y 並將 a, b 互換，問題等價
        if (x < y)
        {
            (x, y) = (y, x);
            char[] arr = s.ToCharArray();
            for (int i = 0; i < arr.Length; i++)
            {
                // 互換 a, b
                if (arr[i] == 'a')
                {
                    arr[i] = 'b';
                }
                else if (arr[i] == 'b')
                {
                    arr[i] = 'a';
                }
            }
            s = new string(arr);
        }

        int res = 0;
        for (int i = 0; i < s.Length; i++)
        {
            int cntA = 0; // 記錄當前區段 a 的數量
            int cntB = 0; // 記錄當前區段 b 的數量
            // 只處理連續的 a, b 區段
            while (i < s.Length && (s[i] == 'a' || s[i] == 'b'))
            {
                if (s[i] == 'a')
                {
                    cntA++; // 累加 a
                }
                else // s[i] == 'b'
                {
                    if (cntA > 0)
                    {
                        // 若前面有 a，則消去一組 ab 並加分
                        cntA--;
                        res += x;
                    }
                    else
                    {
                        // 沒有 a，累加 b
                        cntB++;
                    }
                }
                i++;
            }
            // 剩下的 a, b 可組成 ba，每組加 y 分
            res += Math.Min(cntA, cntB) * y;
        }
        return res;
    }
}
