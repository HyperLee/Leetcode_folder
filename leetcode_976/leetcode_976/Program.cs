namespace leetcode_976;

class Program
{
    /// <summary>
    /// 976. Largest Perimeter Triangle
    /// https://leetcode.com/problems/largest-perimeter-triangle/description/?envType=daily-question&envId=2025-09-28
    /// 976. 三角形的最大周长
    /// https://leetcode.cn/problems/largest-perimeter-triangle/description/?envType=daily-question&envId=2025-09-28
    /// 
    /// Given an integer array nums, return the largest perimeter of a triangle with a non-zero area, 
    /// formed from three of these lengths. If it is impossible to form any triangle of a non-zero area, return 0.
    /// 給定一個整數數組 nums，返回由這三個長度組成的非零面積三角形的最長周長。如果不可能形成任何非零面積的三角形，則返回 0。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();
        
        Console.WriteLine("🏁 LeetCode 976: 三角形的最大周長 - 多種解法比較");
        Console.WriteLine(new string('=', 80));
        
        // 準備測試數據
        int[][] testCases = {
            new int[] { 2, 1, 2 },     // 預期: 5
            new int[] { 1, 2, 1 },     // 預期: 0  
            new int[] { 3, 2, 3, 4 },  // 預期: 10
            new int[] { 1, 1, 10 },    // 預期: 0
            new int[] { 3, 6, 2, 3 }   // 預期: 8
        };
        
        int[] expected = { 5, 0, 10, 0, 8 };
        
        for (int i = 0; i < testCases.Length; i++)
        {
            var testCase = testCases[i];
            var expectedResult = expected[i];
            
            Console.WriteLine($"\n📋 測試案例 {i + 1}: [{string.Join(", ", testCase)}]");
            Console.WriteLine($"預期結果: {expectedResult}");
            Console.WriteLine(new string('-', 50));
            
            // 方法1: 排序 + 貪心策略（推薦）
            var test1 = (int[])testCase.Clone();
            var result1 = solution.LargestPerimeter(test1);
            Console.WriteLine($"方法1 (排序+貪心): {result1} {(result1 == expectedResult ? "✅" : "❌")}");
            
            // 方法2: 暴力解法
            var test2 = (int[])testCase.Clone();
            var result2 = solution.LargestPerimeterBruteForce(test2);
            Console.WriteLine($"方法2 (暴力解法): {result2} {(result2 == expectedResult ? "✅" : "❌")}");
            
            // 方法3: 優化的暴力解法
            var test3 = (int[])testCase.Clone();
            var result3 = solution.LargestPerimeterOptimizedBruteForce(test3);
            Console.WriteLine($"方法3 (優化暴力): {result3} {(result3 == expectedResult ? "✅" : "❌")}");
            
            // 方法4: 兩指針法
            var test4 = (int[])testCase.Clone();
            var result4 = solution.LargestPerimeterTwoPointers(test4);
            Console.WriteLine($"方法4 (兩指針法): {result4} {(result4 == expectedResult ? "✅" : "❌")}");
            
            // 方法5: 不修改原數組
            var test5 = (int[])testCase.Clone();
            var result5 = solution.LargestPerimeterImmutable(test5);
            Console.WriteLine($"方法5 (不修改原數組): {result5} {(result5 == expectedResult ? "✅" : "❌")}");
            Console.WriteLine($"原數組是否改變: [{string.Join(", ", test5)}] {(ArraysEqual(test5, testCase) ? "✅ 未改變" : "❌ 已改變")}");
            
            // 方法6: 遞歸解法
            var test6 = (int[])testCase.Clone();
            var result6 = solution.LargestPerimeterRecursive(test6);
            Console.WriteLine($"方法6 (遞歸解法): {result6} {(result6 == expectedResult ? "✅" : "❌")}");
            
            // 方法7: 真正的兩指針實現
            var test7 = (int[])testCase.Clone();
            var result7 = solution.LargestPerimeterTrueTwoPointers(test7);
            Console.WriteLine($"方法7 (真兩指針): {result7} {(result7 == expectedResult ? "✅" : "❌")}");
        }
        
