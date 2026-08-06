using System;

namespace leetcode_2461
{
    internal class Program
    {
        /// <summary>
        /// 2461. Maximum Sum of Distinct Subarrays With Length K
        /// https://leetcode.com/problems/maximum-sum-of-distinct-subarrays-with-length-k/description/?envType=daily-question&envId=2024-11-19
        /// 
        /// 2461. 长度为 K 子数组中的最大和
        /// https://leetcode.cn/problems/maximum-sum-of-distinct-subarrays-with-length-k/description/
        /// 
        /// 您被給定一個整數陣列 nums 和一個整數 k。請找出所有滿足以下條件的子陣列中，最大的子陣列總和：
        /// 子陣列的長度為 k，且
        /// 子陣列中的所有元素都是互不相同的。
        /// 返回滿足上述條件的所有子陣列中最大的子陣列總和。如果沒有任何子陣列符合條件，則返回 0。
        /// 子陣列是陣列中連續且非空的一段元素序列。
        /// </summary>
        /// <param name="args"></param>
        /// <remarks>
        /// 以固定案例比較兩種解法；所有驗證通過時回傳 0，否則回傳 1。
        /// </remarks>
        static int Main(string[] args)
        {
            (string Name, int[] Nums, int K, long Expected)[] testCases =
            {
                ("官方案例", new[] { 1, 5, 4, 2, 9, 9, 9 }, 3, 15L),
                ("全部重複", new[] { 4, 4, 4 }, 3, 0L),
                ("重複後仍有合法窗口", new[] { 1, 2, 1, 3, 4 }, 3, 8L),
                ("k 等於 1", new[] { 5, 5, 5 }, 1, 5L),
                ("整個陣列皆不重複", new[] { 1, 2, 3, 4 }, 4, 10L),
                ("交錯重複", new[] { 1, 1, 2, 2, 3 }, 2, 5L),
                ("長整數總和", Enumerable.Range(1, 100000).ToArray(), 100000, 5_000_050_000L)
            };

            int totalChecks = 0;
            int passedChecks = 0;

            foreach ((string Name, int[] Nums, int K, long Expected) testCase in testCases)
            {
                bool casePassed = RunCase(testCase.Name, testCase.Nums, testCase.K, testCase.Expected,
                    out long actualByFrequencyWindow,
                    out long actualByLastSeenIndex);

                bool frequencyWindowPassed = actualByFrequencyWindow == testCase.Expected;
                bool lastSeenIndexPassed = actualByLastSeenIndex == testCase.Expected;

                Console.WriteLine($"案例：{testCase.Name}（n = {testCase.Nums.Length}, k = {testCase.K}）");
                Console.WriteLine($"  Expected: {testCase.Expected}");
                Console.WriteLine($"  MaximumSubarraySum Actual: {actualByFrequencyWindow} -> {(frequencyWindowPassed ? "PASS" : "FAIL")}");
                Console.WriteLine($"  MaximumSubarraySum2 Actual: {actualByLastSeenIndex} -> {(lastSeenIndexPassed ? "PASS" : "FAIL")}");
                Console.WriteLine($"  案例結果: {(casePassed ? "PASS" : "FAIL")}");
                Console.WriteLine();

                totalChecks += 2;
                passedChecks += (frequencyWindowPassed ? 1 : 0) + (lastSeenIndexPassed ? 1 : 0);
            }

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
            return passedChecks == totalChecks ? 0 : 1;
        }


        /// <summary>
        /// 使用兩份獨立輸入資料執行兩種解法，回傳各自的計算結果供 Main 比較預期值。
        /// </summary>
        /// <param name="name">測試案例名稱，供除錯與文件辨識使用。</param>
        /// <param name="nums">符合題目限制的整數陣列。</param>
        /// <param name="k">候選子陣列的固定長度。</param>
        /// <param name="expected">此案例預期的最大合法子陣列總和。</param>
        /// <param name="actualByFrequencyWindow">頻率表滑動視窗解法的實際結果。</param>
        /// <param name="actualByLastSeenIndex">最後出現位置解法的實際結果。</param>
        /// <returns>兩種解法都得到預期結果時回傳 true，否則回傳 false。</returns>
        private static bool RunCase(
            string name,
            int[] nums,
            int k,
            long expected,
            out long actualByFrequencyWindow,
            out long actualByLastSeenIndex)
        {
            actualByFrequencyWindow = MaximumSubarraySum((int[])nums.Clone(), k);
            actualByLastSeenIndex = MaximumSubarraySum2((int[])nums.Clone(), k);
            return actualByFrequencyWindow == expected && actualByLastSeenIndex == expected;
        }


