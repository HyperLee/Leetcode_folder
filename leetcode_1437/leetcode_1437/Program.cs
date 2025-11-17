namespace leetcode_1437;

class Program
{
    /// <summary>
    /// 1437. Check If All 1's Are at Least Length K Places Away
    /// https://leetcode.com/problems/check-if-all-1s-are-at-least-length-k-places-away/description/?envType=daily-question&envId=2025-11-17
    /// 1437. 是否所有 1 都至少相隔 k 個元素
    /// https://leetcode.cn/problems/check-if-all-1s-are-at-least-length-k-places-away/description/?envType=daily-question&envId=2025-11-17
    /// 題目描述(繁體中文): 給定一個二進位陣列 nums 和整數 k，若所有值為 1 的元素彼此間至少相隔 k 個元素，回傳 true；否則回傳 false。
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

        // 更多測試資料（比較兩個方法的輸出：prev-index vs zeros-counter）
        var tests = new (int[] nums, int k, string desc)[]
        {
            (new int[] { 1, 0, 0, 0, 1, 0, 0, 1 }, 2, "sparse ones - valid"),
            (new int[] { 1, 0, 0, 1, 0, 1 }, 2, "violates k=2"),
            (new int[] { 0, 0, 0, 0 }, 1, "all zeros"),
            (new int[] { 1 }, 0, "single one, k=0"),
            (new int[] { }, 1, "empty array"),
            (new int[] { 1, 0, 1 }, 1, "borderline k=1 valid"),
            (new int[] { 1, 0, 1 }, 2, "borderline k=2 invalid"),
        };

        Console.WriteLine();
        foreach (var (nums, k, desc) in tests)
        {
            var r1 = program.KLengthApart(nums, k);
            var r2 = program.KLengthApartZerosCounter(nums, k);
            Console.WriteLine($"{desc,-30} k={k,-2} -> prev-method: {r1,-5} zeros-method: {r2,-5}");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    /// <summary>
    /// 方法一：遍歷陣列
    /// 思路：
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

    /// <summary>
    /// 方法二：滑動視窗 / 零計數（zeros counter）
    /// 思路：
    /// 從左到右維護一個記錄自上次出現 1 之後的連續零數量 zeros。
    /// - 初始 zeros = Int32.MaxValue (或使用一個 boolean 表示是否已出現過 1)
    /// - 當遇到 0 時，如果之前已出現過 1，則 zeros++。
    /// - 當遇到 1 時，如果是第一次遇到，先標記已出現 1，否則檢查 zeros 是否至少為 k。
    /// - 如果 zeros < k，則回傳 false；否則重置 zeros = 0，繼續遍歷。
    /// 時間複雜度：O(n)，空間複雜度：O(1)
    /// </summary>
    /// <param name="nums">二進位陣列</param>
    /// <param name="k">最少間隔的 0 的數目</param>
    /// <returns>若所有 1 之間至少相隔 k 個元素則回傳 true，否則 false</returns>
    public bool KLengthApartZerosCounter(int[] nums, int k)
    {
        // 使用 boolean 來追蹤是否已經看到第一個 1
        bool seenOne = false;
        int zeros = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == 1)
            {
                if (!seenOne)
                {
                    // 第一次看到 1，標記並繼續
                    seenOne = true;
                    zeros = 0;
                }
                else
                {
                    // 若之前已看到 1，檢查是否有足夠的 0
                    if (zeros < k)
                    {
                        return false;
                    }
                    // 重置零計數
                    zeros = 0;
                }
            }
            else if (seenOne)
            {
                // 遇到 0 並且已經看到過 1，才開始計數
                zeros++;
            }
        }

        return true;
    }
}
