namespace leetcode_1630;

class Program
{
    /// <summary>
    /// 1630. Arithmetic Subarrays
    /// https://leetcode.com/problems/arithmetic-subarrays/description/
    /// English
    /// A sequence of numbers is called arithmetic if it consists of at least two elements,
    /// and the difference between every two consecutive elements is the same. More formally,
    /// a sequence s is arithmetic if and only if
    /// s[i+1] - s[i] == s[1] - s[0] for all valid i.
    ///
    /// For example, these are arithmetic sequences:
    /// <list type="bullet">
    /// <description>1, 3, 5, 7, 9</description>
    /// <description>7, 7, 7, 7</description>
    /// <description>3, -1, -5, -9</description>
    /// </list>
    /// The following sequence is not arithmetic: 1, 1, 2, 5, 7.
    ///
    /// You are given an array of n integers, nums, and two arrays of m
    /// integers each, l and r, representing the m range queries, where
    /// the ith query is the range [l[i], r[i]]. All the arrays are 0-indexed.
    ///
    /// Return a list of boolean elements answer, where answer[i] is true
    /// if the subarray nums[l[i]], nums[l[i]+1], ... , nums[r[i]] can be rearranged to
    /// form an arithmetic sequence, and false otherwise.
    ///
    /// 1630. 等差子陣列
    /// https://leetcode.cn/problems/arithmetic-subarrays/description/
    /// 繁體中文
    /// 如果一個數列至少包含兩個元素，且每兩個相鄰元素之間的差皆相同，則稱此數列為等差數列。
    /// 更正式地說，若且唯若對所有有效的 i，數列 s 都滿足
    /// s[i+1] - s[i] == s[1] - s[0]，則 s 為等差數列。
    ///
    /// 例如，下列數列皆為等差數列：
    /// <list type="bullet">
    /// <description>1, 3, 5, 7, 9</description>
    /// <description>7, 7, 7, 7</description>
    /// <description>3, -1, -5, -9</description>
    /// </list>
    /// 下列數列不是等差數列：1, 1, 2, 5, 7。
    ///
    /// 給定一個包含 n 個整數的陣列 nums，以及兩個各自包含 m 個整數的陣列
    /// l 和 r，用來表示 m 個範圍查詢；其中第 i 個查詢的範圍為
    /// [l[i], r[i]]。所有陣列皆採用從 0 開始的索引。
    ///
    ///
    /// 回傳一個布林值串列 answer。如果子陣列
    /// nums[l[i]], nums[l[i]+1], ... , nums[r[i]] 能重新排列成等差數列，則
    /// answer[i] 為 true；否則為 false。
    ///
    /// </summary>
    /// <param name="args">Command-line arguments (not used).</param>
    static void Main(string[] args)
    {
        Program solver = new Program();
        bool allPassed = true;

        Console.WriteLine("LeetCode 1630 - Arithmetic Subarrays");

        allPassed &= RunTestCase(
            solver,
            "Official example 1",
            [4, 6, 5, 9, 3, 7],
            [0, 0, 2],
            [2, 3, 5],
            [true, false, true]);

        allPassed &= RunTestCase(
            solver,
            "Official example 2",
            [-12, -9, -3, -12, -6, 15, 20, -25, -20, -15, -10],
            [0, 1, 6, 4, 8, 7],
            [4, 4, 9, 7, 9, 10],
            [false, true, false, false, true, true]);

        allPassed &= RunTestCase(
            solver,
            "All values are equal",
            [7, 7, 7, 7],
            [0, 1],
            [3, 2],
            [true, true]);

        Console.WriteLine($"Overall: {(allPassed ? "PASS" : "FAIL")}");
    }
    /// <summary>
    /// 使用同一組輸入執行線性標記法與排序法，並將兩者的結果分別與預期答案比較。
    /// </summary>
    /// <param name="solver">提供兩種解法的解題物件。</param>
    /// <param name="name">顯示於主控台的案例名稱。</param>
    /// <param name="nums">題目給定的整數陣列。</param>
    /// <param name="l">各查詢的左端點。</param>
    /// <param name="r">各查詢的右端點。</param>
    /// <param name="expected">每筆查詢的預期判斷結果。</param>
    /// <returns>兩種解法都符合預期答案時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
    private static bool RunTestCase(
        Program solver,
        string name,
        int[] nums,
        int[] l,
        int[] r,
        bool[] expected)
    {
        IList<bool> linearActual = solver.CheckArithmeticSubarrays(nums, l, r);
        IList<bool> sortingActual = solver.CheckArithmeticSubarraysBySorting(nums, l, r);
        bool linearPassed = linearActual.SequenceEqual(expected);
        bool sortingPassed = sortingActual.SequenceEqual(expected);

        Console.WriteLine();
        Console.WriteLine(name);
        Console.WriteLine($"Expected: {FormatBooleanList(expected)}");
        Console.WriteLine($"CheckArithmeticSubarrays: {FormatBooleanList(linearActual)} => {(linearPassed ? "PASS" : "FAIL")}");
        Console.WriteLine($"CheckArithmeticSubarraysBySorting: {FormatBooleanList(sortingActual)} => {(sortingPassed ? "PASS" : "FAIL")}");

        return linearPassed && sortingPassed;
    }