        /// <summary>
        /// 使用固定長度滑動視窗與頻率 Dictionary，找出長度為 k 且元素互不相同的子陣列最大總和。
        /// 先維護前 k 個元素，再於窗口右移時同步更新總和與每個值的出現次數；
        /// 當不同元素數量等於 k，即代表目前窗口符合條件。輸入須符合
        /// 1 <= k <= nums.Length，若沒有合法窗口則回傳 0。
        /// </summary>
        /// <remarks>
        /// 此解法的核心是不重新計算每個窗口：移動一次只移除左端元素並加入右端元素，
        /// 因此每個元素只被處理常數次。參考資料：
        /// https://leetcode.cn/problems/maximum-sum-of-distinct-subarrays-with-length-k/solutions/2757534/2461-chang-du-wei-k-zi-shu-zu-zhong-de-z-ge3d/
        /// https://leetcode.cn/problems/maximum-sum-of-distinct-subarrays-with-length-k/solutions/1951940/hua-dong-chuang-kou-by-endlesscheng-m0gm/
        /// </remarks>
        /// <param name="nums">待搜尋的正整數陣列。</param>
        /// <param name="k">候選子陣列的固定長度。</param>
        /// <returns>所有合法長度 k 子陣列中的最大總和；沒有合法子陣列時回傳 0。</returns>
        public static long MaximumSubarraySum(int[] nums, int k)
        {
            long maxSum = 0;
            long windowSum = 0;
            Dictionary<int, int> counts = new Dictionary<int, int>();
            int n = nums.Length;

            // 先建立第一個固定窗口，總和與頻率表會成為後續窗口的更新基礎。
            for (int i = 0; i < k; i++)
            {
                int num = nums[i];
                windowSum += num;

                if (counts.ContainsKey(num))
                {
                    counts[num]++;
                }
                else
                {
                    counts.Add(num, 1);
                }
            }

            // 固定窗口長度是 k；不同元素數量也為 k 時，代表窗口內沒有重複值。
            if (counts.Count == k)
            {
                maxSum = windowSum;
            }

            for (int i = k; i < n; i++)
            {
                int outgoing = nums[i - k];
                int incoming = nums[i];

                // 窗口右移一格只改變兩個元素，因此總和可以 O(1) 更新。
                windowSum = windowSum - outgoing + incoming;

                // 移除左端元素；頻率降為 0 時，才從表中刪除該值。
                counts[outgoing]--;
                if (counts[outgoing] == 0)
                {
                    counts.Remove(outgoing);
                }

                if (counts.ContainsKey(incoming))
                {
                    counts[incoming]++;
                }
                else
                {
                    counts.Add(incoming, 1);
                }

                // 只有 k 個元素都不同時，才用目前窗口總和更新答案。
                if (counts.Count == k)
                {
                    maxSum = Math.Max(maxSum, windowSum);
                }
            }

            return maxSum;
        }


        /// <summary>
        /// 使用每個數字的最後出現位置與前綴和，找出長度為 k 且元素互不相同的子陣列最大總和。
        /// 當重複值仍位於目前窗口時，直接將左界移到該值最後一次出現位置之後；
        /// 當窗口長度符合 k 時，再以前綴和 O(1) 取得窗口總和。輸入須符合
        /// 1 <= k <= nums.Length，若沒有合法窗口則回傳 0。
        /// </summary>
        /// <param name="nums">待搜尋的正整數陣列。</param>
        /// <param name="k">候選子陣列的固定長度。</param>
        /// <returns>所有合法長度 k 子陣列中的最大總和；沒有合法子陣列時回傳 0。</returns>
        public static long MaximumSubarraySum2(int[] nums, int k)
        {
            long[] prefixSums = new long[nums.Length + 1];
            Dictionary<int, int> lastSeenIndex = new Dictionary<int, int>();
            int left = 0;
            long maxSum = 0;

            for (int right = 0; right < nums.Length; right++)
            {
                prefixSums[right + 1] = prefixSums[right] + nums[right];

                // 重複值若仍在窗口內，左界必須跨過它，才能恢復「全部相異」的不變量。
                if (lastSeenIndex.TryGetValue(nums[right], out int previousIndex) && previousIndex >= left)
                {
                    left = previousIndex + 1;
                }

                lastSeenIndex[nums[right]] = right;

                // 沒有重複值時，仍要把窗口限制在固定長度 k。
                if (right - left + 1 > k)
                {
                    left = right - k + 1;
                }

                if (right - left + 1 == k)
                {
                    long currentSum = prefixSums[right + 1] - prefixSums[left];
                    maxSum = Math.Max(maxSum, currentSum);
                }
            }

            return maxSum;
        }
    }
}