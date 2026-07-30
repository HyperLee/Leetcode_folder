namespace leetcode_424;

class Program
{
    /// <summary>
    /// 424. Longest Repeating Character Replacement
    /// https://leetcode.com/problems/longest-repeating-character-replacement/description/?envType=problem-list-v2&envId=oizxjoit
    /// 424. 替换后的最长重复字符
    /// https://leetcode.cn/problems/longest-repeating-character-replacement/description/
    /// 
    /// 解題概念：
    /// 使用滑動窗口 (sliding window) 的方法來解決問題。
    /// 我們需要找到一個子字串，該子字串可以通過最多 k 次替換操作將其變成由相同字符組成的最長子字串。
    /// 核心邏輯是維護一個窗口，窗口內的字符可以通過 k 次替換形成一個有效的子字串。
    /// 當窗口內的字符數量超過 maxCount + k 時，縮小窗口。
    /// 
    /// 時間複雜度：O(n)，其中 n 是字串的長度。
    /// 空間複雜度：O(1)，因為我們只使用了一個固定大小的陣列來記錄字符頻率。
    /// 
    /// 這種解法的巧妙之處在於維護maxCount變數時，只增不減。即使在左指針移動時，對應的字符計數減少可能影響最大出現次數，程式碼也沒有重新計算maxCount。
    /// 這是因為在窗口內的字符數量不會減少，只有當窗口大小超過maxCount + k時才會縮小窗口。這樣可以保證maxCount始終是正確的。
    /// 對於這個問題，我們只關心找到最長的有效子字串，而不需要保證每個窗口都是精確最優的。
    /// </summary>
    /// <param name="args"></param> 
    static void Main(string[] args)
    {
        SampleCase[] sampleCases =
        [
            new("Official example 2", "AABABBA", 1, 4),
            new("Official example 1", "ABAB", 2, 4),
            new("All characters identical", "AAAA", 2, 4),
            new("Larger replacement budget", "AABABBA", 2, 5),
            new("Minimum input", "A", 0, 1),
            new("No replacements allowed", "ABCDE", 0, 1),
            new("Replace the whole window", "ABCDE", 4, 5)
        ];

        Program solution = new Program();
        int passed = 0;

        foreach (SampleCase sampleCase in sampleCases)
        {
            int actual = solution.CharacterReplacement(sampleCase.S, sampleCase.K);
            bool isPassed = actual == sampleCase.Expected;
            if (isPassed)
            {
                passed++;
            }

            Console.WriteLine($"Case: {sampleCase.Name}");
            Console.WriteLine($"Input: s = \"{sampleCase.S}\", k = {sampleCase.K}");
            Console.WriteLine($"Expected: {sampleCase.Expected}");
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine($"Result: {(isPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {passed}/{sampleCases.Length} checks passed.");
        if (passed != sampleCases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 計算最多替換 <paramref name="k"/> 個字元後，可形成的最長單一重複字元子字串長度。
    /// 解法以滑動窗口記錄 26 個大寫英文字母的出現次數，並以
    /// 「窗口長度減去窗口內最高字元頻率」判斷所需替換次數。當替換成本超過
    /// <paramref name="k"/> 時移動左邊界，使窗口長度維持為目前可達的最佳候選。
    /// 適用於題目限制內的非 null 大寫英文字串；時間複雜度為 O(n)，
    /// 輔助空間複雜度為 O(1)。
    /// </summary>
    /// <param name="s">長度介於 1 至 100000，且僅包含大寫英文字母的字串。</param>
    /// <param name="k">最多允許替換的次數，介於 0 至 <paramref name="s"/> 的長度。</param>
    /// <returns>最多替換 <paramref name="k"/> 個字元後，內容可全部相同的最長子字串長度。</returns>
    public int CharacterReplacement(string s, int k)
    {
        int len = s.Length;
        if (len < 2)
        {
            return len;
        }

        int left = 0;
        int right = 0;
        int res = 0;
        int maxCount = 0;
        int[] count = new int[26];

        while (right < len)
        {
            count[s[right] - 'A']++;

            // maxCount 保留掃描至今的最高頻率，不隨左邊界移動而下降；
            // 本題只求最長長度，較寬鬆的歷史上限不會錯過更長的候選窗口。
            if (count[s[right] - 'A'] > maxCount)
            {
                maxCount = count[s[right] - 'A'];
            }

            right++;

            // 窗口長度減去最高字元頻率，就是把窗口統一成單一字元所需的替換次數。
            if (right - left > maxCount + k)
            {
                count[s[left] - 'A']--;
                left++;

                // 即使把剩餘字元全接到目前窗口，也無法超越 res 時可提前結束。
                if (res >= right - left + (len - right))
                {
                    return res;
                }
            }

            res = Math.Max(res, right - left);
        }

        return res;
    }

    private sealed record SampleCase(string Name, string S, int K, int Expected);
}
