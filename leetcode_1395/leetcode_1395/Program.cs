namespace leetcode_1395
{
    internal class Program
    {
        /// <summary>
        /// 1395. Count Number of Teams
        /// https://leetcode.com/problems/count-number-of-teams/description/?envType=daily-question&envId=2024-07-29
        /// 1395. 统计作战单位数
        /// https://leetcode.cn/problems/count-number-of-teams/description/
        /// 
        /// 有 n 個士兵排成一列。每位士兵都有獨特的評分值。
        /// 
        /// 簡單說就是找出 3 個評分是 遞增 或是 遞減的組合 
        /// 3 個index分別是 i, j, k,  0 <= i < j < k < n
        /// index 可以是非連續
        /// 
        /// </summary>
        /// <remarks>
        /// 執行固定測試案例，分別比對三層枚舉與枚舉中間點兩種解法的預期值與實際值。
        /// 任一檢查失敗時，程式會設定非零結束碼，方便在終端機或自動化環境中驗收。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用此參數。</param>
        static void Main(string[] args)
        {
            var testCases = new (string Name, int[] Rating, int Expected)[]
            {
                ("官方範例：遞增與遞減隊伍並存", new[] { 2, 5, 3, 4, 1 }, 3),
                ("最小長度：無法形成有效隊伍", new[] { 2, 1, 3 }, 0),
                ("完全遞增", new[] { 1, 2, 3, 4 }, 4),
                ("完全遞減", new[] { 4, 3, 2, 1 }, 4),
                ("混合排列", new[] { 1, 3, 2, 4 }, 2)
            };

            int passedChecks = 0;
            int totalChecks = testCases.Length * 2;

            for (int caseIndex = 0; caseIndex < testCases.Length; caseIndex++)
            {
                var testCase = testCases[caseIndex];
                int enumerationActual = NumTeams(testCase.Rating);
                int middlePointActual = NumTeams2(testCase.Rating);
                bool enumerationPassed = enumerationActual == testCase.Expected;
                bool middlePointPassed = middlePointActual == testCase.Expected;

                passedChecks += enumerationPassed ? 1 : 0;
                passedChecks += middlePointPassed ? 1 : 0;

                Console.WriteLine($"Case {caseIndex + 1}: {testCase.Name}");
                Console.WriteLine($"Input: [{string.Join(", ", testCase.Rating)}]");
                Console.WriteLine($"Expected: {testCase.Expected}");
                Console.WriteLine($"NumTeams  Actual: {enumerationActual} | {(enumerationPassed ? "PASS" : "FAIL")}");
                Console.WriteLine($"NumTeams2 Actual: {middlePointActual} | {(middlePointPassed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
            Environment.ExitCode = passedChecks == totalChecks ? 0 : 1;
        }

        /// <summary>
        /// 以三層迴圈枚舉所有索引滿足 <c>i &lt; j &lt; k</c> 的三人組合，
        /// 計算評分嚴格遞增或嚴格遞減的有效隊伍數量。
        /// 輸入必須是至少包含三個互異正整數的非 <see langword="null"/> 陣列；回傳所有有效隊伍的總數。
        /// </summary>
        /// <remarks>
        /// 時間複雜度為 O(n³)，額外空間複雜度為 O(1)，且不會修改輸入陣列。
        /// </remarks>
        /// <param name="rating">依站位順序排列的士兵評分，評分值彼此互異。</param>
        /// <returns>符合嚴格遞增或嚴格遞減條件的三人士兵隊伍數量。</returns>
        public static int NumTeams(int[] rating)
        {
            int soldierCount = rating.Length;
            int teamCount = 0;

            for (int i = 0; i < soldierCount; i++)
            {
                for (int j = i + 1; j < soldierCount; j++)
                {
                    for (int k = j + 1; k < soldierCount; k++)
                    {
                        // 索引順序已由迴圈保證，這裡只需檢查三個評分是否保持同一單調方向。
                        if ((rating[i] < rating[j] && rating[j] < rating[k]) || (rating[i] > rating[j] && rating[j] > rating[k]))
                        {
                            teamCount++;
                        }
                    }
                }
            }

            return teamCount;
        }

        /// <summary>
        /// 依序固定每個中間位置 <c>j</c>，統計其左右兩側較低與較高的評分數量，
        /// 以乘法原理加總嚴格遞增與嚴格遞減的隊伍組合。
        /// 輸入必須是至少包含三個互異正整數的非 <see langword="null"/> 陣列；回傳所有有效隊伍的總數。
        /// </summary>
        /// <remarks>
        /// 時間複雜度為 O(n²)，額外空間複雜度為 O(1)，且不會修改輸入陣列。
        /// </remarks>
        /// <param name="rating">依站位順序排列的士兵評分，評分值彼此互異。</param>
        /// <returns>符合嚴格遞增或嚴格遞減條件的三人士兵隊伍數量。</returns>
        public static int NumTeams2(int[] rating)
        {
            int soldierCount = rating.Length;
            int teamCount = 0;

            // 中間點必須同時保留左、右兩側各至少一位士兵。
            for (int j = 1; j < soldierCount - 1; j++)
            {
                int leftLowerCount = 0;
                int leftHigherCount = 0;
                int rightLowerCount = 0;
                int rightHigherCount = 0;

                for (int i = 0; i < j; i++)
                {
                    if (rating[i] < rating[j])
                    {
                        leftLowerCount++;
                    }
                    else if (rating[i] > rating[j])
                    {
                        leftHigherCount++;
                    }
                }

                for (int k = j + 1; k < soldierCount; k++)
                {
                    if (rating[k] < rating[j])
                    {
                        rightLowerCount++;
                    }
                    else if (rating[k] > rating[j])
                    {
                        rightHigherCount++;
                    }
                }

                // 左低×右高形成遞增隊伍；左高×右低形成遞減隊伍。
                teamCount += leftLowerCount * rightHigherCount + leftHigherCount * rightLowerCount;
            }

            return teamCount;
        }
    }
}
