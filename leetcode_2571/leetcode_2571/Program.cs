namespace leetcode_2571;

class Program
{
    /// <summary>
    /// 2571. Minimum Operations to Reduce an Integer to 0
    /// https://leetcode.com/problems/minimum-operations-to-reduce-an-integer-to-0/description/
    ///
    /// Problem (English):
    /// You are given a positive integer n. You can do the following operation any number of times:
    ///     Add or subtract a power of 2 from n.
    /// Return the minimum number of operations to make n equal to 0.
    /// A number x is power of 2 if x == 2^i where i >= 0.
    ///
    /// 題目（繁體中文）:
    /// 給定一個正整數 n，你可以任意次數執行以下操作：
    ///     對 n 加上或減去一個 2 的冪次方（power of 2）。
    /// 回傳將 n 變為 0 所需的最少操作次數。
    /// 若 x 為 2 的冪次方，則 x == 2^i（i >= 0）。
    ///
    /// 參考：
    /// https://leetcode.cn/problems/minimum-operations-to-reduce-an-integer-to-0/description/
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("=== Minimum Operations to Reduce Binary Number to Zero ===");
        Console.WriteLine();

        var program = new Program();

        // 測試案例：(輸入值, 預期結果)
        (int n, int expected)[] testCases =
        [
            (1, 1),      // 1 (1) -> 0，操作：-1
            (2, 1),      // 2 (10) -> 0，操作：-2
            (3, 2),      // 3 (11) -> 4 (100) -> 0，操作：+1, -4
            (7, 2),      // 7 (111) -> 8 (1000) -> 0，操作：+1, -8
            (15, 2),     // 15 (1111) -> 16 (10000) -> 0，操作：+1, -16
            (39, 3),     // 39 (100111) -> 40 (101000) -> 32 -> 0
            (100, 3),    // 100 (1100100) 需要 3 次操作
        ];

        int passed = 0;
        int failed = 0;

        foreach (var (n, expected) in testCases)
        {
            int result1 = program.MinOperations(n);
            int result2 = program.MinOperations2(n);
            string binary = Convert.ToString(n, 2);

            bool isPass = result1 == expected && result2 == expected;
            string status = isPass ? "✅ PASS" : "❌ FAIL";

            Console.WriteLine($"{status}: n={n,-5} (二進位: {binary,-10}) -> 解法一={result1}, 解法二={result2}, 預期={expected}");

            if (isPass)
            {
                passed++;
            }
            else
            {
                failed++;
            }
        }

