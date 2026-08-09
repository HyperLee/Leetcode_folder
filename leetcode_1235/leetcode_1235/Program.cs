namespace leetcode_1235;

class Program
{
    /// <summary>
    /// <para>
    /// 1235. Maximum Profit in Job Scheduling
    /// https://leetcode.com/problems/maximum-profit-in-job-scheduling/description/
    ///
    /// We have n jobs, where every job is scheduled to be done from startTime[i] to endTime[i], obtaining a
    /// profit of profit[i].
    /// You are given the startTime, endTime and profit arrays. Return the maximum profit you can take such that
    /// there are no two jobs in the subset with overlapping time ranges.
    /// If you choose a job that ends at time X, you will be able to start another job that starts at time X.
    ///
    /// Example 1:
    /// Input: startTime = [1,2,3,3], endTime = [3,4,5,6], profit = [50,10,40,70]
    /// Output: 120
    /// Illustration: https://assets.leetcode.com/uploads/2019/10/10/sample1_1584.png
    /// Explanation: The subset chosen is the first and fourth job.
    /// Time range [1-3] + [3-6], we get profit of 120 = 50 + 70.
    ///
    /// Example 2:
    /// Input: startTime = [1,2,3,4,6], endTime = [3,5,10,6,9], profit = [20,20,100,70,60]
    /// Output: 150
    /// Illustration: https://assets.leetcode.com/uploads/2019/10/10/sample22_1584.png
    /// Explanation: The subset chosen is the first, fourth and fifth job.
    /// Profit obtained 150 = 20 + 70 + 60.
    ///
    /// Example 3:
    /// Input: startTime = [1,1,1], endTime = [2,3,4], profit = [5,6,4]
    /// Output: 6
    /// Illustration: https://assets.leetcode.com/uploads/2019/10/10/sample3_1584.png
    ///
    /// Constraints:
    /// 1 &lt;= startTime.length == endTime.length == profit.length &lt;= 5 * 10^4
    /// 1 &lt;= startTime[i] &lt; endTime[i] &lt;= 10^9
    /// 1 &lt;= profit[i] &lt;= 10^4
    /// </para>
    /// <para>
    /// 1235. 工作排程的最大收益
    /// https://leetcode.cn/problems/maximum-profit-in-job-scheduling/description/
    ///
    /// 有 n 份工作，每份工作排定從 startTime[i] 執行到 endTime[i]，可獲得 profit[i] 的收益。
    /// 給定 startTime、endTime 與 profit 陣列，請回傳可取得的最大收益，且所選工作中任兩份工作的
    /// 時間範圍都不可重疊。
    /// 如果選擇的工作在時間 X 結束，就可以開始另一份同樣在時間 X 開始的工作。
    ///
    /// 範例 1：
    /// 輸入：startTime = [1,2,3,3], endTime = [3,4,5,6], profit = [50,10,40,70]
    /// 輸出：120
    /// 示意圖：https://assets.leetcode.com/uploads/2019/10/10/sample1_1584.png
    /// 解釋：選擇第一份與第四份工作。
    /// 時間範圍為 [1-3] + [3-6]，取得的收益為 120 = 50 + 70。
    ///
    /// 範例 2：
    /// 輸入：startTime = [1,2,3,4,6], endTime = [3,5,10,6,9], profit = [20,20,100,70,60]
    /// 輸出：150
    /// 示意圖：https://assets.leetcode.com/uploads/2019/10/10/sample22_1584.png
    /// 解釋：選擇第一、第四與第五份工作。
    /// 取得的收益為 150 = 20 + 70 + 60。
    ///
    /// 範例 3：
    /// 輸入：startTime = [1,1,1], endTime = [2,3,4], profit = [5,6,4]
    /// 輸出：6
    /// 示意圖：https://assets.leetcode.com/uploads/2019/10/10/sample3_1584.png
    ///
    /// 限制條件：
    /// 1 &lt;= startTime.length == endTime.length == profit.length &lt;= 5 * 10^4
    /// 1 &lt;= startTime[i] &lt; endTime[i] &lt;= 10^9
    /// 1 &lt;= profit[i] &lt;= 10^4
    /// </para>
    /// </summary>
    /// <remarks>
    /// 程式主要進入點；執行八組固定案例與三種解法，並以 process exit code 表示驗證結果。
    /// </remarks>
    /// <param name="args">此程式不使用命令列參數。</param>
    static void Main(string[] args)
    {
        Environment.ExitCode = RunSamples() ? 0 : 1;
    }

