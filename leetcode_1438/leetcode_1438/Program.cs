namespace leetcode_1438
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 1438. Longest Continuous Subarray With Absolute Diff Less Than or Equal to Limit
        /// https://leetcode.com/problems/longest-continuous-subarray-with-absolute-diff-less-than-or-equal-to-limit/description/
        ///
        /// Given an array of integers nums and an integer limit, return the size of the longest non-empty subarray such that
        /// the absolute difference between any two elements of this subarray is less than or equal to limit.
        ///
        /// Example 1:
        /// Input: nums = [8,2,4,7], limit = 4
        /// Output: 2
        /// Explanation: All subarrays are:
        /// [8] with maximum absolute diff |8-8| = 0 &lt;= 4.
        /// [8,2] with maximum absolute diff |8-2| = 6 &gt; 4.
        /// [8,2,4] with maximum absolute diff |8-2| = 6 &gt; 4.
        /// [8,2,4,7] with maximum absolute diff |8-2| = 6 &gt; 4.
        /// [2] with maximum absolute diff |2-2| = 0 &lt;= 4.
        /// [2,4] with maximum absolute diff |2-4| = 2 &lt;= 4.
        /// [2,4,7] with maximum absolute diff |2-7| = 5 &gt; 4.
        /// [4] with maximum absolute diff |4-4| = 0 &lt;= 4.
        /// [4,7] with maximum absolute diff |4-7| = 3 &lt;= 4.
        /// [7] with maximum absolute diff |7-7| = 0 &lt;= 4.
        /// Therefore, the size of the longest subarray is 2.
        ///
        /// Example 2:
        /// Input: nums = [10,1,2,4,7,2], limit = 5
        /// Output: 4
        /// Explanation: The subarray [2,4,7,2] is the longest since the maximum absolute diff is |2-7| = 5 &lt;= 5.
        ///
        /// Example 3:
        /// Input: nums = [4,2,2,2,4,4,2,2], limit = 0
        /// Output: 3
        ///
        /// Constraints:
        /// - 1 &lt;= nums.length &lt;= 10^5
        /// - 1 &lt;= nums[i] &lt;= 10^9
        /// - 0 &lt;= limit &lt;= 10^9
        /// </para>
        /// <para>
        /// 1438. 絕對差不超過限制的最長連續子陣列
        /// https://leetcode.cn/problems/longest-continuous-subarray-with-absolute-diff-less-than-or-equal-to-limit/description/
        ///
        /// 給定整數陣列 nums 與整數 limit，回傳最長非空子陣列的長度，使該子陣列中任意兩個元素的絕對差
        /// 小於或等於 limit。
        ///
        /// 範例 1：
        /// 輸入：nums = [8,2,4,7]，limit = 4
        /// 輸出：2
        /// 解釋：所有子陣列如下：
        /// [8] 的最大絕對差 |8-8| = 0 &lt;= 4。
        /// [8,2] 的最大絕對差 |8-2| = 6 &gt; 4。
        /// [8,2,4] 的最大絕對差 |8-2| = 6 &gt; 4。
        /// [8,2,4,7] 的最大絕對差 |8-2| = 6 &gt; 4。
        /// [2] 的最大絕對差 |2-2| = 0 &lt;= 4。
        /// [2,4] 的最大絕對差 |2-4| = 2 &lt;= 4。
        /// [2,4,7] 的最大絕對差 |2-7| = 5 &gt; 4。
        /// [4] 的最大絕對差 |4-4| = 0 &lt;= 4。
        /// [4,7] 的最大絕對差 |4-7| = 3 &lt;= 4。
        /// [7] 的最大絕對差 |7-7| = 0 &lt;= 4。
        /// 因此，最長子陣列的長度為 2。
        ///
        /// 範例 2：
        /// 輸入：nums = [10,1,2,4,7,2]，limit = 5
        /// 輸出：4
        /// 解釋：子陣列 [2,4,7,2] 最長，因為最大絕對差為 |2-7| = 5 &lt;= 5。
        ///
        /// 範例 3：
        /// 輸入：nums = [4,2,2,2,4,4,2,2]，limit = 0
        /// 輸出：3
        ///
        /// 限制條件：
        /// - 1 &lt;= nums.length &lt;= 10^5
        /// - 1 &lt;= nums[i] &lt;= 10^9
        /// - 0 &lt;= limit &lt;= 10^9
        /// </para>
        /// </summary>
        /// <remarks>
        /// 執行固定的題目範例與回歸案例，逐一比較三種解法的結果，並以程序結束碼表示是否全部通過。
        /// </remarks>
        /// <param name="args">主控台啟動參數；本範例不使用外部輸入。</param>
        static void Main(string[] args)
        {
            int failedCases = RunSamples();
            Environment.ExitCode = failedCases == 0 ? 0 : 1;
        }

        /// <summary>
        /// 執行固定測試資料，確認每種解法都回傳題目要求的最長合法連續子陣列長度。
        /// </summary>
        /// <returns>失敗案例數；全部通過時回傳 0。</returns>
        private static int RunSamples()
        {
            var testCases = new[]
            {
                (Name: "Example 1", Nums: new[] { 8, 2, 4, 7 }, Limit: 4, Expected: 2),
                (Name: "Example 2", Nums: new[] { 10, 1, 2, 4, 7, 2 }, Limit: 5, Expected: 4),
                (Name: "Example 3", Nums: new[] { 4, 2, 2, 2, 4, 4, 2, 2 }, Limit: 0, Expected: 3),
                (Name: "Single element", Nums: new[] { 5 }, Limit: 0, Expected: 1),
                (Name: "Duplicate values", Nums: new[] { 2, 2, 2, 2 }, Limit: 0, Expected: 4),
                (Name: "All values valid", Nums: new[] { 1, 3, 2, 4 }, Limit: 3, Expected: 4),
                (Name: "Regression - middle value reconnect", Nums: new[] { 1, 10, 5 }, Limit: 5, Expected: 2),
                (Name: "Empty input", Nums: Array.Empty<int>(), Limit: 0, Expected: 0)
            };

            int failedCases = 0;

            foreach (var testCase in testCases)
            {
                int monotonicQueueActual = LongestSubarray(testCase.Nums, testCase.Limit);
                int sortedSetActual = LongestSubarrayWithSortedSet(testCase.Nums, testCase.Limit);
                int bruteForceActual = LongestSubarrayBruteForce(testCase.Nums, testCase.Limit);
                bool passed = monotonicQueueActual == testCase.Expected
                    && sortedSetActual == testCase.Expected
                    && bruteForceActual == testCase.Expected;

                Console.WriteLine(
                    $"[{testCase.Name}] nums=[{string.Join(", ", testCase.Nums)}], limit={testCase.Limit}; "
                    + $"Expected={testCase.Expected}; LongestSubarray={monotonicQueueActual}; "
                    + $"LongestSubarrayWithSortedSet={sortedSetActual}; "
                    + $"LongestSubarrayBruteForce={bruteForceActual}; {(passed ? "PASS" : "FAIL")}");

                if (!passed)
                {
                    failedCases++;
                }
            }

            Console.WriteLine($"Summary: {testCases.Length - failedCases}/{testCases.Length} cases passed.");
            return failedCases;
        }
        /// <summary>
        /// 使用兩個單調佇列維護滑動視窗中的最大值與最小值，回傳符合最大差值限制的最長連續子陣列長度。
        /// 當視窗超過限制時，移動左界並移除過期索引；每個索引最多進出各佇列一次，因此整體為 O(n)。
        /// </summary>
        /// <remarks>
        /// https://leetcode.cn/problems/longest-continuous-subarray-with-absolute-diff-less-than-or-equal-to-limit/solutions/1767774/by-chusep-knqg/
        /// https://leetcode.cn/problems/longest-continuous-subarray-with-absolute-diff-less-than-or-equal-to-limit/solutions/612773/he-gua-de-shu-ju-jie-gou-hua-dong-chuang-v46j/
        /// https://leetcode.cn/problems/longest-continuous-subarray-with-absolute-diff-less-than-or-equal-to-limit/solutions/230223/longest-continuous-subarray-by-ikaruga/
        /// https://leetcode.cn/problems/longest-continuous-subarray-with-absolute-diff-less-than-or-equal-to-limit/solutions/612688/jue-dui-chai-bu-chao-guo-xian-zhi-de-zui-5bki/
        /// </remarks>
        /// <param name="nums">待檢查的整數陣列；依題目限制至少包含一個元素。</param>
        /// <param name="limit">子陣列中最大值與最小值的差值上限。</param>
        /// <returns>符合限制的最長非空連續子陣列長度；空陣列時回傳 0。</returns>
        public static int LongestSubarray(int[] nums, int limit)
        {
            if (nums.Length == 0)
            {
                return 0;
            }

            var maxDeque = new LinkedList<int>();
            var minDeque = new LinkedList<int>();
            int left = 0;
            int best = 0;

            for (int right = 0; right < nums.Length; right++)
            {
                // 保持最大值佇列遞減、最小值佇列遞增；被新值支配的索引不必再保留。
                while (maxDeque.Last is not null && nums[maxDeque.Last.Value] <= nums[right])
                {
                    maxDeque.RemoveLast();
                }

                maxDeque.AddLast(right);

                while (minDeque.Last is not null && nums[minDeque.Last.Value] >= nums[right])
                {
                    minDeque.RemoveLast();
                }

                minDeque.AddLast(right);

                // 任意兩元素的最大絕對差等於 max - min；超限時只需右移左界恢復合法性。
                while (left <= right
                    && (long)nums[maxDeque.First!.Value] - nums[minDeque.First!.Value] > limit)
                {
                    // 左界離開視窗時，只有位於佇列首端的索引需要同步移除。
                    if (maxDeque.First!.Value == left)
                    {
                        maxDeque.RemoveFirst();
                    }

                    if (minDeque.First!.Value == left)
                    {
                        minDeque.RemoveFirst();
                    }

                    left++;
                }

                best = Math.Max(best, right - left + 1);
            }

            return best;
        }

        /// <summary>
        /// 使用可排序集合維護滑動視窗中的所有值，透過集合首尾取得最小值與最大值，回傳最長合法連續子陣列長度。
        /// 此方法以值與索引組成唯一項目，因此能正確保留重複值；每次加入或移除元素的成本為 O(log n)。
        /// </summary>
        /// <param name="nums">待檢查的整數陣列；依題目限制至少包含一個元素。</param>
        /// <param name="limit">子陣列中最大值與最小值的差值上限。</param>
        /// <returns>符合限制的最長非空連續子陣列長度；空陣列時回傳 0。</returns>
        public static int LongestSubarrayWithSortedSet(int[] nums, int limit)
        {
            if (nums.Length == 0)
            {
                return 0;
            }

            var window = new SortedSet<(int Value, int Index)>();
            int left = 0;
            int best = 0;

            for (int right = 0; right < nums.Length; right++)
            {
                // 索引是值相同時的第二排序鍵，讓 SortedSet 不會合併重複元素。
                window.Add((nums[right], right));

                while (window.Count > 0
                    && (long)window.Max.Value - window.Min.Value > limit)
                {
                    window.Remove((nums[left], left));
                    left++;
                }

                best = Math.Max(best, right - left + 1);
            }

            return best;
        }

        /// <summary>
        /// 枚舉每個可能的左界並逐步延伸右界，直接維護目前範圍的最小值與最大值，作為容易理解的正確性基準解。
        /// 當目前範圍已超過限制時，繼續延伸只會讓範圍更差，因此可提早停止該左界的搜尋；最壞時間複雜度為 O(n²)。
        /// </summary>
        /// <param name="nums">待檢查的整數陣列；依題目限制至少包含一個元素。</param>
        /// <param name="limit">子陣列中最大值與最小值的差值上限。</param>
        /// <returns>符合限制的最長非空連續子陣列長度；空陣列時回傳 0。</returns>
        public static int LongestSubarrayBruteForce(int[] nums, int limit)
        {
            int best = 0;

            for (int left = 0; left < nums.Length; left++)
            {
                int currentMin = nums[left];
                int currentMax = nums[left];

                for (int right = left; right < nums.Length; right++)
                {
                    currentMin = Math.Min(currentMin, nums[right]);
                    currentMax = Math.Max(currentMax, nums[right]);

                    if ((long)currentMax - currentMin > limit)
                    {
                        break;
                    }

                    best = Math.Max(best, right - left + 1);
                }
            }

            return best;
        }
    }
}
