namespace leetcode_2911;

class Program
{
    /// <summary>
    /// 2911. Minimum Changes to Make K Semi-palindromes
    /// https://leetcode.com/problems/minimum-changes-to-make-k-semi-palindromes/description/
    /// 2911. 得到 K 個半回文串的最少修改次數
    /// https://leetcode.cn/problems/minimum-changes-to-make-k-semi-palindromes/description/
    /// <para>
    /// English: Given a string s and an integer k, partition s into exactly k substrings and change as few letters as possible so every substring is a semi-palindrome. A semi-palindrome has some proper divisor d of its length such that every sequence of characters at positions i, i + d, i + 2d, and so on is a palindrome. Return the minimum total number of letter changes.
    /// </para>
    /// <para>
    /// 繁體中文：給定字串 s 與整數 k，將 s 恰好切分為 k 個子字串，並以最少字元變更使每個子字串都成為半回文串。半回文串存在一個小於字串長度的正因數 d，使每個由位置 i、i + d、i + 2d 等位置組成的字元序列皆為迴文。請回傳最少字元變更總數。
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    /// <remarks>執行內建測試資料，逐筆比對兩種解法與預期的最少修改次數。執行時無需命令列參數。</remarks>
    /// <returns>全部比對通過時回傳 0，任一比對失敗時回傳 1。</returns>
    static int Main(string[] args)
    {
        var cases = new (string Name, string S, int K, int Expected)[]
        {
            ("abcac", "abcac", 2, 1),
            ("abcdef", "abcdef", 2, 2),
            ("aabbaa", "aabbaa", 3, 0),
            ("aa", "aa", 1, 0),
            ("ab", "ab", 1, 1),
            ("abcabc", "abcabc", 1, 0),
            ("abcabcxy", "abcabcxy", 2, 1),
            ("abababab", "abababab", 3, 1),
            ("200 copies of 'a'", new string('a', 200), 100, 0)
        };
        var solver = new Program();
        var methods = new (string Name, Func<string, int, int> Solve)[]
        {
            (nameof(MinimumChanges), solver.MinimumChanges),
            (nameof(MinimumChanges2), solver.MinimumChanges2)
        };

        int passed = 0;
        foreach (var (name, s, k, expected) in cases)
        {
            foreach (var (method, solve) in methods)
            {
                int actual = solve(s, k);
                bool matches = actual == expected;
                if (matches)
                {
                    passed++;
                }

                string result = matches ? "PASS" : "FAIL";
                Console.WriteLine($"{result} {method} ({name}, k={k}): expected={expected}, actual={actual}");
            }
        }

        int total = cases.Length * methods.Length;
        Console.WriteLine($"{passed}/{total} checks passed.");
        return passed == total ? 0 : 1;
    }

    /// <summary>
    /// 將符合題目限制的字串恰好切成 k 段，使用自頂向下記憶化搜尋取得最少修改次數。
    /// </summary>
    /// <param name="s">長度介於 2 到 200 的小寫英文字串。</param>
    /// <param name="k">目標段數，至少為 1，最多為字串長度除以 2 後無條件捨去的值。</param>
    /// <returns>讓每一段都成為半回文串的最少字元修改總數。</returns>
    public int MinimumChanges(string s, int k)
    {
        int n = s.Length;
        int[][] cost = BuildCosts(s);
        int[][] memo = new int[k + 1][];
        for (int seg = 0; seg <= k; seg++)
        {
            memo[seg] = new int[n];
            Array.Fill(memo[seg], -1);
        }

        return Solve(k, 0, n, cost, memo);
    }

    /// <summary>
    /// 方法二：自底向上遞推
    /// <para>將符合題目限制的字串恰好切成 k 段，以滾動陣列計算最少修改次數。</para>
    /// </summary>
    /// <param name="s">長度介於 2 到 200 的小寫英文字串。</param>
    /// <param name="k">目標段數，至少為 1，最多為字串長度除以 2 後無條件捨去的值。</param>
    /// <returns>讓每一段都成為半回文串的最少字元修改總數。</returns>
    public int MinimumChanges2(string s, int k)
    {
        int n = s.Length;
        int[][] cost = BuildCosts(s);
        int[] prev = new int[n];
        for (int start = 0; start + 1 < n; start++)
        {
            prev[start] = cost[start][n - 1];
        }

        for (int seg = 2; seg <= k; seg++)
        {
            int[] cur = new int[n];
            Array.Fill(cur, n);

            // 第一段之後，每個剩餘段至少要保留兩個字元。
            int limit = n - 1 - 2 * (seg - 1);
            for (int start = 0; start + 2 * seg <= n; start++)
            {
                for (int e = start + 1; e <= limit; e++)
                {
                    cur[start] = Math.Min(cur[start], cost[start][e] + prev[e + 1]);
                }
            }

            // 同一層的轉移都依賴前一層，整層算完後才能更新 prev。
            prev = cur;
        }

        return prev[0];
    }

