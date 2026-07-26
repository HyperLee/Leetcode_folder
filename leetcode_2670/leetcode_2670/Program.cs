namespace leetcode_2670;

class Program
{
    /// <summary>
    /// 2670. Find the Distinct Difference Array
    /// https://leetcode.com/problems/find-the-distinct-difference-array/description/
    ///
    /// You are given a 0-indexed array nums of length n.
    ///
    /// The distinct difference array of nums is an array diff of length n such that
    /// diff[i] is equal to the number of distinct elements in the suffix
    /// nums[i + 1, ..., n - 1] subtracted from the number of distinct elements in
    /// the prefix nums[0, ..., i].
    ///
    /// Return the distinct difference array of nums.
    ///
    /// Note that nums[i, ..., j] denotes the subarray of nums starting at index i
    /// and ending at index j inclusive. Particularly, if i &gt; j, then
    /// nums[i, ..., j] denotes an empty subarray.
    ///
    /// 繁體中文：
    /// 給你一個長度為 n、索引從 0 開始的陣列 nums。
    ///
    /// nums 的相異元素數量差陣列是一個長度為 n 的陣列 diff，其中 diff[i]
    /// 等於前綴 nums[0, ..., i] 中相異元素的數量，減去後綴
    /// nums[i + 1, ..., n - 1] 中相異元素的數量。
    ///
    /// 請回傳 nums 的相異元素數量差陣列。
    ///
    /// 請注意，nums[i, ..., j] 表示 nums 中從索引 i 開始、到索引 j 結束
    /// （包含兩端）的子陣列。特別是，若 i &gt; j，則 nums[i, ..., j]
    /// 表示空子陣列。
    ///
    /// 2670. 找出不同元素數目差陣列
    /// https://leetcode.cn/problems/find-the-distinct-difference-array/description/
    /// </summary>
    /// <remarks>
    /// 程式進入點。建立固定測試案例，逐一呼叫解法並比較完整結果陣列，
    /// 最後輸出通過數量；若任一案例失敗，程序會以非零結束碼結束。
    /// </remarks>
    /// <param name="args">命令列參數；此範例不使用。</param>
    static void Main(string[] args)
    {
        Program solution = new();
        int passed = 0;
        const int total = 4;

        Console.WriteLine("LeetCode 2670 - Find the Distinct Difference Array");
        Console.WriteLine();

        passed += RunTestCase(
            solution,
            "官方範例一",
            [1, 2, 3, 4, 5],
            [-3, -1, 1, 3, 5]) ? 1 : 0;
        passed += RunTestCase(
            solution,
            "官方範例二",
            [3, 2, 3, 4, 2],
            [-2, -1, 0, 2, 3]) ? 1 : 0;
        passed += RunTestCase(
            solution,
            "單一元素",
            [1],
            [1]) ? 1 : 0;
        passed += RunTestCase(
            solution,
            "全部重複",
            [5, 5, 5],
            [0, 0, 1]) ? 1 : 0;

        Console.WriteLine($"Result: {passed}/{total} passed.");

        if (passed != total)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 使用 HashSet 與前後綴預處理，計算每個索引的相異元素數量差。
    /// <para>
    /// 先由右向左記錄每個後綴的相異元素數量，再由左向右維護目前前綴的相異元素集合；
    /// 索引 <c>i</c> 的答案即為前綴集合大小減去 <c>sufCnt[i + 1]</c>。
    /// </para>
    /// </summary>
    /// <param name="nums">
    /// 長度介於 1 到 50 的整數陣列，且每個元素介於 1 到 50。
    /// </param>
    /// <returns>
    /// 與 <paramref name="nums"/> 等長的陣列；第 <c>i</c> 個值為
    /// <c>nums[0..i]</c> 的相異元素數量減去 <c>nums[i+1..n-1]</c> 的相異元素數量。
    /// </returns>
    public int[] DistinctDifferenceArray(int[] nums)
    {
        int n = nums.Length;
        ISet<int> set = new HashSet<int>();
        int[] sufCnt = new int[n + 1];

        // sufCnt[n] 預設為 0，代表最後一個索引之後的空後綴；答案只會查詢起點 1 到 n，因此不必計算 sufCnt[0]。
        for (int i = n - 1; i > 0; i--)
        {
            set.Add(nums[i]);
            sufCnt[i] = set.Count;
        }

        int[] res = new int[n];
        set.Clear();

        // 正向加入 nums[i] 形成當前前綴，再扣除 i + 1 起始後綴的相異元素數量。
        for (int i = 0; i < n; i++)
        {
            set.Add(nums[i]);
            res[i] = set.Count - sufCnt[i + 1];
        }

        return res;
    }

    /// <summary>
    /// 執行單一固定案例，呼叫相異元素數量差解法並比較完整結果陣列。
    /// 輸入包含案例名稱、待測陣列與預期結果；輸出案例明細並回傳是否通過。
    /// </summary>
    /// <param name="solution">提供 <see cref="DistinctDifferenceArray"/> 解法的物件。</param>
    /// <param name="name">顯示於主控台的案例名稱。</param>
    /// <param name="nums">傳入解法的測試陣列。</param>
    /// <param name="expected">預期的完整相異元素數量差陣列。</param>
    /// <returns>實際結果與預期結果逐項相同時為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
    private static bool RunTestCase(Program solution, string name, int[] nums, int[] expected)
    {
        int[] actual = solution.DistinctDifferenceArray(nums);
        bool passed = actual.SequenceEqual(expected);

        Console.WriteLine($"[{(passed ? "PASS" : "FAIL")}] {name}");
        Console.WriteLine($"  Input:    [{string.Join(", ", nums)}]");
        Console.WriteLine($"  Expected: [{string.Join(", ", expected)}]");
        Console.WriteLine($"  Actual:   [{string.Join(", ", actual)}]");
        Console.WriteLine();

        return passed;
    }
}