namespace leetcode_506
{
    internal class Program
    {
        /// <summary>
        /// 506. Relative Ranks
        /// https://leetcode.com/problems/relative-ranks/description/
        /// <para>
        /// You are given an integer array score of size n, where score[i] is the score of the i-th athlete in a competition. All scores are unique.
        ///
        /// Athletes are placed by score: 1st has the highest score, 2nd has the second-highest score, and so on. Each athlete's placement determines the rank:
        /// - The 1st-place rank is "Gold Medal".
        /// - The 2nd-place rank is "Silver Medal".
        /// - The 3rd-place rank is "Bronze Medal".
        /// - From 4th through n-th place, the rank is the placement number; the x-th-place rank is "x".
        ///
        /// Return an array answer of size n where answer[i] is the rank of the i-th athlete.
        ///
        /// Example 1:
        /// Input: score = [5,4,3,2,1]
        /// Output: ["Gold Medal","Silver Medal","Bronze Medal","4","5"]
        /// Explanation: The placements are [1st, 2nd, 3rd, 4th, 5th].
        ///
        /// Example 2:
        /// Input: score = [10,3,8,9,4]
        /// Output: ["Gold Medal","5","Bronze Medal","Silver Medal","4"]
        /// Explanation: The placements are [1st, 5th, 3rd, 2nd, 4th].
        ///
        /// Constraints:
        /// - n == score.length
        /// - 1 &lt;= n &lt;= 10^4
        /// - 0 &lt;= score[i] &lt;= 10^6
        /// - All values in score are unique.
        /// </para>
        /// <para>
        /// 506. 相對名次
        /// https://leetcode.cn/problems/relative-ranks/description/
        ///
        /// 給定大小為 n 的整數陣列 score，其中 score[i] 是競賽中第 i 位運動員的分數。所有分數都不相同。
        ///
        /// 運動員依分數決定名次：第 1 名分數最高，第 2 名分數第二高，依此類推。每位運動員的名次決定其排名文字：
        /// - 第 1 名的排名為 "Gold Medal"。
        /// - 第 2 名的排名為 "Silver Medal"。
        /// - 第 3 名的排名為 "Bronze Medal"。
        /// - 從第 4 名到第 n 名，排名為其名次數字；第 x 名的排名為 "x"。
        ///
        /// 回傳大小為 n 的陣列 answer，其中 answer[i] 是第 i 位運動員的排名。
        ///
        /// 範例 1：
        /// 輸入：score = [5,4,3,2,1]
        /// 輸出：["Gold Medal","Silver Medal","Bronze Medal","4","5"]
        /// 解釋：各運動員的名次為 [第 1 名, 第 2 名, 第 3 名, 第 4 名, 第 5 名]。
        ///
        /// 範例 2：
        /// 輸入：score = [10,3,8,9,4]
        /// 輸出：["Gold Medal","5","Bronze Medal","Silver Medal","4"]
        /// 解釋：各運動員的名次為 [第 1 名, 第 5 名, 第 3 名, 第 2 名, 第 4 名]。
        ///
        /// 限制條件：
        /// - n == score.length
        /// - 1 &lt;= n &lt;= 10^4
        /// - 0 &lt;= score[i] &lt;= 10^6
        /// - score 中所有值都不相同。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var samples = new (string Name, int[] Scores, string[] Expected)[]
            {
                (
                    "官方範例一",
                    [5, 4, 3, 2, 1],
                    ["Gold Medal", "Silver Medal", "Bronze Medal", "4", "5"]
                ),
                (
                    "官方範例二",
                    [10, 3, 8, 9, 4],
                    ["Gold Medal", "5", "Bronze Medal", "Silver Medal", "4"]
                ),
                (
                    "單一選手",
                    [42],
                    ["Gold Medal"]
                ),
                (
                    "恰好三名選手",
                    [30, 10, 20],
                    ["Gold Medal", "Bronze Medal", "Silver Medal"]
                ),
                (
                    "輸入順序與排名相反",
                    [1, 2, 3, 4, 5],
                    ["5", "4", "Bronze Medal", "Silver Medal", "Gold Medal"]
                ),
                (
                    "合法分數上下界",
                    [1_000_000, 0, 999_999, 500_000],
                    ["Gold Medal", "4", "Silver Medal", "Bronze Medal"]
                )
            };

            int passedChecks = 0;
            int totalChecks = samples.Length * 2;

