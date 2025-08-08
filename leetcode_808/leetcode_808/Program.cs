namespace leetcode_808;

class Program
{
    /// <summary>
    /// 808. Soup Servings
    /// https://leetcode.com/problems/soup-servings/description/?envType=daily-question&envId=2025-08-08
    /// 808. 分湯
    /// https://leetcode.cn/problems/soup-servings/description/?envType=daily-question&envId=2025-08-08
    ///
    /// 題目說明：
    /// 有兩種湯 A 和 B，每種最初都有 n 毫升。每回合隨機選擇以下四種操作之一（機率均為 0.25）：
    /// 1. A 減 100，B 不變
    /// 2. A 減 75，B 減 25
    /// 3. A 減 50，B 減 50
    /// 4. A 減 25，B 減 75
    /// 若某次操作超過剩餘量，則只倒出剩下的全部。只要有一種湯被倒完，過程即停止。
    ///
    /// 請返回「A 先倒完」的機率，加上「A 和 B 同時倒完」的機率的一半。
    ///
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        int[] testCases = { 50, 100, 200, 500, 1000, 5000 };
        var program = new Program();
        foreach (var n in testCases)
        {
            double result = program.SoupServings(n);
            Console.WriteLine($"n = {n}, 機率 = {result}");
        }
    }


    /// <summary>
    /// 動態規劃解法，計算分湯問題的期望值。
    /// 
    /// 解題思路：
    /// 1. 由於每次操作都是 25 的倍數，可將 n 轉換為單位份數（n = ceil(n/25)），四種操作分別為 (4,0)、(3,1)、(2,2)、(1,3)。
    /// 2. 狀態 dp[i][j] 表示 A 剩 i 份、B 剩 j 份時，最終答案的期望值。
    /// 3. 狀態轉移：
    ///    dp[i][j] = 0.25 * (dp[i-4][j] + dp[i-3][j-1] + dp[i-2][j-2] + dp[i-1][j-3])
    ///    其中 i, j < 0 時視為 0。
    /// 4. 邊界條件：
    ///    - i <= 0 且 j <= 0：A、B 同時倒完，答案為 0.5
    ///    - i <= 0 且 j > 0：A 先倒完，答案為 1
    ///    - i > 0 且 j <= 0：B 先倒完，答案為 0
    /// 5. 當 n 很大時（n >= 179），A 幾乎必定先倒完，直接回傳 1.0。
    ///
    /// 時間複雜度 O(n^2)，空間複雜度 O(n^2)。
    /// </summary>
    /// <param name="n">初始湯量（毫升）</param>
    /// <returns>期望值（A 先倒完的機率 + 同時倒完的機率 * 0.5）</returns>
    public double SoupServings(int n)
    {
        // 1. 將 n 換算成 25 毫升為一單位的份數
        n = (int)Math.Ceiling((double)n / 25);
        // 2. 當 n >= 179 時，A 幾乎必定先倒完，直接回傳 1.0
        if (n >= 179)
        {
            return 1.0; // 當 n >= 179 時，結果趨近於 1
        }

        // 3. 建立 dp 陣列，dp[i][j] 表示 A 剩 i 份、B 剩 j 份時的期望值
        double[][] dp = new double[n + 1][];
        for (int i = 0; i <= n; i++)
        {
            dp[i] = new double[n + 1];
        }

        // 4. 邊界條件
        dp[0][0] = 0.5; // A、B 同時倒完
        for (int i = 1; i <= n; i++)
        {
            dp[0][i] = 1.0; // A 先倒完
            dp[i][0] = 0.0; // B 先倒完
        }

        // 5. 狀態轉移
        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                // 四種分配方式，平均機率 0.25
                dp[i][j] = (
                    dp[Math.Max(0, i - 4)][j] +           // (4,0)
                    dp[Math.Max(0, i - 3)][Math.Max(0, j - 1)] + // (3,1)
                    dp[Math.Max(0, i - 2)][Math.Max(0, j - 2)] + // (2,2)
                    dp[Math.Max(0, i - 1)][Math.Max(0, j - 3)]   // (1,3)
                ) / 4.0;
            }
        }

        // 6. 回傳初始狀態 (n, n) 的期望值
        return dp[n][n];
    }
}
