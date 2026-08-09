namespace _2483;

class Program
{
    /// <summary>
    /// <para>
    /// 2483. Minimum Penalty for a Shop
    /// https://leetcode.com/problems/minimum-penalty-for-a-shop/description/
    ///
    /// You are given a 0-indexed customer log string customers containing only 'N' and 'Y'. Character 'Y' at index i means customers arrive during hour i, while 'N' means none arrive. If the shop closes at hour j, where 0 &lt;= j &lt;= n, add 1 penalty for each open hour without customers and for each closed hour with customers. Return the earliest closing hour that produces the minimum penalty. Closing at hour j means the shop is closed during hour j.
    ///
    /// Example 1:
    /// Input: customers = "YYNY"
    /// Output: 2
    /// Explanation: Closing at hour 0 gives 1 + 1 + 0 + 1 = 3 penalty; hour 1 gives 0 + 1 + 0 + 1 = 2; hour 2 gives 0 + 0 + 0 + 1 = 1; hour 3 gives 0 + 0 + 1 + 1 = 2; hour 4 gives 0 + 0 + 1 + 0 = 1. Hours 2 and 4 are minimal, and 2 is earlier.
    ///
    /// Example 2:
    /// Input: customers = "NNNNN"
    /// Output: 0
    /// Explanation: Closing at hour 0 is best because no customers arrive.
    ///
    /// Example 3:
    /// Input: customers = "YYYY"
    /// Output: 4
    /// Explanation: Closing at hour 4 is best because customers arrive every hour.
    ///
    /// Constraints:
    /// - 1 &lt;= customers.length &lt;= 10^5
    /// - customers contains only 'Y' and 'N'.
    /// </para>
    /// <para>
    /// 2483. 商店的最少罰分
    /// https://leetcode.cn/problems/minimum-penalty-for-a-shop/description/
    ///
    /// 給定只包含 'N'、'Y' 的 0 索引顧客紀錄字串 customers。索引 i 的字元為 'Y' 表示第 i 小時有顧客到來，'N' 表示無顧客。若商店在第 j 小時關門，其中 0 &lt;= j &lt;= n，則每個開門但無顧客的小時增加 1 分罰分，每個關門但有顧客的小時也增加 1 分。回傳產生最小罰分的最早關門時間。第 j 小時關門表示商店在第 j 小時已關閉。
    ///
    /// 範例 1：
    /// 輸入：customers = "YYNY"
    /// 輸出：2
    /// 說明：第 0 小時關門的罰分為 1 + 1 + 0 + 1 = 3；第 1 小時為 0 + 1 + 0 + 1 = 2；第 2 小時為 0 + 0 + 0 + 1 = 1；第 3 小時為 0 + 0 + 1 + 1 = 2；第 4 小時為 0 + 0 + 1 + 0 = 1。第 2、4 小時的罰分最小，而 2 較早。
    ///
    /// 範例 2：
    /// 輸入：customers = "NNNNN"
    /// 輸出：0
    /// 說明：因為沒有顧客到來，所以在第 0 小時關門最佳。
    ///
    /// 範例 3：
    /// 輸入：customers = "YYYY"
    /// 輸出：4
    /// 說明：因為每小時都有顧客到來，所以在第 4 小時關門最佳。
    ///
    /// 限制條件：
    /// - 1 &lt;= customers.length &lt;= 10^5
    /// - customers 僅包含 'Y' 與 'N'。
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();
        
        Console.WriteLine("=== LeetCode 2483: 商店的最少代價 ===\n");
        
        // 測試案例 1: "YYNY"
        string customers1 = "YYNY";
        int result1_method1 = solution.BestClosingTime(customers1);
        int result1_method2 = solution.BestClosingTime_OneTimes(customers1);
        Console.WriteLine($"測試案例 1: customers = \"{customers1}\"");
        Console.WriteLine($"方法一（枚舉法）: 最佳關門時間 = {result1_method1}");
        Console.WriteLine($"方法二（一次遍歷）: 最佳關門時間 = {result1_method2}");
        Console.WriteLine($"結果一致: {result1_method1 == result1_method2}\n");
        
        // 測試案例 2: "NNNNN"
        string customers2 = "NNNNN";
        int result2_method1 = solution.BestClosingTime(customers2);
        int result2_method2 = solution.BestClosingTime_OneTimes(customers2);
        Console.WriteLine($"測試案例 2: customers = \"{customers2}\"");
        Console.WriteLine($"方法一（枚舉法）: 最佳關門時間 = {result2_method1}");
        Console.WriteLine($"方法二（一次遍歷）: 最佳關門時間 = {result2_method2}");
        Console.WriteLine($"結果一致: {result2_method1 == result2_method2}\n");
        
