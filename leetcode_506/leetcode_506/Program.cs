namespace leetcode_506
{
    internal class Program
    {
        /// <summary>
        /// 506. Relative Ranks
        /// https://leetcode.com/problems/relative-ranks/?envType=daily-question&envId=2024-05-08
        /// 506. 相对名次
        /// https://leetcode.cn/problems/relative-ranks/description/
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