using System.Diagnostics;

namespace leetcode_898;

class Program
{
    /// <summary>
    /// 898. Bitwise ORs of Subarrays
    /// https://leetcode.com/problems/bitwise-ors-of-subarrays/description/?envType=daily-question&envId=2025-07-31
    /// 898. 子数组按位或操作
    /// https://leetcode.cn/problems/bitwise-ors-of-subarrays/description/
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
        
        // 演示集合方法的執行過程
        Console.WriteLine("=== 集合方法演算法演示 ===");
        solution.DemonstrateSetMethod(arr1);
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
        
        // 測試初始化方式的差異
        Console.WriteLine("=== 初始化方式差異分析 ===");
        solution.AnalyzeInitializationDifference(arr1);
        Console.WriteLine();
        
        solution.TestInitializationImpact();
        Console.WriteLine();
        
        // 比較三種解法的效能
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
        
        stopwatch.Restart();
        int resultJavaStyle = solution.SubarrayBitwiseORsJavaStyle(largeArr);
        stopwatch.Stop();
        Console.WriteLine($"Java風格解法耗時: {stopwatch.ElapsedMilliseconds} ms, 結果: {resultJavaStyle}");
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
    /// 【與方法3的差異對比】：
    /// 相同點：
    /// - 核心演算法完全相同，都是維護以當前位置結尾的所有可能結果
    /// - 時間複雜度和空間複雜度都相同：O(n × log(max)) 和 O(log(max))
    /// - 都利用了按位或運算的單調性優化
    /// 
    /// 差異點（僅實現風格不同）：
    /// 1. 【關鍵差異】初始化方式與處理順序：
    ///    - 方法2：currentResults 初始為空集合 {}，因為「先加入元素，再做運算」
    ///      邏輯順序：newResults.Add(num) → 再與 currentResults 做 OR 運算
    ///    - 方法3：cur 初始包含 {0}，因為「先做運算，再加入元素」
    ///      邏輯順序：先與 cur 做 OR 運算 → cur2.Add(x) 確保包含當前元素
    ///      必須初始化為 {0} 避免第一輪運算時 cur 為空集合造成錯誤
    /// 
    /// 2. 為什麼初始化不同？
    ///    - 方法2：先添加當前元素，所以空集合沒問題（不會遺漏任何元素）
    ///    - 方法3：先做 OR 運算，空集合會導致第一輪無法產生任何結果
    ///      利用「0 是按位或恆等元素」(x | 0 = x) 巧妙解決這個問題
    /// 
    /// 3. 程式碼風格：
    ///    - 方法2：變數名更直觀 (currentResults, newResults)
    ///    - 方法3：變數名更簡潔 (cur, cur2)，更接近 LeetCode 官方解答
    /// 
    /// 4. 結果合併時機：
    ///    - 方法2：每輪迭代結束後將新結果合併到總結果集
    ///    - 方法3：每輪迭代結束後立即合併當前結果到答案集
    /// 
    /// 實質上這兩種方法是同一個演算法的不同寫法，效能完全相同！
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
            
