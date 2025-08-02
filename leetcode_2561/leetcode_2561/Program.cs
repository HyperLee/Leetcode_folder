namespace leetcode_2561;

class Program
{
    /// <summary>
    /// 2561. Rearranging Fruits
    /// https://leetcode.com/problems/rearranging-fruits/description/?envType=daily-question&envId=2025-08-02
    /// 2561. 重排水果
    /// https://leetcode.cn/problems/rearranging-fruits/description/?envType=daily-question&envId=2025-08-02
    /// 
    /// 題目描述：
    /// 你有兩個水果籃，每個籃子裡有 n 個水果。給定兩個 0-indexed 整數陣列 basket1 和 basket2，分別代表每個籃子中水果的成本。
    /// 你想讓兩個籃子完全相同。為此，你可以進行任意次以下操作：
    /// 選擇兩個索引 i 和 j，將 basket1 的第 i 個水果與 basket2 的第 j 個水果交換。
    /// 交換的成本為 min(basket1[i], basket2[j])。
    /// 當兩個籃子排序後完全相同時，視為兩個籃子相等。
    /// 請返回使兩個籃子相等的最小成本，如果無法做到則返回 -1。
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
    ///    若直接交換成本過高，可考慮用全局最小值作為中介進行間接交換。
    /// 4. 累加所有交換的最小成本即為答案。
    /// </summary>
    /// <param name="basket1">第一個水果籃的成本陣列</param>
    /// <param name="basket2">第二個水果籃的成本陣列</param>
    /// <returns>最小交換成本，若無法重排則回傳 -1</returns>
    public long MinCost(int[] basket1, int[] basket2)
    {
        // 統計每個數字的總出現次數
        var count = new Dictionary<int, int>();
        foreach (var num in basket1)
        {
            count.TryGetValue(num, out var val);
            count[num] = val + 1;
        }
        foreach (var num in basket2)
        {
            count.TryGetValue(num, out var val);
            count[num] = val + 1;
        }

        // 若有任何數字出現次數為奇數，無法重排
        foreach (var kv in count)
        {
            if (kv.Value % 2 != 0)
            {
                return -1;
            }
        }

        // 計算 basket1、basket2 多出來的部分
        var extra1 = new List<int>(); // basket1 多出來要換出去的
        var extra2 = new List<int>(); // basket2 多出來要換進來的
        var freq1 = new Dictionary<int, int>();
        var freq2 = new Dictionary<int, int>();
        foreach (var num in basket1)
        {
            freq1.TryGetValue(num, out var val);
            freq1[num] = val + 1;
        }
        foreach (var num in basket2)
        {
            freq2.TryGetValue(num, out var val);
            freq2[num] = val + 1;
        }
        foreach (var kv in count)
        {
            int num = kv.Key;
            int diff = (freq1.GetValueOrDefault(num) - freq2.GetValueOrDefault(num)) / 2;
            if (diff > 0)
            {
                for (int i = 0; i < diff; i++) extra1.Add(num);
            }
            else if (diff < 0)
            {
                for (int i = 0; i < -diff; i++) extra2.Add(num);
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
