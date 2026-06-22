namespace leetcode_219;

class Program
{
    /// <summary>
    /// 219. Contains Duplicate II
    /// https://leetcode.com/problems/contains-duplicate-ii/description/
    /// 219. 存在重复元素 II
    /// https://leetcode.cn/problems/contains-duplicate-ii/description/
    ///
    /// Given an integer array nums and an integer k, return true if there are two distinct indices i and j
    /// in the array such that nums[i] == nums[j] and abs(i - j) &lt;= k.
    ///
    /// 給定整數陣列 nums 和整數 k，若陣列中存在兩個相異索引 i 與 j，使得 nums[i] == nums[j]
    /// 且 abs(i - j) &lt;= k，則回傳 true。
    /// </summary>
    /// <remarks>
    /// 執行固定的官方與邊界測資，並同時驗證雜湊表與滑動視窗兩種解法的輸出。
    /// </remarks>
    /// <param name="args">目前未使用的命令列參數。</param>
    static void Main(string[] args)
    {
        RunSamples();
    }

    /// <summary>
    /// 執行 LeetCode 219 的固定測資，逐案比較兩種解法與預期結果。
    /// </summary>
    /// <remarks>
    /// 測資涵蓋三個官方案例，以及 k 為零、距離恰為 k、距離超出 k 與負數元素等邊界情境。
    /// </remarks>
    private static void RunSamples()
    {
        Program solution = new Program();
        (string Name, int[] Nums, int K, bool Expected)[] samples =
        [
            ("官方案例 1：重複元素距離等於 k", [1, 2, 3, 1], 3, true),
            ("官方案例 2：相鄰重複元素", [1, 0, 1, 1], 1, true),
            ("官方案例 3：所有重複元素距離都超出 k", [1, 2, 3, 1, 2, 3], 2, false),
            ("邊界：k = 0", [1, 1], 0, false),
            ("邊界：距離恰為 k", [1, 2, 1], 2, true),
            ("邊界：距離超出 k", [1, 2, 1], 1, false),
            ("邊界：包含負數元素", [-1, 2, -1], 2, true)
        ];

        int passedChecks = 0;

        Console.WriteLine("LeetCode 219 - Contains Duplicate II");
        Console.WriteLine("==================================================");

        for (int index = 0; index < samples.Length; index++)
        {
            (string name, int[] nums, int k, bool expected) = samples[index];
            bool hashMapResult = solution.ContainsNearbyDuplicate(nums, k);
            bool slidingWindowResult = solution.ContainsNearbyDuplicate2(nums, k);
            bool hashMapPassed = hashMapResult == expected;
            bool slidingWindowPassed = slidingWindowResult == expected;

            passedChecks += hashMapPassed ? 1 : 0;
            passedChecks += slidingWindowPassed ? 1 : 0;

            Console.WriteLine($"[{index + 1}] {name}");
            Console.WriteLine($"nums = [{string.Join(", ", nums)}], k = {k}");
            Console.WriteLine($"預期結果：{expected}");
            Console.WriteLine($"方法一（雜湊表）：{hashMapResult} ({(hashMapPassed ? "PASS" : "FAIL")})");
            Console.WriteLine($"方法二（滑動視窗）：{slidingWindowResult} ({(slidingWindowPassed ? "PASS" : "FAIL")})");
            Console.WriteLine();
        }

        Console.WriteLine($"總結：{passedChecks}/{samples.Length * 2} 項驗證通過");
    }

    /// <summary>
    /// 方法一：以 <see cref="Dictionary{TKey, TValue}"/> 記錄每個數值最近出現的索引，
    /// 若與目前索引的距離小於或等於 <paramref name="k"/>，即回傳 <see langword="true"/>。
    /// </summary>
    /// <remarks>
    /// 使用字典保存每個數值最近出現的索引；輸入需符合題目限制的非 null 整數陣列與非負 k。
    /// </remarks>
    /// <param name="nums">要檢查的整數陣列。</param>
    /// <param name="k">允許的兩個相同元素索引距離上限。</param>
    /// <returns>若存在相同元素且其索引距離小於或等於 k，則回傳 <see langword="true"/>；否則回傳 <see langword="false"/>。</returns>
    public bool ContainsNearbyDuplicate(int[] nums, int k)
    {
        Dictionary<int, int> dic = new Dictionary<int, int>();
        int length = nums.Length;
        for (int i = 0; i < length; i++)
        {
            int num = nums[i];
            // 字典只保留最近索引，才能以最小的可能距離判斷目前元素是否符合條件。
            if (dic.ContainsKey(num) && i - dic[num] <= k)
            {
                return true;
            }

            if (dic.ContainsKey(num))
            {
                // 目前索引較新；覆寫後可讓下一次比較使用最近的相同元素。
                dic[num] = i;
            }
            else
            {
                dic.Add(num, i);
            }
        }
        return false;
    }

    /// <summary>
    /// 方法二：以 <see cref="HashSet{T}"/> 維持距離目前索引不超過 <paramref name="k"/> 的元素；
    /// 加入目前值失敗時，即找到符合條件的重複元素。
    /// </summary>
    /// <remarks>
    /// 集合只保留目前索引之前、距離不超過 k 的元素；輸入需符合題目限制的非 null 整數陣列與非負 k。
    /// </remarks>
    /// <param name="nums">要檢查的整數陣列。</param>
    /// <param name="k">允許的兩個相同元素索引距離上限。</param>
    /// <returns>若任一滑動視窗內出現重複元素，則回傳 <see langword="true"/>；否則回傳 <see langword="false"/>。</returns>
    public bool ContainsNearbyDuplicate2(int[] nums, int k)
    {
        ISet<int> set = new HashSet<int>();
        int length = nums.Length;
        for (int i = 0; i < length; i++)
        {
            if (i > k)
            {
                // 移除距離已大於 k 的元素，讓集合恰好代表可與目前索引配對的視窗。
                set.Remove(nums[i - k - 1]);
            }

            if (!set.Add(nums[i]))
            {
                return true;
            }
        }
        return false;
    }
}
