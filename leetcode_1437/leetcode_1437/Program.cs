namespace leetcode_1437;

class Program
{
    /// <summary>
    /// 1437. Check If All 1's Are at Least Length K Places Away
    /// https://leetcode.com/problems/check-if-all-1s-are-at-least-length-k-places-away/description/?envType=daily-question&envId=2025-11-17
    /// 1437. 是否所有 1 都至少相隔 k 個元素
    ///
    /// 解題說明 (方法一：遍歷)
    /// 「所有 1 都至少相隔 k 個元素」等價於「任意兩個相鄰的 1 都至少相隔 k 個元素」，
    /// 因此可從左到右遍歷陣列，記錄上一次出現 1 的索引 prev；當遇到下一個 1 時，計算
    /// i - prev - 1（兩個 1 中間的 0 的數量），若小於 k 則回傳 false，否則更新 prev
    /// 為目前 i，繼續遍歷，最後皆符合則回傳 true。
    /// https://leetcode.cn/problems/check-if-all-1s-are-at-least-length-k-places-away/description/?envType=daily-question&envId=2025-11-17
    /// 題目描述(繁體中文): 給定一個二進位陣列 nums 和整數 k，若所有值為 1 的元素彼此間至少相隔 k 個元素，回傳 true；否則回傳 false。
    /// 
    /// 
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 簡單測試範例
        var program = new Program();
        int[] nums1 = new int[] { 1, 0, 0, 0, 1, 0, 0, 1 };
        Console.WriteLine($"nums1 -> k=2 : {program.KLengthApart(nums1, 2)} (expected True)");
        int[] nums2 = new int[] { 1, 0, 0, 1, 0, 1 };
        Console.WriteLine($"nums2 -> k=2 : {program.KLengthApart(nums2, 2)} (expected False)");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    /// <summary>
    /// 方法一：遍歷（說明重複）
    /// 遍歷陣列並記錄上一次出現 1 的索引：prev
    /// - 若 prev 為 -1（尚未出現 1）：將 prev 設為 i
    /// - 若 prev 已有值：判斷 i - prev - 1 是否大於等於 k
    ///     - 若小於 k：回傳 false
    ///     - 否則更新 prev 為 i，繼續
    /// 時間複雜度：O(n)，空間複雜度：O(1)
    /// </summary>
    /// 
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public bool KLengthApart(int[] nums, int k)
    {
        // prev 指向上一個出現 '1' 的索引，初始為 -1（表示尚未出現）
        int prev = -1;

        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] != 1) continue; // 只在遇到 1 時處理

            // 若 prev != -1，表示先前遇過 1，需要檢查間隔
            if (prev != -1)
            {
                // 兩個 1 中間的 0 的數目 = i - prev - 1
                if (i - prev - 1 < k)
                {
                    return false;
                }
            }

            // 更新 prev 為目前索引
            prev = i;
        }

        // 若整個陣列檢查完沒有違規，則回傳 true
        return true;
    }
}
