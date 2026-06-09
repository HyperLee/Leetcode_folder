namespace leetcode_167;

class Program
{
    /// <summary>
    /// 167. Two Sum II - Input Array Is Sorted
    /// https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/description/
    /// 167. 两数之和 II - 输入有序数组
    /// https://leetcode.cn/problems/two-sum-ii-input-array-is-sorted/description/
    ///
    /// <para>English</para>
    /// <para>Given a 1-indexed array of integers numbers that is already sorted in non-decreasing order, find two numbers such that they add up to a specific target number. Let these two numbers be numbers[index1] and numbers[index2] where 1 &lt;= index1 &lt; index2 &lt;= numbers.length.</para>
    /// <para>Return the indices of the two numbers index1 and index2, each incremented by one, as an integer array [index1, index2] of length 2.</para>
    /// <para>The tests are generated such that there is exactly one solution. You may not use the same element twice.</para>
    /// <para>Your solution must use only constant extra space.</para>
    ///
    /// <para>繁體中文</para>
    /// <para>給定一個 1-indexed 的整數陣列 numbers，該陣列已依非遞減順序排序。請找出兩個數字，使它們的總和等於指定的 target。設這兩個數字分別為 numbers[index1] 與 numbers[index2]，其中 1 &lt;= index1 &lt; index2 &lt;= numbers.length。</para>
    /// <para>請回傳這兩個數字的索引 index1 和 index2，並以長度為 2 的整數陣列 [index1, index2] 表示。</para>
    /// <para>測試資料保證恰好只有一組解。你不能重複使用同一個元素。</para>
    /// <para>你的解法必須只使用常數額外空間。</para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solver = new Program();
        (int[] Numbers, int Target)[] sampleCases =
        [
            (new int[] { 2, 7, 11, 15 }, 9),
            (new int[] { 2, 3, 4 }, 6),
            (new int[] { -1, 0 }, -1),
            (new int[] { 1, 1, 3, 4 }, 2)
        ];

        Console.WriteLine("LeetCode 167 - Two Sum II");
        Console.WriteLine();

