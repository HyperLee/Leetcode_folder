namespace leetcode_1606;

class Program
{
    /// <summary>
    /// 1606. Find Servers That Handled Most Number of Requests
    /// https://leetcode.com/problems/find-servers-that-handled-most-number-of-requests/description/
    ///
    /// English:
    /// You have k servers numbered from 0 to k-1 that are being used to handle multiple requests simultaneously.
    /// Each server has infinite computational capacity but cannot handle more than one request at a time.
    /// The requests are assigned to servers according to a specific algorithm:
    ///
    /// - The ith (0-indexed) request arrives.
    /// - If all servers are busy, the request is dropped (not handled at all).
    /// - If the (i % k)th server is available, assign the request to that server.
    /// - Otherwise, assign the request to the next available server (wrapping around the list of servers
    ///   and starting from 0 if necessary). For example, if the ith server is busy, try to assign the
    ///   request to the (i+1)th server, then the (i+2)th server, and so on.
    ///
    /// You are given a strictly increasing array arrival of positive integers, where arrival[i] represents
    /// the arrival time of the ith request, and another array load, where load[i] represents the load of
    /// the ith request (the time it takes to complete). Your goal is to find the busiest server(s).
    /// A server is considered busiest if it handled the most number of requests successfully among all the servers.
    ///
    /// Return a list containing the IDs (0-indexed) of the busiest server(s). You may return the IDs in any order.
    ///
    /// 繁體中文：
    /// 1606. 找出處理最多請求的伺服器
    /// https://leetcode.cn/problems/find-servers-that-handled-most-number-of-requests/description/
    ///
    /// 你有 k 台伺服器，編號從 0 到 k-1，用來同時處理多個請求。
    /// 每台伺服器都具有無限的運算能力，但同一時間不能處理超過一個請求。
    /// 請求會依照以下演算法分派給伺服器：
    ///
    /// - 第 i 個請求到達（索引從 0 開始）。
    /// - 如果所有伺服器都忙碌，則丟棄該請求（完全不處理）。
    /// - 如果編號為 i % k 的伺服器可用，則將該請求分派給它。
    /// - 否則，將該請求分派給編號順序中的下一台可用伺服器；必要時繞回編號 0 繼續尋找。
    ///   例如，如果第 i 台伺服器忙碌，則嘗試將請求分派給第 i+1 台，再嘗試第 i+2 台，依此類推。
    ///
    /// 給定一個由正整數組成且嚴格遞增的陣列 arrival，其中 arrival[i] 表示第 i 個請求的到達時間；
    /// 另有一個陣列 load，其中 load[i] 表示第 i 個請求的負載（完成該請求所需的時間）。
    /// 你的目標是找出最忙碌的伺服器。如果某台伺服器成功處理的請求數量為所有伺服器中最多，
    /// 則該伺服器被視為最忙碌的伺服器。
    ///
    /// 回傳一個清單，包含所有最忙碌伺服器的編號（從 0 開始）。你可以依任意順序回傳這些編號。
    /// </summary>
    /// <param name="args">命令列引數。</param>
    /// <remarks>
    /// 執行固定測試資料，將兩種解法的結果排序後與預期答案比較。
    /// 不使用命令列引數；成功時輸出每組輸入與結果，失敗時拋出例外。
    /// 完整隨機比對與大型案例請執行 checks 專案。
    /// </remarks>
    static void Main(string[] args)
    {
        RunExample("官方範例一", 3, [1, 2, 3, 4, 5], [5, 2, 3, 3, 3], [1]);
        RunExample("官方範例二", 3, [1, 2, 3, 4], [1, 2, 1, 2], [0]);
        RunExample("官方範例三", 3, [1, 2, 3], [10, 12, 11], [0, 1, 2]);
        RunExample("單伺服器", 1, [1, 2, 3], [2, 1, 1], [0]);
        RunExample("全部忙碌時丟棄", 2, [1, 2, 3, 11], [10, 10, 1, 1], [0]);
        RunExample("繞回編號零", 3, [1, 2, 3, 4, 5], [3, 10, 10, 1, 1], [0]);
        RunExample("並列最忙碌", 2, [1, 2, 3, 4], [1, 1, 1, 1], [0, 1]);
        RunExample("完成時間等於到達時間", 2, [1, 2, 3], [2, 10, 1], [0]);
        Console.WriteLine("PASS: 8 examples (both solutions).");
    }

    /// <summary>
    /// 執行一組合法題目輸入，顯示兩種解法與預期答案並以無順序比較驗證。
    /// 所有答案相符時正常結束，否則拋出例外，讓命令列執行回報失敗。
    /// </summary>
    /// <param name="name">顯示於主控台的案例名稱。</param>
    /// <param name="k">伺服器數量，介於 1 與 100,000。</param>
    /// <param name="arrival">嚴格遞增的到達時間，長度與 load 相同且介於 1 與 100,000。</param>
    /// <param name="load">各請求所需時間；與 arrival 的元素均介於 1 與 1,000,000,000。</param>
    /// <param name="expected">預期的全部最忙碌伺服器編號，順序不限。</param>
    private static void RunExample(string name, int k, int[] arrival, int[] load, int[] expected)
    {
        Program solution = new Program();
        int[] first = solution.BusiestServers(k, arrival, load).OrderBy(id => id).ToArray();
        int[] second = solution.BusiestServers2(k, arrival, load).OrderBy(id => id).ToArray();
        int[] sortedExpected = expected.OrderBy(id => id).ToArray();

        Console.WriteLine($"{name}: k={k}, arrival=[{string.Join(",", arrival)}], load=[{string.Join(",", load)}]");
        Console.WriteLine($"  expected=[{string.Join(",", sortedExpected)}], solution1=[{string.Join(",", first)}], solution2=[{string.Join(",", second)}]");
        if (!first.SequenceEqual(sortedExpected) || !second.SequenceEqual(sortedExpected))
        {
            throw new InvalidOperationException($"{name}: 解法結果與預期答案不符。");
        }
    }

