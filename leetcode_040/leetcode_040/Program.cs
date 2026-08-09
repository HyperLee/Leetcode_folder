namespace leetcode_040
{
    internal class Program
    {
        /// <summary>
        /// 40. Combination Sum II
        /// https://leetcode.com/problems/combination-sum-ii/description/
        /// <para>
        /// Given a collection of candidate numbers, candidates, and a target number, target, find all unique combinations in candidates where the candidate numbers sum to target.
        ///
        /// Each number in candidates may be used only once in a combination.
        ///
        /// Note: The solution set must not contain duplicate combinations.
        ///
        /// Example 1:
        /// Input: candidates = [10,1,2,7,6,1,5], target = 8
        /// Output:
        /// [
        /// [1,1,6],
        /// [1,2,5],
        /// [1,7],
        /// [2,6]
        /// ]
        ///
        /// Example 2:
        /// Input: candidates = [2,5,2,1,2], target = 5
        /// Output:
        /// [
        /// [1,2,2],
        /// [5]
        /// ]
        ///
        /// Constraints:
        /// - 1 &lt;= candidates.length &lt;= 100
        /// - 1 &lt;= candidates[i] &lt;= 50
        /// - 1 &lt;= target &lt;= 30
        /// </para>
        /// <para>
        /// 40. 組合總和 II
        /// https://leetcode.cn/problems/combination-sum-ii/description/
        ///
        /// 給定一組候選數字 candidates 和一個目標數字 target，請找出 candidates 中所有數字總和等於 target 的唯一組合。
        ///
        /// candidates 中的每個數字在一個組合中只能使用一次。
        ///
        /// 注意：答案集合不得包含重複的組合。
        ///
        /// 範例 1：
        /// 輸入：candidates = [10,1,2,7,6,1,5], target = 8
        /// 輸出：
        /// [
        /// [1,1,6],
        /// [1,2,5],
        /// [1,7],
        /// [2,6]
        /// ]
        ///
        /// 範例 2：
        /// 輸入：candidates = [2,5,2,1,2], target = 5
        /// 輸出：
        /// [
        /// [1,2,2],
        /// [5]
        /// ]
        ///
        /// 限制條件：
        /// - 1 &lt;= candidates.length &lt;= 100
        /// - 1 &lt;= candidates[i] &lt;= 50
        /// - 1 &lt;= target &lt;= 30
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            (int[] Candidates, int Target, int[][] Expected)[] testCases =
            [
                (
                    [10, 1, 2, 7, 6, 1, 5],
                    8,
                    [[1, 1, 6], [1, 2, 5], [1, 7], [2, 6]]
                ),
                ([2, 5, 2, 1, 2], 5, [[1, 2, 2], [5]]),
                ([2], 1, []),
                ([1, 1, 1, 1], 2, [[1, 1]]),
                ([3, 1, 2], 3, [[1, 2], [3]]),
                ([1, 1], 3, []),
                ([30], 30, [[30]]),
                ([50], 30, []),
            ];

            int passedChecks = 0;
            int totalChecks = 0;

            for (int index = 0; index < testCases.Length; index++)
            {
                (int[] candidates, int target, int[][] expected) = testCases[index];
                int[] firstInput = [.. candidates];
                int[] secondInput = [.. candidates];

                IList<IList<int>> firstActual = CombinationSum2(firstInput, target);
                IList<IList<int>> secondActual = CombinationSum2ByFrequency(secondInput, target);
                string expectedText = FormatCombinations(expected);
                string firstActualText = FormatCombinations(firstActual);
                string secondActualText = FormatCombinations(secondActual);
                bool firstResultPassed = firstActualText == expectedText;
                bool firstInputPassed = firstInput.SequenceEqual(candidates);
                bool secondResultPassed = secondActualText == expectedText;
                bool secondInputPassed = secondInput.SequenceEqual(candidates);

                bool[] caseChecks =
                [
                    firstResultPassed,
                    firstInputPassed,
                    secondResultPassed,
                    secondInputPassed,
                ];

                passedChecks += caseChecks.Count(check => check);
                totalChecks += caseChecks.Length;

                Console.WriteLine($"案例 {index + 1}");
                Console.WriteLine($"輸入：candidates = [{string.Join(", ", candidates)}], target = {target}");
                Console.WriteLine($"預期：{expectedText}");
                Console.WriteLine($"解法一：{firstActualText} => {(firstResultPassed ? "PASS" : "FAIL")}");
                Console.WriteLine($"解法一輸入未變：{(firstInputPassed ? "PASS" : "FAIL")}");
                Console.WriteLine($"解法二：{secondActualText} => {(secondResultPassed ? "PASS" : "FAIL")}");
                Console.WriteLine($"解法二輸入未變：{(secondInputPassed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項檢查通過");
        }

        /// <summary>
        /// 將組合集合正規化為固定文字格式，讓不同列舉順序但內容相同的答案可以比較。
        /// 每個組合依數值排序，再將所有組合依字典順序排列。
        /// </summary>
        /// <param name="combinations">要正規化的組合集合；每個內層序列代表一組答案。</param>
        /// <returns>格式為 <c>[[數字, ...], [...]]</c> 的穩定文字；空集合回傳 <c>[]</c>。</returns>
        private static string FormatCombinations(IEnumerable<IEnumerable<int>> combinations)
        {
            IEnumerable<string> normalized = combinations
                .Select(combination => $"[{string.Join(", ", combination.Order())}]")
                .Order(StringComparer.Ordinal);

            return $"[{string.Join(", ", normalized)}]";
        }


        /// <summary>
        /// 找出所有總和等於 <paramref name="target"/> 的不重複組合。
        /// 方法先複製並排序候選陣列，再以索引遞增的回溯搜尋確保每個位置最多使用一次，
        /// 並跳過同一搜尋層的相同數值以避免重複答案。
        /// </summary>
        /// <param name="candidates">由正整數組成、可能包含重複值的陣列；呼叫後內容與順序保持不變。</param>
        /// <param name="target">要由候選數字加總而成的正整數目標值。</param>
        /// <returns>所有總和等於 <paramref name="target"/> 的唯一組合；無解時回傳空集合。</returns>
        public static IList<IList<int>> CombinationSum2(int[] candidates, int target)
        {
            int[] sortedCandidates = [.. candidates];
            IList<IList<int>> result = new List<IList<int>>();
            List<int> combination = new List<int>();

            // 使用副本排序，既能支援剪枝與同層去重，也不會改動呼叫端資料。
            Array.Sort(sortedCandidates);
            Backtrack(sortedCandidates, target, 0, combination, result);

            return result;
        }


        /// <summary>
        /// 從指定索引開始搜尋剩餘目標值，以索引遞增限制每個陣列位置只能使用一次。
        /// 已排序資料可在候選值過大時停止搜尋，且同一層只探索一次相同數值。
        /// </summary>
        /// <param name="candidates">已按遞增順序排序的正整數陣列。</param>
        /// <param name="remainingTarget">目前仍需湊出的非負整數總和。</param>
        /// <param name="start">本層可以選擇的最小候選索引。</param>
        /// <param name="combination">目前遞迴路徑中已選擇的數字。</param>
        /// <param name="result">收集所有已完成組合的結果集合。</param>
        private static void Backtrack(
            int[] candidates,
            int remainingTarget,
            int start,
            List<int> combination,
            IList<IList<int>> result)
        {
            if (remainingTarget == 0)
            {
                // 必須複製目前路徑，避免後續撤銷選擇時改動已保存的答案。
                result.Add(new List<int>(combination));
                return;
            }

            for (int index = start; index < candidates.Length; index++)
            {
                if (candidates[index] > remainingTarget)
                {
                    // 陣列已排序，後續數值只會更大，可直接結束本層搜尋。
                    break;
                }

                if (index > start && candidates[index] == candidates[index - 1])
                {
                    // 只跳過同一層的重複值；下一層仍可使用另一個相同數值。
                    continue;
                }

                combination.Add(candidates[index]);
                Backtrack(
                    candidates,
                    remainingTarget - candidates[index],
                    index + 1,
                    combination,
                    result);

                // 撤銷本次選擇，恢復進入這個分支前的路徑。
                combination.RemoveAt(combination.Count - 1);
            }
        }

        /// <summary>
        /// 找出所有總和等於 <paramref name="target"/> 的不重複組合。
        /// 方法先複製、排序並將相同數值壓縮成頻率群組，再枚舉每個數值可取用的次數，
        /// 從資料模型上限制使用量並避免建立重複分支。
        /// </summary>
        /// <param name="candidates">由正整數組成、可能包含重複值的陣列；呼叫後內容與順序保持不變。</param>
        /// <param name="target">要由候選數字加總而成的正整數目標值。</param>
        /// <returns>所有總和等於 <paramref name="target"/> 的唯一組合；無解時回傳空集合。</returns>
        public static IList<IList<int>> CombinationSum2ByFrequency(int[] candidates, int target)
        {
            int[] sortedCandidates = [.. candidates];
            List<(int Value, int Count)> frequencies = new List<(int Value, int Count)>();
            IList<IList<int>> result = new List<IList<int>>();
            List<int> combination = new List<int>();

            Array.Sort(sortedCandidates);

            foreach (int candidate in sortedCandidates)
            {
                if (frequencies.Count == 0 || frequencies[^1].Value != candidate)
                {
                    frequencies.Add((candidate, 1));
                    continue;
                }

                (int value, int count) = frequencies[^1];
                frequencies[^1] = (value, count + 1);
            }

            BacktrackByFrequency(frequencies, target, 0, combination, result);

            return result;
        }

        /// <summary>
        /// 依頻率群組索引搜尋剩餘目標值，枚舉目前數值從零次到可用上限的所有選擇。
        /// 每次遞迴都前進到下一個群組，因此取用次數不會超過輸入中的實際出現次數。
        /// </summary>
        /// <param name="frequencies">依數值遞增排列的候選值及其可用次數。</param>
        /// <param name="remainingTarget">目前仍需湊出的非負整數總和。</param>
        /// <param name="groupIndex">本層要決定取用次數的頻率群組索引。</param>
        /// <param name="combination">目前遞迴路徑中已選擇的數字。</param>
        /// <param name="result">收集所有已完成組合的結果集合。</param>
        private static void BacktrackByFrequency(
            IReadOnlyList<(int Value, int Count)> frequencies,
            int remainingTarget,
            int groupIndex,
            List<int> combination,
            IList<IList<int>> result)
        {
            if (remainingTarget == 0)
            {
                result.Add(new List<int>(combination));
                return;
            }

            if (groupIndex == frequencies.Count
                || frequencies[groupIndex].Value > remainingTarget)
            {
                return;
            }

            (int value, int availableCount) = frequencies[groupIndex];
            int maximumUses = Math.Min(availableCount, remainingTarget / value);

            // 先探索不使用目前數值，再逐次加入一個，涵蓋 0 到 maximumUses 次。
            BacktrackByFrequency(
                frequencies,
                remainingTarget,
                groupIndex + 1,
                combination,
                result);

            for (int usedCount = 1; usedCount <= maximumUses; usedCount++)
            {
                combination.Add(value);
                BacktrackByFrequency(
                    frequencies,
                    remainingTarget - (value * usedCount),
                    groupIndex + 1,
                    combination,
                    result);
            }

            // 一次移除本層加入的所有項目，將路徑還原給上一層。
            combination.RemoveRange(combination.Count - maximumUses, maximumUses);
        }

    }
}