        for (int i = 0; i < sampleCases.Length; i++)
        {
            RunSampleCase(solver, i + 1, sampleCases[i].Numbers, sampleCases[i].Target);
        }
    }

    /// <summary>
    /// 依序執行三種解法並輸出同一筆測資的結果，方便直接觀察不同策略是否得到一致的 1-based 索引答案。
    /// 輸入的 numbers 必須已依非遞減順序排序，target 需符合題目保證的唯一解條件。
    /// 輸出會列出原始陣列、目標值，以及 Dictionary、Binary Search、Two Pointers 三種方法的回傳結果。
    /// </summary>
    /// <param name="solver">提供解法方法的執行個體。</param>
    /// <param name="caseNumber">顯示用途的案例編號。</param>
    /// <param name="numbers">已排序的整數陣列。</param>
    /// <param name="target">欲湊出的目標總和。</param>
    private static void RunSampleCase(Program solver, int caseNumber, int[] numbers, int target)
    {
        int[] dictionaryResult = solver.TwoSum(numbers, target);
        int[] binarySearchResult = solver.TwoSum2(numbers, target);
        int[] twoPointersResult = solver.TwoSum3(numbers, target);

        Console.WriteLine($"Case {caseNumber}");
        Console.WriteLine($"numbers = {FormatArray(numbers)}, target = {target}");
        Console.WriteLine($"Dictionary    -> {FormatArray(dictionaryResult)}");
        Console.WriteLine($"Binary Search -> {FormatArray(binarySearchResult)}");
        Console.WriteLine($"Two Pointers  -> {FormatArray(twoPointersResult)}");
        Console.WriteLine();
    }

    /// <summary>
    /// 將整數陣列轉成 README 與主控台都容易對照的字串格式。
    /// 輸入可為任意長度的整數陣列，輸出固定為 [a, b, c] 形式。
    /// </summary>
    /// <param name="values">要格式化的整數陣列。</param>
    /// <returns>以中括號包住、逗號分隔的陣列字串。</returns>
    private static string FormatArray(int[] values)
    {
        return $"[{string.Join(", ", values)}]";
    }

    /// <summary>
    /// 使用 Dictionary 記錄先前看過的數值與其 1-based 索引，對每個元素即時尋找是否已存在對應補數。
    /// 輸入條件是 numbers 必須已排序，且依題意可假設恰好只有一組解、不可重複使用同一元素。
    /// 若找到解，回傳對應的 1-based 索引；若輸入不符合題目保證而未找到解，則防禦性回傳 [-1, -1]。
    /// </summary>
    /// <param name="numbers">已依非遞減順序排序的整數陣列。</param>
    /// <param name="target">兩數相加後必須等於的目標值。</param>
    /// <returns>長度為 2 的 1-based 索引陣列。</returns>
    public int[] TwoSum(int[] numbers, int target)
    {
        Dictionary<int, int> visitedNumbers = new Dictionary<int, int>();

        for (int i = 0; i < numbers.Length; i++)
        {
            int complement = target - numbers[i];

            // 只要補數已經出現過，就能立刻組成答案。
            if (visitedNumbers.TryGetValue(complement, out int previousIndex))
            {
                return new int[] { previousIndex, i + 1 };
            }

            // 只保留第一次出現的索引，避免重複值覆蓋掉較靠左的合法答案位置。
            if (!visitedNumbers.ContainsKey(numbers[i]))
            {
                visitedNumbers.Add(numbers[i], i + 1);
            }
        }

        return new int[] { -1, -1 };
    }

    /// <summary>
    /// 先固定左側元素，再利用已排序特性於右側區間做二分搜尋，尋找 target - numbers[i] 是否存在。
    /// 輸入條件是 numbers 必須已排序，且答案必定位於當前索引右側，因此每次搜尋區間都從 i + 1 開始。
    /// 若找到解，回傳對應的 1-based 索引；若輸入不符合題目保證而未找到解，則防禦性回傳 [-1, -1]。
    /// </summary>
    /// <param name="numbers">已依非遞減順序排序的整數陣列。</param>
    /// <param name="target">兩數相加後必須等於的目標值。</param>
    /// <returns>長度為 2 的 1-based 索引陣列。</returns>
    public int[] TwoSum2(int[] numbers, int target)
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            int low = i + 1;
            int high = numbers.Length - 1;
            int complement = target - numbers[i];

            while (low <= high)
            {
                int mid = low + (high - low) / 2;

                // 排序陣列可讓我們根據中間值與補數的大小關係快速縮小搜尋區間。
                if (numbers[mid] == complement)
                {
                    return new int[] { i + 1, mid + 1 };
                }

                if (numbers[mid] > complement)
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }
        }

        return new int[] { -1, -1 };
    }

    /// <summary>
    /// 使用雙指標從陣列兩端往中間收斂，藉由目前總和與 target 的大小比較來決定移動哪一側指標。
    /// 輸入條件是 numbers 必須已排序，這樣左指標右移會讓總和變大，右指標左移會讓總和變小，才能保證不漏解。
    /// 若找到解，回傳對應的 1-based 索引；若輸入不符合題目保證而未找到解，則防禦性回傳 [-1, -1]。
    /// </summary>
    /// <param name="numbers">已依非遞減順序排序的整數陣列。</param>
    /// <param name="target">兩數相加後必須等於的目標值。</param>
    /// <returns>長度為 2 的 1-based 索引陣列。</returns>
    public int[] TwoSum3(int[] numbers, int target)
    {
        int low = 0;
        int high = numbers.Length - 1;

        while (low < high)
        {
            int sum = numbers[low] + numbers[high];

            if (sum == target)
            {
                return new int[] { low + 1, high + 1 };
            }

            // 總和偏小就增加左值，總和偏大就減少右值，藉此持續縮小搜尋範圍。
            if (sum < target)
            {
                low++;
            }
            else
            {
                high--;
            }
        }

        return new int[] { -1, -1 };
    }
}
