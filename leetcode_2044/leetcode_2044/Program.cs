namespace leetcode_2044;

class Program
{
    /// <summary>
    /// 2044. Count Number of Maximum Bitwise-OR Subsets
    /// https://leetcode.com/problems/count-number-of-maximum-bitwise-or-subsets/description/?envType=daily-question&envId=2025-07-28
    /// 2044. 统计按位或能得到最大值的子集数目
    /// https://leetcode.cn/problems/count-number-of-maximum-bitwise-or-subsets/description/?envType=daily-question&envId=2025-07-28
    /// 
    /// 題目描述：
    /// 給定一個整數陣列 nums，請找出 nums 的所有子集（非空），其按位或（Bitwise OR）能達到最大值，並返回能達到最大按位或的不同子集數量。
    /// 
    /// 陣列 a 是陣列 b 的子集，若 a 可以透過刪除 b 中的某些（可能為零個）元素得到。
    /// 若選取元素的索引不同，則兩個子集被視為不同。
    /// 陣列 a 的按位或為 a[0] OR a[1] OR ... OR a[a.length - 1]（0-indexed）。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料設計：LeetCode 範例與自訂案例
        int[] nums1 = {3, 1}; // LeetCode 範例1，最大 OR = 3
        int[] nums2 = {2, 2, 2}; // LeetCode 範例2，最大 OR = 2
        int[] nums3 = {1, 2, 4}; // LeetCode 範例3，最大 OR = 7
        int[] nums4 = {0, 0, 0}; // 全零邊界案例，最大 OR = 0
        int[] nums5 = {7, 7, 7}; // 全相同且最大 OR = 7
        int[] nums6 = {1, 2, 3, 4, 5}; // 較大測資

        var program = new Program();

