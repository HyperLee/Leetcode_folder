namespace leetcode_621
{
    internal class Program
    {
        /// <summary>
        /// 621. Task Scheduler
        /// https://leetcode.com/problems/task-scheduler/description/?envType=daily-question&envId=2024-03-19
        /// 621. 任务调度器
        /// https://leetcode.cn/problems/task-scheduler/description/
        /// 
        /// 題目說明:
        /// 給定一個字符陣列 tasks，表示需要執行的任務順序，其中每個字母代表不同種類的任務。
        /// 任務可以以任意順序執行，且每個任務都可以在 1 個單位時間內完成。
        /// 每個任務之間必須至少間隔 n 個單位時間。
        /// 
        /// 關鍵條件:
        /// 1. 相同任務必須間隔 n 個單位時間
        /// 2. 不同任務之間可以立即執行
        /// 3. 任務執行順序可調整
        /// 
        /// 限制條件:
        /// - 1 <= task.length <= 10^4
        /// - tasks[i] 是大寫英文字母
        /// - n 的範圍是 [0, 100]
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
