namespace leetcode_283;

class Program
{
    /// <summary>
    /// 283. Move Zeroes
    /// https://leetcode.com/problems/move-zeroes/description/
    /// <para>
    /// Given an integer array nums, move all 0s to the end while maintaining the relative order of the non-zero elements.
    ///
    /// Note that you must do this in-place without making a copy of the array.
    ///
    /// Example 1:
    /// Input: nums = [0,1,0,3,12]
    /// Output: [1,3,12,0,0]
    ///
    /// Example 2:
    /// Input: nums = [0]
    /// Output: [0]
    ///
    /// Constraints:
    /// - 1 &lt;= nums.length &lt;= 10^4
    /// - -2^31 &lt;= nums[i] &lt;= 2^31 - 1
    ///
    /// Follow-up: Could you minimize the total number of operations performed?
    /// </para>
    /// <para>
    /// 283. 移動零
    /// https://leetcode.cn/problems/move-zeroes/description/
    ///
    /// 給定一個整數陣列 nums，將所有 0 移到陣列末端，同時保持非零元素的相對順序。
    ///
    /// 注意：你必須原地完成此操作，不得複製陣列。
    ///
    /// 範例 1：
    /// 輸入：nums = [0,1,0,3,12]
    /// 輸出：[1,3,12,0,0]
    ///
    /// 範例 2：
    /// 輸入：nums = [0]
    /// 輸出：[0]
    ///
    /// 限制條件：
    /// - 1 &lt;= nums.length &lt;= 10^4
    /// - -2^31 &lt;= nums[i] &lt;= 2^31 - 1
    ///
    /// 進階：你能將執行的操作總數降到最少嗎？
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試案例 1：範例輸入
        int[] nums1 = new int[] { 0, 1, 0, 3, 12 };
        Console.WriteLine("測試案例 1 - 原始陣列:");
        PrintArray(nums1);
        MoveZeroes(nums1);
        Console.WriteLine("測試案例 1 - 移動後:");
        PrintArray(nums1);
        Console.WriteLine();

        // 測試案例 2：所有 0 的情況
        int[] nums2 = new int[] { 0, 0, 0 };
        Console.WriteLine("測試案例 2 - 原始陣列:");
        PrintArray(nums2);
        MoveZeroes(nums2);
        Console.WriteLine("測試案例 2 - 移動後:");
        PrintArray(nums2);
        Console.WriteLine();

        // 測試案例 3：無 0 的情況
        int[] nums3 = new int[] { 1, 2, 3 };
        Console.WriteLine("測試案例 3 - 原始陣列:");
        PrintArray(nums3);
        MoveZeroes(nums3);
        Console.WriteLine("測試案例 3 - 移動後:");
        PrintArray(nums3);
        Console.WriteLine();

        // 測試案例 4：0 在尾端
        int[] nums4 = new int[] { 1, 0, 2, 0, 0, 3, 4 };
        Console.WriteLine("測試案例 4 - 原始陣列:");
        PrintArray(nums4);
        MoveZeroes(nums4);
        Console.WriteLine("測試案例 4 - 移動後:");
        PrintArray(nums4);
        Console.WriteLine();
    }

    /// <summary>
    /// 將陣列中的所有 0 移至末尾，同時保持非零元素的相對順序（在原地完成）。
    /// 
    /// 解題說明：
    /// 使用一個 pointer (寫入位置) 來記錄目前可交換的第一個 0 的索引。
    /// 逐一掃描陣列，若遇到 nums[i] != 0，則把 nums[i] 與 pointer 指向的索引交換，
    /// 並將 pointer 向右移動一格。這樣可確保所有非零元素保持相對順序，且最終
    /// 陣列右側皆為 0。
    /// 
    /// 時間複雜度：O(n)。空間複雜度：O(1)。
    /// </summary>
    /// <param name="nums">要在原地重新排列的非 null 整數陣列（參考上方繁體中文題目描述）。</param>
    /// <exception cref="ArgumentNullException">傳入的陣列為 null 時會拋出例外。</exception>
    public static void MoveZeroes(int[] nums)
    {
        // 參數檢查
        if (nums is null)
        {
            throw new ArgumentNullException(nameof(nums));
        }

        // pointer: 記錄目前第一個可交換的 0 的索引（即寫入下一個非零元素的位置）
        int pointer = 0;

        // 逐一掃描陣列，遇到非零元素時把它寫到 pointer 的位置（若 pointer != i 才交換），
        // 再將 pointer 往右移動一格，代表下一次要寫入的位置。
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] != 0)
            {
                // 如果 pointer 與當前索引 i 相同，代表目前這個非零元素已經是正確位置，不需要交換
                if (pointer != i)
                {
                    // 交換 nums[i] 與 nums[pointer]，把非零元素往前移，把 0 放到目前位置
                    nums[pointer] = nums[i];
                    nums[i] = 0;
                }

                // pointer 往右移動，尋找下一個可交換的 0
                pointer++;
            }
        }
    }

    /// <summary>
    /// Helper method: 以 console 輸出陣列內容（格式化輸出）。
    /// </summary>
    /// <param name="nums">要輸出的整數陣列，不可為 null。</param>
    private static void PrintArray(int[] nums)
    {
        if (nums is null)
        {
            Console.WriteLine("null");
            return;
        }

        Console.WriteLine("[" + string.Join(", ", nums) + "]");
    }
}