            // 【方法2的關鍵設計】當前元素本身就是一個結果（單元素子陣列）
            // 因為採用「先加入元素，再做運算」的順序，所以初始化空集合沒問題
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
    /// 方法：集合（Set-based Solution）- Java 風格實現
    /// 
    /// 【與方法2的關係】：
    /// 這是完全相同演算法的另一種實現風格，核心邏輯和效能完全相同！
    /// 唯一差別在於：
    /// 1. 初始化使用 cur = {0}，利用 0 是按位或恆等元素的特性
    /// 2. 變數命名更簡潔，更接近 LeetCode 官方解答風格
    /// 3. 邏輯順序略有調整，但本質完全相同
    /// 
    /// 官方解題思路分析：
    /// 最簡單的方法是枚舉所有滿足 i ≤ j 的 (i, j)，並計算出不同的 result(i, j) = A[i] | A[i+1] | ... | A[j] 數量。
    /// 由於 result(i, j+1) = result(i, j) | A[j+1]，因此我們可以在 O(N²) 的時間複雜度計算出所有的 result(i, j)。
    /// 
    /// 【關鍵優化觀察】：
    /// 對於固定的 j，result(j, j), result(j-1, j), result(j-2, j), ..., result(1, j) 的值是單調不降的，
    /// 因為將 result(k, j) 對 A[k-1] 做按位或操作，得到的結果 result(k-1, j) 一定不會變小。
    /// 
    /// 【按位或運算的關鍵特性】：
    /// 如果把 result(k, j) 和 result(k-1, j) 都用二進位表示，那麼後者將前者二進位表示中的若干個 0 變成了 1。
    /// 由於陣列 A 中都是小於 10^9 的正整數，它們的二進位表示最多只有 32 位。
    /// 因此從 result(j, j) 開始到 result(1, j) 結束，最多只會有 32 個 0 變成了 1，
    /// 也就是說，result(j, j), result(j-1, j), ..., result(1, j) 中最多只有 32 個不同的數字。
    /// 
    /// 【演算法核心】：
    /// 我們用一個集合 cur 儲存以 j 為結尾的 result 值，也就是所有滿足 i ≤ j 的 A[i] | ... | A[j] 的值。
    /// 集合的大小不會超過 32。當結尾從 j 枚舉到 j+1 時，我們將集合中的每個數對 A[j+1] 做按位或操作，
    /// 得到的新的 result 值也不會超過 32 個。
    /// 
    /// 時間複雜度：O(n * 32) = O(n * log(max(arr)))，其中 n 是陣列長度
    /// 空間複雜度：O(32) = O(log(max(arr)))
    /// 
    /// 為什麼初始化 cur = {0}？
    /// 0 是按位或運算的恆等元素（任何數與 0 做 OR 等於自己），這樣可以統一處理邏輯。
    /// 
    /// ref: https://leetcode.cn/problems/bitwise-ors-of-subarrays/solutions/18772/zi-shu-zu-an-wei-huo-cao-zuo-by-leetcode/
    /// 
    /// </summary>
    /// <param name="arr">輸入的整數陣列</param>
    /// <returns>所有非空子陣列按位或運算的不同結果數量</returns>
    public int SubarrayBitwiseORsJavaStyle(int[] arr)
    {
        if (arr is null || arr.Length == 0)
            return 0;
            
        // ans：儲存所有不同的按位或結果（最終答案集合）
        HashSet<int> ans = new HashSet<int>();
        
        // cur：儲存以當前位置 j 結尾的所有可能按位或結果
        // 即所有滿足 i ≤ j 的 result(i, j) = A[i] | A[i+1] | ... | A[j] 的值
        // 【關鍵】初始化為 {0}，因為採用「先做運算，再加入元素」的順序
        // 如果初始化為空集合，第一輪迴圈會因為沒有元素可做 OR 運算而出錯
        // 利用「0 是按位或恆等元素」(x | 0 = x) 巧妙解決這個問題
        HashSet<int> cur = new HashSet<int> { 0 };
        
        // 外層迴圈：遍歷陣列中的每個元素 x（對應位置 j）
        foreach (int x in arr)
        {
            // cur2：儲存以新位置 j+1 結尾的所有可能按位或結果
            HashSet<int> cur2 = new HashSet<int>();
            
            // 【方法3的關鍵設計】內層迴圈：先與所有之前的結果做按位或運算
            // 這一步計算所有 result(i, j+1) = result(i, j) | A[j+1] 的值
            // 【關鍵】：cur 的大小最多 32，因此內層迴圈複雜度是 O(32) 而非 O(n)
            foreach (int y in cur)
            {
                // y 代表某個 result(i, j)，x | y 就是 result(i, j+1)
                // 當 y = 0 時，x | 0 = x，自然包含了當前元素本身
                cur2.Add(x | y);
            }
            
            // 確保包含當前元素本身（單元素子陣列 [j+1, j+1]）
            // 對應 result(j+1, j+1) = A[j+1]
            // 註：實際上透過 x | 0 = x 已經包含了，這行是為了確保邏輯完整性
            cur2.Add(x);
            
            // 更新當前結果集：cur 現在儲存以位置 j+1 結尾的所有結果
            cur = cur2;
            
            // 將所有新結果加入最終答案集
            // 這樣確保我們收集了所有不同的子陣列按位或結果
            ans.UnionWith(cur);
        }
        
        return ans.Count;
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

    /// <summary>
    /// 演示集合方法的執行過程，幫助理解演算法運作機制
    /// </summary>
    /// <param name="arr">輸入的整數陣列</param>
    public void DemonstrateSetMethod(int[] arr)
    {
        if (arr is null || arr.Length == 0)
            return;
            
        Console.WriteLine("=== 集合方法演算法執行過程 ===");
        Console.WriteLine($"輸入陣列: [{string.Join(", ", arr)}]");
        Console.WriteLine();
        
        HashSet<int> ans = new HashSet<int>();
        HashSet<int> cur = new HashSet<int> { 0 };
        
        Console.WriteLine($"初始化: cur = {{{string.Join(", ", cur.OrderBy(x => x))}}}");
        Console.WriteLine();
        
        for (int i = 0; i < arr.Length; i++)
        {
            int x = arr[i];
            Console.WriteLine($"步驟 {i + 1}: 處理元素 A[{i}] = {x}");
            
            HashSet<int> cur2 = new HashSet<int>();
            
            Console.WriteLine($"  當前 cur = {{{string.Join(", ", cur.OrderBy(x => x))}}}");
            Console.WriteLine($"  計算新結果 cur2:");
            
            // 與之前結果做按位或運算
            foreach (int y in cur)
            {
                int result = x | y;
                cur2.Add(result);
                Console.WriteLine($"    {x} | {y} = {result}");
            }
            
            // 添加當前元素本身
            cur2.Add(x);
            Console.WriteLine($"    添加單元素: {x}");
            
            cur = cur2;
            Console.WriteLine($"  更新後 cur = {{{string.Join(", ", cur.OrderBy(x => x))}}}");
            Console.WriteLine($"  cur 的大小: {cur.Count} (≤ 32)");
            
            ans.UnionWith(cur);
            Console.WriteLine($"  加入答案集後 ans = {{{string.Join(", ", ans.OrderBy(x => x))}}}");
            Console.WriteLine($"  ans 的大小: {ans.Count}");
            Console.WriteLine();
        }
        
        Console.WriteLine($"最終結果: {ans.Count} 個不同的按位或值");
        Console.WriteLine($"所有不同結果: {{{string.Join(", ", ans.OrderBy(x => x))}}}");
    }

    /// <summary>
    /// 詳細分析：初始化方式對比 - cur = {0} vs currentResults = {}
    /// 
    /// 這是一個很好的觀察！讓我們深入分析兩種初始化方式的差異：
    /// </summary>
    /// <param name="arr">測試陣列</param>
    public void AnalyzeInitializationDifference(int[] arr)
    {
        if (arr is null || arr.Length == 0) return;
        
        Console.WriteLine("=== 初始化方式差異分析 ===");
        Console.WriteLine($"測試陣列: [{string.Join(", ", arr)}]");
        Console.WriteLine();
        
        // 方法2風格：currentResults 初始為空集合
        Console.WriteLine("【方法2】currentResults 初始為空集合：");
        AnalyzeMethod2Style(arr);
        Console.WriteLine();
        
        // 方法3風格：cur 初始為 {0}
        Console.WriteLine("【方法3】cur 初始為 {0}：");
        AnalyzeMethod3Style(arr);
        Console.WriteLine();
        
        // 關鍵差異總結
        Console.WriteLine("=== 關鍵差異總結 ===");
        Console.WriteLine("1. 【核心差異】邏輯順序與初始化的因果關係：");
        Console.WriteLine("   - 方法2：「先加入元素，再做運算」→ 所以初始化空集合 {} 沒問題");
        Console.WriteLine("   - 方法3：「先做運算，再加入元素」→ 所以必須初始化 {0} 避免錯誤");
        Console.WriteLine();
        Console.WriteLine("2. 初始化的數學意義：");
        Console.WriteLine("   - 空集合 {}：表示「還沒有任何結果」，適合先加入再運算的邏輯");
        Console.WriteLine("   - {0}：利用「0 是按位或恆等元素」特性，確保先運算時有基礎值");
        Console.WriteLine();
        Console.WriteLine("3. 為什麼方法3不能用空集合？");
        Console.WriteLine("   - 如果 cur 初始為空，第一輪 foreach (int y in cur) 不會執行");
        Console.WriteLine("   - 導致第一個元素無法被正確處理");
        Console.WriteLine("   - {0} 確保第一輪運算：x | 0 = x，完美解決這個問題");
        Console.WriteLine();
        Console.WriteLine("4. 實際效果：");
        Console.WriteLine("   - 兩種方式產生的最終結果完全相同");
        Console.WriteLine("   - 只是實現邏輯的順序不同，都能正確處理所有測試案例");
    }
    
    private void AnalyzeMethod2Style(int[] arr)
    {
        HashSet<int> allResults = new HashSet<int>();
        HashSet<int> currentResults = new HashSet<int>(); // 初始為空
        
        for (int i = 0; i < arr.Length; i++)
        {
            int num = arr[i];
            Console.WriteLine($"步驟 {i + 1}: 處理 arr[{i}] = {num}");
            Console.WriteLine($"  前一輪 currentResults = {{{string.Join(", ", currentResults.OrderBy(x => x))}}}");
            
            HashSet<int> newResults = new HashSet<int>();
            
            // 關鍵：先添加當前元素
            newResults.Add(num);
            Console.WriteLine($"  先添加當前元素: {num}");
            
            // 再與前一輪結果做 OR
            foreach (int prevResult in currentResults)
            {
                int result = prevResult | num;
                newResults.Add(result);
                Console.WriteLine($"  {prevResult} | {num} = {result}");
            }
            
            allResults.UnionWith(newResults);
            currentResults = newResults;
            Console.WriteLine($"  本輪 newResults = {{{string.Join(", ", newResults.OrderBy(x => x))}}}");
            Console.WriteLine($"  累積 allResults = {{{string.Join(", ", allResults.OrderBy(x => x))}}}");
            Console.WriteLine();
        }
        
        Console.WriteLine($"方法2最終結果: {allResults.Count} 個不同值");
    }
    
    private void AnalyzeMethod3Style(int[] arr)
    {
        HashSet<int> ans = new HashSet<int>();
        HashSet<int> cur = new HashSet<int> { 0 }; // 初始為 {0}
        
        for (int i = 0; i < arr.Length; i++)
        {
            int x = arr[i];
            Console.WriteLine($"步驟 {i + 1}: 處理 arr[{i}] = {x}");
            Console.WriteLine($"  前一輪 cur = {{{string.Join(", ", cur.OrderBy(x => x))}}}");
            
            HashSet<int> cur2 = new HashSet<int>();
            
            // 關鍵：先與前一輪結果（包含0）做 OR
            foreach (int y in cur)
            {
                int result = x | y;
                cur2.Add(result);
                Console.WriteLine($"  {x} | {y} = {result}");
            }
            
            // 再添加當前元素（實際上已經包含在上面的計算中，因為 x | 0 = x）
            cur2.Add(x);
            Console.WriteLine($"  確保包含當前元素: {x}");
            
            cur = cur2;
            ans.UnionWith(cur);
            Console.WriteLine($"  本輪 cur = {{{string.Join(", ", cur.OrderBy(x => x))}}}");
            Console.WriteLine($"  累積 ans = {{{string.Join(", ", ans.OrderBy(x => x))}}}");
            Console.WriteLine();
        }
        
        Console.WriteLine($"方法3最終結果: {ans.Count} 個不同值");
    }
    
    /// <summary>
    /// 測試不同初始化方式是否會產生不同結果
    /// </summary>
    public void TestInitializationImpact()
    {
        Console.WriteLine("=== 測試初始化方式對結果的影響 ===");
        
        // 測試多種不同的陣列
        int[][] testCases = {
            new int[] {0, 1, 1, 2},
            new int[] {1, 1, 2},
            new int[] {1, 2, 4},
            new int[] {0},
            new int[] {0, 0, 0},
            new int[] {7, 8, 9}
        };
        
        foreach (var testCase in testCases)
        {
            Console.WriteLine($"\n測試案例: [{string.Join(", ", testCase)}]");
            
            // 方法2結果
            var solution = new Program();
            int result2 = solution.SubarrayBitwiseORsOptimized(testCase);
            
            // 方法3結果
            int result3 = solution.SubarrayBitwiseORsJavaStyle(testCase);
            
            Console.WriteLine($"方法2結果: {result2}");
            Console.WriteLine($"方法3結果: {result3}");
            Console.WriteLine($"結果是否相同: {result2 == result3}");
            
            if (result2 != result3)
            {
                Console.WriteLine("⚠️  發現差異！需要進一步調查");
            }
        }
    }
}
