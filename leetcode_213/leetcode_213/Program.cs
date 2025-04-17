namespace leetcode_213;

class Program
{
    /// <summary>
    /// 213. House Robber II
    /// https://leetcode.com/problems/house-robber-ii/description/?envType=problem-list-v2&envId=oizxjoit
    /// 213. 打家劫舍 II
    /// https://leetcode.cn/problems/house-robber-ii/description/
    /// 
    /// 題目描述：
    /// 你是一個專業的小偷，計劃沿著一條環形街道搶劫住戶。每間房屋都存放著特定金額的錢。
    /// 相鄰的房屋裝有相互連通的防盜系統，如果同時搶劫相鄰的兩間房屋會自動報警。
    /// 由於街道是環形的，第一間房屋和最後一間房屋被認為是相鄰的。
    /// 
    /// 解題思路：
    /// 1. 環形街道的特點是第一間和最後一間房屋相鄰，不能同時搶劫
    /// 2. 將問題拆分為兩個子問題：
    ///    - 搶劫第1間到倒數第2間房屋（不包含最後一間）
    ///    - 搶劫第2間到最後一間房屋（不包含第一間）
    /// 3. 對每個子問題使用動態規劃求解，最後取兩個子問題結果的最大值
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("開始執行 LeetCode 213：打家劫舍 II 測試");
        
        // 建立解題實例
        Program solution = new Program();
        
        // 測試用例1：[2,3,2] -> 預期結果：3
        int[] test1 = { 2, 3, 2 };
        int result1 = solution.Rob(test1);
        Console.WriteLine($"測試用例1: [{string.Join(", ", test1)}]");
        Console.WriteLine($"結果: {result1}");
        Console.WriteLine($"預期: 3");
        Console.WriteLine($"驗證: {(result1 == 3 ? "通過" : "失敗")}");
        
        // 測試用例2：[1,2,3,1] -> 預期結果：4
        int[] test2 = { 1, 2, 3, 1 };
        int result2 = solution.Rob(test2);
        Console.WriteLine($"\n測試用例2: [{string.Join(", ", test2)}]");
        Console.WriteLine($"結果: {result2}");
        Console.WriteLine($"預期: 4");
        Console.WriteLine($"驗證: {(result2 == 4 ? "通過" : "失敗")}");
        
        // 測試用例3：[1,2,3] -> 預期結果：3
        int[] test3 = { 1, 2, 3 };
        int result3 = solution.Rob(test3);
        Console.WriteLine($"\n測試用例3: [{string.Join(", ", test3)}]");
        Console.WriteLine($"結果: {result3}");
        Console.WriteLine($"預期: 3");
        Console.WriteLine($"驗證: {(result3 == 3 ? "通過" : "失敗")}");
        
        // 測試極端情況
        int[] test4 = { 200 };
        int result4 = solution.Rob(test4);
        Console.WriteLine($"\n測試用例4 (單個元素): [{string.Join(", ", test4)}]");
        Console.WriteLine($"結果: {result4}");
        Console.WriteLine($"預期: 200");
        Console.WriteLine($"驗證: {(result4 == 200 ? "通過" : "失敗")}");
        
        // 測試較長數組
        int[] test5 = { 2, 7, 9, 3, 1, 8, 5, 4 };
        int result5 = solution.Rob(test5);
        Console.WriteLine($"\n測試用例5 (較長數組): [{string.Join(", ", test5)}]");
        Console.WriteLine($"結果: {result5}");
        Console.WriteLine($"說明: 由於長度較長，不直接驗證結果的正確性");
        
        Console.WriteLine("\n所有測試完成");
    }

    /// <summary>
    /// 解決 House Robber II 問題
    /// 由於房屋排列成環形，第一個房屋和最後一個房屋相鄰，不能同時搶劫
    /// </summary>
    /// <param name="nums">每間房屋的金額陣列</param>
    /// <returns>能搶劫到的最大金額</returns>
    public int Rob(int[] nums)
    {
        int n = nums.Length;
        
        // 特殊情況處理
        if (n == 0) return 0;
        if (n == 1) return nums[0];
        
        // 分兩種情況：
        // 1. 搶劫索引 0 到 n-2 的房屋 (不包含最後一間)
        // 2. 搶劫索引 1 到 n-1 的房屋 (不包含第一間)
        return Math.Max(RobRange(nums, 0, n - 2), RobRange(nums, 1, n - 1));
    }
    
    /// <summary>
    /// 針對特定範圍的房屋計算最大搶劫金額
    /// </summary>
    /// <param name="nums">房屋金額陣列</param>
    /// <param name="start">起始索引</param>
    /// <param name="end">結束索引</param>
    /// <returns>此範圍內最大搶劫金額</returns>
    private int RobRange(int[] nums, int start, int end)
    {
        // 計算當前範圍內房屋的數量
        int n = end - start + 1;
        
        // 特殊情況處理：如果範圍內沒有房屋，則返回 0
        if (n == 0) return 0;
        // 特殊情況處理：如果範圍內只有一間房屋，直接返回該房屋的金額
        if (n == 1) return nums[start];
        
        // 初始化動態規劃變數
        // pre 代表搶劫第一間房屋的最大金額
        int pre = nums[start];
        // next 代表搶劫前兩間房屋中的最大金額
        int next = Math.Max(nums[start], nums[start + 1]);
        // result 存儲當前計算的最大金額，初始化為 next
        int result = next;
        
        // 從第三間房屋開始迭代計算 (對應索引 start+2)
        for (int i = start + 2; i <= end; i++)
        {
            // 對於第 i 間房屋，有兩種選擇：
            // 1. 搶劫第 i 間房屋，則不能搶劫第 i-1 間，因此金額為 pre + nums[i]
            // 2. 不搶劫第 i 間房屋，則金額為 next (前 i-1 間房屋的最大金額)
            // 取兩種情況的最大值
            result = Math.Max(pre + nums[i], next);
            
            // 更新 pre 為前一輪的 next，表示前 i-1 間房屋的最大金額
            pre = next;
            // 更新 next 為當前計算的 result，表示前 i 間房屋的最大金額
            next = result;
        }
        
        // 返回計算的最大搶劫金額
        return result;
    }
}
