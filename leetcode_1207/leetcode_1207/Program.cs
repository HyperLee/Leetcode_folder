namespace leetcode_1207;

class Program
{
    /// <summary>
    /// 1207. Unique Number of Occurrences
    /// https://leetcode.com/problems/unique-number-of-occurrences/description/
    /// 1207. 獨一無二的出現次數
    /// https://leetcode.cn/problems/unique-number-of-occurrences/description/
    ///
    /// English:
    /// Given an array of integers arr, return true if the number of occurrences of each value in the array is unique or false otherwise.
    ///
    /// 繁體中文：
    /// 給定一個整數陣列 arr，若陣列中每個數值的出現次數皆為唯一，則回傳 true；否則回傳 false。
    /// </summary>
    /// <remarks>
    /// 使用三組固定測試資料執行雜湊與排序解法，並輸出各解法是否符合預期結果。
    /// </remarks>
    /// <param name="args">Command-line arguments (unused).</param>
    static void Main(string[] args)
    {
        Program solution = new Program();

        RunTestCase(solution, "Example 1", [1, 2, 2, 1, 1, 3], true);
        RunTestCase(solution, "Example 2", [1, 2], false);
        RunTestCase(solution, "Example 3", [-3, 0, 1, -3, 1, 1, 1, -3, 10, 0], true);
    }

    /// <summary>
    /// 使用同一組整數陣列執行兩種解法，比較實際結果與預期結果，並輸出 PASS 或 FAIL。
    /// 輸入必須符合題目條件；此方法不回傳值，而是將測試名稱、輸入與驗證結果寫到主控台。
    /// </summary>
    /// <param name="solution">提供兩種解法的 <see cref="Program"/> 實例。</param>
    /// <param name="name">顯示在主控台上的測試案例名稱。</param>
    /// <param name="input">要驗證的整數陣列。</param>
    /// <param name="expected">此案例預期得到的布林結果。</param>
    private static void RunTestCase(Program solution, string name, int[] input, bool expected)
    {
        bool dictionaryResult = solution.UniqueOccurrences(input);
        bool sortingResult = solution.UniqueOccurrencesBySorting(input);

        Console.WriteLine($"{name}: [{string.Join(", ", input)}], Expected: {expected}");
        Console.WriteLine($"  Dictionary + HashSet: {dictionaryResult} ({(dictionaryResult == expected ? "PASS" : "FAIL")})");
        Console.WriteLine($"  Sorting + HashSet:    {sortingResult} ({(sortingResult == expected ? "PASS" : "FAIL")})");
    }

    /// <summary>
    /// 使用 Dictionary 統計每個整數的出現次數，再以 HashSet 確認所有出現次數皆不重複。
    /// 輸入必須是符合題目限制的非 null、非空整數陣列；若每個數值的出現次數皆唯一則回傳 true，否則回傳 false。
    /// </summary>
    /// <param name="arr">長度為 1 到 1000，且元素值介於 -1000 到 1000 的整數陣列。</param>
    /// <returns>所有出現次數皆不相同時回傳 true；任兩個數值的出現次數相同時回傳 false。</returns>
    public bool UniqueOccurrences(int[] arr)
    {
        // 第一階段：建立「數值 -> 出現次數」對照表。
        Dictionary<int, int> dic = new Dictionary<int, int>();
        foreach (int value in arr)
        {
            if (!dic.ContainsKey(value))
            {
                dic.Add(value, 1);
            }
            else
            {
                dic[value]++;
            }
        }

        // 第二階段：HashSet.Add 回傳 false 代表這個出現次數已被其他數值使用。
        HashSet<int> hashset = new HashSet<int>();
        foreach (KeyValuePair<int, int> pair in dic)
        {
            if (!hashset.Add(pair.Value))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 複製並排序輸入陣列，使相同數值相鄰，再逐段計算出現次數並以 HashSet 檢查次數是否重複。
    /// 輸入必須是符合題目限制的非 null、非空整數陣列；原始陣列不會被修改，結果語意與 <see cref="UniqueOccurrences"/> 相同。
    /// </summary>
    /// <param name="arr">長度為 1 到 1000，且元素值介於 -1000 到 1000 的整數陣列。</param>
    /// <returns>所有出現次數皆不相同時回傳 true；任兩個數值的出現次數相同時回傳 false。</returns>
    public bool UniqueOccurrencesBySorting(int[] arr)
    {
        // 複製後排序，讓相同數值連續排列，同時避免改動呼叫端的輸入。
        int[] sorted = (int[])arr.Clone();
        Array.Sort(sorted);

        HashSet<int> occurrenceCounts = new HashSet<int>();
        int currentCount = 1;

        for (int i = 1; i <= sorted.Length; i++)
        {
            if (i < sorted.Length && sorted[i] == sorted[i - 1])
            {
                currentCount++;
                continue;
            }

            // 到達一段相同數值的尾端；次數若已存在即可提前判定為 false。
            if (!occurrenceCounts.Add(currentCount))
            {
                return false;
            }

            currentCount = 1;
        }

        return true;
    }
}
