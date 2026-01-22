namespace leetcode_567;

class Program
{
    /// <summary>
    /// Problem 567 — Permutation in String
    /// English: Given two strings s1 and s2, return true if s2 contains a permutation of s1, or false otherwise.
    /// In other words, return true if one of s1's permutations is the substring of s2.
    ///
    /// 中文（繁體）: 給定兩個字串 s1 與 s2，如果 s2 包含 s1 的某個排列則回傳 true，否則回傳 false。
    /// 換句話說，若 s1 的任一排列為 s2 的子字串則回傳 true。
    ///
    /// 參考: https://leetcode.com/problems/permutation-in-string/description/
    /// 題號: 567. 字符串的排列
    /// </summary>
    /// <param name="args">命令列參數（未使用）。</param>
    static void Main(string[] args)
    {
        var program = new Program();

        // 測試案例 1：s1 = "ab", s2 = "eidbaooo" => true ("ba" 是 "ab" 的排列)
        string s1_1 = "ab";
        string s2_1 = "eidbaooo";
        Console.WriteLine($"測試 1: s1=\"{s1_1}\", s2=\"{s2_1}\"");
        Console.WriteLine($"  CheckInclusion:  {program.CheckInclusion(s1_1, s2_1)}");  // 預期: true
        Console.WriteLine($"  CheckInclusion2: {program.CheckInclusion2(s1_1, s2_1)}"); // 預期: true

        // 測試案例 2：s1 = "ab", s2 = "eidboaoo" => false
        string s1_2 = "ab";
        string s2_2 = "eidboaoo";
        Console.WriteLine($"測試 2: s1=\"{s1_2}\", s2=\"{s2_2}\"");
        Console.WriteLine($"  CheckInclusion:  {program.CheckInclusion(s1_2, s2_2)}");  // 預期: false
        Console.WriteLine($"  CheckInclusion2: {program.CheckInclusion2(s1_2, s2_2)}"); // 預期: false

        // 測試案例 3：s1 = "adc", s2 = "dcda" => true ("cda" 是 "adc" 的排列)
        string s1_3 = "adc";
        string s2_3 = "dcda";
        Console.WriteLine($"測試 3: s1=\"{s1_3}\", s2=\"{s2_3}\"");
        Console.WriteLine($"  CheckInclusion:  {program.CheckInclusion(s1_3, s2_3)}");  // 預期: true
        Console.WriteLine($"  CheckInclusion2: {program.CheckInclusion2(s1_3, s2_3)}"); // 預期: true

        // 測試案例 4：s1 較長，s2 較短 => false
        string s1_4 = "abcdef";
        string s2_4 = "abc";
        Console.WriteLine($"測試 4: s1=\"{s1_4}\", s2=\"{s2_4}\"");
        Console.WriteLine($"  CheckInclusion:  {program.CheckInclusion(s1_4, s2_4)}");  // 預期: false
        Console.WriteLine($"  CheckInclusion2: {program.CheckInclusion2(s1_4, s2_4)}"); // 預期: false

        // 測試案例 5：完全相同
        string s1_5 = "abc";
        string s2_5 = "abc";
        Console.WriteLine($"測試 5: s1=\"{s1_5}\", s2=\"{s2_5}\"");
        Console.WriteLine($"  CheckInclusion:  {program.CheckInclusion(s1_5, s2_5)}");  // 預期: true
        Console.WriteLine($"  CheckInclusion2: {program.CheckInclusion2(s1_5, s2_5)}"); // 預期: true
    }

