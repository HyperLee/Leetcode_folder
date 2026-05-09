namespace leetcode_136;

class Program
{
    /// <summary>
    /// 136. Single Number
    /// https://leetcode.com/problems/single-number/description/
    /// 136. 只出现一次的数字
    /// https://leetcode.cn/problems/single-number/description/
    /// 
    /// English:
    /// Given a non-empty array of integers nums, every element appears twice except for one. Find that single one.
    /// You must implement a solution with a linear runtime complexity and use only constant extra space.
    /// 
    /// 繁體中文：
    /// 給定一個非空的整數陣列 nums，除了某個元素只出現一次以外，其餘每個元素都會出現兩次。請找出那個只出現一次的元素。
    /// 你必須實作一個具備線性時間複雜度，且只使用常數額外空間的解法。
    /// 
    /// Main 用途：
    /// 建立可直接執行的範例測試資料，並示範本檔案中的三種解法都能找出只出現一次的數字。
    /// 輸入條件：每組 nums 皆為非空整數陣列，且除了答案以外，其餘數字都剛好出現兩次。
    /// 輸出結果：在主控台列印每組測試資料的預期答案與三種解法的實際結果。
    ///
    /// </summary>
    /// <param name="args">命令列參數，本範例未使用。</param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        (int[] Nums, int Expected)[] testCases =
        [
            ([2, 2, 1], 1),
            ([4, 1, 2, 1, 2], 4),
            ([1], 1),
            ([-1, -1, -2], -2),
        ];

        foreach (var (nums, expected) in testCases)
        {
            string input = $"[{string.Join(", ", nums)}]";

            Console.WriteLine($"Input: nums = {input}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"SingleNumber  (Dictionary + LINQ lookup): {solution.SingleNumber(nums)}");
            Console.WriteLine($"SingleNumber2 (Dictionary + foreach):     {solution.SingleNumber2(nums)}");
            Console.WriteLine($"SingleNumber3 (XOR):                      {solution.SingleNumber3(nums)}");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 使用 Dictionary 統計每個數字出現次數，再透過 LINQ 找出出現次數為 1 的數字。
    /// 解題概念：成對出現的數字次數會是 2，唯一答案的次數會是 1，因此統計後即可辨識答案。
    /// 輸入條件：nums 為非空整數陣列，且除了單一答案以外，其餘數字皆出現兩次。
    /// 輸出結果：回傳只出現一次的整數；若輸入不符合題目保證且找不到答案，回傳 0。
    /// </summary>
    /// <param name="nums">待搜尋的整數陣列。</param>
    /// <returns>只出現一次的整數。</returns>
    public int SingleNumber(int[] nums)
    {
        Dictionary<int, int> dict = new Dictionary<int, int>();

        // 先統計每個數字的出現次數，後續即可直接找出次數為 1 的候選答案。
        foreach (int num in nums)
        {
            if (dict.ContainsKey(num))
            {
                dict[num]++;
            }
            else
            {
                dict.Add(num, 1);
            }
        }

        // 題目保證只有一個數字出現一次，因此第一個次數為 1 的 Key 就是答案。
        if (dict.ContainsValue(1))
        {
            return dict.FirstOrDefault(x => x.Value == 1).Key;
        }

        return 0;
    }

    /// <summary>
    /// 使用 Dictionary 統計每個數字出現次數，再手動遍歷 Dictionary 找出出現次數為 1 的數字。
    /// 解題概念：和 <see cref="SingleNumber"/> 相同，差異在於最後查找答案時不使用 LINQ。
    /// 輸入條件：nums 為非空整數陣列，且除了單一答案以外，其餘數字皆出現兩次。
    /// 輸出結果：回傳只出現一次的整數；若輸入不符合題目保證且找不到答案，回傳 0。
    /// </summary>
    /// <param name="nums">待搜尋的整數陣列。</param>
    /// <returns>只出現一次的整數。</returns>
    public int SingleNumber2(int[] nums)
    {
        Dictionary<int, int> dict = new Dictionary<int, int>();

        // 先建立「數字 -> 出現次數」的對照表。
        for (int i = 0; i < nums.Length; i++)
        {
            if (dict.ContainsKey(nums[i]))
            {
                dict[nums[i]]++;
            }
            else
            {
                dict.Add(nums[i], 1);
            }
        }

        // 手動遍歷統計結果，找到次數為 1 的項目即可立即回傳。
        foreach (var num in dict)
        {
            if (num.Value == 1)
            {
                return num.Key;
            }
        }
        return 0;
    }

    /// <summary>
    /// 使用 XOR 位元運算找出只出現一次的數字。
    /// 解題概念：任何數字與自身 XOR 會抵消為 0，任何數字與 0 XOR 會保留原值；因 XOR 具交換律與結合律，所有成對數字會互相抵消，最後剩下唯一答案。
    /// 輸入條件：nums 為非空整數陣列，且除了單一答案以外，其餘數字皆出現兩次。
    /// 輸出結果：回傳只出現一次的整數。
    /// </summary>
    /// <param name="nums">待搜尋的整數陣列。</param>
    /// <returns>只出現一次的整數。</returns>
    public int SingleNumber3(int[] nums)
    {
        int res = 0;

        // 成對數字經過 XOR 會歸零，最後保留的就是只出現一次的數字。
        foreach (var num in nums)
        {
            res ^= num;
        }
        return res;
    }
}
