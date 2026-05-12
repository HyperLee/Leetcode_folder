namespace leetcode_005;

class Program
{
    /// <summary>
    /// 5. Longest Palindromic Substring
    /// https://leetcode.com/problems/longest-palindromic-substring/description/
    /// 5. 最长回文子串
    /// https://leetcode.cn/problems/longest-palindromic-substring/description/
    /// 
    /// English:
    /// Given a string s, return the longest palindromic substring in s.
    ///
    /// Traditional Chinese:
    /// 給定一個字串 s，回傳 s 中最長的回文子字串。
    /// </summary>
    /// <param name="args">命令列參數，本範例程式不使用。</param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        (string Input, string[] Expected, string Note)[] examples =
        [
            ("babad", ["bab", "aba"], "奇數長度回文，bab 與 aba 都是合法最長答案。"),
            ("cbbd", ["bb"], "偶數長度回文，中心落在兩個 b 中間。"),
            ("a", ["a"], "單一字元本身就是回文。"),
            ("ac", ["a", "c"], "沒有更長回文時，任一單字元皆可作為答案。"),
            ("forgeeksskeegfor", ["geeksskeeg"], "最長回文位於字串中段。")
        ];

        Console.WriteLine("LeetCode 005 - Longest Palindromic Substring");
        Console.WriteLine("可接受答案若有多個，輸出其中任一個最長回文即可。");
        Console.WriteLine();

