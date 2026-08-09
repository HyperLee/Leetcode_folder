namespace leetcode_621
{
    internal class Program
    {
        /// <summary>
        /// 621. Task Scheduler
        /// https://leetcode.com/problems/task-scheduler/description/
        /// <para>
        /// You are given an array of CPU tasks, each represented by letters A to Z, and a cooling time n. Each cycle or interval allows the completion of one task. Tasks can be completed in any order, but there is a constraint: identical tasks must be separated by at least n intervals due to cooling time.
        ///
        /// Return the minimum number of intervals required to complete all tasks.
        ///
        /// Example 1:
        /// Input: tasks = ["A","A","A","B","B","B"], n = 2
        /// Output: 8
        /// Explanation: A possible sequence is: A -&gt; B -&gt; idle -&gt; A -&gt; B -&gt; idle -&gt; A -&gt; B.
        /// After completing task A, you must wait two intervals before doing A again. The same applies to task B. In the 3rd interval, neither A nor B can be done, so you idle. By the 4th interval, you can do A again as two intervals have passed.
        ///
        /// Example 2:
        /// Input: tasks = ["A","C","A","B","D","B"], n = 1
        /// Output: 6
        /// Explanation: A possible sequence is: A -&gt; B -&gt; C -&gt; D -&gt; A -&gt; B.
        /// With a cooling interval of 1, you can repeat a task after just one other task.
        ///
        /// Example 3:
        /// Input: tasks = ["A","A","A","B","B","B"], n = 3
        /// Output: 10
        /// Explanation: A possible sequence is: A -&gt; B -&gt; idle -&gt; idle -&gt; A -&gt; B -&gt; idle -&gt; idle -&gt; A -&gt; B.
        /// There are only two types of tasks, A and B, which need to be separated by three intervals. This leads to idling twice between repetitions of these tasks.
        ///
        /// Constraints:
        /// - 1 &lt;= tasks.length &lt;= 10^4
        /// - tasks[i] is an uppercase English letter.
        /// - 0 &lt;= n &lt;= 100
        /// </para>
        /// <para>
        /// 621. 任務排程器
        /// https://leetcode.cn/problems/task-scheduler/description/
        ///
        /// 給定一組 CPU 任務陣列，每個任務以 A 到 Z 的字母表示，並給定冷卻時間 n。每個週期或時間區間可完成一項任務。任務能以任意順序完成，但相同任務因冷卻時間限制，彼此之間必須至少相隔 n 個區間。
        ///
        /// 回傳完成所有任務所需的最少區間數。
        ///
        /// 範例 1：
        /// 輸入：tasks = ["A","A","A","B","B","B"], n = 2
        /// 輸出：8
        /// 解釋：一種可能的順序是：A -&gt; B -&gt; 閒置 -&gt; A -&gt; B -&gt; 閒置 -&gt; A -&gt; B。
        /// 完成任務 A 後，必須等待兩個區間才能再次執行 A；任務 B 亦同。第 3 個區間無法執行 A 或 B，因此必須閒置。到了第 4 個區間，因為已經過兩個區間，所以可以再次執行 A。
        ///
        /// 範例 2：
        /// 輸入：tasks = ["A","C","A","B","D","B"], n = 1
        /// 輸出：6
        /// 解釋：一種可能的順序是：A -&gt; B -&gt; C -&gt; D -&gt; A -&gt; B。
        /// 冷卻區間為 1，因此只要隔著另一項任務，就能重複執行同一任務。
        ///
        /// 範例 3：
        /// 輸入：tasks = ["A","A","A","B","B","B"], n = 3
        /// 輸出：10
        /// 解釋：一種可能的順序是：A -&gt; B -&gt; 閒置 -&gt; 閒置 -&gt; A -&gt; B -&gt; 閒置 -&gt; 閒置 -&gt; A -&gt; B。
        /// 只有 A 與 B 兩種任務，且相同任務必須相隔三個區間，因此每次重複這些任務前都需閒置兩次。
        ///
        /// 限制條件：
        /// - 1 &lt;= tasks.length &lt;= 10^4
        /// - tasks[i] 是大寫英文字母。
        /// - 0 &lt;= n &lt;= 100
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SampleCase[] sampleCases =
            [
                new("官方冷卻案例", ['A', 'A', 'A', 'B', 'B', 'B'], 2, 8),
                new("任務種類足以填滿間隔", ['A', 'B', 'C', 'D', 'E', 'A', 'B', 'C', 'D', 'E'], 1, 10),
                new("單一任務重複", ['A', 'A', 'A', 'A'], 2, 10),
                new("單一任務搭配大冷卻值", ['A'], 100, 1),
                new("無冷卻時間", ['A', 'A', 'B', 'B'], 0, 4),
                new("多個最高頻任務剛好填滿排程", ['A', 'A', 'A', 'B', 'B', 'B', 'C', 'C'], 2, 8)
            ];

            int passedCount = 0;

            for (int index = 0; index < sampleCases.Length; index++)
            {
                SampleCase sampleCase = sampleCases[index];
                int actual = LeastInterval(sampleCase.Tasks, sampleCase.Cooldown);
                bool isPassed = actual == sampleCase.Expected;

                if (isPassed)
                {
                    passedCount++;
                }

                Console.WriteLine($"案例 {index + 1}：{sampleCase.Name}");
                Console.WriteLine($"Input: tasks = [{string.Join(", ", sampleCase.Tasks)}], n = {sampleCase.Cooldown}");
                Console.WriteLine($"Expected: {sampleCase.Expected}");
                Console.WriteLine($"Actual: {actual}");
                Console.WriteLine($"Result: {(isPassed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedCount}/{sampleCases.Length} 筆測試通過");

            if (passedCount != sampleCases.Length)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 計算完成所有任務所需的最少單位時間。
        /// 先以固定 26 格陣列統計各任務頻率，再由最高頻率建立包含冷卻間隔的排程框架，
        /// 最後取框架長度與任務總數的較大值，避免任務種類充足時低估實際執行時間。
        /// </summary>
        /// <param name="tasks">
        /// 非 null 且長度介於 1 到 10,000 的任務陣列；每個元素皆為大寫英文字母 A 到 Z。
        /// </param>
        /// <param name="n">相同任務之間的冷卻時間，範圍為 0 到 100。</param>
        /// <returns>完成全部任務所需的最少單位時間，包含必要的閒置時間。</returns>
        /// <remarks>
        /// 時間複雜度為 O(N)，其中 N 為任務數量；額外空間複雜度為 O(1)。
        /// </remarks>
        public static int LeastInterval(char[] tasks, int n)
        {
            int maxCount = 0;
            int[] counts = new int[26];

            // 統計各任務頻率時同步保存最高頻率，後續只需再掃描固定大小的計數陣列。
            foreach (char task in tasks)
            {
                counts[task - 'A']++;
                maxCount = Math.Max(maxCount, counts[task - 'A']);
            }

            int maxFrequencyTaskCount = 0;
            foreach (int count in counts)
            {
                if (count == maxCount)
                {
                    maxFrequencyTaskCount++;
                }
            }

            // 前 maxCount - 1 輪各占 n + 1 格，最後一輪只需放入所有最高頻任務。
            int scheduleFrameLength = (n + 1) * (maxCount - 1) + maxFrequencyTaskCount;

            // 冷卻框架與任務總數都是答案下界；任務種類充足時由 tasks.Length 主導。
            return Math.Max(scheduleFrameLength, tasks.Length);
        }

        /// <summary>
        /// 表示一筆可執行範例，包含案例名稱、合法任務陣列、冷卻時間與預期最少單位時間。
        /// </summary>
        private sealed record SampleCase(string Name, char[] Tasks, int Cooldown, int Expected);
    }
}
