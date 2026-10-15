namespace leetcode_2913;

/// <summary>
/// 2913. Subarrays Distinct Element Sum of Squares I
/// https://leetcode.com/problems/subarrays-distinct-element-sum-of-squares-i/description/
/// 2913. 子数组不同元素数目的平方和 I
/// https://leetcode.cn/problems/subarrays-distinct-element-sum-of-squares-i/description/
/// <para>Given a 0-indexed integer array <c>nums</c>.</para>
/// <para>The distinct count of a subarray of <c>nums</c> is defined as:</para>
/// <para>Let <c>nums[i..j]</c> be a subarray of <c>nums</c> consisting of all the indices from <c>i</c> to <c>j</c> such that <c>0 &lt;= i &lt;= j &lt; nums.length</c>. Then the number of distinct values in <c>nums[i..j]</c> is called the distinct count of <c>nums[i..j]</c>.</para>
/// <para>Return the sum of the squares of distinct counts of all subarrays of <c>nums</c>.</para>
/// <para>A subarray is a contiguous non-empty sequence of elements within an array.</para>
/// <para>繁體中文版本：</para>
/// <para>給定一個以 0 為起始索引的整數陣列 <c>nums</c>。</para>
/// <para><c>nums</c> 的子陣列之不同值數量定義如下：令 <c>nums[i..j]</c> 為 <c>nums</c> 的一個子陣列，由索引 <c>i</c> 到 <c>j</c> 的所有元素組成，且 <c>0 &lt;= i &lt;= j &lt; nums.length</c>。<c>nums[i..j]</c> 中不同值的數量，稱為 <c>nums[i..j]</c> 的不同值數量。</para>
/// <para>請回傳 <c>nums</c> 所有子陣列的不同值數量平方總和。</para>
/// <para>子陣列是陣列中連續且非空的元素序列。</para>
/// </summary>
class Program
{
    /// <summary>
    /// 執行六組固定案例，分別比對模擬法與樹狀數組法的結果和預期值；任一案例不符時以非零結束碼表示失敗。
    /// </summary>
    /// <param name="args">命令列參數；本程式不使用。</param>
    static void Main(string[] args)
    {
        var testCases = new (string Name, int[] Nums, int Expected)[]
        {
            ("LeetCode example 1", new[] { 1, 2, 1 }, 15),
            ("LeetCode example 2", new[] { 2, 2 }, 3),
            ("All distinct", new[] { 1, 2, 3 }, 20),
            ("All equal", new[] { 7, 7, 7 }, 6),
            ("Single element", new[] { 5 }, 1),
            ("Maximum-length all-distinct input", Enumerable.Range(1, 100).ToArray(), 8670850)
        };

        var solution = new Program();
        int passedCount = 0;

        foreach (var testCase in testCases)
        {
            int simulationResult = solution.SumCounts(testCase.Nums);
            int fenwickResult = solution.SumCounts2(testCase.Nums);
            bool passed = simulationResult == testCase.Expected
                && fenwickResult == testCase.Expected
                && simulationResult == fenwickResult;

            if (passed)
            {
                passedCount++;
            }

            string status = passed ? "PASS" : "FAIL";
            Console.WriteLine(
                $"{status} | {testCase.Name} | expected={testCase.Expected} | SumCounts={simulationResult} | SumCounts2={fenwickResult}");
        }

        Console.WriteLine($"Summary: {passedCount}/{testCases.Length} cases passed.");
        Environment.ExitCode = passedCount == testCases.Length ? 0 : 1;
    }

    /// <summary>
    /// 以起點與終點雙層枚舉所有非空連續子陣列，使用集合維護不同值數量並累加其平方。
    /// 適用於長度和元素值皆介於 1 至 100 的整數陣列，回傳所有子陣列不同值數量的平方總和。
    /// </summary>
    /// <param name="nums">長度介於 1 到 100，且每個元素介於 1 到 100 的整數陣列。</param>
    /// <returns>所有非空子陣列的不同值數量平方總和。</returns>
    public int SumCounts(IList<int> nums)
    {
        int n = nums.Count;
        int result = 0;

        for (int start = 0; start < n; start++)
        {
            var distinctValues = new HashSet<int>();
            for (int end = start; end < n; end++)
            {
                // 固定起點並向右延伸；集合大小就是目前子陣列的不同值數量。
                distinctValues.Add(nums[end]);
                int distinctCount = distinctValues.Count;
                result += distinctCount * distinctCount;
            }
        }

        return result;
    }

