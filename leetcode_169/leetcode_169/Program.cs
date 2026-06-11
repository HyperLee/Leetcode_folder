namespace leetcode_169;

class Program
{
    /// <summary>
    /// 169. Majority Element
    /// https://leetcode.com/problems/majority-element/description/
    /// 169. 多数元素
    /// https://leetcode.cn/problems/majority-element/description/
    ///
    /// English:
    /// Given an array nums of size n, return the majority element.
    /// The majority element is the element that appears more than floor(n / 2) times.
    /// You may assume that the majority element always exists in the array.
    ///
    /// 繁體中文：
    /// 給定一個大小為 n 的陣列 nums，回傳其中的多數元素。
    /// 多數元素是指在陣列中出現次數大於 floor(n / 2) 的元素。
    /// 你可以假設多數元素一定存在於陣列中。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        (int[] Numbers, int Expected)[] sampleCases =
        [
            (new int[] { 3, 2, 3 }, 3),
            (new int[] { 2, 2, 1, 1, 1, 2, 2 }, 2),
            (new int[] { 1 }, 1),
            (new int[] { 6, 5, 5 }, 5)
        ];

        Console.WriteLine("LeetCode 169 - Majority Element");
        Console.WriteLine();

        for (int i = 0; i < sampleCases.Length; i++)
        {
            RunSampleCase(i + 1, sampleCases[i].Numbers, sampleCases[i].Expected);
        }
    }

    /// <summary>
    /// 執行單一測試案例，列出原始輸入、預期答案，以及三種解法的實際結果，方便直接比對不同策略是否都得到相同的多數元素。
    /// 解題概念是以同一份測資同步驗證 Dictionary、Sorting、Boyer-Moore 三種方法；輸入需符合題目前提，也就是陣列非空且一定存在多數元素。
    /// 輸出結果會寫到主控台，內容包含案例編號、陣列內容、預期值與每種解法的回傳值。
    /// </summary>
    /// <param name="caseNumber">顯示用途的案例編號。</param>
    /// <param name="numbers">題目輸入的整數陣列，保證非空且存在多數元素。</param>
    /// <param name="expected">此案例的預期多數元素。</param>
    private static void RunSampleCase(int caseNumber, int[] numbers, int expected)
    {
        int dictionaryResult = MajorityElementByDictionary(numbers);
        int sortingResult = MajorityElementBySorting((int[])numbers.Clone());
        int boyerMooreResult = MajorityElementByBoyerMoore(numbers);

        Console.WriteLine($"Case {caseNumber}");
        Console.WriteLine($"nums         = {FormatArray(numbers)}");
        Console.WriteLine($"Expected     -> {expected}");
        Console.WriteLine($"Dictionary   -> {dictionaryResult}");
        Console.WriteLine($"Sorting      -> {sortingResult}");
        Console.WriteLine($"Boyer-Moore  -> {boyerMooreResult}");
        Console.WriteLine();
    }

    /// <summary>
    /// 將整數陣列格式化成易於閱讀的字串，讓主程式輸出與 README 範例能維持一致。
    /// 這個 helper 不處理解題邏輯，只假設輸入是任意長度的整數陣列，並回傳以中括號包住、逗號分隔的結果。
    /// 輸出格式固定為 [a, b, c] 形式，適合直接顯示在主控台或文件中。
    /// </summary>
    /// <param name="values">要格式化的整數陣列。</param>
    /// <returns>以中括號包住、逗號分隔的陣列字串。</returns>
    private static string FormatArray(int[] values)
    {
        return $"[{string.Join(", ", values)}]";
    }

    /// <summary>
    /// 使用 Dictionary 統計每個數字出現的次數，當某個數字的頻率首次超過 floor(n / 2) 時立即回傳它。
    /// 解題概念是以雜湊表保存每個值的累計次數；輸入必須是非空陣列，且依題意保證一定存在多數元素，因此不需要額外處理無解情況。
    /// 輸出結果為陣列中的多數元素，時間複雜度為 O(n)，空間複雜度為 O(n)。
    /// </summary>
    /// <param name="nums">題目輸入的整數陣列，保證非空且存在多數元素。</param>
    /// <returns>在陣列中出現次數超過一半的數字。</returns>
    public static int MajorityElementByDictionary(int[] nums)
    {
        Dictionary<int, int> counts = new Dictionary<int, int>();
        int threshold = nums.Length / 2;

        foreach (int num in nums)
        {
            if (counts.ContainsKey(num))
            {
                counts[num]++;
            }
            else
            {
                counts.Add(num, 1);
            }

            // 一旦某個值的出現次數超過門檻，就已經可以確定它是多數元素。
            if (counts[num] > threshold)
            {
                return num;
            }
        }

        return nums[0];
    }

    /// <summary>
    /// 先將陣列排序，再直接回傳中間位置的元素，利用多數元素必定覆蓋排序後中位區段的性質求解。
    /// 解題概念是：若某個值出現次數超過一半，排序後它一定會佔住索引 nums.Length / 2；輸入必須是非空陣列，且允許此方法原地排序。
    /// 輸出結果為排序後中間位置的數字，也就是題目保證存在的多數元素，時間複雜度為 O(n log n)，空間複雜度取決於排序實作。
    /// </summary>
    /// <param name="nums">題目輸入的整數陣列，保證非空且存在多數元素。</param>
    /// <returns>排序後位於中間位置的多數元素。</returns>
    public static int MajorityElementBySorting(int[] nums)
    {
        // 多數元素出現超過一半，排序後一定會覆蓋陣列的中間索引。
        Array.Sort(nums);
        return nums[nums.Length / 2];
    }

    /// <summary>
    /// 使用 Boyer-Moore Voting Algorithm 透過候選人與票數互相抵消的方式，在一次走訪中找出多數元素。
    /// 解題概念是把不同元素彼此配對抵消，最後留下的候選人必定是多數元素；輸入必須是非空陣列，且題目保證多數元素確實存在。
    /// 輸出結果為最終留下的候選值，時間複雜度為 O(n)，空間複雜度為 O(1)。
    /// </summary>
    /// <param name="nums">題目輸入的整數陣列，保證非空且存在多數元素。</param>
    /// <returns>經過投票抵消後留下的多數元素。</returns>
    public static int MajorityElementByBoyerMoore(int[] nums)
    {
        int candidate = 0;
        int count = 0;

        foreach (int num in nums)
        {
            if (count == 0)
            {
                candidate = num;
            }

            // 相同值幫候選人加票，不同值互相抵消，最後留下的就是多數元素。
            if (num == candidate)
            {
                count++;
            }
            else
            {
                count--;
            }
        }

        return candidate;
    }
}
