namespace leetcode_2948;

class Program
{
    /// <summary>
    /// 2948. Make Lexicographically Smallest Array by Swapping Elements
    /// https://leetcode.com/problems/make-lexicographically-smallest-array-by-swapping-elements/description
    /// 2948. 交換得到字典序最小的陣列
    /// https://leetcode.cn/problems/make-lexicographically-smallest-array-by-swapping-elements
    /// English problem statement:
    /// Given a 0-indexed array of positive integers nums and a positive integer limit.
    /// In one operation, you can choose any two indices i and j and swap nums[i] and nums[j] if |nums[i] - nums[j]| &lt;= limit.
    /// Return the lexicographically smallest array that can be obtained by performing the operation any number of times.
    /// An array a is lexicographically smaller than an array b if in the first position where they differ, a has an element that is less than the corresponding element in b. For example, the array [2,10,3] is lexicographically smaller than [10,2,3] because they differ at index 0 and 2 &lt; 10.
    /// 繁體中文題目描述：
    /// 給定一個以 0 為起始索引的正整數陣列 nums，以及一個正整數 limit。
    /// 在一次操作中，你可以選擇任意兩個索引 i 和 j；如果 |nums[i] - nums[j]| &lt;= limit，就交換 nums[i] 與 nums[j]。
    /// 請回傳經過任意次操作後可以得到的字典序最小陣列。
    /// 若陣列 a 與陣列 b 在第一個不同的位置上，a 的元素小於 b 對應位置的元素，則稱 a 的字典序小於 b。例如，陣列 [2,10,3] 的字典序小於 [10,2,3]，因為它們在索引 0 的位置不同，且 2 &lt; 10。
    /// </summary>
    /// <remarks>
    /// 程式進入點會依序執行固定案例，分別驗證兩種解法，並透過結束碼回報是否全部通過。
    /// </remarks>
    /// <param name="args">Command-line arguments (unused).</param>
    static void Main(string[] args)
    {
        TestCase[] testCases =
        [
            new("官方案例一", [1, 5, 3, 9, 8], 2, [1, 3, 5, 8, 9]),
            new("官方案例二", [1, 7, 6, 18, 2, 1], 3, [1, 6, 7, 18, 1, 2]),
            new("官方案例三", [1, 7, 28, 19, 10], 3, [1, 7, 28, 19, 10]),
            new("連鎖連通", [10, 1, 5], 5, [1, 5, 10]),
            new("重複值", [4, 3, 3, 1], 1, [3, 3, 4, 1]),
            new("多個群組", [4, 1, 7, 6, 10, 3], 1, [3, 1, 6, 7, 10, 4])
        ];

        bool allPassed = RunTestCases(testCases);
        Environment.ExitCode = allPassed ? 0 : 1;
    }

    /// <summary>
    /// 代表一組固定的輸入、交換限制與預期輸出，供兩個解法重複驗證。
    /// </summary>
    private sealed record TestCase(
        string Name,
        int[] Nums,
        int Limit,
        int[] Expected);

    /// <summary>
    /// 執行固定測試案例，分別驗證兩個解法是否回傳預期的字典序最小陣列。
    /// </summary>
    /// <param name="testCases">要依序執行的測試案例集合。</param>
    /// <returns>若所有解法在所有案例中都通過則回傳 <c>true</c>，否則回傳 <c>false</c>。</returns>
    private static bool RunTestCases(IReadOnlyList<TestCase> testCases)
    {
        Program solver = new();
        int passedCount = 0;
        int totalCount = 0;

        Console.WriteLine("LeetCode 2948 - Make Lexicographically Smallest Array by Swapping Elements");
        Console.WriteLine();

        foreach (TestCase testCase in testCases)
        {
            totalCount++;
            if (RunSingleTest("解法一：排序", solver.LexicographicallySmallestArray, testCase))
            {
                passedCount++;
            }

            totalCount++;
            if (RunSingleTest("解法二：索引排序", solver.LexicographicallySmallestArray2, testCase))
            {
                passedCount++;
            }
        }

        Console.WriteLine($"Summary: {passedCount}/{totalCount} PASS");
        return passedCount == totalCount;
    }

