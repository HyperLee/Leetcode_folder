namespace leetcode_2441
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 2441. Largest Positive Integer That Exists With Its Negative
        /// https://leetcode.com/problems/largest-positive-integer-that-exists-with-its-negative/description/
        ///
        /// Given integer array nums containing no zeros, find the largest positive integer k for which -k also occurs in nums. Return k, or return -1 if no such integer exists.
        ///
        /// Example 1:
        /// Input: nums = [-1,2,-3,3]
        /// Output: 3
        /// Explanation: 3 is the only valid k in the array.
        ///
        /// Example 2:
        /// Input: nums = [-1,10,6,7,-7,1]
        /// Output: 7
        /// Explanation: Both 1 and 7 have their corresponding negatives in the array, and 7 is larger.
        ///
        /// Example 3:
        /// Input: nums = [-10,8,6,7,-2,-3]
        /// Output: -1
        /// Explanation: There is no valid k, so return -1.
        ///
        /// Constraints:
        /// - 1 &lt;= nums.length &lt;= 1000
        /// - -1000 &lt;= nums[i] &lt;= 1000
        /// - nums[i] != 0
        /// </para>
        /// <para>
        /// 2441. 與對應負數同時存在的最大正整數
        /// https://leetcode.cn/problems/largest-positive-integer-that-exists-with-its-negative/description/
        ///
        /// 給定不含零的整數陣列 nums，找出滿足 -k 也存在於 nums 中的最大正整數 k。若存在則回傳 k，否則回傳 -1。
        ///
        /// 範例 1：
        /// 輸入：nums = [-1,2,-3,3]
        /// 輸出：3
        /// 說明：3 是陣列中唯一有效的 k。
        ///
        /// 範例 2：
        /// 輸入：nums = [-1,10,6,7,-7,1]
        /// 輸出：7
        /// 說明：1 與 7 的對應負數都存在於陣列中，且 7 較大。
        ///
        /// 範例 3：
        /// 輸入：nums = [-10,8,6,7,-2,-3]
        /// 輸出：-1
        /// 說明：沒有有效的 k，因此回傳 -1。
        ///
        /// 限制條件：
        /// - 1 &lt;= nums.length &lt;= 1000
        /// - -1000 &lt;= nums[i] &lt;= 1000
        /// - nums[i] != 0
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            (string Name, int[] Nums, int Expected)[] cases =
            [
                ("官方範例一：只有一組有效配對", [-1, 2, -3, 3], 3),
                ("官方範例二：多組配對取最大值", [-1, 10, 6, 7, -7, 1], 7),
                ("官方範例三：沒有相反數配對", [-10, 8, 6, 7, -2, -3], -1),
                ("單一元素", [1], -1),
                ("含有重複值", [-1, 1, -1], 1),
                ("數值邊界", [1000, -1000, 999, -999], 1000)
            ];

            int passedChecks = 0;
            foreach ((string name, int[] nums, int expected) in cases)
            {
                passedChecks += RunCase(name, nums, expected);
            }

            int totalChecks = cases.Length * 2;
            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項測試通過");

            if (passedChecks != totalChecks)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 以同一筆測試資料分別執行排序雙指針與雜湊集合解法，並比較結果是否符合預期值。
        /// 輸入陣列須符合題目限制且不含零；方法會為每種解法建立獨立複本，避免排序造成交互影響。
        /// </summary>
        /// <param name="name">顯示於主控台的案例名稱。</param>
        /// <param name="nums">要交給兩種解法處理的整數陣列。</param>
        /// <param name="expected">人工推導的預期最大正整數；沒有有效配對時為 -1。</param>
        /// <returns>兩種解法中結果符合預期值的數量，範圍為 0 到 2。</returns>
        private static int RunCase(string name, int[] nums, int expected)
        {
            int twoPointersResult = FindMaxK((int[])nums.Clone());
            int hashSetResult = FindMaxK2((int[])nums.Clone());
            bool twoPointersPassed = twoPointersResult == expected;
            bool hashSetPassed = hashSetResult == expected;

            Console.WriteLine($"案例：{name}");
            Console.WriteLine($"輸入：[{string.Join(", ", nums)}]");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"FindMaxK: {twoPointersResult} - {(twoPointersPassed ? "PASS" : "FAIL")}");
            Console.WriteLine($"FindMaxK2: {hashSetResult} - {(hashSetPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return (twoPointersPassed ? 1 : 0) + (hashSetPassed ? 1 : 0);
        }

        /// <summary>
        /// 以排序與雙指針尋找同時存在正負值的最大正整數。
        /// 排序後從最小值與最大值向中央收斂，依兩端總和判斷應排除過大的正數或過小的負數；
        /// 輸入須為長度 1 到 1000、元素介於 -1000 到 1000 且不含零的陣列，並會被原地排序。
        /// </summary>
        /// <param name="nums">要搜尋的整數陣列；呼叫後元素順序會改為遞增排列。</param>
        /// <returns>符合條件的最大正整數；沒有任何相反數配對時回傳 -1。</returns>
        public static int FindMaxK(int[] nums)
        {
            Array.Sort(nums);

            int left = 0, right = nums.Length - 1;
            while (left < right)
            {
                if (nums[left] + nums[right] == 0)
                {
                    // right 從最大值開始向左移動，因此第一組相反數配對就是最大答案。
                    return nums[right];
                }
                else if (nums[left] + nums[right] > 0)
                {
                    // 總和大於零代表右側正數過大，向左尋找較小的正數。
                    right--;
                }
                else
                {
                    // 總和小於零代表左側負數絕對值過大，向右尋找較大的負數。
                    left++;
                }
            }

            return -1;
        }

        /// <summary>
        /// 以雜湊集合記錄已看過的數值，單次掃描尋找相反數配對並維護最大正整數。
        /// 每次讀取元素時檢查其相反數是否已存在，若存在便以絕對值更新答案；
        /// 輸入須為長度 1 到 1000、元素介於 -1000 到 1000 且不含零的陣列，方法不會修改輸入內容。
        /// </summary>
        /// <param name="nums">要搜尋且不會被修改的整數陣列。</param>
        /// <returns>符合條件的最大正整數；沒有任何相反數配對時回傳 -1。</returns>
        public static int FindMaxK2(int[] nums)
        {
            int ans = -1;
            ISet<int> seen = new HashSet<int>();

            foreach (int x in nums)
            {
                // 相反數已出現時即形成有效配對，絕對值就是該配對的正整數。
                if (seen.Contains(-x))
                {
                    ans = Math.Max(ans, Math.Abs(x));
                }

                seen.Add(x);
            }

            return ans;
        }
    }
}