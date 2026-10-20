namespace leetcode_2089;

class Program
{
    /// <summary>
    /// 2089. Find Target Indices After Sorting Array
    /// https://leetcode.com/problems/find-target-indices-after-sorting-array/description/
    /// 2089. 找出数组排序后的目标下标
    /// https://leetcode.cn/problems/find-target-indices-after-sorting-array/description/
    /// English:
    /// You are given a 0-indexed integer array `nums` and a target element `target`.
    /// A target index is an index `i` such that `nums[i] == target`.
    /// Return a list of the target indices of `nums` after sorting `nums` in non-decreasing order.
    /// If there are no target indices, return an empty list. The returned list must be sorted in increasing order.
    ///
    /// 繁體中文：
    /// 給定一個從 0 開始索引的整數陣列 `nums` 和目標元素 `target`。若 `nums[i] == target`，則索引 `i` 為目標索引。
    /// 請將 `nums` 依非遞減順序排序後，回傳所有目標索引組成的清單；若沒有目標索引，則回傳空清單。
    /// 回傳的清單必須依遞增順序排列。
    /// </summary>
    /// <remarks>
    /// 此進入點不讀取命令列參數，使用固定測試資料執行兩種解法並比對預期結果。
    /// 每項檢查會輸出 PASS 或 FAIL；若有任何檢查失敗，程式會以非零結束碼結束。
    /// </remarks>
    /// <param name="args">未使用；執行範例不需要命令列輸入。</param>
    static void Main(string[] args)
    {
        (string Name, int[] Nums, int Target, int[] Expected)[] testCases =
        {
            ("官方範例 1", new int[] { 1, 2, 5, 2, 3 }, 2, new int[] { 1, 2 }),
            ("官方範例 2", new int[] { 1, 2, 5, 2, 3 }, 3, new int[] { 3 }),
            ("官方範例 3", new int[] { 1, 2, 5, 2, 3 }, 5, new int[] { 4 }),
            ("找不到目標值", new int[] { 1, 2, 3 }, 4, Array.Empty<int>()),
            ("最小長度與數值下界", new int[] { 1 }, 1, new int[] { 0 }),
            ("最大數值邊界", new int[] { 100 }, 100, new int[] { 0 })
        };

        Program solution = new Program();
        int totalChecks = testCases.Length * 2;
        int passedChecks = 0;

        foreach ((string name, int[] nums, int target, int[] expected) in testCases)
        {
            IList<int> sortedResult = solution.TargetIndices((int[])nums.Clone(), target);
            bool sortedPassed = sortedResult.SequenceEqual(expected);
            string sortedStatus = sortedPassed ? "PASS" : "FAIL";
            Console.WriteLine($"[{sortedStatus}] {name} | TargetIndices => [{string.Join(", ", sortedResult)}] (預期 [{string.Join(", ", expected)}])");
            if (sortedPassed)
            {
                passedChecks++;
            }

            IList<int> countedResult = solution.TargetIndices2((int[])nums.Clone(), target);
            bool countedPassed = countedResult.SequenceEqual(expected);
            string countedStatus = countedPassed ? "PASS" : "FAIL";
            Console.WriteLine($"[{countedStatus}] {name} | TargetIndices2 => [{string.Join(", ", countedResult)}] (預期 [{string.Join(", ", expected)}])");
            if (countedPassed)
            {
                passedChecks++;
            }
        }

        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
        if (passedChecks != totalChecks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 方法一：將陣列排序後由左至右掃描，收集值等於 target 的索引。
    /// 排序後相同值會相鄰，因此掃描時可直接依遞增順序建立答案。
    /// </summary>
    /// <param name="nums">長度介於 1 至 100、元素值介於 1 至 100 的非空整數陣列；本方法會原地排序此陣列。</param>
    /// <param name="target">介於 1 至 100 的目標值。</param>
    /// <returns>依遞增順序排列的目標索引；若找不到目標值，則回傳空清單。</returns>
    public IList<int> TargetIndices(int[] nums, int target)
    {
        int n = nums.Length;
        Array.Sort(nums);

        // 排序後相同值會連續出現；由左至右掃描即可依序收集目標索引。
        List<int> res = new List<int>();
        for(int i = 0; i < n; i++)
        {
            if(nums[i] == target)
            {
                res.Add(i);
            }
        }
        return res;
    }

    /// <summary>
    /// 方法二：不排序陣列，改為統計小於 target 與等於 target 的元素數量。
    /// 小於 target 的數量就是排序後目標索引區段的起點，等於 target 的數量則是區段長度。
    /// </summary>
    /// <param name="nums">長度介於 1 至 100、元素值介於 1 至 100 的非空整數陣列；本方法不會排序或改動此陣列。</param>
    /// <param name="target">介於 1 至 100 的目標值。</param>
    /// <returns>依遞增順序排列的目標索引；若找不到目標值，則回傳空清單。</returns>
    public IList<int> TargetIndices2(int[] nums, int target)
    {
        int cnt1 = 0; // 小於 target 的元素數量
        int cnt2 = 0; // 等於 target 的元素數量

        foreach(int num in nums)
        {
            if(num < target)
            {
                cnt1++;
            }
            else if(num == target)
            {
                cnt2++;
            }
        }

        List<int> res = new List<int>();

        // 排序後 target 會占據 [小於它的數量, 小於它的數量 + 等於它的數量) 區間。
        for(int i = cnt1; i < cnt1 + cnt2; i++)
        {
            res.Add(i);
        }
        return res;
    }
}
