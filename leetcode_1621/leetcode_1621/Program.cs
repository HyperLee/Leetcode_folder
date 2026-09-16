namespace leetcode_1621;

class Program
{
    /// <summary>
    /// 1621. Number of Sets of K Non-Overlapping Line Segments
    /// https://leetcode.com/problems/number-of-sets-of-k-non-overlapping-line-segments/description/
    /// <para>
    /// English:
    /// Given n points on a 1-D plane, where the ith point (from 0 to n-1) is at x = i, find the number of ways we can draw exactly k non-overlapping line segments such that each segment covers two or more points. The endpoints of each segment must have integral coordinates. The k line segments do not have to cover all n points, and they are allowed to share endpoints.
    ///
    /// Return the number of ways we can draw k non-overlapping line segments. Since this number can be huge, return it modulo 10^9 + 7.
    ///
    /// Example 1:
    /// Input: n = 4, k = 2
    /// Output: 5
    /// Explanation: The two line segments are shown in red and blue.
    /// The image above shows the 5 different ways {(0,2),(2,3)}, {(0,1),(1,3)}, {(0,1),(2,3)}, {(1,2),(2,3)}, {(0,1),(1,2)}.
    ///
    /// Example 2:
    /// Input: n = 3, k = 1
    /// Output: 3
    /// Explanation: The 3 ways are {(0,1)}, {(0,2)}, {(1,2)}.
    ///
    /// Example 3:
    /// Input: n = 30, k = 7
    /// Output: 796297179
    /// Explanation: The total number of possible ways to draw 7 line segments is 3796297200. Taking this number modulo 10^9 + 7 gives us 796297179.
    ///
    /// Constraints:
    /// - 2 &lt;= n &lt;= 1000
    /// - 1 &lt;= k &lt;= n - 1
    /// </para>
    /// <para>
    /// 1621. 大小為 K 的不重疊線段數目
    /// https://leetcode.cn/problems/number-of-sets-of-k-non-overlapping-line-segments/description/
    ///
    /// 繁體中文：
    /// 給定一維平面上的 n 個點，其中第 i 個點（編號從 0 到 n - 1）位於 x = i。請找出恰好繪製 k 條互不重疊線段的方式數量，且每條線段都必須涵蓋兩個或以上的點。每條線段的端點都必須具有整數座標。這 k 條線段不需要涵蓋全部 n 個點，而且允許共用端點。
    ///
    /// 請回傳繪製 k 條互不重疊線段的方式數量。由於這個數字可能非常大，請回傳其對 10^9 + 7 取模後的結果。
    ///
    /// 範例 1：
    /// 輸入：n = 4, k = 2
    /// 輸出：5
    /// 解釋：兩條線段分別以紅色與藍色表示。
    /// 上圖展示了 5 種不同的方式：{(0,2),(2,3)}、{(0,1),(1,3)}、{(0,1),(2,3)}、{(1,2),(2,3)}、{(0,1),(1,2)}。
    ///
    /// 範例 2：
    /// 輸入：n = 3, k = 1
    /// 輸出：3
    /// 解釋：共有 3 種方式：{(0,1)}、{(0,2)}、{(1,2)}。
    ///
    /// 範例 3：
    /// 輸入：n = 30, k = 7
    /// 輸出：796297179
    /// 解釋：繪製 7 條線段的所有可能方式共有 3796297200 種。將這個數字對 10^9 + 7 取模後，結果為 796297179。
    ///
    /// 限制條件：
    /// - 2 &lt;= n &lt;= 1000
    /// - 1 &lt;= k &lt;= n - 1
    /// </para>
    /// </summary>
    /// <remarks>
    /// 直接執行程式時會使用題目提供的三組範例，分別呼叫兩種解法，輸出預期值、實際值與 PASS/FAIL；
    /// 所有案例都通過時結束碼為 0，否則為 1。
    /// </remarks>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new();
        (int n, int k, int expected)[] testCases =
        {
            (4, 2, 5),
            (3, 1, 3),
            (30, 7, 796297179)
        };

        int passedCount = 0;
        for (int caseIndex = 0; caseIndex < testCases.Length; caseIndex++)
        {
            (int n, int k, int expected) testCase = testCases[caseIndex];
            int dynamicProgrammingResult = solution.NumberOfSets(testCase.n, testCase.k);
            int combinatoricsResult = solution.NumberOfSets2(testCase.n, testCase.k);
            bool dynamicProgrammingPassed = dynamicProgrammingResult == testCase.expected;
            bool combinatoricsPassed = combinatoricsResult == testCase.expected;
            bool casePassed = dynamicProgrammingPassed && combinatoricsPassed;

            Console.WriteLine($"Case: Example {caseIndex + 1} (n = {testCase.n}, k = {testCase.k})");
            Console.WriteLine($"Expected: {testCase.expected}");
            Console.WriteLine($"NumberOfSets (DP): {dynamicProgrammingResult} [{(dynamicProgrammingPassed ? "PASS" : "FAIL")}]");
            Console.WriteLine($"NumberOfSets2 (Combinatorics): {combinatoricsResult} [{(combinatoricsPassed ? "PASS" : "FAIL")}]");
            Console.WriteLine();

            if (casePassed)
            {
                passedCount++;
            }
        }

