namespace leetcode_3946;

class Program
{
    /// <summary>
    /// 3946. Maximum Number of Items From Sale I
    /// https://leetcode.com/problems/maximum-number-of-items-from-sale-i/description/
    /// 3946. 购买最多物品数目 I
    /// https://leetcode.cn/problems/maximum-number-of-items-from-sale-i/description/
    ///
    /// English
    /// You are given a 2D integer array items, where items[i] = [factori, pricei] represents the ith item. You are also given an integer budget.
    /// There are unlimited copies of each item available for purchase.You may buy any number of copies of any items such that the total cost of the purchased copies is at most budget.
    /// After buying items, you may receive free copies according to the following rules:
    /// 
    /// For each item i that you bought at least one copy of, you receive one free copy of every item j such that j != i and factori divides factorj.
    /// Buying multiple copies of the same item i does not give additional free copies through item i.
    /// The same item j can be received multiple times for free if it is received from purchases of different item types.
    /// 
    /// Return the maximum total number of item copies you can obtain, including both purchased copies and free copies, while spending at most budget on purchased items.
    ///
    /// 繁體中文
    /// 給定一個二維整數陣列 items，其中 items[i] = [factorᵢ, priceᵢ] 代表第 i 種物品的因數與單價。另給定一個整數 budget。
    /// 每種物品都有無限件可供購買。你可以購買任意種類及數量的物品，購買總金額不得超過 budget。
    /// 購買後，你可以依下列規則獲得免費物品。
    /// 
    /// 對於每種你至少購買一件的物品 i，只要 j != i 且 factorᵢ 能整除 factorⱼ，你就會免費獲得一件物品 j。
    /// 購買多件同一種物品 i，不會透過該物品額外獲得免費物品。
    /// 如果購買不同種類的物品都能贈送物品 j，則同一種物品 j 可以免費獲得多件。
    /// 
    /// 回傳在購買總金額不超過 budget 的情況下，可以獲得的最大物品總件數，包含購買及免費獲得的物品。
    /// </summary>
    /// <param name="args"></param>
    /// <remarks>
    /// 執行內建案例，使用同樣的物品資料與預算比對兩種解法。
    /// 命令列參數不影響測試。全部通過時結束碼為 0，否則為 1。
    /// </remarks>
    static int Main(string[] args)
    {
        int[][] maximumItemCount = Enumerable.Repeat<int[]>([1500, 1], 1000).ToArray();
        (string Name, int[][] Items, int Budget, int Expected)[] cases =
        [
            ("官方範例 1", [[6, 2], [2, 6], [3, 4]], 9, 4),
            ("官方範例 2", [[2, 4], [3, 2], [4, 1], [6, 4], [12, 4]], 8, 10),
            ("預算買不起任何物品", [[2, 4], [4, 5]], 3, 0),
            ("剛好買一件", [[5, 3]], 3, 1),
            ("同因數的不同種類互送", [[3, 2], [3, 2]], 4, 4),
            ("重複購買只觸發一次贈品", [[2, 2], [4, 9]], 6, 4),
            ("免費取得的物品不再觸發贈品", [[2, 2], [4, 9], [12, 9]], 2, 3),
            ("最大因數、單價與預算", [[1500, 1], [1500, 1500], [1, 1500]], 1500, 1501),
            ("最大種類數與大量重複因數", maximumItemCount, 1500, 1_000_500)
        ];

        Program solver = new Program();
        int passed = 0;
        foreach ((string name, int[][] items, int budget, int expected) in cases)
        {
            int pairwiseResult = solver.MaximumSaleItems(items, budget);
            int factorCountsResult = solver.MaximumSaleItemsWithFactorCounts(items, budget);
            bool isMatch = pairwiseResult == expected && factorCountsResult == expected;
            if (isMatch)
            {
                passed++;
            }

            string status = isMatch ? "PASS" : "FAIL";
            Console.WriteLine(
                $"[{status}] {name} | 預期 {expected} | 逐項整除 {pairwiseResult} | 倍數頻率 {factorCountsResult}");
        }

        Console.WriteLine($"通過 {passed}/{cases.Length} 個案例。");
        return passed == cases.Length ? 0 : 1;
    }