    /// <summary>
    /// 建立並執行八組固定工作排程案例，依序驗證三種最大報酬解法。
    /// 此方法不接受外部輸入；輸出每個案例的預期值、實際值、輸入保留狀態與 PASS/FAIL，
    /// 並回傳全部解法是否通過所有案例。
    /// </summary>
    /// <returns>全部 24 項檢查通過時為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
    private static bool RunSamples()
    {
        (string Name, Func<(int[] StartTime, int[] EndTime, int[] Profit)> BuildInput, int Expected)[] cases =
        [
            ("官方範例一", () => ([1, 2, 3, 3], [3, 4, 5, 6], [50, 10, 40, 70]), 120),
            ("官方範例二", () => ([1, 2, 3, 4, 6], [3, 5, 10, 6, 9], [20, 20, 100, 70, 60]), 150),
            ("官方範例三", () => ([1, 1, 1], [2, 3, 4], [5, 6, 4]), 6),
            ("單一工作與數值上界", () => ([1], [1_000_000_000], [10_000]), 10_000),
            ("未排序且首尾時間相接", () => ([5, 1, 3], [7, 3, 5], [30, 10, 20]), 60),
            ("重複區間與重複報酬", () => ([1, 1, 2, 2], [2, 2, 3, 3], [50, 50, 60, 60]), 110),
            ("局部高報酬的貪心陷阱", () => ([1, 2, 3, 4], [3, 5, 4, 6], [50, 100, 70, 60]), 180),
            ("五萬份互不重疊工作的輸入上界", BuildUpperBoundCase, 500_000_000)
        ];

        int passedChecks = 0;
        int totalChecks = 0;

        foreach ((string name, Func<(int[] StartTime, int[] EndTime, int[] Profit)> buildInput, int expected) in cases)
        {
            (int passed, int total) = RunTestCase(name, buildInput(), expected);
            passedChecks += passed;
            totalChecks += total;
        }

        Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過。");
        return passedChecks == totalChecks;
    }

    /// <summary>
    /// 對同一組工作資料建立三份互不共用的陣列，執行三種解法並驗證答案與輸入保留契約。
    /// 輸入須符合題目限制，三個陣列長度相同且至少包含一份工作；輸出各解法結果，
    /// 並回傳本案例通過的解法數與固定檢查總數 3。
    /// </summary>
    /// <param name="name">顯示於主控台的案例名稱。</param>
    /// <param name="input">開始時間、結束時間與報酬陣列。</param>
    /// <param name="expected">此案例的預期最大報酬。</param>
    /// <returns>本案例通過的解法數與檢查總數。</returns>
    private static (int Passed, int Total) RunTestCase(
        string name,
        (int[] StartTime, int[] EndTime, int[] Profit) input,
        int expected)
    {
        (string Name, Func<int[], int[], int[], int> Solve)[] solutions =
        [
            ("解法一（結束時間前綴 DP）", JobScheduling),
            ("解法二（開始時間後綴 DP）", JobScheduling2),
            ("解法三（優先佇列掃描）", JobScheduling3)
        ];

        int passedChecks = 0;

        Console.WriteLine($"案例：{name}");
        Console.WriteLine($"startTime：{FormatArray(input.StartTime)}");
        Console.WriteLine($"endTime：{FormatArray(input.EndTime)}");
        Console.WriteLine($"profit：{FormatArray(input.Profit)}");
        Console.WriteLine($"預期：{expected}");

        foreach ((string solutionName, Func<int[], int[], int[], int> solve) in solutions)
        {
            int[] startTime = (int[])input.StartTime.Clone();
            int[] endTime = (int[])input.EndTime.Clone();
            int[] profit = (int[])input.Profit.Clone();
            int actual = solve(startTime, endTime, profit);
            bool inputPreserved = startTime.SequenceEqual(input.StartTime)
                && endTime.SequenceEqual(input.EndTime)
                && profit.SequenceEqual(input.Profit);
            bool passed = actual == expected && inputPreserved;

            Console.WriteLine(
                $"{solutionName}實際：{actual}；輸入保留：{(inputPreserved ? "是" : "否")} => {(passed ? "PASS" : "FAIL")}");

            if (passed)
            {
                passedChecks++;
            }
        }

        Console.WriteLine();
        return (passedChecks, solutions.Length);
    }

