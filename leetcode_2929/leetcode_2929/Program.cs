namespace leetcode_2929;

class Program
{
    /// <summary>
    /// 2929. Distribute Candies Among Children II
    /// https://leetcode.com/problems/distribute-candies-among-children-ii/description/?envType=daily-question&envId=2025-06-01
    /// 
    /// 2929. 給小朋友們分糖果 II
    /// https://leetcode.cn/problems/distribute-candies-among-children-ii/description/?envType=daily-question&envId=2025-06-01
    /// 
    /// 給定兩個正整數 n 和 limit。
    /// 請回傳將 n 顆糖果分給 3 個小朋友，且每個小朋友最多只能拿 limit 顆糖果的所有分配方式總數。
    /// 
    /// 解題說明：
    /// 本題屬於組合數學問題。枚舉第一個小朋友 a 可以拿的糖果數（0~min (n, limit)），剩下的糖果分給 b、c 兩人，
    /// 並且每人最多只能拿 limit 顆。對於每個 a，b 的範圍是 max (0, remaining-limit) 到 min (limit, remaining)，
    /// 每個合法 b 都對應唯一一個合法 c。將所有情況累加即為答案。
    /// 
    /// 時間複雜度：O (min (n, limit))，只需枚舉 a 的所有可能值。
    /// 空間複雜度：O (1)，只用到常數記憶體。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        var prog = new Program();
        Console.WriteLine($"n=5, limit=2 => {prog.DistributeCandies(5, 2)}"); // 預期: 6
        Console.WriteLine($"n=10, limit=5 => {prog.DistributeCandies(10, 5)}"); // 預期: 21
        Console.WriteLine($"n=0, limit=0 => {prog.DistributeCandies(0, 0)}"); // 預期: 1
        Console.WriteLine($"n=7, limit=3 => {prog.DistributeCandies(7, 3)}"); // 預期: 8
    }


    /// <summary>
    /// 整數分割加上限制條件的組合問題
    /// </summary>
    /// <param name="n"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public long DistributeCandies(int n, int limit)
    {
        long count = 0;

        // 枚舉 a（第一個人拿的糖果數）
        for (int a = 0; a <= Math.Min(n, limit); a++)
        {
            int remaining = n - a; // 剩下的糖果數，給 b 和 c 分

            // b 和 c 必須都在 0 到 limit 之間，且 b + c = remaining
            // 那麼 b 的範圍是 max(0, remaining - limit) 到 min(limit, remaining)
            int minB = Math.Max(0, remaining - limit);
            int maxB = Math.Min(limit, remaining);

            // 有效的 b 的數量 = maxB - minB + 1（對應每一個 b，都有一個對應的 c）
            long ways = Math.Max(0, maxB - minB + 1);

            count += ways;
        }

        return count;
    }
}
