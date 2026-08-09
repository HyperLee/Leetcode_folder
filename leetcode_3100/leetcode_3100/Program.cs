namespace leetcode_3100;

class Program
{
    /// <summary>
    /// 3100. Water Bottles II
    /// https://leetcode.com/problems/water-bottles-ii/description/
    /// <para>
    /// You are given two integers numBottles and numExchange.
    ///
    /// numBottles is the number of full water bottles you initially have. In one operation, you may do one of the following:
    /// - Drink any number of full bottles, turning them into empty bottles.
    /// - Exchange numExchange empty bottles for one full bottle, then increase numExchange by one.
    ///
    /// You cannot exchange multiple batches of empty bottles for the same numExchange value. For example, if numBottles == 3 and numExchange == 1, you cannot exchange 3 empty bottles for 3 full bottles.
    ///
    /// Return the maximum number of water bottles you can drink.
    ///
    /// Example 1:
    /// Image: https://assets.leetcode.com/uploads/2024/01/28/exampleone1.png
    /// Input: numBottles = 13, numExchange = 6
    /// Output: 15
    /// Explanation: The table above shows the full bottles, empty bottles, current numExchange value, and number of bottles drunk.
    ///
    /// Example 2:
    /// Image: https://assets.leetcode.com/uploads/2024/01/28/example231.png
    /// Input: numBottles = 10, numExchange = 3
    /// Output: 13
    /// Explanation: The table above shows the full bottles, empty bottles, current numExchange value, and number of bottles drunk.
    ///
    /// Constraints:
    /// - 1 &lt;= numBottles &lt;= 100
    /// - 1 &lt;= numExchange &lt;= 100
    /// </para>
    /// <para>
    /// 3100. 換水問題 II
    /// https://leetcode.cn/problems/water-bottles-ii/description/
    ///
    /// 給定兩個整數 numBottles 和 numExchange。
    ///
    /// numBottles 表示你起初擁有的滿水瓶數量。一次操作中，你可以執行下列其中一項：
    /// - 喝掉任意數量的滿水瓶，使它們變成空瓶。
    /// - 用 numExchange 個空瓶換一個滿水瓶，接著將 numExchange 增加一。
    ///
    /// 你不能在 numExchange 為相同值時交換多批空瓶。例如，若 numBottles == 3 且 numExchange == 1，不能用 3 個空瓶換 3 個滿水瓶。
    ///
    /// 回傳你最多可以喝掉的水瓶數量。
    ///
    /// 範例 1：
    /// 圖片：https://assets.leetcode.com/uploads/2024/01/28/exampleone1.png
    /// 輸入：numBottles = 13, numExchange = 6
    /// 輸出：15
    /// 解釋：上表顯示滿水瓶、空水瓶、目前的 numExchange 值，以及已喝水瓶數量。
    ///
    /// 範例 2：
    /// 圖片：https://assets.leetcode.com/uploads/2024/01/28/example231.png
    /// 輸入：numBottles = 10, numExchange = 3
    /// 輸出：13
    /// 解釋：上表顯示滿水瓶、空水瓶、目前的 numExchange 值，以及已喝水瓶數量。
    ///
    /// 限制條件：
    /// - 1 &lt;= numBottles &lt;= 100
    /// - 1 &lt;= numExchange &lt;= 100
    /// </para>
    /// </summary>
    /// <remarks>
    /// 使用固定案例比較逐次模擬與整數二分搜尋兩種解法；全部驗證通過時回傳 0，否則回傳 1。
    /// </remarks>
    /// <param name="args">命令列參數；本程式使用固定案例，不讀取外部輸入。</param>
    /// <returns>全部驗證通過時回傳 0，任一驗證失敗時回傳 1。</returns>
    static int Main(string[] args)
    {
        return RunSamples();
    }

    /// <summary>
    /// 建立符合題目限制的固定案例，執行兩種解法並統計通過的驗證數量。
    /// </summary>
    /// <returns>全部驗證通過時回傳 0，否則回傳 1。</returns>
    private static int RunSamples()
    {
        SampleCase[] samples =
        {
            new("官方範例一", 13, 6, 15),
            new("官方範例二", 10, 3, 13),
            new("最小值且立即兌換", 1, 1, 2),
            new("交換門檻從一開始", 3, 1, 5),
            new("空瓶不足無法兌換", 1, 2, 1),
            new("空瓶剛好足夠兌換", 5, 5, 6),
            new("一般情況", 9, 3, 11),
            new("較小交換門檻", 10, 2, 13),
            new("最大瓶數與最小門檻", 100, 1, 114),
            new("最大瓶數與最大門檻", 100, 100, 101)
        };

        int passedChecks = 0;
        foreach (SampleCase sample in samples)
        {
            passedChecks += RunCase(sample);
        }

        int totalChecks = samples.Length * 2;
        Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
        return passedChecks == totalChecks ? 0 : 1;
    }

