namespace leetcode_3362;

class Program
{
    /// <summary>
    /// 3362. Zero Array Transformation III
    /// https://leetcode.com/problems/zero-array-transformation-iii/description/?envType=daily-question&envId=2025-05-22
    /// 3362. 零數組變換 III
    /// https://leetcode.cn/problems/zero-array-transformation-iii/description/?envType=daily-question&envId=2025-05-22
    /// 
    /// 題目說明：
    /// 給定一個長度為 n 的整數陣列 nums 和一個二維陣列 queries，其中 queries[i] = [li, ri]。
    /// 
    /// 每個 queries[i] 代表對 nums 執行以下操作：
    ///     將 nums 中索引範圍 [li, ri] 內的每個值最多減少 1。
    ///     對於每個索引，減少的數值可以獨立選擇。
    /// 
    /// 零陣列是指所有元素均為 0 的陣列。
    /// 
    /// 返回可以從 queries 中移除的最大元素數量，使得使用剩餘的查詢仍然可以將 nums 轉換為零陣列。
    /// 如果無法將 nums 轉換為零陣列，則返回 -1。
    /// </summary>    
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        // 建立 Program 實例以呼叫非靜態方法
        Program program = new Program();

        Console.WriteLine("===== 零數組變換 III (Zero Array Transformation III) 測試 =====\n");

        // 測試案例 1：基本測試
        Console.WriteLine("測試案例 1 - 基本測試：");
        int[] nums1 = { 2, 3, 1, 4 };
        int[][] queries1 = new int[][] {
            new int[] { 1, 3 },   // [1,3] 範圍內的元素最多減少 1
            new int[] { 0, 2 }  // [0,2] 範圍內的元素最多減少 1
        };
        
        Console.WriteLine("輸入: nums = [2,3,1,4], queries = [[0,2],[1,3]]");
        Console.WriteLine("MaxRemoval 結果: " + program.MaxRemoval(nums1, queries1));
        Console.WriteLine("MaxRemoval2 結果: " + program.MaxRemoval2(nums1, queries1));
        Console.WriteLine();
        
        // 測試案例 2：需要所有查詢
        Console.WriteLine("測試案例 2 - 需要所有查詢：");
        int[] nums2 = { 3, 2, 1, 4 };
        int[][] queries2 = new int[][] {
            new int[] { 0, 1 },
            new int[] { 1, 2 },
            new int[] { 2, 3 }
        };
        
        Console.WriteLine("輸入: nums = [3,2,1,4], queries = [[0,1],[1,2],[2,3]]");
        Console.WriteLine("MaxRemoval 結果: " + program.MaxRemoval(nums2, queries2));
        Console.WriteLine("MaxRemoval2 結果: " + program.MaxRemoval2(nums2, queries2));
        Console.WriteLine();
        
        // 測試案例 3：無解案例
        Console.WriteLine("測試案例 3 - 無解案例：");
        int[] nums3 = { 3, 3, 3 };
        int[][] queries3 = new int[][] {
            new int[] { 0, 1 },
            new int[] { 1, 2 }
        };
        
        Console.WriteLine("輸入: nums = [3,3,3], queries = [[0,1],[1,2]]");
        Console.WriteLine("MaxRemoval 結果: " + program.MaxRemoval(nums3, queries3));
        Console.WriteLine("MaxRemoval2 結果: " + program.MaxRemoval2(nums3, queries3));
        Console.WriteLine();
        
        // 測試案例 4：可移除部分查詢
        Console.WriteLine("測試案例 4 - 可移除部分查詢：");
        int[] nums4 = { 1, 2, 3, 1 };
        int[][] queries4 = new int[][] {
            new int[] { 0, 2 },
            new int[] { 1, 3 },
            new int[] { 0, 3 },
            new int[] { 2, 3 }
        };
        
        Console.WriteLine("輸入: nums = [1,2,3,1], queries = [[0,2],[1,3],[0,3],[2,3]]");
        Console.WriteLine("MaxRemoval 結果: " + program.MaxRemoval(nums4, queries4));
        Console.WriteLine("MaxRemoval2 結果: " + program.MaxRemoval2(nums4, queries4));
        Console.WriteLine();
        
        // 測試案例 5：邊界情況
        Console.WriteLine("測試案例 5 - 邊界情況：");
        int[] nums5 = { 5 };
        int[][] queries5 = new int[][] {
            new int[] { 0, 0 },
            new int[] { 0, 0 },
            new int[] { 0, 0 },
            new int[] { 0, 0 },
            new int[] { 0, 0 }
        };
        
