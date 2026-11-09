namespace leetcode_1606;

internal static class CheckRunner
{
    /// <summary>
    /// 驗證兩種解法的固定案例、合法數值上界與固定種子隨機比對。
    /// 預設另執行十萬規模案例；全部通過時輸出 PASS，失敗時拋出例外。
    /// </summary>
    /// <param name="args">傳入 --skip-large 可略過大型案例；其他引數不使用。</param>
    private static void Main(string[] args)
    {
        Check("BusiestServers_Example1", 3, [1, 2, 3, 4, 5], [5, 2, 3, 3, 3], [1]);
        Check("BusiestServers_Example2", 3, [1, 2, 3, 4], [1, 2, 1, 2], [0]);
        Check("BusiestServers_Example3", 3, [1, 2, 3], [10, 12, 11], [0, 1, 2]);
        Check("BusiestServers_OneServer", 1, [1, 2, 3], [2, 1, 1], [0]);
        Check("BusiestServers_AllServersBusy_DropsRequest", 2, [1, 2, 3, 11], [10, 10, 1, 1], [0]);
        Check("BusiestServers_WraparoundAssignment", 3, [1, 2, 3, 4, 5], [3, 10, 10, 1, 1], [0]);
        Check("BusiestServers_TiedBusiestServers", 2, [1, 2, 3, 4], [1, 1, 1, 1], [0, 1]);
        Check("BusiestServers_CompletionExactlyAtArrival", 2, [1, 2, 3], [2, 10, 1], [0]);
        Check("BusiestServers_SelectsSuccessor", 4, [1, 2, 3, 4, 5, 6], [10, 10, 1, 10, 1, 1], [2]);
        Check("BusiestServers_MoreServersThanRequests", 5, [1, 2], [10, 10], [0, 1]);

        Check("BusiestServers_MaximumTimeValues", 2,
            [999_999_998, 999_999_999, 1_000_000_000],
            [1_000_000_000, 1_000_000_000, 1_000_000_000], [0, 1]);
        Check("BusiestServers_MaximumFinishTime", 1, [1_000_000_000], [1_000_000_000], [0]);

        Random random = new Random(1606);
        for (int sample = 0; sample < 500; sample++)
        {
            int k = random.Next(1, 15);
            int n = random.Next(1, 100);
            int[] arrival = new int[n];
            int[] load = new int[n];
            int time = 0;
            for (int i = 0; i < n; i++)
            {
                time += random.Next(1, 5);
                arrival[i] = time;
                load[i] = random.Next(1, 40);
            }

            Check($"BusiestServers_Random_{sample}", k, arrival, load, BruteForce(k, arrival, load));
        }

        Console.WriteLine("PASS: 12 fixed cases and 500 seeded brute-force comparisons (both solutions).");
        if (args.Contains("--skip-large"))
        {
            return;
        }

        // 大量空閒伺服器且 target 持續增加，涵蓋原本逐一掃描的慢速情境。
        int[] largeArrival = Enumerable.Range(1, 100_000).ToArray();
        Check("BusiestServers_LargeIdleSet", 100_000, largeArrival,
            Enumerable.Repeat(1, 100_000).ToArray(), Enumerable.Range(0, 100_000).ToArray());
        Console.WriteLine("PASS: 1 large case with 100,000 servers and requests (both solutions).");
    }

    /// <summary>對同一組合法題目輸入執行兩種解法，分別與預期答案比較。</summary>
    /// <param name="name">失敗時回報的案例名稱。</param>
    /// <param name="k">1 至 100,000 台伺服器。</param>
    /// <param name="arrival">1 至 100,000 個嚴格遞增時間，值介於 1 與 1,000,000,000。</param>
    /// <param name="load">與 arrival 等長，值介於 1 與 1,000,000,000 的處理時間。</param>
    /// <param name="expected">預期的全部最忙碌編號，順序不限。</param>
    private static void Check(string name, int k, int[] arrival, int[] load, int[] expected)
    {
        Program solution = new Program();
        AssertIds(name + "_Solution1", solution.BusiestServers(k, arrival, load), expected);
        AssertIds(name + "_Solution2", solution.BusiestServers2(k, arrival, load), expected);
    }

    /// <summary>排序後比較完整編號序列；相符時正常結束，不符時拋出含案例名稱的例外。</summary>
    /// <param name="name">識別案例與解法的名稱。</param>
    /// <param name="actual">解法回傳的編號清單。</param>
    /// <param name="expected">預期編號清單，順序不限。</param>
    private static void AssertIds(string name, IList<int> actual, int[] expected)
    {
        if (!actual.OrderBy(id => id).SequenceEqual(expected.OrderBy(id => id)))
        {
            throw new InvalidOperationException($"{name}: expected [{string.Join(",", expected)}], got [{string.Join(",", actual)}]");
        }
    }

    /// <summary>
    /// 以 O(nk) 循環掃描直接模擬題意，作為小型隨機資料的獨立正確性基準。
    /// 接受合法題目輸入，回傳所有處理次數最多的伺服器編號；額外空間 O(k)。
    /// </summary>
    /// <param name="k">正整數伺服器數量；隨機比對僅使用小規模。</param>
    /// <param name="arrival">非空且嚴格遞增的正整數到達時間。</param>
    /// <param name="load">與 arrival 等長的正整數處理時間。</param>
    /// <returns>全部並列最忙碌的伺服器編號。</returns>
    private static int[] BruteForce(int k, int[] arrival, int[] load)
    {
        int[] finishTimes = new int[k];
        int[] counts = new int[k];
        for (int i = 0; i < arrival.Length; i++)
        {
            // 從原始請求索引開始繞一圈；第一台已完成的伺服器就是題目指定的選擇。
            for (int offset = 0; offset < k; offset++)
            {
                int server = (i + offset) % k;
                if (finishTimes[server] <= arrival[i])
                {
                    finishTimes[server] = arrival[i] + load[i];
                    counts[server]++;
                    break;
                }
            }
        }

        int maxCount = counts.Max();
        return Enumerable.Range(0, k).Where(id => counts[id] == maxCount).ToArray();
    }
}