        Console.WriteLine($"Summary: {passedCount}/{testCases.Length} cases passed.");
        Environment.ExitCode = passedCount == testCases.Length ? 0 : 1;
    }

    /// <summary>
    /// 方法一：使用一維動態規劃計算恰好繪製 k 條不重疊線段的方式數量。
    /// <c>dp[j]</c> 表示目前線段數量下，所有端點不超過第 j 個點的方式數；
    /// 轉移時用前綴和一次整理「最後一條線段以 j 為右端點」的所有起點選擇。
    /// 輸入必須符合 <c>2 &lt;= n &lt;= 1000</c> 與 <c>1 &lt;= k &lt;= n - 1</c>，輸出為對 10^9 + 7 取模後的方式數。
    /// </summary>
    /// <param name="n">一維平面上的點數，範圍為 2 到 1000。</param>
    /// <param name="k">必須繪製的線段數，範圍為 1 到 n - 1。</param>
    /// <returns>繪製恰好 k 條不重疊線段的方式數量，結果已對 10^9 + 7 取模。</returns>
    public int NumberOfSets(int n, int k)
    {
        const int MOD = 1000000007;
        int[] dp = new int[n];
        int[] prefixSums = new int[n + 1];

        // k = 0 時不選任何線段，因此每個右界都只有一種空集合。
        for(int j = 0; j < n; j++)
        {
            dp[j] = 1;
            prefixSums[j + 1] = (prefixSums[j] + dp[j]) % MOD;
        }

        for(int i = 1; i <= k; i++)
        {
            dp[0] = 0;
            for(int j = 1; j < n; j++)
            {
                // 不使用 j 作為端點時沿用 dp[j - 1]；使用 j 作為最後右端點時，
                // prefixSums[j] 已累加所有合法起點 t < j 的前一輪狀態。
                dp[j] = (dp[j - 1] + prefixSums[j]) % MOD;
            }

            // 將本輪結果重建為前綴和，供下一條線段的轉移使用。
            for(int j = 0; j < n; j++)
            {
                prefixSums[j + 1] = (prefixSums[j] + dp[j]) % MOD;
            }
        }
        return dp[n - 1];
    }


    private const long MOD = 1000000007;

    /// <summary>
    /// 使用二進位快速冪計算 <c>a^e mod MOD</c>。
    /// 指數 e 必須是非負數；此方法也用來依費馬小定理求組合數分母的模反元素。
    /// </summary>
    /// <param name="a">要計算的底數，會在每次乘法後對 MOD 取模。</param>
    /// <param name="e">非負整數指數。</param>
    /// <returns>a 的 e 次方對 MOD 取模後的結果。</returns>
    private long QuickPow(long a, long e)
    {
        long res = 1;
        while(e > 0)
        {
            // 指數的最低位為 1 時，將目前這一位的冪乘入答案。
            if((e & 1) != 0)
            {
                res = res * a % MOD;
            }

            // 平方底數，準備處理指數的下一個二進位位元。
            a = a * a % MOD;
            e >>= 1;
        }
        return res;
    }

    /// <summary>
    /// 方法二：使用組合數學計算答案 <c>C(n + k - 1, 2k)</c>。
    /// 將線段端點轉換為嚴格遞增序列後，問題等價於從 n + k - 1 個位置選出 2k 個端點；
    /// 方法以乘法累計分子與分母，再用費馬小定理的模反元素完成除法。
    /// 輸入必須符合 <c>2 &lt;= n &lt;= 1000</c> 與 <c>1 &lt;= k &lt;= n - 1</c>，輸出為取模後的方式數。
    /// </summary>
    /// <param name="n">一維平面上的點數，範圍為 2 到 1000。</param>
    /// <param name="k">必須繪製的線段數，範圍為 1 到 n - 1。</param>
    /// <returns>繪製恰好 k 條不重疊線段的方式數量，結果已對 10^9 + 7 取模。</returns>
    public int NumberOfSets2(int n, int k)
    {
        int m = 2 * k;
        long numerator = 1;
        long denominator = 1;

        // C(n + k - 1, 2k) = 分子 / (2k)!；每一步都先取模避免整數溢位。
        for(int i = 1; i <= m; i++)
        {
            numerator = numerator * (n + k - i) % MOD;
            denominator = denominator * i % MOD;
        }

        // MOD 是質數，依費馬小定理以 denominator^(MOD - 2) 取代模除法。
        return (int)(numerator * QuickPow(denominator, MOD - 2) % MOD);
    }
}