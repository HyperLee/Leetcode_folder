namespace leetcode_3110
{
    internal class Program
    {
        /// <summary>
        /// 3110. Score of a String
        /// https://leetcode.com/problems/score-of-a-string/description/
        /// <para>
        /// You are given a string s. The score of a string is the sum of the absolute differences between the ASCII values of adjacent characters.
        ///
        /// Return the score of s.
        ///
        /// Example 1:
        /// Input: s = "hello"
        /// Output: 13
        /// Explanation: The ASCII values are 'h' = 104, 'e' = 101, 'l' = 108, and 'o' = 111. The score is |104 - 101| + |101 - 108| + |108 - 108| + |108 - 111| = 3 + 7 + 0 + 3 = 13.
        ///
        /// Example 2:
        /// Input: s = "zaz"
        /// Output: 50
        /// Explanation: The ASCII values are 'z' = 122 and 'a' = 97. The score is |122 - 97| + |97 - 122| = 25 + 25 = 50.
        ///
        /// Constraints:
        /// - 2 &lt;= s.length &lt;= 100
        /// - s consists only of lowercase English letters.
        /// </para>
        /// <para>
        /// 3110. 字串的分數
        /// https://leetcode.cn/problems/score-of-a-string/description/
        ///
        /// 給定一個字串 s。字串的分數定義為所有相鄰字元 ASCII 值之絕對差的總和。
        ///
        /// 回傳 s 的分數。
        ///
        /// 範例 1：
        /// 輸入：s = "hello"
        /// 輸出：13
        /// 解釋：ASCII 值分別為 'h' = 104、'e' = 101、'l' = 108、'o' = 111。分數為 |104 - 101| + |101 - 108| + |108 - 108| + |108 - 111| = 3 + 7 + 0 + 3 = 13。
        ///
        /// 範例 2：
        /// 輸入：s = "zaz"
        /// 輸出：50
        /// 解釋：ASCII 值分別為 'z' = 122 與 'a' = 97。分數為 |122 - 97| + |97 - 122| = 25 + 25 = 50。
        ///
        /// 限制條件：
        /// - 2 &lt;= s.length &lt;= 100
        /// - s 只由小寫英文字母組成。
        /// </para>
        /// </summary>
        /// <remarks>
        /// 以固定案例執行三種線性掃描解法，逐一比較預期值與實際值；全部案例通過時回傳 0，否則回傳 1。
        /// </remarks>
        /// <param name="args">命令列參數；本程式使用固定案例，不讀取外部輸入。</param>
        /// <returns>所有驗證通過時回傳 0，任一驗證失敗時回傳 1。</returns>
        static int Main(string[] args)
        {
            return RunSamples();
        }

        /// <summary>
        /// 建立符合題目限制的固定字串案例，執行三種解法並統計通過的驗證數量。
        /// </summary>
        /// <returns>24 項驗證全部通過時回傳 0，否則回傳 1。</returns>
        private static int RunSamples()
        {
            SampleCase[] samples =
            {
                new("官方範例一", "hello", 13),
                new("官方範例二", "zaz", 50),
                new("最短同字元", "aa", 0),
                new("最短最大差", "az", 25),
                new("含重複相鄰字元", "aabb", 1),
                new("交錯最大差", "azaz", 75),
                new("一般遞增字元", "abcde", 4),
                new("長度上限同字元", new string('z', 100), 0)
            };

            int passedChecks = 0;
            foreach (SampleCase sample in samples)
            {
                passedChecks += RunCase(sample);
            }

            int totalChecks = samples.Length * 3;
            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
            return passedChecks == totalChecks ? 0 : 1;
        }

        /// <summary>
        /// 對單一字串案例執行三種分數計算方法，輸出預期值、實際值與 PASS/FAIL。
        /// </summary>
        /// <param name="sample">包含案例名稱、輸入字串與預期分數的固定案例。</param>
        /// <returns>本案例通過的解法數量，範圍為 0 到 3。</returns>
        private static int RunCase(SampleCase sample)
        {
            Console.WriteLine($"案例：{sample.Name}");
            Console.WriteLine($"輸入 = \"{sample.Input}\"");
            Console.WriteLine($"預期 = {sample.Expected}");

            (string Name, int Actual)[] results =
            {
                ("ScoreOfString", ScoreOfString(sample.Input)),
                ("ScoreOfString2", ScoreOfString2(sample.Input)),
                ("ScoreOfString3", ScoreOfString3(sample.Input))
            };

            int passedChecks = 0;
            foreach ((string name, int actual) in results)
            {
                bool passed = actual == sample.Expected;
                if (passed)
                {
                    passedChecks++;
                }

                Console.WriteLine($"{name,-16} 實際 = {actual} => {(passed ? "PASS" : "FAIL")}");
            }

            Console.WriteLine();
            return passedChecks;
        }

        /// <summary>
        /// 描述一筆固定測試案例的名稱、輸入字串與預期分數。
        /// </summary>
        /// <param name="Name">用於輸出的案例名稱。</param>
        /// <param name="Input">長度 2 到 100、僅含小寫英文字母的輸入字串。</param>
        /// <param name="Expected">所有相鄰字元 ASCII 絕對差的預期總和。</param>
        private sealed record SampleCase(string Name, string Input, int Expected);

        /// <summary>
        /// 以索引從第二個字元開始掃描，累加每一對相鄰字元 ASCII 值的絕對差，計算字串分數。
        /// 輸入須為長度 2 到 100 且僅含小寫英文字母的字串；輸出為非負整數分數。
        /// </summary>
        /// <param name="s">符合題目限制的非空小寫英文字串。</param>
        /// <returns>所有相鄰字元 ASCII 值絕對差的總和。</returns>
        /// <remarks>
        /// 索引 i 與 i - 1 恰好代表一組相鄰字元，每組只會計算一次。
        /// 時間複雜度為 O(n)，額外空間複雜度為 O(1)。
        /// 參考：
        /// https://leetcode.cn/problems/score-of-a-string/solutions/2738900/bian-li-pythonjavacgo-by-endlesscheng-x63p/
        /// https://leetcode.cn/problems/score-of-a-string/solutions/2739065/3110-zi-fu-chuan-de-fen-shu-by-stormsuns-4yuk/
        /// </remarks>
        public static int ScoreOfString(string s)
        {
            int score = 0;

            for (int index = 1; index < s.Length; index++)
            {
                // 每次只計算目前字元與前一字元，確保每組相鄰字元恰好累加一次。
                score += Math.Abs(s[index] - s[index - 1]);
            }

            return score;
        }

        /// <summary>
        /// 以 foreach 逐字走訪並保存前一字元，遇到下一字元時累加兩者 ASCII 值的絕對差。
        /// 輸入須為長度 2 到 100 且僅含小寫英文字母的字串；輸出為非負整數分數。
        /// </summary>
        /// <param name="s">符合題目限制的非空小寫英文字串。</param>
        /// <returns>所有相鄰字元 ASCII 值絕對差的總和。</returns>
        /// <remarks>
        /// previous 保存上一輪讀到的字元；第一個字元沒有前驅，因此只建立狀態而不計分。
        /// 時間複雜度為 O(n)，額外空間複雜度為 O(1)。
        /// </remarks>
        public static int ScoreOfString2(string s)
        {
            int score = 0;
            char? previous = null;

            foreach (char current in s)
            {
                if (previous.HasValue)
                {
                    score += Math.Abs(current - previous.Value);
                }

                // 將目前字元留給下一輪配對，避免依賴索引存取。
                previous = current;
            }

            return score;
        }

        /// <summary>
        /// 以 LINQ Zip 配對原字串與向右偏移一位的序列，再加總每對字元 ASCII 值的絕對差。
        /// 輸入須為長度 2 到 100 且僅含小寫英文字母的字串；輸出為非負整數分數。
        /// </summary>
        /// <param name="s">符合題目限制的非空小寫英文字串。</param>
        /// <returns>所有相鄰字元 ASCII 值絕對差的總和。</returns>
        /// <remarks>
        /// s 與 s.Skip(1) 的同位置元素分別是相鄰字元的左側與右側，Zip 會在較短序列結束時停止。
        /// 時間複雜度為 O(n)，迭代器使用固定數量的額外空間。
        /// </remarks>
        public static int ScoreOfString3(string s)
        {
            // 將兩個錯開一位的序列合併，直接形成 (s[i], s[i + 1]) 相鄰配對。
            return s.Zip(s.Skip(1), static (left, right) => Math.Abs(left - right)).Sum();
        }
    }
}