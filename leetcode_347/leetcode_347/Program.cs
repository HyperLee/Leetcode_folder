namespace leetcode_347;

class Program
{
    /// <summary>
    /// 347. Top K Frequent Elements
    /// https://leetcode.com/problems/top-k-frequent-elements/description/
    /// 347. 前 K 個高頻元素
    /// https://leetcode.cn/problems/top-k-frequent-elements/description/
    ///
    /// Given an integer array nums and an integer k, return the k most frequent elements.
    /// You may return the answer in any order.
    ///
    /// 給定一個整數陣列 nums 與一個整數 k，請回傳出現頻率最高的 k 個元素。
    /// 你可以以任意順序回傳答案。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        bool allPassed = true;

        allPassed &= RunSample(
            solution,
            1,
            new int[] { 1, 1, 1, 2, 2, 3 },
            2,
            new int[] { 1, 2 });
        allPassed &= RunSample(
            solution,
            2,
            new int[] { 1 },
            1,
            new int[] { 1 });
        allPassed &= RunSample(
            solution,
            3,
            new int[] { 1, 2, 1, 2, 1, 2, 3, 1, 3, 2 },
            2,
            new int[] { 1, 2 });
        allPassed &= RunSample(
            solution,
            4,
            new int[] { -1, -1, -1, -2, -2, -3, -3, -3, -3 },
            2,
            new int[] { -3, -1 });

        if (!allPassed)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 使用桶排序找出出現頻率最高的前 <paramref name="k"/> 個整數。
    /// 先以 Dictionary 統計頻率，再以頻率作為桶索引，最後由高頻率向低頻率取值，
    /// 因此不需要依元素數量進行比較排序。
    /// </summary>
    /// <param name="nums">符合題目限制的非空整數陣列。</param>
    /// <param name="k">要回傳的元素數量，必須介於 1 與陣列相異元素數量之間。</param>
    /// <returns>出現頻率最高的前 <paramref name="k"/> 個元素；元素順序不保證。</returns>
    public int[] TopKFrequent(int[] nums, int k)
    {
        // 先建立「元素 -> 出現次數」的對照表，供後續依頻率分桶。
        Dictionary<int, int> counts = new Dictionary<int, int>();

        foreach (int number in nums)
        {
            if (counts.ContainsKey(number))
            {
                counts[number]++;
            }
            else
            {
                counts[number] = 1;
            }
        }

        int maxCount = counts.Values.Max();

        // 桶的索引就是頻率；同頻率的元素可放在同一個 List 中。
        List<int>[] buckets = new List<int>[maxCount + 1];

        for (int i = 0; i < buckets.Length; i++)
        {
            buckets[i] = new List<int>();
        }

        foreach (KeyValuePair<int, int> item in counts)
        {
            int number = item.Key;
            int frequency = item.Value;

            buckets[frequency].Add(number);
        }

        // 從最大頻率反向掃描，先取得的元素必定比尚未掃描到的元素更常出現。
        int[] result = new int[k];
        int resultIndex = 0;

        for (int frequency = maxCount; resultIndex < k; frequency--)
        {
            foreach (int number in buckets[frequency])
            {
                result[resultIndex] = number;
                resultIndex++;

                if (resultIndex == k)
                {
                    break;
                }
            }
        }

        return result;
    }

    /// <summary>
    /// 使用 Dictionary 統計頻率後，依頻率由高到低排序以找出前 <paramref name="k"/> 個整數。
    /// 此方法直接利用排序建立優先順序，概念直觀，但排序步驟不符合題目要求優於
    /// <c>O(n log n)</c> 的 follow-up。
    /// </summary>
    /// <param name="nums">符合題目限制的非空整數陣列。</param>
    /// <param name="k">要回傳的元素數量，必須介於 1 與陣列相異元素數量之間。</param>
    /// <returns>出現頻率最高的前 <paramref name="k"/> 個元素；元素順序不保證。</returns>
    public static int[] TopKFrequent2(int[] nums, int k)
    {
        Dictionary<int, int> counts = new Dictionary<int, int>();

        foreach (int number in nums)
        {
            if (counts.ContainsKey(number))
            {
                counts[number]++;
            }
            else
            {
                counts.Add(number, 1);
            }
        }

        // 依頻率遞減排列後，列舉到第 k 個元素即可停止。
        IEnumerable<KeyValuePair<int, int>> sorted =
            counts.OrderByDescending(item => item.Value);

        int[] result = new int[k];
        int resultIndex = 0;

        foreach (KeyValuePair<int, int> item in sorted)
        {
            result[resultIndex] = item.Key;
            resultIndex++;

            if (resultIndex == k)
            {
                break;
            }
        }

        return result;
    }

    /// <summary>
    /// 執行一組範例，並以排序後的集合比較兩種解法結果與預期值。
    /// 題目允許回傳任意順序，因此比較前正規化結果可避免把順序差異誤判為失敗。
    /// </summary>
    /// <param name="solution">用於呼叫桶排序解法的執行個體。</param>
    /// <param name="caseNumber">輸出時顯示的案例編號。</param>
    /// <param name="nums">要測試的符合題目限制的整數陣列。</param>
    /// <param name="k">要取得的高頻元素數量。</param>
    /// <param name="expected">預期的高頻元素集合。</param>
    /// <returns>兩種解法都與預期集合相同時回傳 <see langword="true"/>。</returns>
    private static bool RunSample(Program solution, int caseNumber, int[] nums, int k, int[] expected)
    {
        int[] bucketResult = solution.TopKFrequent(nums, k);
        int[] sortingResult = TopKFrequent2(nums, k);
        int[] normalizedExpected = NormalizeResult(expected);
        int[] normalizedBucketResult = NormalizeResult(bucketResult);
        int[] normalizedSortingResult = NormalizeResult(sortingResult);

        bool bucketPassed = normalizedBucketResult.SequenceEqual(normalizedExpected);
        bool sortingPassed = normalizedSortingResult.SequenceEqual(normalizedExpected);

        Console.WriteLine($"案例 {caseNumber}");
        Console.WriteLine($"輸入 nums = [{FormatArray(nums)}], k = {k}");
        Console.WriteLine($"預期結果（排序後）: [{FormatArray(normalizedExpected)}]");
        Console.WriteLine($"桶排序法: [{FormatArray(normalizedBucketResult)}] {(bucketPassed ? "PASS" : "FAIL")}");
        Console.WriteLine($"字典排序法: [{FormatArray(normalizedSortingResult)}] {(sortingPassed ? "PASS" : "FAIL")}");

        if (caseNumber < 4)
        {
            Console.WriteLine();
        }

        return bucketPassed && sortingPassed;
    }

    /// <summary>
    /// 建立由小到大排列的結果副本，讓可任意排序的答案能以集合內容比較。
    /// </summary>
    /// <param name="result">符合題目限制的解法回傳陣列。</param>
    /// <returns>已由小到大排序的新陣列，不會修改原始結果。</returns>
    private static int[] NormalizeResult(int[] result)
    {
        return result.OrderBy(number => number).ToArray();
    }

    /// <summary>
    /// 將整數序列格式化為以逗號與空白分隔的可讀文字。
    /// </summary>
    /// <param name="numbers">要顯示的整數序列。</param>
    /// <returns>不含方括號的格式化元素文字。</returns>
    private static string FormatArray(IEnumerable<int> numbers)
    {
        return string.Join(", ", numbers);
    }
}
