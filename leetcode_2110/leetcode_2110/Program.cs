namespace leetcode_2110;

class Program
{
    /// <summary>
    /// 2110. Number of Smooth Descent Periods of a Stock
    /// https://leetcode.com/problems/number-of-smooth-descent-periods-of-a-stock/description/?envType=daily-question&envId=2025-12-15
    /// 2110. 股票平滑下跌階段的數目
    /// https://leetcode.cn/problems/number-of-smooth-descent-periods-of-a-stock/description/?envType=daily-question&envId=2025-12-15
    ///
    /// 繁體中文題目描述：給定一個整數陣列 prices，表示股票每日的價格歷史，其中 prices[i] 為第 i 天的價格。
    /// 一個平滑下跌階段由一個或多個連續天數組成，且從第二天起每一天的價格都比前一天低恰好 1（期間的第一天不受此規則限制）。
    /// 回傳所有平滑下跌階段的總數。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var program = new Program();
        
        // 測試案例 1: [3,2,1,4]
        // 預期輸出: 7
        // 說明: 平滑下跌階段有 [3], [2], [1], [4], [3,2], [2,1], [3,2,1] 共 7 個
        int[] prices1 = [3, 2, 1, 4];
        long result1 = program.GetDescentPeriods(prices1);
        Console.WriteLine($"測試案例 1: [{string.Join(", ", prices1)}]");
        Console.WriteLine($"結果: {result1}");
        Console.WriteLine($"預期: 7\n");
        
        // 測試案例 2: [8,6,7,7]
        // 預期輸出: 4
        // 說明: 平滑下跌階段有 [8], [6], [7], [7] 共 4 個
        int[] prices2 = [8, 6, 7, 7];
        long result2 = program.GetDescentPeriods(prices2);
        Console.WriteLine($"測試案例 2: [{string.Join(", ", prices2)}]");
        Console.WriteLine($"結果: {result2}");
        Console.WriteLine($"預期: 4\n");
        
        // 測試案例 3: [1]
        // 預期輸出: 1
        // 說明: 平滑下跌階段只有 [1] 共 1 個
        int[] prices3 = [1];
        long result3 = program.GetDescentPeriods(prices3);
        Console.WriteLine($"測試案例 3: [{string.Join(", ", prices3)}]");
        Console.WriteLine($"結果: {result3}");
        Console.WriteLine($"預期: 1");
    }

    /// <summary>
    /// 計算股票價格陣列中所有平滑下跌階段的總數
    /// 
    /// 解題思路：
    /// 使用動態規劃的方法，統計以每一天為結尾的平滑下跌階段數目。
    /// 
    /// 狀態定義：
    /// dp[i] 表示以第 i 天為結尾的平滑下跌階段數目
    /// 
    /// 狀態轉移方程：
    /// - 當 i = 0 時，dp[0] = 1（第一天本身）
    /// - 當 i > 0 時：
    ///   - 若 prices[i] = prices[i-1] - 1，則 dp[i] = dp[i-1] + 1（可以與前一天組成連續下跌）
    ///   - 若 prices[i] ≠ prices[i-1] - 1，則 dp[i] = 1（只能是當天本身）
    /// 
    /// 時間複雜度：O(n)，其中 n 為陣列長度
    /// 空間複雜度：O(1)，只使用常數空間（優化後不需要完整的 dp 陣列）
    /// </summary>
    /// <param name="prices">股票每日價格陣列</param>
    /// <returns>所有平滑下跌階段的總數</returns>
    public long GetDescentPeriods(int[] prices)
    {
        int n = prices.Length;
        
        // res: 平滑下跌階段的總數，初值為 dp[0] = 1
        long res = 1;
        
        // prev: 以上一個元素為結尾的平滑下跌階段數目，初值為 dp[0] = 1
        // 用於空間優化，避免維護完整的 dp 陣列
        int prev = 1;
        
        // 從第 2 天開始遍歷，根據遞推式更新 prev 以及總數 res
        for (int i = 1; i < n; i++)
        {
            // 判斷當前天價格是否比前一天恰好少 1
            if (prices[i] == prices[i - 1] - 1)
            {
                // 可以與前一天組成連續下跌，階段數累加
                // dp[i] = dp[i-1] + 1
                prev++;
            }
            else
            {
                // 無法與前一天組成連續下跌，重新開始計數
                // dp[i] = 1
                prev = 1;
            }
            
            // 累加以當前天為結尾的平滑下跌階段數目
            res += prev;
        }
        
        return res;
    }
}