        Console.WriteLine("\n" + new string('=', 80));
        Console.WriteLine("📊 解法比較總結:");
        Console.WriteLine("方法1 (排序+貪心): 時間 O(n log n), 空間 O(1) - 🏆 推薦");
        Console.WriteLine("方法2 (暴力解法): 時間 O(n³), 空間 O(1) - 適合小數據");
        Console.WriteLine("方法3 (優化暴力): 時間 O(n³), 空間 O(1) - 平均性能較好");
        Console.WriteLine("方法4 (兩指針法): 時間 O(n log n), 空間 O(1) - 等同方法1");
        Console.WriteLine("方法5 (不修改原數組): 時間 O(n log n), 空間 O(n) - 保護原數據");
        Console.WriteLine("方法6 (遞歸解法): 時間 O(n³), 空間 O(n) - 學習用途");
        Console.WriteLine("方法7 (真兩指針): 時間 O(n²), 空間 O(1) - 完整掃描所有組合");
    }
    
    // 輔助方法：檢查兩個數組是否相等
    private static bool ArraysEqual(int[] arr1, int[] arr2)
    {
        if (arr1.Length != arr2.Length) return false;
        for (int i = 0; i < arr1.Length; i++)
        {
            if (arr1[i] != arr2[i]) return false;
        }
        return true;
    }

    /// <summary>
    /// 【方法1：排序 + 貪心策略】（推薦解法）
    /// 
    /// 解題思路：
    /// 1. 將數組按升序排序，這樣可以方便檢查三角不等式
    /// 2. 從最大的三個數開始檢查，因為我們要找最大周長
    /// 3. 對於排序後的數組，只需檢查 nums[i-2] + nums[i-1] > nums[i] 即可
    ///    （因為其他兩個不等式在排序後必然成立）
    /// 4. 一旦找到滿足條件的三個數，立即返回其周長（貪心策略）
    /// 
    /// 優點：時間效率高，程式碼簡潔
    /// 缺點：需要修改原數組（排序）
    /// 
    /// 時間複雜度：O(n log n) - 主要來自排序
    /// 空間複雜度：O(1) - 只使用常數額外空間
    /// </summary>
    /// <param name="nums">包含邊長的整數數組</param>
    /// <returns>最大三角形周長，若無法形成三角形則返回 0</returns>
    public int LargestPerimeter(int[] nums)
    {
        // 步驟1: 將數組按升序排序，便於後續檢查三角不等式
        Array.Sort(nums);
        
        // 步驟2: 從最大的三個數開始向前檢查（貪心策略）
        // 由於排序後 nums[i] >= nums[i-1] >= nums[i-2]
        // 只需檢查最小的兩邊之和是否大於最大邊即可
        for (int i = nums.Length - 1; i >= 2; i--)
        {
            // 檢查三角不等式：兩小邊之和 > 最大邊
            if (nums[i - 2] + nums[i - 1] > nums[i])
            {
                // 找到第一組滿足條件的三邊，返回其周長
                return nums[i - 2] + nums[i - 1] + nums[i];
            }
        }
        
        // 若沒有找到滿足條件的三邊組合，返回 0
        return 0;
    }

    /// <summary>
    /// 【方法2：暴力解法】（適合小數據量）
    /// 
    /// 解題思路：
    /// 1. 三重嵌套迴圈，遍歷所有可能的三邊組合
    /// 2. 對每個組合檢查完整的三角不等式（三個條件都要檢查）
    /// 3. 記錄滿足條件的最大周長
    /// 
    /// 優點：不需要排序，保持原數組不變，邏輯直觀
    /// 缺點：時間複雜度高，不適合大數據量
    /// 
    /// 時間複雜度：O(n³) - 三重嵌套迴圈
    /// 空間複雜度：O(1) - 只使用常數額外空間
    /// </summary>
    /// <param name="nums">包含邊長的整數數組</param>
    /// <returns>最大三角形周長，若無法形成三角形則返回 0</returns>
    public int LargestPerimeterBruteForce(int[] nums)
    {
        int maxPerimeter = 0;
        int n = nums.Length;
        
        // 三重嵌套迴圈，檢查所有可能的三邊組合
        for (int i = 0; i < n - 2; i++)
        {
            for (int j = i + 1; j < n - 1; j++)
            {
                for (int k = j + 1; k < n; k++)
                {
                    int a = nums[i], b = nums[j], c = nums[k];
                    
                    // 檢查完整的三角不等式（三個條件）
                    if (a + b > c && a + c > b && b + c > a)
                    {
                        int perimeter = a + b + c;
                        maxPerimeter = Math.Max(maxPerimeter, perimeter);
                    }
                }
            }
        }
        
        return maxPerimeter;
    }

    /// <summary>
    /// 【方法3：優化的暴力解法】（加入早期終止）
    /// 
    /// 解題思路：
    /// 1. 先對數組排序（降序），優先檢查大數值組合
    /// 2. 三重嵌套迴圈，但一旦找到有效三角形就立即返回
    /// 3. 利用排序特性進行剪枝優化
    /// 
    /// 優點：結合排序和暴力搜索的優勢，有早期終止機制
    /// 缺點：最壞情況下仍是 O(n³)
    /// 
    /// 時間複雜度：O(n log n + n³) 最壞情況，平均情況下更好
    /// 空間複雜度：O(1) - 只使用常數額外空間
    /// </summary>
    /// <param name="nums">包含邊長的整數數組</param>
    /// <returns>最大三角形周長，若無法形成三角形則返回 0</returns>
    public int LargestPerimeterOptimizedBruteForce(int[] nums)
    {
        // 降序排序，優先檢查大數值
        Array.Sort(nums, (a, b) => b.CompareTo(a));
        int n = nums.Length;
        
        // 從最大的組合開始檢查
        for (int i = 0; i < n - 2; i++)
        {
            for (int j = i + 1; j < n - 1; j++)
            {
                for (int k = j + 1; k < n; k++)
                {
                    int a = nums[i], b = nums[j], c = nums[k];
                    
                    // 由於是降序排序，只需檢查 b + c > a
                    // （其他兩個不等式必然成立）
                    if (b + c > a)
                    {
                        // 找到第一個有效三角形，即為最大周長
                        return a + b + c;
                    }
                }
            }
        }
        
        return 0;
    }

    /// <summary>
    /// 【方法4：兩指針法】（需要先排序）
    /// 
    /// 解題思路：
    /// 1. 先排序數組
    /// 2. 固定最大邊，用兩指針在剩餘元素中尋找滿足條件的兩邊
    /// 3. 利用排序特性進行指針移動優化
    /// 
    /// 優點：時間效率較好，邏輯清晰
    /// 缺點：實現相對複雜，仍需要排序
    /// 
    /// 時間複雜度：O(n² + n log n) = O(n²)
    /// 空間複雜度：O(1) - 只使用常數額外空間
    /// </summary>
    /// <param name="nums">包含邊長的整數數組</param>
    /// <returns>最大三角形周長，若無法形成三角形則返回 0</returns>
    public int LargestPerimeterTwoPointers(int[] nums)
    {
        Array.Sort(nums);
        int n = nums.Length;
        
        // 從最大邊開始往前檢查，使用與方法1相同的邏輯
        // 兩指針法在這個問題上實際上等同於方法1
        for (int i = n - 1; i >= 2; i--)
        {
            // 檢查當前最大邊與前兩個邊是否能組成三角形
            if (nums[i - 2] + nums[i - 1] > nums[i])
            {
                return nums[i - 2] + nums[i - 1] + nums[i];
            }
        }
        
        return 0;
    }

    /// <summary>
    /// 【方法5：不修改原數組的解法】（使用額外空間）
    /// 
    /// 解題思路：
    /// 1. 複製原數組，避免修改原始數據
    /// 2. 對副本進行排序和處理
    /// 3. 使用與方法1相同的邏輯
    /// 
    /// 優點：不修改原數組，適合需要保持原數據的場景
    /// 缺點：需要額外的空間來存儲副本
    /// 
    /// 時間複雜度：O(n log n) - 主要來自排序
    /// 空間複雜度：O(n) - 需要額外數組空間
    /// </summary>
    /// <param name="nums">包含邊長的整數數組</param>
    /// <returns>最大三角形周長，若無法形成三角形則返回 0</returns>
    public int LargestPerimeterImmutable(int[] nums)
    {
        // 創建原數組的副本，避免修改原數組
        int[] sortedNums = new int[nums.Length];
        Array.Copy(nums, sortedNums, nums.Length);
        
        // 對副本進行排序
        Array.Sort(sortedNums);
        
        // 使用與方法1相同的邏輯
        for (int i = sortedNums.Length - 1; i >= 2; i--)
        {
            if (sortedNums[i - 2] + sortedNums[i - 1] > sortedNums[i])
            {
                return sortedNums[i - 2] + sortedNums[i - 1] + sortedNums[i];
            }
        }
        
        return 0;
    }

    /// <summary>
    /// 【方法6：遞歸解法】（學術意義較大）
    /// 
    /// 解題思路：
    /// 1. 將問題分解為子問題：在當前數組中找最大三角形
    /// 2. 對於每個可能的最大邊，遞歸尋找另外兩邊
    /// 3. 使用記憶化避免重複計算
    /// 
    /// 優點：展示了遞歸思維，便於理解問題結構
    /// 缺點：效率較低，主要用於學習目的
    /// 
    /// 時間複雜度：O(n³) 最壞情況
    /// 空間複雜度：O(n) - 遞歸呼叫堆疊
    /// </summary>
    /// <param name="nums">包含邊長的整數數組</param>
    /// <returns>最大三角形周長，若無法形成三角形則返回 0</returns>
    public int LargestPerimeterRecursive(int[] nums)
    {
        Array.Sort(nums, (a, b) => b.CompareTo(a)); // 降序排序
        return FindLargestTriangleRecursive(nums, 0, new int[3], 0);
    }

    private int FindLargestTriangleRecursive(int[] nums, int startIndex, int[] current, int currentIndex)
    {
        // 如果已選擇了3個邊
        if (currentIndex == 3)
        {
            // 檢查是否能形成三角形
            if (current[1] + current[2] > current[0])
            {
                return current[0] + current[1] + current[2];
            }
            return 0;
        }

        int maxPerimeter = 0;
        
        // 遞歸選擇剩餘的邊
        for (int i = startIndex; i < nums.Length; i++)
        {
            current[currentIndex] = nums[i];
            int result = FindLargestTriangleRecursive(nums, i + 1, current, currentIndex + 1);
            maxPerimeter = Math.Max(maxPerimeter, result);
            
            // 如果找到有效三角形且是降序排列，可以提前返回
            if (result > 0 && currentIndex == 0)
            {
                return result;
            }
        }
        
        return maxPerimeter;
    }

    /// <summary>
    /// 【方法7：真正的兩指針實現】（學術展示）
    /// 
    /// 解題思路：
    /// 1. 排序後，對每個可能的最大邊，使用雙指針掃描所有可能的另外兩邊
    /// 2. 利用排序的單調性來移動指針
    /// 3. 記錄所有滿足條件的組合中周長最大的
    /// 
    /// 時間複雜度：O(n²)
    /// 空間複雜度：O(1)
    /// </summary>
    /// <param name="nums">包含邊長的整數數組</param>
    /// <returns>最大三角形周長，若無法形成三角形則返回 0</returns>
    public int LargestPerimeterTrueTwoPointers(int[] nums)
    {
        Array.Sort(nums);
        int n = nums.Length;
        int maxPerimeter = 0;
        
        // 對每個可能作為最大邊的元素（從小到大）
        for (int k = 2; k < n; k++)
        {
            int left = 0, right = k - 1;
            
            // 使用兩指針找到所有滿足 nums[left] + nums[right] > nums[k] 的組合
            while (left < right)
            {
                if (nums[left] + nums[right] > nums[k])
                {
                    // 找到有效三角形
                    // 由於排序，從 left 到 right-1 的所有位置配合 right 都能形成三角形
                    // 但我們只關心最大周長，所以記錄當前組合
                    int currentPerimeter = nums[left] + nums[right] + nums[k];
                    maxPerimeter = Math.Max(maxPerimeter, currentPerimeter);
                    
                    // 為了找到所有可能的組合，我們需要移動指針
                    // 在找到滿足條件後，我們可以嘗試更大的left（保持right不變）
                    // 或者移動right到下一個位置
                    int tempLeft = left + 1;
                    while (tempLeft < right)
                    {
                        if (nums[tempLeft] + nums[right] > nums[k])
                        {
                            currentPerimeter = nums[tempLeft] + nums[right] + nums[k];
                            maxPerimeter = Math.Max(maxPerimeter, currentPerimeter);
                        }
                        tempLeft++;
                    }
                    right--; // 移動right指針
                }
                else
                {
                    // 和太小，移動左指針
                    left++;
                }
            }
        }
        
        return maxPerimeter;
    }
}
