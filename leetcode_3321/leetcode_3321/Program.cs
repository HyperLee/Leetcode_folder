namespace leetcode_3321;

class Program
{
    /// <summary>
    /// 3321. Find X-Sum of All K-Long Subarrays II
    /// https://leetcode.com/problems/find-x-sum-of-all-k-long-subarrays-ii/description/?envType=daily-question&envId=2025-11-05
    /// 3321. 计算子数组的 x-sum II
    /// https://leetcode.cn/problems/find-x-sum-of-all-k-long-subarrays-ii/description/?envType=daily-question&envId=2025-11-05
    /// 
    /// 你給定一個包含 n 個整數的陣列 nums 和兩個整數 k 和 x。
    /// 陣列的 x-sum 通過以下程序計算：
    /// 統計陣列中所有元素的出現次數。
    /// 僅保留前 x 個最頻繁元素的出現次數。如果兩個元素出現次數相同，則值較大的元素被視為更頻繁。
    /// 計算結果陣列的總和。
    /// 請注意，如果陣列的相異元素少於 x 個，則其 x-sum 為陣列的總和。
    /// 返回一個長度為 n - k + 1 的整數陣列 answer，其中 answer[i] 是子陣列 nums[i..i + k - 1] 的 x-sum。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("3321. Find X-Sum of All K-Long Subarrays II");
        Console.WriteLine("========================================");
        
        // 測試案例 1: 基本測試
        Console.WriteLine("\n測試案例 1: 基本測試");
        int[] nums1 = {1, 1, 2, 2, 3, 4, 2, 3};
        int k1 = 6, x1 = 2;
        long[] result1 = FindXSum(nums1, k1, x1);
        Console.WriteLine($"輸入: nums = [{string.Join(", ", nums1)}], k = {k1}, x = {x1}");
        Console.WriteLine($"輸出: [{string.Join(", ", result1)}]");
        
        // 測試案例 2: 所有元素相同
        Console.WriteLine("\n測試案例 2: 所有元素相同");
        int[] nums2 = {5, 5, 5, 5, 5};
        int k2 = 3, x2 = 1;
        long[] result2 = FindXSum(nums2, k2, x2);
        Console.WriteLine($"輸入: nums = [{string.Join(", ", nums2)}], k = {k2}, x = {x2}");
        Console.WriteLine($"輸出: [{string.Join(", ", result2)}]");
        
        // 測試案例 3: x 大於相異元素數量
        Console.WriteLine("\n測試案例 3: x 大於相異元素數量");
        int[] nums3 = {1, 2, 3, 4, 5};
        int k3 = 3, x3 = 5;
        long[] result3 = FindXSum(nums3, k3, x3);
        Console.WriteLine($"輸入: nums = [{string.Join(", ", nums3)}], k = {k3}, x = {x3}");
        Console.WriteLine($"輸出: [{string.Join(", ", result3)}]");
        
        // 測試案例 4: 頻率相同時數值決定順序
        Console.WriteLine("\n測試案例 4: 頻率相同時數值決定順序");
        int[] nums4 = {3, 1, 3, 1, 2};
        int k4 = 5, x4 = 2;
        long[] result4 = FindXSum(nums4, k4, x4);
        Console.WriteLine($"輸入: nums = [{string.Join(", ", nums4)}], k = {k4}, x = {x4}");
        Console.WriteLine($"輸出: [{string.Join(", ", result4)}]");
        
        // 測試案例 5: 大數值測試 (驗證 long 類型)
        Console.WriteLine("\n測試案例 5: 大數值測試");
        int[] nums5 = {100000, 200000, 100000, 200000, 300000};
        int k5 = 4, x5 = 2;
        long[] result5 = FindXSum(nums5, k5, x5);
        Console.WriteLine($"輸入: nums = [{string.Join(", ", nums5)}], k = {k5}, x = {x5}");
        Console.WriteLine($"輸出: [{string.Join(", ", result5)}]");
        
