namespace leetcode_1833;

class Program
{
    /// <summary>
    /// 1833. Maximum Ice Cream Bars
    /// https://leetcode.com/problems/maximum-ice-cream-bars/description/
    /// 1833. 雪糕的最大數量（繁體中文）
    /// https://leetcode.cn/problems/maximum-ice-cream-bars/description/
    /// 
    /// 題目描述：
    /// 在炎熱的夏日，一位男孩想買一些雪糕棒。
    /// 店裡有 n 支雪糕棒。給定一個長度為 n 的陣列 costs，其中 costs[i] 是第 i 支雪糕棒的價格（以硬幣計）。
    /// 男孩起始有 coins 枚硬幣可以花，他想盡可能多買雪糕棒。
    /// 注意：男孩可以按任意順序購買雪糕棒。
    /// 回傳男孩使用 coins 枚硬幣最多能買到的雪糕棒數量。
    /// 
    /// 要求：請用計數排序（counting sort）來解題。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("=== LeetCode 1833. Maximum Ice Cream Bars ===\n");

        // 測試案例 1：標準情況
        // costs = [1, 3, 2, 4, 1], coins = 7
        // 排序後：[1, 1, 2, 3, 4]
        // 購買順序：1 + 1 + 2 + 3 = 7，共 4 支
        int[] costs1 = [1, 3, 2, 4, 1];
        int coins1 = 7;
        var solution = new Program();
        int result1 = solution.MaxIceCream(costs1, coins1);
        Console.WriteLine($"測試案例 1: costs = [1, 3, 2, 4, 1], coins = {coins1}");
        Console.WriteLine($"結果: {result1} (預期: 4)\n");

        // 測試案例 2：硬幣不足以購買任何雪糕
        // costs = [10, 6, 8, 7, 7, 8], coins = 5
        // 最便宜的是 6，但硬幣只有 5，無法購買
        int[] costs2 = [10, 6, 8, 7, 7, 8];
        int coins2 = 5;
        int result2 = solution.MaxIceCream(costs2, coins2);
        Console.WriteLine($"測試案例 2: costs = [10, 6, 8, 7, 7, 8], coins = {coins2}");
        Console.WriteLine($"結果: {result2} (預期: 0)\n");

        // 測試案例 3：硬幣足夠買所有雪糕
        // costs = [1, 6, 3, 1, 2, 5], coins = 20
        // 總價 = 1 + 6 + 3 + 1 + 2 + 5 = 18 <= 20，可全部購買
        int[] costs3 = [1, 6, 3, 1, 2, 5];
        int coins3 = 20;
        int result3 = solution.MaxIceCream(costs3, coins3);
        Console.WriteLine($"測試案例 3: costs = [1, 6, 3, 1, 2, 5], coins = {coins3}");
        Console.WriteLine($"結果: {result3} (預期: 6)\n");

        // 測試案例 4：邊界情況 - 單一元素
        int[] costs4 = [5];
        int coins4 = 5;
        int result4 = solution.MaxIceCream(costs4, coins4);
        Console.WriteLine($"測試案例 4: costs = [5], coins = {coins4}");
        Console.WriteLine($"結果: {result4} (預期: 1)\n");

        // 測試案例 5：所有雪糕價格相同
        int[] costs5 = [2, 2, 2, 2, 2];
        int coins5 = 7;
        int result5 = solution.MaxIceCream(costs5, coins5);
        Console.WriteLine($"測試案例 5: costs = [2, 2, 2, 2, 2], coins = {coins5}");
        Console.WriteLine($"結果: {result5} (預期: 3)\n");

        Console.WriteLine("=== 測試完成 ===");
    }

    /// <summary>
    /// 方法一：排序 + 貪心演算法
    /// 
    /// <para><b>解題思路：</b></para>
    /// <para>
    /// 要最大化購買雪糕的數量，應優先購買價格最低的雪糕。
    /// 這是一個典型的貪心問題：每次都選擇當前最優的選擇（最便宜的雪糕），
    /// 最終能得到全域最優解（最多的雪糕數量）。
    /// </para>
    /// 
    /// <para><b>演算法步驟：</b></para>
    /// <list type="number">
    ///   <item><description>對價格陣列進行升序排序</description></item>
    ///   <item><description>從最便宜的雪糕開始遍歷</description></item>
    ///   <item><description>若剩餘硬幣足夠購買當前雪糕，則購買並扣除硬幣</description></item>
    ///   <item><description>若硬幣不足，則停止購買（後面的更貴，也買不起）</description></item>
    /// </list>
    /// 
    /// <para><b>時間複雜度：</b>O(n log n)，排序所需時間</para>
    /// <para><b>空間複雜度：</b>O(log n)，排序所需的堆疊空間</para>
    /// </summary>
    /// <param name="costs">雪糕價格陣列，costs[i] 表示第 i 支雪糕的價格</param>
    /// <param name="coins">可用的硬幣數量</param>
    /// <returns>最多能購買的雪糕數量</returns>
    /// <example>
    /// <code>
    /// // 範例：costs = [1, 3, 2, 4, 1], coins = 7
    /// // 排序後：[1, 1, 2, 3, 4]
    /// // 購買過程：
    /// //   - 買第 1 支 (價格 1)，剩餘 6 枚硬幣
    /// //   - 買第 2 支 (價格 1)，剩餘 5 枚硬幣
    /// //   - 買第 3 支 (價格 2)，剩餘 3 枚硬幣
    /// //   - 買第 4 支 (價格 3)，剩餘 0 枚硬幣
    /// //   - 無法買第 5 支 (價格 4 > 0)
    /// // 結果：4 支雪糕
    /// var solution = new Program();
    /// int result = solution.MaxIceCream(new int[] {1, 3, 2, 4, 1}, 7);
    /// // result = 4
    /// </code>
    /// </example>
    public int MaxIceCream(int[] costs, int coins)
    {
        // 步驟 1：升序排序，讓最便宜的雪糕排在前面
        Array.Sort(costs);

        int count = 0;  // 已購買的雪糕數量
        int n = costs.Length;

        // 步驟 2：從最便宜的雪糕開始遍歷
        for (int i = 0; i < n; i++)
        {
            int cost = costs[i];  // 當前雪糕的價格

            // 步驟 3：檢查是否有足夠的硬幣購買
            if (coins >= cost)
            {
                // 購買這支雪糕：扣除硬幣，增加計數
                coins -= cost;
                count++;
            }
            else
            {
                // 步驟 4：硬幣不足，後續雪糕更貴，直接結束
                break;
            }
        }

        return count;
    }
}
