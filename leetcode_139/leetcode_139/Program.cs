namespace leetcode_139
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 139. Word Break
        /// https://leetcode.com/problems/word-break/description/
        ///
        /// Given a string s and a dictionary of strings wordDict, return true if s can be segmented into a space-separated
        /// sequence of one or more dictionary words.
        ///
        /// Note that the same word in the dictionary may be reused multiple times in the segmentation.
        ///
        /// Example 1:
        /// Input: s = "leetcode", wordDict = ["leet","code"]
        /// Output: true
        /// Explanation: Return true because "leetcode" can be segmented as "leet code".
        ///
        /// Example 2:
        /// Input: s = "applepenapple", wordDict = ["apple","pen"]
        /// Output: true
        /// Explanation: Return true because "applepenapple" can be segmented as "apple pen apple".
        /// Note that you are allowed to reuse a dictionary word.
        ///
        /// Example 3:
        /// Input: s = "catsandog", wordDict = ["cats","dog","sand","and","cat"]
        /// Output: false
        ///
        /// Constraints:
        /// - 1 &lt;= s.length &lt;= 300
        /// - 1 &lt;= wordDict.length &lt;= 1000
        /// - 1 &lt;= wordDict[i].length &lt;= 20
        /// - s and wordDict[i] consist of only lowercase English letters.
        /// - All the strings of wordDict are unique.
        /// </para>
        /// <para>
        /// 139. 單字拆分
        /// https://leetcode.cn/problems/word-break/description/
        ///
        /// 給定一個字串 s 與一個字串字典 wordDict，如果 s 可以被拆分成由一個或多個字典單字組成、
        /// 並以空格分隔的序列，則回傳 true。
        ///
        /// 請注意，在拆分過程中可以重複使用字典中的同一個單字多次。
        ///
        /// 範例 1：
        /// 輸入：s = "leetcode", wordDict = ["leet","code"]
        /// 輸出：true
        /// 解釋：回傳 true，因為 "leetcode" 可以拆分為 "leet code"。
        ///
        /// 範例 2：
        /// 輸入：s = "applepenapple", wordDict = ["apple","pen"]
        /// 輸出：true
        /// 解釋：回傳 true，因為 "applepenapple" 可以拆分為 "apple pen apple"。
        /// 請注意，可以重複使用字典中的單字。
        ///
        /// 範例 3：
        /// 輸入：s = "catsandog", wordDict = ["cats","dog","sand","and","cat"]
        /// 輸出：false
        ///
        /// 限制條件：
        /// - 1 &lt;= s.length &lt;= 300
        /// - 1 &lt;= wordDict.length &lt;= 1000
        /// - 1 &lt;= wordDict[i].length &lt;= 20
        /// - s 與 wordDict[i] 僅由小寫英文字母組成。
        /// - wordDict 中的所有字串均不相同。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SampleCase[] samples =
            [
                new("leetcode", ["leet", "code"], true),
                new("applepenapple", ["apple", "pen"], true),
                new("catsandog", ["cats", "dog", "sand", "and", "cat"], false),
                new("aaaaaaa", ["aaaa", "aaa"], true),
                new("cars", ["car", "ca", "rs"], true),
                new("a", ["a"], true),
                new("aaaaab", ["a", "aa", "aaa"], false)
            ];

            int passedChecks = 0;
            int totalChecks = samples.Length * 2;

            for (int index = 0; index < samples.Length; index++)
            {
                SampleCase sample = samples[index];
                SampleResult result = EvaluateSample(sample);
                bool dynamicProgrammingPassed = result.DynamicProgrammingResult == sample.Expected;
                bool breadthFirstSearchPassed = result.BreadthFirstSearchResult == sample.Expected;

                if (dynamicProgrammingPassed)
                {
                    passedChecks++;
                }

                if (breadthFirstSearchPassed)
                {
                    passedChecks++;
                }

                Console.WriteLine($"案例 {index + 1}");
                Console.WriteLine(
                    $"輸入：s = \"{sample.Input}\", wordDict = {FormatWordDictionary(sample.WordDictionary)}");
                Console.WriteLine($"預期：{FormatBoolean(sample.Expected)}");
                Console.WriteLine(
                    $"動態規劃：{FormatBoolean(result.DynamicProgrammingResult)} => {FormatStatus(dynamicProgrammingPassed)}");
                Console.WriteLine(
                    $"廣度優先搜尋：{FormatBoolean(result.BreadthFirstSearchResult)} => {FormatStatus(breadthFirstSearchPassed)}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
        }

        /// <summary>
        /// 使用動態規劃判斷字串能否由字典單字完整拼接。以
        /// <c>dp[i]</c> 表示前 <c>i</c> 個字元能否完成拆分，並列舉每個可能的分割點；
        /// 輸入字串與字典須符合題目所定義的非空、小寫英文字母條件，方法會回傳整個字串是否可拆分。
        /// </summary>
        /// <param name="s">要判斷的非空小寫英文字串。</param>
        /// <param name="wordDict">可重複使用單字的非空字典，字典內的單字互不相同。</param>
        /// <returns>若 <paramref name="s"/> 可完全拆成一個或多個字典單字則為 <see langword="true"/>；否則為 <see langword="false"/>。</returns>
        public static bool WordBreak(string s, IList<string> wordDict)
        {
            var wordsDictSet = new HashSet<string>(wordDict);
            var dp = new bool[s.Length + 1];

            // dp[0] 代表空前綴；它是所有合法拆分路徑的起點。
            dp[0] = true;

            for (int i = 1; i <= s.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    // 只有前綴可拆分，且分割點後的字串存在於字典時，位置 i 才可到達。
                    if (dp[j] && wordsDictSet.Contains(s.Substring(j, i - j)))
                    {
                        dp[i] = true;
                        break;
                    }
                }
            }

            return dp[s.Length];
        }

        /// <summary>
        /// 使用廣度優先搜尋判斷字串能否由字典單字完整拼接。將每個切分索引視為圖節點，
        /// 字典中存在的子字串視為節點間的邊；輸入字串與字典須符合題目所定義的非空、
        /// 小寫英文字母條件，方法會回傳搜尋是否能抵達字串尾端。
        /// </summary>
        /// <param name="s">要判斷的非空小寫英文字串。</param>
        /// <param name="wordDict">可重複使用單字的非空字典，字典內的單字互不相同。</param>
        /// <returns>若搜尋可由索引 0 抵達字串尾端則為 <see langword="true"/>；否則為 <see langword="false"/>。</returns>
        public static bool WordBreak2(string s, IList<string> wordDict)
        {
            var wordSet = new HashSet<string>(wordDict);
            var queue = new Queue<int>();
            var visited = new HashSet<int>();

            // 索引 0 是搜尋起點；佇列中的每個索引都代表一個已匹配的字串前綴。
            queue.Enqueue(0);

            while (queue.Count > 0)
            {
                int start = queue.Dequeue();

                // 同一切分位置的後續選擇完全相同，只需展開一次。
                if (visited.Contains(start))
                {
                    continue;
                }

                visited.Add(start);

                for (int end = start + 1; end <= s.Length; end++)
                {
                    string sub = s.Substring(start, end - start);

                    if (wordSet.Contains(sub))
                    {
                        // 能沿字典單字形成的邊抵達尾端，即代表整個字串可完整拆分。
                        if (end == s.Length)
                        {
                            return true;
                        }

                        queue.Enqueue(end);
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 對單一合法測試案例執行動態規劃與廣度優先搜尋兩種解法，
        /// 保留案例輸入不變，並回傳兩種演算法各自的布林結果供主要進入點比對。
        /// </summary>
        /// <param name="sample">包含非空輸入字串、合法字典與預期結果的測試案例。</param>
        /// <returns>同時包含動態規劃與廣度優先搜尋結果的資料物件。</returns>
        private static SampleResult EvaluateSample(SampleCase sample)
        {
            return new SampleResult(
                WordBreak(sample.Input, sample.WordDictionary),
                WordBreak2(sample.Input, sample.WordDictionary));
        }

        /// <summary>
        /// 將非空字典格式化為含雙引號的陣列表示法，方便主程式輸出可重現的測試資料。
        /// </summary>
        /// <param name="wordDictionary">要顯示的非空字典。</param>
        /// <returns>格式如 <c>["leet", "code"]</c> 的字串。</returns>
        private static string FormatWordDictionary(IList<string> wordDictionary)
        {
            return $"[{string.Join(", ", wordDictionary.Select(word => $"\"{word}\""))}]";
        }

        /// <summary>
        /// 將布林結果轉換為題目範例使用的小寫文字。
        /// </summary>
        /// <param name="value">要格式化的布林值。</param>
        /// <returns><c>true</c> 或 <c>false</c>。</returns>
        private static string FormatBoolean(bool value)
        {
            return value ? "true" : "false";
        }

        /// <summary>
        /// 將單項驗證結果轉換為固定的通過或失敗標記。
        /// </summary>
        /// <param name="passed">實際結果是否符合預期。</param>
        /// <returns><c>PASS</c> 或 <c>FAIL</c>。</returns>
        private static string FormatStatus(bool passed)
        {
            return passed ? "PASS" : "FAIL";
        }

        /// <summary>
        /// 表示一筆符合題目輸入限制的可執行案例，以及人工推導的預期結果。
        /// </summary>
        /// <param name="Input">要判斷的非空小寫英文字串。</param>
        /// <param name="WordDictionary">內容互異且可重複使用的非空單字字典。</param>
        /// <param name="Expected">字串是否可完整拆分的預期結果。</param>
        private sealed record SampleCase(string Input, string[] WordDictionary, bool Expected);

        /// <summary>
        /// 保存同一案例經動態規劃與廣度優先搜尋求得的結果。
        /// </summary>
        /// <param name="DynamicProgrammingResult">動態規劃解法的結果。</param>
        /// <param name="BreadthFirstSearchResult">廣度優先搜尋解法的結果。</param>
        private sealed record SampleResult(
            bool DynamicProgrammingResult,
            bool BreadthFirstSearchResult);
    }
}