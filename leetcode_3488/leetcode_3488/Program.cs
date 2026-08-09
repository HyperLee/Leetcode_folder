namespace leetcode_3488;

class Program
{
    /// <summary>
    /// 3488. Closest Equal Element Queries
    /// https://leetcode.com/problems/closest-equal-element-queries/description/?envType=daily-question&envId=2026-04-16
    /// 3488. 距離最小相等元素查詢
    /// https://leetcode.cn/problems/closest-equal-element-queries/description/?envType=daily-question&envId=2026-04-16
    ///
    /// [EN]
    /// You are given a circular array nums and an array queries.
    /// For each query i, you have to find the following:
    /// The minimum distance between the element at index queries[i] and any other index j
    /// in the circular array, where nums[j] == nums[queries[i]].
    /// If no such index exists, the answer for that query should be -1.
    /// Return an array answer of the same size as queries, where answer[i] represents the result for query i.
    ///
    /// [繁體中文]
    /// 給你一個環形陣列 nums 以及一個查詢陣列 queries。
    /// 對於每個查詢 i，你需要找出以下內容：
    /// 在環形陣列中，索引 queries[i] 處的元素與任意其他索引 j（滿足 nums[j] == nums[queries[i]]）之間的最小距離。
    /// 若不存在這樣的索引，則該查詢的答案為 -1。
    /// 回傳一個與 queries 大小相同的陣列 answer，其中 answer[i] 代表第 i 個查詢的結果。
    /// </summary>
    /// <remarks>
    /// 執行六組固定案例，對照二分搜尋與預先計算距離兩種解法；
    /// 若任一驗證失敗，程序會以非零結束碼結束。
    /// </remarks>
    /// <param name="args">命令列參數；本範例不使用此參數。</param>
    static void Main(string[] args)
    {
        int passedChecks = RunSamples();
        Environment.ExitCode = passedChecks == 12 ? 0 : 1;
    }

    /// <summary>
    /// 執行六組符合題目限制的固定案例，比較兩種解法的結果。
    /// 案例涵蓋官方範例、單次出現、環形首尾、交錯重複與最大陣列長度。
    /// </summary>
    /// <returns>兩種解法合計通過的驗證數。</returns>
    private static int RunSamples()
    {
        (string Name, int[] Nums, int[] Queries, int[] Expected)[] cases =
        [
            ("官方範例 1", [1, 3, 1, 4, 1, 3, 2], [0, 3, 5], [2, -1, 3]),
            ("官方範例 2：全部唯一值", [1, 2, 3, 4], [0, 1, 2, 3], [-1, -1, -1, -1]),
            ("環形首尾為最近位置", [1, 2, 3, 1], [0, 3], [1, 1]),
            ("只有兩個相同元素", [7, 7], [0, 1], [1, 1]),
            ("多組交錯重複值", [1, 2, 1, 2, 1, 2], [0, 1, 2, 5], [2, 2, 2, 2]),
            ("最大長度的相同元素", Enumerable.Repeat(9, 100_000).ToArray(), [0, 50_000, 99_999], [1, 1, 1])
        ];

        var solution = new Program();
        int passedChecks = 0;

        Console.WriteLine("LeetCode 3488 - Closest Equal Element Queries");
        Console.WriteLine("兩種解法對照驗證");
        Console.WriteLine();

        for (int i = 0; i < cases.Length; i++)
        {
            passedChecks += RunCase(solution, i + 1, cases[i]);
        }

        Console.WriteLine($"總結：{passedChecks}/{cases.Length * 2} 項測試通過");
        return passedChecks;
    }

    /// <summary>
    /// 用彼此獨立的查詢陣列副本執行兩種解法，並將結果與預期陣列逐項比對。
    /// </summary>
    /// <param name="solution">提供兩種查詢解法的程序實例。</param>
    /// <param name="caseNumber">顯示用的案例編號。</param>
    /// <param name="testCase">包含名稱、環形陣列、查詢與預期結果的案例。</param>
    /// <returns>本案例通過的解法數，範圍為 0 至 2。</returns>
    private static int RunCase(
        Program solution,
        int caseNumber,
        (string Name, int[] Nums, int[] Queries, int[] Expected) testCase)
    {
        int passedChecks = 0;

        Console.WriteLine($"案例 {caseNumber}：{testCase.Name}");
        Console.WriteLine($"nums = {FormatArray(testCase.Nums)}");
        Console.WriteLine($"queries = {FormatArray(testCase.Queries)}");

        int[] binarySearchQueries = testCase.Queries.ToArray();
        IList<int> binarySearchResult = solution.SolveQueries(testCase.Nums, binarySearchQueries);
        passedChecks += PrintResult("SolveQueries", testCase.Expected, binarySearchResult);

        int[] precomputedQueries = testCase.Queries.ToArray();
        IList<int> precomputedResult = solution.SolveQueriesByPrecomputedDistances(testCase.Nums, precomputedQueries);
        passedChecks += PrintResult("SolveQueriesByPrecomputedDistances", testCase.Expected, precomputedResult);

        Console.WriteLine();
        return passedChecks;
    }

    /// <summary>
    /// 輸出單一解法的預期與實際陣列，並回傳是否通過。
    /// </summary>
    /// <param name="methodName">顯示用的解法名稱。</param>
    /// <param name="expected">事先人工推導的預期結果。</param>
    /// <param name="actual">解法實際回傳的結果。</param>
    /// <returns>結果完全相同時回傳 1，否則回傳 0。</returns>
    private static int PrintResult(string methodName, int[] expected, IList<int> actual)
    {
        bool passed = expected.SequenceEqual(actual);
        Console.WriteLine(
            $"{methodName}: Expected = {FormatArray(expected)}, Actual = {FormatArray(actual)} => {(passed ? "PASS" : "FAIL")}");
        return passed ? 1 : 0;
    }

