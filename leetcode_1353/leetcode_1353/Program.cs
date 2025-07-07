namespace leetcode_1353;

class Program
{
    /// <summary>
    /// 1353. Maximum Number of Events That Can Be Attended
    /// https://leetcode.com/problems/maximum-number-of-events-that-can-be-attended/description/?envType=daily-question&envId=2025-07-07
    /// 1353. 最多可以參加的會議數目
    /// https://leetcode.cn/problems/maximum-number-of-events-that-can-be-attended/description/?envType=daily-question&envId=2025-07-07
    ///
    /// 題目描述：
    /// 給定一個 events 陣列，events[i] = [startDayi, endDayi]，每個會議 i 從 startDayi 開始到 endDayi 結束。
    /// 你可以在 startDayi <= d <= endDayi 的任一天參加會議 i，但同一天只能參加一場會議。
    /// 請回傳你最多可以參加多少場會議。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        int[][] events = new int[][]
        {
            new int[] {1, 2},
            new int[] {2, 3},
            new int[] {3, 4}
        };
        var program = new Program();
        int result1 = program.MaxEvents(events);
        int result2 = program.MaxEventsDSU(events);
        Console.WriteLine($"MaxEvents 結果: {result1}");
        Console.WriteLine($"MaxEventsDSU 結果: {result2}");
    }


    /// <summary>
    /// 方法一：貪心演算法
    ///
    /// 解題思路：
    /// 1. 先將所有會議依照開始時間排序。
    /// 2. 用一個最小堆（小根堆）維護所有當前可參加的會議的結束時間。
    /// 3. 從第 1 天開始，依序處理到所有會議結束的最晚一天：
    ///    - 將所有開始時間 <= 當前天數的會議加入堆中。
    ///    - 移除所有結束時間 < 當前天數的會議（已過期）。
    ///    - 若堆不為空，選擇結束時間最早的會議參加，並將其從堆中移除，計數+1。
    /// 4. 回傳最多可參加的會議數。
    ///
    /// 時間複雜度：O(N log N + D log N)，N 為會議數，D 為天數範圍。
    /// 空間複雜度：O(N)
    ///
    /// <example>
    /// <code>
    /// int[][] events = new int[][] { new int[] {1,2}, new int[] {2,3}, new int[] {3,4} };
    /// int result = MaxEvents(events); // result = 3
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="events">每個元素為 [startDay, endDay]，代表會議的開始與結束天</param>
    /// <returns>最多可參加的會議數</returns>
    public int MaxEvents(int[][] events)
    {
        int n = events.Length;
        int maxDays = 0;
        // 計算所有會議的最晚結束日
        foreach (var e in events)
        {
            maxDays = Math.Max(maxDays, e[1]);
        }

        // 小根堆，存放可參加會議的結束日，優先選擇最早結束的會議
        var pq = new PriorityQueue<int, int>();
        // 依照會議開始日排序
        Array.Sort(events, (a, b) => a[0] - b[0]);
        int res = 0;
        for (int day = 1, idx = 0; day <= maxDays; day++)
        {
            // 將所有開始日 <= 當前天的會議加入堆
            while (idx < n && events[idx][0] <= day)
            {
                pq.Enqueue(events[idx][1], events[idx][1]);
                idx++;
            }
            // 移除所有結束日 < 當前天的會議（已過期）
            while (pq.Count > 0 && pq.Peek() < day)
            {
                pq.Dequeue();
            }
            // 若有可參加的會議，選擇結束日最早的那一場
            if (pq.Count > 0)
            {
                res++;
                pq.Dequeue();
            }
        }
        return res;
    }

    /// <summary>
    /// 方法二：貪心 + 並查集（Disjoint Set）
    ///
    /// 解題思路：
    /// 1. 先將所有會議依照結束日排序。
    /// 2. 用一個並查集（Disjoint Set）維護每一天的可用狀態。
    /// 3. 對每個會議，嘗試從其開始日到結束日中，選擇最早可用的一天參加。
    ///    - 若該天可用，則將該天標記為已用，並將其與下一天合併。
    /// 4. 回傳最多可參加的會議數。
    ///
    /// 時間複雜度：O(N log D)，N 為會議數，D 為天數範圍。
    /// 空間複雜度：O(D)
    ///
    /// <example>
    /// <code>
    /// int[][] events = new int[][] { new int[] {1,2}, new int[] {2,3}, new int[] {3,4} };
    /// int result = MaxEventsDSU(events); // result = 3
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="events">每個元素為 [startDay, endDay]，代表會議的開始與結束天</param>
    /// <returns>最多可參加的會議數</returns>
    public int MaxEventsDSU(int[][] events)
    {
        int n = events.Length;
        // 依照結束日排序，優先安排早結束的會議
        Array.Sort(events, (a, b) => a[1] - b[1]);
        int maxDay = 0;
        // 找出所有會議的最晚結束日
        foreach (var e in events)
        {
            maxDay = Math.Max(maxDay, e[1]);
        }
        // 並查集 parent[i] 代表 i 天的下一個可用天
        int[] parent = new int[maxDay + 2];
        for (int i = 0; i <= maxDay + 1; i++)
        {
            parent[i] = i;
        }
        // 並查集尋找函式，回傳 x 的根節點（即下一個可用天）
        int Find(int x)
        {
            if (parent[x] != x)
            {
                parent[x] = Find(parent[x]); // 路徑壓縮，優化查找效率
            }
            return parent[x];
        }
        int res = 0;
        // 依序處理每個會議
        foreach (var e in events)
        {
            // 嘗試找到該會議可用的最早天數
            int available = Find(e[0]);
            // 若該天在會議結束日前，則可以參加
            if (available <= e[1])
            {
                res++;
                // 將該天標記為已用，合併到下一天
                parent[available] = Find(available + 1);
            }
        }
        return res;
    }
}
