namespace leetcode_1882;

class Program
{
    /// <summary>
    /// 1882. Process Tasks Using Servers
    /// https://leetcode.com/problems/process-tasks-using-servers/description/
    /// 1882. 使用伺服器處理任務
    /// https://leetcode.cn/problems/process-tasks-using-servers/description/
    /// English:
    /// 
    /// You are given two 0-indexed integer arrays servers and tasks of lengths n and m respectively.
    /// servers[i] is the weight of the ith server, and tasks[j] is the time needed to process the jth task in seconds.
    /// 
    /// 
    /// Tasks are assigned to the servers using a task queue. Initially, all servers are free, and the queue is empty.
    /// 
    /// 
    /// At second j, the jth task is inserted into the queue (starting with the 0th task being inserted at second 0).
    /// As long as there are free servers and the queue is not empty, the task in the front of the queue will be
    /// assigned to a free server with the smallest weight, and in case of a tie, it is assigned to a free server
    /// with the smallest index.
    /// 
    /// 
    /// If there are no free servers and the queue is not empty, we wait until a server becomes free and immediately
    /// assign the next task. If multiple servers become free at the same time, then multiple tasks from the queue
    /// will be assigned in order of insertion following the weight and index priorities above.
    /// 
    /// 
    /// A server that is assigned task j at second t will be free again at second t + tasks[j].
    /// 
    /// 
    /// Build an array ans of length m, where ans[j] is the index of the server the jth task will be assigned to.
    /// 
    /// Return the array ans.
    /// 繁體中文：
    /// 
    /// 給定兩個索引從 0 開始的整數陣列 servers 與 tasks，長度分別為 n 與 m。
    /// servers[i] 表示第 i 台伺服器的權重，tasks[j] 表示處理第 j 個任務所需的時間，單位為秒。
    /// 
    /// 
    /// 任務透過任務佇列分配給伺服器。最初，所有伺服器皆為閒置狀態，且佇列為空。
    /// 
    /// 
    /// 在第 j 秒，第 j 個任務會被加入佇列（第 0 個任務於第 0 秒加入）。
    /// 只要存在閒置伺服器且佇列不為空，就會將佇列最前端的任務分配給權重最小的閒置伺服器；
    /// 若權重相同，則分配給索引最小的閒置伺服器。
    /// 
    /// 
    /// 若沒有閒置伺服器且佇列不為空，就等待直到有伺服器恢復閒置，並立即分配下一個任務。
    /// 若多台伺服器同時恢復閒置，則依照任務加入佇列的順序，
    /// 遵循上述權重與索引的優先順序，分配佇列中的多個任務。
    /// 
    /// 
    /// 若某台伺服器於第 t 秒被分配第 j 個任務，則該伺服器會於第 t + tasks[j] 秒恢復閒置。
    /// 
    /// 
    /// 建立長度為 m 的陣列 ans，其中 ans[j] 表示第 j 個任務被分配到的伺服器索引。
    /// 
    /// 回傳陣列 ans。
    /// </summary>
    /// <param name="args">命令列引數。</param>
    static void Main(string[] args)
    {
        var cases = new (string Name, int[] Servers, int[] Tasks, int[] Expected)[]
        {
            ("OfficialExample1", [3, 3, 2], [1, 2, 3, 2, 1, 2], [2, 2, 0, 2, 1, 2]),
            ("OfficialExample2", [5, 1, 4, 3, 2], [2, 1, 2, 4, 5, 2, 1], [1, 4, 1, 4, 1, 3, 2]),
            ("EqualWeights_UsesSmallestIndex", [2, 2], [1, 1, 1], [0, 0, 0]),
            ("WeightBeforeIndex", [5, 1], [3, 1], [1, 0]),
            ("SimultaneousReleases_UsesWeight", [3, 1, 2], [4, 3, 2, 1, 1, 1], [1, 2, 0, 1, 2, 1]),
            ("SimultaneousReleases_UsesIndex", [1, 1], [3, 2, 1, 1], [0, 1, 0, 1]),
            ("QueuedTasks_JumpsToRelease", [2, 1], [5, 5, 1, 1, 1], [1, 0, 1, 1, 0]),
            ("ReleaseAtArrival_IsAvailable", [1, 2], [1, 1, 1], [0, 0, 0]),
            ("SingleServer_ProcessesInOrder", [7], [5, 2, 3], [0, 0, 0]),
            ("SingleTask_UsesLowestWeight", [3, 1, 2], [5], [1]),
            ("SingleServer_TimeExceedsInt32", [1], Enumerable.Repeat(200_000, 11_000).ToArray(), new int[11_000]),
            ("TwoServers_TimeExceedsInt32", [1, 1], Enumerable.Repeat(200_000, 22_000).ToArray(),
                Enumerable.Range(0, 22_000).Select(i => i % 2).ToArray())
        };

        var solution = new Program();
        int passed = 0;
        foreach (var test in cases)
        {
            if (RunTest(solution, test.Name, test.Servers, test.Tasks, test.Expected))
            {
                passed++;
            }
        }

        Console.WriteLine($"測試結果：{passed}/{cases.Length} 通過。");
        Environment.ExitCode = passed == cases.Length ? 0 : 1;
    }

