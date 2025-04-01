namespace leetcode_152;

class Program
{
    /// <summary>
    /// 152. Maximum Product Subarray
    /// https://leetcode.com/problems/maximum-product-subarray/description/?envType=problem-list-v2&envId=oizxjoit
    /// 152. 乘积最大子数组
    /// https://leetcode.cn/problems/maximum-product-subarray/description/
    /// 
    /// 解題概念出發點：
    /// 1. 連續子陣列乘積最大值問題的特殊性：
    ///    - 與一般最大子陣列和的問題不同，乘積可能因為負數而改變最大最小關係
    ///    - 兩個負數相乘會變成正數，可能產生更大的結果
    ///    - 零會重置所有計算
    /// 
    /// 2. 解決方案選擇理由：
    ///    - 使用動態規劃而非暴力法，可以優化時間複雜度
    ///    - 同時追蹤最大值和最小值，因為負數會使兩者互換
    ///    - 使用滾動變數代替陣列，優化空間複雜度
    /// 
    /// 3. 兩種解法比較：
    ///    動態規劃解法 (MaxProduct)：
    ///    優點：
    ///    - 思路直觀，易於理解和實現
    ///    - 程式碼結構清晰
    ///    缺點：
    ///    - 需要額外的變數來追蹤最大和最小值
    ///    - 需要額外的邏輯來處理負數情況
    ///    
    ///    雙指針解法 (MaxProductTwoPointers)：
    ///    優點：
    ///    - 程式碼更簡潔
    ///    - 不需要額外的空間來存儲最大最小值
    ///    - 自然處理負數情況，無需特別判斷
    ///    缺點：
    ///    - 思路較不直觀
    ///    - 在處理複雜測試案例時可能較難除錯
    /// 
    /// 負數 * 大  = 小(負越多越小)
    /// 負數 * 小  = 大(負越小越大)
    /// 正數 * 大  = 大
    /// 正數 * 小  = 小
    /// 
    /// </summary>
    static void Main(string[] args)
    {
        Program solution = new Program();
        
        // 測試案例1：包含正數的情況
        int[] test1 = new int[] { 2, 3, -2, 4 };
        Console.WriteLine($"Test Case 1: Input: [{string.Join(", ", test1)}]");
        Console.WriteLine($"Output: {solution.MaxProduct(test1)}"); // 預期輸出：6
        
        // 測試案例2：包含負數的情況
        int[] test2 = new int[] { -2, 0, -1 };
        Console.WriteLine($"Test Case 2: Input: [{string.Join(", ", test2)}]");
        Console.WriteLine($"Output: {solution.MaxProduct(test2)}"); // 預期輸出：0
        
        // 測試案例3：全負數的情況
        int[] test3 = new int[] { -2, -3, -4 };
        Console.WriteLine($"Test Case 3: Input: [{string.Join(", ", test3)}]");
        Console.WriteLine($"Output: {solution.MaxProduct(test3)}"); // 預期輸出：12
        
        // 測試案例4：包含零的情況
        int[] test4 = new int[] { -2, 3, 0, -4 };
        Console.WriteLine($"Test Case 4: Input: [{string.Join(", ", test4)}]");
        Console.WriteLine($"Output: {solution.MaxProduct(test4)}"); // 預期輸出：3

        Console.WriteLine("\n=== Testing MaxProductTwoPointers Method ===");
    
        // 測試案例1：包含正數的情況
        Console.WriteLine($"Test Case 1 (Two Pointers): Input: [{string.Join(", ", test1)}]");
        Console.WriteLine($"Output: {solution.MaxProductTwoPointers(test1)}"); // 預期輸出：6
        
        // 測試案例2：包含負數的情況
        Console.WriteLine($"Test Case 2 (Two Pointers): Input: [{string.Join(", ", test2)}]");
        Console.WriteLine($"Output: {solution.MaxProductTwoPointers(test2)}"); // 預期輸出：0
        
        // 測試案例3：全負數的情況
        Console.WriteLine($"Test Case 3 (Two Pointers): Input: [{string.Join(", ", test3)}]");
        Console.WriteLine($"Output: {solution.MaxProductTwoPointers(test3)}"); // 預期輸出：12
        
        // 測試案例4：包含零的情況
        Console.WriteLine($"Test Case 4 (Two Pointers): Input: [{string.Join(", ", test4)}]");
        Console.WriteLine($"Output: {solution.MaxProductTwoPointers(test4)}"); // 預期輸出：3
        
        // 新增測試案例5：空陣列的情況
        int[] test5 = new int[] { };
        Console.WriteLine($"Test Case 5 (Two Pointers): Input: []");
        Console.WriteLine($"Output: {solution.MaxProductTwoPointers(test5)}"); // 預期輸出：0
    }