    /// <summary>
    /// 以最後出現位置找出不同值數量會增加的起點區間，並用支援區間加值與區間總和查詢的樹狀數組計算平方增量。
    /// 適用於長度和元素值皆介於 1 至 100 的整數陣列，回傳所有子陣列不同值數量的平方總和。
    /// </summary>
    /// <param name="nums">長度介於 1 到 100，且每個元素介於 1 到 100 的整數陣列。</param>
    /// <returns>所有非空子陣列的不同值數量平方總和。</returns>
    public int SumCounts2(IList<int> nums)
    {
        int n = nums.Count;
        long result = 0;
        long endingSubarraySquareSum = 0;
        var bit = new BIT(n + 10);
        var seen = new Dictionary<int, int>();
        const long mod = 1000000007;

        for (int i = 0; i < n; i++)
        {
            int value = nums[i];
            int start = seen.TryGetValue(value, out int previousIndex) ? previousIndex + 1 : 0;

            // 只有從上次出現位置之後開始的子陣列，才會因目前值而多一個不同值。
            long distinctCountSum = bit.query(start, i);
            long affectedSubarrayCount = i - start + 1;

            // (d + 1)^2 - d^2 = 2d + 1，因此可由區間內 d 的總和算出平方總和增量。
            endingSubarraySquareSum =
                (endingSubarraySquareSum + 2 * distinctCountSum + affectedSubarrayCount) % mod;
            result = (result + endingSubarraySquareSum) % mod;

            // 更新起點區間的不同值數量，供後續終點使用。
            bit.add(start, i, 1);
            seen[value] = i;
        }

        return (int)result;
    }
}

/// <summary>
/// 以兩棵 Fenwick tree 維護差分與加權差分，支援 0 起始索引的區間加值和區間總和查詢。
/// </summary>
public class BIT
{
    public int size;
    public long[] _tree1;
    public long[] _tree2;

    /// <summary>
    /// 建立指定容量的區間更新、區間查詢樹狀數組。
    /// </summary>
    /// <param name="n">可維護索引範圍的容量。</param>
    public BIT(int n)
    {
        this.size = n;
        this._tree1 = new long[n + 2];
        this._tree2 = new long[n + 2];
    }

    /// <summary>
    /// 取得正整數索引的最低有效位元，用來計算 Fenwick tree 的跳躍距離。
    /// </summary>
    /// <param name="index">大於 0 的 Fenwick tree 索引。</param>
    /// <returns>索引中最低有效位元所代表的值。</returns>
    private int _lowbit(int index)
    {
        return index & -index;
    }

    /// <summary>
    /// 將指定增量加到包含左右端點的索引區間。
    /// </summary>
    /// <param name="left">0 起始的包含式左端點，且不大於 <paramref name="right"/>。</param>
    /// <param name="right">0 起始的包含式右端點。</param>
    /// <param name="delta">要加到區間每個位置的整數增量。</param>
    public void add(int left, int right, int delta)
    {
        _add(left, delta);
        _add(right + 1, -delta);
    }

    /// <summary>
    /// 查詢包含左右端點的索引區間目前數值總和。
    /// </summary>
    /// <param name="left">0 起始的包含式左端點，且不大於 <paramref name="right"/>。</param>
    /// <param name="right">0 起始的包含式右端點。</param>
    /// <returns>區間內所有位置的數值總和。</returns>
    public long query(int left, int right)
    {
        return _query(right) - _query(left - 1);
    }

    /// <summary>
    /// 更新差分樹狀數組在指定 0 起始位置的差分值。
    /// </summary>
    /// <param name="index">差分更新位置。</param>
    /// <param name="delta">要加入該位置的差分增量。</param>
    private void _add(int index, int delta)
    {
        // Fenwick tree 使用 1 起始索引；此處將外部索引轉成內部索引。
        index += 1;
        long rawIndex = index;
        while (index <= size)
        {
            _tree1[index] += delta;
            _tree2[index] += rawIndex * delta;
            index += _lowbit(index);
        }
    }

    /// <summary>
    /// 取得從索引 0 到指定索引的前綴總和；索引為 -1 時回傳 0。
    /// </summary>
    /// <param name="index">0 起始的包含式前綴端點，或代表空前綴的 -1。</param>
    /// <returns>指定前綴內所有位置的數值總和。</returns>
    private long _query(int index)
    {
        index += 1;
        long rawIndex = index + 1;
        if (index > size)
        {
            index = size;
        }

        long result = 0;
        while (index > 0)
        {
            result += rawIndex * _tree1[index] - _tree2[index];
            index -= _lowbit(index);
        }

        return result;
    }
}