    /// <summary>
    /// 解法一：滑動視窗 + 字符計數匹配法
    /// <para>
    /// 【思路】
    /// 若 s2 包含 s1 的某個排列，則必須存在一個長度為 s1.Length 的子字串，
    /// 其中每個字符的出現次數與 s1 完全相同。
    /// </para>
    /// <para>
    /// 【演算法】
    /// 1. 統計 s1 中每個字符的出現次數 (pFreq) 以及有多少種不同字符 (pCount)。
    /// 2. 使用雙指標 left/right 維護滑動視窗，記錄視窗內字符的出現次數 (winFreq)。
    /// 3. 當某字符在視窗中的次數恰好等於 s1 中該字符的次數時，winCount++。
    /// 4. 當 winCount == pCount（所有字符都匹配）且視窗長度 == s1 長度時，找到排列。
    /// 5. 若視窗過大或字符不再匹配，則左指標右移縮小視窗。
    /// </para>
    /// <para>
    /// 【複雜度】
    /// 時間複雜度：O(n)，其中 n 為 s2 長度，每個字符最多被存取兩次。
    /// 空間複雜度：O(1)，僅使用固定大小 26 的陣列。
    /// </para>
    /// </summary>
    /// <param name="s1">模式字串，需要找其排列。</param>
    /// <param name="s2">目標字串，在其中搜尋 s1 的排列。</param>
    /// <returns>若 s2 包含 s1 的任一排列則回傳 true，否則回傳 false。</returns>
    /// <example>
    /// <code>
    /// CheckInclusion("ab", "eidbaooo"); // 回傳 true，因為 "ba" 是 "ab" 的排列
    /// CheckInclusion("ab", "eidboaoo"); // 回傳 false
    /// </code>
    /// </example>
    public bool CheckInclusion(string s1, string s2)
    {
        // 將字串轉為字元陣列以便操作
        char[] pattern = s1.ToCharArray();
        char[] text = s2.ToCharArray();

        int pLen = pattern.Length;  // s1 長度
        int tLen = text.Length;     // s2 長度

        // pFreq: 記錄 s1 中每個字符的出現次數
        // winFreq: 記錄滑動視窗中每個字符的出現次數
        int[] pFreq = new int[26];
        int[] winFreq = new int[26];

        // 步驟 1: 統計 s1 中每個字符的出現次數
        for (int i = 0; i < pLen; i++)
        {
            pFreq[pattern[i] - 'a']++;
        }

        // 步驟 2: 計算 s1 中有多少種不同的字符
        int pCount = 0;
        for (int i = 0; i < 26; i++)
        {
            if (pFreq[i] > 0)
            {
                pCount++;
            }
        }

        // 步驟 3: 使用雙指標維護滑動視窗
        int left = 0;      // 視窗左邊界
        int right = 0;     // 視窗右邊界
        int winCount = 0;  // 視窗中已匹配的字符種類數

        while (right < tLen)
        {
            // 擴展右邊界：將 text[right] 加入視窗
            if (pFreq[text[right] - 'a'] > 0)
            {
                // 只處理 s1 中存在的字符
                winFreq[text[right] - 'a']++;

                // 若該字符在視窗中的次數恰好等於 s1 中的次數，則匹配種類數 +1
                if (winFreq[text[right] - 'a'] == pFreq[text[right] - 'a'])
                {
                    winCount++;
                }
            }
            right++;

            // 當所有字符種類都匹配時，嘗試縮小視窗
            while (winCount == pCount)
            {
                // 若視窗長度恰好等於 s1 長度，找到一個排列
                if (right - left == pLen)
                {
                    return true;
                }

                // 收縮左邊界：將 text[left] 移出視窗
                if (pFreq[text[left] - 'a'] > 0)
                {
                    winFreq[text[left] - 'a']--;

                    // 若移除後該字符次數不足，則匹配種類數 -1
                    if (winFreq[text[left] - 'a'] < pFreq[text[left] - 'a'])
                    {
                        winCount--;
                    }
                }
                left++;
            }
        }

        return false;
    }