        // 測試案例 3: "YYYY"
        string customers3 = "YYYY";
        int result3_method1 = solution.BestClosingTime(customers3);
        int result3_method2 = solution.BestClosingTime_OneTimes(customers3);
        Console.WriteLine($"測試案例 3: customers = \"{customers3}\"");
        Console.WriteLine($"方法一（枚舉法）: 最佳關門時間 = {result3_method1}");
        Console.WriteLine($"方法二（一次遍歷）: 最佳關門時間 = {result3_method2}");
        Console.WriteLine($"結果一致: {result3_method1 == result3_method2}\n");
        
        // 測試案例 4: "YN"
        string customers4 = "YN";
        int result4_method1 = solution.BestClosingTime(customers4);
        int result4_method2 = solution.BestClosingTime_OneTimes(customers4);
        Console.WriteLine($"測試案例 4: customers = \"{customers4}\"");
        Console.WriteLine($"方法一（枚舉法）: 最佳關門時間 = {result4_method1}");
        Console.WriteLine($"方法二（一次遍歷）: 最佳關門時間 = {result4_method2}");
        Console.WriteLine($"結果一致: {result4_method1 == result4_method2}\n");
        
        Console.WriteLine("=== 所有測試完成 ===");
    }

    /// <summary>
    /// 計算商店的最佳關門時間以達到最少代價
    /// 
    /// 解題思路：
    /// 使用枚舉法遍歷所有可能的關門時間點 i (0 ≤ i ≤ n)，計算每個時間點的代價：
    /// - pre：在關門前（0 ≤ j < i）沒有顧客的小時數（customers[j] == 'N'）
    /// - suf：在關門後（i ≤ j < n）有顧客的小時數（customers[j] == 'Y'）
    /// - 總代價 = pre + suf
    /// 
    /// 演算法最佳化：
    /// 不需要預先計算所有 'Y' 的數量，而是以第 0 小時關門的代價為基準，
    /// 在遍歷過程中動態調整 pre 和 suf 的值來計算相對代價。
    /// </summary>
    /// <param name="customers">顧客到店資訊字串，'Y' 表示有顧客，'N' 表示無顧客</param>
    /// <returns>能達到最小代價的最早關門時間</returns>
    public int BestClosingTime(string customers)
    {
        int n = customers.Length;
        int suf = 0;      // 後綴代價：關門後有顧客的罰分
        int pre = 0;      // 前綴代價：關門前無顧客的罰分
        int minCost = 0;  // 目前找到的最小代價
        int res = 0;      // 最佳關門時間

        // 枚舉所有可能的關門時間 i (0 ≤ i ≤ n)
        for(int i = 0; i <= n; i++)
        {
            // 如果目前代價比最小代價還小，更新最小代價和結果
            if(minCost > suf + pre)
            {
                minCost = suf + pre;
                res = i;
            }
            
            // 更新下一輪的代價計算
            if(i < n && customers[i] == 'N')
            {
                // 如果第 i 小時沒有顧客，延後關門會增加前綴代價
                pre++;
            }
            else if(i < n)
            {
                // 如果第 i 小時有顧客，延後關門會減少後綴代價
                suf--;
            }
        }
        return res;
    }

    /// <summary>
    /// 計算商店的最佳關門時間以達到最少代價（最佳化：一次遍歷）
    /// 
    /// 解題思路：
    /// 使用一次遍歷的最佳化方法，將問題轉化為尋找最小前綴和的問題。
    /// 核心觀念：
    /// - 以第 0 小時關門的代價為基準（penalty = 0）
    /// - 每延後一小時關門：
    ///   * 遇到 'N'：代價增加 1（多開一小時但無顧客）
    ///   * 遇到 'Y'：代價減少 1（避免關門時有顧客的罰分）
    /// - 追蹤過程中的最小代價及對應的關門時間
    /// 
    /// 演算法複雜度：
    /// - 時間複雜度：O(n) - 僅需一次遍歷
    /// - 空間複雜度：O(1) - 僅使用常數額外空間
    /// </summary>
    /// <param name="customers">顧客到店資訊字串，'Y' 表示有顧客，'N' 表示無顧客</param>
    /// <returns>能達到最小代價的最早關門時間</returns>
    public int BestClosingTime_OneTimes(string customers)
    {
        int penalty = 0;      // 目前相對於第 0 小時關門的代價差異
        int minPenalty = 0;   // 遇到的最小代價差異
        int res = 0;          // 最佳關門時間
        
        // 遍歷每個小時，動態計算延後關門的代價變化
        for(int i = 0; i < customers.Length; i++)
        {
            // 更新代價：'N' 增加罰分，'Y' 減少罰分
            penalty += customers[i] == 'N' ? 1 : -1;
            
            // 如果發現更小的代價，更新最佳解
            if(penalty < minPenalty)
            {
                minPenalty = penalty;
                res = i + 1;  // 關門時間為下一小時（i+1）
            }
        }
        return res;
    }
}
