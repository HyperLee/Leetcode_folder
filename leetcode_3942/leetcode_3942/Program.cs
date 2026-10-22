namespace leetcode_3942;

/// <summary>
/// 3942. Minimum Operations to Sort a Permutation
/// https://leetcode.com/problems/minimum-operations-to-sort-a-permutation/description/
/// 3942. 排序排列的最少操作数
/// https://leetcode.cn/problems/minimum-operations-to-sort-a-permutation/description/
/// <para>English:</para>
/// <para>You are given an integer array <c>nums</c> of length <c>n</c>, where <c>nums</c> is a permutation of the integers from 0 to <c>n - 1</c>.</para>
/// <para>You may perform only the following operations:</para>
/// <list type="bullet">
/// <item><description>Reverse the entire array.</description></item>
/// <item><description>Rotate Left by One: Move the first element to the end of the array, and rest elements to left by one position.</description></item>
/// </list>
/// <para>Return an integer denoting the minimum number of operations required to sort the array in increasing order. If it is not possible to sort the array using only the given operations, return -1.</para>
/// <para>繁體中文：</para>
/// <para>給定一個長度為 <c>n</c> 的整數陣列 <c>nums</c>，其中 <c>nums</c> 是由 0 到 <c>n - 1</c> 組成的排列。</para>
/// <para>你只能執行以下操作：</para>
/// <list type="bullet">
/// <item><description>反轉整個陣列。</description></item>
/// <item><description>向左旋轉一格：將第一個元素移至陣列末端，其餘元素向左移動一格。</description></item>
/// </list>
/// <para>請回傳一個整數，表示將陣列排序為遞增順序所需的最少操作次數。若只使用上述操作無法完成排序，請回傳 -1。</para>
/// </summary>
class Program
{
    /// <summary>
    /// 使用固定測資呼叫解法，逐筆輸出輸入、預期值、實際值與通過狀態，並彙總通過數。
    /// 解題概念：以同一組模擬解法驗證不同排列形態。
    /// 輸入條件：不讀取命令列參數，測資直接定義於本方法。
    /// 輸出結果：將每筆檢查結果與總通過數輸出至主控台。
    /// </summary>
    /// <param name="args">未使用的命令列參數；測試資料直接定義於 Main。</param>
    static void Main(string[] args)
    {
        var testCases = new (int[] Nums, int Expected)[]
        {
            (new[] { 0 }, 0),
            (new[] { 0, 2, 1 }, 2),
            (new[] { 1, 0, 2 }, 2),
            (new[] { 2, 0, 1, 3 }, -1)
        };

        var solution = new Program();
        int passed = 0;

        foreach (var testCase in testCases)
        {
            int actual = solution.MinOperations(testCase.Nums);
            bool isPassed = actual == testCase.Expected;

            if (isPassed)
            {
                passed++;
            }

            string status = isPassed ? "PASS" : "FAIL";
            string nums = string.Join(", ", testCase.Nums);
            Console.WriteLine($"{status} | nums=[{nums}] | expected={testCase.Expected} | actual={actual}");
        }

        Console.WriteLine($"Summary: {passed}/{testCases.Length} checks passed.");
    }

    /// <summary>
    /// 方法一：模擬循環序列，判斷 nums 是否為遞增或遞減排列的循環位移，並計算最少操作數。
    /// 解題概念：從 0 開始檢查循環方向；兩種方向都不符合時回傳 -1。
    /// 輸入條件：nums 是由 0 到 n - 1 組成且 n >= 1 的排列。
    /// 輸出結果：可排序時回傳最少操作數，否則回傳 -1。
    /// </summary>
    /// <param name="nums">由 0 到 n - 1 組成的整數排列，且長度至少為 1。</param>
    /// <returns>排序所需的最少操作數；若無法排序則回傳 -1。</returns>
    public int MinOperations(int[] nums)
    {
        int n = nums.Length;
        if (n == 1) {
            return 0;
        }

        int start = Array.IndexOf(nums, 0);

        // 以 0 為循環起點；第 i 個值須符合遞增的 i 或遞減的 n - i。
        bool asc = true;
        bool desc = true;

        for (int i = 1; i < n; i++) {
            int value = nums[(start + i) % n];

            if (value != i) {
                asc = false;
            }
            if (value != n - i) {
                desc = false;
            }

            if (!asc && !desc) {
                // 兩種循環方向都不成立，旋轉與反轉無法改變此排列的循環順序。
                return -1;
            }
        }

        if (asc) {
            // 可直接左旋 start 次，或反轉、左旋 n-start 次、再反轉；取較短路徑。
            return Math.Min(start, n - start + 2);
        }

        // 遞減方向可反轉後左旋 n-start-1 次，或先左旋 start+1 次再反轉。
        return Math.Min(n - start, start + 2);
    }
}
