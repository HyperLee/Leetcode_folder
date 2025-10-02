namespace leetcode_3100;

class Program
{
    /// <summary>
    /// 3100. Water Bottles II
    /// https://leetcode.com/problems/water-bottles-ii/description/?envType=daily-question&envId=2025-10-02
    /// 3100. 换水问题 II
    /// https://leetcode.cn/problems/water-bottles-ii/description/?envType=daily-question&envId=2025-10-02
    /// 
    /// 給你兩個整數 numBottles 和 numExchange。
    /// numBottles 表示你最初擁有的滿水瓶數量。
    /// 在一次操作中，你可以執行以下操作之一：
    ///     喝任意數量的滿水瓶，將它們變成空瓶。
    ///     用 numExchange 個空瓶交換一個滿水瓶。然後，將 numExchange 增加一。
    /// 注意，你不能在同一個 numExchange 值下交換多批空瓶。例如，如果 numBottles == 3 且 numExchange == 1，你不能用 3 個空瓶交換 3 個滿瓶。
    /// 返回你可以喝的最大水瓶數量。
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料 (numBottles, numExchange)
        var tests = new (int numBottles, int numExchange)[]
        {
            (9, 3),    // 一般情況
            (3, 1),    // 特殊交換為 1 的情況
            (5, 5),    // 等量交換
            (10, 2),   // 小交換數
            (1, 2),    // 無法兌換
            (100, 1)   // 大量、numExchange = 1
        };

        var solver = new Program();
        foreach (var (numBottles, numExchange) in tests)
        {
            int drank = solver.MaxBottlesDrunk(numBottles, numExchange);
            Console.WriteLine($"Input: numBottles={numBottles}, numExchange={numExchange} => Max drank = {drank}");
        }
    }

    /// <summary>
    /// 計算最多可以喝到的水瓶數量。
    ///
    /// 解題說明：
    /// 1. 先將初始的所有滿瓶喝掉，這會產生等量的空瓶。
    /// 2. 根據題目限制：在同一個 <c>numExchange</c> 值下，最多只能進行一次空瓶兌換（換一瓶）；兌換後
    ///    會將 <c>numExchange</c> 增加 1。故此處直接模擬「每次以目前的 <c>numExchange</c> 兌換最多一瓶、喝掉並
    ///    更新空瓶與 <c>numExchange</c>」的流程，直到空瓶不足以以當前的 <c>numExchange</c> 再兌換為止。
    ///
    /// 範例：numBottles = 10, numExchange = 3
    /// - 初始喝 10 瓶，空瓶 = 10
    /// - 用 3 個空瓶換 1 瓶（只能換一次），喝後空瓶 = 10 - 3 + 1 = 8，numExchange -> 4，總喝 = 11
    /// - 用 4 個空瓶換 1 瓶，喝後空瓶 = 8 - 4 + 1 = 5，numExchange -> 5，總喝 = 12
    /// - 用 5 個空瓶換 1 瓶，喝後空瓶 = 5 - 5 + 1 = 1，numExchange -> 6，總喝 = 13
    /// - 空瓶 1 少於 6，停止。答案為 13。
    ///
    /// 時間複雜度：O(k)，k 為實際兌換次數；在最壞情況下 k = O(numBottles)。空間複雜度：O(1)。
    ///
    /// 注意：此方法假設輸入為合理的正整數（numBottles >= 0, numExchange >= 1）。若需嚴格輸入驗證，可在入口
    /// 加入參數檢查並拋出例外。
    /// </summary>
    /// <param name="numBottles">初始滿水瓶數量。</param>
    /// <param name="numExchange">當前需要的空瓶數以兌換一瓶（每次兌換後會自動 +1）。</param>
    /// <returns>能喝到的最大水瓶數量。</returns>
    public int MaxBottlesDrunk(int numBottles, int numExchange)
    {
        // 當前手上的空瓶數（開始時喝完初始滿瓶會形成空瓶）
        int empty = 0;
        // 總共喝到的瓶數
        int totalDrank = 0;

        // 先喝掉初始的滿瓶
        totalDrank += numBottles;
        empty += numBottles;

        // 當空瓶數夠換新的一瓶時，持續以「每次 numExchange 只兌換一次」的規則兌換並喝
        while (empty >= numExchange)
        {
            // 每個 numExchange 值只能兌換一次（一瓶）
            totalDrank += 1;
            // 使用 numExchange 個空瓶兌換一瓶，喝掉後會得到一個空瓶
            empty = empty - numExchange + 1;
            // 每次兌換後，numExchange 增加 1
            numExchange++;
        }

        return totalDrank;
    }
}
