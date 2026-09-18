namespace leetcode_1477;

class Program
{
    /// <summary>
    /// 1477. Find Two Non-overlapping Sub-arrays Each With Target Sum
    ///
    /// English:
    /// You are given an array of integers arr and an integer target.
    ///
    /// You have to find two non-overlapping sub-arrays of arr each with a sum equal target.
    /// There can be multiple answers so you have to find an answer where the sum of the lengths of the two sub-arrays is minimum.
    ///
    /// Return the minimum sum of the lengths of the two required sub-arrays, or return -1 if you cannot find such two sub-arrays.
    ///
    /// 繁體中文：
    /// 給定一個整數陣列 arr 和一個整數 target。
    ///
    /// 請找出 arr 中兩個互不重疊的子陣列，且每個子陣列的總和都等於 target。
    /// 可能存在多組答案，因此請找出兩個子陣列長度總和最小的答案。
    ///
    /// 請回傳這兩個子陣列長度總和的最小值；如果找不到符合條件的兩個子陣列，則回傳 -1。
    ///
    /// English problem: https://leetcode.com/problems/find-two-non-overlapping-sub-arrays-each-with-target-sum/description/
    /// 中文題目：https://leetcode.cn/problems/find-two-non-overlapping-sub-arrays-each-with-target-sum/description/
    /// </summary>
    /// <param name="args">Command-line arguments; no user input is required.</param>
    static void Main(string[] args)
    {
        // 以官方案例與邊界案例驗證兩個解法的結果一致。
        (string Name, int[] Arr, int Target, int Expected)[] testCases =
        [
            ("官方範例 1", [3, 2, 2, 4, 3], 3, 2),
            ("官方範例 2", [7, 3, 4, 7], 7, 2),
            ("相鄰非重疊", [1, 1, 1, 1], 2, 4),
            ("官方範例 3（無解）", [4, 3, 2, 6, 2, 3, 4], 6, -1)
        ];

        Program solver = new();
        (string Name, Func<int[], int, int> Solve)[] solutions =
        [
            (nameof(MinSumOfLengths), solver.MinSumOfLengths),
            (nameof(MinSumOfLengths2), solver.MinSumOfLengths2)
        ];

        int passedCases = 0;

        foreach ((string Name, int[] Arr, int Target, int Expected) testCase in testCases)
        {
            bool casePassed = true;

            foreach ((string Name, Func<int[], int, int> Solve) solution in solutions)
            {
                int actual = solution.Solve(testCase.Arr, testCase.Target);
                bool passed = actual == testCase.Expected;
                casePassed = casePassed && passed;

                Console.WriteLine(
                    $"案例：{testCase.Name} | {solution.Name} | " +
                    $"Expected={testCase.Expected}, Actual={actual} | " +
                    $"{(passed ? "PASS" : "FAIL")}");
            }

            if (casePassed)
            {
                passedCases++;
            }
        }

        Console.WriteLine($"Summary: {passedCases}/{testCases.Length} cases passed.");

        if (passedCases != testCases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 使用滑動視窗與前綴最小值，找出兩個互不重疊且總和等於 target 的子陣列。
    /// 對每個右端點維護目前的 target 視窗，再用 preMin 記錄左側可搭配的最短子陣列。
    /// arr 的元素必須是正整數，找不到兩個合法子陣列時回傳 -1。
    /// </summary>
    /// <param name="arr">長度至少為 1 且元素為正整數的輸入陣列。</param>
    /// <param name="target">每個子陣列必須達到的目標總和。</param>
    /// <returns>兩個合法子陣列長度總和的最小值；找不到兩段時回傳 -1。</returns>
    public int MinSumOfLengths(int[] arr, int target)
    {
        int n = arr.Length;
        int ans = n + 1;
        // preMin[i] 表示右端點小於 i 的和為 target 的最短子陣列長度。
        // 不存在子陣列時，長度設為 n + 1。
        int[] preMin = new int[n + 1];
        preMin[0] = n + 1;
        int minLen = n + 1;
        int sum = 0;
        int l = 0;

        for (int r = 0; r < n; r++)
        {
            sum += arr[r];

            // arr 元素為正數，因此總和超過 target 時移除左端元素即可縮小視窗。
            while (sum > target)
            {
                sum -= arr[l];
                l++;
            }

            if (sum == target)
            {
                // [l, r] 是目前的 target 子陣列；前一段的右端點必須小於 l 才不會重疊。
                ans = Math.Min(ans, preMin[l] + r - l + 1);
                // 保留掃描過的 target 子陣列最短長度，供後續視窗配對。
                minLen = Math.Min(minLen, r - l + 1);
            }

            // 將目前最短長度保存給下一個右端點使用。
            preMin[r + 1] = minLen;
        }
        return ans > n ? -1 : ans;
    }

    /// <summary>
    /// 使用後綴最小值與滑動視窗，先預處理每個右側區域中的最短 target 子陣列，
    /// 再枚舉第一段子陣列並與右側結果配對。
    /// arr 的元素必須是正整數，找不到兩個合法子陣列時回傳 -1。
    /// </summary>
    /// <param name="arr">長度至少為 1 且元素為正整數的輸入陣列。</param>
    /// <param name="target">每個子陣列必須達到的目標總和。</param>
    /// <returns>兩個合法子陣列長度總和的最小值；找不到兩段時回傳 -1。</returns>
    public int MinSumOfLengths2(int[] arr, int target)
    {
        int n = arr.Length;

        // sufMin[i] 表示左端點大於或等於 i 的和為 target 的最短子陣列長度。
        // 不存在子陣列時，長度設為 n + 1。
        int[] sufMin = new int[n];

        int minLen = n + 1;
        int sum = 0;
        int r = n - 1;

        // 從右往左，計算每個起點之後的最短 target 子陣列。
        for (int l = n - 1; l > 0; l--)
        {
            sum += arr[l];

            // 正整數保證視窗總和超過 target 時，向左移動右端點能縮小總和。
            while (sum > target)
            {
                sum -= arr[r];
                r--;
            }

            if (sum == target)
            {
                // 保留目前右側區域內找到的 target 子陣列最短長度。
                minLen = Math.Min(minLen, r - l + 1);
            }

            sufMin[l] = minLen;
        }

        int ans = n + 1;

        sum = 0;
        int left = 0;

        // 由左至右尋找第一段 target 子陣列。
        for (r = 0; r < n - 1; r++)
        {
            sum += arr[r];

            while (sum > target)
            {
                sum -= arr[left];
                left++;
            }

            if (sum == target)
            {
                // 第一段長度加上右側不重疊區域中的最短第二段。
                ans = Math.Min(
                    ans,
                    r - left + 1 + sufMin[r + 1]
                );
            }
        }

        return ans > n ? -1 : ans;
    }
}
