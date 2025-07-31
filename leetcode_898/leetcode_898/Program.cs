using System.Diagnostics;

namespace leetcode_898;

class Program
{
    /// <summary>
    /// 898. Bitwise ORs of Subarrays
    /// https://leetcode.com/problems/bitwise-ors-of-subarrays/description/?envType=daily-question&envId=2025-07-31
    /// 2683. 相鄰值的按位異或
    /// https://leetcode.cn/problems/neighboring-bitwise-xor/description/?envType=daily-question&envId=2025-07-31
    /// 
    /// 題目描述：
    /// 給定一個整數陣列 arr，回傳所有非空子陣列的按位或運算的不同結果數量。
    /// 
    /// 子陣列的按位或運算是該子陣列中每個整數的按位或運算。
    /// 單一整數的子陣列的按位或運算就是該整數本身。
    /// 
    /// 子陣列是陣列中連續非空的元素序列。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();
        
        // 測試案例 1
        int[] arr1 = {0, 1, 1, 2};
        int result1 = solution.SubarrayBitwiseORs(arr1);
        Console.WriteLine($"測試案例 1: [{string.Join(", ", arr1)}]");
        Console.WriteLine($"結果: {result1} (預期: 3)");
        solution.ShowAllSubarraysWithOR(arr1);
        Console.WriteLine();
        
        // 測試案例 2
        int[] arr2 = {1, 1, 2};
        int result2 = solution.SubarrayBitwiseORs(arr2);
        Console.WriteLine($"測試案例 2: [{string.Join(", ", arr2)}]");
        Console.WriteLine($"結果: {result2} (預期: 3)\n");
        
        // 測試案例 3
        int[] arr3 = {1, 2, 4};
        int result3 = solution.SubarrayBitwiseORs(arr3);
        Console.WriteLine($"測試案例 3: [{string.Join(", ", arr3)}]");
        Console.WriteLine($"結果: {result3} (預期: 6)\n");
        
        // 比較兩種解法的效能
        Console.WriteLine("=== 效能比較 ===");
        int[] largeArr = Enumerable.Range(1, 1000).ToArray();
        
        var stopwatch = Stopwatch.StartNew();
        int resultBrute = solution.SubarrayBitwiseORs(largeArr);
        stopwatch.Stop();
        Console.WriteLine($"暴力解法耗時: {stopwatch.ElapsedMilliseconds} ms, 結果: {resultBrute}");
        
