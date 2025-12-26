namespace _2483;

class Program
{
    /// <summary>
    /// 2483. Minimum Penalty for a Shop
    /// https://leetcode.com/problems/minimum-penalty-for-a-shop/description/?envType=daily-question&envId=2025-12-26
    /// 2483. 商店的最少代價（簡體中文）
    /// https://leetcode.cn/problems/minimum-penalty-for-a-shop/description/?envType=daily-question&envId=2025-12-26
    /// 
    /// 繁體中文翻譯：
    /// 給定一個由 'N' 和 'Y' 組成的 0 索引字串 `customers`（每小時是否有顧客到來），
    /// 若第 i 個字元為 'Y' 則表示第 i 小時有顧客，'N' 則表示沒有顧客。
    /// 若商店在第 j 小時關門（0 <= j <= n），罰分計算如下：
    /// - 商店開門但該小時沒有顧客：罰分 +1
    /// - 商店關門但該小時有顧客：罰分 +1
    /// 回傳能使罰分最小的最早關店時間 j。
    /// 注意：若在第 j 小時關門，表示第 j 小時商店已關閉。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();
        
        // 測試案例 1: "YYNY"
        string customers1 = "YYNY";
        int result1 = solution.BestClosingTime(customers1);
        Console.WriteLine($"測試案例 1: customers = \"{customers1}\", 最佳關門時間 = {result1}");
        
        // 測試案例 2: "NNNNN"
        string customers2 = "NNNNN";
        int result2 = solution.BestClosingTime(customers2);
        Console.WriteLine($"測試案例 2: customers = \"{customers2}\", 最佳關門時間 = {result2}");
        
        // 測試案例 3: "YYYY"
        string customers3 = "YYYY";
        int result3 = solution.BestClosingTime(customers3);
        Console.WriteLine($"測試案例 3: customers = \"{customers3}\", 最佳關門時間 = {result3}");
        
        // 測試案例 4: "YN"
        string customers4 = "YN";
        int result4 = solution.BestClosingTime(customers4);
        Console.WriteLine($"測試案例 4: customers = \"{customers4}\", 最佳關門時間 = {result4}");
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
}