    /// <summary>
    /// 方法一：模擬 + 有序集合 + 優先佇列。
    /// available 保存所有空閒伺服器；busy 依完成時間釋放伺服器。
    /// 以區間最小值實作 ceiling 查找，避免逐一掃描空閒集合。
    /// 對合法輸入逐一模擬分派與丟棄，最後回傳處理次數最多的全部伺服器。
    /// 時間 O((n + k) log k)，空間 O(k)，其中 n 為請求數（k = 1 時為 O(n)）。
    /// </summary>
    /// <param name="k">伺服器數量，1 &lt;= k &lt;= 100,000。</param>
    /// <param name="arrival">嚴格遞增的到達時間；1 &lt;= 長度 &lt;= 100,000，各值介於 1 與 1,000,000,000。</param>
    /// <param name="load">與 arrival 等長，各請求所需時間介於 1 與 1,000,000,000。</param>
    /// <returns>成功處理請求數最多的所有伺服器編號；編號介於 0 與 k - 1，順序不限。</returns>
    public IList<int> BusiestServers(int k, int[] arrival, int[] load)
    {
        SortedSet<int> available = new SortedSet<int>();
        for (int i = 0; i < k; i++)
        {
            available.Add(i);
        }

        PriorityQueue<(int finishTime, int server), int> busy = new PriorityQueue<(int, int), int>(
            Comparer<int>.Create((a, b) => a.CompareTo(b)));

        int[] requests = new int[k];
        for (int i = 0; i < arrival.Length; i++)
        {
            // 先釋放所有已完成的伺服器；等於到達時間也能立即接下新請求。
            while (busy.Count > 0 && busy.Peek().finishTime <= arrival[i])
            {
                var item = busy.Dequeue();
                available.Add(item.server);
            }

            if (available.Count == 0)
            {
                // 丟棄仍消耗原始請求索引 i，下一輪的起點仍是 (i + 1) % k。
                continue;
            }

            int target = i % k;
            // 等同 Java TreeSet.ceiling(target)：找出 >= target 的最小空閒編號。
            // 若最大編號也小於 target，則繞回最小編號；否則區間必定非空。
            // 不讀取區間視圖的 Count，避免為計數而遍歷區間。
            int serverId = available.Max < target
                ? available.Min
                : available.GetViewBetween(target, k - 1).Min;

            requests[serverId]++;
            busy.Enqueue((arrival[i] + load[i], serverId), arrival[i] + load[i]);
            available.Remove(serverId);
        }

        // 收集所有最大值，不能只保留第一台最忙碌伺服器。
        int maxRequests = requests.Max();
        List<int> result = new List<int>();
        for (int i = 0; i < k; i++)
        {
            if (requests[i] == maxRequests)
            {
                result.Add(i);
            }
        }
        return result;
    }

    /// <summary>
    /// 方法二：模擬 + 雙優先佇列。
    /// busy 依完成時間釋放伺服器，available 以虛擬順位將循環搜尋轉成最小值查找。
    /// 對合法輸入逐一模擬分派與丟棄，最後回傳處理次數最多的全部伺服器。
    /// 時間 O((n + k) log k)，空間 O(k)，其中 n 為請求數（k = 1 時為 O(n)）。
    /// </summary>
    /// <param name="k">伺服器數量，1 &lt;= k &lt;= 100,000。</param>
    /// <param name="arrival">嚴格遞增的到達時間；1 &lt;= 長度 &lt;= 100,000，各值介於 1 與 1,000,000,000。</param>
    /// <param name="load">與 arrival 等長，各請求所需時間介於 1 與 1,000,000,000。</param>
    /// <returns>成功處理請求數最多的所有伺服器編號；編號介於 0 與 k - 1，順序不限。</returns>
    public IList<int> BusiestServers2(int k, int[] arrival, int[] load)
    {
        var available = new PriorityQueue<int, int>();
        for (int i = 0; i < k; i++)
        {
            available.Enqueue(i, i);
        }

        var busy = new PriorityQueue<(int finishTime, int server), int>();
        int[] requests = new int[k];

        for (int i = 0; i < arrival.Length; i++)
        {
            // 先釋放所有已完成的伺服器；等於到達時間也能立即接下新請求。
            while (busy.Count > 0 && busy.Peek().finishTime <= arrival[i])
            {
                var (_, id) = busy.Dequeue();
                // 距離 d = ((id - i) % k + k) % k 位於 [0, k - 1]，修正 C# 的負餘數。
                // pos = i + d 是從 i 起第一個與 id 同餘的順位，最小 pos 即循環搜尋的首選。
                int pos = i + ((id - i) % k + k) % k;
                available.Enqueue(id, pos);
            }

            if (available.Count == 0)
            {
                // 丟棄仍消耗原始請求索引 i，下一輪的起點仍是 (i + 1) % k。
                continue;
            }

            // 每輪開始的順位都 >= i；本輪若有順位 i，必定先被取出。
            // 因此剩下的順位都 >= i + 1，既有空閒項目不需重新計算順位。
            // 佇列元素已是實際編號；% k 保留原寫法，套用在 id 上不改變結果。
            int serverId = available.Dequeue() % k;
            requests[serverId]++;
            busy.Enqueue((arrival[i] + load[i], serverId), arrival[i] + load[i]);
        }

        // 保留所有並列最大次數的編號。
        int maxRequest = requests.Max();
        return Enumerable.Range(0, k)
            .Where(i => requests[i] == maxRequest)
            .ToList();
    }
}