namespace leetcode_808;

class Program
{
    /// <summary>
    /// 808. Soup Servings
    /// https://leetcode.com/problems/soup-servings/description/
    /// <para>
    /// You have two soups, A and B, each starting with n mL. On every turn, one of these four serving operations is chosen at random, each with probability 0.25 independently of all previous turns:
    /// - Pour 100 mL from A and 0 mL from B.
    /// - Pour 75 mL from A and 25 mL from B.
    /// - Pour 50 mL from A and 50 mL from B.
    /// - Pour 25 mL from A and 75 mL from B.
    ///
    /// Notes:
    /// - There is no operation that pours 0 mL from A and 100 mL from B.
    /// - The amounts from A and B are poured simultaneously during a turn.
    /// - If an operation asks for more soup than remains, pour all that remains of that soup.
    ///
    /// The process stops immediately after any turn in which one of the soups is used up.
    ///
    /// Return the probability that A is used up before B, plus half the probability that both soups are used up in the same turn. Answers within 10^-5 of the actual answer are accepted.
    ///
    /// Example 1:
    /// Input: n = 50
    /// Output: 0.62500
    /// Explanation: With either of the first two operations, A becomes empty first. With the third, A and B become empty together. With the fourth, B becomes empty first. The requested probability is 0.25 * (1 + 1 + 0.5 + 0) = 0.625.
    ///
    /// Example 2:
    /// Input: n = 100
    /// Output: 0.71875
    /// Explanation: After the first operation, A becomes empty first. After the second operation, A becomes empty when the next operation is [1, 2, 3], and both become empty when it is 4. After the third operation, A becomes empty when the next operation is [1, 2], and both become empty when it is 3. After the fourth operation, A becomes empty when the next operation is 1, and both become empty when it is 2. The requested total probability is 0.71875.
    ///
    /// Constraints:
    /// - 0 &lt;= n &lt;= 10^9
    /// </para>
    /// <para>
    /// 808. 分湯
    /// https://leetcode.cn/problems/soup-servings/description/
    ///
    /// 有 A、B 兩種湯，起初各有 n mL。每一回合會隨機選擇下列四種供應操作之一；各操作機率皆為 0.25，且與先前回合相互獨立：
    /// - 從 A 倒出 100 mL，從 B 倒出 0 mL。
    /// - 從 A 倒出 75 mL，從 B 倒出 25 mL。
    /// - 從 A 倒出 50 mL，從 B 倒出 50 mL。
    /// - 從 A 倒出 25 mL，從 B 倒出 75 mL。
    ///
    /// 注意：
    /// - 沒有從 A 倒出 0 mL、從 B 倒出 100 mL 的操作。
    /// - 每回合會同時倒出 A 與 B 的指定份量。
    /// - 若操作要求的份量超過某種湯的剩餘量，則倒出該湯的全部剩餘量。
    ///
    /// 任何一種湯在某回合用盡後，程序立即停止。
    ///
    /// 回傳 A 比 B 先用盡的機率，加上 A、B 在同一回合用盡之機率的一半。與實際答案相差不超過 10^-5 的答案都會被接受。
    ///
    /// 範例 1：
    /// 輸入：n = 50
    /// 輸出：0.62500
    /// 解釋：執行前兩種操作之一時，A 會先用盡；執行第三種時，A 與 B 同時用盡；執行第四種時，B 先用盡。因此所求機率為 0.25 * (1 + 1 + 0.5 + 0) = 0.625。
    ///
    /// 範例 2：
    /// 輸入：n = 100
    /// 輸出：0.71875
    /// 解釋：先執行第一種操作時，A 先用盡。先執行第二種後，下一次操作為 [1, 2, 3] 時 A 用盡，為 4 時兩者同時用盡。先執行第三種後，下一次操作為 [1, 2] 時 A 用盡，為 3 時兩者同時用盡。先執行第四種後，下一次操作為 1 時 A 用盡，為 2 時兩者同時用盡。所求總機率為 0.71875。
    ///
    /// 限制條件：
    /// - 0 &lt;= n &lt;= 10^9
    /// </para>
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