        stopwatch.Restart();
        int resultOptimized = solution.SubarrayBitwiseORsOptimized(largeArr);
        stopwatch.Stop();
        Console.WriteLine($"優化解法耗時: {stopwatch.ElapsedMilliseconds} ms, 結果: {resultOptimized}");
    }

    /// <summary>
    /// 計算所有非空子陣列的按位或運算的不同結果數量。
    /// 
    /// 解題思路：
    /// 1. 使用雙重迴圈枚舉所有可能的子陣列
    /// 2. 外層迴圈確定子陣列的起始位置 i
    /// 3. 內層迴圈確定子陣列的結束位置 j，形成子陣列 [i, j]
    /// 4. 對於每個子陣列，計算其按位或運算結果
    /// 5. 使用 HashSet 自動去重，最終返回不同結果的數量
    /// 
    /// 時間複雜度：O(n²)，其中 n 是陣列長度
    /// 空間複雜度：O(k)，其中 k 是不同按位或結果的數量
    /// </summary>
    /// <param name="arr">輸入的整數陣列</param>
    /// <returns>所有非空子陣列按位或運算的不同結果數量</returns>
    /// <example>
    /// <code>
    /// int[] arr = {0, 1, 1, 2};
    /// int result = SubarrayBitwiseORs(arr); // 返回 3
    /// 所有子陣列的按位或結果：
    /// [0] = 0, [1] = 1, [1] = 1, [2] = 2
    /// [0,1] = 1, [1,1] = 1, [1,2] = 3
    /// [0,1,1] = 1, [1,1,2] = 3
    /// [0,1,1,2] = 3
    /// 不同的結果：{0, 1, 2, 3}，共 3 個
    /// </code>
    /// </example>
    public int SubarrayBitwiseORs(int[] arr)
    {
        // 使用 HashSet 儲存所有不同的按位或結果，自動去重
        HashSet<int> result = new HashSet<int>();
        
        // 外層迴圈：枚舉所有可能的子陣列起始位置
        for (int i = 0; i < arr.Length; i++)
        {
            // 用於儲存當前子陣列的按位或結果，在內層迴圈中宣告並初始化
            int current = 0;
            
            // 內層迴圈：枚舉以位置 i 為起點的所有子陣列的結束位置
            for (int j = i; j < arr.Length; j++)
            {
                // 將當前元素 arr[j] 與之前的結果進行按位或運算
                // 這樣就得到了子陣列 [i, j] 的按位或結果
                current |= arr[j];
                
                // 將結果加入 HashSet，如果已存在則自動忽略（去重）
                result.Add(current);
            }
        }
        
        // 返回所有不同按位或結果的數量
        return result.Count;
    }

    /// <summary>
    /// 優化版本：計算所有非空子陣列的按位或運算的不同結果數量。
    /// 
    /// 【重要】本質上仍是雙層迴圈，但內層迴圈的大小受到按位或運算特性限制！
    /// 
    /// 優化思路：
    /// 1. 利用按位或運算的特性：對於每個新位置 i，只需要考慮以 i 結尾的所有可能結果
    /// 2. 維護一個集合，儲存以當前位置結尾的所有可能按位或結果
    /// 3. 按位或運算具有單調性，不同結果的數量有限（最多約 30 個）
    /// 4. 每次迭代時，將前一步的所有結果與當前元素做按位或運算
    /// 
    /// 為什麼內層迴圈變小了？
    /// - 暴力解法：內層迴圈大小 = n（陣列長度）
    /// - 優化解法：內層迴圈大小 = currentResults.Count ≤ 32（按位或結果的數量）
    /// - 按位或運算的單調性確保了不同結果數量的上界
    /// 
    /// 時間複雜度：O(n * log(max(arr)))，其中 log(max(arr)) ≈ 30
    /// 空間複雜度：O(log(max(arr)))
    /// 
    /// 優化解法的關鍵機制解析：
    /// 1. 外層迴圈：遍歷陣列每個元素 O(n)
    /// 2. 內層迴圈：只處理「不重複的中間結果」，而非所有子陣列
    /// 3. HashSet 自動去重：避免重複計算相同的按位或結果
    /// </summary>
    /// <param name="arr">輸入的整數陣列</param>
    /// <returns>所有非空子陣列按位或運算的不同結果數量</returns>
    public int SubarrayBitwiseORsOptimized(int[] arr)
    {
        if (arr is null || arr.Length == 0)
            return 0;
            
        // 儲存所有不同的按位或結果
        HashSet<int> allResults = new HashSet<int>();
        
        // 儲存以當前位置結尾的所有可能按位或結果
        HashSet<int> currentResults = new HashSet<int>();
        
        foreach (int num in arr)  // 外層迴圈：遍歷陣列中的每個元素，O(n)
        {
            // 建立新的結果集合，用於儲存以當前元素結尾的所有可能結果
            HashSet<int> newResults = new HashSet<int>();
            
            // 當前元素本身就是一個結果（單元素子陣列）
            newResults.Add(num);

            // 內層迴圈：將前一步的所有結果與當前元素做按位或運算
            // 關鍵：currentResults.Count ≤ 32，而不是 O(n)
            // 【關鍵優化】：內層迴圈處理的是「去重後的中間結果」, 而不是所有可能的起始位置！
            foreach (int prevResult in currentResults)  // 內層迴圈：O(log(max))
            {
                // 將之前的結果與當前元素做按位或運算
                // 這一步相當於「延伸所有以前一個位置結尾的子陣列」
                newResults.Add(prevResult | num);
            }
            
            // 將所有新結果加入總結果集
            allResults.UnionWith(newResults);
            
            // 更新當前結果集，為下一輪迭代做準備
            currentResults = newResults;
        }
        
        return allResults.Count;
    }

    /// <summary>
    /// 輔助方法：展示所有子陣列及其按位或結果，用於理解題目
    /// </summary>
    /// <param name="arr">輸入的整數陣列</param>
    public void ShowAllSubarraysWithOR(int[] arr)
    {
        Console.WriteLine("=== 所有子陣列及其按位或結果 ===");
        HashSet<int> allResults = new HashSet<int>();
        
        for (int i = 0; i < arr.Length; i++)
        {
            int current = 0;
            for (int j = i; j < arr.Length; j++)
            {
                current |= arr[j];
                
                // 顯示子陣列和結果
                string subarray = string.Join(", ", arr[i..(j+1)]);
                Console.WriteLine($"[{subarray}] → {current}");
                
                allResults.Add(current);
            }
        }
        
        Console.WriteLine($"\n不同的結果：{{{string.Join(", ", allResults.OrderBy(x => x))}}}");
        Console.WriteLine($"總共 {allResults.Count} 個不同結果");
    }
}