    /// <summary>
    /// 建立包含五萬份互不重疊工作的合法輸入上界案例。
    /// 每份工作報酬皆為 10000，所有工作皆可選取，因此輸出應為 500000000。
    /// </summary>
    /// <returns>開始時間、結束時間與報酬陣列。</returns>
    private static (int[] StartTime, int[] EndTime, int[] Profit) BuildUpperBoundCase()
    {
        const int jobCount = 50_000;
        int[] startTime = new int[jobCount];
        int[] endTime = new int[jobCount];
        int[] profit = new int[jobCount];

        for (int index = 0; index < jobCount; index++)
        {
            startTime[index] = (index * 2) + 1;
            endTime[index] = (index * 2) + 2;
            profit[index] = 10_000;
        }

        return (startTime, endTime, profit);
    }

    /// <summary>
    /// 將整數陣列轉成穩定的單行字串；短陣列完整顯示，長陣列只顯示長度與前後五筆。
    /// 輸入須為整數陣列；輸出不受地區設定影響，適合主控台與 README 紀錄。
    /// </summary>
    /// <param name="values">要格式化的整數陣列。</param>
    /// <returns>完整或摘要形式的陣列字串。</returns>
    private static string FormatArray(int[] values)
    {
        if (values.Length <= 10)
        {
            return $"[{string.Join(",", values)}]";
        }

        return $"長度={values.Length}，前五筆=[{string.Join(",", values.Take(5))}]，後五筆=[{string.Join(",", values.TakeLast(5))}]";
    }

