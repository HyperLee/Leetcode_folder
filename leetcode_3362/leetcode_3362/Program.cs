namespace leetcode_3362;

class Program
{    /// <summary>
    /// 3362. Zero Array Transformation III
    /// https://leetcode.com/problems/zero-array-transformation-iii/description/?envType=daily-question&envId=2025-05-22
    /// 3362. 零数组变换 III
    /// https://leetcode.cn/problems/zero-array-transformation-iii/description/?envType=daily-question&envId=2025-05-22
    /// 
    /// 題目說明：
    /// 給定一個整數陣列 nums 和一個二維整數陣列 queries，其中 queries[i] = [threshold_i, limit_i]。
    /// 對於每個查詢，你可以將不超過 limit_i 個小於或等於 threshold_i 的元素從 nums 中移除。
    /// 返回在執行所有查詢後能夠從 nums 中移除的最大元素數量。
    /// 
    /// 解題思路：
    /// 1. 排序 nums 陣列和 queries 陣列（按照 threshold 排序）
    /// 2. 使用雙指針技術計算每個查詢能移除的元素數量
    /// 3. 追蹤累計移除的元素總數
    /// 4. 返回能夠移除的最大元素數量
    /// </summary>    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        // 建立 Program 實例以呼叫非靜態方法
        Program program = new Program();
        
        // 測試案例 1：簡單範例
        Console.WriteLine("測試案例 1：");
        int[] nums1 = { 1, 2, 3, 4, 5 };
        int[][] queries1 = new int[][] {
            new int[] { 3, 2 },  // 閾值為 3，限制為 2
            new int[] { 4, 3 }   // 閾值為 4，限制為 3
        };
        
        Console.WriteLine("MaxRemoval 結果: " + program.MaxRemoval(nums1, queries1));
        Console.WriteLine("MaxRemoval2 結果: " + program.MaxRemoval2(nums1, queries1));
        Console.WriteLine();
        
        // 測試案例 2：較複雜範例
        Console.WriteLine("測試案例 2：");
        int[] nums2 = { 5, 3, 2, 7, 1, 6, 4 };
        int[][] queries2 = new int[][] {
            new int[] { 4, 3 },  // 閾值為 4，限制為 3
            new int[] { 6, 2 },  // 閾值為 6，限制為 2
            new int[] { 2, 1 }   // 閾值為 2，限制為 1
        };
        
        Console.WriteLine("MaxRemoval 結果: " + program.MaxRemoval(nums2, queries2));
        Console.WriteLine("MaxRemoval2 結果: " + program.MaxRemoval2(nums2, queries2));
        Console.WriteLine();
        
        // 測試案例 3：邊界案例
        Console.WriteLine("測試案例 3 - 邊界案例：");
        int[] nums3 = { 10, 20, 30 };
        int[][] queries3 = new int[][] {
            new int[] { 15, 1 },  // 閾值為 15，限制為 1
            new int[] { 25, 2 }   // 閾值為 25，限制為 2
        };
        
        Console.WriteLine("MaxRemoval 結果: " + program.MaxRemoval(nums3, queries3));
        Console.WriteLine("MaxRemoval2 結果: " + program.MaxRemoval2(nums3, queries3));
        Console.WriteLine();
        
        // 測試案例 4：無解案例
        Console.WriteLine("測試案例 4 - 可能無解案例：");
        int[] nums4 = { 5, 5, 5 };
        int[][] queries4 = new int[][] {
            new int[] { 3, 1 },  // 閾值為 3，限制為 1
            new int[] { 4, 1 }   // 閾值為 4，限制為 1
        };
        
