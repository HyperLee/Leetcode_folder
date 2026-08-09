namespace leetcode_1608
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 1608. Special Array With X Elements Greater Than or Equal X
        /// https://leetcode.com/problems/special-array-with-x-elements-greater-than-or-equal-x/description/
        ///
        /// You are given an array nums of non-negative integers. nums is special if there is a number x such that exactly x
        /// numbers in nums are greater than or equal to x. x does not have to be an element in nums.
        /// Return x if nums is special; otherwise return -1. If nums is special, x is unique.
        ///
        /// Example 1:
        /// Input: nums = [3,5]
        /// Output: 2
        /// Explanation: There are 2 values, 3 and 5, that are greater than or equal to 2.
        ///
        /// Example 2:
        /// Input: nums = [0,0]
        /// Output: -1
        /// Explanation: No x fits. For x = 0 there should be 0 values &gt;= x, but there are 2; for x = 1 there should be
        /// 1, but there are 0; for x = 2 there should be 2, but there are 0. x cannot be larger because nums has 2 values.
        ///
        /// Example 3:
        /// Input: nums = [0,4,3,0,4]
        /// Output: 3
        /// Explanation: There are 3 values greater than or equal to 3.
        ///
        /// Constraints:
        /// - 1 &lt;= nums.length &lt;= 100
        /// - 0 &lt;= nums[i] &lt;= 1000
        /// </para>
        /// <para>
        /// 1608. 特殊陣列的特徵值
        /// https://leetcode.cn/problems/special-array-with-x-elements-greater-than-or-equal-x/description/
        ///
        /// 給定非負整數陣列 nums。若存在數字 x，使 nums 中恰好有 x 個數字大於或等於 x，則 nums 是特殊
        /// 陣列；x 不必是 nums 的元素。若 nums 特殊，回傳 x；否則回傳 -1。特殊陣列的 x 是唯一的。
        ///
        /// 範例 1：
        /// 輸入：nums = [3,5]
        /// 輸出：2
        /// 解釋：有 2 個值 3 與 5 大於或等於 2。
        ///
        /// 範例 2：
        /// 輸入：nums = [0,0]
        /// 輸出：-1
        /// 解釋：沒有符合條件的 x。x = 0 時應有 0 個值 &gt;= x，但實際有 2 個；x = 1 時應有 1 個，但
        /// 實際有 0 個；x = 2 時應有 2 個，但實際有 0 個。nums 只有 2 個值，因此 x 不可能更大。
        ///
        /// 範例 3：
        /// 輸入：nums = [0,4,3,0,4]
        /// 輸出：3
        /// 解釋：有 3 個值大於或等於 3。
        ///
        /// 限制條件：
        /// - 1 &lt;= nums.length &lt;= 100
        /// - 0 &lt;= nums[i] &lt;= 1000
        /// </para>
        /// </summary>
        /// <remarks>
        /// 以固定案例比較三種解法，同時驗證回傳值與輸入陣列是否保持不變。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用。</param>
        /// <returns>所有檢查通過時回傳 0，否則回傳 1。</returns>
        static int Main(string[] args)
        {
            (string Name, int[] Input, int Expected)[] testCases =
            [
                ("官方範例 1", [3, 5], 2),
                ("全為零", [0, 0], -1),
                ("含重複值的官方範例", [0, 4, 3, 0, 4], 3),
                ("單一元素且有解", [1], 1),
                ("元素值上界", [1000], 1),
                ("無符合的候選值", [3, 6, 7, 7, 0], -1),
                ("特徵值等於陣列長度", [4, 4, 4, 4], 4)
            ];

            (string Name, Func<int[], int> Solve)[] solutions =
            [
                (nameof(SpecialArray), SpecialArray),
                (nameof(SpecialArray2), SpecialArray2),
                (nameof(SpecialArray3), SpecialArray3)
            ];

            int passedChecks = 0;
            int totalChecks = testCases.Length * solutions.Length;

            for (int caseIndex = 0; caseIndex < testCases.Length; caseIndex++)
            {
                (string caseName, int[] input, int expected) = testCases[caseIndex];
                Console.WriteLine($"Case {caseIndex + 1}: {caseName}");
                Console.WriteLine($"Input: [{string.Join(", ", input)}]");
                Console.WriteLine($"Expected: {expected}");

                foreach ((string solutionName, Func<int[], int> solve) in solutions)
                {
                    int[] workingInput = [.. input];
                    int[] originalInput = [.. workingInput];
                    int actual = solve(workingInput);
                    bool inputUnchanged = workingInput.SequenceEqual(originalInput);
                    bool passed = actual == expected && inputUnchanged;

                    Console.WriteLine($"  {solutionName}");
                    Console.WriteLine($"    Actual: {actual}");
                    Console.WriteLine($"    Input unchanged: {inputUnchanged}");
                    Console.WriteLine($"    Result: {(passed ? "PASS" : "FAIL")}");

                    if (passed)
                    {
                        passedChecks++;
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
            return passedChecks == totalChecks ? 0 : 1;
        }

        /// <summary>
        /// 將輸入副本降序排列，掃描候選值並檢查其左右分界是否恰好對應特徵值。
        /// </summary>
        /// <param name="nums">長度為 1 至 100、元素值為 0 至 1000 的整數陣列；方法不會修改此陣列。</param>
        /// <returns>若恰有 x 個元素大於或等於 x，回傳唯一的 x；否則回傳 -1。</returns>
        public static int SpecialArray(int[] nums)
        {
            // 排序只作用於副本，避免改變呼叫端的輸入順序。
            int[] sortedNumbers = [.. nums];
            Array.Sort(sortedNumbers);
            Array.Reverse(sortedNumbers);

            for (int candidate = 1; candidate <= sortedNumbers.Length; candidate++)
            {
                // 降序後，分界左側需全部 >= candidate，右側第一個值則必須 < candidate。
                bool leftSideQualifies = sortedNumbers[candidate - 1] >= candidate;
                bool rightSideExcluded = candidate == sortedNumbers.Length || sortedNumbers[candidate] < candidate;

                if (leftSideQualifies && rightSideExcluded)
                {
                    return candidate;
                }
            }

            return -1;
        }

        /// <summary>
        /// 逐一嘗試 1 到陣列長度的候選值，每次完整統計大於或等於候選值的元素數量。
        /// </summary>
        /// <param name="nums">長度為 1 至 100、元素值為 0 至 1000 的整數陣列；方法不會修改此陣列。</param>
        /// <returns>若恰有 x 個元素大於或等於 x，回傳唯一的 x；否則回傳 -1。</returns>
        public static int SpecialArray2(int[] nums)
        {
            for (int candidate = 1; candidate <= nums.Length; candidate++)
            {
                int qualifyingCount = 0;

                foreach (int number in nums)
                {
                    if (number >= candidate)
                    {
                        qualifyingCount++;
                    }
                }

                if (qualifyingCount == candidate)
                {
                    return candidate;
                }
            }

            return -1;
        }

        /// <summary>
        /// 將數值壓縮到 0 至陣列長度的計數桶，再由右向左累加以取得每個候選值的合格數量。
        /// </summary>
        /// <param name="nums">長度為 1 至 100、元素值為 0 至 1000 的整數陣列；方法不會修改此陣列。</param>
        /// <returns>若恰有 x 個元素大於或等於 x，回傳唯一的 x；否則回傳 -1。</returns>
        public static int SpecialArray3(int[] nums)
        {
            int[] counts = new int[nums.Length + 1];

            foreach (int number in nums)
            {
                // 候選值不會超過 n，因此所有 >= n 的數都可放入同一個桶。
                counts[Math.Min(number, nums.Length)]++;
            }

            int qualifyingCount = 0;

            for (int candidate = nums.Length; candidate >= 1; candidate--)
            {
                // 後綴累加值就是大於或等於 candidate 的元素數量。
                qualifyingCount += counts[candidate];

                if (qualifyingCount == candidate)
                {
                    return candidate;
                }
            }

            return -1;
        }
    }
}