    /// <summary>
    /// 逐項檢查因數整除關係，求出每種物品首次購買的收益。
    /// 用 0-1 背包選擇要首次購買的種類，再用最低單價計算可追加的付費件數。
    /// </summary>
    /// <param name="items">1 至 1000 種物品，每列為 [因數, 單價]，兩個數值皆介於 1 至 1500。</param>
    /// <param name="budget">介於 1 至 1500 的購買預算。</param>
    /// <returns>預算內可取得的最大總件數，包含付費件數和贈品件數。</returns>
    public int MaximumSaleItems(int[][] items, int budget)
    {
        int[] dp = new int[budget + 1];
        int minPrice = int.MaxValue;

        foreach (int[] item in items)
        {
            int factor = item[0];
            int price = item[1];
            minPrice = Math.Min(minPrice, price);

            // 自身也符合整除條件，在收益中代表付費買到的第一件。
            int firstPurchaseCount = 0;
            foreach (int[] other in items)
            {
                if (other[0] % factor == 0)
                {
                    firstPurchaseCount++;
                }
            }

            // 倒序更新，讓每種物品的首次收益最多計入一次。
            for (int limit = budget; limit >= price; limit--)
            {
                dp[limit] = Math.Max(dp[limit], dp[limit - price] + firstPurchaseCount);
            }
        }

        int maxCount = 0;
        for (int limit = 0; limit <= budget; limit++)
        {
            int additionalCount = (budget - limit) / minPrice;
            maxCount = Math.Max(maxCount, dp[limit] + additionalCount);
        }

        return maxCount;
    }

    /// <summary>
    /// 統計各因數出現的次數，加總倍數頻率以共用相同因數的首次購買收益。
    /// 每個物品索引仍各自參與 0-1 背包，最後用最低單價計算可追加的付費件數。
    /// </summary>
    /// <param name="items">1 至 1000 種物品，每列為 [因數, 單價]，兩個數值皆介於 1 至 1500。</param>
    /// <param name="budget">介於 1 至 1500 的購買預算。</param>
    /// <returns>預算內可取得的最大總件數，包含付費件數和贈品件數。</returns>
    public int MaximumSaleItemsWithFactorCounts(int[][] items, int budget)
    {
        int maxFactor = 0;
        int minPrice = int.MaxValue;
        foreach (int[] item in items)
        {
            maxFactor = Math.Max(maxFactor, item[0]);
            minPrice = Math.Min(minPrice, item[1]);
        }

        int[] frequency = new int[maxFactor + 1];
        foreach (int[] item in items)
        {
            frequency[item[0]]++;
        }

        // 從因數自身開始走訪倍數，計入付費首件及所有符合資格的贈品。
        int[] firstPurchaseCounts = new int[maxFactor + 1];
        for (int factor = 1; factor <= maxFactor; factor++)
        {
            if (frequency[factor] == 0)
            {
                continue;
            }

            for (int multiple = factor; multiple <= maxFactor; multiple += factor)
            {
                firstPurchaseCounts[factor] += frequency[multiple];
            }
        }

        int[] dp = new int[budget + 1];
        foreach (int[] item in items)
        {
            AddFirstPurchase(dp, item[1], firstPurchaseCounts[item[0]]);
        }

        int maxCount = 0;
        for (int limit = 0; limit <= budget; limit++)
        {
            // 追加購買每件至少增加一件；全買最低單價的物品可使追加的付費件數最多。
            int additionalCount = (budget - limit) / minPrice;
            maxCount = Math.Max(maxCount, dp[limit] + additionalCount);
        }

        return maxCount;
    }

    /// <summary>
    /// 將一種物品的首次購買加入 0-1 背包，就每個預算上限更新最大件數。
    /// </summary>
    /// <param name="dp">既有背包狀態。索引是預算上限，元素會就地更新。</param>
    /// <param name="price">此物品的正整數單價。</param>
    /// <param name="firstPurchaseCount">首次購買取得的總件數，包含付費首件及贈品。</param>
    private static void AddFirstPurchase(int[] dp, int price, int firstPurchaseCount)
    {
        // 倒序保留上一輪的 dp[limit - price]，使同一種類的首次購買收益只加入一次。
        for (int limit = dp.Length - 1; limit >= price; limit--)
        {
            dp[limit] = Math.Max(dp[limit], dp[limit - price] + firstPurchaseCount);
        }
    }
}