    /// <summary>
    /// 使用指定解法執行一組案例，列印輸入、預期結果、實際結果與通過狀態。
    /// </summary>
    /// <param name="solutionName">目前執行的解法名稱。</param>
    /// <param name="solver">接受 nums 與 limit 並回傳結果陣列的解法函式。</param>
    /// <param name="testCase">要執行的固定測試案例。</param>
    /// <returns>實際結果與預期結果相同時回傳 <c>true</c>，否則回傳 <c>false</c>。</returns>
    private static bool RunSingleTest(
        string solutionName,
        Func<int[], int, int[]> solver,
        TestCase testCase)
    {
        int[] actual = solver(testCase.Nums.ToArray(), testCase.Limit);
        bool passed = actual.SequenceEqual(testCase.Expected);

        Console.WriteLine($"[{solutionName}] {testCase.Name}");
        Console.WriteLine($"Input: nums = [{string.Join(", ", testCase.Nums)}], limit = {testCase.Limit}");
        Console.WriteLine($"Expected: [{string.Join(", ", testCase.Expected)}]");
        Console.WriteLine($"Actual: [{string.Join(", ", actual)}]");
        Console.WriteLine($"Result: {(passed ? "PASS" : "FAIL")}");
        Console.WriteLine();

        return passed;
    }

    /// <summary>
    /// 解法一：排序
    /// 
    /// 在滿足交換條件的情況下，將 <paramref name="nums"/> 重新排列成字典序最小的陣列。
    ///
    /// 若兩個元素的值差不超過 <paramref name="limit"/>，則可以交換它們的位置。
    /// 由於交換次數與順序不限，只要多個元素之間能透過合法交換間接連接，
    /// 位於同一連通塊中的元素就可以任意重新排列。
    ///
    /// 為了得到字典序最小的結果，對每個連通塊：
    /// 1. 找出該連通塊所有元素的原始下標。
    /// 2. 將連通塊內的元素值依非遞減順序排列。
    /// 3. 將較小的元素依序放回較小的原始下標。
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>核心概念：連通塊</b>
    /// </para>
    ///
    /// <para>
    /// 如果元素 x 可以與 y 交換，而 y 可以與 z 交換，
    /// 即使 x 與 z 無法直接交換，也可以透過 y 間接完成 x 與 z 的位置交換。
    /// 因此，可以把每個元素視為一個節點，符合交換條件的元素之間建立一條邊。
    /// 位於同一連通塊中的元素，可以透過若干次合法交換任意重新排列；
    /// 不同連通塊之間則無法交換。
    /// </para>
    ///
    /// <para>
    /// <b>如何有效找出連通塊</b>
    /// </para>
    ///
    /// <para>
    /// 若直接比較所有元素是否可以交換，需要檢查所有元素對，
    /// 時間複雜度為 <c>O(n²)</c>。
    /// </para>
    ///
    /// <para>
    /// 將元素按照值由小到大排序後，設排序結果為：
    /// </para>
    ///
    /// <code>
    /// v[0] &lt;= v[1] &lt;= ... &lt;= v[n - 1]
    /// </code>
    ///
    /// <para>
    /// 對於排序後相鄰的兩個元素：
    /// </para>
    ///
    /// <code>
    /// v[i] - v[i - 1] &lt;= limit
    /// </code>
    ///
    /// <para>
    /// 則兩者屬於同一個連通塊。
    /// </para>
    ///
    /// <para>
    /// 如果：
    /// </para>
    ///
    /// <code>
    /// v[i] - v[i - 1] &gt; limit
    /// </code>
    ///
    /// <para>
    /// 因為陣列已排序，所以對所有 <c>j &lt; i</c> 都有：
    /// </para>
    ///
    /// <code>
    /// v[i] - v[j] &gt; limit
    /// </code>
    ///
    /// <para>
    /// 因此 <c>v[i]</c> 不可能與左側任何元素建立交換關係，
    /// 連通塊一定會在這個位置分裂。
    /// </para>
    ///
    /// <para>
    /// 所以每個連通塊在排序後的陣列中一定是一段連續區間，
    /// 只需要比較相鄰元素的差值即可完成連通塊劃分，
    /// 不需要真的建立圖或執行 DFS / BFS。
    /// </para>
    ///
    /// <para>
    /// 排序時必須同時保留每個元素的原始下標，
    /// 因為找到連通塊後，仍需要把排序後的元素值放回對應的原始位置。
    /// </para>
    ///
    /// <para>
    /// <b>時間複雜度：</b>
    /// 排序需要 <c>O(n log n)</c>，掃描與重建答案需要 <c>O(n)</c>，
    /// 因此總時間複雜度為 <c>O(n log n)</c>。
    /// </para>
    ///
    /// <para>
    /// <b>空間複雜度：</b>
    /// 需要額外保存排序後的元素、原始下標以及答案，
    /// 空間複雜度為 <c>O(n)</c>。
    /// </para>
    /// </remarks>
    /// <param name="nums">要重新排列的原始正整數陣列。</param>
    /// <param name="limit">兩個元素可以直接交換時允許的最大值差。</param>
    /// <returns>所有合法交換完成後的字典序最小陣列。</returns>
    public int[] LexicographicallySmallestArray(int[] nums, int limit)
    {
        int n = nums.Length;
        int[] ans = new int[n];

        // 將元素值與原下標綁定，排序後才能同時知道值的順序與放回位置。
        List<(int value, int index)> arr = new();
        for (int i = 0; i < n; i++)
        {
            arr.Add((nums[i], i));
        }

        // 按照元素升序排序
        arr.Sort((a, b) => a.value.CompareTo(b.value));

        List<int> values = new();
        List<int> indices = new();

        foreach (var p in arr)
        {
            values.Add(p.value);
            indices.Add(p.index);
        }

        int ptr = 0;
        while (ptr < n)
        {
            int start = ptr;

            // 相鄰值差距超過 limit 就會切開連通塊；同一群組內的值可以互相重排。
            List<int> groupIndices = new();
            List<int> groupValues = new();

            while (ptr < n && (ptr == start || values[ptr] - values[ptr - 1] <= limit))
            {
                groupIndices.Add(indices[ptr]);
                groupValues.Add(values[ptr]);
                ptr++;
            }

            // 值已經由小到大排列，只需要把原下標排序，才能依小下標放入較小值。
            groupIndices.Sort();

            // 將群組內較小的元素放到較小的原始下標，得到字典序最小結果。
            for (int k = 0; k < groupIndices.Count; k++)
            {
                ans[groupIndices[k]] = groupValues[k];
            }
        }
        return ans;
    }

