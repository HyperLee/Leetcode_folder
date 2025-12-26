namespace leetcode_047;

class Program
{
    /// <summary>
    /// 47. Permutations II
    /// https://leetcode.com/problems/permutations-ii/description/
    /// 47. 全排列 II（簡體中文）
    /// https://leetcode.cn/problems/permutations-ii/description/
    /// 繁體中文：
    /// 給定一組可能包含重複數字的整數陣列 `nums`，請回傳所有可能的不重複排列，回傳順序不限。
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        Program program = new Program();

        // 測試範例 1：包含重複元素的陣列
        int[] nums1 = [1, 1, 2];
        Console.WriteLine("輸入: nums = [1, 1, 2]");
        Console.WriteLine("輸出:");
        IList<IList<int>> result1 = program.PermuteUnique(nums1);
        PrintResult(result1);
        // 預期輸出: [[1,1,2], [1,2,1], [2,1,1]]

        Console.WriteLine();

        // 測試範例 2：包含多個重複元素的陣列
        int[] nums2 = [1, 2, 3];
        Console.WriteLine("輸入: nums = [1, 2, 3]");
        Console.WriteLine("輸出:");
        IList<IList<int>> result2 = program.PermuteUnique(nums2);
        PrintResult(result2);
        // 預期輸出: [[1,2,3], [1,3,2], [2,1,3], [2,3,1], [3,1,2], [3,2,1]]

        Console.WriteLine();

        // 測試範例 3：全部相同元素的陣列
        int[] nums3 = [2, 2, 2];
        Console.WriteLine("輸入: nums = [2, 2, 2]");
        Console.WriteLine("輸出:");
        IList<IList<int>> result3 = program.PermuteUnique(nums3);
        PrintResult(result3);
        // 預期輸出: [[2,2,2]]
    }

    /// <summary>
    /// 輸出結果的輔助方法
    /// </summary>
    /// <param name="result">排列結果集合</param>
    private static void PrintResult(IList<IList<int>> result)
    {
        Console.WriteLine("[");
        foreach (var list in result)
        {
            Console.WriteLine($"  [{string.Join(", ", list)}]");
        }
        Console.WriteLine("]");
    }

    /// <summary>
    /// 解題思路：搜索回溯（Backtracking）+ 剪枝去重
    /// 
    /// 核心想法：
    /// 1. 此題與 LeetCode 46 (Permutations) 的差異在於輸入陣列可能包含重複元素
    /// 2. 為了避免產生重複的排列，需要進行「剪枝」操作
    /// 3. 先將陣列排序，使相同元素相鄰，便於後續判斷是否跳過
    /// 
    /// 去重條件說明：
    /// - 當 nums[i] == nums[i-1] 且 used[i-1] == false 時跳過
    /// - 這表示前一個相同元素還未被使用，代表我們應該先使用前一個元素
    /// - 確保相同元素只會按照固定順序出現，避免重複排列
    /// 
    /// 時間複雜度：O(n × n!)，n 為陣列長度
    /// 空間複雜度：O(n)，遞迴堆疊深度
    /// </summary>
    /// <param name="nums">可能包含重複數字的整數陣列</param>
    /// <returns>所有不重複的排列組合</returns>
    public IList<IList<int>> PermuteUnique(int[] nums)
    {
        // 初始化結果集合
        List<IList<int>> res = new List<IList<int>>();

        // 排序陣列，讓相同元素相鄰，方便後續去重判斷
        Array.Sort(nums);

        // 建立使用標記陣列，追蹤每個元素是否已被選入當前排列
        bool[] used = new bool[nums.Length];

        // 開始回溯搜索
        Backtracking(nums, new List<int>(), res, used);

        return res;
    }

    /// <summary>
    /// 回溯演算法核心函式
    /// 
    /// 回溯法的基本框架：
    /// 1. 終止條件：當前排列長度等於陣列長度，表示找到一個完整排列
    /// 2. 遍歷選擇：嘗試將每個未使用的元素加入當前排列
    /// 3. 剪枝去重：跳過會產生重複排列的分支
    /// 4. 遞迴探索：繼續建構下一個位置
    /// 5. 回溯還原：撤銷選擇，嘗試其他可能性
    /// 
    /// 剪枝條件詳解：
    /// - used[i] == true：該元素已在當前排列中，跳過
    /// - nums[i] == nums[i-1] 且 used[i-1] == false：
    ///   表示前一個相同元素在「同一層」已經被選過並回溯了
    ///   若再選當前元素，會產生重複的排列，故跳過
    /// </summary>
    /// <param name="nums">已排序的整數陣列</param>
    /// <param name="list">當前正在建構的排列</param>
    /// <param name="res">儲存所有有效排列的結果集合</param>
    /// <param name="used">標記陣列，記錄每個元素是否已被使用</param>
    private void Backtracking(int[] nums, List<int> list, List<IList<int>> res, bool[] used)
    {
        // 終止條件：當前排列長度等於陣列長度，找到一個完整的排列
        if (list.Count == nums.Length)
        {
            // 複製當前排列加入結果集（必須建立新的 List，避免引用問題）
            res.Add(new List<int>(list));
            return;
        }

        // 遍歷所有元素，嘗試將每個元素加入當前排列
        for (int i = 0; i < nums.Length; i++)
        {
            // 剪枝條件 1：該元素已被使用，跳過
            if (used[i])
            {
                continue;
            }

            // 剪枝條件 2：去重邏輯
            // 當前元素與前一個元素相同，且前一個元素未被使用（表示已回溯）
            // 這代表在同一層遞迴中，相同的元素已經處理過，跳過以避免重複
            if (i > 0 && nums[i] == nums[i - 1] && !used[i - 1])
            {
                continue;
            }

            // 做選擇：將當前元素加入排列
            list.Add(nums[i]);
            used[i] = true;

            // 遞迴：繼續建構下一個位置的排列
            Backtracking(nums, list, res, used);

            // 回溯：撤銷選擇，嘗試其他可能性
            list.RemoveAt(list.Count - 1);
            used[i] = false;
        }
    }
}
