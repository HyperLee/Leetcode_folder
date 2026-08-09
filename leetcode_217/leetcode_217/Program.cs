namespace leetcode_217
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 217. Contains Duplicate
        /// https://leetcode.com/problems/contains-duplicate/description/
        ///
        /// Given an integer array nums, return true if any value appears at least twice; return false if every element is distinct.
        ///
        /// Example 1:
        /// Input: nums = [1,2,3,1]
        /// Output: true
        /// Explanation: Value 1 occurs at indices 0 and 3.
        ///
        /// Example 2:
        /// Input: nums = [1,2,3,4]
        /// Output: false
        /// Explanation: Every element is distinct.
        ///
        /// Example 3:
        /// Input: nums = [1,1,1,3,3,4,3,2,4,2]
        /// Output: true
        ///
        /// Constraints:
        /// - 1 &lt;= nums.length &lt;= 10^5
        /// - -10^9 &lt;= nums[i] &lt;= 10^9
        /// </para>
        /// <para>
        /// 217. 存在重複元素
        /// https://leetcode.cn/problems/contains-duplicate/description/
        ///
        /// 給定整數陣列 nums，若任何值至少出現兩次則回傳 true；若所有元素皆不相同則回傳 false。
        ///
        /// 範例 1：
        /// 輸入：nums = [1,2,3,1]
        /// 輸出：true
        /// 說明：數值 1 出現在索引 0 與 3。
        ///
        /// 範例 2：
        /// 輸入：nums = [1,2,3,4]
        /// 輸出：false
        /// 說明：所有元素皆不相同。
        ///
        /// 範例 3：
        /// 輸入：nums = [1,1,1,3,3,4,3,2,4,2]
        /// 輸出：true
        ///
        /// 限制條件：
        /// - 1 &lt;= nums.length &lt;= 10^5
        /// - -10^9 &lt;= nums[i] &lt;= 10^9
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TestCase[] testCases =
            [
                new("官方案例 1：非相鄰重複值", [1, 2, 3, 1], true),
                new("官方案例 2：所有元素皆不重複", [1, 2, 3, 4], false),
                new("官方案例 3：多個數值重複出現", [1, 1, 1, 3, 3, 4, 3, 2, 4, 2], true),
                new("防禦性案例：空陣列", [], false),
                new("邊界案例：單一元素", [1], false),
                new("邊界案例：包含負數重複值", [-1, 0, -1], true),
                new("邊界案例：最小值與最大值", [-1_000_000_000, 1_000_000_000, -1_000_000_000], true)
            ];

            int passedChecks = 0;

            Console.WriteLine("LeetCode 217 - Contains Duplicate");
            Console.WriteLine("==================================================");

            foreach (TestCase testCase in testCases)
            {
                int[] sortingInput = [.. testCase.Numbers];
                int[] dictionaryInput = [.. testCase.Numbers];
                bool sortingResult = ContainsDuplicate(sortingInput);
                bool dictionaryResult = ContainsDuplicate2(dictionaryInput);
                bool sortingPassed = sortingResult == testCase.Expected;
                bool dictionaryPassed = dictionaryResult == testCase.Expected;

                passedChecks += sortingPassed ? 1 : 0;
                passedChecks += dictionaryPassed ? 1 : 0;

                Console.WriteLine($"Case: {testCase.Name}");
                Console.WriteLine($"Input: [{string.Join(", ", testCase.Numbers)}]");
                Console.WriteLine($"Expected: {testCase.Expected}");
                Console.WriteLine($"ContainsDuplicate: {sortingResult} ({(sortingPassed ? "PASS" : "FAIL")})");
                Console.WriteLine($"ContainsDuplicate2: {dictionaryResult} ({(dictionaryPassed ? "PASS" : "FAIL")})");
                Console.WriteLine();
            }

            int totalChecks = testCases.Length * 2;
            Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
            if (passedChecks != totalChecks)
            {
                Environment.ExitCode = 1;
            }
        }


        /// <summary>
        /// 檢查整數陣列是否包含重複值。先原地排序，讓相同數值相鄰，再逐對比較；
        /// 適用於非 <see langword="null"/> 的輸入，會改變陣列順序，並回傳是否存在重複值。
        /// 時間複雜度為 O(n log n)，輔助空間為 O(log n)，結果空間為 O(1)。
        /// </summary>
        /// <param name="nums">
        /// 要檢查的整數陣列；官方有效輸入長度為 1 至 100000，本方法對空陣列亦回傳
        /// <see langword="false"/>。
        /// </param>
        /// <returns>若任一數值至少出現兩次則回傳 <see langword="true"/>；否則回傳 <see langword="false"/>。</returns>
        public static bool ContainsDuplicate(int[] nums)
        {
            Array.Sort(nums);

            // 排序後相同值必定相鄰，因此只需檢查相鄰元素，不必枚舉所有索引組合。
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i - 1] == nums[i])
                {
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// 檢查整數陣列是否包含重複值。使用 <see cref="Dictionary{TKey, TValue}"/> 記錄
        /// 已看過的數值，第二次遇到相同鍵時立即回傳；適用於非 <see langword="null"/>
        /// 的輸入，不會改變陣列內容。平均時間複雜度為 O(n)，輔助空間為 O(n)，結果空間為 O(1)。
        /// </summary>
        /// <param name="nums">
        /// 要檢查的整數陣列；官方有效輸入長度為 1 至 100000，本方法對空陣列亦回傳
        /// <see langword="false"/>。
        /// </param>
        /// <returns>若任一數值至少出現兩次則回傳 <see langword="true"/>；否則回傳 <see langword="false"/>。</returns>
        public static bool ContainsDuplicate2(int[] nums)
        {
            Dictionary<int, int> seenNumbers = new Dictionary<int, int>();

            foreach (int num in nums)
            {
                if (seenNumbers.ContainsKey(num))
                {
                    // 字典已有此鍵，代表目前值至少是第二次出現，可以立即確定答案。
                    return true;
                }

                seenNumbers[num] = 1;
            }

            return false;
        }

        private sealed record TestCase(string Name, int[] Numbers, bool Expected);
    }
}
