namespace leetcode_075;

internal class Program
{
    /// <summary>
    /// <para>
    /// 75. Sort Colors
    /// https://leetcode.com/problems/sort-colors/
    ///
    /// Given an array nums with n objects colored red, white, or blue, sort them in-place so that objects
    /// of the same color are adjacent, with the colors in the order red, white, and blue.
    /// We will use the integers 0, 1, and 2 to represent the color red, white, and blue, respectively.
    /// You must solve this problem without using the library's sort function.
    ///
    /// Example 1:
    /// Input: nums = [2,0,2,1,1,0]
    /// Output: [0,0,1,1,2,2]
    ///
    /// Example 2:
    /// Input: nums = [2,0,1]
    /// Output: [0,1,2]
    ///
    /// Constraints:
    /// n == nums.length
    /// 1 &lt;= n &lt;= 300
    /// nums[i] is either 0, 1, or 2.
    ///
    /// Follow up: Could you come up with a one-pass algorithm using only constant extra space?
    /// </para>
    /// <para>
    /// 75. 顏色分類
    /// https://leetcode.cn/problems/sort-colors/
    ///
    /// 給定一個包含 n 個物件的陣列 nums，物件的顏色分別為紅色、白色或藍色，請原地排序，
    /// 使相同顏色的物件彼此相鄰，且顏色依紅、白、藍的順序排列。
    /// 我們分別使用整數 0、1、2 表示紅色、白色與藍色。
    /// 你必須在不使用函式庫排序函式的情況下解決此問題。
    ///
    /// 範例 1：
    /// 輸入：nums = [2,0,2,1,1,0]
    /// 輸出：[0,0,1,1,2,2]
    ///
    /// 範例 2：
    /// 輸入：nums = [2,0,1]
    /// 輸出：[0,1,2]
    ///
    /// 限制條件：
    /// n == nums.length
    /// 1 &lt;= n &lt;= 300
    /// nums[i] 只能是 0、1 或 2。
    ///
    /// 進階：你能設計一個僅使用常數額外空間的一趟掃描演算法嗎？
    /// </para>
    /// </summary>
    /// <remarks>
    /// 主要進入點會以八組固定資料驗證唯一保留的荷蘭國旗解法，
    /// 並輸出輸入、預期結果、實際結果與 PASS/FAIL。
    /// </remarks>
    /// <param name="args">命令列參數；本範例不使用。</param>
    private static void Main(string[] args)
    {
        (string Name, int[] Input, int[] Expected)[] testCases =
        {
            ("官方範例 1", new[] { 2, 0, 2, 1, 1, 0 }, new[] { 0, 0, 1, 1, 2, 2 }),
            ("官方範例 2", new[] { 2, 0, 1 }, new[] { 0, 1, 2 }),
            ("單一元素", new[] { 1 }, new[] { 1 }),
            ("已排序", new[] { 0, 0, 1, 1, 2, 2 }, new[] { 0, 0, 1, 1, 2, 2 }),
            ("反向排列", new[] { 2, 2, 1, 1, 0, 0 }, new[] { 0, 0, 1, 1, 2, 2 }),
            ("全部相同", new[] { 2, 2, 2 }, new[] { 2, 2, 2 }),
            ("只含兩色", new[] { 2, 0, 2, 0 }, new[] { 0, 0, 2, 2 }),
            ("右側換回未分類值", new[] { 2, 2, 0, 1, 0 }, new[] { 0, 0, 1, 2, 2 })
        };

        int passed = 0;

        foreach ((string caseName, int[] input, int[] expected) in testCases)
        {
            int[] actual = (int[])input.Clone();
            SortColors(actual);

            bool isCorrect = actual.SequenceEqual(expected);
            if (isCorrect)
            {
                passed++;
            }

            Console.WriteLine(
                $"{caseName} | Input: {FormatArray(input)} | Expected: {FormatArray(expected)} | " +
                $"Actual: {FormatArray(actual)} | {(isCorrect ? "PASS" : "FAIL")}");
        }

        Console.WriteLine();
        Console.WriteLine($"Overall: {passed}/{testCases.Length} passed.");

        if (passed != testCases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 將整數陣列格式化為方括號包住、以逗號分隔的緊湊表示法。
    /// 方法不會修改傳入的陣列。
    /// </summary>
    /// <param name="nums">要格式化的非 null 整數陣列。</param>
    /// <returns>例如 <c>[2,0,1]</c> 的陣列字串。</returns>
    private static string FormatArray(int[] nums)
    {
        return $"[{string.Join(",", nums)}]";
    }

    /// <summary>
    /// 使用荷蘭國旗三指標法，將只包含 0、1、2 的陣列原地排序。
    /// 處理期間維持四個區間：<c>[0, low)</c> 全為 0、<c>[low, mid)</c> 全為 1、
    /// <c>[mid, high]</c> 尚未分類、<c>(high, n)</c> 全為 2。
    /// 輸入長度須為 1 到 300，且元素只能是 0、1 或 2。
    /// 時間複雜度為 O(n)，額外空間複雜度為 O(1)。
    /// </summary>
    /// <param name="nums">要直接修改並排序的顏色陣列。</param>
    public static void SortColors(int[] nums)
    {
        int low = 0;
        int mid = 0;
        int high = nums.Length - 1;

        while (mid <= high)
        {
            switch (nums[mid])
            {
                case 0:
                    (nums[low], nums[mid]) = (nums[mid], nums[low]);
                    low++;
                    mid++;
                    break;
                case 1:
                    mid++;
                    break;
                case 2:
                    (nums[mid], nums[high]) = (nums[high], nums[mid]);
                    high--;

                    // 右側換回來的值尚未分類，因此 mid 必須留在原位再次判斷。
                    break;
            }
        }
    }
}