        // 測試所有方法
        TestAndPrint(program, nums1, "nums1");
        TestAndPrint(program, nums2, "nums2");
        TestAndPrint(program, nums3, "nums3");
        TestAndPrint(program, nums4, "nums4");
        TestAndPrint(program, nums5, "nums5");
        TestAndPrint(program, nums6, "nums6");
    }

    /// <summary>
    /// 測試所有解法並印出結果
    /// </summary>
    /// <param name="program">Program 實例</param>
    /// <param name="nums">測試資料</param>
    /// <param name="label">資料標籤</param>
    static void TestAndPrint(Program program, int[] nums, string label)
    {
        Console.WriteLine($"--- {label} 測試 ---");
        Console.WriteLine($"輸入: [{string.Join(", ", nums)}]");
        Console.WriteLine($"CountMaxOrSubsets: {program.CountMaxOrSubsets(nums)}");
        Console.WriteLine($"CountMaxOrSubsetsOptimized: {program.CountMaxOrSubsetsOptimized(nums)}");
        Console.WriteLine($"CountMaxOrSubsetsDP: {program.CountMaxOrSubsetsDP(nums)}");
        Console.WriteLine();
    }

    /// <summary>
    /// 方法一：使用位遮罩技術計算能達到最大按位或值的子集數量
    /// 
    /// 解題思路：
    /// 1. 首先計算所有元素的按位或，這就是可能的最大值（因為按位或只會增加或保持位元）
    /// 2. 使用位遮罩（Bit Masking）技術枚舉所有可能的非空子集
    /// 3. 對每個子集計算其按位或值，若等於最大值則計數+1
    /// 
    /// 時間複雜度：O(n * 2^n)，其中 n 是陣列長度
    /// 空間複雜度：O(1)
    /// </summary>
    /// <param name="nums">輸入的整數陣列</param>
    /// <returns>能達到最大按位或值的不同子集數量</returns>
    public int CountMaxOrSubsets(int[] nums)
    {
        // 步驟1：計算所有元素的按位或，得到可能的最大值
        // 因為按位或運算只會讓結果變大或保持不變，所以所有元素的OR就是最大值
        int maxOr = 0;
        foreach (int num in nums)
        {
            maxOr |= num;
        }

        // 步驟2：枚舉所有可能的非空子集並計算符合條件的數量
        int count = 0;
        int n = nums.Length;
        
        // 使用位遮罩技術：從1到2^n-1，每個數字代表一種子集選擇方式
        // i的二進位表示法中，第j位為1表示選擇nums[j]，為0表示不選
        for (int i = 1; i < (1 << n); i++)
        {
            int currOr = 0;
            
            // 檢查當前遮罩i對應的子集
            for (int j = 0; j < n; j++)
            {
                // 檢查第j位是否為1（即是否選擇nums[j]）
                if ((i & (1 << j)) != 0)
                {
                    currOr |= nums[j];  // 將選中的元素加入按位或計算
                }
            }
            
            // 如果當前子集的按位或值等於最大值，計數器加1
            if (currOr == maxOr)
            {
                count++;
            }
        }
        
        return count;
    }

    /// <summary>
    /// 方法二：使用回溯法（Backtracking）優化計算能達到最大按位或值的子集數量
    /// 
    /// 解題思路：
    /// 1. 首先計算所有元素的按位或，得到最大值
    /// 2. 使用深度優先搜尋（DFS）+ 回溯法遍歷所有子集
    /// 3. 透過剪枝優化：當當前OR值已經等於最大值時，可以提前結束某些分支
    /// 
    /// 優化點：
    /// - 避免重複計算：每次遞迴直接傳遞當前OR值
    /// - 剪枝策略：當已達到最大值時可進行優化
    /// - 空間效率：使用遞迴堆疊而非額外陣列
    /// 
    /// 時間複雜度：O(2^n)，但實際運行通常更快（因為剪枝）
    /// 空間複雜度：O(n)，遞迴深度
    /// </summary>
    /// <param name="nums">輸入的整數陣列</param>
    /// <returns>能達到最大按位或值的不同子集數量</returns>
    public int CountMaxOrSubsetsOptimized(int[] nums)
    {
        // 計算最大可能的按位或值
        int maxOr = 0;
        foreach (int num in nums)
        {
            maxOr |= num;
        }

        // 使用回溯法計算符合條件的子集數量
        return Backtrack(nums, 0, 0, maxOr);
    }

    /// <summary>
    /// 回溯法的遞迴輔助函式
    /// </summary>
    /// <param name="nums">原始陣列</param>
    /// <param name="index">當前處理的元素索引</param>
    /// <param name="currentOr">當前子集的按位或值</param>
    /// <param name="maxOr">目標最大按位或值</param>
    /// <param name="hasElement">當前子集是否包含至少一個元素</param>
    /// <returns>從當前狀態開始能形成的符合條件的子集數量</returns>
    private int Backtrack(int[] nums, int index, int currentOr, int maxOr, bool hasElement = false)
    {
        // 基本情況：已經處理完所有元素
        if (index == nums.Length)
        {
            // 如果當前OR值等於最大值且不是空集合，則計數
            return currentOr == maxOr && hasElement ? 1 : 0;
        }

        // 選擇1：不選擇當前元素
        int count = Backtrack(nums, index + 1, currentOr, maxOr, hasElement);

        // 選擇2：選擇當前元素
        int newOr = currentOr | nums[index];
        count += Backtrack(nums, index + 1, newOr, maxOr, true);

        return count;
    }

    /// <summary>
    /// 方法三：使用動態規劃（DP）+ 位元操作優化
    /// 
    /// 解題思路：
    /// 使用Map記錄每個可能的OR值對應的子集數量，逐個添加元素更新狀態
    /// 
    /// 優化點：
    /// - 避免重複計算相同OR值的子集
    /// - 空間效率：只記錄實際出現的OR值
    /// - 時間效率：避免枚舉所有可能子集
    /// 
    /// 時間複雜度：O(n * k)，其中k是不同OR值的數量（通常遠小於2^n）
    /// 空間複雜度：O(k)
    /// </summary>
    /// <param name="nums">輸入的整數陣列</param>
    /// <returns>能達到最大按位或值的不同子集數量</returns>
    public int CountMaxOrSubsetsDP(int[] nums)
    {
        // 使用字典記錄每個OR值對應的子集數量（不包含空集合）
        var orCount = new Dictionary<int, int>();

        foreach (int num in nums)
        {
            var nextStateOrCount = new Dictionary<int, int>(orCount);
            
            // 加入只包含當前元素的子集
            if (nextStateOrCount.TryGetValue(num, out int singleCount))
            {
                nextStateOrCount[num] = singleCount + 1;
            }
            else
            {
                nextStateOrCount[num] = 1;
            }
            
            // 對於每個已存在的OR值，嘗試加入當前數字
            foreach (var kvp in orCount)
            {
                int currentOr = kvp.Key;
                int count = kvp.Value;
                int newOr = currentOr | num;
                
                // 使用 TryGetValue 提高效能和可讀性
                if (nextStateOrCount.TryGetValue(newOr, out int existingCount))
                {
                    nextStateOrCount[newOr] = existingCount + count;
                }
                else
                {
                    nextStateOrCount[newOr] = count;
                }
            }
            
            orCount = nextStateOrCount;
        }

        // 找到最大OR值並直接返回對應的計數
        int maxOr = orCount.Keys.Max();
        
        return orCount[maxOr];
    }
}
