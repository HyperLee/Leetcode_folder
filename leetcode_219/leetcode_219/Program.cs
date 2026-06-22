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
    /// 方法一：哈希表
    /// i - dic1[num] <= k
    /// i是迴圈當前第i位置
    /// dic1[num]: 取出 dic中num的存放位置是多少
    /// 運算是否小於等於k
    /// 
    /// 从左到右遍历数组 nums，当遍历到下标 i 时，如果存在下标 j<i 使得 nums[i]=nums[j]，则当 i−j≤k 时即找到了两个符合要
    /// 求的下标 j 和 i。
    /// 如果在下标 i 之前存在多个元素都和 nums[i] 相等，为了判断是否存在满足 nums[i]=nums[j] 且 i−j≤k 的下标 j，应该在这些
    /// 元素中寻找下标最大的元素，将最大下标记为 j，判断 i−j≤k 是否成立。
    /// 如果 i−j≤k，则找到了两个符合要求的下标 j 和 i；如果 i−j>k，则在下标 i 之前不存在任何元素满足与 nums[i] 相等且下
    /// 标差的绝对值不超过 k，理由如下。
    /// 假设存在下标 j′满足 j′<j<i 且 nums[j′]=nums[j]=nums[i]，则 i−j′>i−j，由于 i−j>k，因此必有 i−j′ >k。
    /// 因此，当遍历到下标 i 时，如果在下标 i 之前存在与 nums[i] 相等的元素，应该在这些元素中寻找最大的下标 j，判断 i−j≤k 是
    /// 否成立。
    /// 可以使用哈希表记录每个元素的最大下标。从左到右遍历数组 nums，当遍历到下标 i 时，进行如下操作：
    /// 1. 如果哈希表中已经存在和 nums[i] 相等的元素且该元素在哈希表中记录的下标 j 满足 i−j≤k，返回 true；
    /// 2.将 nums[i] 和下标 i 存入哈希表，此时 i 是 nums[i] 的最大下标。
    /// 上述两步操作的顺序不能改变，因为当遍历到下标 i 时，只能在下标 i 之前的元素中寻找与当前元素相等的元素及该元素的最大下
    /// 标。
    /// 当遍历结束时，如果没有遇到两个相等元素的下标差的绝对值不超过 k，返回 false。
    /// 
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
    /// 方法二：滑动窗口
    /// 考虑数组 nums 中的每个长度不超过 k+1 的滑动窗口，同一个滑动窗口中的任意两个下标差的绝对值不超过 k。如果存在一个滑
    /// 动窗口，其中有重复元素，则存在两个不同的下标 i 和 j 满足 nums[i]=nums[j] 且 ∣i−j∣≤k。如果所有滑动窗口中都没有重复
    /// 元素，则不存在符合要求的下标。因此，只要遍历每个滑动窗口，判断滑动窗口中是否有重复元素即可。
    /// 如果一个滑动窗口的结束下标是 i，则该滑动窗口的开始下标是 max(0,i−k)。可以使用哈希集合存储滑动窗口中的元素。从左到
    /// 右遍历数组 nums，当遍历到下标 i 时，具体操作如下：
    /// 1. 如果 i>k，则下标 i−k−1 处的元素被移出滑动窗口，因此将 nums[i−k−1] 从哈希集合中删除；
    /// 2. 判断 nums[i] 是否在哈希集合中，如果在哈希集合中则在同一个滑动窗口中有重复元素，返回 true，如果不在哈希集合中则将
    /// 其加入哈希集合。
    /// 当遍历结束时，如果所有滑动窗口中都没有重复元素，返回 false。
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
