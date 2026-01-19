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
        
        Console.WriteLine("=== 解法一：回溯法（HashSet 去重）===");
        var result1 = program.FindSubsequences(nums1);
        Console.WriteLine($"輸入: [{string.Join(", ", nums1)}]");
        Console.WriteLine($"輸出: [{string.Join(", ", result1.Select(r => $"[{string.Join(", ", r)}]"))}]");
        Console.WriteLine();

        Console.WriteLine("=== 解法二：優化版回溯法（陣列去重 + stackalloc）===");
        var result1Optimized = program.FindSubsequencesOptimized(nums1);
        Console.WriteLine($"輸入: [{string.Join(", ", nums1)}]");
        Console.WriteLine($"輸出: [{string.Join(", ", result1Optimized.Select(r => $"[{string.Join(", ", r)}]"))}]");
        Console.WriteLine();

        Console.WriteLine("=== 解法三：位元遮罩枚舉法 ===");
        var result1Bitmask = program.FindSubsequencesBitmask(nums1);
        Console.WriteLine($"輸入: [{string.Join(", ", nums1)}]");
        Console.WriteLine($"輸出: [{string.Join(", ", result1Bitmask.Select(r => $"[{string.Join(", ", r)}]"))}]");
        Console.WriteLine();

        // 測試範例 2: [4, 4, 3, 2, 1]
        // 預期輸出: [[4, 4]]
        int[] nums2 = [4, 4, 3, 2, 1];
        Console.WriteLine("=== 測試範例 2 ===");
        var result2 = program.FindSubsequencesOptimized(nums2);
        Console.WriteLine($"輸入: [{string.Join(", ", nums2)}]");
        Console.WriteLine($"輸出: [{string.Join(", ", result2.Select(r => $"[{string.Join(", ", r)}]"))}]");
        Console.WriteLine();

        // 測試範例 3: [1, 2, 3, 4, 5]
        // 全部遞增序列，結果較多
        int[] nums3 = [1, 2, 3, 4, 5];
        Console.WriteLine("=== 測試範例 3（效能比較）===");
        var result3 = program.FindSubsequencesOptimized(nums3);
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
    private static void Backtrack(int startIndex, int[] nums, List<int> current, List<IList<int>> result)
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
        var used = new HashSet<int>();

        for (int index = startIndex; index < nums.Length; index++)
        {
            // 去重：如果這個數字在當前層已經被選過，跳過
            // 例如：[4, 7, 7] 在選擇第二個位置時，第一個 7 和第二個 7 會產生相同的子序列
            if (used.Contains(nums[index]))
            {
                continue;
            }

            // 非遞減條件檢查：新元素必須 >= 子序列的最後一個元素
            // 如果 current 為空，則任何元素都可以加入
            // 使用索引存取 [^1] 取代 LINQ 的 Last() 方法，效能更佳
            if (current.Count > 0 && current[^1] > nums[index])
            {
                continue;
            }

            // 記錄當前層已選取的數字
            used.Add(nums[index]);

            // 選擇當前元素：將其加入子序列
            current.Add(nums[index]);

            // 遞迴：從下一個位置繼續搜尋
            Backtrack(index + 1, nums, current, result);

            // 回溯：撤銷選擇，移除最後加入的元素，以便嘗試其他可能性
            current.RemoveAt(current.Count - 1);
        }
    }

    #region 解法二：優化版回溯法（使用陣列取代 HashSet）

    /// <summary>
    /// 找出所有不同的非遞減子序列（優化版本）。
    /// <para>
    /// 優化策略：
    /// 1. 使用固定大小的布林陣列取代 HashSet 進行去重，減少雜湊運算開銷
    /// 2. 由於題目限制 -100 ≤ nums[i] ≤ 100，可使用大小為 201 的陣列（索引 0~200 對應值 -100~100）
    /// 3. 使用索引存取 [^1] 取代 LINQ 的 Last() 方法
    /// </para>
    /// <example>
    /// <code>
    /// var program = new Program();
    /// int[] nums = [4, 6, 7, 7];
    /// var result = program.FindSubsequencesOptimized(nums);
    ///  結果: [[4,6], [4,6,7], [4,6,7,7], [4,7], [4,7,7], [6,7], [6,7,7], [7,7]]
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="nums">輸入的整數陣列</param>
    /// <returns>所有不同的非遞減子序列清單</returns>
    public IList<IList<int>> FindSubsequencesOptimized(int[] nums)
    {
        var current = new List<int>();
        var result = new List<IList<int>>();
        BacktrackOptimized(0, nums, current, result);
        return result;
    }

    /// <summary>
    /// 優化版回溯法核心函式。
    /// <para>
    /// 優化重點：
    /// 1. 使用 Span&lt;bool&gt; 配合 stackalloc 在堆疊上分配記憶體，避免堆積記憶體分配
    /// 2. 陣列索引計算：nums[i] + 100 將 -100~100 映射到 0~200
    /// 3. 陣列查詢與設值皆為 O(1)，比 HashSet 的雜湊運算更快
    /// </para>
    /// <para>
    /// 時間複雜度：O(2^n * n)，與原版相同，但常數因子更小
    /// 空間複雜度：O(n)，遞迴堆疊深度 + 當前子序列長度（去重陣列使用 stackalloc 不計入堆積空間）
    /// </para>
    /// </summary>
    /// <param name="startIndex">當前搜尋的起始索引位置</param>
    /// <param name="nums">原始輸入陣列</param>
    /// <param name="current">當前正在建構的子序列</param>
    /// <param name="result">儲存所有有效結果的清單</param>
    public static void BacktrackOptimized(int startIndex, int[] nums, List<int> current, List<IList<int>> result)
    {
        if (current.Count >= 2)
        {
            result.Add(new List<int>(current));
        }

        if (startIndex == nums.Length)
        {
            return;
        }

        // 使用 stackalloc 在堆疊上分配 201 個 bool 的空間
        // 索引 0~200 對應數值 -100~100
        // 這比 HashSet 更快，因為避免了雜湊運算和堆積記憶體分配
        Span<bool> used = stackalloc bool[201];

        for (int index = startIndex; index < nums.Length; index++)
        {
            int mappedIndex = nums[index] + 100;

            // 去重：檢查當前層是否已選過此數值
            if (used[mappedIndex])
            {
                continue;
            }

            // 非遞減條件檢查
            if (current.Count > 0 && current[^1] > nums[index])
            {
                continue;
            }

            // 標記當前層已選取此數值
            used[mappedIndex] = true;

            current.Add(nums[index]);
            BacktrackOptimized(index + 1, nums, current, result);
            current.RemoveAt(current.Count - 1);
        }
    }

    #endregion

    #region 解法三：位元遮罩枚舉法（Bitmask Enumeration）

    /// <summary>
    /// 使用位元遮罩枚舉所有子序列，找出非遞減的子序列。
    /// <para>
    /// 解題思路：
    /// 1. 對於長度為 n 的陣列，共有 2^n 個子序列（包含空集合）
    /// 2. 使用整數的位元表示選取哪些元素：若第 i 位為 1，表示選取 nums[i]
    /// 3. 檢查每個子序列是否為非遞減序列
    /// 4. 使用 HashSet 去除重複的子序列
    /// </para>
    /// <para>
    /// 適用場景：
    /// - 當 n ≤ 15 時效能尚可接受
    /// - 程式碼簡潔，易於理解
    /// - 不需要遞迴，避免堆疊溢位風險
    /// </para>
    /// <example>
    /// <code>
    /// var program = new Program();
    /// int[] nums = [4, 6, 7, 7];
    /// var result = program.FindSubsequencesBitmask(nums);
    /// // 結果: [[4,6], [4,6,7], [4,6,7,7], [4,7], [4,7,7], [6,7], [6,7,7], [7,7]]
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="nums">輸入的整數陣列</param>
    /// <returns>所有不同的非遞減子序列清單</returns>
    /// <remarks>
    /// 時間複雜度：O(2^n * n)，需要枚舉所有子集並檢查每個子集
    /// 空間複雜度：O(2^n * n)，最壞情況下需要儲存所有子序列用於去重
    /// </remarks>
    public IList<IList<int>> FindSubsequencesBitmask(int[] nums)
    {
        int n = nums.Length;
        var result = new List<IList<int>>();

        // 使用字串形式的序列作為 key 來去重
        var seen = new HashSet<string>();

        // 枚舉所有 2^n 個子集（從 1 開始，跳過空集合）
        int totalSubsets = 1 << n;

        for (int mask = 1; mask < totalSubsets; mask++)
        {
            var subsequence = new List<int>();

            // 根據位元遮罩選取元素
            for (int i = 0; i < n; i++)
            {
                if ((mask & (1 << i)) != 0)
                {
                    subsequence.Add(nums[i]);
                }
            }

            // 檢查是否符合條件：長度 >= 2 且為非遞減序列
            if (subsequence.Count >= 2 && IsNonDecreasing(subsequence))
            {
                // 使用字串表示來去重
                string key = string.Join(",", subsequence);

                if (seen.Add(key))
                {
                    result.Add(subsequence);
                }
            }
        }

        return result;
    }

    /// <summary>
    /// 檢查序列是否為非遞減序列。
    /// </summary>
    /// <param name="sequence">要檢查的序列</param>
    /// <returns>若為非遞減序列則回傳 true，否則回傳 false</returns>
    private static bool IsNonDecreasing(List<int> sequence)
    {
        for (int i = 1; i < sequence.Count; i++)
        {
            if (sequence[i] < sequence[i - 1])
            {
                return false;
            }
        }

        return true;
    }

    #endregion
}
