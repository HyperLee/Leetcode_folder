namespace leetcode_2787;

class Program
{
    /// <summary>
    /// 2787. Ways to Express an Integer as Sum of Powers
    /// https://leetcode.com/problems/ways-to-express-an-integer-as-sum-of-powers/description/?envType=daily-question&envId=2025-08-12
    /// 2787. 將一個數字表示成冪的和的方案數
    /// https://leetcode.cn/problems/ways-to-express-an-integer-as-sum-of-powers/description/?envType=daily-question&envId=2025-08-12
    /// 
    /// 給定兩個正整數 n 和 x。
    /// 
    /// 請回傳將 n 表示為若干個不同正整數的 x 次冪之和的方案數。換句話說，回傳有多少組唯一整數集合 [n1, n2, ..., nk]，使得 n = n1^x + n2^x + ... + nk^x。
    /// 
    /// 由於答案可能非常大，請將結果對 10^9 + 7 取模。
    /// 
    /// 例如，若 n = 160 且 x = 3，其中一種表示方式為 n = 2^3 + 3^3 + 5^3。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
    // 測試資料
    var program = new Program();
    int n1 = 10, x1 = 2;
    int n2 = 160, x2 = 3;
    int n3 = 100, x3 = 2;

    Console.WriteLine($"NumberOfWays(n={n1}, x={x1}) = {program.NumberOfWays(n1, x1)}"); // 預期: 1
    Console.WriteLine($"NumberOfWays2(n={n1}, x={x1}) = {program.NumberOfWays2(n1, x1)}");

    Console.WriteLine($"NumberOfWays(n={n2}, x={x2}) = {program.NumberOfWays(n2, x2)}"); // 範例: 2^3+3^3+5^3
    Console.WriteLine($"NumberOfWays2(n={n2}, x={x2}) = {program.NumberOfWays2(n2, x2)}");

    Console.WriteLine($"NumberOfWays(n={n3}, x={x3}) = {program.NumberOfWays(n3, x3)}");
    Console.WriteLine($"NumberOfWays2(n={n3}, x={x3}) = {program.NumberOfWays2(n3, x3)}");
    }


    /// <summary>
    /// 動態規劃解法：計算將 n 表示為若干個不同正整數的 x 次冪之和的方案數。
    /// 
    /// 解題說明：
    /// 本題可視為 0-1 背包問題。將 n 視為背包容量，[1^x, 2^x, 3^x, ...] 視為物品。
    /// 其中 maxBase 的計算方式需特別注意，必須用整數遞增直到 i^x > n，
    /// 避免浮點誤差導致最大基底被低估（如 64^(1/3) 會因精度問題變成 3.999... 而取整數後為 3）。
    /// 設 dp[i, j] 表示前 i 個數字中，選擇不同數字的 x 次冪之和為 j 的方案數。
    /// 狀態轉移：
    ///   - 不選第 i 個數字：dp[i, j] = dp[i-1, j]
    ///   - 選第 i 個數字（i^x <= j）：dp[i, j] += dp[i-1, j-i^x]
    /// 最終 dp[maxBase, n] 即為答案。
    /// </summary>
    /// <param name="n">目標數字</param>
    /// <param name="x">冪次</param>
    /// <returns>方案數 (取模 10^9+7)</returns>
    public int NumberOfWays(int n, int x)
    {
        // 取模常數
        int mod = 1000000007;
        // 正確計算最大基底：找到最大的 i 使得 i^x <= n
        int maxBase = 1;
        while (Math.Pow(maxBase, x) <= n)
        {
            maxBase++;
        }
        maxBase--; // 使 maxBase^x <= n

        // dp[i, j]: 前 i 個數字中，選擇不同數字的 x 次冪之和為 j 的方案數
        int[,] dp = new int[maxBase + 1, n + 1];
        dp[0, 0] = 1; // 初始狀態：不選任何數，和為 0 的方案數為 1

        // 枚舉 1~maxBase 作為可選數字
        for (int i = 1; i <= maxBase; i++)
        {
            int power = (int)Math.Pow(i, x); // 當前數字的 x 次冪
            for (int j = 0; j <= n; j++)
            {
                // 不選 i：方案數繼承自上一層
                dp[i, j] = dp[i - 1, j];
                // 選 i：若 j >= i^x，累加選擇 i 的方案數
                if (j >= power)
                {
                    dp[i, j] = (dp[i, j] + dp[i - 1, j - power]) % mod;
                }
            }
        }

        // 回傳將 n 表示為若干個不同正整數的 x 次冪之和的方案數
        return dp[maxBase, n];
    }

    private const int MOD = 1000000007;

    /// <summary>
    /// 動態規劃解法：計算將 n 表示為若干個不同正整數的 x 次冪之和的方案數。
    /// 
    /// 解題說明：
    /// 將 n 視為背包容量，[1^x, 2^x, 3^x, ...] 視為物品，題目本質為 0-1 背包問題。
    /// 設 dp[i, j] 表示前 i 個數字中，選擇不同數字的 x 次冪之和為 j 的方案數。
    /// 狀態轉移：
    ///   - 不選第 i 個數字：dp[i, j] = dp[i-1, j]
    ///   - 選第 i 個數字（i^x <= j）：dp[i, j] += dp[i-1, j-i^x]
    /// 最終 dp[n, n] 即為答案。
    /// </summary>
    /// <param name="n">目標數字</param>
    /// <param name="x">冪次</param>
    /// <returns>方案數 (取模 10^9+7)</returns>
    public int NumberOfWays2(int n, int x)
    {
        // dp[i, j]：前 i 個數字中，選擇不同數字的 x 次冪之和為 j 的方案數
        long[,] dp = new long[n + 1, n + 1];
        dp[0, 0] = 1; // 初始狀態：不選任何數，和為 0 的方案數為 1

        // 外層枚舉 1~n 作為可選數字
        for (int i = 1; i <= n; i++)
        {
            long val = (long)Math.Pow(i, x); // 當前數字的 x 次冪
            for (int j = 0; j <= n; j++)
            {
                // 不選 i：方案數繼承自上一層
                dp[i, j] = dp[i - 1, j];
                // 選 i：若 j >= i^x，累加選擇 i 的方案數
                if (j >= val)
                {
                    dp[i, j] = (dp[i, j] + dp[i - 1, j - (int)val]) % MOD;
                }
            }
        }
        // 回傳將 n 表示為若干個不同正整數的 x 次冪之和的方案數
        return (int)dp[n, n];
    }
}
