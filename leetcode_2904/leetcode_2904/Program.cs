using System.Runtime.InteropServices.Marshalling;

namespace leetcode_2904;

class Program
{
    /// <summary>
    /// English:
    ///
    /// 2904. Shortest and Lexicographically Smallest Beautiful String
    /// https://leetcode.com/problems/shortest-and-lexicographically-smallest-beautiful-string/description
    ///
    /// Given a binary string s and a positive integer k.
    ///
    /// A substring of s is beautiful if the number of 1's in it is exactly k.
    ///
    /// Let len be the length of the shortest beautiful substring.
    ///
    /// Return the lexicographically smallest beautiful substring of string s with length equal to len. If s doesn't contain a beautiful substring, return an empty string.
    ///
    /// A string a is lexicographically larger than a string b (of the same length) if in the first position where they differ, a has a character strictly larger than the corresponding character in b.
    ///
    /// For example, "abcd" is lexicographically larger than "abcc" because the first position they differ is at the fourth character, and d is greater than c.
    ///
    /// Traditional Chinese（繁體中文）：
    ///
    /// 2904. 最短且字典序最小的美丽子字符串
    /// https://leetcode.cn/problems/shortest-and-lexicographically-smallest-beautiful-string/description
    ///
    /// 給定一個二進位字串 s 和一個正整數 k。
    ///
    /// 如果子字串中 1 的數量恰好等於 k，則稱該子字串為美麗子字串。
    ///
    /// 令 len 為最短美麗子字串的長度。
    ///
    /// 請回傳 s 中長度等於 len 的字典序最小美麗子字串。如果 s 中不存在美麗子字串，請回傳空字串。
    ///
    /// 若兩個長度相同的字串 a 和 b 在第一個不同的位置上，a 對應字元的字典序嚴格大於 b 對應字元，則稱 a 的字典序大於 b。
    ///
    /// 例如，因為兩個字串第一個不同的位置在第四個字元，且 d 大於 c，所以 "abcd" 的字典序大於 "abcc"。
    /// </summary>
    /// <remarks>
    /// 程式進入點會以五組固定案例驗證兩種解法，逐一輸出實際結果與通過狀態，最後彙整通過數量。
    /// 案例涵蓋官方範例、無解情況、同長度字典序比較，以及 k 等於字串長度的邊界。
    /// </remarks>
    /// <param name="args">命令列參數；本範例不使用。</param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        int passedChecks = 0;
        const int totalChecks = 10;

        Console.WriteLine("LeetCode 2904 - 最短且字典序最小的美麗子字串");
        Console.WriteLine();

        passedChecks += solution.RunTestCase(1, "官方案例一", "100011001", 3, "11001");
        passedChecks += solution.RunTestCase(2, "官方案例二", "1011", 2, "11");
        passedChecks += solution.RunTestCase(3, "無符合子字串", "000", 1, "");
        passedChecks += solution.RunTestCase(4, "同長度字典序比較", "11011011", 3, "1011");
        passedChecks += solution.RunTestCase(5, "k 等於字串長度", "11111", 5, "11111");