        Console.WriteLine("MaxRemoval 結果: " + program.MaxRemoval(nums4, queries4));
        Console.WriteLine("MaxRemoval2 結果: " + program.MaxRemoval2(nums4, queries4));
    }
    /// <summary>
    /// 零數組變換 III 的主要解法
    /// 
    /// 題目概述：
    /// 給定一個整數陣列 nums 和一組查詢 queries，其中 queries[i] = [threshold_i, limit_i]
    /// 每個查詢允許移除不超過 limit_i 個小於或等於 threshold_i 的元素
    /// 
    /// 解題思路：
    /// 1. 先排序 nums 陣列以便高效處理查詢
    /// 2. 將原始查詢的 limit 值儲存到答案陣列，同時記錄每個查詢的原始索引
    /// 3. 依據 threshold 值排序查詢陣列，以便使用單次掃描處理所有查詢
    /// 4. 使用雙指針技術計算每個閾值下的元素總和
    /// 5. 計算每個查詢的結果值（可用於移除的最大元素數量）
    /// 6. 返回所有查詢結果中的最大值
    /// 
    /// 時間複雜度：O(n log n + m log m)，其中 n 是 nums 長度，m 是查詢數量
    /// 空間複雜度：O(m)，用於儲存結果陣列
    /// </summary>
    /// <param name="nums">一個整數陣列</param>
    /// <param name="queries">二維查詢陣列，每個查詢包含 [threshold, limit]</param>
    /// <returns>所有查詢後能夠移除的最大元素數量</returns>
    public int MaxRemoval(int[] nums, int[][] queries)
    {
        // 取得輸入陣列的長度
        int n = nums.Length;
        int m = queries.Length;
        
        // 初始化答案陣列，用於存儲每個查詢的結果
        int[] ans = new int[m];
        
        // 對 nums 陣列進行排序，以便後續高效處理查詢
        Array.Sort(nums);
        
        // 預處理查詢：
        // 1. 將每個查詢的 limit 值（queries[i][1]）儲存到答案陣列
        // 2. 將每個查詢的原始索引 i 存入 queries[i][1]，以便之後還原順序
        for (int i = 0; i < m; i++)
        {
            ans[i] = queries[i][1]; // 儲存 limit 值
            queries[i][1] = i;      // 記錄原始索引
        }
        
        // 根據閾值 (threshold) 對查詢進行排序
        Array.Sort(queries, (a, b) => a[0] - b[0]);
        
        // 初始化變數：
        // j：用於遍歷 nums 陣列的指針
        // sum：表示到目前為止所有不大於當前查詢閾值的數字總和
        int j = 0;
        int sum = 0;
        
        // 處理每個排序後的查詢
        for (int i = 0; i < m; i++)
        {
            // 找出所有小於等於當前查詢閾值的數字，並將它們加入總和
            while (j < n && nums[j] <= queries[i][0])
            {
                sum += nums[j];
                j++;
            }
            
            // 計算結果：原始 limit 減去累計總和，並存入對應的原始索引位置
            ans[queries[i][1]] -= sum;
        }
        
        // 返回所有查詢結果中的最大值
        return ans.Max();
    }

    /// <summary>
    /// 零數組變換 III 的替代解法，使用優先佇列
    /// 
    /// 此解法使用不同的策略來解決相同問題：
    /// 1. 使用優先佇列(堆疊)來追蹤可用的操作限制
    /// 2. 利用增量數組(deltaArray)來維護不同位置的操作計數
    /// 3. 對每個元素執行必要的操作使其為零
    /// 
    /// 時間複雜度：O(n log m)，其中 n 是 nums 長度，m 是查詢數量
    /// 空間複雜度：O(n + m)，用於存儲優先佇列和增量數組
    /// </summary>
    /// <param name="nums">一個整數陣列</param>
    /// <param name="queries">二維查詢陣列，每個查詢包含 [threshold, limit]</param>
    /// <returns>可執行的最大操作次數，如無法將所有元素變為零則返回 -1</returns>
    public int MaxRemoval2(int[] nums, int[][] queries)
    {        // 根據閾值 (threshold) 對查詢進行排序
        Array.Sort(queries, (a, b) => a[0] - b[0]);
        
        // 建立一個優先佇列，用於儲存查詢的 limit 值
        // 設定比較器讓較大的值優先出佇列
        var heap = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));
        
        // 建立增量數組，用於追蹤操作在不同位置的影響
        int[] deltaArray = new int[nums.Length + 1];
        
        // 追蹤已執行的操作數量
        int operations = 0;

        // 遍歷 nums 陣列，同時處理查詢
        for (int i = 0, j = 0; i < nums.Length; i++)
        {
            // 增加當前位置的操作數量
            operations += deltaArray[i];

            // 處理所有閾值為 i 的查詢
            while (j < queries.Length && queries[j][0] == i)
            {
                // 將查詢的 limit 值加入優先佇列
                heap.Enqueue(queries[j][1], queries[j][1]);
                j++;
            }

            // 執行必要的操作使 nums[i] 為零
            while (operations < nums[i] && heap.Count > 0 && heap.Peek() >= i)
            {
                // 增加操作計數
                operations++;
                // 在操作期限到達時減少操作計數
                deltaArray[heap.Dequeue() + 1]--;
            }

            // 如果操作不足以將 nums[i] 變為零，則無解
            if (operations < nums[i])
            {
                return -1;
            }
        }
        
        // 返回剩餘的可用操作數量
        return heap.Count;
    }
}
