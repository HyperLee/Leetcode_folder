namespace leetcode_046
{
    internal class Program
    {
        /// <summary>
        /// 46. Permutations
        /// https://leetcode.com/problems/permutations/description/
        /// <para>
        /// Given an array nums of distinct integers, return all possible permutations. You may return the answer in any order.
        ///
        /// Example 1:
        /// Input: nums = [1,2,3]
        /// Output: [[1,2,3],[1,3,2],[2,1,3],[2,3,1],[3,1,2],[3,2,1]]
        ///
        /// Example 2:
        /// Input: nums = [0,1]
        /// Output: [[0,1],[1,0]]
        ///
        /// Example 3:
        /// Input: nums = [1]
        /// Output: [[1]]
        ///
        /// Constraints:
        /// - 1 &lt;= nums.length &lt;= 6
        /// - -10 &lt;= nums[i] &lt;= 10
        /// - All integers in nums are unique.
        /// </para>
        /// <para>
        /// 46. 全排列
        /// https://leetcode.cn/problems/permutations/description/
        ///
        /// 給定一個由不同整數組成的陣列 nums，請回傳所有可能的排列。答案可以任意順序回傳。
        ///
        /// 範例 1：
        /// 輸入：nums = [1,2,3]
        /// 輸出：[[1,2,3],[1,3,2],[2,1,3],[2,3,1],[3,1,2],[3,2,1]]
        ///
        /// 範例 2：
        /// 輸入：nums = [0,1]
        /// 輸出：[[0,1],[1,0]]
        ///
        /// 範例 3：
        /// 輸入：nums = [1]
        /// 輸出：[[1]]
        ///
        /// 限制條件：
        /// - 1 &lt;= nums.length &lt;= 6
        /// - -10 &lt;= nums[i] &lt;= 10
        /// - nums 中的所有整數都不相同。
        /// </para>
        /// </summary>
        /// <remarks>
        /// 主要進入點不使用命令列參數，會執行三組固定案例，驗證兩種回溯解法並輸出 PASS/FAIL 摘要。
        /// </remarks>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var testCases = new (string Name, int[] Input, int[][] Expected)[]
            {
                (
                    "Case 1",
                    new[] { 1, 2, 3 },
                    new[]
                    {
                        new[] { 1, 2, 3 },
                        new[] { 1, 3, 2 },
                        new[] { 2, 1, 3 },
                        new[] { 2, 3, 1 },
                        new[] { 3, 1, 2 },
                        new[] { 3, 2, 1 }
                    }
                ),
                (
                    "Case 2",
                    new[] { 0, 1 },
                    new[]
                    {
                        new[] { 0, 1 },
                        new[] { 1, 0 }
                    }
                ),
                (
                    "Case 3",
                    new[] { 1 },
                    new[]
                    {
                        new[] { 1 }
                    }
                )
            };

            int passed = 0;
            int total = 0;

            foreach (var testCase in testCases)
            {
                passed += RunTestCase(testCase.Name, testCase.Input, testCase.Expected);
                total += 2;
            }

            Console.WriteLine($"Overall: {passed}/{total} passed.");
        }

        /// <summary>
        /// 執行單一固定案例，分別呼叫路徑回溯法與交換回溯法，並以完整排列集合驗證結果。
        /// 輸入必須符合題目條件：陣列長度為 1 到 6、元素互不相同；回傳通過驗證的解法數量。
        /// </summary>
        /// <param name="caseName">顯示於主控台的案例名稱。</param>
        /// <param name="input">要產生全排列的相異整數陣列。</param>
        /// <param name="expected">該輸入預期產生的完整排列集合。</param>
        /// <returns>此案例中通過驗證的解法數量，範圍為 0 到 2。</returns>
        private static int RunTestCase(string caseName, int[] input, IReadOnlyList<int[]> expected)
        {
            var solutions = new (string Name, Func<int[], IList<IList<int>>> Solve)[]
            {
                (nameof(Permute), Permute),
                (nameof(PermuteBySwapping), PermuteBySwapping)
            };

            HashSet<string> expectedSet = NormalizePermutations(expected);
            int passed = 0;

            Console.WriteLine($"{caseName}: nums = {FormatArray(input)}");

            foreach (var solution in solutions)
            {
                int[] inputCopy = (int[])input.Clone();
                IList<IList<int>> actual = solution.Solve(inputCopy);
                HashSet<string> actualSet = NormalizePermutations(actual);
                bool inputUnchanged = input.SequenceEqual(inputCopy);
                bool isCorrect =
                    actual.Count == expected.Count &&
                    actualSet.Count == actual.Count &&
                    actualSet.SetEquals(expectedSet) &&
                    inputUnchanged;

                if (isCorrect)
                {
                    passed++;
                }

                Console.WriteLine(
                    $"  {solution.Name}: {(isCorrect ? "PASS" : "FAIL")} " +
                    $"(expected: {expected.Count}, actual: {actual.Count}, " +
                    $"unique: {actualSet.Count}, input unchanged: {(inputUnchanged ? "Yes" : "No")})");
            }

            return passed;
        }

        /// <summary>
        /// 將排列集合轉成可比較的字串集合，使驗證不受各解法的列舉順序影響。
        /// 輸入中的每個排列皆可包含任意整數；回傳值可用於集合相等與重複排列檢查。
        /// </summary>
        /// <param name="permutations">要正規化的排列集合。</param>
        /// <returns>以逗號分隔各排列內容的唯一字串集合。</returns>
        private static HashSet<string> NormalizePermutations(IEnumerable<IEnumerable<int>> permutations)
        {
            return permutations
                .Select(permutation => string.Join(",", permutation))
                .ToHashSet();
        }

        /// <summary>
        /// 將整數序列格式化為易讀的方括號表示法，供固定案例輸出使用。
        /// 輸入可為任意整數序列；回傳格式例如 <c>[1, 2, 3]</c>。
        /// </summary>
        /// <param name="values">要格式化的整數序列。</param>
        /// <returns>以逗號與空白分隔的方括號字串。</returns>
        private static string FormatArray(IEnumerable<int> values)
        {
            return $"[{string.Join(", ", values)}]";
        }


        /// <summary>
        /// 使用路徑清單進行回溯，逐層選取尚未出現在路徑中的元素，產生所有可能的全排列。
        /// 輸入必須包含 1 到 6 個互不相同的整數；回傳所有排列，排列順序不限定，且不修改輸入陣列。
        /// </summary>
        /// <param name="nums">要產生全排列的相異整數陣列。</param>
        /// <returns>包含 <c>nums.Length!</c> 組結果的排列集合。</returns>
        public static IList<IList<int>> Permute(int[] nums)
        {
            IList<IList<int>> result = new List<IList<int>>();
            List<int> path = new List<int>();
            Backtrack(nums, path, result);
            return result;
        }


        /// <summary>
        /// 延伸路徑清單中的部分排列，透過「選擇、遞迴、撤銷選擇」走訪完整排列樹。
        /// 輸入陣列的元素必須互不相同；當路徑長度等於輸入長度時，將路徑副本加入結果集合。
        /// </summary>
        /// <param name="nums">提供每一層候選值的相異整數陣列。</param>
        /// <param name="path">目前已選取的部分排列。</param>
        /// <param name="result">收集完整排列的結果集合。</param>
        private static void Backtrack(int[] nums, List<int> path, IList<IList<int>> result)
        {
            if (path.Count == nums.Length)
            {
                // 必須複製目前路徑，否則後續回溯會改動已加入的結果。
                result.Add(new List<int>(path));
                return;
            }

            for (int i = 0; i < nums.Length; i++)
            {
                // 相異元素在同一條路徑中只能使用一次。
                if (path.Contains(nums[i]))
                {
                    continue;
                }

                path.Add(nums[i]);
                Backtrack(nums, path, result);
                path.RemoveAt(path.Count - 1);
            }
        }

        /// <summary>
        /// 使用原地交換概念進行回溯，依序固定每個索引位置並交換其後的候選元素。
        /// 輸入必須包含 1 到 6 個互不相同的整數；方法在內部複製陣列，因此回傳所有排列後不會修改呼叫端輸入。
        /// </summary>
        /// <param name="nums">要產生全排列的相異整數陣列。</param>
        /// <returns>包含 <c>nums.Length!</c> 組結果的排列集合。</returns>
        public static IList<IList<int>> PermuteBySwapping(int[] nums)
        {
            int[] workingCopy = (int[])nums.Clone();
            IList<IList<int>> result = new List<IList<int>>();
            BacktrackBySwapping(workingCopy, 0, result);
            return result;
        }

        /// <summary>
        /// 固定 <paramref name="startIndex"/> 之前的排列前綴，逐一交換候選值到目前位置並遞迴處理下一格。
        /// 輸入陣列的元素必須互不相同；當索引到達陣列尾端時，將目前陣列副本加入結果集合。
        /// </summary>
        /// <param name="nums">目前正在交換與還原的工作陣列。</param>
        /// <param name="startIndex">本層要固定的索引位置，範圍為 0 到 <c>nums.Length</c>。</param>
        /// <param name="result">收集完整排列的結果集合。</param>
        private static void BacktrackBySwapping(
            int[] nums,
            int startIndex,
            IList<IList<int>> result)
        {
            if (startIndex == nums.Length)
            {
                result.Add(new List<int>(nums));
                return;
            }

            for (int candidateIndex = startIndex; candidateIndex < nums.Length; candidateIndex++)
            {
                Swap(nums, startIndex, candidateIndex);
                BacktrackBySwapping(nums, startIndex + 1, result);

                // 還原本層交換，讓下一個候選值從相同的排列狀態開始。
                Swap(nums, startIndex, candidateIndex);
            }
        }

        /// <summary>
        /// 交換工作陣列中的兩個位置，供交換回溯法進行選擇與還原。
        /// 索引必須位於陣列有效範圍內；方法完成後兩個指定位置的值會互換。
        /// </summary>
        /// <param name="nums">要原地交換內容的工作陣列。</param>
        /// <param name="firstIndex">第一個有效索引。</param>
        /// <param name="secondIndex">第二個有效索引。</param>
        private static void Swap(int[] nums, int firstIndex, int secondIndex)
        {
            (nums[firstIndex], nums[secondIndex]) = (nums[secondIndex], nums[firstIndex]);
        }

    }
}
