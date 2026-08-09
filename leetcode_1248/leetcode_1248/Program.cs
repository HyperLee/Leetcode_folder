namespace leetcode_1248
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 1248. Count Number of Nice Subarrays
        /// https://leetcode.com/problems/count-number-of-nice-subarrays/description/
        ///
        /// Given an array of integers nums and an integer k. A continuous subarray is called nice if there are
        /// k odd numbers in it.
        /// Return the number of nice subarrays.
        ///
        /// Example 1:
        /// Input: nums = [1,1,2,1,1], k = 3
        /// Output: 2
        /// Explanation: The only subarrays with 3 odd numbers are [1,1,2,1] and [1,2,1,1].
        ///
        /// Example 2:
        /// Input: nums = [2,4,6], k = 1
        /// Output: 0
        /// Explanation: There are no odd numbers in the array.
        ///
        /// Example 3:
        /// Input: nums = [2,2,2,1,2,2,1,2,2,2], k = 2
        /// Output: 16
        ///
        /// Constraints:
        /// 1 &lt;= nums.length &lt;= 50000
        /// 1 &lt;= nums[i] &lt;= 10^5
        /// 1 &lt;= k &lt;= nums.length
        /// </para>
        /// <para>
        /// 1248. 統計「優美子陣列」
        /// https://leetcode.cn/problems/count-number-of-nice-subarrays/description/
        ///
        /// 給定整數陣列 nums 與整數 k。若一個連續子陣列中恰好有 k 個奇數，則稱它為優美子陣列。
        /// 請回傳優美子陣列的數量。
        ///
        /// 範例 1：
        /// 輸入：nums = [1,1,2,1,1], k = 3
        /// 輸出：2
        /// 解釋：僅有 [1,1,2,1] 與 [1,2,1,1] 這兩個子陣列含有 3 個奇數。
        ///
        /// 範例 2：
        /// 輸入：nums = [2,4,6], k = 1
        /// 輸出：0
        /// 解釋：陣列中沒有奇數。
        ///
        /// 範例 3：
        /// 輸入：nums = [2,2,2,1,2,2,1,2,2,2], k = 2
        /// 輸出：16
        ///
        /// 限制條件：
        /// 1 &lt;= nums.length &lt;= 50000
        /// 1 &lt;= nums[i] &lt;= 10^5
        /// 1 &lt;= k &lt;= nums.length
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            (int[] Nums, int K, int Expected)[] cases =
            [
                ([1, 1, 2, 1, 1], 3, 2),
                ([2, 4, 6], 1, 0),
                ([2, 2, 2, 1, 2, 2, 1, 2, 2, 2], 2, 16),
                ([1], 1, 1),
                ([2, 1, 2], 2, 0),
                ([2, 2, 1, 2, 2], 1, 9),
                ([1, 3, 5, 7], 2, 3)
            ];

            int passedChecks = 0;
            int totalChecks = cases.Length * 2;

            for (int i = 0; i < cases.Length; i++)
            {
                (int[] nums, int k, int expected) = cases[i];
                int slidingWindowActual = NumberOfSubarrays([.. nums], k);
                int oddIndicesActual = NumberOfSubarrays2([.. nums], k);
                bool slidingWindowPassed = slidingWindowActual == expected;
                bool oddIndicesPassed = oddIndicesActual == expected;

                if (slidingWindowPassed)
                {
                    passedChecks++;
                }

                if (oddIndicesPassed)
                {
                    passedChecks++;
                }

                Console.WriteLine($"Case {i + 1}: nums = [{string.Join(", ", nums)}], k = {k}");
                Console.WriteLine($"Expected: {expected}");
                Console.WriteLine($"NumberOfSubarrays Actual: {slidingWindowActual} => {(slidingWindowPassed ? "PASS" : "FAIL")}");
                Console.WriteLine($"NumberOfSubarrays2 Actual: {oddIndicesActual} => {(oddIndicesPassed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
            Environment.ExitCode = passedChecks == totalChecks ? 0 : 1;
        }


        /// <summary>
        /// 使用滑動視窗計算恰好包含 <paramref name="k"/> 個奇數的連續子陣列數量。
        /// 當視窗包含指定數量的奇數時，分別計算第一個奇數左側與最後一個奇數右側可選的偶數邊界，
        /// 將兩側選擇數相乘並累加。輸入需符合題目限制：陣列長度為 1 到 50000、元素為 1 到 100000，
        /// 且 <paramref name="k"/> 介於 1 與陣列長度之間；回傳所有符合條件的非空連續子陣列數量。
        /// </summary>
        /// <param name="nums">要檢查的正整數陣列；此方法不會修改陣列內容。</param>
        /// <param name="k">每個目標子陣列必須包含的奇數個數。</param>
        /// <returns>恰好包含 <paramref name="k"/> 個奇數的連續子陣列數量。</returns>
        public static int NumberOfSubarrays(int[] nums, int k)
        {
            int left = 0;
            int right = 0;
            int oddCount = 0;
            int result = 0;

            while (right < nums.Length)
            {
                // 擴張右邊界，直到視窗內累積到第 k 個奇數。
                if ((nums[right++] & 1) == 1)
                {
                    oddCount++;
                }

                if (oddCount == k)
                {
                    // 第 k 個奇數右側連續偶數的數量，決定合法結尾的選擇數。
                    int nextOddIndex = right;
                    while (right < nums.Length && (nums[right] & 1) == 0)
                    {
                        right++;
                    }
                    int rightEvenCount = right - nextOddIndex;

                    // 第一個奇數左側連續偶數的數量，決定合法起點的選擇數。
                    int leftEvenCount = 0;
                    while ((nums[left] & 1) == 0)
                    {
                        leftEvenCount++;
                        left++;
                    }

                    // 左右都可以不取偶數，因此各多一種選擇；兩側組合數即為本輪答案。
                    result += (leftEvenCount + 1) * (rightEvenCount + 1);

                    // 移除視窗中的第一個奇數，讓下一輪尋找下一組 k 個奇數。
                    left++;
                    oddCount--;
                }
            }

            return result;
        }


        /// <summary>
        /// 使用奇數索引與頭尾哨兵計算恰好包含 <paramref name="k"/> 個奇數的連續子陣列數量。
        /// 對每一組連續的 <paramref name="k"/> 個奇數，將第一個奇數與前一個奇數的索引差，
        /// 乘上最後一個奇數與下一個奇數的索引差。輸入需符合題目限制：陣列長度為 1 到 50000、
        /// 元素為 1 到 100000，且 <paramref name="k"/> 介於 1 與陣列長度之間；回傳所有合法邊界組合數。
        /// </summary>
        /// <param name="nums">要檢查的正整數陣列；此方法不會修改陣列內容。</param>
        /// <param name="k">每個目標子陣列必須包含的奇數個數。</param>
        /// <returns>恰好包含 <paramref name="k"/> 個奇數的連續子陣列數量。</returns>
        public static int NumberOfSubarrays2(int[] nums, int k)
        {
            int length = nums.Length;
            int[] oddIndices = new int[length + 2];
            int oddCount = 0;
            int result = 0;

            for (int i = 0; i < length; i++)
            {
                if ((nums[i] & 1) != 0)
                {
                    oddIndices[++oddCount] = i;
                }
            }

            // 頭尾哨兵代表陣列外側邊界，統一首尾兩組奇數的距離計算。
            oddIndices[0] = -1;
            oddIndices[oddCount + 1] = length;

            for (int firstOdd = 1; firstOdd + k <= oddCount + 1; firstOdd++)
            {
                // 左側索引差是起點選擇數，右側索引差是終點選擇數。
                int leftChoices = oddIndices[firstOdd] - oddIndices[firstOdd - 1];
                int rightChoices = oddIndices[firstOdd + k] - oddIndices[firstOdd + k - 1];
                result += leftChoices * rightChoices;
            }

            return result;
        }
    }
}
