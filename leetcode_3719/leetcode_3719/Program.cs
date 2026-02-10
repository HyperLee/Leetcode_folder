namespace leetcode_3719;

class Program
{
    /// <summary>
    /// 3719. Longest Balanced Subarray I
    /// https://leetcode.com/problems/longest-balanced-subarray-i/description/?envType=daily-question&envId=2026-02-10
    /// 3719. 最長平衡子陣列 I
    /// https://leetcode.cn/problems/longest-balanced-subarray-i/description/?envType=daily-question&envId=2026-02-10
    ///
    /// English:
    /// You are given an integer array nums.
    /// A subarray is called balanced if the number of distinct even numbers in the subarray is equal to the number of distinct odd numbers.
    /// Return the length of the longest balanced subarray.
    ///
    /// 繁體中文:
    /// 給定一個整數陣列 nums。
    /// 若子陣列中不同的偶數數量等於不同的奇數數量，則稱該子陣列為平衡。
    /// 回傳最長平衡子陣列的長度。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();

        // 測試案例 1
        int[] nums1 = { 1, 2, 3, 4, 5 };
        Console.WriteLine($"測試案例 1: [{string.Join(", ", nums1)}]");
        Console.WriteLine($"結果: {program.LongestBalanced(nums1)}");
        Console.WriteLine();

        // 測試案例 2
        int[] nums2 = { 2, 4, 6, 8 };
        Console.WriteLine($"測試案例 2: [{string.Join(", ", nums2)}]");
        Console.WriteLine($"結果: {program.LongestBalanced(nums2)}");
        Console.WriteLine();

        // 測試案例 3
        int[] nums3 = { 1, 3, 5, 7, 9 };
        Console.WriteLine($"測試案例 3: [{string.Join(", ", nums3)}]");
        Console.WriteLine($"結果: {program.LongestBalanced(nums3)}");
        Console.WriteLine();

        // 測試案例 4
        int[] nums4 = { 1, 1, 1, 3, 3, 3, 2, 2 };
        Console.WriteLine($"測試案例 4: [{string.Join(", ", nums4)}]");
        Console.WriteLine($"結果: {program.LongestBalanced(nums4)}");
        Console.WriteLine();

        // 測試案例 5
        int[] nums5 = { 4, 5, 6, 7 };
        Console.WriteLine($"測試案例 5: [{string.Join(", ", nums5)}]");
        Console.WriteLine($"結果: {program.LongestBalanced(nums5)}");
    }

    /// <summary>
    /// 暴力法 - 時間複雜度 O(n²)
    /// 
    /// 解題思路:
    /// 1. 使用雙層迴圈遍歷所有可能的子陣列區間 [i, j]
    /// 2. 對於每個子陣列，使用兩個雜湊表分別記錄不同的偶數和奇數
    /// 3. 當偶數種類數等於奇數種類數時，表示該子陣列是平衡的
    /// 4. 不斷更新最長平衡子陣列的長度
    /// 
    /// 關鍵點:
    /// - Dictionary 的 Count 屬性代表不同數字的數量(不是出現次數)
    /// - 使用位元運算 (nums[j] & 1) 來判斷奇偶性，比 % 2 更有效率
    /// - 在每次確定區間左端點 i 時建立新的雜湊表
    /// - 在擴展右端點 j 的同時更新結果
    /// 
    /// 注意事項:
    /// 本題目比的是不同數字的種類數，而非出現次數
    /// 也就是 奇數種類數 == 偶數種類數
    /// 相同數字的多次出現不影響種類數的計算
    /// </summary>
    /// <param name="nums">整數陣列</param>
    /// <returns>最長平衡子陣列的長度</returns>
    public int LongestBalanced(int[] nums)
    {
        int len = 0;

        // 外層迴圈：固定子陣列的起始位置 i
        for(int i = 0; i < nums.Length; i++)
        {
            // 每次確定新的左端點時，建立新的雜湊表
            Dictionary<int, int> even = new Dictionary<int, int>();  // 記錄偶數及其出現次數
            Dictionary<int, int> odd = new Dictionary<int, int>();   // 記錄奇數及其出現次數

            // 內層迴圈：擴展子陣列的結束位置 j
            for(int j = i; j < nums.Length; j++)
            {
                // 判斷當前數字是奇數還是偶數，選擇對應的雜湊表
                Dictionary<int, int> dict = (nums[j] & 1) == 1 ? odd : even;
                
                // 更新該數字的出現次數
                dict[nums[j]] = dict.GetValueOrDefault(nums[j], 0) + 1;

                // 檢查是否為平衡子陣列：不同的偶數數量 == 不同的奇數數量
                if (even.Count == odd.Count)
                {
                    // 更新最大長度
                    len = Math.Max(len, j - i + 1);
                }
            }
        }
        return len;
    }
}