            for (int index = 0; index < samples.Length; index++)
            {
                var sample = samples[index];
                int[] firstInput = [.. sample.Scores];
                int[] secondInput = [.. sample.Scores];

                string[] firstActual = FindRelativeRanks(firstInput);
                string[] secondActual = FindRelativeRanks2(secondInput);
                bool firstPassed = firstActual.SequenceEqual(sample.Expected);
                bool secondPassed = secondActual.SequenceEqual(sample.Expected);

                if (firstPassed)
                {
                    passedChecks++;
                }

                if (secondPassed)
                {
                    passedChecks++;
                }

                Console.WriteLine($"案例 {index + 1}：{sample.Name}");
                Console.WriteLine($"輸入：{FormatScores(sample.Scores)}");
                Console.WriteLine($"Expected: {FormatRanks(sample.Expected)}");
                Console.WriteLine($"解法一 Actual: {FormatRanks(firstActual)}");
                Console.WriteLine($"解法一結果：{(firstPassed ? "PASS" : "FAIL")}");
                Console.WriteLine($"解法二 Actual: {FormatRanks(secondActual)}");
                Console.WriteLine($"解法二結果：{(secondPassed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
        }



        /// <summary>
        /// 將每筆分數與原始索引配對，依分數由高到低排序後決定名次，
        /// 再透過保存的索引把名次放回原始選手順序。
        /// 輸入必須至少包含一筆資料，且所有分數都在題目範圍內並互不重複。
        /// 此方法不會修改傳入的分數陣列。
        /// </summary>
        /// <param name="score">依原始選手順序排列且互不重複的分數。</param>
        /// <returns>與輸入順序相同的名次文字陣列。</returns>
        /// <remarks>
        /// 參考資料：
        /// https://leetcode.cn/problems/relative-ranks/solutions/1131693/xiang-dui-ming-ci-by-leetcode-solution-5sua/
        /// https://leetcode.cn/problems/relative-ranks/solutions/1133453/gong-shui-san-xie-jian-dan-pai-xu-mo-ni-cmuzj/
        /// https://leetcode.cn/problems/relative-ranks/solutions/1509971/506-xiang-dui-ming-ci-by-stormsunshine-7gyl/
        /// </remarks>
        public static string[] FindRelativeRanks(int[] score)
        {
            int[][] scoreWithOriginalIndexes = new int[score.Length][];

            for (int index = 0; index < score.Length; index++)
            {
                // 排序會改變資料位置，因此同時保存分數與原始索引。
                scoreWithOriginalIndexes[index] = [score[index], index];
            }

            Array.Sort(
                scoreWithOriginalIndexes,
                (first, second) => second[0].CompareTo(first[0])
            );

            string[] answer = new string[score.Length];

            for (int rankIndex = 0; rankIndex < scoreWithOriginalIndexes.Length; rankIndex++)
            {
                int originalIndex = scoreWithOriginalIndexes[rankIndex][1];
                answer[originalIndex] = GetRankLabel(rankIndex);
            }

            return answer;
        }

        /// <summary>
        /// 依分數副本的排序結果建立「分數到名次」的映射，再按照原始輸入順序回傳每位選手的名次。
        /// 輸入必須符合題目條件：陣列至少包含一筆資料、分數皆在合法範圍內且互不重複。
        /// 此方法不會修改傳入的分數陣列。
        /// </summary>
        /// <param name="score">依原始選手順序排列且互不重複的分數。</param>
        /// <returns>與輸入順序相同的名次文字陣列。</returns>
        public static string[] FindRelativeRanks2(int[] score)
        {
            int[] sortedScores = [.. score];
            Array.Sort(sortedScores);

            var rankByScore = new Dictionary<int, string>(score.Length);

            for (int rankIndex = 0; rankIndex < sortedScores.Length; rankIndex++)
            {
                int currentScore = sortedScores[sortedScores.Length - 1 - rankIndex];
                // 唯一分數可安全作為鍵，讓原始順序中的選手直接查回名次。
                rankByScore[currentScore] = GetRankLabel(rankIndex);
            }

            string[] answer = new string[score.Length];

            for (int index = 0; index < score.Length; index++)
            {
                // 走訪原陣列而非排序副本，確保輸出順序與輸入完全一致。
                answer[index] = rankByScore[score[index]];
            }

            return answer;
        }

        /// <summary>
        /// 將從零開始的排序位置轉換為題目要求的名次文字：
        /// 前三名使用獎牌名稱，其餘名次使用從一開始的十進位數字。
        /// </summary>
        /// <param name="rankIndex">從零開始的排序位置。</param>
        /// <returns>對應的獎牌名稱或名次數字。</returns>
        private static string GetRankLabel(int rankIndex)
        {
            return rankIndex switch
            {
                0 => "Gold Medal",
                1 => "Silver Medal",
                2 => "Bronze Medal",
                _ => (rankIndex + 1).ToString()
            };
        }

        /// <summary>
        /// 將分數集合格式化為主控台與 README 共用的陣列表示法。
        /// </summary>
        /// <param name="scores">要顯示的分數集合。</param>
        /// <returns>以方括號包住、逗號分隔的分數字串。</returns>
        private static string FormatScores(IEnumerable<int> scores)
        {
            return $"[{string.Join(", ", scores)}]";
        }

        /// <summary>
        /// 將名次集合格式化為主控台與 README 共用的帶引號陣列表示法。
        /// </summary>
        /// <param name="ranks">要顯示的名次集合。</param>
        /// <returns>以方括號包住、每個名次加上雙引號的字串。</returns>
        private static string FormatRanks(IEnumerable<string> ranks)
        {
            return $"[{string.Join(", ", ranks.Select(rank => $"\"{rank}\""))}]";
        }
    }
}