    /// <summary>
    /// 枚舉各長度的真因數，預先計算每個長度至少為 2 的區間變成半回文串的成本。
    /// </summary>
    /// <param name="s">符合題目限制的小寫英文字串。</param>
    /// <returns>兩端都包含的區間成本表，只有右端點大於左端點的項目有效。</returns>
    private int[][] BuildCosts(string s)
    {
        int n = s.Length;
        List<int>[] factors = new List<int>[n + 1];
        for (int i = 0; i <= n; i++)
        {
            factors[i] = new List<int>();
        }

        // 從 2d 開始列舉 d 的倍數，確保 d 是真因數。
        for (int d = 1; d <= n / 2; d++)
        {
            for (int m = d * 2; m <= n; m += d)
            {
                factors[m].Add(d);
            }
        }

        int[][] cost = new int[n][];
        for (int l = 0; l < n; l++)
        {
            cost[l] = new int[n];
            for (int r = l + 1; r < n; r++)
            {
                cost[l][r] = SegmentCost(s, l, r - l + 1, factors);
            }
        }

        return cost;
    }

    /// <summary>
    /// 對合法區間嘗試每個真因數，計算各組的鏡像字元配對並取最低成本。
    /// </summary>
    /// <param name="s">包含該區間的原字串。</param>
    /// <param name="l">區間起點，從 0 開始。</param>
    /// <param name="m">區間長度，至少為 2，且區間不得超出字串。</param>
    /// <param name="factors">依長度索引的真因數清單。</param>
    /// <returns>將此區間變成半回文串的最少字元修改次數。</returns>
    private int SegmentCost(string s, int l, int m, List<int>[] factors)
    {
        int best = m;
        foreach (int d in factors[m])
        {
            int groupLength = m / d;
            int changes = 0;
            for (int offset = 0; offset < d; offset++)
            {
                // 每組沿著間距 d 取字元，從兩端配對，每對不相等的字元只需修改一個。
                for (int t = 0; t < groupLength / 2; t++)
                {
                    int left = l + offset + t * d;
                    int right = l + offset + (groupLength - 1 - t) * d;
                    if (s[left] != s[right])
                    {
                        changes++;
                    }
                }
            }

            best = Math.Min(best, changes);
        }

        return best;
    }

    /// <summary>
    /// 枚舉第一段的結尾，用記憶化搜尋求出合法後綴恰好切成指定段數的最低成本。
    /// </summary>
    /// <param name="seg">剩餘段數，至少為 1。</param>
    /// <param name="start">從 0 開始的後綴起點，後綴長度至少為 2 * seg。</param>
    /// <param name="n">原字串長度。</param>
    /// <param name="cost">預先計算的區間修改成本，左右端點皆包含。</param>
    /// <param name="memo">以段數與起點索引的記憶表，-1 表示尚未計算。</param>
    /// <returns>將後綴恰好切成 seg 個半回文串的最少字元修改總數。</returns>
    private int Solve(int seg, int start, int n, int[][] cost, int[][] memo)
    {
        if (seg == 1)
        {
            return cost[start][n - 1];
        }

        // 不同切法可能到達同一個後綴與段數，可重用先前計算的成本。
        if (memo[seg][start] != -1)
        {
            return memo[seg][start];
        }

        int best = n;
        // 第一段至少兩個字元，後綴還要為剩下的 seg - 1 段保留足夠長度。
        int limit = n - 1 - 2 * (seg - 1);
        for (int e = start + 1; e <= limit; e++)
        {
            best = Math.Min(best, cost[start][e] + Solve(seg - 1, e + 1, n, cost, memo));
        }

        memo[seg][start] = best;
        return best;
    }
}
