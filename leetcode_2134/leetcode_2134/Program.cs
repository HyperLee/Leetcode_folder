namespace leetcode_2134
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 2134. Minimum Swaps to Group All 1's Together II
        /// https://leetcode.com/problems/minimum-swaps-to-group-all-1s-together-ii/description/
        ///
        /// A swap exchanges values at two distinct positions. In a circular array, the first and last elements are adjacent. Given a binary circular array nums, return the minimum swaps needed to group all 1's together at any location.
        ///
        /// Example 1:
        /// Input: nums = [0,1,0,1,1,0,0]
        /// Output: 1
        /// Explanation: Several arrangements use 1 swap; a circular arrangement may use 2. No arrangement uses 0, so the minimum is 1.
        ///
        /// Example 2:
        /// Input: nums = [0,1,1,1,0,0,1,1,0]
        /// Output: 2
        /// Explanation: All 1's can be grouped using 2 swaps. It cannot be done with 0 or 1, so the minimum is 2.
        ///
        /// Example 3:
        /// Input: nums = [1,1,0,0,1]
        /// Output: 0
        /// Explanation: The 1's are already grouped because the array is circular, so 0 swaps are needed.
        ///
        /// Constraints:
        /// - 1 &lt;= nums.length &lt;= 10^5
        /// - nums[i] is 0 or 1.
        /// </para>
        /// <para>
        /// 2134. 最少交換次數來組合所有的 1 II
        /// https://leetcode.cn/problems/minimum-swaps-to-group-all-1s-together-ii/description/
        ///
        /// 一次交換是互換兩個不同位置的值。環形陣列的第一個與最後一個元素相鄰。給定二元環形陣列 nums，回傳在任意位置將所有 1 聚集在一起所需的最少交換次數。
        ///
        /// 範例 1：
        /// 輸入：nums = [0,1,0,1,1,0,0]
        /// 輸出：1
        /// 說明：有數種排列使用 1 次交換，某個環形排列會使用 2 次；不存在使用 0 次的方法，因此最少為 1。
        ///
        /// 範例 2：
        /// 輸入：nums = [0,1,1,1,0,0,1,1,0]
        /// 輸出：2
        /// 說明：可使用 2 次交換聚集所有 1；無法使用 0 或 1 次完成，因此最少為 2。
        ///
        /// 範例 3：
        /// 輸入：nums = [1,1,0,0,1]
        /// 輸出：0
        /// 說明：由於陣列是環形，所有 1 已聚集在一起，因此需要 0 次交換。
        ///
        /// 限制條件：
        /// - 1 &lt;= nums.length &lt;= 10^5
        /// - nums[i] 為 0 或 1。
        /// </para>
        /// </summary>
        /// <remarks>
        /// 主要進入點會執行六組固定案例，比較取模環狀視窗與雙倍陣列視窗兩種解法，
        /// 並以 Expected、Actual 與 PASS/FAIL 顯示驗證結果。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用。</param>
        private static void Main(string[] args)
        {
            bool allPassed = RunSamples();
            Environment.ExitCode = allPassed ? 0 : 1;
        }

        /// <summary>
        /// 執行六組符合題目限制的固定案例，分別驗證取模環狀視窗與雙倍陣列視窗解法。
        /// 每組案例都以人工推導的預期值檢查兩個公開方法的輸出。
        /// </summary>
        /// <returns>十二項答案檢查全部通過時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
        private static bool RunSamples()
        {
            (string Name, int[] Numbers, int Expected)[] cases =
            {
                ("1. 官方範例一", new[] { 0, 1, 0, 1, 1, 0, 0 }, 1),
                ("2. 官方範例二", new[] { 0, 1, 1, 1, 0, 0, 1, 1, 0 }, 2),
                ("3. 跨邊界已成群", new[] { 1, 1, 0, 0, 1 }, 0),
                ("4. 全部為零", new[] { 0, 0, 0, 0 }, 0),
                ("5. 全部為一", new[] { 1, 1, 1, 1 }, 0),
                ("6. 最小長度", new[] { 1 }, 0)
            };

            int passedChecks = 0;
            const int checksPerCase = 2;
            int totalChecks = cases.Length * checksPerCase;

            foreach ((string name, int[] numbers, int expected) in cases)
            {
                passedChecks += RunCase(name, numbers, expected);
            }

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項測試通過");
            return passedChecks == totalChecks;
        }

        /// <summary>
        /// 將單一合法二元環狀陣列交給兩種解法，並顯示輸入、預期答案、實際答案與驗證結果。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="numbers">非空且只包含 0 與 1 的環狀陣列。</param>
        /// <param name="expected">人工推導的預期最少交換次數。</param>
        /// <returns>本案例通過的解法數量，範圍為零到二。</returns>
        private static int RunCase(string name, int[] numbers, int expected)
        {
            Console.WriteLine($"案例：{name}");
            Console.WriteLine($"Input：nums = {FormatArray(numbers)}");

            int moduloActual = MinSwaps(numbers);
            int doubledActual = MinSwaps2(numbers);
            bool moduloPassed = moduloActual == expected;
            bool doubledPassed = doubledActual == expected;

            Console.WriteLine("解法一：MinSwaps（取模環狀視窗）");
            Console.WriteLine($"Expected：{expected}");
            Console.WriteLine($"Actual：{moduloActual}");
            Console.WriteLine($"Result：{(moduloPassed ? "PASS" : "FAIL")}");
            Console.WriteLine("解法二：MinSwaps2（雙倍陣列視窗）");
            Console.WriteLine($"Expected：{expected}");
            Console.WriteLine($"Actual：{doubledActual}");
            Console.WriteLine($"Result：{(doubledPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return (moduloPassed ? 1 : 0) + (doubledPassed ? 1 : 0);
        }

        /// <summary>
        /// 將整數陣列格式化為穩定的方括號字串，供測試輸出與 README 範例使用。
        /// </summary>
        /// <param name="values">要格式化的整數陣列。</param>
        /// <returns>格式為 <c>[value1, value2, ...]</c> 的字串。</returns>
        private static string FormatArray(int[] values)
        {
            return $"[{string.Join(", ", values)}]";
        }

        /// <summary>
        /// 以取模索引在合法的二元環狀陣列上滑動固定長度視窗，尋找視窗內最少的 0。
        /// 視窗長度等於輸入中 1 的總數；視窗內每個 0 都必須與視窗外的 1 交換。
        /// 輸入必須為非空且只包含 0 與 1 的陣列，方法不修改輸入，回傳把所有 1 相鄰排列所需的
        /// 最少交換次數；時間複雜度為 O(n)，額外空間複雜度為 O(1)。
        /// </summary>
        /// <remarks>
        /// 參考：
        /// https://leetcode.cn/problems/minimum-swaps-to-group-all-1s-together-ii/solutions/1202043/zui-shao-jiao-huan-ci-shu-lai-zu-he-suo-iaghf/
        /// https://leetcode.cn/problems/minimum-swaps-to-group-all-1s-together-ii/solutions/2591173/2134-zui-shao-jiao-huan-ci-shu-lai-zu-he-u617/
        /// </remarks>
        /// <param name="nums">非空且只包含 0 與 1 的環狀陣列。</param>
        /// <returns>將所有 1 在環狀陣列中組成相鄰區段所需的最少交換次數。</returns>
        public static int MinSwaps(int[] nums)
        {
            int length = nums.Length;
            int oneCount = 0;

            for (int i = 0; i < length; i++)
            {
                oneCount += nums[i];
            }

            if (oneCount == 0)
            {
                return 0;
            }

            int zeroCount = 0;
            for (int i = 0; i < oneCount; i++)
            {
                zeroCount += 1 - nums[i];
            }

            int minimumSwaps = zeroCount;
            for (int start = 1; start < length; start++)
            {
                // 滑出前一個元素，並用取模索引滑入可能跨越陣列尾端的新元素。
                if (nums[start - 1] == 0)
                {
                    zeroCount--;
                }

                if (nums[(start + oneCount - 1) % length] == 0)
                {
                    zeroCount++;
                }

                minimumSwaps = Math.Min(minimumSwaps, zeroCount);
            }

            return minimumSwaps;
        }

        /// <summary>
        /// 將合法的二元環狀陣列複製成長度兩倍的線性陣列，再用固定長度滑動視窗尋找最少的 0。
        /// 視窗長度等於輸入中 1 的總數；視窗內每個 0 都必須與視窗外的 1 交換。
        /// 輸入必須為非空且只包含 0 與 1 的陣列，方法不修改輸入，回傳把所有 1 相鄰排列所需的
        /// 最少交換次數；時間複雜度為 O(n)，額外空間複雜度為 O(n)。
        /// </summary>
        /// <param name="nums">非空且只包含 0 與 1 的環狀陣列。</param>
        /// <returns>將所有 1 在環狀陣列中組成相鄰區段所需的最少交換次數。</returns>
        public static int MinSwaps2(int[] nums)
        {
            int length = nums.Length;
            int oneCount = 0;

            foreach (int number in nums)
            {
                oneCount += number;
            }

            if (oneCount == 0)
            {
                return 0;
            }

            int[] doubledNumbers = new int[length * 2];
            for (int i = 0; i < doubledNumbers.Length; i++)
            {
                doubledNumbers[i] = nums[i % length];
            }

            int zeroCount = 0;
            for (int i = 0; i < oneCount; i++)
            {
                zeroCount += 1 - doubledNumbers[i];
            }

            int minimumSwaps = zeroCount;
            for (int start = 1; start < length; start++)
            {
                // 雙倍陣列把跨越尾端的區段接成連續資料，因此滑動時不必另外處理環狀邊界。
                zeroCount -= 1 - doubledNumbers[start - 1];
                zeroCount += 1 - doubledNumbers[start + oneCount - 1];
                minimumSwaps = Math.Min(minimumSwaps, zeroCount);
            }

            return minimumSwaps;
        }
    }
}