    /// <summary>
    /// 執行一組合法題目輸入，以完整預期陣列驗證分配結果，並檢查輸入未被修改。
    /// 小型案例顯示分配陣列，大型案例僅顯示數量；失敗時回報第一個差異並回傳 false。
    /// </summary>
    /// <param name="solution">接受測試的雙優先佇列解法。</param>
    /// <param name="name">描述案例行為的名稱。</param>
    /// <param name="servers">符合題目限制的伺服器權重。</param>
    /// <param name="tasks">符合題目限制的任務耗時。</param>
    /// <param name="expected">每個任務預期分配的伺服器索引。</param>
    /// <returns>結果完全相符且輸入保持原樣時為 true，否則為 false。</returns>
    private static bool RunTest(Program solution, string name, int[] servers, int[] tasks, int[] expected)
    {
        int[] originalServers = (int[])servers.Clone();
        int[] originalTasks = (int[])tasks.Clone();
        try
        {
            int[] actual = solution.AssignTasks(servers, tasks);
            if (!servers.SequenceEqual(originalServers) || !tasks.SequenceEqual(originalTasks))
            {
                Console.WriteLine($"[FAIL] {name}：輸入陣列被修改。");
                return false;
            }

            if (actual.Length != expected.Length)
            {
                Console.WriteLine($"[FAIL] {name}：預期長度 {expected.Length}，實際 {actual.Length}。");
                return false;
            }

            for (int i = 0; i < expected.Length; i++)
            {
                if (actual[i] != expected[i])
                {
                    Console.WriteLine($"[FAIL] {name}：任務 {i} 預期 {expected[i]}，實際 {actual[i]}。");
                    return false;
                }
            }

            string result = actual.Length <= 20 ? $"[{string.Join(",", actual)}]" : $"{actual.Length:N0} 個任務";
            Console.WriteLine($"[PASS] {name}：{result}");
            return true;
        }
        catch (Exception exception)
        {
            Console.WriteLine($"[FAIL] {name}：{exception.GetType().Name}：{exception.Message}");
            return false;
        }
    }

    /// <summary>
    /// 使用雙優先佇列與事件跳躍，依任務到達順序分配權重最小、同權重時索引最小的閒置伺服器。
    /// 輸入須符合題目限制：兩陣列長度介於 1 與 200,000，元素介於 1 與 200,000。
    /// 回傳與 tasks 等長的伺服器索引陣列，不修改輸入；以 long 儲存可能超過 Int32 上限的時間。
    /// </summary>
    /// <param name="servers">各伺服器的權重，陣列索引即伺服器編號。</param>
    /// <param name="tasks">各任務耗時；任務 i 於第 i 秒到達。</param>
    /// <returns>第 i 個元素為處理任務 i 的伺服器索引。</returns>
    public int[] AssignTasks(int[] servers, int[] tasks)
    {
        // busy 只需依完成時間排序；同時完成者會全部移入 idle 後再比較權重與索引。
        var busy = new PriorityQueue<(long t, int idx), long>();
        var idle = new PriorityQueue<(int weight, int idx), (int weight, int idx)>();

        for (int i = 0; i < servers.Length; i++)
        {
            // ValueTuple 的預設比較先比較 weight，再比較 idx。
            idle.Enqueue((servers[i], i), (servers[i], i));
        }

        // 最長累計時間可達 200,000 × 200,000，完成時間與優先權也必須使用 long。
        long ts = 0;
        int[] ans = new int[tasks.Length];

        // 依索引處理即維持 FIFO；即使 ts 已超過到達時間，也不會讓後面的任務插隊。
        for (int i = 0; i < tasks.Length; i++)
        {
            ts = Math.Max(ts, i);
            Release(busy, idle, servers, ts);

            if (idle.Count == 0)
            {
                // 無閒置伺服器時直接跳到下一個完成事件，避免逐秒等待。
                ts = busy.Peek().t;
                Release(busy, idle, servers, ts);
            }

            var server = idle.Dequeue();
            int idx = server.idx;
            ans[i] = idx;
            long finishTime = ts + tasks[i];
            busy.Enqueue((finishTime, idx), finishTime);
        }

        return ans;
    }

    /// <summary>
    /// 將完成時間不晚於 ts 的所有忙碌伺服器移回閒置佇列，讓下一次選擇遵循權重與索引順序。
    /// 兩個佇列須描述同一組伺服器且互不重複；方法更新佇列，不修改權重陣列，無回傳值。
    /// </summary>
    /// <param name="busy">以 long 完成時間排序的忙碌伺服器佇列。</param>
    /// <param name="idle">以權重、索引依序排序的閒置伺服器佇列。</param>
    /// <param name="servers">用於查回伺服器權重的原始陣列。</param>
    /// <param name="ts">目前模擬時間，必須不小於零。</param>
    private void Release(PriorityQueue<(long t, int idx), long> busy,
                         PriorityQueue<(int weight, int idx), (int weight, int idx)> idle,
                         int[] servers, long ts)
    {
        // 必須使用 while 與 <=：包含恰好完成者，並一次釋放所有同時完成的伺服器。
        while (busy.Count > 0 && busy.Peek().t <= ts)
        {
            var server = busy.Dequeue();
            int idx = server.idx;
            idle.Enqueue((servers[idx], idx), (servers[idx], idx));
        }
    }
}