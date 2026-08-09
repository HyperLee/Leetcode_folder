namespace leetcode_2561;

class Program
{
    /// <summary>
    /// 2561. Rearranging Fruits
    /// https://leetcode.com/problems/rearranging-fruits/description/
    /// <para>
    /// You have two fruit baskets containing n fruits each. You are given two 0-indexed integer arrays basket1 and basket2 representing the cost of fruit in each basket. You want to make both baskets equal. To do so, you can use the following operation as many times as you want:
    ///
    /// - Choose two indices i and j, and swap the i-th fruit of basket1 with the j-th fruit of basket2.
    /// - The cost of the swap is min(basket1[i], basket2[j]).
    ///
    /// Two baskets are considered equal if sorting them according to the fruit cost makes them exactly the same baskets.
    ///
    /// Return the minimum cost to make both baskets equal, or -1 if it is impossible.
    ///
    /// Example 1:
    /// Input: basket1 = [4,2,2,2], basket2 = [1,4,1,2]
    /// Output: 1
    /// Explanation: Swap index 1 of basket1 with index 0 of basket2, which has cost 1. Now basket1 = [4,1,2,2] and basket2 = [2,4,1,2]. Rearranging both arrays makes them equal.
    ///
    /// Example 2:
    /// Input: basket1 = [2,3,4,1], basket2 = [3,2,5,1]
    /// Output: -1
    /// Explanation: It can be shown that it is impossible to make both baskets equal.
    ///
    /// Constraints:
    /// - basket1.length == basket2.length
    /// - 1 &lt;= basket1.length &lt;= 10^5
    /// - 1 &lt;= basket1[i], basket2[i] &lt;= 10^9
    /// </para>
    /// <para>
    /// 2561. 重新排列水果
    /// https://leetcode.cn/problems/rearranging-fruits/description/
    ///
    /// 你有兩個水果籃，每個籃子各裝有 n 個水果。給定兩個 0-indexed 整數陣列 basket1 和 basket2，分別表示每個籃子中水果的成本。你想讓兩個籃子相等。為此，你可以任意次執行下列操作：
    ///
    /// - 選擇兩個索引 i 和 j，交換 basket1 的第 i 個水果與 basket2 的第 j 個水果。
    /// - 交換成本為 min(basket1[i], basket2[j])。
    ///
    /// 若依水果成本排序後，兩個籃子的內容完全相同，就視為兩個籃子相等。
    ///
    /// 回傳使兩個籃子相等的最小成本；若無法做到，則回傳 -1。
    ///
    /// 範例 1：
    /// 輸入：basket1 = [4,2,2,2], basket2 = [1,4,1,2]
    /// 輸出：1
    /// 解釋：交換 basket1 中索引 1 的水果與 basket2 中索引 0 的水果，成本為 1。此時 basket1 = [4,1,2,2]，basket2 = [2,4,1,2]。重新排列兩個陣列後，它們會相等。
    ///
    /// 範例 2：
    /// 輸入：basket1 = [2,3,4,1], basket2 = [3,2,5,1]
    /// 輸出：-1
    /// 解釋：可以證明無法使兩個籃子相等。
    ///
    /// 限制條件：
    /// - basket1.length == basket2.length
    /// - 1 &lt;= basket1.length &lt;= 10^5
    /// - 1 &lt;= basket1[i], basket2[i] &lt;= 10^9
    /// </para>
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 範例測試資料
        int[] basket1 = { 4, 2, 2, 2 };
        int[] basket2 = { 1, 4, 1, 2 };
        var program = new Program();
        long result = program.MinCost(basket1, basket2);
        Console.WriteLine($"最小交換成本: {result}"); // 預期輸出: 1

        // 另一組無法重排的測試資料
        int[] basket3 = { 1, 2, 3 };
        int[] basket4 = { 2, 3, 4 };
        long result2 = program.MinCost(basket3, basket4);
        Console.WriteLine($"最小交換成本: {result2}"); // 預期輸出: -1
    }


    /// <summary>
    /// 計算將兩個水果籃內容重排為相同所需的最小成本。
    /// 解題說明：
    /// 1. 統計 basket1、basket2 內所有數字的總出現次數，若有任何數字出現次數為奇數，則無法重排為相同，直接回傳 -1。
    /// 2. 計算每個數字在 basket1、basket2 中的多餘部分，分別記錄需要換出去與換進來的數字。
    /// 3. 將多餘的數字排序，配對交換。每次交換成本為 min(要換出去的數字, 要換進來的數字, 2*全局最小值)。
    ///    若直接交換成本過高，會自動考慮用全局最小值作為中介進行「間接交換」，以降低總成本。
    ///    間接交換即：先將一個水果換成全局最小值，再用最小值與另一個水果交換，總成本為 2*全局最小值。
    /// 4. 累加所有交換的最小成本即為答案。
    /// </summary>
    /// <param name="basket1">第一個水果籃的成本陣列</param>
    /// <param name="basket2">第二個水果籃的成本陣列</param>
    /// <returns>最小交換成本，若無法重排則回傳 -1</returns>
    public long MinCost(int[] basket1, int[] basket2)
    {
        // 統計每個數字的總出現次數，並直接產生 extra1/extra2
        var count = new Dictionary<int, int>();
        foreach (var num in basket1)
        {
            count.TryGetValue(num, out var val);
            count[num] = val + 1;
        }
        foreach (var num in basket2)
        {
            count.TryGetValue(num, out var val);
            count[num] = val - 1;
        }

        var extra1 = new List<int>(); // basket1 多出來要換出去的
        var extra2 = new List<int>(); // basket2 多出來要換進來的
        foreach (var kv in count)
        {
            // 若有任何數字出現次數為奇數（非 0），無法重排
            if (kv.Value % 2 != 0)
            {
                return -1;
            }
            // value > 0: basket1 多出來的
            // value < 0: basket2 多出來的
            int half = Math.Abs(kv.Value) / 2;
            if (kv.Value > 0)
            {
                for (int i = 0; i < half; i++) extra1.Add(kv.Key);
            }
            else if (kv.Value < 0)
            {
                for (int i = 0; i < half; i++) extra2.Add(kv.Key);
            }
        }

        // 將多餘的數字排序，方便配對交換
        extra1.Sort();
        extra2.Sort();
        extra2.Reverse(); // 由大到小，與 extra1 配對

        int minVal = count.Keys.Min(); // 全局最小值，用於間接交換
        long cost = 0;
        for (int i = 0; i < extra1.Count; i++)
        {
            int a = extra1[i];
            int b = extra2[i];
            // 直接交換與間接交換取最小成本
            cost += Math.Min(Math.Min(a, b), 2 * minVal);
        }
        return cost;
    }
}