    /// <summary>
    /// 解法二：固定長度滑動視窗 + 字符頻率比對法
    /// <para>
    /// 【思路】
    /// 若 s2 包含 s1 的排列，則 s1 長度必須 ≤ s2 長度。
    /// 我們只需檢查 s2 中所有長度為 s1.Length 的子字串，
    /// 判斷其字符頻率是否與 s1 相同即可。
    /// </para>
    /// <para>
    /// 【演算法】
    /// 1. 若 s1.Length > s2.Length，直接回傳 false。
    /// 2. 同時統計 s1 和 s2 前 s1.Length 個字符的頻率。
    /// 3. 若首個視窗的頻率與 s1 相同，回傳 true。
    /// 4. 否則，將視窗向右滑動：移除最左字符頻率，加入新字符頻率。
    /// 5. 每次滑動後比對頻率，若相同則回傳 true。
    /// 6. 遍歷完畢仍未找到，回傳 false。
    /// </para>
    /// <para>
    /// 【複雜度】
    /// 時間複雜度：O(26 * n) = O(n)，其中 n 為 s2 長度。每次滑動需比對 26 個字符。
    /// 空間複雜度：O(1)，僅使用固定大小 26 的陣列。
    /// </para>
    /// <para>
    /// 【與解法一比較】
    /// 此方法邏輯更直觀易懂，每次滑動都完整比對 26 個字符頻率。
    /// 雖然每次比對是 O(26)，但常數較小，實務上效能表現良好。
    /// </para>
    /// </summary>
    /// <param name="s1">模式字串，需要找其排列。</param>
    /// <param name="s2">目標字串，在其中搜尋 s1 的排列。</param>
    /// <returns>若 s2 包含 s1 的任一排列則回傳 true，否則回傳 false。</returns>
    /// <example>
    /// <code>
    /// CheckInclusion2("ab", "eidbaooo"); // 回傳 true，因為 "ba" 是 "ab" 的排列
    /// CheckInclusion2("ab", "eidboaoo"); // 回傳 false
    /// </code>
    /// </example>
    public bool CheckInclusion2(string s1, string s2)
    {
        int length1 = s1.Length, length2 = s2.Length;

        // 步驟 1: 長度檢查 - 若 s1 比 s2 長，不可能包含其排列
        if (length1 > length2)
        {
            return false;
        }

        // counts1: 記錄 s1 中每個字符的出現次數
        // counts2: 記錄 s2 目前視窗中每個字符的出現次數
        int[] counts1 = new int[26];
        int[] counts2 = new int[26];

        // 步驟 2: 初始化 - 同時統計 s1 全部和 s2 前 length1 個字符的頻率
        for (int i = 0; i < length1; i++)
        {
            char c1 = s1[i];
            counts1[c1 - 'a']++;  // 統計 s1 字符頻率

            char c2 = s2[i];
            counts2[c2 - 'a']++;  // 統計 s2 首個視窗字符頻率
        }

        // 步驟 3: 檢查首個視窗是否為 s1 的排列
        if (CheckEqual(counts1, counts2))
        {
            return true;
        }

        // 步驟 4: 滑動視窗 - 從索引 length1 開始，視窗向右移動
        // 每次移動：移除最左邊字符，加入新的右邊字符
        for (int i = length1; i < length2; i++)
        {
            // 移除視窗最左邊的字符（滑出視窗）
            char prev = s2[i - length1];
            counts2[prev - 'a']--;

            // 加入視窗最右邊的新字符（滑入視窗）
            char curr = s2[i];
            counts2[curr - 'a']++;

            // 比對目前視窗的字符頻率是否與 s1 相同
            if (CheckEqual(counts1, counts2))
            {
                return true;
            }
        }

        // 遍歷完畢，未找到任何匹配的排列
        return false;
    }

    /// <summary>
    /// 比對兩兩是否相同
    /// </summary>
    /// <param name="counts1"></param>
    /// <param name="counts2"></param>
    /// <returns></returns>
    public bool CheckEqual(int[] counts1, int[] counts2)
    {
        for(int i = 0; i < 26; i++)
        {
            if(counts1[i] != counts2[i])
            {
                return false;
            }
        }
        return true;
    }
}
