namespace leetcode_2333;

class Program
{
    /// <summary>
    /// 2333. Minimum Sum of Squared Difference
    /// https://leetcode.com/problems/minimum-sum-of-squared-difference/description/
    /// 2333. 最小差值平方和
    /// https://leetcode.cn/problems/minimum-sum-of-squared-difference/description/
    ///
    /// English:
    /// You are given two positive 0-indexed integer arrays nums1 and nums2, both of length n.
    ///
    /// The sum of squared difference of arrays nums1 and nums2 is defined as the sum of
    /// (nums1[i] - nums2[i])^2 for each 0 &lt;= i &lt; n.
    ///
    /// You are also given two positive integers k1 and k2. You can modify any of the elements
    /// of nums1 by +1 or -1 at most k1 times. Similarly, you can modify any of the elements
    /// of nums2 by +1 or -1 at most k2 times.
    ///
    /// Return the minimum sum of squared difference after modifying array nums1 at most
    /// k1 times and modifying array nums2 at most k2 times.
    ///
    /// Note: You are allowed to modify the array elements to become negative integers.
    ///
    /// 繁體中文：
    /// 給定兩個長度皆為 n、索引從 0 開始的正整數陣列 nums1 和 nums2。
    ///
    /// 陣列 nums1 和 nums2 的差值平方和，定義為所有滿足 0 &lt;= i &lt; n 的索引 i
    /// 所對應的 (nums1[i] - nums2[i])^2 之總和。
    ///
    /// 另給定兩個正整數 k1 和 k2。你可以對 nums1 中任意元素進行 +1 或 -1 的修改，
    /// 總共最多操作 k1 次。同樣地，你可以對 nums2 中任意元素進行 +1 或 -1 的修改，
    /// 總共最多操作 k2 次。
    ///
    /// 回傳在對 nums1 最多操作 k1 次、對 nums2 最多操作 k2 次後，能得到的最小差值平方和。
    ///
    /// 注意：允許將陣列元素修改為負整數。
    ///
    /// </summary>
    /// <param name="args"></param>
    /// <remarks>
    /// 直接執行 12 組固定案例，各以獨立陣列複本驗證排序貪心與二分答案。
    /// 輸出 Expected、Actual、PASS/FAIL 與總計；任何結果不符時以非零結束碼結束。
    /// 不讀取命令列參數，也不等待使用者輸入。
    /// </remarks>
    static void Main(string[] args)
    {
        Program solution = new Program();
        int[] largeDifferences = new int[100000];
        Array.Fill(largeDifferences, 100000);

        (string Name, int[] Nums1, int[] Nums2, int K1, int K2, long Expected)[] cases =
        [
            ("Official example 1", [1, 2, 3, 4], [2, 10, 20, 19], 0, 0, 579L),
            ("Official example 2", [1, 4, 10, 12], [5, 8, 6, 9], 1, 1, 43L),
            ("Equal arrays", [1, 2], [1, 2], 0, 0, 0L),
            ("Exact elimination budget", [0, 0], [3, 1], 2, 2, 0L),
            ("Excess budget", [0, 0], [3, 1], 3, 2, 0L),
            ("Single element", [0], [5], 1, 1, 9L),
            ("Equal maxima with remainder", [0, 0, 0], [4, 4, 4], 1, 1, 34L),
            ("Exact next-level cost", [0, 0, 0], [7, 4, 1], 1, 2, 33L),
            ("Multiple levels", [0, 0, 0], [9, 5, 1], 3, 3, 33L),
            ("Large square", [0], [100000], 0, 0, 10000000000L),
            ("Maximum length and sum", new int[100000], largeDifferences, 0, 0, 1000000000000000L),
            ("Maximum budgets", [0], [1], 1000000000, 1000000000, 0L)
        ];

        int passed = 0;
        int total = cases.Length * 2;
        Console.WriteLine("LeetCode 2333 - Minimum Sum of Squared Difference");
        foreach ((string name, int[] nums1, int[] nums2, int k1, int k2, long expected) in cases)
        {
            // 兩種方法都會改寫 nums1；各自使用複本，避免前一次呼叫污染下一次輸入。
            long greedy = solution.MinSumSquareDiff((int[])nums1.Clone(), (int[])nums2.Clone(), k1, k2);
            long binary = solution.MinSumSquareDiff2((int[])nums1.Clone(), (int[])nums2.Clone(), k1, k2);
            bool greedyPassed = greedy == expected;
            bool binaryPassed = binary == expected;
            passed += greedyPassed ? 1 : 0;
            passed += binaryPassed ? 1 : 0;

            Console.WriteLine($"[{name}]");
            Console.WriteLine($"  Greedy: Expected={expected}, Actual={greedy}, {(greedyPassed ? "PASS" : "FAIL")}");
            Console.WriteLine($"  Binary: Expected={expected}, Actual={binary}, {(binaryPassed ? "PASS" : "FAIL")}");
        }

        Console.WriteLine($"Summary: {passed}/{total} checks passed.");
        Environment.ExitCode = passed == total ? 0 : 1;
    }

