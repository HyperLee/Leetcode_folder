using System.Numerics;

namespace leetcode_2917
{
    internal class Program
    {
        /// <summary>
        /// 2917. Find the K-or of an Array
        /// https://leetcode.com/problems/find-the-k-or-of-an-array/description/
        /// <para>
        /// You are given an integer array nums and an integer k. The K-or operation extends the standard bitwise OR: a bit position in the result is set to 1 if at least k numbers in nums have a 1 in that position.
        ///
        /// Return the K-or of nums.
        ///
        /// Example 1:
        /// Input: nums = [7,12,9,8,9,15], k = 4
        /// Output: 9
        /// Explanation: In binary, the rows are 7 = 0111, 12 = 1100, 9 = 1001, 8 = 1000, 9 = 1001, and 15 = 1111. Bit 0 equals 1 in 7, 9, 9, and 15. Bit 3 equals 1 in 12, 9, 8, 9, and 15. Only bits 0 and 3 qualify, so the result is (1001)2 = 9.
        ///
        /// Example 2:
        /// Input: nums = [2,12,1,11,4,5], k = 6
        /// Output: 0
        /// Explanation: No bit appears as 1 in all six array numbers, as required when k = 6. Thus, the result is 0.
        ///
        /// Example 3:
        /// Input: nums = [10,8,5,9,11,6,8], k = 1
        /// Output: 15
        /// Explanation: Since k == 1, the 1-or is the bitwise OR of every element. Thus, 10 OR 8 OR 5 OR 9 OR 11 OR 6 OR 8 = 15.
        ///
        /// Constraints:
        /// - 1 &lt;= nums.length &lt;= 50
        /// - 0 &lt;= nums[i] &lt; 2^31
        /// - 1 &lt;= k &lt;= nums.length
        /// </para>
        /// <para>
        /// 2917. 找出陣列的 K-or 值
        /// https://leetcode.cn/problems/find-the-k-or-of-an-array/description/
        ///
        /// 給定一個整數陣列 nums 和整數 k。K-or 操作是標準位元 OR 的延伸：若 nums 中至少有 k 個數字在某個位元位置為 1，結果的該位元就設為 1。
        ///
        /// 回傳 nums 的 K-or。
        ///
        /// 範例 1：
        /// 輸入：nums = [7,12,9,8,9,15], k = 4
        /// 輸出：9
        /// 解釋：以二進位表示，各數依序為 7 = 0111、12 = 1100、9 = 1001、8 = 1000、9 = 1001、15 = 1111。位元 0 在 7、9、9、15 中為 1；位元 3 在 12、9、8、9、15 中為 1。只有位元 0 與 3 符合，因此結果為 (1001)2 = 9。
        ///
        /// 範例 2：
        /// 輸入：nums = [2,12,1,11,4,5], k = 6
        /// 輸出：0
        /// 解釋：當 k = 6 時，沒有任何位元在全部六個陣列數字中都為 1，因此結果為 0。
        ///
        /// 範例 3：
        /// 輸入：nums = [10,8,5,9,11,6,8], k = 1
        /// 輸出：15
        /// 解釋：由於 k == 1，1-or 等同所有元素的位元 OR。因此 10 OR 8 OR 5 OR 9 OR 11 OR 6 OR 8 = 15。
        ///
        /// 限制條件：
        /// - 1 &lt;= nums.length &lt;= 50
        /// - 0 &lt;= nums[i] &lt; 2^31
        /// - 1 &lt;= k &lt;= nums.length
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            (string Name, int[] Nums, int K, int Expected)[] testCases =
            {
                ("官方範例一：四個數字支援 bit 0 與 bit 3", new[] { 7, 12, 9, 8, 9, 15 }, 4, 9),
                ("官方範例二：沒有 bit 出現在全部數字中", new[] { 2, 12, 1, 11, 4, 5 }, 6, 0),
                ("官方範例三：k 為 1 等同一般 OR", new[] { 10, 8, 5, 9, 11, 6, 8 }, 1, 15),
                ("單一零值", new[] { 0 }, 1, 0),
                ("重複值剛好達到門檻", new[] { 5, 5, 2 }, 2, 5),
                ("不同數字共同支援三個 bit", new[] { 3, 5, 6 }, 2, 7),
                ("第 30 位邊界", new[] { 1 << 30, 1 << 30, 0 }, 2, 1 << 30)
            };

            int passedChecks = 0;
            int totalChecks = testCases.Length * 3;

            foreach ((string name, int[] nums, int k, int expected) in testCases)
            {
                Console.WriteLine($"Case: {name}");
                Console.WriteLine($"Input: nums = [{string.Join(", ", nums)}], k = {k}");

                passedChecks += RunCase(nameof(FindKOr), FindKOr, nums, k, expected) ? 1 : 0;
                passedChecks += RunCase(nameof(FindKOr2), FindKOr2, nums, k, expected) ? 1 : 0;
                passedChecks += RunCase(nameof(FindKOr3), FindKOr3, nums, k, expected) ? 1 : 0;

                Console.WriteLine();
            }

            Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");