    /// <summary>
    /// 將布林值序列格式化為接近 LeetCode 答案格式的小寫文字，方便比對執行結果。
    /// </summary>
    /// <param name="values">要顯示的布林值序列。</param>
    /// <returns>例如 <c>[true, false, true]</c> 的格式化字串。</returns>
    private static string FormatBooleanList(IEnumerable<bool> values)
    {
        return $"[{string.Join(", ", values.Select(value => value.ToString().ToLowerInvariant()))}]";
    }

    /// <summary>
    /// <para>方法一：最小值、最大值與位置標記。</para>
    /// <para>
    /// 對每筆長度為 <c>k</c> 的查詢先找出最小值與最大值。若能重排為等差數列，公差必須是
    /// <c>(最大值 - 最小值) / (k - 1)</c>，而且每個值都必須剛好落在一個不重複的等差位置上。
    /// 使用長度為 <c>k</c> 的布林陣列記錄位置，即可避免真的排序。
    /// </para>
    /// <para>每筆查詢的時間複雜度為 O(k)，額外空間複雜度為 O(k)。</para>
    /// </summary>
    /// <param name="nums">包含 <c>n</c> 個整數的來源陣列。</param>
    /// <param name="l">每筆查詢的左端點；長度必須與 <paramref name="r"/> 相同。</param>
    /// <param name="r">每筆查詢的右端點；每個範圍都滿足 <c>0 &lt;= l[i] &lt; r[i] &lt; nums.Length</c>。</param>
    /// <returns>依查詢順序回傳是否能將各子陣列重新排列為等差數列。</returns>
    public IList<bool> CheckArithmeticSubarrays(int[] nums, int[] l, int[] r)
    {
        int queryCount = l.Length;
        IList<bool> results = new List<bool>();

        for (int i = 0; i < queryCount; i++)
        {
            int left = l[i];
            int right = r[i];
            int minimum = nums[left];
            int maximum = nums[left];

            for (int j = left + 1; j <= right; j++)
            {
                minimum = Math.Min(minimum, nums[j]);
                maximum = Math.Max(maximum, nums[j]);
            }

            // 所有元素相同時，公差為 0 且一定成立；提前處理也能避免後續除以 0。
            if (minimum == maximum)
            {
                results.Add(true);
                continue;
            }

            // k 個元素只有 k - 1 個間隔；最大值與最小值的距離必須能平均分配到這些間隔。
            int intervalCount = right - left;

            if ((maximum - minimum) % intervalCount != 0)
            {
                results.Add(false);
                continue;
            }

            int difference = (maximum - minimum) / intervalCount;
            bool isArithmetic = true;
            // seen[position] 表示該等差位置是否已被元素占用，陣列長度正好等於元素數量 k。
            bool[] seen = new bool[intervalCount + 1];

            for (int j = left; j <= right; j++)
            {
                int valueOffset = nums[j] - minimum;

                // 合法元素必須符合 value = minimum + position × difference。
                if (valueOffset % difference != 0)
                {
                    isArithmetic = false;
                    break;
                }

                int position = valueOffset / difference;

                // 同一位置重複代表某個必要位置缺值，無法組成完整的等差數列。
                if (seen[position])
                {
                    isArithmetic = false;
                    break;
                }

                seen[position] = true;
            }

            results.Add(isArithmetic);
        }

        return results;
    }

    /// <summary>
    /// <para>方法二：複製子陣列後排序。</para>
    /// <para>
    /// 對每筆長度為 <c>k</c> 的查詢建立獨立副本，排序後以第一對元素決定公差，再逐一確認其餘相鄰差值。
    /// 因為只排序副本，所以不會修改呼叫者傳入的 <paramref name="nums"/>。
    /// </para>
    /// <para>每筆查詢的時間複雜度為 O(k log k)，額外空間複雜度為 O(k)。</para>
    /// </summary>
    /// <param name="nums">包含 <c>n</c> 個整數的來源陣列。</param>
    /// <param name="l">每筆查詢的左端點；長度必須與 <paramref name="r"/> 相同。</param>
    /// <param name="r">每筆查詢的右端點；每個範圍都滿足 <c>0 &lt;= l[i] &lt; r[i] &lt; nums.Length</c>。</param>
    /// <returns>依查詢順序回傳是否能將各子陣列重新排列為等差數列。</returns>
    public IList<bool> CheckArithmeticSubarraysBySorting(int[] nums, int[] l, int[] r)
    {
        IList<bool> results = new List<bool>();

        for (int i = 0; i < l.Length; i++)
        {
            // C# Range 語法: 陣列[開始索引..結束索引]; 
            // 注意: 開始索引包含在範圍內，結束索引不包含在範圍內。
            // 所以右端點要 + 1, 不然的話右端點會少一個元素計算會錯誤
            int[] sortedValues = nums[l[i]..(r[i] + 1)];
            Array.Sort(sortedValues);

            int difference = sortedValues[1] - sortedValues[0];
            bool isArithmetic = true;

            // 排序後若為等差數列，所有相鄰元素的差都必須相同。
            for (int j = 2; j < sortedValues.Length; j++)
            {
                if (sortedValues[j] - sortedValues[j - 1] != difference)
                {
                    isArithmetic = false;
                    break;
                }
            }

            results.Add(isArithmetic);
        }

        return results;
    }
}