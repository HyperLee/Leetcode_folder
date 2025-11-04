namespace leetcode_3318;

class Program
{
    /// <summary>
    /// 3318. Find X-Sum of All K-Long Subarrays I
    /// 3318. 計算子陣列的 x-sum I
    ///
    /// 給定一個整數陣列 nums（長度為 n）以及兩個整數 k 和 x。
    /// 對於一個陣列，其 x-sum 定義如下：
    /// 1. 統計陣列中每個元素的出現次數。
    /// 2. 保留出現次數最多的前 x 個不同元素的所有出現。若兩個元素出現次數相同，則數值較大的元素視為較頻繁。
    /// 3. 將保留下來的元素全部相加，得到該陣列的 x-sum。
    /// 若陣列中的不同元素少於 x，則 x-sum 即為整個陣列的總和。
    ///
    /// 請回傳一個長度為 n - k + 1 的整數陣列 answer，其中 answer[i] 是子陣列 nums[i..i+k-1] 的 x-sum。
    ///
    /// 題目原文連結：
    /// https://leetcode.com/problems/find-x-sum-of-all-k-long-subarrays-i/description/?envType=daily-question&envId=2025-11-04
    /// https://leetcode.cn/problems/find-x-sum-of-all-k-long-subarrays-i/description/?envType=daily-question&envId=2025-11-04
    ///
    /// </summary>
    /// <param name="args">命令列參數(未使用)</param>
    static void Main(string[] args)
    {
        Program program = new Program();

        // 測試範例 1
        int[] nums1 = new int[] { 1, 1, 2, 2, 3, 4, 2, 3 };
        int k1 = 6;
        int x1 = 2;
        int[] result1 = program.FindXSum(nums1, k1, x1);
        Console.WriteLine($"範例 1: [{string.Join(", ", result1)}]");
        Console.WriteLine("預期輸出: [6, 10, 12]");
        Console.WriteLine();

        // 測試範例 2
        int[] nums2 = new int[] { 3, 8, 7, 8, 7, 5 };
        int k2 = 2;
        int x2 = 2;
        int[] result2 = program.FindXSum(nums2, k2, x2);
        Console.WriteLine($"範例 2: [{string.Join(", ", result2)}]");
        Console.WriteLine("預期輸出: [11, 15, 15, 15, 12]");
        Console.WriteLine();
    }

    /// <summary>
    /// 計算所有長度為 k 的子陣列的 x-sum
    /// 
    /// 解題思路:
    /// 1. 遍歷陣列中所有長度為 k 的子陣列(共有 n-k+1 個)
    /// 2. 對於每個子陣列:
    ///    - 使用字典統計每個元素的出現次數
    ///    - 將元素與頻率組成二元組,並按照以下規則排序:
    ///      a. 優先按出現次數降序排列
    ///      b. 出現次數相同時,按元素數值降序排列
    ///    - 取前 x 個元素(如果不足 x 個則取全部)
    ///    - 計算這些元素的加權和(元素值 × 出現次數)
    /// 3. 返回結果陣列
    /// 
    /// 時間複雜度: O((n-k+1) * k * log k)
    /// - 外層迴圈: O(n-k+1)
    /// - 內層統計頻率: O(k)
    /// - 排序: O(k * log k)
    /// 
    /// 空間複雜度: O(k)
    /// - 頻率字典和列表最多存儲 k 個不同元素
    /// </summary>
    /// <param name="nums">輸入的整數陣列</param>
    /// <param name="k">子陣列的長度</param>
    /// <param name="x">要計算的前 x 個最頻繁元素</param>
    /// <returns>長度為 n-k+1 的整數陣列,包含每個子陣列的 x-sum</returns>
    public int[] FindXSum(int[] nums, int k, int x)
    {
        // 取得陣列長度
        int n = nums.Length;
        // 建立結果陣列,大小為子陣列的數量
        int[] result = new int[n - k + 1];

        // 遍歷每個長度為 k 的子陣列
        for (int i = 0; i < n - k + 1; i++)
        {
            // 使用字典統計當前子陣列中每個元素的出現次數
            IDictionary<int, int> frequencyMap = new Dictionary<int, int>();
            for (int j = 0; j < k; j++)
            {
                // 獲取當前元素的出現次數(如果不存在則為 0)
                frequencyMap.TryGetValue(nums[i + j], out int count);
                // 更新該元素的出現次數
                frequencyMap[nums[i + j]] = count + 1;
            }
            
            // 將字典轉換為列表,每個元素是 [數值, 頻率] 的陣列
            IList<int[]> numsWithFrequency = new List<int[]>();
            foreach (var entry in frequencyMap)
            {
                numsWithFrequency.Add(new int[] { entry.Key, entry.Value });
            }
            
            // 對列表進行排序:
            // 1. 首先按頻率降序排列(出現次數多的在前)
            // 2. 頻率相同時,按數值降序排列(數值大的在前)
            numsWithFrequency = numsWithFrequency
                .OrderByDescending(pair => pair[1])  // 按頻率降序
                .ThenByDescending(pair => pair[0])   // 按數值降序
                .ToList();

            // 計算 x-sum
            int sum = 0;
            // 取前 x 個元素(如果元素總數少於 x,則取全部)
            int maxElements = Math.Min(x, numsWithFrequency.Count);
            for (int j = 0; j < maxElements; j++)
            {
                int num = numsWithFrequency[j][0];   // 元素數值
                int freq = numsWithFrequency[j][1];  // 出現次數
                // 加權和 = 元素值 × 出現次數
                sum += num * freq;
            }
            
            // 將當前子陣列的 x-sum 存入結果陣列
            result[i] = sum;
        }
        
        return result;
    }
}