        Console.WriteLine("輸入: nums = [5], queries = [[0,0],[0,0],[0,0],[0,0],[0,0]]");
        Console.WriteLine("MaxRemoval 結果: " + program.MaxRemoval(nums5, queries5));
        Console.WriteLine("MaxRemoval2 結果: " + program.MaxRemoval2(nums5, queries5));
    }


    /// <summary>
    /// 零數組變換 III 解法，使用優先佇列
    /// 
    /// 此解法針對題目要求：
    /// 1. 使用優先佇列來管理對索引範圍 [li, ri] 的操作
    /// 2. 利用差分數組 (deltaArray) 來追蹤範圍操作的效果
    /// 3. 最大化可移除的查詢數量，同時確保仍能將數組轉為零數組
    /// 
    /// 時間複雜度：O(n log m)，其中 n 是 nums 長度，m 是查詢數量
    /// 空間複雜度：O(n + m)，用於存儲優先佇列和差分數組
    /// 
    /// ref:
    /// https://leetcode.cn/problems/zero-array-transformation-iii/solutions/3674726/ling-shu-zu-bian-huan-iii-by-leetcode-so-ptvl/?envType=daily-question&envId=2025-05-22
    /// 
    /// </summary>
    /// <param name="nums">一個整數陣列</param>
    /// <param name="queries">二維查詢陣列，每個查詢包含 [li, ri]，表示操作範圍</param>
    /// <returns>可以從 queries 中移除的最大元素數量，如無法將陣列轉為零陣列則返回 -1</returns>
    public int MaxRemoval(int[] nums, int[][] queries)
    {
        // 根據左邊界 (li) 對查詢進行排序
        Array.Sort(queries, (a, b) => a[0] - b[0]);

        // 建立一個優先佇列，用於儲存查詢的右邊界 (ri) 值
        // 設定比較器讓較大的值優先出佇列
        var heap = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));

        // 建立差分陣列，用於追蹤操作在不同位置的影響
        int[] deltaArray = new int[nums.Length + 1];

        // 追蹤已執行的操作數量
        int operations = 0;

        // 遍歷 nums 陣列，同時處理查詢
        for (int i = 0, j = 0; i < nums.Length; i++)
        {
            // 更新當前有效操作數
            operations += deltaArray[i];
            // 處理所有左邊界為 i 的查詢
            while (j < queries.Length && queries[j][0] == i)
            {
                // 將查詢的右邊界 (ri) 值加入優先佇列
                heap.Enqueue(queries[j][1], queries[j][1]);
                j++;
            }

            // 執行必要的操作使 nums[i] 為零
            // 貪婪選擇：使用必要的操作
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

    
    /// <summary>
    /// 零數組變換 III 的另一種解法（從 Java 轉換）
    /// 
    /// 此解法基於原題目描述：
    /// 1. 將範圍 [li, ri] 內的每個元素最多減少 1
    /// 2. 透過優先佇列來選擇右邊界最大的操作區間
    /// 3. 利用差分數組 (diff) 來追蹤各個位置的操作效果
    /// 
    /// 時間複雜度：O(n log m)，其中 n 是 nums 長度，m 是查詢數量
    /// 空間複雜度：O(n + m)，用於存儲優先佇列和差分數組
    /// 
    /// ref:
    /// https://leetcode.cn/problems/zero-array-transformation-iii/solutions/2998650/tan-xin-zui-da-dui-chai-fen-shu-zu-pytho-35o6/?envType=daily-question&envId=2025-05-22
    /// 
    /// </summary>
    /// <param name="nums">一個整數陣列</param>
    /// <param name="queries">二維查詢陣列，每個查詢包含 [li, ri]，表示操作範圍</param>
    /// <returns>可以從 queries 中移除的最大元素數量，如無法將陣列轉為零陣列則返回 -1</returns>
    public int MaxRemoval2(int[] nums, int[][] queries)
    {
        // 根據左邊界對查詢進行排序
        Array.Sort(queries, (a, b) => a[0] - b[0]);

        // 建立一個優先佇列，用於儲存查詢的右邊界值
        // 設定比較器讓較大的值優先出佇列
        var heap = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));

        int n = nums.Length;

        // 建立差分數組，用於追蹤操作在不同位置的影響
        int[] diff = new int[n + 1];

        // 追蹤已執行的操作總數
        int operations = 0;

        // 用於遍歷查詢的指標
        int j = 0;

        // 遍歷原始數組
        for (int i = 0; i < n; i++)
        {
            // 更新當前位置的操作數量
            operations += diff[i];

            // 處理所有左邊界 <= i 的查詢
            while (j < queries.Length && queries[j][0] <= i)
            {
                heap.Enqueue(queries[j][1], queries[j][1]);
                j++;
            }

            // 選擇右邊界最大的區間進行操作
            while (operations < nums[i] && heap.Count > 0 && heap.Peek() >= i)
            {
                operations++;
                diff[heap.Dequeue() + 1]--;
            }

            // 如果操作不足以將 nums[i] 變為零，則無解
            if (operations < nums[i])
            {
                return -1;
            }
        }

        // 返回未使用的查詢數量
        return heap.Count;
    }
}