    /// <summary>
    /// 將整數清單格式化為易讀文字；長清單只顯示前後各六個值與總長度。
    /// </summary>
    /// <param name="values">要顯示的整數清單。</param>
    /// <returns>方括號包覆的清單文字。</returns>
    private static string FormatArray(IList<int> values)
    {
        if (values.Count <= 12)
        {
            return $"[{string.Join(", ", values)}]";
        }

        string firstValues = string.Join(", ", values.Take(6));
        string lastValues = string.Join(", ", values.Skip(values.Count - 6));
        return $"[{firstValues}, ..., {lastValues}] (length = {values.Count})";
    }

    /// <summary>
    /// 使用索引分組與二分搜尋，解答環形陣列中每個查詢索引的最近相同元素距離。
    /// 每個值的有序位置清單會在首尾加入環形虛擬位置，再對查詢做二分搜尋並比較左右鄰居。
    /// 輸入必須符合題目限制：<paramref name="nums"/> 非空，且每個查詢都是有效索引。
    /// </summary>
    /// <param name="nums">只讀取的環形整數陣列，長度至少為 1。</param>
    /// <param name="queries">有效的查詢索引陣列；方法會將其就地覆寫為答案。</param>
    /// <returns>已覆寫的 <paramref name="queries"/>；無其他相同元素時對應值為 -1。</returns>
    /// <remarks>時間複雜度為 O(n + q log n)，空間複雜度為 O(n)。</remarks>
    /// <example>
    /// <code>
    /// var result = SolveQueries([1,3,1,4,1,3,2], [0,3,5]);
    /// result = [2, -1, 3]
    /// </code>
    /// </example>
    public IList<int> SolveQueries(int[] nums, int[] queries)
    {
        int n = nums.Length;

        Dictionary<int, List<int>> valueToPositions = new Dictionary<int, List<int>>();

        for (int i = 0; i < n; i++)
        {
            if (!valueToPositions.ContainsKey(nums[i]))
            {
                valueToPositions[nums[i]] = new List<int>();
            }
            valueToPositions[nums[i]].Add(i);
        }

        // 把最後一個位置向左平移一圈、第一個位置向右平移一圈，
        // 使每個真實位置都能直接比較環形的左右最近鄰居。
        foreach (List<int> positions in valueToPositions.Values)
        {
            int firstPos = positions[0];
            int lastPos = positions[^1];
            positions.Insert(0, lastPos - n);
            positions.Add(firstPos + n);
        }

        for (int i = 0; i < queries.Length; i++)
        {
            int queryIndex = queries[i];
            int value = nums[queryIndex];
            List<int> positions = valueToPositions[value];

            // 原本只出現一次時，加上頭尾虛擬位置後長度恰為 3。
            if (positions.Count == 3)
            {
                queries[i] = -1;
                continue;
            }

            int idx = positions.BinarySearch(queryIndex);
            if (idx < 0)
            {
                idx = ~idx;
            }

            // 同值索引已排序，最近對象必然是左鄰居或右鄰居。
            int distRight = positions[idx + 1] - positions[idx];
            int distLeft = positions[idx] - positions[idx - 1];
            queries[i] = Math.Min(distRight, distLeft);
        }

        return queries;
    }

    /// <summary>
    /// 先為每個陣列索引預先計算最近相同元素的環形距離，再以 O(1) 時間回答每筆查詢。
    /// 同值索引會依陣列出現順序分組；對每個位置只需比較該組的環形前驅與後繼。
    /// 輸入必須符合題目限制：<paramref name="nums"/> 非空，且每個查詢都是有效索引。
    /// </summary>
    /// <param name="nums">只讀取的環形整數陣列，長度至少為 1。</param>
    /// <param name="queries">有效的查詢索引陣列；方法會將其就地覆寫為答案。</param>
    /// <returns>已覆寫的 <paramref name="queries"/>；無其他相同元素時對應值為 -1。</returns>
    /// <remarks>時間複雜度為 O(n + q)，空間複雜度為 O(n)。</remarks>
    public IList<int> SolveQueriesByPrecomputedDistances(int[] nums, int[] queries)
    {
        int n = nums.Length;
        Dictionary<int, List<int>> valueToPositions = new Dictionary<int, List<int>>();

        for (int i = 0; i < n; i++)
        {
            if (!valueToPositions.TryGetValue(nums[i], out List<int>? positions))
            {
                positions = new List<int>();
                valueToPositions[nums[i]] = positions;
            }

            positions.Add(i);
        }

        int[] distanceByIndex = new int[n];

        foreach (List<int> positions in valueToPositions.Values)
        {
            if (positions.Count == 1)
            {
                distanceByIndex[positions[0]] = -1;
                continue;
            }

            for (int i = 0; i < positions.Count; i++)
            {
                int current = positions[i];

                // 第一與最後位置的前驅、後繼需跨越陣列邊界，
                // 以 -n 或 +n 平移後便能直接用直線距離表示環形距離。
                int previous = i == 0 ? positions[^1] - n : positions[i - 1];
                int next = i == positions.Count - 1 ? positions[0] + n : positions[i + 1];

                distanceByIndex[current] = Math.Min(current - previous, next - current);
            }
        }

        for (int i = 0; i < queries.Length; i++)
        {
            queries[i] = distanceByIndex[queries[i]];
        }

        return queries;
    }
}