    /// <summary>
    /// 執行單一案例，比較兩種解法的實際輸出與預期結果，並顯示 PASS 或 FAIL。
    /// </summary>
    /// <param name="sample">包含案例名稱、兩個輸入整數與預期最大飲用瓶數的測試資料。</param>
    /// <returns>本案例通過的解法數量，範圍為 0 到 2。</returns>
    private static int RunCase(SampleCase sample)
    {
        Program solver = new();
        Console.WriteLine($"案例：{sample.Name}");
        Console.WriteLine($"numBottles = {sample.NumBottles}, numExchange = {sample.NumExchange}");
        Console.WriteLine($"預期 = {sample.Expected}");

        (string Name, int Actual)[] results =
        {
            ("MaxBottlesDrunk", solver.MaxBottlesDrunk(sample.NumBottles, sample.NumExchange)),
            ("MaxBottlesDrunk2", solver.MaxBottlesDrunk2(sample.NumBottles, sample.NumExchange))
        };

        int passedChecks = 0;
        foreach ((string name, int actual) in results)
        {
            bool passed = actual == sample.Expected;
            if (passed)
            {
                passedChecks++;
            }

            Console.WriteLine($"{name,-18} 實際 = {actual} => {(passed ? "PASS" : "FAIL")}");
        }

        Console.WriteLine();
        return passedChecks;
    }

    /// <summary>
    /// 描述一筆固定案例的輸入條件與預期最大飲用瓶數。
    /// </summary>
    /// <param name="Name">案例名稱。</param>
    /// <param name="NumBottles">初始滿水瓶數量。</param>
    /// <param name="NumExchange">第一次兌換所需的空瓶數量。</param>
    /// <param name="Expected">最多可以喝到的水瓶數量。</param>
    private sealed record SampleCase(string Name, int NumBottles, int NumExchange, int Expected);

    /// <summary>
    /// 逐次模擬喝水與空瓶兌換，計算最多可以喝到的水瓶數量。
    /// 輸入為題目保證的正整數；每次兌換只取得一瓶水，喝完後留下空瓶並將下一次門檻加一。
    /// </summary>
    /// <param name="numBottles">初始滿水瓶數量，限制為 1 到 100。</param>
    /// <param name="numExchange">第一次兌換一瓶水所需的空瓶數量，限制為 1 到 100。</param>
    /// <returns>依照交換門檻逐次增加的規則，最多可以喝到的水瓶數量。</returns>
    /// <remarks>
    /// 初始滿瓶全部喝完後，滿瓶數同時是已喝總數與空瓶數。每次兌換再喝掉新水，空瓶淨減少
    /// <c>numExchange - 1</c>，直到空瓶少於目前門檻。若實際兌換 k 次，時間複雜度為 O(k)，
    /// 額外空間複雜度為 O(1)。
    /// </remarks>
    public int MaxBottlesDrunk(int numBottles, int numExchange)
    {
        int emptyBottles = numBottles;
        int totalDrank = numBottles;

        while (emptyBottles >= numExchange)
        {
            totalDrank++;

            // 兌換會消耗目前門檻數量的空瓶，但喝完換來的水後會補回一個空瓶。
            emptyBottles -= numExchange - 1;
            numExchange++;
        }

        return totalDrank;
    }

    /// <summary>
    /// 利用累積空瓶需求的單調性，以整數二分搜尋計算可完成的最大兌換次數。
    /// 輸入為題目保證的正整數，輸出為初始瓶數加上最多可兌換並喝掉的新水瓶數量。
    /// </summary>
    /// <param name="numBottles">初始滿水瓶數量，限制為 1 到 100。</param>
    /// <param name="numExchange">第一次兌換一瓶水所需的空瓶數量，限制為 1 到 100。</param>
    /// <returns>依照交換門檻逐次增加的規則，最多可以喝到的水瓶數量。</returns>
    /// <remarks>
    /// 完成 k 次兌換後，空瓶的累積淨消耗形成等差級數；若 k 大於 0，最少初始空瓶需求為
    /// <c>k * (2 * (numExchange - 1) + k - 1) / 2 + 1</c>。最後的 1 代表完成第 k 次兌換時，
    /// 喝完新水仍會留下的一個空瓶。需求會隨 k 單調增加，因此可二分搜尋最大可行值。
    /// 時間複雜度為 O(log numBottles)，額外空間複雜度為 O(1)。
    /// </remarks>
    public int MaxBottlesDrunk2(int numBottles, int numExchange)
    {
        int lower = 0;
        int upper = numBottles;

        while (lower < upper)
        {
            int exchanges = lower + (upper - lower + 1) / 2;
            long requiredEmptyBottles = (long)exchanges
                * (2L * (numExchange - 1) + exchanges - 1) / 2 + 1;

            // 可行解形成連續前綴；採用上中位數可在可行時安全推進下界。
            if (requiredEmptyBottles <= numBottles)
            {
                lower = exchanges;
            }
            else
            {
                upper = exchanges - 1;
            }
        }

        return numBottles + lower;
    }
}
