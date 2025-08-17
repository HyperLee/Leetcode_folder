namespace leetcode_837;

class Program
{
    /// <summary>
    /// 837. 新 21 點
    /// Alice 玩一個類似「21點」的遊戲：
    /// Alice 從 0 分開始，只要分數小於 k，就持續抽牌。每次抽牌，她會隨機獲得 [1, maxPts] 範圍內的一個整數分數（maxPts 為整數），每次抽牌獨立且機率均等。
    /// 當分數達到 k 或以上時，Alice 停止抽牌。
    /// 請回傳 Alice 最終分數不超過 n 的機率。
    /// 答案誤差在 10^-5 以內都視為正確。
    /// 英文題目：https://leetcode.com/problems/new-21-game/description/?envType=daily-question&envId=2025-08-17
    /// 中文題目：https://leetcode.cn/problems/new-21-game/description/?envType=daily-question&envId=2025-08-17
    /// </summary>
    static void Main(string[] args)
    {
        // 測試資料
        int n = 21;
        int k = 17;
        int maxPts = 10;
        var prob = new Program().New21Game(n, k, maxPts);
        Console.WriteLine($"n={n}, k={k}, maxPts={maxPts} => 機率={prob:F5}");
        // 其他測試
        Console.WriteLine($"n=6, k=1, maxPts=10 => 機率={new Program().New21Game(6, 1, 10):F5}");
        Console.WriteLine($"n=10, k=1, maxPts=10 => 機率={new Program().New21Game(10, 1, 10):F5}");
    }

    /// <summary>
    /// 求 Alice 玩新 21 點遊戲，最終分數不超過 n 的機率。
    /// 解題思路：
    /// 定義 dp[i] 為從 i 分開始，最終分數落在 [k, n] 的機率。
    /// 當 i >= k 且 i <= n，遊戲結束且分數合法，dp[i]=1。
    /// 當 i > n，遊戲結束但分數超過 n，dp[i]=0。
    /// 當 i < k，需等概率抽取 [1, maxPts]，狀態轉移：
    /// dp[i] = (dp[i+1] + dp[i+2] + ... + dp[i+maxPts]) / maxPts。
    /// 利用滑動窗口優化計算。
    /// 
    /// ref:
    /// https://leetcode.cn/problems/new-21-game/solutions/3755107/hua-dong-chuang-kou-you-hua-dpjian-ji-xi-lybl/?envType=daily-question&envId=2025-08-17
    /// https://leetcode.cn/problems/new-21-game/solutions/273085/huan-you-bi-zhe-geng-jian-dan-de-ti-jie-ma-tian-ge/?envType=daily-question&envId=2025-08-17
    /// https://leetcode.cn/problems/new-21-game/solutions/272858/zen-yang-de-dao-guan-fang-ti-jie-zhong-de-zhuang-t/?envType=daily-question&envId=2025-08-17
    /// 
    /// <example>
    /// <code>
    /// var prob = New21Game(21, 17, 10); // 機率約 0.73278
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="n">最終分數上限</param>
    /// <param name="k">停止抽牌的分數</param>
    /// <param name="maxPts">每次抽牌最大分數</param>
    /// <returns>最終分數不超過 n 的機率</returns>
    public double New21Game(int n, int k, int maxPts)
    {
        // dp[i]: 從 i 分開始，最終分數落在 [k, n] 的機率
        double[] dp = new double[n + 1];
        double sum = 0;
        // 倒序計算，i 代表目前分數
        for (int i = n; i >= 0; i--)
        {
            // 當分數 >= k 且 <= n，遊戲結束且合法
            if (i >= k && i <= n)
            {
                dp[i] = 1;
            }
            // 當分數 < k，需等概率抽取 [1, maxPts]
            else if (i < k)
            {
                dp[i] = sum / maxPts;
            }
            // i > n 的情況，dp[i]=0，陣列外不用處理
            // 更新滑動窗口 sum
            sum += dp[i];
            // 滑動窗口移除右端點
            if (i + maxPts <= n)
            {
                sum -= dp[i + maxPts];
            }
        }
        // dp[0] 為答案
        return dp[0];
    }
}
