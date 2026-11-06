namespace leetcode_2747;

class Program
{
    /// <summary>
    /// 2747. Count Zero Request Servers
    /// https://leetcode.com/problems/count-zero-request-servers/description/
    /// 2747. 统计没有收到请求的服务器数目
    /// https://leetcode.cn/problems/count-zero-request-servers/description/
    ///
    /// English:
    /// You are given an integer n denoting the total number of servers and a 2D 0-indexed
    /// integer array logs, where logs[i] = [server_id, time] denotes that the server with
    /// id server_id received a request at time time.
    ///
    /// You are also given an integer x and a 0-indexed integer array queries.
    ///
    /// Return a 0-indexed integer array arr of length queries.length where arr[i]
    /// represents the number of servers that did not receive any requests during the
    /// time interval [queries[i] - x, queries[i]].
    ///
    /// Note that the time intervals are inclusive.
    ///
    /// 繁體中文：
    /// 給定一個整數 n，表示伺服器總數，以及一個索引從 0 開始的二維整數陣列 logs。
    /// 其中 logs[i] = [server_id, time] 表示編號為 server_id 的伺服器在時間 time 收到一筆請求。
    ///
    /// 另外給定一個整數 x，以及一個索引從 0 開始的整數陣列 queries。
    ///
    /// 回傳一個索引從 0 開始、長度為 queries.length 的整數陣列 arr，
    /// 其中 arr[i] 表示在時間區間 [queries[i] - x, queries[i]] 內沒有收到任何請求的伺服器數量。
    ///
    /// 請注意，時間區間包含起點與終點。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 保留上方題目描述；此進入點執行固定案例及可重現的亂數交叉驗證。
        Program solution = new Program();
        (string Name, int N, int[][] Logs, int X, int[] Queries, int[] Expected)[] cases =
        [
            ("官方範例一", 3, [[1, 3], [2, 6], [1, 5]], 5, [10, 11], [1, 2]),
            ("官方範例二", 3, [[2, 4], [2, 1], [1, 2], [3, 1]], 2, [3, 4], [0, 1]),
            ("閉區間與端點外一格", 4, [[1, 4], [2, 5], [3, 10], [4, 11]], 5, [10], [2]),
            ("移除舊請求仍然活躍", 2, [[1, 2], [1, 4], [2, 5]], 3, [5, 6, 8], [0, 0, 1]),
            ("未排序與重複查詢", 3, [[2, 6], [1, 3], [1, 5]], 5, [11, 6, 10, 6], [2, 1, 1, 1]),
            ("視窗無請求", 3, [[1, 2], [2, 20]], 2, [5, 10], [3, 3]),
            ("所有伺服器活躍", 3, [[3, 4], [1, 3], [2, 5]], 2, [5], [0]),
            ("單一伺服器", 1, [[1, 2], [1, 2]], 1, [2, 3, 4], [0, 0, 1]),
            ("題目數值上限", 100000, [[100000, 1000000], [1, 900000], [2, 899999]],
                100000, [1000000, 999999], [99998, 99998])
        ];

        foreach ((string name, int n, int[][] logs, int x, int[] queries, int[] expected) in cases)
        {
            VerifyCase(solution, name, n, logs, x, queries, expected, true);
        }

        const int randomCaseCount = 300;
        Random random = new Random(2747);
        for (int test = 0; test < randomCaseCount; test++)
        {
            int n = random.Next(1, 9);
            int x = random.Next(1, 11);
            int[][] logs = new int[random.Next(1, 31)][];
            for (int i = 0; i < logs.Length; i++)
            {
                logs[i] = [random.Next(1, n + 1), random.Next(1, 41)];
            }

            int[] queries = new int[random.Next(1, 16)];
            for (int i = 0; i < queries.Length; i++)
            {
                queries[i] = random.Next(x + 1, 51);
            }

            int[] expected = solution.CountServersBruteForce(n, CloneLogs(logs), x, (int[])queries.Clone());
            VerifyCase(solution, $"亂數案例 {test + 1}（seed=2747）", n, logs, x, queries, expected, false);
        }