    /// <summary>
    /// 使用「依結束時間排序的前綴動態規劃」與二分搜尋計算可取得的最大報酬。
    /// 將每份工作整合後按結束時間升序排列，令 <c>dp[i]</c> 表示前 i 份工作的最佳答案；
    /// 每一步比較略過目前工作與選取目前工作兩種選擇。輸入須為長度相同且非空的合法題目陣列，
    /// 方法不修改傳入陣列，輸出為互不重疊工作集合的最大報酬。
    ///
    /// ref:動態規劃 ＋二分法查找
    /// https://leetcode.cn/problems/maximum-profit-in-job-scheduling/solutions/1910416/gui-hua-jian-zhi-gong-zuo-by-leetcode-so-gu0e/
    /// https://leetcode.cn/problems/maximum-profit-in-job-scheduling/solutions/1913089/dong-tai-gui-hua-er-fen-cha-zhao-you-hua-zkcg/
    /// https://leetcode.cn/problems/maximum-profit-in-job-scheduling/solutions/1913143/by-ac_oier-rgup/
    /// 使用動態規劃解決工作規劃問題
    /// 解題思路：
    /// 1. 將工作按結束時間排序，方便我們進行後續的動態規劃
    /// 2. 使用 dp[i] 表示前 i 個工作能獲得的最大利潤
    /// 3. 對每個工作，我們可以選擇做或不做：
    ///    - 不做：保持前一個狀態的利潤 dp[i-1]
    ///    - 做：當前工作的利潤 + 不衝突工作的最大利潤
    /// 4. 使用二分查找找到不衝突的工作
    /// 5. 返回 dp[n] 即為最大利潤
    /// 
    /// dp[i] = Math.Max(dp[i - 1], dp[k] + jobs[i - 1][2]);
    /// dp[i - 1]: 不做當前工作的最大利潤, 直接繼承前一個狀態的最大利潤
    /// dp[k] + jobs[i - 1][2]: 做當前工作的最大利潤
    /// dp[k]: 前 k 個工作的最大利潤（k 是最後一個不衝突的工作）
    /// jobs[i - 1][2]: 當前工作的利潤（陣列中的第三個元素是 profit）
    /// </summary>
    /// <param name="startTime">每個工作的開始時間陣列</param>
    /// <param name="endTime">每個工作的結束時間陣列</param>
    /// <param name="profit">每個工作的利潤陣列</param>
    /// <returns>能獲得的最大利潤</returns>
    public static int JobScheduling(int[] startTime, int[] endTime, int[] profit)
    {
        int n = startTime.Length;
        int[][] jobs = new int[n][];

        for (int i = 0; i < n; i++)
        {
            jobs[i] = [startTime[i], endTime[i], profit[i]];
        }

        Array.Sort(jobs, (left, right) => left[1].CompareTo(right[1]));

        // dp[0] 是未選工作時的基底；dp[i] 代表前 i 份工作的最佳報酬。
        int[] dp = new int[n + 1];

        for (int i = 1; i <= n; i++)
        {
            int compatibleJobCount = BinarySearch(jobs, i - 1, jobs[i - 1][0]);

            // 不選目前工作沿用 dp[i - 1]；選取時只能接在相容工作的最佳答案之後。
            dp[i] = Math.Max(dp[i - 1], dp[compatibleJobCount] + jobs[i - 1][2]);
        }

        return dp[n];
    }

    /// <summary>
    /// 使用「依開始時間排序的後綴動態規劃」與二分搜尋計算可取得的最大報酬。
    /// 令 <c>dp[i]</c> 表示從第 i 份工作起可取得的最佳答案，從右向左比較略過目前工作，
    /// 或選取目前工作並接上第一份開始時間不早於其結束時間的工作。輸入須為長度相同且非空的
    /// 合法題目陣列，方法不修改傳入陣列，輸出為互不重疊工作集合的最大報酬。
    /// </summary>
    /// <param name="startTime">每份工作的開始時間陣列。</param>
    /// <param name="endTime">每份工作的結束時間陣列。</param>
    /// <param name="profit">每份工作的報酬陣列。</param>
    /// <returns>可取得且工作時間互不重疊的最大報酬。</returns>
    public static int JobScheduling2(int[] startTime, int[] endTime, int[] profit)
    {
        int jobCount = startTime.Length;
        int[][] jobs = new int[jobCount][];

        for (int index = 0; index < jobCount; index++)
        {
            jobs[index] = [startTime[index], endTime[index], profit[index]];
        }

        Array.Sort(jobs, (left, right) => left[0].CompareTo(right[0]));
        int[] dp = new int[jobCount + 1];

        for (int index = jobCount - 1; index >= 0; index--)
        {
            int nextJobIndex = FindNextJobIndex(jobs, index + 1, jobs[index][1]);

            // dp[index + 1] 是略過目前工作；另一項是選取後跳到下一份相容工作。
            dp[index] = Math.Max(dp[index + 1], jobs[index][2] + dp[nextJobIndex]);
        }

        return dp[0];
    }

