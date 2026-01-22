namespace leetcode_3507;

class Program
{
    /// <summary>
    /// 3507. Minimum Pair Removal to Sort Array I
    /// https://leetcode.com/problems/minimum-pair-removal-to-sort-array-i/description/?envType=daily-question&envId=2026-01-22
    /// 3507. 移除最小数对使数组有序 I
    /// https://leetcode.cn/problems/minimum-pair-removal-to-sort-array-i/description/?envType=daily-question&envId=2026-01-22
    /// 
    /// 題目：給定一個陣列 nums，可以執行下列操作任意次數：
    /// - 選擇陣列中相鄰且和最小的一對元素。若有多個，選擇最左邊的那一對。
    /// - 將該對替換為它們的和。
    /// 回傳使陣列變成非遞減（每個元素 >= 前一元素）所需的最少操作次數。
    ///
    /// English:
    /// Problem: Given an array nums, you can perform the following operation any number of times:
    /// - Select the adjacent pair with the minimum sum in nums. If multiple such pairs exist, choose the leftmost one.
    /// - Replace the pair with their sum.
    /// Return the minimum number of operations needed to make the array non-decreasing.
    /// An array is non-decreasing if each element is greater than or equal to its previous element.
    /// </summary>
    /// <param name="args">Runtime arguments (unused).</param>
    static void Main(string[] args)
    {
        var program = new Program();

        // 測試資料 1: [5, 2, 3, 1]
        // 預期輸出: 2
        // 說明: 需要 2 次操作使陣列變成非遞減
        int[] nums1 = [5, 2, 3, 1];
        Console.WriteLine($"Input: [{string.Join(", ", nums1)}]");
        Console.WriteLine($"Output: {program.MinimumPairRemoval(nums1)}");
        Console.WriteLine();

        // 測試資料 2: [1, 2, 2]
        // 預期輸出: 0
        // 說明: 陣列已經是非遞減，不需要操作
        int[] nums2 = [1, 2, 2];
        Console.WriteLine($"Input: [{string.Join(", ", nums2)}]");
        Console.WriteLine($"Output: {program.MinimumPairRemoval(nums2)}");
        Console.WriteLine();

        // 測試資料 3: [3, 2, 1]
        // 預期輸出: 2
        // 說明: 完全遞減的陣列，需要多次合併
        int[] nums3 = [3, 2, 1];
        Console.WriteLine($"Input: [{string.Join(", ", nums3)}]");
        Console.WriteLine($"Output: {program.MinimumPairRemoval(nums3)}");
        Console.WriteLine();

        // 測試資料 4: [1]
        // 預期輸出: 0
        // 說明: 單一元素，已經是非遞減
        int[] nums4 = [1];
        Console.WriteLine($"Input: [{string.Join(", ", nums4)}]");
        Console.WriteLine($"Output: {program.MinimumPairRemoval(nums4)}");
    }

    /// <summary>
    /// 使用模擬法解決「移除最小數對使陣列有序」問題。
    /// 
    /// <para><b>解題思路：</b></para>
    /// <para>
    /// 由於題目的資料範圍非常小，我們可以直接按照題意進行模擬：
    /// </para>
    /// <list type="number">
    ///     <item>
    ///         <description>遍歷陣列的相鄰元素，找出和最小的相鄰數對</description>
    ///     </item>
    ///     <item>
    ///         <description>同時檢查陣列是否已經滿足非嚴格單調遞增</description>
    ///     </item>
    ///     <item>
    ///         <description>若不滿足條件，將和最小的相鄰數對合併為新元素</description>
    ///     </item>
    ///     <item>
    ///         <description>重複以上操作，直到陣列滿足非嚴格單調遞增或長度為 1</description>
    ///     </item>
    /// </list>
    /// 
    /// <para><b>時間複雜度：</b>O(n²)，其中 n 為陣列長度</para>
    /// <para><b>空間複雜度：</b>O(n)，使用 List 儲存陣列元素</para>
    /// </summary>
    /// <param name="nums">輸入的整數陣列</param>
    /// <returns>使陣列變成非遞減所需的最少操作次數</returns>
    /// <example>
    /// <code>
    /// var solution = new Program();
    /// int result = solution.MinimumPairRemoval([5, 2, 3, 1]);
    ///  result = 2
    /// </code>
    /// </example>
    public int MinimumPairRemoval(int[] nums)
    {
        // 記錄操作次數
        int count = 0;

        // 將陣列轉換為 List 以便進行動態操作（刪除、修改）
        var list = new List<int>(nums);

        // 持續執行操作直到陣列長度為 1 或已滿足條件
        while (list.Count > 1)
        {
            // 假設陣列已經是非嚴格單調遞增
            bool isAscending = true;

            // 追蹤最小相鄰數對的和
            int minSum = int.MaxValue;

            // 追蹤最小相鄰數對的起始索引（最左邊的那一對）
            int targetIndex = -1;

            // 遍歷所有相鄰數對
            for (int i = 0; i < list.Count - 1; i++)
            {
                // 計算當前相鄰數對的和
                var sum = list[i] + list[i + 1];

                // 檢查是否違反非嚴格單調遞增條件
                // 若前一個元素大於後一個元素，則不滿足條件
                if (list[i] > list[i + 1])
                {
                    isAscending = false;
                }

                // 更新最小和及其索引（取最左邊的最小和數對）
                if (sum < minSum)
                {
                    minSum = sum;
                    targetIndex = i;
                }
            }

            // 若陣列已經滿足非嚴格單調遞增，結束迴圈
            if (isAscending)
            {
                break;
            }

            // 執行合併操作：將最小和數對合併為一個元素
            count++;

            // 將 targetIndex 位置的元素更新為兩元素的和
            list[targetIndex] = minSum;

            // 移除 targetIndex + 1 位置的元素（已被合併）
            list.RemoveAt(targetIndex + 1);
        }

        return count;
    }
}

