namespace leetcode_3403;

class Program
{
    /// <summary>
    /// 3403. Find the Lexicographically Largest String From the Box I
    /// https://leetcode.com/problems/find-the-lexicographically-largest-string-from-the-box-i/description/?envType=daily-question&envId=2025-06-04
    /// 3403. 從盒子中找出字典序最大的字串 I
    /// https://leetcode.cn/problems/find-the-lexicographically-largest-string-from-the-box-i/description/?envType=daily-question&envId=2025-06-04
    /// 
    /// 題目描述（繁體中文）：
    /// 給定一個字串 word 和一個整數 numFriends。
    /// Alice 為她的 numFriends 位朋友舉辦一個遊戲。遊戲有多輪，每一輪：
    /// - 將 word 拆分成 numFriends 個非空字串，且每一輪的拆分方式都不能與之前完全相同。
    /// - 將所有拆分後的字串放入盒子中。
    /// 請找出所有輪結束後，盒子中字典序最大的字串。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        string word = "leetcode";
        int numFriends = 3;

        // 三種解法都執行
        var program = new Program();
        string result1 = program.AnswerString(word, numFriends);
        string result2 = program.AnswerStringDFS(word, numFriends);
        string result3 = program.AnswerString_TwoPoint(word, numFriends);

        Console.WriteLine($"word = {word}, numFriends = {numFriends}");
        Console.WriteLine($"枚舉法結果: {result1}");
        Console.WriteLine($"DFS+回溯法結果: {result2}");
        Console.WriteLine($"雙指針法結果: {result3}");
    }


    /// <summary>
    /// 枚舉方法解題
    /// 解題說明：
    /// 給定一個字串 word 和一個整數 numFriends，
    /// 你可以選擇 word 中任意一個長度為 n - numFriends + 1 的子字串，
    /// 目標是找出字典序最大的子字串。
    /// 若 numFriends == 1，直接回傳原字串。
    /// 否則，枚舉所有長度為 n - numFriends + 1 的子字串，取字典序最大的。
    ///
    /// 時間複雜度：O(n^2)
    ///   - 需枚舉每個起始點，對每個起始點取長度為 n - numFriends + 1 的子字串，最壞情況下為 O(n^2)。
    /// 空間複雜度：O(n)
    ///   - 只需儲存當前最大子字串與臨時子字串，空間與字串長度成正比。
    /// </summary>
    /// <param name="word"> 原始字串 </param>
    /// <param name="numFriends"> 朋友數量 </param>
    /// <returns > 字典序最大的子字串 </returns>
    public string AnswerString(string word, int numFriends)
    {
        if (numFriends == 1)
        {
            // 只有一個朋友，直接回傳原字串
            return word;
        }

        int n = word.Length;
        string res = "";
        // 枚舉所有可能的起始位置 i
        for (int i = 0; i < n; i++)
        {
            // 取從 i 開始，長度為 n - numFriends + 1 的子字串（若剩餘長度不足則取到結尾）
            string s = word.Substring(i, Math.Min(n - numFriends + 1, n - i));
            // 比較目前 res 與 s 的字典序，若 s 較大則更新 res
            if (string.Compare(res, s) <= 0)
            {
                res = s;
            }
        }
        // 回傳字典序最大的子字串
        return res;
    }


    /// <summary>
    /// DFS + 回溯法（Backtracking） 方法解題
    /// 解題說明：
    /// 使用深度優先搜尋（DFS）枚舉所有將 word 拆分成 numFriends 個非空字串的方式，
    /// 最後回傳字典序最大的字串。
    /// 時間複雜度：指數級，O(2^n)，不建議大資料量使用。
    /// 空間複雜度：O(n)
    /// 
    /// 此方法在 ＬeetCode 上的執行時間會超時（Time Limit Exceeded），
    /// 因為它需要枚舉所有可能的拆分方式，對於長字串和多個朋友的情況，會產生大量的組合。
    /// </summary>
    /// <param name="word">原始字串</param>
    /// <param name="numFriends">朋友數量</param>
    /// <returns>字典序最大的子字串</returns>
    public string AnswerStringDFS(string word, int numFriends)
    {
        int n = word.Length;
        string maxStr = "";
        void DFS(int idx, int remain, List<string> split)
        {
            if (remain == 1)
            {
                // 最後一段直接取到結尾
                split.Add(word.Substring(idx));
                foreach (var s in split)
                {
                    if (string.Compare(maxStr, s) < 0)
                    {
                        maxStr = s; // 更新最大字串
                    }
                }
                // 回溯，移除最後一個拆分的字串，恢復狀態
                split.RemoveAt(split.Count - 1);
                return;
            }

            // 每一段至少要有一個字元
            for (int i = idx + 1; i <= n - remain + 1; i++)
            {
                // 建立新狀態
                split.Add(word.Substring(idx, i - idx));
                // 繼續 DFS，剩餘需要拆分的段數減一
                DFS(i, remain - 1, split);
                // 回溯，移除最後一個拆分的字串，恢復狀態
                split.RemoveAt(split.Count - 1);
            }
        }

        DFS(0, numFriends, new List<string>());
        return maxStr;
    }


    /// <summary>
    /// 求字串 s 的所有子字串中，字典序最大的那一段（最靠後的子字串）
    /// 解題說明：
    /// 使用雙指針法，i, j 分別指向當前最大子字串的起點與候選起點，
    /// 逐步比較字元，若發現更大的子字串則更新 i。
    /// 時間複雜度 O(n)，空間複雜度 O(1)。
    /// </summary>
    /// <param name="s">原始字串</param>
    /// <returns>字典序最大的子字串</returns>
    public string LastSubstring(string s)
    {
        int i = 0; // 當前最大子字串的起點
        int j = 1; // 候選子字串的起點
        int n = s.Length;
        while (j < n)
        {
            int k = 0;
            // 比較 i+k 與 j+k 的字元，直到不同或到字串結尾
            while (j + k < n && s[i + k] == s[j + k])
            {
                k++;
            }

            if (j + k < n && s[i + k] < s[j + k])
            {
                // 若 j+k 的字元較大，更新最大子字串起點 i
                int t = i;
                i = j;
                // j 往後移動，避免重複比較
                // t + k + 1 則是跳過所有已經比對過且相同的字元，直接移動到下一個可能會有不同結果的位置。
                // j + 1 表示單純將 j 往後移動一格
                j = Math.Max(j + 1, t + k + 1);
            }
            else
            {
                // 否則 j 往後移動
                // 為了避免重複比較已經確認過的部分，直接將 j 指標往後移動 k + 1 個位置，也就是 j = j + k + 1。
                // 這樣可以跳過所有已經比對且相同的字元，直接移動到下一個可能會有不同結果的位置。
                j = j + k + 1;
            }
        }
        // 回傳最大子字串
        return s.Substring(i);
    }


    /// <summary>
    /// 雙指針法解題
    /// 解題說明：
    /// 先利用 LastSubstring 函式，找出字串 s 的所有子字串中，字典序最大的那一段（即最靠後的子字串）。
    /// 再根據題目規則，回傳這段子字串的前 n - numFriends + 1 個字元，
    /// 這樣就能得到所有拆分方式中，字典序最大的子字串。
    /// 此方法時間複雜度 O(n)，空間複雜度 O(1)。
    /// 
    /// ref:
    /// https://leetcode.cn/problems/find-the-lexicographically-largest-string-from-the-box-i/solutions/3685906/cong-he-zi-zhong-zhao-chu-zi-dian-xu-zui-eg0v/?envType=daily-question&envId=2025-06-04
    /// </summary>
    /// <param name="word">原始字串</param>
    /// <param name="numFriends">朋友數量</param>
    /// <returns>字典序最大的子字串</returns>
    public string AnswerString_TwoPoint(string word, int numFriends)
    {
        if (numFriends == 1)
        {
            // 只有一個朋友，直接回傳原字串
            return word;
        }

        // 取得字典序最大的子字串
        string last = LastSubstring(word);
        int n = word.Length;
        int m = last.Length;

        // 回傳長度為 n - numFriends + 1 的前綴
        return last.Substring(0, Math.Min(n - numFriends + 1, m));
    }

}