        for (int i = 0; i < examples.Length; i++)
        {
            PrintExample(i + 1, examples[i], solution);
        }
    }

    /// <summary>
    /// 執行並列印單一測試案例的三種解法結果。
    /// 這個方法用於 console 範例展示，會顯示輸入、可接受答案、每種解法的結果與解題流程提示。
    /// 輸入案例需提供非 null 字串與至少一個可接受答案；輸出為寫入主控台的示範內容。
    /// </summary>
    /// <param name="index">案例編號，從 1 開始顯示。</param>
    /// <param name="example">包含輸入、可接受答案與案例說明的測試資料。</param>
    /// <param name="solution">提供三種最長回文子字串解法的物件。</param>
    private static void PrintExample(
        int index,
        (string Input, string[] Expected, string Note) example,
        Program solution)
    {
        string bruteForceResult = solution.LongestPalindrome(example.Input);
        string centerResult = solution.LongestPalindrome2(example.Input);
        string optimizedCenterResult = solution.LongestPalindrome3(example.Input);

        Console.WriteLine($"案例 {index}: {example.Note}");
        Console.WriteLine($"Input: \"{example.Input}\"");
        Console.WriteLine($"Expected: {FormatExpected(example.Expected)}");
        Console.WriteLine($"方法一 暴力法: \"{bruteForceResult}\" ({FormatStatus(bruteForceResult, example.Expected)})");
        Console.WriteLine($"  流程: 枚舉所有子字串 -> 雙指標確認回文 -> 保留目前最長結果。");
        Console.WriteLine($"方法二 中心擴展法: \"{centerResult}\" ({FormatStatus(centerResult, example.Expected)})");
        Console.WriteLine($"  流程: 逐一選擇單字元與雙字元中心 -> 向左右擴展 -> 比較最長結果。");
        Console.WriteLine($"方法三 優化中心擴展法: \"{optimizedCenterResult}\" ({FormatStatus(optimizedCenterResult, example.Expected)})");
        Console.WriteLine($"  流程: 用 2n - 1 個中心統一奇偶長度 -> 換算左右邊界 -> 擴展後更新最長結果。");
        Console.WriteLine();
    }

    /// <summary>
    /// 將可接受答案格式化為 console 顯示文字。
    /// 解題案例可能有多個同長度最長回文，因此輸入為答案集合；輸出為以逗號分隔的字串。
    /// </summary>
    /// <param name="expected">可接受答案集合。</param>
    /// <returns>格式化後的可接受答案文字。</returns>
    private static string FormatExpected(string[] expected)
    {
        return string.Join(", ", expected.Select(answer => $"\"{answer}\""));
    }

    /// <summary>
    /// 判斷實際結果是否屬於可接受答案集合，並回傳 console 顯示用狀態。
    /// 輸入為單一解法結果與可接受答案集合；輸出為 PASS 或 CHECK 字樣。
    /// </summary>
    /// <param name="actual">解法回傳的最長回文子字串。</param>
    /// <param name="expected">可接受答案集合。</param>
    /// <returns>若結果符合可接受答案則回傳 PASS，否則回傳 CHECK。</returns>
    private static string FormatStatus(string actual, string[] expected)
    {
        return expected.Contains(actual) ? "PASS" : "CHECK";
    }

    /// <summary>
    /// 使用暴力法尋找最長回文子字串。
    /// 解題概念是枚舉所有可能的子字串，對每個子字串使用左右雙指標檢查是否為回文，
    /// 並在找到更長的回文時更新答案。輸入需為非 null 字串；若輸入為空字串則回傳空字串。
    /// 輸出為其中一個最長回文子字串。
    /// </summary>
    /// <param name="s">要搜尋最長回文子字串的輸入字串。</param>
    /// <returns>輸入字串中的最長回文子字串。</returns>
    public string LongestPalindrome(string s)
    {
        string res = "";
        int n = s.Length;

        for (int i = 0; i < n; i++)
        {
            for (int j = i; j < n; j++)
            {
                // 固定左右邊界後，用雙指標往中心靠攏確認目前子字串是否為回文。
                int p = i;
                int q = j;
                bool isPalindromic = true;

                while (p < q)
                {
                    if (s[p++] != s[q--])
                    {
                        isPalindromic = false;
                        break;
                    }
                }

                if (isPalindromic)
                {
                    int len = j - i + 1;
                    if (len > res.Length)
                    {
                        res = s.Substring(i, len);
                    }
                }
            }
        }
        return res;
    }

    /// <summary>
    /// 使用中心擴展法尋找最長回文子字串。
    /// 解題概念是每個回文都有中心點，中心可能是單一字元或兩個字元之間，
    /// 因此逐一嘗試奇數與偶數中心並向左右擴展。輸入需為非 null 字串；
    /// 若輸入長度小於等於 1 則直接回傳原字串。輸出為其中一個最長回文子字串。
    /// </summary>
    /// <param name="s">要搜尋最長回文子字串的輸入字串。</param>
    /// <returns>輸入字串中的最長回文子字串。</returns>
    public string LongestPalindrome2(string s)
    {
        if (s.Length <= 1)
        {
            return s;
        }

        string res = s.Substring(0, 1);
        for (int i = 0; i < s.Length; i++)
        {
            // 奇數長度以 s[i] 為中心，偶數長度以 s[i] 與 s[i + 1] 的縫隙為中心。
            string temp1 = ExpendCenter(s, i, i);
            string temp2 = ExpendCenter(s, i, i + 1);

            if (res.Length < temp1.Length)
            {
                res = temp1;
            }

            if (res.Length < temp2.Length)
            {
                res = temp2;
            }
        }
        return res;
    }

    /// <summary>
    /// 從指定左右邊界向外擴展，取得該中心可形成的最長回文子字串。
    /// 解題概念是在左右字元相同且仍在字串範圍內時持續擴張，停止後回推有效邊界。
    /// 輸入需為非 null 字串，且左右邊界可表示單字元中心或雙字元中心；輸出為該中心的最長回文。
    /// </summary>
    /// <param name="s">要搜尋回文的輸入字串。</param>
    /// <param name="l">左側起始索引。</param>
    /// <param name="r">右側起始索引。</param>
    /// <returns>指定中心可擴展出的最長回文子字串。</returns>
    public string ExpendCenter(string s, int l, int r)
    {
        while (l >= 0 && r < s.Length && s[l] == s[r])
        {
            l--;
            r++;
        }
        return s.Substring(l + 1, r - l - 1);
    }

    /// <summary>
    /// 使用統一中心索引的中心擴展法尋找最長回文子字串。
    /// 解題概念是將長度 n 的字串視為有 2n - 1 個可能中心，讓奇數與偶數回文共用同一段擴展流程。
    /// 每個中心索引可換算為實際左右邊界，再向外比對直到不相同為止。
    /// 輸入需為非 null 字串；若輸入為空字串則回傳空字串。輸出為其中一個最長回文子字串。
    /// </summary>
    /// <param name="s">要搜尋最長回文子字串的輸入字串。</param>
    /// <returns>輸入字串中的最長回文子字串。</returns>
    public string LongestPalindrome3(string s)
    {
        string res = "";
        int n = s.Length;
        int end = 2 * n - 1;

        for (int i = 0; i < end; i++)
        {
            double mid = i / 2.0;
            // Floor/Ceiling 將整數中心映射為同一字元，將 .5 中心映射為相鄰兩字元。
            int p = (int)(Math.Floor(mid));
            int q = (int)(Math.Ceiling(mid));

            while (p >= 0 && q < n)
            {
                if (s[p] != s[q])
                {
                    break;
                }

                // 字元仍相同時向左右各擴一格，直到超界或遇到不同字元。
                p--;
                q++;
            }

            int len = q - p - 1;
            if (len > res.Length)
            {
                res = s.Substring(p + 1, len);
            }
        }
        return res;
    }
}
