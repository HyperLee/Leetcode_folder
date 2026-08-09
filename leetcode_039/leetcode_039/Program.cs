namespace leetcode_039
{
    internal class Program
    {
        /// <summary>
        /// 39. Combination Sum
        /// https://leetcode.com/problems/combination-sum/description/
        /// <para>
        /// Given an array of distinct integers candidates and a target integer target, return a list of all unique combinations of candidates where the chosen numbers sum to target. You may return the combinations in any order.
        ///
        /// The same number may be chosen from candidates an unlimited number of times. Two combinations are unique if the frequency of at least one chosen number is different.
        ///
        /// The test cases are generated so that the number of unique combinations that sum to target is less than 150 for the given input.
        ///
        /// Example 1:
        /// Input: candidates = [2,3,6,7], target = 7
        /// Output: [[2,2,3],[7]]
        /// Explanation:
        /// 2 and 3 are candidates, and 2 + 2 + 3 = 7. The number 2 can be used multiple times.
        /// 7 is a candidate, and 7 = 7.
        /// These are the only two combinations.
        ///
        /// Example 2:
        /// Input: candidates = [2,3,5], target = 8
        /// Output: [[2,2,2,2],[2,3,3],[3,5]]
        ///
        /// Example 3:
        /// Input: candidates = [2], target = 1
        /// Output: []
        ///
        /// Constraints:
        /// - 1 &lt;= candidates.length &lt;= 30
        /// - 2 &lt;= candidates[i] &lt;= 40
        /// - All elements of candidates are distinct.
        /// - 1 &lt;= target &lt;= 40
        /// </para>
        /// <para>
        /// 39. 組合總和
        /// https://leetcode.cn/problems/combination-sum/description/
        ///
        /// 給定一個由不同整數組成的陣列 candidates 和一個目標整數 target，請回傳 candidates 中所選數字總和等於 target 的所有唯一組合。組合可以任意順序回傳。
        ///
        /// candidates 中的同一個數字可以選取無限次。若兩個組合中至少有一個所選數字的出現次數不同，則視為不同組合。
        ///
        /// 測試案例會保證，對給定輸入而言，總和為 target 的唯一組合數量少於 150。
        ///
        /// 範例 1：
        /// 輸入：candidates = [2,3,6,7], target = 7
        /// 輸出：[[2,2,3],[7]]
        /// 解釋：
        /// 2 和 3 都是 candidates 中的數字，且 2 + 2 + 3 = 7。數字 2 可以重複使用。
        /// 7 是 candidates 中的數字，且 7 = 7。
        /// 這是僅有的兩種組合。
        ///
        /// 範例 2：
        /// 輸入：candidates = [2,3,5], target = 8
        /// 輸出：[[2,2,2,2],[2,3,3],[3,5]]
        ///
        /// 範例 3：
        /// 輸入：candidates = [2], target = 1
        /// 輸出：[]
        ///
        /// 限制條件：
        /// - 1 &lt;= candidates.length &lt;= 30
        /// - 2 &lt;= candidates[i] &lt;= 40
        /// - candidates 的所有元素都不相同。
        /// - 1 &lt;= target &lt;= 40
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            (int[] Candidates, int Target, int[][] Expected)[] testCases =
            [
                ([2, 3, 6, 7], 7, [[2, 2, 3], [7]]),
                ([2, 3, 5], 8, [[2, 2, 2, 2], [2, 3, 3], [3, 5]]),
                ([2], 1, []),
                ([7, 3, 2, 6], 7, [[2, 2, 3], [7]]),
                ([40], 40, [[40]]),
            ];

            int passed = 0;

            for (int index = 0; index < testCases.Length; index++)
            {
                (int[] candidates, int target, int[][] expected) = testCases[index];
                int[] originalCandidates = [.. candidates];

                IList<IList<int>> actual = CombinationSum(candidates, target);
                string expectedText = FormatCombinations(expected);
                string actualText = FormatCombinations(actual);
                bool isPassed = expectedText == actualText;

                if (isPassed)
                {
                    passed++;
                }

                Console.WriteLine($"案例 {index + 1}");
                Console.WriteLine($"輸入：candidates = [{string.Join(", ", originalCandidates)}], target = {target}");
                Console.WriteLine($"預期：{expectedText}");
                Console.WriteLine($"實際：{actualText} => {(isPassed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passed}/{testCases.Length} 筆測試通過");
        }

        /// <summary>
        /// 找出所有總和等於 <paramref name="target"/> 的不重複候選數字組合。
        /// 解法先原地排序 <paramref name="candidates"/>，再以回溯法依索引由左至右選擇數字；
        /// 遞迴時保留目前索引以允許重複取用相同數字，並在候選值超過剩餘目標時提前剪枝。
        /// </summary>
        /// <param name="candidates">互不相同的正整數陣列；方法執行後會被原地排序為遞增順序。</param>
        /// <param name="target">要由候選數字加總而成的正整數目標值。</param>
        /// <returns>所有總和等於 <paramref name="target"/> 的唯一組合；若沒有可行組合則回傳空集合。</returns>
        public static IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            IList<IList<int>> result = new List<IList<int>>();
            List<int> combination = new List<int>();

            // 排序後才能在候選值超過剩餘目標時安全停止後續搜尋。
            Array.Sort(candidates);
            Backtrack(candidates, target, result, combination, 0);

            return result;
        }

        /// <summary>
        /// 從指定起始索引開始，以深度優先回溯搜尋剩餘目標值的所有組合。
        /// 起始索引限制下一層只能選擇目前或其後的候選數字，因此保留重複取值能力，
        /// 同時避免把相同數字集合的不同排列重複加入結果。
        /// </summary>
        /// <param name="candidates">已按遞增順序排序、元素互不相同的正整數陣列。</param>
        /// <param name="target">目前仍需湊出的非負整數總和。</param>
        /// <param name="result">收集所有已完成組合的結果集合。</param>
        /// <param name="combination">目前遞迴路徑中已選擇的數字。</param>
        /// <param name="start">本層可以選擇的最小候選索引。</param>
        public static void Backtrack(int[] candidates, int target, IList<IList<int>> result, List<int> combination, int start)
        {
            if (target == 0)
            {
                // 複製目前路徑，避免後續回溯修改已保存的答案。
                result.Add(new List<int>(combination));
                return;
            }

            for (int i = start; i < candidates.Length; i++)
            {
                if (candidates[i] > target)
                {
                    // 陣列已排序，後續候選值只會更大，可以直接結束本層搜尋。
                    break;
                }

                // 選擇目前數字；遞迴仍從 i 開始，代表同一數字可以重複使用。
                combination.Add(candidates[i]);
                Backtrack(candidates, target - candidates[i], result, combination, i);

                // 撤銷本次選擇，讓下一輪可以探索其他候選分支。
                combination.RemoveAt(combination.Count - 1);
            }
        }

        /// <summary>
        /// 將組合集合正規化為固定文字格式，供任意順序的預期結果與實際結果比較及顯示。
        /// 每個組合會先依數值排序，再將所有組合依字典順序排列，因此等價答案會得到相同輸出。
        /// </summary>
        /// <param name="combinations">要正規化的組合集合；每個內層序列代表一個候選數字組合。</param>
        /// <returns>格式為 <c>[[數字, ...], [...]]</c> 的穩定文字；空集合回傳 <c>[]</c>。</returns>
        private static string FormatCombinations(IEnumerable<IEnumerable<int>> combinations)
        {
            IEnumerable<string> normalized = combinations
                .Select(combination => $"[{string.Join(", ", combination.Order())}]")
                .Order(StringComparer.Ordinal);

            return $"[{string.Join(", ", normalized)}]";
        }
    }
}
