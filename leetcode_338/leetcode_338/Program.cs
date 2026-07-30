namespace leetcode_338;

class Program
{
    /// <summary>
    /// 338. Counting Bits
    /// https://leetcode.com/problems/counting-bits/description/?envType=problem-list-v2&envId=oizxjoit
    /// 338. 位元數
    /// https://leetcode.cn/problems/counting-bits/description/
    /// 
    /// ref:
    /// https://leetcode.cn/problems/counting-bits/solutions/7882/hen-qing-xi-de-si-lu-by-duadua/
    /// https://leetcode.cn/problems/counting-bits/solutions/631479/yi-bu-bu-fen-xi-tui-dao-chu-dong-tai-gui-3yog/
    /// https://leetcode.cn/problems/counting-bits/solutions/627418/bi-te-wei-ji-shu-by-leetcode-solution-0t1i/
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();

        (string Name, int Input, int[] Expected)[] testCases =
        {
            ("題目下界", 0, new[] { 0 }),
            ("最小正整數", 1, new[] { 0, 1 }),
            ("官方範例一", 2, new[] { 0, 1, 1 }),
            ("官方範例二", 5, new[] { 0, 1, 1, 2, 1, 2 }),
            ("跨越 2 的三次方", 8, new[] { 0, 1, 1, 2, 1, 2, 2, 3, 1 }),
            (
                "跨越 2 的四次方",
                16,
                new[] { 0, 1, 1, 2, 1, 2, 2, 3, 1, 2, 2, 3, 2, 3, 3, 4, 1 })
        };

        int passedChecks = 0;

        for (int index = 0; index < testCases.Length; index++)
        {
            (string name, int input, int[] expected) = testCases[index];
            int[] result1 = solution.CountBits(input);
            int[] result2 = solution.CountBits2(input);
            bool result1Passed = result1.SequenceEqual(expected);
            bool result2Passed = result2.SequenceEqual(expected);

            passedChecks += result1Passed ? 1 : 0;
            passedChecks += result2Passed ? 1 : 0;

            Console.WriteLine($"案例 {index + 1}：{name}");
            Console.WriteLine($"  輸入：{input}");
            Console.WriteLine($"  預期：[{string.Join(", ", expected)}]");
            Console.WriteLine(
                $"  解法一（右移＋最低位）：[{string.Join(", ", result1)}] => {(result1Passed ? "PASS" : "FAIL")}");
            Console.WriteLine(
                $"  解法二（奇偶遞推）：[{string.Join(", ", result2)}] => {(result2Passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        int totalChecks = testCases.Length * 2;
        Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
    }


    /// <summary>
    /// 計算從 0 到 <paramref name="n"/> 每個整數的二進位表示中 1 的個數。
    /// 解題概念是移除目前數字的最低位，重用 <c>dp[i >> 1]</c>，
    /// 再以 <c>i &amp; 1</c> 判斷被移除的最低位是否為 1。
    /// 輸入需符合 <c>0 &lt;= n &lt;= 100000</c>；輸出索引 <c>i</c>
    /// 對應數字 <c>i</c> 的位元 1 數量。時間複雜度為 O(n)，回傳陣列空間為 O(n)，
    /// 不計回傳陣列時的額外空間為 O(1)。
    /// </summary>
    /// <param name="n">要計算的非負整數上限，範圍為 0 到 100000。</param>
    /// <returns>長度為 <c>n + 1</c> 的陣列，其中索引 <c>i</c> 的值為 <c>i</c> 的位元 1 數量。</returns>
    public int[] CountBits(int n)
    {
        int[] dp = new int[n + 1];

        for (int i = 1; i <= n; i++)
        {
            // 右移移除最低位；再把最低位是否為 1 加回已知結果。
            dp[i] = dp[i >> 1] + (i & 1);
        }

        return dp;
    }

    /// <summary>
    /// 計算從 0 到 <paramref name="n"/> 每個整數的二進位表示中 1 的個數。
    /// 解題概念是依奇偶性重用較小數字的結果：偶數除以 2 後位元 1 數量不變，
    /// 奇數則比前一個偶數多一個最低位 1。
    /// 輸入需符合 <c>0 &lt;= n &lt;= 100000</c>；輸出索引 <c>i</c>
    /// 對應數字 <c>i</c> 的位元 1 數量。時間複雜度為 O(n)，回傳陣列空間為 O(n)，
    /// 不計回傳陣列時的額外空間為 O(1)。
    /// </summary>
    /// <param name="n">要計算的非負整數上限，範圍為 0 到 100000。</param>
    /// <returns>長度為 <c>n + 1</c> 的陣列，其中索引 <c>i</c> 的值為 <c>i</c> 的位元 1 數量。</returns>
    public int[] CountBits2(int n)
    {
        int[] result = new int[n + 1];

        for (int i = 1; i <= n; i++)
        {
            if (i % 2 == 1)
            {
                // 奇數由前一個偶數補上最低位 1，因此位元 1 數量多一個。
                result[i] = result[i - 1] + 1;
            }
            else
            {
                // 偶數除以 2 等同右移一位，不會移除任何位元 1。
                result[i] = result[i / 2];
            }
        }

        return result;
    }
}