        Console.WriteLine($"PASS: {cases.Length} 組固定案例（雙解法）與 {randomCaseCount} 組亂數交叉驗證全部通過。");
    }

    /// <summary>
    /// 複製合法的二欄請求紀錄，讓每次解法呼叫使用獨立資料，避免原地排序影響其他檢查。
    /// </summary>
    /// <param name="logs">每列為 [伺服器編號, 時間] 的題目合法紀錄。</param>
    /// <returns>外層與每個內層陣列皆獨立的新紀錄。</returns>
    private static int[][] CloneLogs(int[][] logs)
    {
        return logs.Select(log => (int[])log.Clone()).ToArray();
    }

    /// <summary>
    /// 對合法題目輸入執行兩種解法，逐項比對預期答案；失敗時輸出重現資料並拋出例外。
    /// 每次呼叫都複製輸入，因此不會變動呼叫端的紀錄與查詢順序。
    /// </summary>
    /// <param name="solution">提供兩種解法的實例。</param>
    /// <param name="name">顯示於結果與失敗訊息的案例名稱。</param>
    /// <param name="n">伺服器數量，範圍為 1 至 100000。</param>
    /// <param name="logs">非空請求紀錄，編號為 1 至 n，時間為 1 至 1000000。</param>
    /// <param name="x">視窗寬度，範圍為 1 至 100000。</param>
    /// <param name="queries">非空查詢，滿足 x 小於查詢時間且查詢時間不超過 1000000。</param>
    /// <param name="expected">依原始查詢順序排列的預期答案。</param>
    /// <param name="printResult">是否逐一印出成功結果；失敗一律輸出。</param>
    private static void VerifyCase(Program solution, string name, int n, int[][] logs,
        int x, int[] queries, int[] expected, bool printResult)
    {
        (string Name, Func<int, int[][], int, int[], int[]> Solve)[] methods =
        [
            ("滑動視窗", solution.CountServers),
            ("暴力掃描", solution.CountServersBruteForce)
        ];

        foreach ((string method, Func<int, int[][], int, int[], int[]> solve) in methods)
        {
            int[] actual;
            try
            {
                actual = solve(n, CloneLogs(logs), x, (int[])queries.Clone());
                if (!actual.SequenceEqual(expected))
                {
                    throw new InvalidOperationException($"預期 [{string.Join(",", expected)}]，實際 [{string.Join(",", actual)}]");
                }
            }
            catch (Exception)
            {
                Console.Error.WriteLine($"FAIL: {name} / {method}");
                Console.Error.WriteLine($"n={n}, logs={System.Text.Json.JsonSerializer.Serialize(logs)}, x={x}, queries=[{string.Join(",", queries)}]");
                throw;
            }

            if (printResult)
            {
                Console.WriteLine($"PASS: {name} / {method} | 預期 [{string.Join(",", expected)}] | 實際 [{string.Join(",", actual)}]");
            }
        }
    }

    /// <summary>
    /// 逐查詢掃描全部合法紀錄，以集合去重活躍伺服器，回傳原查詢順序的零請求伺服器數。
    /// 不修改 logs 或 queries；適合作為小型資料的參考解法。
    /// 時間平均為 O(Q × L)，集合空間為 O(min(n, L))，另需 O(Q) 的回傳陣列。
    /// </summary>
    /// <param name="n">伺服器數量，範圍為 1 至 100000。</param>
    /// <param name="logs">1 至 100000 筆 [編號, 時間]；編號為 1 至 n，時間為 1 至 1000000。</param>
    /// <param name="x">視窗寬度，範圍為 1 至 100000。</param>
    /// <param name="queries">1 至 100000 個時間，滿足 x 小於查詢時間且查詢時間不超過 1000000。</param>
    /// <returns>每個閉區間 [queries[i] - x, queries[i]] 內未收到請求的伺服器數。</returns>
    public int[] CountServersBruteForce(int n, int[][] logs, int x, int[] queries)
    {
        int[] answers = new int[queries.Length];
        for (int i = 0; i < queries.Length; i++)
        {
            HashSet<int> activeServers = new HashSet<int>();
            foreach (int[] log in logs)
            {
                // 兩端都包含；集合只計不同伺服器，不計請求筆數。
                if (queries[i] - x <= log[1] && log[1] <= queries[i])
                {
                    activeServers.Add(log[0]);
                }
            }

            answers[i] = n - activeServers.Count;
        }

        return answers;
    }

    /// <summary>
    /// 將紀錄及查詢依時間排序，以只向右移動的雙指標維護閉區間內各伺服器的請求次數。
    /// 回傳原始查詢順序的零請求伺服器數；logs 會原地排序，queries 不會修改。
    /// 時間為 O(L log L + Q log Q + n)，工作陣列空間為 O(n + Q)，L 為紀錄數、Q 為查詢數。
    /// </summary>
    /// <param name="n">伺服器數量，範圍為 1 至 100000。</param>
    /// <param name="logs">1 至 100000 筆 [編號, 時間]；編號為 1 至 n，時間為 1 至 1000000。</param>
    /// <param name="x">視窗寬度，範圍為 1 至 100000。</param>
    /// <param name="queries">1 至 100000 個時間，滿足 x 小於查詢時間且查詢時間不超過 1000000。</param>
    /// <returns>每個閉區間 [queries[i] - x, queries[i]] 內未收到請求的伺服器數。</returns>
    public int[] CountServers(int n, int[][] logs, int x, int[] queries)
    {
        Array.Sort(logs, (a, b) => a[1].CompareTo(b[1]));

        int m = queries.Length;
        int[] ids = Enumerable.Range(0, m).ToArray();
        // 只排序查詢索引，讓處理時間單調遞增，仍能把答案寫回原位置。
        Array.Sort(ids, (i, j) => queries[i].CompareTo(queries[j]));

        int[] ans = new int[m];
        int[] cnt = new int[n + 1];
        int left = 0, right = 0, active = 0;

        foreach (int i in ids)
        {
            int q = queries[i];
            // right 是下一筆尚未加入的紀錄；時間等於右端點也要納入。
            while (right < logs.Length && logs[right][1] <= q)
            {
                int id = logs[right++][0];
                // 只有從零筆變成一筆，才新增一台活躍伺服器。
                if (cnt[id]++ == 0)
                {
                    ++active;
                }
            }

            // 左端點仍在視窗內，僅移除嚴格小於 q - x 的紀錄。
            while (left < right && logs[left][1] < q - x)
            {
                int id = logs[left++][0];
                // 同一台可能還有其他請求，必須移除最後一筆才能減少 active。
                if (--cnt[id] == 0)
                {
                    --active;
                }
            }

            // 此刻 [left, right) 恰為有效紀錄，active 等於 cnt 大於零的伺服器數。
            ans[i] = n - active;
        }

        return ans;
    }
}