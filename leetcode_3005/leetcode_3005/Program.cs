namespace leetcode_3005;

class Program
{
    /// <summary>
    /// 3005. Count Elements With Maximum Frequency
    /// https://leetcode.com/problems/count-elements-with-maximum-frequency/description/?envType=daily-question&envId=2025-09-22
    /// 3005. 最大頻率元素計數
    /// https://leetcode.cn/problems/count-elements-with-maximum-frequency/description/?envType=daily-question&envId=2025-09-22
    ///
    /// 題目描述：
    /// 給定一個由正整數組成的陣列 nums。
    /// 回傳陣列中所有出現次數等於最大頻率的元素的總出現次數。
    /// 元素的頻率是該元素在陣列中出現的次數。
    ///
    /// 解題核心概念：
    /// - 頻率統計：使用 Dictionary 記錄每個元素的出現次數
    /// - 最大頻率追蹤：在統計過程中即時更新最大頻率值
    /// - 結果計算：累加所有最大頻率元素的出現次數
    ///
    /// </summary>
    /// <param name="args">命令行參數</param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        
        // 測試案例 1: [1,2,2,3,1,4]
        // 預期結果: 4 (元素1出現2次 + 元素2出現2次 = 4)
        int[] nums1 = {1, 2, 2, 3, 1, 4};
        int result1 = solution.MaxFrequencyElements(nums1);
        Console.WriteLine($"測試案例 1: [{string.Join(",", nums1)}]");
        Console.WriteLine($"結果: {result1}");
        Console.WriteLine();
        
        // 測試案例 2: [1,2,3,4,5]
        // 預期結果: 5 (每個元素都出現1次，最大頻率是1，所以 1+1+1+1+1 = 5)
        int[] nums2 = {1, 2, 3, 4, 5};
        int result2 = solution.MaxFrequencyElements(nums2);
        Console.WriteLine($"測試案例 2: [{string.Join(",", nums2)}]");
        Console.WriteLine($"結果: {result2}");
        Console.WriteLine();
        
        // 測試案例 3: [1,1,1,2,2,3]
        // 預期結果: 3 (元素1出現3次，是最大頻率，所以結果是3)
        int[] nums3 = {1, 1, 1, 2, 2, 3};
        int result3 = solution.MaxFrequencyElements(nums3);
        Console.WriteLine($"測試案例 3: [{string.Join(",", nums3)}]");
        Console.WriteLine($"結果: {result3}");
    }

    /// <summary>
    /// 計算陣列中所有出現次數等於最大頻率的元素的總出現次數
    /// 
    /// 解題思路：
    /// 1. 使用 Dictionary 統計每個元素的出現頻率
    /// 2. 在統計過程中同時追蹤最大頻率值
    /// 3. 遍歷 Dictionary，找出所有頻率等於最大頻率的元素
    /// 4. 累加這些元素的出現次數作為最終結果
    /// 
    /// 時間複雜度：O(n)，其中 n 是陣列長度
    /// 空間複雜度：O(k)，其中 k 是陣列中不同元素的個數
    /// </summary>
    /// <param name="nums">由正整數組成的陣列</param>
    /// <returns>所有最大頻率元素的總出現次數</returns>
    /// <example>
    /// 範例 1: nums = [1,2,2,3,1,4] 
    /// 頻率統計: 1→2次, 2→2次, 3→1次, 4→1次
    /// 最大頻率: 2
    /// 結果: 2+2=4 (元素1出現2次 + 元素2出現2次)
    /// </example>
    public int MaxFrequencyElements(int[] nums)
    {
        // 使用 Dictionary 來統計每個元素的出現頻率
        Dictionary<int, int> frequencyMap = new Dictionary<int, int>();
        int maxFrequencyElements = 0; // 儲存最大頻率元素的總出現次數
        int maxFrequency = 0;         // 追蹤目前找到的最大頻率

        // 第一階段：統計每個元素的頻率並同時更新最大頻率
        foreach (int value in nums) 
        {
            // 更新該元素的頻率計數
            if (frequencyMap.ContainsKey(value))
            {
                frequencyMap[value]++; // 已存在，頻率加一
            }
            else
            {
                frequencyMap.Add(value, 1); // 第一次出現，設定頻率為1
            }

            // 即時更新最大頻率值
            // 這樣可以避免第二次完整遍歷來找最大值
            maxFrequency = Math.Max(maxFrequency, frequencyMap[value]);
        }

        // 第二階段：找出所有頻率等於最大頻率的元素，並累加它們的出現次數
        foreach (KeyValuePair<int, int> kvp in frequencyMap) 
        {
            // 檢查該元素的頻率是否等於最大頻率
            if (kvp.Value == maxFrequency)
            {
                // 累加該元素的出現次數到結果中
                // 注意：這裡是累加 kvp.Value（該元素的出現次數），
                // 而不是累加 1（元素種類數量）
                maxFrequencyElements += kvp.Value;
            }
        }

        return maxFrequencyElements;
    }
}
