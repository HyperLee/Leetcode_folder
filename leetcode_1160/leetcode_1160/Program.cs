namespace leetcode_1160;

class Program
{
    /// <summary>
    /// 1160. Find Words That Can Be Formed by Characters
    /// https://leetcode.com/problems/find-words-that-can-be-formed-by-characters/description/
    /// 1160. 拼写单词
    /// https://leetcode.cn/problems/find-words-that-can-be-formed-by-characters/description/
    /// 
    /// 
    /// </summary>
    /// <remarks>
    /// 執行四組固定範例，分別驗證逐字移除與字頻統計兩種解法。
    /// 命令列參數不會影響測試資料；所有檢查通過時結束碼為 0，否則為 1。
    /// </remarks>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solver = new Program();
        bool allPassed = true;

        allPassed &= RunTestCase(
            solver,
            "Case 1 - LeetCode Example 1",
            new[] { "cat", "bt", "hat", "tree" },
            "atach",
            6);
        allPassed &= RunTestCase(
            solver,
            "Case 2 - LeetCode Example 2",
            new[] { "hello", "world", "leetcode" },
            "welldonehoneyr",
            10);
        allPassed &= RunTestCase(
            solver,
            "Case 3 - Repeated letters",
            new[] { "a", "aa", "aaa", "b" },
            "aa",
            3);
        allPassed &= RunTestCase(
            solver,
            "Case 4 - No word can be formed",
            new[] { "ab", "cd" },
            "a",
            0);

        Console.WriteLine(allPassed
            ? "Overall: PASS (4/4 cases, 8/8 checks)"
            : "Overall: FAIL");

        Environment.ExitCode = allPassed ? 0 : 1;
    }

    /// <summary>
    /// 解法一：逐字消耗可用字元，計算所有可由 <paramref name="chars"/> 組成之單字的長度總和。
    /// 每個單字都取得一份完整的可用字元字串；找到相符字元後立即移除，藉此確保同一字元不能重複使用。
    /// 輸入應符合題目條件：陣列與字串皆非空，且內容僅包含小寫英文字母。
    ///
    /// </summary>
    /// <param name="words">要逐一判斷是否可被組成的單字陣列。</param>
    /// <param name="chars">每次判斷新單字時可完整使用一次的字元集合。</param>
    /// <returns>所有可被組成之單字的長度總和。</returns>
    public int CountCharacters(string[] words, string chars)
    {
        int count = 0;
        foreach (string word in words)
        {
            bool flag = false;
            string newchars = chars;

            // 每個單字都從完整的 chars 開始，並逐字消耗一個相符字元。
            foreach (char item in word)
            {
                if (newchars.Contains(item))
                {
                    flag = true;
                    // 移除已使用的字元，避免重複字母超過 chars 可提供的次數。
                    newchars = newchars.Remove(newchars.IndexOf(item), 1);
                }
                else
                {
                    flag = false;
                    break;
                }
            }

            // 只有整個單字的每個字元都成功配對，才累加其長度。
            if (flag)
            {
                count += word.Length;
            }
        }

        return count;
    }

    /// <summary>
    /// 解法二：使用字頻雜湊表，計算所有可由 <paramref name="chars"/> 組成之單字的長度總和。
    /// 先統計可用字元數量，再為每個單字建立需求字頻；只要任一需求大於供應量，就判定該單字無法組成。
    /// 輸入應符合題目條件：陣列與字串皆非空，且內容僅包含小寫英文字母。
    ///
    /// </summary>
    /// <param name="words">要逐一判斷是否可被組成的單字陣列。</param>
    /// <param name="chars">每次判斷新單字時可完整使用一次的字元集合。</param>
    /// <returns>所有字頻需求不超過可用字頻之單字的長度總和。</returns>
    public int CountCharacters2(string[] words, string chars)
    {
        Dictionary<char, int> charsCnt = new Dictionary<char, int>();

        // 可用字頻只需建立一次，所有單字都能獨立使用這份完整供應量。
        foreach (char c in chars)
        {
            if (charsCnt.ContainsKey(c))
            {
                charsCnt[c]++;
            }
            else
            {
                charsCnt[c] = 1;
            }
        }

        int ans = 0;

        foreach (string word in words)
        {
            Dictionary<char, int> wordCnt = new Dictionary<char, int>();

            // 每個單字重新建立需求字頻，避免前一個單字影響下一個單字。
            foreach (char c in word)
            {
                if (wordCnt.ContainsKey(c))
                {
                    wordCnt[c]++;
                }
                else
                {
                    wordCnt[c] = 1;
                }
            }

            bool isAns = true;

            // 任一字元需求超過可用數量時即可提早停止檢查。
            foreach (char c in word)
            {
                int charsCount = charsCnt.TryGetValue(c, out int availableCount)
                    ? availableCount
                    : 0;

                int wordCount = wordCnt.TryGetValue(c, out int requiredCount)
                    ? requiredCount
                    : 0;

                if (charsCount < wordCount)
                {
                    isAns = false;
                    break;
                }
            }

            if (isAns)
            {
                ans += word.Length;
            }
        }

        return ans;
    }

    /// <summary>
    /// 執行單一固定案例，將相同輸入交給兩種解法並比較預期與實際結果。
    /// 此方法負責輸出案例資料與 PASS/FAIL 狀態，不會修改傳入的單字陣列或字元字串。
    /// </summary>
    /// <param name="solver">提供兩種解法的 <see cref="Program"/> 實例。</param>
    /// <param name="caseName">顯示於主控台的案例名稱。</param>
    /// <param name="words">符合題目限制的待檢查單字陣列。</param>
    /// <param name="chars">每個單字可獨立使用的字元集合。</param>
    /// <param name="expected">兩種解法都應回傳的長度總和。</param>
    /// <returns>兩種解法都等於 <paramref name="expected"/> 時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
    private static bool RunTestCase(
        Program solver,
        string caseName,
        string[] words,
        string chars,
        int expected)
    {
        int removeCharactersResult = solver.CountCharacters(words, chars);
        int frequencyCountResult = solver.CountCharacters2(words, chars);
        bool removeCharactersPassed = removeCharactersResult == expected;
        bool frequencyCountPassed = frequencyCountResult == expected;
        string formattedWords = string.Join(", ", words.Select(word => $"\"{word}\""));

        Console.WriteLine(caseName);
        Console.WriteLine($"  words = [{formattedWords}]");
        Console.WriteLine($"  chars = \"{chars}\"");
        Console.WriteLine(
            $"  CountCharacters : expected={expected}, actual={removeCharactersResult}, " +
            $"{(removeCharactersPassed ? "PASS" : "FAIL")}");
        Console.WriteLine(
            $"  CountCharacters2: expected={expected}, actual={frequencyCountResult}, " +
            $"{(frequencyCountPassed ? "PASS" : "FAIL")}");
        Console.WriteLine();

        return removeCharactersPassed && frequencyCountPassed;
    }
}