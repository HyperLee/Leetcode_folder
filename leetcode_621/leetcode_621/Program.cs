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
            // 測試案例1: 基本案例，有冷卻時間
            char[] chars1 = {'A', 'A', 'A', 'B', 'B', 'B'};
            int n1 = 2;
            Console.WriteLine("Test Case 1 - 有冷卻時間:");
            Console.WriteLine($"Input: tasks = [{string.Join(",", chars1)}], n = {n1}");
            Console.WriteLine($"Output: {LeastInterval(chars1, n1)}\n");

            // 測試案例2: 任務種類多，幾乎不需要冷卻時間
            char[] chars2 = {'A', 'B', 'C', 'D', 'E', 'A', 'B', 'C', 'D', 'E'};
            int n2 = 1;
            Console.WriteLine("Test Case 2 - 任務種類多:");
            Console.WriteLine($"Input: tasks = [{string.Join(",", chars2)}], n = {n2}");
            Console.WriteLine($"Output: {LeastInterval(chars2, n2)}\n");

            // 測試案例3: 單一任務重複多次
            char[] chars3 = {'A', 'A', 'A', 'A'};
            int n3 = 2;
            Console.WriteLine("Test Case 3 - 單一任務重複:");
            Console.WriteLine($"Input: tasks = [{string.Join(",", chars3)}], n = {n3}");
            Console.WriteLine($"Output: {LeastInterval(chars3, n3)}");
        }

        /// <summary>
        /// 參考:
        /// https://leetcode.cn/problems/task-scheduler/solutions/2020840/by-stormsunshine-hxv6/
        /// https://leetcode.cn/problems/task-scheduler/solutions/510292/c-jie-fa-by-jian-wei-z-km3w/
        /// https://leetcode.cn/problems/task-scheduler/solutions/509687/ren-wu-diao-du-qi-by-leetcode-solution-ur9w/
        /// https://leetcode.cn/problems/task-scheduler/solutions/1924711/by-ac_oier-3560/
        /// 
        /// 須注意
        /// case 1:
        /// 任務種類較少(輸入的英文字母種類少), 要插入任務需要的時間(理解為要增加間隔或是cd時間)
        /// 即為公式計算 (n + 1) * (maxcount - 1) + maxtasks;
        /// 
        /// (n + 1) * (maxcount - 1) 可計算出不包含冷卻時間光char輪流插入所需耗時
        /// 再加上 maxtasks(可視為冷卻時間 or 任務種類次數) 即為總需要時間
        /// 
        /// case 2:
        /// 任務種類較多(輸入的英文字母種類多), 不太會出現冷卻時間可以插入情況(不需要等待間隔或是cd時間), 所以就要改計算任務次數
        /// 
        /// 
        /// 任務調度器解題思路：
        /// 1. 核心概念是找出執行次數最多的任務(maxcount)和相同最多次數的任務數量(maxtasks)
        /// 2. 計算方式分為兩種情況：
        ///    - 當任務種類少時：需考慮冷卻時間，使用公式 (n + 1) * (maxcount - 1) + maxtasks
        ///    - 當任務種類多時：實際執行時間就是任務總數，即 tasks.Length
        /// 3. 最後取兩者較大值作為答案
        /// 
        /// 時間複雜度：O(N)，其中 N 為任務總數
        /// 空間複雜度：O(1)，使用固定大小的數組存儲計數
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int LeastInterval(char[] tasks, int n)
        {
            // 步驟1: 初始化變數
            int maxcount = 0;              // 記錄最多的任務出現次數
            int[] counts = new int[26];    // 用於統計每個任務的出現次數

            // 步驟2: 統計每個任務出現"次數"並找出"最大值"
            foreach (char c in tasks) 
            {
                counts[c - 'A']++;         // 將字母轉換為索引並計數
                maxcount = Math.Max(maxcount, counts[c - 'A']);
            }

            // 步驟3: 計算具有最大出現"次數"的任務數量
            int maxtasks = 0;
            foreach (int count in counts)
            {
                if(count == maxcount)
                {
                    maxtasks++;  // 累計具有最大出現次數的任務種類數
                }
            }

            // 步驟4: 計算總需要時間
            // (n + 1): 表示每輪任務間隔
            // (maxcount - 1): 表示需要的輪數
            // maxtasks: 最後一輪的任務數
            int total = (n + 1) * (maxcount - 1) + maxtasks;

            // 步驟5: 返回實際所需的最少時間
            // 比較公式計算結果與任務總數，取較大值
            // 步驟5以前都是在處理 case1 情況.
            return Math.Max(total, tasks.Length);
        }
    }
}
