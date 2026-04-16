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
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // Example 1: nums = [1,3,1,4,1,3,2], queries = [0,3,5]
        // Expected: [2,-1,3]
        int[] nums1 = [1, 3, 1, 4, 1, 3, 2];
        int[] queries1 = [0, 3, 5];
        Console.WriteLine($"Example 1: [{string.Join(", ", solution.SolveQueries(nums1, queries1))}]");

        // Example 2: nums = [1,2,3,4], queries = [0,1,2,3]
        // Expected: [-1,-1,-1,-1]
        int[] nums2 = [1, 2, 3, 4];
        int[] queries2 = [0, 1, 2, 3];
        Console.WriteLine($"Example 2: [{string.Join(", ", solution.SolveQueries(nums2, queries2))}]");
    }

    /// <summary>
    /// 解法：雜湊表 + 二分搜尋
    ///
    /// 核心思路：
    /// 1. 用雜湊表記錄每個元素值出現的所有索引位置。
    /// 2. 因為陣列是環形的，在位置清單頭尾各補一個「虛擬位置」以消除邊界問題：
    ///    - 頭部補上 lastPos - n（最後一個位置往左繞一圈）
    ///    - 尾部補上 firstPos + n（第一個位置往右繞一圈）
    /// 3. 對每個查詢，用二分搜尋在位置清單中定位，再取左右鄰居的最小距離。
    /// 4. 若某元素值只出現一次（補完後 Count == 3），代表無相同元素，回傳 -1。
    ///
    /// 時間複雜度：O(n + q log n)，n 為陣列長度，q 為查詢數量。
    /// 空間複雜度：O(n)。
    /// </summary>
    /// <param name="nums">環形陣列</param>
    /// <param name="queries">查詢索引陣列</param>
    /// <returns>每個查詢對應的最小距離，若無相同元素則為 -1</returns>
    /// <example>
    /// <code>
    /// var result = SolveQueries([1,3,1,4,1,3,2], [0,3,5]);
    /// // result = [2, -1, 3]
    /// </code>
    /// </example>
    public IList<int> SolveQueries(int[] nums, int[] queries)
    {
        int n = nums.Length;

        // 雜湊表：元素值 -> 該值在 nums 中出現的所有索引位置
        Dictionary<int, List<int>> valueToPositions = new Dictionary<int, List<int>>();

        for (int i = 0; i < n; i++)
        {
            if (!valueToPositions.ContainsKey(nums[i]))
            {
                valueToPositions[nums[i]] = new List<int>();
            }
            valueToPositions[nums[i]].Add(i);
        }

        // 為每個位置清單頭尾補上虛擬位置，處理環形邊界
        // 例如 positions = [2, 5]，n = 7
        //   頭部補 5 - 7 = -2，尾部補 2 + 7 = 9
        //   變成 [-2, 2, 5, 9]，確保每個真實位置左右都有鄰居
        foreach (var positions in valueToPositions.Values.ToList())
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

            // 補完後 Count == 3 代表原本只有 1 個位置（加頭尾共 3 個），無相同元素
            if (positions.Count == 3)
            {
                queries[i] = -1;
                continue;
            }

            // 二分搜尋定位 queryIndex 在位置清單中的位置
            int idx = positions.BinarySearch(queryIndex);
            if (idx < 0)
            {
                idx = ~idx;
            }

            // 取與左鄰居、右鄰居的距離，回傳較小值
            int distRight = positions[idx + 1] - positions[idx];
            int distLeft = positions[idx] - positions[idx - 1];
            queries[i] = Math.Min(distRight, distLeft);
        }

        return queries;
    }
}
