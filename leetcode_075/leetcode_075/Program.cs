namespace leetcode_075;

internal class Program
{
    /// <summary>
    /// 75. Sort Colors
    /// https://leetcode.com/problems/sort-colors/
    /// 75. 顏色分類
    /// https://leetcode.cn/problems/sort-colors/
    ///
    /// 給定一個只包含 0、1、2 的整數陣列，請直接修改原陣列，
    /// 使相同數值彼此相鄰，並依照 0、1、2 的順序排列。
    /// 不可使用內建排序函式。
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