namespace leetcode_3471;

class Program
{
    /// <summary>
    /// 3471. Find the Largest Almost Missing Integer
    /// https://leetcode.com/problems/find-the-largest-almost-missing-integer/description
    /// 3471. 找出最大的幾乎缺失整數
    /// https://leetcode.cn/problems/find-the-largest-almost-missing-integer/description/
    ///
    /// <para>English original:</para>
    /// <para>You are given an integer array nums and an integer k.</para>
    /// <para>An integer x is almost missing from nums if x appears in exactly one subarray of size k within nums.</para>
    /// <para>Return the largest almost missing integer from nums. If no such integer exists, return -1.</para>
    /// <para>A subarray is a contiguous sequence of elements within an array.</para>
    ///
    /// <para>繁體中文翻譯：</para>
    /// <para>給定一個整數陣列 nums 和一個整數 k。</para>
    /// <para>如果整數 x 在 nums 中恰好出現在一個大小為 k 的子陣列中，則稱 x 為 nums 中的幾乎缺失整數。</para>
    /// <para>請回傳 nums 中最大的幾乎缺失整數。如果不存在這樣的整數，請回傳 -1。</para>
    /// <para>子陣列是陣列中一段連續的元素序列。</para>
    /// </summary>
    /// <param name="args"></param>
    /// <remarks>
    /// 主控台進入點會以固定案例分別執行兩種解法，列出預期值、實際值與通過狀態；若任一檢查失敗，程式會以非零狀態碼結束。
    /// </remarks>
    static void Main(string[] args)
    {
        Program solution = new Program();
        TestCase[] testCases =
        [
            new("官方範例一", [3, 9, 2, 1, 7], 3, 7),
            new("官方範例二", [3, 9, 7, 2, 1, 7], 4, 3),
            new("官方範例三", [0, 0], 1, -1),
            new("k 等於陣列長度", [2, 1, 3], 3, 3),
            new("k 等於 1 且包含重複值", [4, 1, 4, 2, 3], 1, 3),
            new("單元素陣列", [6], 1, 6),
            new("兩端值重複、無答案", [5, 1, 2, 5], 2, -1),
            new("兩端值唯一", [8, 2, 3, 4, 9], 2, 9)
        ];

        int passedChecks = 0;
        foreach (TestCase testCase in testCases)
        {
            passedChecks += RunTestCase(solution, testCase);
        }

        int totalChecks = testCases.Length * 2;
        Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項測試通過");
        if (passedChecks != totalChecks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 執行單一測試案例，分別呼叫兩種解法並輸出結果。
    /// </summary>
    /// <param name="solution">要測試的解法物件。</param>
    /// <param name="testCase">包含輸入資料與預期答案的測試案例。</param>
    /// <returns>該案例通過的檢查數量，範圍為 0 到 2。</returns>
    private static int RunTestCase(Program solution, TestCase testCase)
    {
        Console.WriteLine($"案例：{testCase.Name}");
        Console.WriteLine($"輸入：nums = [{string.Join(", ", testCase.Nums)}], k = {testCase.K}");
        Console.WriteLine($"預期：{testCase.Expected}");

        int classifiedResult = solution.LargestInteger(testCase.Nums.ToArray(), testCase.K);
        bool classifiedPassed = classifiedResult == testCase.Expected;
        Console.WriteLine($"實際：LargestInteger = {classifiedResult} => {(classifiedPassed ? "PASS" : "FAIL")}");

        int windowEnumerationResult = solution.LargestIntegerByWindowEnumeration(testCase.Nums.ToArray(), testCase.K);
        bool windowEnumerationPassed = windowEnumerationResult == testCase.Expected;
        Console.WriteLine($"實際：LargestIntegerByWindowEnumeration = {windowEnumerationResult} => {(windowEnumerationPassed ? "PASS" : "FAIL")}");
        Console.WriteLine();

        return (classifiedPassed ? 1 : 0) + (windowEnumerationPassed ? 1 : 0);
    }

    private sealed record TestCase(string Name, int[] Nums, int K, int Expected);

    /// <summary>
    /// 使用 k 與 nums.Length 的關係，以分類討論法回傳最大的幾乎缺失整數。
    /// 輸入 nums 為符合題目限制的整數陣列，k 為視窗長度；若不存在符合條件的值則回傳 -1。
    /// </summary>
    /// <remarks>
    /// <para>當 k 等於 nums.Length 時，整個陣列只有一個視窗，因此直接回傳陣列最大值。</para>
    /// <para>當 k 等於 1 時，每個元素各自形成一個視窗；全域出現一次就等價於只出現在一個視窗，從大到小尋找即可。</para>
    /// <para>當 1 &lt; k &lt; nums.Length 時，內部位置至少被兩個視窗覆蓋，候選只能是陣列兩端，且端點值必須在全域只出現一次。</para>
    /// <para>依題目限制 nums[i] 的範圍為 0 到 50，因此使用長度 51 的頻率陣列。</para>
    /// </remarks>
    /// <param name="nums">待檢查的整數陣列，長度介於 1 到 50，元素介於 0 到 50。</param>
    /// <param name="k">固定子陣列長度，介於 1 到 nums.Length。</param>
    /// <returns>最大的幾乎缺失整數；若不存在則回傳 -1。</returns>
    public int LargestInteger(int[] nums, int k)
    {
        int n = nums.Length;
        if (n == k)
        {
            // 只有一個長度為 k 的視窗，所有值都只會出現在這個視窗中。
            return nums.Max();
        }

        // 全域頻率用來判斷端點值是否也出現在其他位置。
        int[] frequency = new int[51];
        foreach (int value in nums)
        {
            frequency[value]++;
        }

        if (k == 1)
        {
            // 每個元素各自形成一個視窗，因此全域出現一次就是符合條件。
            for (int value = 50; value >= 0; value--)
            {
                if (frequency[value] == 1)
                {
                    return value;
                }
            }
            return -1;
        }

        // 1 < k < n 時，內部位置至少被兩個視窗覆蓋，候選只能來自陣列兩端。
        int result = -1;
        if (frequency[nums[0]] == 1)
        {
            result = Math.Max(result, nums[0]);
        }

        if (frequency[nums[n - 1]] == 1)
        {
            result = Math.Max(result, nums[n - 1]);
        }
        return result;
    }

    /// <summary>
    /// 枚舉所有固定長度的子陣列，回傳只出現在一個視窗中的最大整數。
    /// 輸入 nums 為整數陣列、k 為視窗長度；若沒有符合條件的整數則回傳 -1。
    /// </summary>
    /// <remarks>
    /// <para>每個視窗先放入 HashSet，讓同一個值在同一視窗內重複出現時只計算一次。</para>
    /// <para>接著統計每個值出現於多少個視窗，最後選出視窗計數恰為 1 的最大值。</para>
    /// <para>時間複雜度為 O((n-k+1) * k + V)，空間複雜度為 O(V+k)，其中 V 為值域大小。</para>
    /// </remarks>
    /// <param name="nums">待檢查的整數陣列，長度介於 1 到 50，元素介於 0 到 50。</param>
    /// <param name="k">固定子陣列長度，介於 1 到 nums.Length。</param>
    /// <returns>最大的幾乎缺失整數；若不存在則回傳 -1。</returns>
    public int LargestIntegerByWindowEnumeration(int[] nums, int k)
    {
        int windowCount = nums.Length - k + 1;
        int[] windowsContainingValue = new int[51];

        for (int start = 0; start < windowCount; start++)
        {
            HashSet<int> valuesInWindow = new HashSet<int>();
            for (int index = start; index < start + k; index++)
            {
                // 同一個值在同一視窗內只能貢獻一次，符合題目對「出現在視窗中」的定義。
                valuesInWindow.Add(nums[index]);
            }

            // 每個視窗完成去重後，再累加其中包含的所有值。
            foreach (int value in valuesInWindow)
            {
                windowsContainingValue[value]++;
            }
        }

        int result = -1;
        for (int value = 0; value < windowsContainingValue.Length; value++)
        {
            if (windowsContainingValue[value] == 1)
            {
                // 由小到大掃描並覆蓋結果，最後留下最大的合法值。
                result = value;
            }
        }

        return result;
    }

}