    /// <summary>
    /// 解題思路：
    /// 1. 使用動態規劃，維護兩個變數 max 和 min
    /// 2. max 儲存到目前為止的最大乘積，min 儲存到目前為止的最小乘積
    /// 3. 因為負數乘以負數會變成正數，所以需要同時追蹤最小值
    /// 4. 當遇到負數時，最大值和最小值會互換，因為負數會使最大變最小，最小變最大
    /// 時間複雜度：O(n)，空間複雜度：O(1)
    /// </summary>
    /// <param name="nums">輸入整數陣列</param>
    /// <returns>返回連續子陣列的最大乘積</returns>
    public int MaxProduct(int[] nums)
    {
        // 初始化：設定第一個元素為初始的最大值、最小值和結果
        int max = nums[0];
        int min = nums[0];
        int result = nums[0];

        // 從第二個元素開始遍歷陣列
        for (int i = 1; i < nums.Length; i++)
        {
            // 遇到負數時，最大值和最小值互換
            // 因為負數會使最大變最小，最小變最大
            if (nums[i] < 0)
            {
                int temp = max;
                max = min;
                min = temp;
            }

            // 計算當前位置的最大值
            // 可能是當前數字本身，或是之前的最大值乘以當前數字
            max = Math.Max(nums[i], max * nums[i]);

            // 計算當前位置的最小值
            // 可能是當前數字本身，或是之前的最小值乘以當前數字
            min = Math.Min(nums[i], min * nums[i]);

            // 更新全局最大值
            result = Math.Max(result, max);
        }

        return result;
    }

    /// <summary>
    /// 使用雙指針法解決乘積最大子數組問題
    /// 
    /// 解題概念：
    /// 1. 使用兩個指針分別從左右方向遍歷陣列
    /// 2. 同時計算左右兩個方向的乘積
    /// 3. 這種方法可以巧妙處理負數的情況，因為：
    ///    - 如果有偶數個負數，從左到右或從右到左的乘積會包含全部數字
    ///    - 如果有奇數個負數，從左到右和從右到左會分別找到不同的子陣列
    /// 
    /// 優勢：
    /// 1. 不需要額外的空間來存儲最大和最小值
    /// 2. 程式碼較為簡潔
    /// 3. 可以有效處理包含零的情況
    /// 
    /// 時間複雜度：O(n)，空間複雜度：O(1)
    /// 
    /// 不需要類似方法一 遇到負數時交換最大最小值的邏輯
    /// 因為雙指針法自然處理了這種情況
    /// </summary>
    /// <param name="nums">輸入整數陣列</param>
    /// <returns>返回連續子陣列的最大乘積</returns>
    public int MaxProductTwoPointers(int[] nums)
    {
        if (nums == null || nums.Length == 0) 
        {
            return 0; // 處理空陣列的情況
        }
        
        // 初始化最大乘積為第一個元素
        // 這樣可以處理陣列中只有一個元素的情況
        int maxProduct = nums[0];
        int n = nums.Length;
        
        // 初始化左右指針的乘積
        // 因為是乘法所以初始值設為 1
        // 這樣可以避免乘積為 0 的情況
        int leftProduct = 1;
        int rightProduct = 1;
        
        for (int i = 0; i < n; i++)
        {
            // 從左向右累積乘積
            leftProduct *= nums[i];
            // 從右向左累積乘積（同時進行）
            rightProduct *= nums[n - 1 - i];
            
            // 在每一步更新最大值，取左右指針乘積的較大者
            maxProduct = Math.Max(maxProduct, Math.Max(leftProduct, rightProduct));
            
            // 遇到 0 時重置乘積
            // 這樣可以處理陣列中包含 0 的情況，將陣列分成多個子陣列處理
            if (leftProduct == 0) 
            {
                leftProduct = 1; // 重置左乘積
            }
            if (rightProduct == 0) 
            {
                rightProduct = 1; // 重置右乘積
            }
        }
        
        return maxProduct;
    }
}
