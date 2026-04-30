namespace leetcode_1046;

class Program
{
    /// <summary>
    /// 1046. Last Stone Weight
    /// https://leetcode.com/problems/last-stone-weight/description/
    /// <para>
    /// You are given an array of integers stones where stones[i] is the weight of the ith stone.
    /// We are playing a game with the stones. On each turn, we choose the heaviest two stones and smash them together.
    /// Suppose the heaviest two stones have weights x and y with x &lt;= y. The result of this smash is:
    /// - If x == y, both stones are destroyed.
    /// - If x != y, the stone of weight x is destroyed, and the stone of weight y has new weight y - x.
    /// At the end of the game, there is at most one stone left.
    /// Return the weight of the last remaining stone. If there are no stones left, return 0.
    /// </para>
    /// <para>
    /// 1046. 最後一塊石頭的重量
    /// https://leetcode.cn/problems/last-stone-weight/description/
    /// </para>
    /// <para>
    /// 給你一個整數陣列 stones，其中 stones[i] 是第 i 顆石頭的重量。
    /// 我們正在用石頭玩一個遊戲。每個回合，我們選擇最重的兩顆石頭並將它們互相砸碎。
    /// 假設最重的兩顆石頭重量分別為 x 和 y，且 x &lt;= y。砸碎的結果如下：
    /// - 如果 x == y，兩顆石頭都被摧毀。
    /// - 如果 x != y，重量為 x 的石頭被摧毀，重量為 y 的石頭新重量為 y - x。
    /// 遊戲結束時，最多只剩一顆石頭。
    /// 回傳最後剩餘石頭的重量。如果沒有石頭剩下，則回傳 0。
    /// </para>
    /// </summary>
    /// <param name="args">命令列引數</param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試案例 1：stones = [2,7,4,1,8,1]，預期輸出：1
        int[] stones1 = [2, 7, 4, 1, 8, 1];
        Console.WriteLine($"Test 1: {solution.LastStoneWeight(stones1)}"); // 1

        // 測試案例 2：stones = [1]，預期輸出：1
        int[] stones2 = [1];
        Console.WriteLine($"Test 2: {solution.LastStoneWeight(stones2)}"); // 1

        // 測試案例 3：stones = [3,3]，預期輸出：0（兩顆重量相同，互相摧毀）
        int[] stones3 = [3, 3];
        Console.WriteLine($"Test 3: {solution.LastStoneWeight(stones3)}"); // 0

        // 測試案例 4：stones = [10,4,2,10]，預期輸出：2（10 和 10 互相摧毀，4 和 2 碰撞剩 2）
        int[] stones4 = [10, 4, 2, 10];
        Console.WriteLine($"Test 4: {solution.LastStoneWeight(stones4)}"); // 2
    }

    /// <summary>
    /// 回傳最後一塊石頭的重量。
    /// <para>
    /// 演算法思路（使用最大優先佇列 / Max-Heap）：
    /// 每次從石頭堆中取出最重的兩顆石頭進行碰撞，根據結果決定是否將差值石頭放回，
    /// 直到佇列中剩餘不足 2 顆石頭為止。
    /// </para>
    /// <para>
    /// 步驟：
    /// 1. 將所有石頭加入優先佇列，以「負值」作為優先度實現最大堆（.NET 預設為最小堆）。
    /// 2. 每輪從佇列取出最大值 y，再取出次大值 x（保證 x ≤ y）。
    /// 3. 若 x == y，兩石頭均毀滅，不加入新元素。
    /// 4. 若 x < y，差值 y - x 加回佇列繼續參與碰撞。
    /// 5. 佇列為空回傳 0，否則回傳唯一剩餘石頭重量。
    /// </para>
    /// <para>
    /// 時間複雜度：O(n log n)，每次 Enqueue/Dequeue 為 O(log n)，最多執行 n 輪。
    /// 空間複雜度：O(n)，優先佇列最多存放 n 個元素。
    /// </para>
    /// <example>
    /// <code>
    /// var sol = new Program();
    /// int[] stones = [2, 7, 4, 1, 8, 1];
    /// int result = sol.LastStoneWeight(stones); // result == 1
    /// </code>
    /// </example>
    /// 
    /// C# 的 PriorityQueue 規則是：
    /// ✅ priority 越小 → 越先被取出
    /// 
    /// ✅ 用負數時會發生什麼？
    /// 假設原本你想要：
    /// priority 越大 → 越先出
    /// 你把它轉成「負數」：
    /// 原本 priority轉成負數
    /// 20   -20
    /// 10   -10
    /// 5    -5
    /// 
    /// ✅ 現在 PriorityQueue 看到的是：
    /// -20, -10, -5
    /// 👉 因為：
    /// -20 < -10 < -5
    /// 👉 所以會這樣取出：
    /// -20 → -10 → -5
    /// 
    /// ✅ ✅ 所以關鍵是
    /// 👉 越大的原始數字 → 變成越小的負數 → 越先被取出
    /// </summary>
    /// <param name="stones">石頭重量陣列，每個元素代表一顆石頭的重量。</param>
    /// <returns>最後剩餘石頭的重量；若無石頭剩下則回傳 0。</returns>
    public int LastStoneWeight(int[] stones)
    {
        // 建立優先佇列：TElement = 石頭重量，TPriority = 負重量
        // .NET PriorityQueue 預設為 Min-Heap，以負值作為優先度即可模擬 Max-Heap
        PriorityQueue<int, int> pq = new PriorityQueue<int, int>();

        // 將所有石頭加入優先佇列，優先度設為負值使最重的石頭最先出列
        foreach(int stone in stones)
        {
            pq.Enqueue(stone, -stone);
        }

        // 每回合取出最重的兩顆石頭進行碰撞，直到剩餘不足 2 顆為止
        while(pq.Count >= 2)
        {
            int y = pq.Dequeue(); // 取出最重的石頭（優先度最小 = 原始值最大）
            int x = pq.Dequeue(); // 取出次重的石頭，此時保證 x <= y

            if(x <= y)
            {
                // x < y：差值 y - x 放回佇列繼續參與後續碰撞
                // x == y：y - x == 0，放入 0 也無影響，但實際上兩顆均摧毀
                // 優先度同樣以負值表示，確保差值石頭在正確位置入堆
                pq.Enqueue(y - x, x - y);
            }
        }

        // 佇列為空代表所有石頭均被摧毀，回傳 0；否則取出唯一剩餘石頭
        return pq.Count == 0 ? 0 : pq.Dequeue();
    }
}
