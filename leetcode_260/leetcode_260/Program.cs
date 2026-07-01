namespace leetcode_260;

class Program
{
    /// <summary>
    /// 260. Single Number III
    /// https://leetcode.com/problems/single-number-iii/description/
    /// 260. 只出现一次的数字 III
    /// https://leetcode.cn/problems/single-number-iii/description/
    ///
    /// English:
    /// Given an integer array nums, in which exactly two elements appear only once and all the other elements appear exactly twice.
    /// Find the two elements that appear only once. You can return the answer in any order.
    /// You must write an algorithm that runs in linear runtime complexity and uses only constant extra space.
    ///
    /// 繁體中文:
    /// 給定一個整數陣列 nums，其中恰好有兩個元素只出現一次，其餘所有元素都恰好出現兩次。
    /// 找出這兩個只出現一次的元素。答案可以任意順序回傳。
    /// 你必須撰寫一個執行時間複雜度為線性，且只使用常數額外空間的演算法。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        RunSamples();
    }

    /// <summary>
    /// 執行固定範例資料，對照兩種解法在排序後是否都能得到預期的兩個唯一數字。
    /// 輸入資料皆符合題目條件：恰好兩個元素只出現一次，其餘元素出現兩次；輸出為主控台驗證報告。
    /// </summary>
    private static void RunSamples()
    {
        Program solution = new Program();
        SampleCase[] samples =
        [
            new SampleCase("官方範例", [1, 2, 1, 3, 2, 5], [3, 5]),
            new SampleCase("兩個元素皆唯一", [-1, 0], [-1, 0]),
            new SampleCase("包含 0 與正數", [0, 1], [0, 1]),
            new SampleCase("唯一數分散在中後段", [2, 1, 2, 3, 4, 1], [3, 4]),
            new SampleCase("包含 int.MinValue", [int.MinValue, 4, 4, 9, 9, 0], [int.MinValue, 0])
        ];

        int passed = 0;
        int total = samples.Length * 2;

        Console.WriteLine("LeetCode 260 - Single Number III");
        Console.WriteLine("可執行範例驗證");
        Console.WriteLine();

        for (int i = 0; i < samples.Length; i++)
        {
            SampleCase sample = samples[i];
            int[] expected = SortedCopy(sample.Expected);
            int[] method1 = SortedCopy(solution.SingleNumber(sample.Nums));
            int[] method2 = SortedCopy(solution.SingleNumber2(sample.Nums));

            bool method1Passed = AreEqual(method1, expected);
            bool method2Passed = AreEqual(method2, expected);

            if (method1Passed)
            {
                passed++;
            }

            if (method2Passed)
            {
                passed++;
            }

            Console.WriteLine($"案例 {i + 1}：{sample.Name}");
            Console.WriteLine($"輸入 nums = {FormatArray(sample.Nums)}");
            Console.WriteLine($"預期答案（排序後）= {FormatArray(expected)}");
            Console.WriteLine($"方法一 哈希表 = {FormatArray(method1)} {(method1Passed ? "PASS" : "FAIL")}");
            Console.WriteLine($"方法二 位元運算 = {FormatArray(method2)} {(method2Passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        Console.WriteLine($"總結：{passed}/{total} 項驗證通過");
    }

    /// <summary>
    /// 方法一：使用雜湊表統計每個整數出現次數，作為直覺且容易檢查的解法。
    /// 輸入陣列需符合題意：恰好兩個元素只出現一次，其餘元素都出現兩次；輸出為兩個只出現一次的元素，順序不固定。
    /// </summary>
    /// <param name="nums">符合題目條件的整數陣列。</param>
    /// <returns>所有出現次數為 1 的整數。</returns>
    public int[] SingleNumber(int[] nums)
    {
        List<int> singles = new List<int>();
        Dictionary<int, int> counts = new Dictionary<int, int>();

        // 先統計每個數字的出現次數，讓後續只需挑出計數為 1 的元素。
        for (int i = 0; i < nums.Length; i++)
        {
            if (counts.ContainsKey(nums[i]))
            {
                counts[nums[i]]++;
            }
            else
            {
                counts.Add(nums[i], 1);
            }
        }

        foreach (KeyValuePair<int, int> item in counts)
        {
            if (item.Value == 1)
            {
                singles.Add(item.Key);
            }
        }

        return singles.ToArray();
    }

    /// <summary>
    /// 方法二：使用位元 XOR 抵消成對數字，再用最低有效位將兩個唯一數分到不同群組。
    /// 輸入陣列需符合題意：恰好兩個元素只出現一次，其餘元素都出現兩次；輸出為兩個只出現一次的元素，順序不固定。
    /// </summary>
    /// <param name="nums">符合題目條件的整數陣列。</param>
    /// <returns>兩個只出現一次的整數。</returns>
    public int[] SingleNumber2(int[] nums)
    {
        int xorsum = 0;
        foreach (int num in nums)
        {
            // 相同數字 XOR 兩次會歸零，最後只留下兩個唯一數字的 XOR 結果。
            xorsum ^= num;
        }

        // xorsum 可能是 int.MinValue，直接取負會溢位；此時它本身就只保留最高位。
        int lsb = (xorsum == int.MinValue ? xorsum : xorsum & (-xorsum));
        int type1 = 0;
        int type2 = 0;
        foreach (int num in nums)
        {
            // 依照最低有效差異位分組，成對數字會進入同一組並在各組內互相抵消。
            if ((num & lsb) != 0)
            {
                type1 ^= num;
            }
            else
            {
                type2 ^= num;
            }
        }
        return new int[] { type1, type2 };
    }

    /// <summary>
    /// 複製並排序答案陣列，讓主控台輸出與驗證不受題目允許的任意回傳順序影響。
    /// 輸入可為任意整數陣列；輸出為排序後的新陣列，不會修改原始輸入。
    /// </summary>
    /// <param name="values">要複製並排序的整數陣列。</param>
    /// <returns>排序後的新整數陣列。</returns>
    private static int[] SortedCopy(int[] values)
    {
        int[] sorted = (int[])values.Clone();
        Array.Sort(sorted);
        return sorted;
    }

    /// <summary>
    /// 比較兩個整數陣列內容是否完全相同，用於範例驗證。
    /// 輸入陣列應已依相同規則排序；輸出為兩者長度與每個位置值是否一致。
    /// </summary>
    /// <param name="left">左側整數陣列。</param>
    /// <param name="right">右側整數陣列。</param>
    /// <returns>兩個陣列內容相同時回傳 true，否則回傳 false。</returns>
    private static bool AreEqual(int[] left, int[] right)
    {
        if (left.Length != right.Length)
        {
            return false;
        }

        for (int i = 0; i < left.Length; i++)
        {
            if (left[i] != right[i])
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 將整數陣列格式化為 README 與主控台都容易閱讀的字串。
    /// 輸入可為任意整數陣列；輸出格式為以逗號分隔的方括號內容。
    /// </summary>
    /// <param name="values">要格式化的整數陣列。</param>
    /// <returns>格式化後的陣列字串。</returns>
    private static string FormatArray(int[] values)
    {
        return $"[{string.Join(", ", values)}]";
    }

    /// <summary>
    /// 表示一筆可執行範例，包含案例名稱、輸入陣列與排序前的預期答案。
    /// 輸入資料由範例建構時提供；輸出由範例驗證流程讀取。
    /// </summary>
    /// <param name="Name">案例名稱。</param>
    /// <param name="Nums">符合題目條件的輸入陣列。</param>
    /// <param name="Expected">兩個只出現一次的預期數字。</param>
    private sealed record SampleCase(string Name, int[] Nums, int[] Expected);
}
