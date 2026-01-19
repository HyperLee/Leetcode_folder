namespace leetcode_491;

class Program
{
    /// <summary>
    /// 491. Non-decreasing Subsequences
    /// https://leetcode.com/problems/non-decreasing-subsequences/description/
    /// 491. 非递减子序列
    /// https://leetcode.cn/problems/non-decreasing-subsequences/description/
    /// 
    /// Given an integer array nums, return all the different possible non-decreasing subsequences of the given array with at least two elements. 
    /// You may return the answer in any order.
    /// 
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        var program = new Program();

        // 測試範例 1: [4, 6, 7, 7]
        // 預期輸出: [[4,6], [4,6,7], [4,6,7,7], [4,7], [4,7,7], [6,7], [6,7,7], [7,7]]
        int[] nums1 = [4, 6, 7, 7];
        var result1 = program.FindSubsequences(nums1);
        Console.WriteLine($"輸入: [{string.Join(", ", nums1)}]");
        Console.WriteLine($"輸出: [{string.Join(", ", result1.Select(r => $"[{string.Join(", ", r)}]"))}]");
        Console.WriteLine();

        // 測試範例 2: [4, 4, 3, 2, 1]
        // 預期輸出: [[4, 4]]
        int[] nums2 = [4, 4, 3, 2, 1];
        var result2 = program.FindSubsequences(nums2);
        Console.WriteLine($"輸入: [{string.Join(", ", nums2)}]");
        Console.WriteLine($"輸出: [{string.Join(", ", result2.Select(r => $"[{string.Join(", ", r)}]"))}]");
        Console.WriteLine();

        // 測試範例 3: [1, 2, 3, 4, 5]
        // 全部遞增序列，結果較多
        int[] nums3 = [1, 2, 3, 4, 5];
        var result3 = program.FindSubsequences(nums3);
        Console.WriteLine($"輸入: [{string.Join(", ", nums3)}]");
        Console.WriteLine($"共找到 {result3.Count} 個非遞減子序列");
    }

    /// <summary>
    /// 找出所有不同的非遞減子序列（至少包含兩個元素）。
    /// <para>
    /// 解題思路：使用深度優先搜尋（DFS）配合回溯法（Backtracking）。
    /// </para>
    /// <para>
    /// 核心概念：
    /// 1. 從陣列的每個位置開始，嘗試將元素加入當前子序列
    /// 2. 只有當新元素 >= 當前子序列的最後一個元素時，才能加入（保持非遞減）
    /// 3. 使用 HashSet 在每層遞迴中避免選取重複的元素，確保結果不重複
    /// </para>
    /// <example>
    /// <code>
    /// var program = new Program();
    /// int[] nums = [4, 6, 7, 7];
    /// var result = program.FindSubsequences(nums);
    ///  結果: [[4,6], [4,6,7], [4,6,7,7], [4,7], [4,7,7], [6,7], [6,7,7], [7,7]]
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="nums">輸入的整數陣列</param>
    /// <returns>所有不同的非遞減子序列清單</returns>
    public IList<IList<int>> FindSubsequences(int[] nums)
    {
        // 當前正在建構的子序列
        var current = new List<int>();

        // 儲存所有符合條件的結果
        var result = new List<IList<int>>();

        // 從索引 0 開始進行回溯搜尋
        Backtrack(0, nums, current, result);

        return result;
    }

    /// <summary>
    /// 回溯法核心函式：遞迴地建構所有非遞減子序列。
    /// <para>
    /// 演算法說明：
    /// 1. 每層遞迴使用 HashSet 記錄已選過的數字，避免同一層選取重複元素產生重複子序列
    /// 2. 當子序列長度 >= 2 時，將其加入結果集
    /// 3. 只有當待加入的元素 >= 子序列最後一個元素時，才符合「非遞減」條件
    /// </para>
    /// <para>
    /// 時間複雜度：O(2^n * n)，最壞情況下需要產生所有子序列
    /// 空間複雜度：O(n)，遞迴堆疊深度 + 當前子序列長度
    /// </para>
    /// </summary>
    /// <param name="startIndex">當前搜尋的起始索引位置</param>
    /// <param name="nums">原始輸入陣列</param>
    /// <param name="current">當前正在建構的子序列</param>
    /// <param name="result">儲存所有有效結果的清單</param>
    public static void Backtrack(int startIndex, int[] nums, List<int> current, List<IList<int>> result)
    {
        // 當子序列長度 >= 2 時，符合題目要求，加入結果集
        // 注意：這裡必須建立新的 List 複製，因為 current 會在回溯過程中被修改
        if (current.Count >= 2)
        {
            result.Add(new List<int>(current));
        }

        // 遞迴終止條件：已經遍歷完所有元素
        if (startIndex == nums.Length)
        {
            return;
        }

        // 在每一層遞迴中，使用 HashSet 來記錄已經選取過的數字
        // 這是去重的關鍵：避免在同一層選取相同的數字，產生重複的子序列
        var set = new HashSet<int>();

        for (int index = startIndex; index < nums.Length; index++)
        {
            // 去重：如果這個數字在當前層已經被選過，跳過
            // 例如：[4, 7, 7] 在選擇第二個位置時，第一個 7 和第二個 7 會產生相同的子序列
            if (set.Contains(nums[index]))
            {
                continue;
            }

            // 非遞減條件檢查：新元素必須 >= 子序列的最後一個元素
            // 如果 current 為空，則任何元素都可以加入
            if (current.Count > 0 && current.Last() > nums[index])
            {
                continue;
            }

            // 記錄當前層已選取的數字
            set.Add(nums[index]);

            // 選擇當前元素：將其加入子序列
            current.Add(nums[index]);

            // 遞迴：從下一個位置繼續搜尋
            Backtrack(index + 1, nums, current, result);

            // 回溯：撤銷選擇，移除最後加入的元素，以便嘗試其他可能性
            current.RemoveAt(current.Count - 1);
        }
    }
}
