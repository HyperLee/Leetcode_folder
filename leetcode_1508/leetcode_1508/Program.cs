namespace leetcode_1508
{
    internal class Program
    {
        private const int Modulo = 1_000_000_007;

        /// <summary>
        /// <para>
        /// 1508. Range Sum of Sorted Subarray Sums
        /// https://leetcode.com/problems/range-sum-of-sorted-subarray-sums/description/
        ///
        /// You are given the array nums consisting of n positive integers. You computed the sums of all non-empty continuous
        /// subarrays and sorted them in non-decreasing order, creating a new array of n * (n + 1) / 2 numbers.
        ///
        /// Return the sum of the numbers from index left to index right (indexed from 1), inclusive, in the new array. Since
        /// the answer can be huge, return it modulo 10^9 + 7.
        ///
        /// Example 1:
        /// Input: nums = [1,2,3,4], n = 4, left = 1, right = 5
        /// Output: 13
        /// Explanation: All subarray sums are 1, 3, 6, 10, 2, 5, 9, 3, 7, 4. After sorting, the array is
        /// [1, 2, 3, 3, 4, 5, 6, 7, 9, 10]. The sum from index 1 through 5 is 1 + 2 + 3 + 3 + 4 = 13.
        ///
        /// Example 2:
        /// Input: nums = [1,2,3,4], n = 4, left = 3, right = 4
        /// Output: 6
        /// Explanation: Using the same sorted array as Example 1, the sum from index 3 through 4 is 3 + 3 = 6.
        ///
        /// Example 3:
        /// Input: nums = [1,2,3,4], n = 4, left = 1, right = 10
        /// Output: 50
        ///
        /// Constraints:
        /// - n == nums.length
        /// - 1 &lt;= nums.length &lt;= 1000
        /// - 1 &lt;= nums[i] &lt;= 100
        /// - 1 &lt;= left &lt;= right &lt;= n * (n + 1) / 2
        /// </para>
        /// <para>
        /// 1508. 子陣列和排序後的區間和
        /// https://leetcode.cn/problems/range-sum-of-sorted-subarray-sums/description/
        ///
        /// 給定由 n 個正整數組成的陣列 nums。計算所有非空連續子陣列的總和，再將這些總和按非遞減順序
        /// 排序，形成一個含有 n * (n + 1) / 2 個數字的新陣列。
        ///
        /// 回傳新陣列中從索引 left 到索引 right（索引從 1 開始，包含兩端）的數字總和。由於答案可能很大，
        /// 請回傳其對 10^9 + 7 取模後的結果。
        ///
        /// 範例 1：
        /// 輸入：nums = [1,2,3,4]，n = 4，left = 1，right = 5
        /// 輸出：13
        /// 解釋：所有子陣列和為 1、3、6、10、2、5、9、3、7、4。排序後陣列為
        /// [1, 2, 3, 3, 4, 5, 6, 7, 9, 10]。索引 1 到 5 的總和為 1 + 2 + 3 + 3 + 4 = 13。
        ///
        /// 範例 2：
        /// 輸入：nums = [1,2,3,4]，n = 4，left = 3，right = 4
        /// 輸出：6
        /// 解釋：使用與範例 1 相同的排序陣列，索引 3 到 4 的總和為 3 + 3 = 6。
        ///
        /// 範例 3：
        /// 輸入：nums = [1,2,3,4]，n = 4，left = 1，right = 10
        /// 輸出：50
        ///
        /// 限制條件：
        /// - n == nums.length
        /// - 1 &lt;= nums.length &lt;= 1000
        /// - 1 &lt;= nums[i] &lt;= 100
        /// - 1 &lt;= left &lt;= right &lt;= n * (n + 1) / 2
        /// </para>
        /// </summary>
        /// <remarks>
        /// 執行涵蓋官方範例、邊界、重複值與最大限制的確定性驗收案例，並比較兩種解法。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用。</param>
        private static void Main(string[] args)
        {
            int passed = 0;
            int total = 7;

            passed += RunCase("Official example 1", new int[] { 1, 2, 3, 4 }, 1, 5, 13) ? 1 : 0;
            passed += RunCase("Official example 2", new int[] { 1, 2, 3, 4 }, 3, 4, 6) ? 1 : 0;
            passed += RunCase("Official example 3", new int[] { 1, 2, 3, 4 }, 1, 10, 50) ? 1 : 0;
            passed += RunCase("Minimum input", new int[] { 7 }, 1, 1, 7) ? 1 : 0;
            passed += RunCase("Duplicate subarray sums", new int[] { 1, 1, 1 }, 2, 5, 6) ? 1 : 0;
            passed += RunCase("Middle rank range", new int[] { 2, 1, 3 }, 2, 5, 12) ? 1 : 0;
            passed += RunCase(
                "Maximum constraints and modulo",
                Enumerable.Repeat(100, 1000).ToArray(),
                1,
                500500,
                716699888,
                "[100 x 1000]") ? 1 : 0;

            Console.WriteLine($"Summary: {passed}/{total} checks passed.");

            if (passed != total)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 執行一組測試資料，分別驗證列舉排序與二分搜尋解法的答案，並確認兩者都不修改輸入陣列。
        /// </summary>
        /// <param name="name">顯示於主控台的案例名稱。</param>
        /// <param name="nums">符合題目限制的正整數陣列。</param>
        /// <param name="left">排序後子陣列和的 1-based 左界。</param>
        /// <param name="right">排序後子陣列和的 1-based 右界。</param>
        /// <param name="expected">此案例的預期區間總和。</param>
        /// <param name="inputDisplay">大型輸入使用的精簡顯示文字；未提供時列出全部元素。</param>
        /// <returns>兩種解法皆正確且各自輸入保持不變時回傳 <see langword="true"/>。</returns>
        private static bool RunCase(
            string name,
            int[] nums,
            int left,
            int right,
            int expected,
            string? inputDisplay = null)
        {
            int[] original = (int[])nums.Clone();
            int[] baselineInput = (int[])nums.Clone();
            int[] optimizedInput = (int[])nums.Clone();

            int baselineActual = RangeSum(baselineInput, baselineInput.Length, left, right);
            int optimizedActual = RangeSum2(optimizedInput, optimizedInput.Length, left, right);
            bool inputPreserved = baselineInput.SequenceEqual(original)
                && optimizedInput.SequenceEqual(original);
            bool passed = baselineActual == expected
                && optimizedActual == expected
                && inputPreserved;

            Console.WriteLine($"Case: {name}");
            Console.WriteLine($"Input: nums = {inputDisplay ?? FormatNumbers(nums)}");
            Console.WriteLine($"Range: left = {left}, right = {right}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"RangeSum: {baselineActual}");
            Console.WriteLine($"RangeSum2: {optimizedActual}");
            Console.WriteLine($"Input preserved: {inputPreserved}");
            Console.WriteLine($"Result: {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return passed;
        }

        /// <summary>
        /// 將小型整數陣列格式化為易讀的方括號清單，供驗收輸出顯示。
        /// </summary>
        /// <param name="nums">要顯示的整數陣列。</param>
        /// <returns>以逗號分隔的陣列內容。</returns>
        private static string FormatNumbers(int[] nums)
        {
            return $"[{string.Join(", ", nums)}]";
        }

        /// <summary>
        /// 列舉所有非空連續子陣列和，排序後加總指定的 1-based 區間。
        /// 此解法直接對應題意，使用巢狀迴圈累積固定起點的子陣列和，再排序全部結果。
        /// </summary>
        /// <param name="nums">包含 <paramref name="n"/> 個正整數的陣列；方法不會修改其內容。</param>
        /// <param name="n">陣列長度，必須等於 <c>nums.Length</c>。</param>
        /// <param name="left">排序後子陣列和的 1-based 左界。</param>
        /// <param name="right">排序後子陣列和的 1-based 右界。</param>
        /// <returns>指定閉區間的總和對 1,000,000,007 取模後的結果。</returns>
        public static int RangeSum(int[] nums, int n, int left, int right)
        {
            int sumLength = n * (n + 1) / 2;
            int[] sums = new int[sumLength];
            int index = 0;

            for (int i = 0; i < n; i++)
            {
                int sum = 0;

                // 固定左端點並逐步延伸右端點，可在 O(1) 更新下一個連續子陣列和。
                for (int j = i; j < n; j++)
                {
                    sum += nums[j];
                    sums[index++] = sum;
                }
            }

            Array.Sort(sums);

            int answer = 0;

            // 題目區間從 1 起算，陣列索引則從 0 起算。
            for (int i = left - 1; i < right; i++)
            {
                answer = (answer + sums[i]) % Modulo;
            }

            return answer;
        }

        /// <summary>
        /// 以二分搜尋子陣列和門檻，搭配滑動視窗計數與加總，求排序後指定 1-based 區間的總和。
        /// 因輸入皆為正整數，視窗總和超過門檻時可安全移動左界，避免列出所有子陣列和。
        /// </summary>
        /// <param name="nums">包含 <paramref name="n"/> 個正整數的陣列；方法不會修改其內容。</param>
        /// <param name="n">陣列長度，必須等於 <c>nums.Length</c>。</param>
        /// <param name="left">排序後子陣列和的 1-based 左界。</param>
        /// <param name="right">排序後子陣列和的 1-based 右界。</param>
        /// <returns>指定閉區間的總和對 1,000,000,007 取模後的結果。</returns>
        public static int RangeSum2(int[] nums, int n, int left, int right)
        {
            long rangeTotal = SumOfFirstK(nums, n, right) - SumOfFirstK(nums, n, left - 1);

            return (int)(rangeTotal % Modulo);
        }

        /// <summary>
        /// 計算排序後最小的前 <paramref name="k"/> 個子陣列和之總和。
        /// 二分取得最小可涵蓋至少 k 筆的門檻，再扣除門檻值上多算的重複項目。
        /// </summary>
        /// <param name="nums">正整數陣列。</param>
        /// <param name="n">要處理的陣列元素數量。</param>
        /// <param name="k">要加總的最小子陣列和數量；可為 0。</param>
        /// <returns>前 k 小子陣列和的未取模總和。</returns>
        private static long SumOfFirstK(int[] nums, int n, int k)
        {
            if (k == 0)
            {
                return 0;
            }

            long low = 0;
            long high = 0;

            for (int i = 0; i < n; i++)
            {
                high += nums[i];
            }

            while (low < high)
            {
                long middle = low + (high - low) / 2;
                (long count, _) = CountAndSumAtMost(nums, n, middle);

                if (count >= k)
                {
                    high = middle;
                }
                else
                {
                    low = middle + 1;
                }
            }

            (long thresholdCount, long thresholdTotal) = CountAndSumAtMost(nums, n, low);

            // 最小可行門檻上的子陣列和可能重複，需扣除超過 k 的門檻值項目。
            return thresholdTotal - (thresholdCount - k) * low;
        }

        /// <summary>
        /// 以滑動視窗統計總和不大於 <paramref name="limit"/> 的子陣列數量及其總和。
        /// 正整數條件保證右移左界會單調降低視窗總和，因此每個索引最多進出視窗一次。
        /// </summary>
        /// <param name="nums">正整數陣列。</param>
        /// <param name="n">要處理的陣列元素數量。</param>
        /// <param name="limit">允許的子陣列和上限。</param>
        /// <returns>符合門檻的子陣列數量，以及這些子陣列和的總和。</returns>
        private static (long Count, long Total) CountAndSumAtMost(int[] nums, int n, long limit)
        {
            int windowLeft = 0;
            long windowSum = 0;
            long endingSums = 0;
            long count = 0;
            long total = 0;

            for (int windowRight = 0; windowRight < n; windowRight++)
            {
                windowSum += nums[windowRight];
                endingSums += (long)nums[windowRight] * (windowRight - windowLeft + 1);

                while (windowSum > limit)
                {
                    // 移除舊左界，也要扣掉從該位置延伸到目前右界的完整子陣列和。
                    endingSums -= windowSum;
                    windowSum -= nums[windowLeft++];
                }

                count += windowRight - windowLeft + 1;
                total += endingSums;
            }

            return (count, total);
        }
    }
}