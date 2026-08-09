using System.Globalization;

namespace leetcode_004;

class Program
{
    /// <summary>
    /// 4. Median of Two Sorted Arrays
    /// https://leetcode.com/problems/median-of-two-sorted-arrays/description/
    /// <para>
    /// Given two sorted arrays nums1 and nums2 of sizes m and n respectively, return the median of the two sorted arrays.
    ///
    /// The overall run time complexity should be O(log (m+n)).
    ///
    /// Example 1:
    /// Input: nums1 = [1,3], nums2 = [2]
    /// Output: 2.00000
    /// Explanation: merged array = [1,2,3] and median is 2.
    ///
    /// Example 2:
    /// Input: nums1 = [1,2], nums2 = [3,4]
    /// Output: 2.50000
    /// Explanation: merged array = [1,2,3,4] and median is (2 + 3) / 2 = 2.5.
    ///
    /// Constraints:
    /// - nums1.length == m
    /// - nums2.length == n
    /// - 0 &lt;= m &lt;= 1000
    /// - 0 &lt;= n &lt;= 1000
    /// - 1 &lt;= m + n &lt;= 2000
    /// - -10^6 &lt;= nums1[i], nums2[i] &lt;= 10^6
    /// </para>
    /// <para>
    /// 4. 尋找兩個排序陣列的中位數
    /// https://leetcode.cn/problems/median-of-two-sorted-arrays/description/
    ///
    /// 給定兩個大小分別為 m 和 n 的排序陣列 nums1 與 nums2，請回傳這兩個排序陣列的中位數。
    ///
    /// 整體執行時間複雜度應為 O(log (m+n))。
    ///
    /// 範例 1：
    /// 輸入：nums1 = [1,3], nums2 = [2]
    /// 輸出：2.00000
    /// 解釋：合併後的陣列為 [1,2,3]，中位數是 2。
    ///
    /// 範例 2：
    /// 輸入：nums1 = [1,2], nums2 = [3,4]
    /// 輸出：2.50000
    /// 解釋：合併後的陣列為 [1,2,3,4]，中位數是 (2 + 3) / 2 = 2.5。
    ///
    /// 限制條件：
    /// - nums1.length == m
    /// - nums2.length == n
    /// - 0 &lt;= m &lt;= 1000
    /// - 0 &lt;= n &lt;= 1000
    /// - 1 &lt;= m + n &lt;= 2000
    /// - -10^6 &lt;= nums1[i], nums2[i] &lt;= 10^6
    /// </para>
    /// </summary>
    /// <remarks>
    /// 使用六組固定案例分別驗證合併排序與二分搜尋解法，並輸出每次檢查及總結。
    /// </remarks>
    /// <param name="args">命令列參數；本範例不使用。</param>
    static void Main(string[] args)
    {
        (string Name, int[] Nums1, int[] Nums2, double Expected)[] testCases =
        [
            ("Odd total length", [1, 3], [2], 2.0),
            ("Even total length", [1, 2], [3, 4], 2.5),
            ("Empty nums1", [], [1], 1.0),
            ("nums1 is longer", [1, 2, 9], [3], 2.5),
            ("Negative values", [-5, -3, -1], [-2], -2.5),
            ("Duplicate values", [0, 0], [0, 0], 0.0)
        ];

        (string Name, Func<int[], int[], double> Solver)[] solutions =
        [
            ("Merge and sort", FindMedianSortedArrays),
            ("Binary search", FindMedianSortedArrays2)
        ];

        int passedChecks = 0;
        int totalChecks = testCases.Length * solutions.Length;

        Console.WriteLine("4. Median of Two Sorted Arrays");
        Console.WriteLine("================================");

        for (int caseIndex = 0; caseIndex < testCases.Length; caseIndex++)
        {
            var testCase = testCases[caseIndex];
            Console.WriteLine($"Case {caseIndex + 1}: {testCase.Name}");
            Console.WriteLine($"  nums1: [{string.Join(", ", testCase.Nums1)}]");
            Console.WriteLine($"  nums2: [{string.Join(", ", testCase.Nums2)}]");
            Console.WriteLine(
                $"  Expected: {testCase.Expected.ToString("0.#####", CultureInfo.InvariantCulture)}");

            foreach (var solution in solutions)
            {
                // 每種解法使用獨立輸入，避免未來實作若修改陣列而影響下一次檢查。
                double result = solution.Solver(
                    (int[])testCase.Nums1.Clone(),
                    (int[])testCase.Nums2.Clone());
                bool passed = result.Equals(testCase.Expected);

                if (passed)
                {
                    passedChecks++;
                }

                Console.WriteLine(
                    $"  {solution.Name}: {result.ToString("0.#####", CultureInfo.InvariantCulture)} " +
                    $"({(passed ? "PASS" : "FAIL")})");
            }

            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");

        if (passedChecks != totalChecks)
        {
            Environment.ExitCode = 1;
        }
    }


    /// <summary>
    /// 合併兩個陣列、重新排序後，依總長度的奇偶性計算中位數。
    /// </summary>
    /// <remarks>
    /// 這是容易理解的基準解法。時間複雜度為 O((m+n) log(m+n))，
    /// 額外空間複雜度為 O(m+n)，未達題目要求的對數時間。
    /// </remarks>
    /// <param name="nums1">第一個遞增排序整數陣列，可以是空陣列。</param>
    /// <param name="nums2">第二個遞增排序整數陣列，可以是空陣列。</param>
    /// <returns>兩個陣列全部元素的中位數。</returns>
    public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        // 合併後重新排序，讓中間位置可直接代表中位數。
        int[] merged = nums1.Concat(nums2).ToArray();
        Array.Sort(merged);

        int length = merged.Length;
        if (length % 2 == 0)
        {
            int index = length / 2;
            // 偶數長度取中央兩數的平均；除以 2.0 保留小數。
            return (merged[index - 1] + merged[index]) / 2.0;
        }

        // 奇數長度只有一個中央元素。
        return merged[length / 2];
    }


    /// <summary>
    /// 在較短陣列上二分搜尋分割點，使左右兩側數量平衡且左側值皆不大於右側值。
    /// </summary>
    /// <remarks>
    /// 找到合法分割後，奇數總長度取左側最大值，偶數總長度取左側最大值與右側最小值的平均。
    /// 時間複雜度為 O(log(min(m,n)))，額外空間複雜度為 O(1)。
    /// </remarks>
    /// <param name="nums1">第一個遞增排序整數陣列，可以是空陣列。</param>
    /// <param name="nums2">第二個遞增排序整數陣列，可以是空陣列。</param>
    /// <returns>兩個陣列全部元素的中位數。</returns>
    public static double FindMedianSortedArrays2(int[] nums1, int[] nums2)
    {
        // 只在較短陣列上搜尋，將搜尋範圍限制在 min(m, n)。
        if (nums1.Length > nums2.Length)
        {
            return FindMedianSortedArrays2(nums2, nums1);
        }

        int x = nums1.Length;
        int y = nums2.Length;
        int low = 0;
        int high = x;

        while (low <= high)
        {
            int partitionX = (low + high) / 2;
            int partitionY = (x + y + 1) / 2 - partitionX;

            // 使用極值表示切割點外沒有元素，統一處理空陣列與邊界分割。
            int maxLeftX = (partitionX == 0) ? int.MinValue : nums1[partitionX - 1];
            int minRightX = (partitionX == x) ? int.MaxValue : nums1[partitionX];
            int maxLeftY = (partitionY == 0) ? int.MinValue : nums2[partitionY - 1];
            int minRightY = (partitionY == y) ? int.MaxValue : nums2[partitionY];

            // 合法分割必須讓兩個左半部的最大值都不大於對側右半部的最小值。
            if (maxLeftX <= minRightY && maxLeftY <= minRightX)
            {
                if ((x + y) % 2 == 0)
                {
                    return (Math.Max(maxLeftX, maxLeftY) +
                        Math.Min(minRightX, minRightY)) / 2.0;
                }

                return Math.Max(maxLeftX, maxLeftY);
            }

            if (maxLeftX > minRightY)
            {
                // nums1 左側過大，分割點必須左移。
                high = partitionX - 1;
            }
            else
            {
                // nums1 左側過小，分割點必須右移。
                low = partitionX + 1;
            }
        }

        // 在符合題目輸入條件時必定能找到合法分割。
        return 0;
    }
}