        Console.WriteLine($"總結：{passedChecks}/{totalChecks} 通過");
    }

    /// <summary>
    /// 執行一組固定案例，分別呼叫枚舉法與滑動視窗法，並輸出預期值、實際值與 PASS/FAIL。
    /// 輸入須符合題目限制；回傳值代表本案例通過的解法數量，範圍為 0 到 2。
    /// </summary>
    /// <param name="caseNumber">從 1 開始顯示的案例編號。</param>
    /// <param name="name">案例用途或涵蓋情境的名稱。</param>
    /// <param name="s">只包含 '0' 和 '1' 的待測二進位字串。</param>
    /// <param name="k">合法子字串必須包含的 '1' 數量。</param>
    /// <param name="expected">此案例的預期最短且字典序最小結果。</param>
    /// <returns>通過的解法數量，範圍為 0 到 2。</returns>
    private int RunTestCase(int caseNumber, string name, string s, int k, string expected)
    {
        string enumerationResult = ShortestBeautifulSubstring(s, k);
        string slidingWindowResult = ShortestBeautifulSubstring2(s, k);
        bool enumerationPassed = enumerationResult == expected;
        bool slidingWindowPassed = slidingWindowResult == expected;

        Console.WriteLine($"案例 {caseNumber}：{name}");
        Console.WriteLine($"輸入：s = \"{s}\"，k = {k}");
        Console.WriteLine($"預期：\"{expected}\"");
        Console.WriteLine(
            $"ShortestBeautifulSubstring（枚舉）：實際 \"{enumerationResult}\" -> {(enumerationPassed ? "PASS" : "FAIL")}");
        Console.WriteLine(
            $"ShortestBeautifulSubstring2（滑動視窗）：實際 \"{slidingWindowResult}\" -> {(slidingWindowPassed ? "PASS" : "FAIL")}");
        Console.WriteLine();

        return (enumerationPassed ? 1 : 0) + (slidingWindowPassed ? 1 : 0);
    }

    /// <summary>
    /// 使用枚舉法找出包含恰好 k 個 '1' 的最短子字串，再從相同長度的合法候選中保留字典序最小者。
    /// 輸入 s 須是長度介於 1 到 100 的二進位字串，k 須介於 1 到 s.Length；若不存在合法子字串則回傳空字串。
    /// 外層依長度由 k 遞增，內層檢查每個窗口，因此時間複雜度為 O(n³)，額外空間複雜度為 O(n)。
    /// </summary>
    /// <param name="s">只包含 '0' 和 '1' 的二進位字串，長度介於 1 到 100。</param>
    /// <param name="k">合法子字串必須恰好包含的 '1' 數量，介於 1 到 s.Length。</param>
    /// <returns>最短且字典序最小的合法子字串；若不存在則回傳空字串。</returns>
    public string ShortestBeautifulSubstring(string s, int k)
    {
        // 長度從 k 開始遞增；第一個找到合法候選的長度必然是最短長度。
        for (int m = k; m <= s.Length; m++)
        {
            string ans = "";
            for (int i = m; i <= s.Length; i++)
            {
                string t = s.Substring(i - m, m);

                // 同一長度只保留合法且字典序較小的候選，外層換長度時不會遺漏答案。
                if ((ans.Length == 0 || string.CompareOrdinal(t, ans) < 0) &&
                    t.Count(c => c == '1') == k)
                {
                    ans = t;
                }
            }
            if (ans.Length > 0)
            {
                return ans;
            }
        }
        return "";
    }

    /// <summary>
    /// 使用滑動視窗維護以目前右端點結尾的最短合法候選，並在候選長度相同時比較字典序。
    /// 輸入 s 須是長度介於 1 到 100 的二進位字串，k 須介於 1 到 s.Length；若全字串的 '1' 不足 k 個則直接回傳空字串。
    /// 指標掃描本身為 O(n)，但目前實作會建立並比較候選字串，最壞時間複雜度為 O(n²)，額外空間複雜度為 O(n)。
    /// </summary>
    /// <param name="s">只包含 '0' 和 '1' 的二進位字串，長度介於 1 到 100。</param>
    /// <param name="k">合法子字串必須恰好包含的 '1' 數量，介於 1 到 s.Length。</param>
    /// <returns>最短且字典序最小的合法子字串；若不存在則回傳空字串。</returns>
    public string ShortestBeautifulSubstring2(string s, int k)
    {
        // 全字串的 1 不足 k 時，不可能存在符合條件的子字串。
        if (s.Count(c => c == '1') < k)
        {
            return "";
        }

        string ans = s;
        int cnt = 0;
        int left = 0;
        for (int right = 0; right < s.Length; right++)
        {
            cnt += s[right] - '0';

            // 移除多餘的 1，或移除左側不影響 1 數量的 0，
            // 讓目前窗口成為以 right 結尾的最短合法候選。
            while (cnt > k || s[left] == '0')
            {
                cnt -= s[left++] - '0';
            }

            if (cnt == k)
            {
                string t = s.Substring(left, right - left + 1);

                // 題目先比較長度，再比較相同長度候選的字典序。
                if (t.Length < ans.Length ||
                    (t.Length == ans.Length && string.CompareOrdinal(t, ans) < 0))
                {
                    ans = t;
                }
            }
        }
        return ans;
    }
}
