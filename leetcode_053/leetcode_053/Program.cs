namespace leetcode_053
{
    internal class Program
    {
        /// <summary>
        /// 53. Maximum Subarray
        /// https://leetcode.com/problems/maximum-subarray/
        /// 53. 最大子数组和
        /// https://leetcode.cn/problems/maximum-subarray/?envType=daily-question&envId=Invalid%20Date
        /// </summary>
        /// <remarks>
        /// 使用固定測試案例執行暴力枚舉、Kadane 與分治法，並比對各解法的實際結果。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用此參數。</param>
        static void Main(string[] args)
        {
            var solutions = new (string Name, Func<int[], int> Solve)[]
            {
                ("Brute Force", MaxSubArrayBruteForce),
                ("Kadane", MaxSubArray),
                ("Divide and Conquer", MaxSubArrayDivideAndConquer)
            };

            var testCases = new (int[] Nums, int Expected)[]
            {
                (new[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 }, 6),
                (new[] { 1 }, 1),
                (new[] { 5, 4, -1, 7, 8 }, 23),
                (new[] { -8, -3, -6, -2, -5, -4 }, -2),
                (new[] { 0, 0, -1, 0 }, 0)
            };

            int passedCount = 0;
            int totalCount = 0;

            for (int index = 0; index < testCases.Length; index++)
            {
                RunTestCase(
                    index + 1,
                    testCases[index].Nums,
                    testCases[index].Expected,
                    solutions,
                    ref passedCount,
                    ref totalCount);
            }

            Console.WriteLine($"Summary: {passedCount}/{totalCount} passed.");
        }

        /// <summary>
        /// 執行單一測試案例，依序呼叫所有最大子陣列解法，將實際結果與預期值比較並輸出 PASS 或 FAIL。
        /// 輸入陣列必須符合題目條件且至少包含一個元素；方法會累計通過數與總驗證數，不回傳結果。
        /// </summary>
        /// <param name="caseNumber">顯示用的測試案例編號。</param>
        /// <param name="nums">要交給各解法處理的非空整數陣列。</param>
        /// <param name="expected">此測試案例預期得到的最大連續子陣列總和。</param>
        /// <param name="solutions">要執行的解法名稱及對應函式。</param>
        /// <param name="passedCount">累計通過的解法驗證數量。</param>
        /// <param name="totalCount">累計執行的解法驗證數量。</param>
        private static void RunTestCase(
            int caseNumber,
            int[] nums,
            int expected,
            (string Name, Func<int[], int> Solve)[] solutions,
            ref int passedCount,
            ref int totalCount)
        {
            Console.WriteLine($"Case {caseNumber}");
            Console.WriteLine($"Input: [{string.Join(", ", nums)}]");
            Console.WriteLine($"Expected: {expected}");

            foreach ((string name, Func<int[], int> solve) in solutions)
            {
                int actual = solve(nums);
                bool passed = actual == expected;

                totalCount++;
                if (passed)
                {
                    passedCount++;
                }

                Console.WriteLine($"{name}: {actual} - {(passed ? "PASS" : "FAIL")}");
            }

            Console.WriteLine();
        }

        /// <summary>
        /// 使用暴力枚舉找出最大連續子陣列總和。
        /// 從每個索引作為起點，向右累加所有可能的連續區間並記錄最大值。
        /// 輸入必須是至少包含一個元素的整數陣列；輸出為所有連續子陣列中的最大總和。
        /// </summary>
        /// <param name="nums">符合題目限制的非空整數陣列。</param>
        /// <returns>可從 <paramref name="nums"/> 取得的最大連續子陣列總和。</returns>
        public static int MaxSubArrayBruteForce(int[] nums)
        {
            int maximumSum = nums[0];

            for (int start = 0; start < nums.Length; start++)
            {
                int currentSum = 0;

                // 固定左端點後向右累加，確保每一個連續子陣列都被檢查一次。
                for (int end = start; end < nums.Length; end++)
                {
                    currentSum += nums[end];
                    maximumSum = Math.Max(maximumSum, currentSum);
                }
            }

            return maximumSum;
        }

        /// <summary>
        /// 使用 Kadane 動態規劃找出最大連續子陣列總和。
        /// 每個位置比較「延續前一段」與「從目前元素重新開始」，並持續保存全域最大值。
        /// 輸入必須是至少包含一個元素的整數陣列；輸出為所有連續子陣列中的最大總和。
        /// </summary>
        /// <param name="nums">符合題目限制的非空整數陣列。</param>
        /// <returns>可從 <paramref name="nums"/> 取得的最大連續子陣列總和。</returns>
        public static int MaxSubArray(int[] nums)
        {
            int currentSum = 0;
            int maximumSum = nums[0];

            foreach (int num in nums)
            {
                // 若前段累積已成為負擔，就從目前元素重新建立連續子陣列。
                currentSum = Math.Max(currentSum + num, num);
                maximumSum = Math.Max(maximumSum, currentSum);
            }

            return maximumSum;
        }

        /// <summary>
        /// 使用分治法找出最大連續子陣列總和。
        /// 將陣列遞迴切成左右兩半，比較左半、右半與跨越中點的最大總和。
        /// 輸入必須是至少包含一個元素的整數陣列；輸出為所有連續子陣列中的最大總和。
        /// </summary>
        /// <param name="nums">符合題目限制的非空整數陣列。</param>
        /// <returns>可從 <paramref name="nums"/> 取得的最大連續子陣列總和。</returns>
        public static int MaxSubArrayDivideAndConquer(int[] nums)
        {
            return FindMaximumSubArray(nums, 0, nums.Length - 1);
        }

        /// <summary>
        /// 計算指定閉區間內的最大連續子陣列總和。
        /// 遞迴求出左右子區間與跨越中點的候選答案，再回傳三者最大值。
        /// 輸入索引必須落在非空陣列範圍內且 <paramref name="left"/> 不大於 <paramref name="right"/>。
        /// </summary>
        /// <param name="nums">要分析的非空整數陣列。</param>
        /// <param name="left">目前區間的左端索引，包含此位置。</param>
        /// <param name="right">目前區間的右端索引，包含此位置。</param>
        /// <returns>指定閉區間內的最大連續子陣列總和。</returns>
        private static int FindMaximumSubArray(int[] nums, int left, int right)
        {
            if (left == right)
            {
                return nums[left];
            }

            int middle = left + (right - left) / 2;
            int leftMaximum = FindMaximumSubArray(nums, left, middle);
            int rightMaximum = FindMaximumSubArray(nums, middle + 1, right);
            int crossingMaximum = FindMaximumCrossingSubArray(nums, left, middle, right);

            // 最大子陣列必定完整位於左半、右半，或同時跨越左右兩半。
            return Math.Max(Math.Max(leftMaximum, rightMaximum), crossingMaximum);
        }

        /// <summary>
        /// 計算跨越指定中點的最大連續子陣列總和。
        /// 分別尋找左半部以中點結尾的最大後綴，以及右半部由中點右側開始的最大前綴。
        /// 輸入索引必須形成有效閉區間；輸出為必定跨越中點的最大連續總和。
        /// </summary>
        /// <param name="nums">要分析的非空整數陣列。</param>
        /// <param name="left">目前區間的左端索引，包含此位置。</param>
        /// <param name="middle">左右子區間的分界索引，屬於左半部。</param>
        /// <param name="right">目前區間的右端索引，包含此位置。</param>
        /// <returns>跨越 <paramref name="middle"/> 與其右側位置的最大連續子陣列總和。</returns>
        private static int FindMaximumCrossingSubArray(int[] nums, int left, int middle, int right)
        {
            int leftMaximum = int.MinValue;
            int currentSum = 0;

            for (int index = middle; index >= left; index--)
            {
                currentSum += nums[index];
                leftMaximum = Math.Max(leftMaximum, currentSum);
            }

            int rightMaximum = int.MinValue;
            currentSum = 0;

            for (int index = middle + 1; index <= right; index++)
            {
                currentSum += nums[index];
                rightMaximum = Math.Max(rightMaximum, currentSum);
            }

            return leftMaximum + rightMaximum;
        }
    }
}