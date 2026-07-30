namespace leetcode_752
{
    internal class Program
    {
        /// <summary>
        /// 752. Open the Lock
        /// https://leetcode.com/problems/open-the-lock/description/?envType=daily-question&envId=2024-04-22
        /// 752. 打开转盘锁
        /// https://leetcode.cn/problems/open-the-lock/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行固定範例，分別呼叫單向與雙向廣度優先搜尋，並將預期值、實際值與驗證結果輸出到主控台。
        /// 測試資料皆符合四位數字字串與死路清單的題目輸入條件；本方法不接受輸入，也不回傳結果。
        /// </summary>
        private static void RunSamples()
        {
            SampleCase[] samples =
            [
                new(
                    "官方一般案例",
                    ["0201", "0101", "0102", "1212", "2002"],
                    "0202",
                    6),
                new(
                    "環繞旋轉一步",
                    ["8888"],
                    "0009",
                    1),
                new(
                    "目標四周皆為死路",
                    ["8887", "8889", "8878", "8898", "8788", "8988", "7888", "9888"],
                    "8888",
                    -1),
                new(
                    "起點即為死路",
                    ["0000"],
                    "8888",
                    -1),
                new(
                    "目標就是起點",
                    ["8888"],
                    "0000",
                    0),
                new(
                    "最遠轉輪距離",
                    ["9999"],
                    "5555",
                    20)
            ];

            int passedChecks = 0;
            int totalChecks = samples.Length * 2;

            Console.WriteLine("LeetCode 752 - Open the Lock");
            Console.WriteLine();

            for (int i = 0; i < samples.Length; i++)
            {
                SampleCase sample = samples[i];
                int singleBfsResult = OpenLock(sample.Deadends, sample.Target);
                int bidirectionalBfsResult = OpenLock2(sample.Deadends, sample.Target);
                bool singleBfsPassed = singleBfsResult == sample.Expected;
                bool bidirectionalBfsPassed = bidirectionalBfsResult == sample.Expected;

                if (singleBfsPassed)
                {
                    passedChecks++;
                }

                if (bidirectionalBfsPassed)
                {
                    passedChecks++;
                }

                Console.WriteLine($"案例 {i + 1}：{sample.Name}");
                Console.WriteLine($"deadends = [{string.Join(", ", sample.Deadends.Select(value => $"\"{value}\""))}]");
                Console.WriteLine($"target = \"{sample.Target}\"");
                Console.WriteLine($"預期：{sample.Expected}");
                Console.WriteLine($"單向 BFS：{singleBfsResult} => {(singleBfsPassed ? "PASS" : "FAIL")}");
                Console.WriteLine($"雙向 BFS：{bidirectionalBfsResult} => {(bidirectionalBfsPassed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
        }

        private sealed record SampleCase(
            string Name,
            string[] Deadends,
            string Target,
            int Expected);

        /// <summary>
        /// 使用單向廣度優先搜尋，從初始狀態 <c>0000</c> 逐層探索所有可旋轉到的狀態。
        /// 輸入的死路與目標皆須為四位數字字串，且目標不在死路清單中。
        /// 因為每一層代表一次旋轉，首次找到目標時即回傳最少旋轉次數；無法到達則回傳 <c>-1</c>。
        /// </summary>
        /// <param name="deadends">不可進入的四位數字狀態。</param>
        /// <param name="target">要解鎖的四位數字狀態。</param>
        /// <returns>從 <c>0000</c> 到目標的最少旋轉次數；無法到達時為 <c>-1</c>。</returns>
        public static int OpenLock(string[] deadends, string target)
        {
            const string start = "0000";
            HashSet<string> dead = new(deadends);

            if (dead.Contains(start))
            {
                return -1;
            }

            if (target == start)
            {
                return 0;
            }

            int step = 0;
            Queue<string> queue = new();
            HashSet<string> seen = [start];
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                // 先固定本層大小，確保 step 每次只增加一個旋轉距離。
                int levelSize = queue.Count;

                for (int i = 0; i < levelSize; i++)
                {
                    string status = queue.Dequeue();

                    foreach (string nextStatus in Get(status))
                    {
                        // 死路不能進入；已拜訪狀態也不必再次加入佇列。
                        if (dead.Contains(nextStatus) || !seen.Add(nextStatus))
                        {
                            continue;
                        }

                        if (nextStatus == target)
                        {
                            return step + 1;
                        }

                        queue.Enqueue(nextStatus);
                    }
                }

                step++;
            }

            return -1;
        }

        /// <summary>
        /// 使用雙向廣度優先搜尋，分別從 <c>0000</c> 與目標建立搜尋邊界，並優先擴展狀態數較少的一側。
        /// 輸入的死路與目標皆須為四位數字字串，且目標不在死路清單中。
        /// 兩側邊界首次相遇時回傳最少旋轉次數；若任一邊界耗盡仍未相遇則回傳 <c>-1</c>。
        /// </summary>
        /// <param name="deadends">不可進入的四位數字狀態。</param>
        /// <param name="target">要解鎖的四位數字狀態。</param>
        /// <returns>從 <c>0000</c> 到目標的最少旋轉次數；無法到達時為 <c>-1</c>。</returns>
        public static int OpenLock2(string[] deadends, string target)
        {
            const string start = "0000";
            HashSet<string> dead = new(deadends);

            if (dead.Contains(start))
            {
                return -1;
            }

            if (target == start)
            {
                return 0;
            }

            HashSet<string> frontier = [start];
            HashSet<string> oppositeFrontier = [target];
            HashSet<string> seen = [start];
            int step = 0;

            while (frontier.Count > 0 && oppositeFrontier.Count > 0)
            {
                // 每輪擴展較小的邊界，通常可減少需要展開的狀態數。
                if (frontier.Count > oppositeFrontier.Count)
                {
                    (frontier, oppositeFrontier) = (oppositeFrontier, frontier);
                }

                HashSet<string> nextFrontier = [];

                foreach (string status in frontier)
                {
                    foreach (string nextStatus in Get(status))
                    {
                        if (oppositeFrontier.Contains(nextStatus))
                        {
                            return step + 1;
                        }

                        if (dead.Contains(nextStatus) || !seen.Add(nextStatus))
                        {
                            continue;
                        }

                        nextFrontier.Add(nextStatus);
                    }
                }

                frontier = nextFrontier;
                step++;
            }

            return -1;
        }

        /// <summary>
        /// 取得轉輪數字往前一格的結果；輸入須為 <c>'0'</c> 到 <c>'9'</c>。
        /// 一般數字減一，<c>'0'</c> 則環繞為 <c>'9'</c>，並回傳旋轉後的字元。
        /// </summary>
        /// <param name="x">目前轉輪上的數字字元。</param>
        /// <returns>往前旋轉一格後的數字字元。</returns>
        public static char NumtPre(char x)
        {
            return x == '0' ? '9' : (char)(x - 1);
        }

        /// <summary>
        /// 取得轉輪數字往後一格的結果；輸入須為 <c>'0'</c> 到 <c>'9'</c>。
        /// 一般數字加一，<c>'9'</c> 則環繞為 <c>'0'</c>，並回傳旋轉後的字元。
        /// </summary>
        /// <param name="x">目前轉輪上的數字字元。</param>
        /// <returns>往後旋轉一格後的數字字元。</returns>
        public static char NumSucc(char x)
        {
            return x == '9' ? '0' : (char)(x + 1);
        }

        /// <summary>
        /// 列舉四位轉盤狀態旋轉一次可以到達的所有相鄰狀態。
        /// 輸入須為恰好四位的數字字串；每一位各產生往前與往後兩種結果，合計回傳八個狀態。
        /// </summary>
        /// <param name="status">目前的四位數字狀態。</param>
        /// <returns>旋轉任一轉輪一格後可到達的八個四位數字狀態。</returns>
        public static IList<string> Get(string status)
        {
            IList<string> result = new List<string>(8);
            char[] array = status.ToCharArray();

            for (int i = 0; i < 4; i++)
            {
                char originalDigit = array[i];
                array[i] = NumtPre(originalDigit);
                result.Add(new string(array));
                array[i] = NumSucc(originalDigit);
                result.Add(new string(array));

                // 下一個位置必須從原狀態開始，避免前一個轉輪的修改累積進結果。
                array[i] = originalDigit;
            }

            return result;
        }
    }
}
