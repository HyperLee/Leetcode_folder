namespace leetcode_2616;

class Program
{
    /// <summary>
    /// 2616. Minimize the Maximum Difference of Pairs
    /// https://leetcode.com/problems/minimize-the-maximum-difference-of-pairs/description/
    /// 2616. 最小化数对的最大差值
    /// https://leetcode.cn/problems/minimize-the-maximum-difference-of-pairs/description/
    ///
    /// English:
    /// You are given a 0-indexed integer array nums and an integer p. Find p pairs of
    /// indices of nums such that the maximum difference amongst all the pairs is
    /// minimized. Also, ensure no index appears more than once amongst the p pairs.
    ///
    /// Note that for a pair of elements at the index i and j, the difference of this
    /// pair is |nums[i] - nums[j]|, where |x| represents the absolute value of x.
    ///
    /// Return the minimum maximum difference among all p pairs. We define the maximum
    /// of an empty set to be zero.
    ///
    /// 繁體中文：
    /// 給定一個索引從 0 開始的整數陣列 nums 與一個整數 p。請找出 nums 中 p 對索引，
    /// 使所有配對差值中的最大值最小。同時，確保每個索引在這 p 對配對中最多只出現一次。
    ///
    /// 對於索引 i 與 j 的一對元素，其差值為 |nums[i] - nums[j]|，其中 |x| 表示 x 的絕對值。
    ///
    /// 回傳所有 p 對配對中最小的最大差值。我們將空集合的最大值定義為 0。
    ///
    /// 本進入點會以固定案例執行三種解法，逐一比較預期值與實際值，最後輸出 PASS/FAIL 統計。
    /// </summary>
    /// <param name="args">命令列參數；本範例不使用此參數。</param>
    static void Main(string[] args)
    {
        Program solution = new();
        (string Name, int[] Nums, int P, int Expected)[] testCases =
        [
            ("官方範例一", [10, 1, 2, 7, 1, 3], 2, 1),
            ("官方範例二", [4, 2, 1, 2], 1, 0),
            ("空配對", [7], 0, 0),
            ("全重複值", [5, 5, 5, 5], 2, 0),
            ("最小可配對長度", [1, 100], 1, 99),
            ("多組候選", [1, 3, 6, 19, 20], 2, 2)
        ];

        int passed = 0;
        int total = 0;

        foreach ((string name, int[] nums, int p, int expected) in testCases)
        {
            (int casePassed, int caseTotal) = RunTestCase(solution, name, nums, p, expected);
            passed += casePassed;
            total += caseTotal;
        }

        Console.WriteLine($"Summary: {passed}/{total} PASS");
        Environment.ExitCode = passed == total ? 0 : 1;
    }

    /// <summary>
    /// 使用同一筆合法題目輸入依序執行三種解法，並回傳通過數與執行總數。
    /// 每次呼叫前會複製陣列，避免某個解法的原地排序影響後續解法。
    /// </summary>
    /// <param name="solution">提供三種解法的 <see cref="Program"/> 執行個體。</param>
    /// <param name="name">顯示於主控台的案例名稱。</param>
    /// <param name="nums">符合題目限制的原始整數陣列。</param>
    /// <param name="p">需要建立的不重複索引配對數，範圍為 0 到陣列長度的一半。</param>
    /// <param name="expected">此案例預期的最小最大差值。</param>
    /// <returns>三種解法中通過的數量，以及本案例的解法執行總數。</returns>
    private static (int Passed, int Total) RunTestCase(
        Program solution,
        string name,
        int[] nums,
        int p,
        int expected)
    {
        (string Name, Func<int[], int, int> Execute)[] methods =
        [
            (nameof(MinimizeMax), solution.MinimizeMax),
            (nameof(MinimizeMax2), solution.MinimizeMax2),
            (nameof(MinimizeMax3), solution.MinimizeMax3)
        ];

        Console.WriteLine($"[{name}] nums = [{string.Join(", ", nums)}], p = {p}, expected = {expected}");

        int passed = 0;
        foreach ((string methodName, Func<int[], int, int> execute) in methods)
        {
            // 每種解法都會原地排序，因此必須各自取得輸入副本，測試才彼此獨立。
            int actual = execute((int[])nums.Clone(), p);
            bool isPassed = actual == expected;
            passed += isPassed ? 1 : 0;

            Console.WriteLine(
                $"  {methodName}: expected = {expected}, actual = {actual}, {(isPassed ? "PASS" : "FAIL")}");
        }

        Console.WriteLine();
        return (passed, methods.Length);
    }

    /// <summary>
    /// 解法一：二分答案搭配動態規劃可行性判斷。
    /// 先原地排序合法的 <paramref name="nums"/>，再搜尋能組成至少 <paramref name="p"/> 對的最小差值上限。
    /// 動態規劃比較略過目前元素與配對相鄰元素兩種選擇，並以滾動變數保存狀態。
    /// </summary>
    /// <param name="nums">長度 1 到 100,000、元素介於 0 到 1,000,000,000 的整數陣列；呼叫後會被排序。</param>
    /// <param name="p">需要建立的不重複索引配對數，範圍為 0 到陣列長度的一半。</param>
    /// <returns><paramref name="p"/> 對配對中，最大差值可達到的最小值；當 <paramref name="p"/> 為 0 時回傳 0。</returns>
    public int MinimizeMax(int[] nums, int p)
    {
        Array.Sort(nums);

        if (p == 0)
        {
            return 0;
        }

        int left = 0;
        int right = nums[^1] - nums[0];

        while (left < right)
        {
            int mid = left + (right - left) / 2;

            if (CanFormPairsWithDynamicProgramming(mid, nums, p))
            {
                right = mid;
            }
            else
            {
                left = mid + 1;
            }
        }

        return left;
    }