        Console.WriteLine("\n所有測試完成!");
    }

    /// <summary>
    /// 方法: 使用雙 SortedSet 優化的滑動視窗
    /// 
    /// 核心思路:
    /// 維護兩個有序集合來動態追蹤前 x 個最頻繁元素:
    /// - topSet: 存放前 x 個最頻繁的元素 (用於計算 x-sum)
    /// - restSet: 存放其餘元素
    /// 
    /// 關鍵優化:
    /// 1. 使用 SortedSet 自動維護排序，插入/刪除都是 O(log k)
    /// 2. 滑動視窗時只需調整受影響的元素，不需重新排序全部
    /// 3. 動態平衡 topSet 和 restSet 的大小
    /// 
    /// 排序規則 (由大到小):
    /// - 優先按頻率降序
    /// - 頻率相同時按數值降序
    /// 
    /// 時間複雜度: O((n-k+1) × log k)
    /// - 每次滑動: 移除 O(log k) + 加入 O(log k) + 平衡 O(log k)
    /// 
    /// 空間複雜度: O(k)
    /// - 頻率字典 + 兩個 SortedSet 最多存儲 k 個不同元素
    /// </summary>
    /// <param name="nums">輸入的整數陣列</param>
    /// <param name="k">子陣列的長度</param>
    /// <param name="x">要計算的前 x 個最頻繁元素</param>
    /// <returns>長度為 n-k+1 的 long 陣列，包含每個子陣列的 x-sum</returns>
    public static long[] FindXSum(int[] nums, int k, int x)
    {
        int n = nums.Length;
        long[] result = new long[n - k + 1];
        
        // 頻率字典: 記錄每個數字在當前視窗中的出現次數
        IDictionary<int, int> frequencyMap = new Dictionary<int, int>();
        
        // 自訂比較器: 先按頻率降序，再按數值降序
        IComparer<(int freq, int value)> comparer = Comparer<(int freq, int value)>.Create((a, b) =>
        {
            // 頻率不同時，頻率大的排前面
            if (a.freq != b.freq)
            {
                return b.freq.CompareTo(a.freq);
            }
            // 頻率相同時，數值大的排前面
            return b.value.CompareTo(a.value);
        });
        
        // topSet: 維護前 x 個最頻繁的元素
        SortedSet<(int freq, int value)> topSet = new SortedSet<(int freq, int value)>(comparer);
        
        // restSet: 維護其他元素
        SortedSet<(int freq, int value)> restSet = new SortedSet<(int freq, int value)>(comparer);
        
        // topSum: 維護 topSet 中所有元素的加權和 (數值 × 頻率)
        long topSum = 0;
        
        // 初始化第一個視窗 (索引 0 到 k-1)
        for (int i = 0; i < k; i++)
        {
            Add(nums[i], frequencyMap, topSet, restSet, ref topSum, x);
        }
        
        result[0] = topSum;
        
        // 滑動視窗: 從第二個視窗開始 (索引 1 到 n-k)
        for (int i = 1; i < n - k + 1; i++)
        {
            // 移除離開視窗的元素 (最左邊)
            Remove(nums[i - 1], frequencyMap, topSet, restSet, ref topSum, x);
            
            // 加入進入視窗的元素 (最右邊)
            Add(nums[i + k - 1], frequencyMap, topSet, restSet, ref topSum, x);
            
            result[i] = topSum;
        }
        
        return result;
    }
    
    /// <summary>
    /// 輔助方法: 向視窗中加入一個元素
    /// 
    /// 步驟:
    /// 1. 如果元素已存在，先從對應集合中移除舊的 (freq, value)
    /// 2. 更新頻率字典 (頻率+1)
    /// 3. 將新的 (freq, value) 加入適當的集合
    /// 4. 重新平衡 topSet 和 restSet
    /// </summary>
    private static void Add(int num, IDictionary<int, int> frequencyMap, 
                     SortedSet<(int freq, int value)> topSet,
                     SortedSet<(int freq, int value)> restSet,
                     ref long topSum, int x)
    {
        // 取得當前頻率
        frequencyMap.TryGetValue(num, out int oldFreq);
        
        // 如果元素已存在，先移除舊的 (freq, value) pair
        if (oldFreq > 0)
        {
            var oldPair = (oldFreq, num);
            if (topSet.Contains(oldPair))
            {
                topSet.Remove(oldPair);
                topSum -= (long)num * oldFreq;
            }
            else
            {
                restSet.Remove(oldPair);
            }
        }
        
        // 更新頻率 (頻率+1)
        int newFreq = oldFreq + 1;
        frequencyMap[num] = newFreq;
        var newPair = (newFreq, num);
        
        // 將新的 pair 加入 topSet
        topSet.Add(newPair);
        topSum += (long)num * newFreq;
        
        // 平衡: 如果 topSet 超過 x 個元素，將最小的移到 restSet
        if (topSet.Count > x)
        {
            var smallest = topSet.Max; // SortedSet.Max 是排序後的最後一個 (最小的)
            topSet.Remove(smallest);
            topSum -= (long)smallest.value * smallest.freq;
            restSet.Add(smallest);
        }
    }
    
    /// <summary>
    /// 輔助方法: 從視窗中移除一個元素
    /// 
    /// 步驟:
    /// 1. 從對應集合中移除當前的 (freq, value)
    /// 2. 更新頻率字典 (頻率-1)
    /// 3. 如果頻率降為 0，從字典移除；否則加入新的 (freq, value)
    /// 4. 重新平衡 topSet 和 restSet
    /// </summary>
    private static void Remove(int num, IDictionary<int, int> frequencyMap,
                       SortedSet<(int freq, int value)> topSet,
                       SortedSet<(int freq, int value)> restSet,
                       ref long topSum, int x)
    {
        // 取得當前頻率
        int oldFreq = frequencyMap[num];
        var oldPair = (oldFreq, num);
        
        // 從對應集合中移除
        bool wasInTop = topSet.Contains(oldPair);
        if (wasInTop)
        {
            topSet.Remove(oldPair);
            topSum -= (long)num * oldFreq;
        }
        else
        {
            restSet.Remove(oldPair);
        }
        
        // 更新頻率 (頻率-1)
        int newFreq = oldFreq - 1;
        if (newFreq == 0)
        {
            // 頻率降為 0，從字典移除
            frequencyMap.Remove(num);
        }
        else
        {
            // 更新頻率並加入 restSet
            frequencyMap[num] = newFreq;
            var newPair = (newFreq, num);
            restSet.Add(newPair);
        }
        
        // 平衡: 如果從 topSet 移除且 restSet 有元素，將最大的移到 topSet
        if (wasInTop && restSet.Count > 0)
        {
            var largest = restSet.Min; // SortedSet.Min 是排序後的第一個 (最大的)
            restSet.Remove(largest);
            topSet.Add(largest);
            topSum += (long)largest.value * largest.freq;
        }
    }
}