        Console.WriteLine();
        Console.WriteLine($"=== 測試結果：{passed} 通過，{failed} 失敗 ===");
    }

    /// <summary>
    /// 計算將正整數 n 變為 0 的最少操作次數。
    /// 每次操作可以加或減 2^i（i ≥ 0）。
    /// </summary>
    /// <param name="n">要處理的正整數</param>
    /// <returns>最少操作次數</returns>
    /// <example>
    /// <code>
    /// int result = Solution.MinOperations(7); // 返回 2
    ///  7 (111) -> 8 (1000) -> 0，操作：+1, -8
    /// </code>
    /// </example>
    /// <remarks>
    /// 解題思路：
    /// 1. 將問題看作二進位制處理
    /// 2. 從低位到高位處理每一位，同時維護進位
    /// 3. 當前位值 = 原位值 + 進位
    ///    - 若值為 0：不需要操作
    ///    - 若值為 1：需要一次操作（減法消掉或加法產生進位）
    ///    - 若值為 2（位 1 + 進位 1）：直接進位，不增加操作
    /// 4. 對於連續的 1，使用加法產生進位更優（例如 111 -> 1000 只需 2 步）
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public int MinOperations(int n)
    {
        if (n <= 0)
        {
            return 0;
        }

        int operations = 0;
        int carry = 0;

        // 從低位到高位處理每一位
        while (n > 0 || carry > 0)
        {
            // 當前位值 = 原位值 + 進位
            int currentBit = (n & 1) + carry;

            // 根據當前位值決定操作
            switch (currentBit)
            {
                // 值為 0：不需要操作，無進位
                case 0:
                    carry = 0;
                    break;

                // 值為 1：需要判斷下一位來決定最優策略
                // 如果下一位也是 1，則用加法產生進位更優（處理連續 1 的情況）
                case 1 when ((n >> 1) & 1) == 1:
                    operations++;
                    carry = 1;
                    break;

                // 值為 1 且下一位是 0：直接減法消掉
                case 1:
                    operations++;
                    carry = 0;
                    break;

                // 值為 2（位 1 + 進位 1）：直接進位，不增加操作
                case 2:
                    carry = 1;
                    break;

                // 值為 3（理論上不會出現，但為了完整性）
                default:
                    operations++;
                    carry = 1;
                    break;
            }

            // 右移處理下一位
            n >>= 1;
        }

        return operations;
    }

    /// <summary>
    /// 計算將正整數 n 變為 0 的最少操作次數（解法二：Lowbit 貪心法）。
    /// 每次操作可以加或減 2^i（i ≥ 0）。
    /// </summary>
    /// <param name="n">要處理的正整數</param>
    /// <returns>最少操作次數</returns>
    /// <example>
    /// <code>
    /// int result = MinOperations2(7); // 返回 2
    ///  7 (111) -> 8 (1000) -> 0，操作：+1, -8
    /// </code>
    /// </example>
    /// <remarks>
    /// <para><b>解題思路：Lowbit 貪心策略</b></para>
    /// <para>
    /// 核心觀察：把 n 看成二進位數，較高位的位元 1 會受到較低位位元 1 的加減影響，
    /// 但最小的位元 1（lowbit）沒有這個約束，因此優先處理 lowbit。
    /// </para>
    /// <para><b>Lowbit 計算原理：</b></para>
    /// <para>
    /// <c>lowbit = n &amp; -n</c> 可取得 n 的最低位 1 所代表的數值。
    /// 例如：n = 12 (1100)，lowbit = 4 (100)。
    /// </para>
    /// <para><b>貪心策略：</b></para>
    /// <list type="bullet">
    ///   <item>若有多個連續的 1：使用加法（n += lowbit），可一次消除多個 1</item>
    ///   <item>若為單個 1：使用減法（n -= lowbit）更優</item>
    /// </list>
    /// <para><b>判斷連續 1 的方法：</b></para>
    /// <para>
    /// <c>(n &amp; (lb &lt;&lt; 1)) &gt; 0</c> 檢查 lowbit 的左邊一位是否也是 1。
    /// 若是，代表有連續的 1，應使用加法。
    /// </para>
    /// <para><b>終止條件：</b></para>
    /// <para>
    /// 當 <c>(n &amp; (n - 1)) == 0</c> 時，n 是 2 的冪次方，只剩一個 1，
    /// 只需一次減法操作即可歸零。
    /// </para>
    /// </remarks>
    public int MinOperations2(int n)
    {
        // 最終 n 會變成 2 的冪次，需要一次操作歸零
        int operations = 1;

        // 當 n & (n - 1) > 0 時，n 不是 2 的冪次（有超過一個位元 1）
        while ((n & (n - 1)) > 0)
        {
            // 取得最低位的 1（lowbit）
            // 原理：-n 是 n 的二補數，n & -n 會保留最低位的 1
            // 例如：n = 12 (1100)，-n = -12 (補碼: ...110100)，n & -n = 4 (100)
            int lb = n & -n;

            // 判斷是否有連續的 1：檢查 lowbit 左邊一位是否為 1
            // lb << 1 將 lowbit 左移一位，若與 n 做 AND 後 > 0，代表該位也是 1
            if ((n & (lb << 1)) > 0)
            {
                // 有多個連續的 1：使用加法，可透過進位一次消除多個連續的 1
                // 例如：7 (111) + 1 = 8 (1000)，三個連續 1 變成一個 1
                n += lb;
            }
            else
            {
                // 單個 1：直接減掉更優
                // 例如：8 (1000) - 8 = 0
                n -= lb;
            }

            operations++;
        }

        return operations;
    }
}