    /// <summary>
    /// 解法二：二分答案搭配內嵌的貪心可行性判斷。
    /// 先原地排序合法的 <paramref name="nums"/>；對每個候選差值由左至右掃描，
    /// 一旦相鄰元素符合上限便立即配對並跳過兩個元素，以計算最多可建立的配對數。
    /// </summary>
    /// <param name="nums">長度 1 到 100,000、元素介於 0 到 1,000,000,000 的整數陣列；呼叫後會被排序。</param>
    /// <param name="p">需要建立的不重複索引配對數，範圍為 0 到陣列長度的一半。</param>
    /// <returns><paramref name="p"/> 對配對中，最大差值可達到的最小值；當 <paramref name="p"/> 為 0 時回傳 0。</returns>
    public int MinimizeMax2(int[] nums, int p)
    {
        Array.Sort(nums);

        if (p == 0)
        {
            return 0;
        }

        int left = 0;
        int right = nums[^1] - nums[0];

        while (left < right)
        {
            int mid = left + (right - left) / 2;
            int count = 0;

            for (int i = 0; i < nums.Length - 1;)
            {
                if (nums[i + 1] - nums[i] <= mid)
                {
                    count++;

                    // 目前兩個元素已被使用，跳過下一個索引以確保配對不重疊。
                    i += 2;
                }
                else
                {
                    i++;
                }
            }

            if (count >= p)
            {
                right = mid;
            }
            else
            {
                left = mid + 1;
            }
        }

        return left;
    }

    /// <summary>
    /// 解法三：使用不可行與可行邊界進行二分答案，並將貪心判斷抽至 <see cref="Check"/>。
    /// 先原地排序合法的 <paramref name="nums"/>，再維持 left 為不可行虛擬邊界、right 為可行上界，
    /// 直到兩者相鄰；最後的 right 即為能建立至少 <paramref name="p"/> 對的最小差值上限。
    /// </summary>
    /// <param name="nums">長度 1 到 100,000、元素介於 0 到 1,000,000,000 的整數陣列；呼叫後會被排序。</param>
    /// <param name="p">需要建立的不重複索引配對數，範圍為 0 到陣列長度的一半。</param>
    /// <returns><paramref name="p"/> 對配對中，最大差值可達到的最小值；當 <paramref name="p"/> 為 0 時回傳 0。</returns>
    public int MinimizeMax3(int[] nums, int p)
    {
        Array.Sort(nums);

        if (p == 0)
        {
            return 0;
        }

        int left = -1;
        int right = nums[^1] - nums[0];

        while (left + 1 < right)
        {
            // 維持 left 不可行、right 可行，並以差值寫法避免中點加總溢位。
            int mid = left + (right - left) / 2;

            if (Check(mid, nums, p))
            {
                right = mid;
            }
            else
            {
                left = mid;
            }
        }

        return right;
    }

    /// <summary>
    /// 以動態規劃判斷排序後的 <paramref name="nums"/>，能否在每對差值不超過
    /// <paramref name="maxDifference"/> 時建立至少 <paramref name="p"/> 組不重複配對。
    /// 輸入必須符合題目限制且已排序；只保留前兩個 DP 狀態，因此使用固定額外空間。
    /// </summary>
    /// <param name="maxDifference">目前允許的非負配對差值上限。</param>
    /// <param name="nums">已依遞增順序排列的合法題目陣列。</param>
    /// <param name="p">目標配對數，範圍為 0 到陣列長度的一半。</param>
    /// <returns>若可以建立至少 <paramref name="p"/> 組配對則為 <see langword="true"/>；否則為 <see langword="false"/>。</returns>
    private bool CanFormPairsWithDynamicProgramming(int maxDifference, int[] nums, int p)
    {
        if (p == 0)
        {
            return true;
        }

        int twoBack = 0;
        int oneBack = 0;

        for (int length = 2; length <= nums.Length; length++)
        {
            int current = oneBack;

            if (nums[length - 1] - nums[length - 2] <= maxDifference)
            {
                // dp[length] 可略過目前元素，或將最後兩個相鄰元素配成一組。
                current = Math.Max(current, twoBack + 1);
            }

            if (current >= p)
            {
                return true;
            }

            twoBack = oneBack;
            oneBack = current;
        }

        return false;
    }

    /// <summary>
    /// 以由左至右的貪心策略檢查排序後的 <paramref name="nums"/>，能否在每對差值不超過
    /// <paramref name="maxDifference"/> 時建立至少 <paramref name="p"/> 組不重複配對。
    /// 輸入必須符合題目限制且已排序；符合上限時立即選擇相鄰元素，可保留最多後續元素。
    /// </summary>
    /// <param name="maxDifference">目前允許的非負配對差值上限。</param>
    /// <param name="nums">已依遞增順序排列的合法題目陣列。</param>
    /// <param name="p">目標配對數，範圍為 0 到陣列長度的一半。</param>
    /// <returns>若可以建立至少 <paramref name="p"/> 組配對則為 <see langword="true"/>；否則為 <see langword="false"/>。</returns>
    private bool Check(int maxDifference, int[] nums, int p)
    {
        int count = 0;

        for (int i = 0; i < nums.Length - 1; i++)
        {
            if (nums[i + 1] - nums[i] <= maxDifference)
            {
                count++;

                // 已選擇 nums[i] 與 nums[i + 1]，必須跳過下一個元素避免重複使用。
                i++;
            }
        }

        return count >= p;
    }
}