    /// <summary>
    /// 解法二：排序原始索引，再依索引群組重建字典序最小陣列。
    ///
    /// 不直接搬動 <paramref name="nums"/> 的值，而是建立按照值排序的索引陣列。
    /// 排序後相鄰值的差距不超過 <paramref name="limit"/> 時，這些索引屬於同一個可交換群組；
    /// 將群組內的原始索引排序後，再把已排序的值依序放回，即可取得字典序最小結果。
    /// </summary>
    /// <param name="nums">要重新排列的原始正整數陣列。</param>
    /// <param name="limit">兩個元素可以直接交換時允許的最大值差。</param>
    /// <returns>所有合法交換完成後的字典序最小陣列。</returns>
    public int[] LexicographicallySmallestArray2(int[] nums, int limit)
    {
        int n = nums.Length;

        int[] idx = new int[n];

        for (int i = 0; i < n; i++)
        {
            idx[i] = i;
        }

        // 只排序索引，讓 nums 保持原狀，並取得按照元素值排列的檢視順序。
        Array.Sort(idx, (i, j) => nums[i].CompareTo(nums[j]));

        int[] ans = new int[n];

        for (int i = 0; i < n;)
        {
            int j = i + 1;

            // 相鄰值差距超過 limit 時，後面的元素無法與目前群組連通。
            while (j < n && nums[idx[j]] - nums[idx[j - 1]] <= limit)
            {
                j++;
            }

            int[] t = idx[i..j];

            // 群組內可以任意重排，因此將較小值放回較小的原始索引。
            Array.Sort(t);

            for (int k = i; k < j; k++)
            {
                ans[t[k - i]] = nums[idx[k]];
            }

            i = j;
        }

        return ans;
    }
}