            if (passedChecks != totalChecks)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 執行指定的 K-or 解法並比對預期值，輸入需符合題目限制，輸出該次檢查結果並回傳是否通過。
        /// </summary>
        /// <param name="solutionName">顯示於測試輸出的解法名稱。</param>
        /// <param name="solution">接受整數陣列與門檻值，並回傳 K-or 的解法。</param>
        /// <param name="nums">長度為 1 到 50、元素介於 0 到 2^31 - 1 的整數陣列。</param>
        /// <param name="k">bit 必須出現的最少次數，介於 1 到陣列長度之間。</param>
        /// <param name="expected">此案例預期得到的 K-or。</param>
        /// <returns>實際結果等於預期值時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
        private static bool RunCase(
            string solutionName,
            Func<int[], int, int> solution,
            int[] nums,
            int k,
            int expected)
        {
            int actual = solution(nums, k);
            bool passed = actual == expected;

            Console.WriteLine(
                $"  {solutionName}: Expected = {expected}, Actual = {actual}, Result = {(passed ? "PASS" : "FAIL")}");

            return passed;
        }


        /// <summary>
        /// 以 bit 為外層逐一檢查第 0 到 30 位，統計該位在多少個數字中為 1；
        /// 輸入需符合題目限制，當出現次數至少為 <paramref name="k"/> 時將該 bit 寫入結果，最後回傳 K-or。
        /// </summary>
        /// <param name="nums">長度為 1 到 50、元素介於 0 到 2^31 - 1 的整數陣列；方法不會修改此陣列。</param>
        /// <param name="k">bit 必須出現的最少次數，介於 1 到陣列長度之間。</param>
        /// <returns>所有出現次數至少為 <paramref name="k"/> 的 bit 所組成的 K-or。</returns>
        public static int FindKOr(int[] nums, int k)
        {
            int result = 0;

            // nums[i] 小於 2^31，因此只需檢查 int 的第 0 到 30 位。
            for (int bit = 0; bit < 31; bit++)
            {
                int occurrenceCount = 0;

                foreach (int number in nums)
                {
                    occurrenceCount += (number >> bit) & 1;
                }

                if (occurrenceCount >= k)
                {
                    result |= 1 << bit;
                }
            }

            return result;
        }

        /// <summary>
        /// 以數字為外層走訪輸入，將每個數字的 31 個 bit 累計到次數陣列；
        /// 輸入需符合題目限制，最後依 <paramref name="k"/> 重建並回傳 K-or。
        /// </summary>
        /// <param name="nums">長度為 1 到 50、元素介於 0 到 2^31 - 1 的整數陣列；方法不會修改此陣列。</param>
        /// <param name="k">bit 必須出現的最少次數，介於 1 到陣列長度之間。</param>
        /// <returns>所有出現次數至少為 <paramref name="k"/> 的 bit 所組成的 K-or。</returns>
        public static int FindKOr2(int[] nums, int k)
        {
            int[] bitCounts = new int[31];

            foreach (int number in nums)
            {
                for (int bit = 0; bit < bitCounts.Length; bit++)
                {
                    if ((number & (1 << bit)) != 0)
                    {
                        bitCounts[bit]++;
                    }
                }
            }

            return BuildKOr(bitCounts, k);
        }

        /// <summary>
        /// 逐一清除每個數字目前最低的已設定 bit，只統計實際為 1 的位置；
        /// 輸入需符合題目限制，最後依 <paramref name="k"/> 重建並回傳 K-or。
        /// </summary>
        /// <param name="nums">長度為 1 到 50、元素介於 0 到 2^31 - 1 的整數陣列；方法不會修改此陣列。</param>
        /// <param name="k">bit 必須出現的最少次數，介於 1 到陣列長度之間。</param>
        /// <returns>所有出現次數至少為 <paramref name="k"/> 的 bit 所組成的 K-or。</returns>
        public static int FindKOr3(int[] nums, int k)
        {
            int[] bitCounts = new int[31];

            foreach (int number in nums)
            {
                uint remainingBits = (uint)number;

                while (remainingBits != 0)
                {
                    int bit = BitOperations.TrailingZeroCount(remainingBits);
                    bitCounts[bit]++;

                    // x & (x - 1) 會清除 x 最低的 1，避免掃描原本為 0 的位置。
                    remainingBits &= remainingBits - 1;
                }
            }

            return BuildKOr(bitCounts, k);
        }

        /// <summary>
        /// 依各 bit 的出現次數與門檻值重建 K-or；次數陣列需涵蓋第 0 到 30 位，並回傳對應整數結果。
        /// </summary>
        /// <param name="bitCounts">索引代表 bit 位置、值代表該 bit 出現次數的 31 格陣列。</param>
        /// <param name="k">bit 必須出現的最少次數。</param>
        /// <returns>所有出現次數至少為 <paramref name="k"/> 的 bit 所組成的 K-or。</returns>
        private static int BuildKOr(int[] bitCounts, int k)
        {
            int result = 0;

            for (int bit = 0; bit < bitCounts.Length; bit++)
            {
                if (bitCounts[bit] >= k)
                {
                    result |= 1 << bit;
                }
            }

            return result;
        }
    }
}