    /// <summary>
    /// 以排序貪心求最小差值平方和：將最大的差值群組批次降低，直到預算不足以降至下一層，
    /// 再以商數與餘數平均分配剩餘操作。輸入須符合題目限制，回傳非負的最小平方和。
    /// </summary>
    /// <param name="nums1">長度為 1 至 100000、元素為 0 至 100000 的陣列；會被改寫為差值，必要時排序。</param>
    /// <param name="nums2">與 nums1 等長、元素為 0 至 100000 的陣列；不會被修改。</param>
    /// <param name="k1">nums1 可使用的操作次數，範圍為 0 至 1000000000。</param>
    /// <param name="k2">nums2 可使用的操作次數，範圍為 0 至 1000000000。</param>
    /// <returns>最多使用兩組預算後的最小差值平方和，以 long 避免平方與累加溢位。</returns>
    /// <remarks>
    /// 時間複雜度為 O(n log n)；除排序的 O(log n) 堆疊外，額外空間為 O(1)。
    /// 假設輸入合法，不額外檢查 null、長度或數值範圍。
    /// </remarks>
    public long MinSumSquareDiff(int[] nums1, int[] nums2, int k1, int k2)
    {
        int n = nums1.Length;
        long k = (long)k1 + k2;
        long ans = 0;
        long sum = 0;

        // 操作任一陣列都能讓對應差值減少 1，因此只需考慮差值與合併預算。
        for (int i = 0; i < n; i++)
        {
            nums1[i] = Math.Abs(nums1[i] - nums2[i]);
            sum += nums1[i];
            ans += (long)nums1[i] * nums1[i];
        }

        if (sum <= k)
        {
            // 題目是「最多」操作；差值全為 0 後可以停止，不必花完預算。
            return 0;
        }

        Array.Sort(nums1);
        for (int i = n - 1; ; i--)
        {
            int m = n - i;
            long v = nums1[i];
            // 已納入的最大 m 個差值都視為 v；整組降至下一個值的成本為 c。
            long c = m * (v - (i > 0 ? nums1[i - 1] : 0));
            // ans 保留尚未納入群組的原始平方和，不在每一層重算已納入的項目。
            ans -= v * v;
            if (c < k)
            {
                k -= c;
                continue;
            }

            // 每個值降低 k / m；餘數個值再降低 1，讓最終群組的差距至多為 1。
            // c == k 也在這裡直接結算；sum > k 保證不會走到負的差值。
            v -= k / m;
            long remainder = k % m;

            return ans + remainder * (v - 1) * (v - 1) + (m - remainder) * v * v;
        }
    }

    /// <summary>
    /// 以二分答案求最小差值平方和：搜尋能用預算將所有差值降至不超過該值的最小門檻，
    /// 再優先將最大的截斷差值降低 1。輸入須符合題目限制，回傳非負的最小平方和。
    /// </summary>
    /// <param name="nums1">長度為 1 至 100000、元素為 0 至 100000 的陣列；會被改寫為差值並排序。</param>
    /// <param name="nums2">與 nums1 等長、元素為 0 至 100000 的陣列；不會被修改。</param>
    /// <param name="k1">nums1 可使用的操作次數，範圍為 0 至 1000000000。</param>
    /// <param name="k2">nums2 可使用的操作次數，範圍為 0 至 1000000000。</param>
    /// <returns>最多使用兩組預算後的最小差值平方和，以 long 避免平方與累加溢位。</returns>
    /// <remarks>
    /// 設 D 為最大差值，時間為 O(n log(D + 1) + n log n)，包含最後的排序。
    /// 除排序的 O(log n) 堆疊外，額外空間為 O(1)；假設輸入合法，不額外做參數驗證。
    /// </remarks>
    public long MinSumSquareDiff2(int[] nums1, int[] nums2, int k1, int k2)
    {
        int n = nums1.Length;
        // 題目限制下合併預算至多 2000000000，仍在 int 範圍內。
        int k = k1 + k2;
        int maxDiff = 0;
        for (int i = 0; i < n; i++)
        {
            nums1[i] = Math.Abs(nums1[i] - nums2[i]);
            maxDiff = Math.Max(maxDiff, nums1[i]);
        }

        int l = 0, r = maxDiff, res = 0;
        while (l <= r)
        {
            int mid = (l + r) / 2;
            long sum = 0;
            foreach (int num in nums1)
            {
                sum += num > mid ? num - mid : 0;
            }

            // 門檻越高，所需操作越少；可行時繼續往左找最小可行門檻。
            if (sum <= k)
            {
                r = mid - 1;
                res = mid;
            }
            else
            {
                l = mid + 1;
            }
        }

        // 先扣掉截斷至 res 的成本，留下尚可分配的操作預算。
        foreach (int num in nums1)
        {
            if (num > res)
            {
                k -= num - res;
            }
        }

        // 降序取值使剩餘操作優先用於最大的截斷差值。
        Array.Sort(nums1);
        long ans = 0;
        for (int i = n - 1; i >= 0; i--)
        {
            long diff = Math.Min(nums1[i], res);
            // res > 0 時，剩餘預算小於門檻群組大小，每個最大值最多再降低 1。
            // res == 0 時不再修改；允許不花完預算。
            if (k > 0 && diff > 0)
            {
                diff--;
                k--;
            }
            ans += diff * diff;
        }
        return ans;
    }
}