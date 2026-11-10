namespace leetcode_3693;

class Program
{
    /// <summary>
    /// 3693. Climbing Stairs II
    /// https://leetcode.com/problems/climbing-stairs-ii/description/
    ///
    /// English:
    /// You are climbing a staircase with n + 1 steps, numbered from 0 to n.
    /// You are also given a 1-indexed integer array costs of length n,
    /// where costs[i] is the cost of step i.
    /// From step i, you can jump only to step i + 1, i + 2, or i + 3.
    /// The cost of jumping from step i to step j is defined as:
    /// costs[j] + (j - i)².
    /// You start from step 0 with cost = 0.
    /// Return the minimum total cost to reach step n.
    ///
    /// 3693. 爬樓梯 II
    /// https://leetcode.cn/problems/climbing-stairs-ii/description/
    ///
    /// 繁體中文：
    /// 你正在攀爬一座共有 n + 1 個階梯的樓梯，階梯編號從 0 到 n。
    /// 給定一個長度為 n、索引從 1 開始的整數陣列 costs，
    /// 其中 costs[i] 表示第 i 個階梯的費用。
    /// 從階梯 i 出發，你只能跳到階梯 i + 1、i + 2 或 i + 3。
    /// 從階梯 i 跳到階梯 j 的費用定義為：
    /// costs[j] + (j - i)²，也就是目的階梯的費用加上跳躍距離的平方。
    /// 你從階梯 0 出發，初始費用為 0。
    /// 請回傳到達階梯 n 所需的最小總費用。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        SolutionChecks.Run();
    }

    /// <summary>
    /// 方法一：动态规划
    ///
    /// </summary>
    /// <param name="n"></param>
    /// <param name="costs"></param>
    /// <returns></returns>
    public int ClimbStairs(int n, int[] costs)
    {
        int a = 0, b = 0, c = 0;
        for (int i = 0; i < n; i++)
        {
            int next = Math.Min(Math.Min(a + 9, b + 4), c + 1) + costs[i];
            a = b;
            b = c;
            c = next;
        }
        return c;
    }

    /// <summary>
    /// 解法二：記憶化搜尋（由上而下的動態規劃）。
    /// 遞迴搜尋 + 保存遞迴回傳值 = 記憶化搜尋。
    /// 定義 Dfs(i) 為從第 0 階到達第 i 階的最小總費用，答案為 Dfs(n)。
    /// 每個狀態最多檢查三個前驅且只計算一次，時間複雜度 O(n)。
    /// memo 與遞迴呼叫堆疊各占 O(n) 空間；大 n 時須留意執行環境的堆疊限制，
    /// 解法一則只需 O(1) 額外空間。
    /// 改寫自靈茶山艾府的 Java 解法（非商業引用，保留作者出處）：
    /// https://leetcode.cn/problems/climbing-stairs-ii/solutions/3793871/jiao-ni-yi-bu-bu-si-kao-dpcong-ji-yi-hua-lkrf/
    /// </summary>
    /// <param name="n">目標階梯編號。</param>
    /// <param name="costs">第 i 階的費用存於 costs[i - 1]。</param>
    /// <returns>到達第 n 階的最小總費用。</returns>
    public int ClimbStairs2(int n, int[] costs)
    {
        // 每次呼叫重新建立快取，避免不同輸入共用舊結果。
        int[] memo = new int[n + 1];
        return Dfs(n, costs, memo);
    }

    private int Dfs(int i, int[] costs, int[] memo)
    {
        // 遞迴終點：出發點第 0 階不需支付費用。
        if (i == 0)
        {
            return 0;
        }

        // 題目費用與跳躍平方皆為正，因此非起點的答案必大於 0。
        // 可用陣列預設值 0 表示「尚未計算」。命中快取就直接回傳。
        if (memo[i] != 0)
        {
            return memo[i];
        }

        int res = int.MaxValue;
        // 最後一步只能來自 i-3、i-2、i-1，且前驅不能小於第 0 階。
        // Dfs(j) 是到達前驅的最小費用，(i-j)² 是本次跳躍的費用。
        for (int j = Math.Max(i - 3, 0); j < i; j++)
        {
            res = Math.Min(res, Dfs(j, costs, memo) + ((i - j) * (i - j)));
        }

        // 無論從哪個 j 跳來都會落在 i，因此落點費用可移到迴圈外只加一次。
        // 題目的階梯從 1 編號，但 C# 陣列從 0 索引，所以使用 costs[i - 1]。
        res += costs[i - 1];
        memo[i] = res; // 保存本次回傳值，下次遇到相同狀態即可重用。
        return res;
    }

    /// <summary>
    /// 解法三：將記憶化搜尋 1:1 翻譯成遞推（由下而上的動態規劃）。
    /// f[i] 對應 Dfs(i)，表示從第 0 階到達第 i 階的最小總費用。
    /// 遞迴終點 Dfs(0) = 0 對應 f[0] = 0（陣列的預設值）。
    /// 將遞迴呼叫 Dfs(j) 換成讀取 f[j]，將保存回傳值換成寫入 f[i]。
    /// 因為前驅 j 一定小於 i，依序計算 i = 1 到 n 時，f[j] 都已經算好。
    /// 每個狀態最多檢查三個前驅，時間複雜度 O(n)，陣列空間 O(n)。
    /// 此解法沒有遞迴呼叫堆疊；解法一則進一步用滾動變數將空間降為 O(1)。
    /// 改寫自靈茶山艾府的 Java 遞推解法（非商業引用，保留作者出處）：
    /// https://leetcode.cn/problems/climbing-stairs-ii/solutions/3793871/jiao-ni-yi-bu-bu-si-kao-dpcong-ji-yi-hua-lkrf/
    /// </summary>
    /// <param name="n">目標階梯編號。</param>
    /// <param name="costs">第 i 階的費用存於 costs[i - 1]。</param>
    /// <returns>到達第 n 階的最小總費用。</returns>
    public int ClimbStairs3(int n, int[] costs)
    {
        int[] f = new int[n + 1];
        for (int i = 1; i <= n; i++)
        {
            int res = int.MaxValue;
            // 最後一步來自前面一到三階，且階梯編號不能小於 0。
            for (int j = Math.Max(i - 3, 0); j < i; j++)
            {
                res = Math.Min(res, f[j] + ((i - j) * (i - j)));
            }

            // 落點費用與前驅 j 無關，取最小值後再統一加上。
            f[i] = res + costs[i - 1];
        }

        return f[n];
    }
}