    /// <summary>
    /// 使用依開始時間排序的掃描線與最小優先佇列計算可取得的最大報酬。
    /// 佇列依工作結束時間排列；掃描到新工作時，先釋放所有已結束工作並更新可銜接的最佳報酬，
    /// 再將「目前工作結束時間、銜接後總報酬」加入佇列。輸入須為長度相同且非空的合法題目陣列，
    /// 方法不修改傳入陣列，輸出為互不重疊工作集合的最大報酬。
    /// </summary>
    /// <param name="startTime">每份工作的開始時間陣列。</param>
    /// <param name="endTime">每份工作的結束時間陣列。</param>
    /// <param name="profit">每份工作的報酬陣列。</param>
    /// <returns>可取得且工作時間互不重疊的最大報酬。</returns>
    public static int JobScheduling3(int[] startTime, int[] endTime, int[] profit)
    {
        int jobCount = startTime.Length;
        int[][] jobs = new int[jobCount][];

        for (int index = 0; index < jobCount; index++)
        {
            jobs[index] = [startTime[index], endTime[index], profit[index]];
        }

        Array.Sort(jobs, (left, right) => left[0].CompareTo(right[0]));

        PriorityQueue<(int EndTime, int TotalProfit), int> activeJobs = new();
        int bestCompletedProfit = 0;

        foreach (int[] job in jobs)
        {
            // 結束時間等於目前開始時間也不重疊，因此可先納入可銜接的最佳報酬。
            while (activeJobs.TryPeek(out _, out int earliestEndTime) && earliestEndTime <= job[0])
            {
                (int _, int completedProfit) = activeJobs.Dequeue();
                bestCompletedProfit = Math.Max(bestCompletedProfit, completedProfit);
            }

            int totalProfit = bestCompletedProfit + job[2];
            activeJobs.Enqueue((job[1], totalProfit), job[1]);
        }

        while (activeJobs.TryDequeue(out (int EndTime, int TotalProfit) completedJob, out _))
        {
            bestCompletedProfit = Math.Max(bestCompletedProfit, completedJob.TotalProfit);
        }

        return bestCompletedProfit;
    }

    /// <summary>
    /// 在依開始時間升序排列的工作中，尋找第一份開始時間大於等於目標結束時間的工作。
    /// 搜尋從指定左邊界開始，輸出可直接作為後綴 DP 索引的位置；若沒有相容工作則回傳陣列長度。
    /// </summary>
    /// <param name="jobs">已按開始時間升序排列的工作陣列。</param>
    /// <param name="left">搜尋範圍的左邊界，包含此位置。</param>
    /// <param name="targetEndTime">目前工作的結束時間。</param>
    /// <returns>第一份相容工作的索引，或工作陣列長度。</returns>
    private static int FindNextJobIndex(int[][] jobs, int left, int targetEndTime)
    {
        int right = jobs.Length;

        while (left < right)
        {
            int middle = left + ((right - left) / 2);

            if (jobs[middle][0] < targetEndTime)
            {
                left = middle + 1;
            }
            else
            {
                right = middle;
            }
        }

        return left;
    }

    /// <summary>
    /// 在依結束時間升序排列的工作前綴中，二分搜尋結束時間小於等於目標開始時間的工作數量。
    /// 搜尋範圍為 <c>[0, right)</c>；回傳值是第一個結束時間大於目標的位置，
    /// 因而同時等於可與目前工作相容的工作數量，可直接作為前綴 DP 索引。
    /// </summary>
    /// <param name="jobs">已按結束時間排序的工作陣列</param>
    /// <param name="right">搜尋範圍不包含在內的右邊界。</param>
    /// <param name="target">目前工作的開始時間。</param>
    /// <returns>相容工作數量，也是前綴 DP 可銜接的索引。</returns>
    public static int BinarySearch(int[][] jobs, int right, int target)
    {
        int left = 0;

        while (left < right)
        {
            int mid = left + ((right - left) / 2);

            if (jobs[mid][1] <= target)
            {
                // 中間工作相容，繼續向右尋找第一個不相容位置。
                left = mid + 1;
            }
            else
            {
                right = mid;
            }
        }

        return left;
    }
}