namespace leetcode_213;

class Program
{
    /// <summary>
    /// 213. House Robber II
    /// https://leetcode.com/problems/house-robber-ii/description/?envType=problem-list-v2&envId=oizxjoit
    /// 213. 打家劫舍 II
    /// https://leetcode.cn/problems/house-robber-ii/description/
    /// 
    /// 題目描述：
    /// 你是一個專業的小偷，計劃沿著一條環形街道搶劫住戶。每間房屋都存放著特定金額的錢。
    /// 相鄰的房屋裝有相互連通的防盜系統，如果同時搶劫相鄰的兩間房屋會自動報警。
    /// 由於街道是環形的，第一間房屋和最後一間房屋被認為是相鄰的。
    /// 
    /// 解題思路：
    /// 1. 環形街道的特點是第一間和最後一間房屋相鄰，不能同時搶劫
    /// 2. 將問題拆分為兩個子問題：
    ///    - 搶劫第1間到倒數第2間房屋(不包含最後一間; 也可以理解為搶劫第一間)）
    ///    - 搶劫第2間到最後一間房屋（(不包含第一間; 也可以理解為搶劫不搶第一間)）
    /// 3. 對每個子問題使用動態規劃求解，最後取兩個子問題結果的最大值
    /// </summary>
    /// <remarks>
    /// 主要進入點會忽略命令列參數，並以固定案例執行滾動動態規劃與遞迴記憶化兩種解法。
    /// 每項結果會輸出至主控台；若任一實際值不符預期，程序結束碼會設為 1，全部通過則為 0。
    /// </remarks>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        bool allPassed = RunSamples();
        Environment.ExitCode = allPassed ? 0 : 1;
    }

    /// <summary>
    /// 執行兩種 House Robber II 解法的固定測試資料，逐項比較預期值與實際值。
    /// 測試涵蓋空陣列防禦行為、邊界案例、官方範例、重複金額、首尾衝突與較長輸入。
    /// 此方法不需要輸入；完成後回傳所有檢查是否皆通過。
    /// </summary>
    /// <returns>全部解法在全部案例上都符合預期時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
    private static bool RunSamples()
    {
        Program solution = new Program();
        (string Name, int[] Nums, int Expected)[] testCases =
        [
            ("空陣列（防禦行為）", [], 0),
            ("單間房屋", [5], 5),
            ("兩間房屋", [2, 3], 3),
            ("官方範例一", [2, 3, 2], 3),
            ("官方範例二", [1, 2, 3, 1], 4),
            ("官方範例三", [1, 2, 3], 3),
            ("重複金額", [1, 1, 1, 1], 2),
            ("首尾皆為高金額", [100, 1, 1, 100], 101),
            ("較長輸入", [2, 7, 9, 3, 1, 8, 5, 4], 22)
        ];
        (string Name, Func<int[], int> Solve)[] solutions =
        [
            ("解法一：滾動動態規劃", solution.Rob),
            ("解法二：遞迴記憶化", solution.Rob2)
        ];

        int passed = 0;
        int total = testCases.Length * solutions.Length;

        Console.WriteLine("LeetCode 213：打家劫舍 II");
        Console.WriteLine("========================================");

        foreach ((string caseName, int[] nums, int expected) in testCases)
        {
            Console.WriteLine($"案例：{caseName}");
            Console.WriteLine($"輸入：[{string.Join(", ", nums)}]");

            foreach ((string solutionName, Func<int[], int> solve) in solutions)
            {
                int actual = solve((int[])nums.Clone());
                bool isPassed = actual == expected;
                passed += isPassed ? 1 : 0;

                Console.WriteLine($"  {solutionName}");
                Console.WriteLine($"    Expected: {expected}");
                Console.WriteLine($"    Actual:   {actual}");
                Console.WriteLine($"    Result:   {(isPassed ? "PASS" : "FAIL")}");
            }

            Console.WriteLine();
        }

        Console.WriteLine($"總結：{passed}/{total} 項測試通過");
        return passed == total;
    }

    /// <summary>
    /// 使用由下而上的滾動動態規劃求出環形房屋可搶得的最大金額。
    /// 將環形限制拆成「不含最後一間」與「不含第一間」兩個線性區間，再取較大結果。
    /// 輸入必須是非 <see langword="null"/> 的非負整數陣列；空陣列視為沒有房屋。
    /// </summary>
    /// <param name="nums">依環形順序排列的房屋金額；題目限制為長度 1 到 100、每個金額 0 到 1000。</param>
    /// <returns>在不搶劫相鄰房屋的前提下可取得的最大金額；空陣列回傳 0。</returns>
    public int Rob(int[] nums)
    {
        int n = nums.Length;

        if (n == 0)
        {
            return 0;
        }

        if (n == 1)
        {
            return nums[0];
        }

        // 首尾相鄰且不能同時選，因此所有合法答案必定落在這兩個線性區間之一。
        return Math.Max(RobRange(nums, 0, n - 2), RobRange(nums, 1, n - 1));
    }

    /// <summary>
    /// 使用兩個滾動狀態計算指定閉區間內、不選相鄰房屋時的最大金額。
    /// 輸入陣列必須非 <see langword="null"/>，且 <paramref name="start"/> 與 <paramref name="end"/> 必須形成有效區間。
    /// </summary>
    /// <param name="nums">房屋金額陣列。</param>
    /// <param name="start">要納入計算的起始索引。</param>
    /// <param name="end">要納入計算的結束索引。</param>
    /// <returns>指定線性區間內可取得的最大金額。</returns>
    private int RobRange(int[] nums, int start, int end)
    {
        int n = end - start + 1;

        if (n == 1)
        {
            return nums[start];
        }

        // previousTwo 與 previousOne 分別表示處理到前兩間、前一間時的最佳金額。
        int previousTwo = nums[start];
        int previousOne = Math.Max(nums[start], nums[start + 1]);

        for (int i = start + 2; i <= end; i++)
        {
            int current = Math.Max(previousTwo + nums[i], previousOne);
            previousTwo = previousOne;
            previousOne = current;
        }

        return previousOne;
    }

    /// <summary>
    /// 使用由上而下的遞迴與記憶化求出環形房屋可搶得的最大金額。
    /// 將環形問題拆成兩個線性區間，並快取各索引起點的最佳結果，避免重複展開子問題。
    /// 輸入必須是非 <see langword="null"/> 的非負整數陣列；空陣列視為沒有房屋。
    /// </summary>
    /// <param name="nums">依環形順序排列的房屋金額；題目限制為長度 1 到 100、每個金額 0 到 1000。</param>
    /// <returns>在不搶劫相鄰房屋的前提下可取得的最大金額；空陣列回傳 0。</returns>
    public int Rob2(int[] nums)
    {
        int n = nums.Length;

        if (n == 0)
        {
            return 0;
        }

        if (n == 1)
        {
            return nums[0];
        }

        return Math.Max(RobRangeMemo(nums, 0, n - 2), RobRangeMemo(nums, 1, n - 1));
    }

    /// <summary>
    /// 為指定線性閉區間建立獨立快取，並啟動由區間起點向後搜尋的記憶化遞迴。
    /// 輸入陣列必須非 <see langword="null"/>，起訖索引必須位於陣列範圍且起點不大於終點。
    /// </summary>
    /// <param name="nums">房屋金額陣列。</param>
    /// <param name="start">線性區間的起始索引。</param>
    /// <param name="end">線性區間的結束索引。</param>
    /// <returns>指定區間內可取得的最大金額。</returns>
    private int RobRangeMemo(int[] nums, int start, int end)
    {
        int[] memo = new int[nums.Length];
        Array.Fill(memo, -1);
        return RobFrom(nums, start, end, memo);
    }

    /// <summary>
    /// 計算從指定索引到區間終點的最佳金額，並以索引為鍵記住已求得的子問題結果。
    /// 輸入陣列與快取必須非 <see langword="null"/>；索引超過終點代表沒有剩餘房屋可選。
    /// </summary>
    /// <param name="nums">房屋金額陣列。</param>
    /// <param name="index">目前考慮的房屋索引。</param>
    /// <param name="end">允許考慮的最後索引。</param>
    /// <param name="memo">各索引起點的最佳結果；-1 表示尚未計算。</param>
    /// <returns>從目前索引到終點之間可取得的最大金額。</returns>
    private int RobFrom(int[] nums, int index, int end, int[] memo)
    {
        if (index > end)
        {
            return 0;
        }

        if (memo[index] != -1)
        {
            return memo[index];
        }

        // 選目前房屋就跳過下一間；不選則移到下一間，兩者取較大值。
        int robCurrent = nums[index] + RobFrom(nums, index + 2, end, memo);
        int skipCurrent = RobFrom(nums, index + 1, end, memo);
        memo[index] = Math.Max(robCurrent, skipCurrent);
        return memo[index];
    }
}