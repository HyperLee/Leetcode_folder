namespace leetcode_2582
{
    internal class Program
    {
        /// <summary>
        /// 2582. Pass the Pillow
        /// https://leetcode.com/problems/pass-the-pillow/description/
        /// <para>
        /// There are n people standing in a line labeled from 1 to n. The first person in the line is holding a pillow initially. Every second, the person holding the pillow passes it to the next person standing in the line. Once the pillow reaches the end of the line, the direction changes, and people continue passing the pillow in the opposite direction.
        ///
        /// For example, once the pillow reaches the n-th person, they pass it to the (n - 1)-th person, then to the (n - 2)-th person, and so on.
        ///
        /// Given the two positive integers n and time, return the index of the person holding the pillow after time seconds.
        ///
        /// Example 1:
        /// Input: n = 4, time = 5
        /// Output: 2
        /// Explanation: People pass the pillow as follows: 1 -&gt; 2 -&gt; 3 -&gt; 4 -&gt; 3 -&gt; 2. After five seconds, the 2nd person is holding the pillow.
        ///
        /// Example 2:
        /// Input: n = 3, time = 2
        /// Output: 3
        /// Explanation: People pass the pillow as follows: 1 -&gt; 2 -&gt; 3. After two seconds, the 3rd person is holding the pillow.
        ///
        /// Constraints:
        /// - 2 &lt;= n &lt;= 1000
        /// - 1 &lt;= time &lt;= 1000
        ///
        /// Note: This question is the same as 3178: Find the Child Who Has the Ball After K Seconds.
        /// </para>
        /// <para>
        /// 2582. 傳遞枕頭
        /// https://leetcode.cn/problems/pass-the-pillow/description/
        ///
        /// 有 n 個人站成一列，編號從 1 到 n。起初隊伍中的第一個人拿著枕頭。每一秒，拿著枕頭的人會把枕頭傳給隊伍中的下一個人。當枕頭到達隊伍末端時，傳遞方向會反轉，大家繼續朝相反方向傳遞枕頭。
        ///
        /// 例如，枕頭到達第 n 個人後，他會把枕頭傳給第 n - 1 個人，接著傳給第 n - 2 個人，依此類推。
        ///
        /// 給定兩個正整數 n 和 time，回傳 time 秒後拿著枕頭之人的編號。
        ///
        /// 範例 1：
        /// 輸入：n = 4, time = 5
        /// 輸出：2
        /// 解釋：枕頭的傳遞順序為：1 -&gt; 2 -&gt; 3 -&gt; 4 -&gt; 3 -&gt; 2。五秒後，第 2 個人拿著枕頭。
        ///
        /// 範例 2：
        /// 輸入：n = 3, time = 2
        /// 輸出：3
        /// 解釋：枕頭的傳遞順序為：1 -&gt; 2 -&gt; 3。兩秒後，第 3 個人拿著枕頭。
        ///
        /// 限制條件：
        /// - 2 &lt;= n &lt;= 1000
        /// - 1 &lt;= time &lt;= 1000
        ///
        /// 注意：本題與 3178「K 秒後拿著球的孩子」相同。
        /// </para>
        /// </summary>
        /// <remarks>
        /// 以固定案例執行三種解法，逐一比較預期值與實際值；全部案例通過時回傳 0，否則回傳非零結束碼。
        /// </remarks>
        /// <param name="args">命令列參數；本程式使用固定案例，不讀取外部輸入。</param>
        /// <returns>所有驗證通過時回傳 0，任一驗證失敗時回傳 1。</returns>
        static int Main(string[] args)
        {
            return RunSamples();
        }

        /// <summary>
        /// 建立符合題目限制的固定案例，執行三種解法並統計驗證結果。
        /// </summary>
        /// <returns>全部驗證通過時回傳 0，否則回傳 1。</returns>
        private static int RunSamples()
        {
            SampleCase[] samples =
            {
                new("官方範例一", 4, 5, 2),
                new("官方範例二", 3, 2, 3),
                new("最少人數第一秒", 2, 1, 2),
                new("最少人數完整多輪", 2, 1000, 1),
                new("抵達右端點", 5, 4, 5),
                new("折返後第一秒", 5, 5, 4),
                new("完整週期回到起點", 4, 6, 1),
                new("最大人數抵達右端點", 1000, 999, 1000),
                new("限制上限折返", 1000, 1000, 999)
            };

            int passedChecks = 0;
            foreach (SampleCase sample in samples)
            {
                passedChecks += RunCase(sample);
            }

            int totalChecks = samples.Length * 3;
            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
            return passedChecks == totalChecks ? 0 : 1;
        }

        /// <summary>
        /// 執行單一案例，輸出三種解法的實際結果並計算通過數量。
        /// </summary>
        /// <param name="sample">包含人數、傳遞秒數與預期持有人編號的測試案例。</param>
        /// <returns>本案例通過的解法驗證數量，範圍為 0 到 3。</returns>
        private static int RunCase(SampleCase sample)
        {
            Console.WriteLine($"案例：{sample.Name}");
            Console.WriteLine($"n = {sample.N}, time = {sample.Time}");
            Console.WriteLine($"預期 = {sample.Expected}");

            (string Name, int Actual)[] results =
            {
                ("PassThePillow", PassThePillow(sample.N, sample.Time)),
                ("PassThePillow2", PassThePillow2(sample.N, sample.Time)),
                ("PassThePillow3", PassThePillow3(sample.N, sample.Time))
            };

            int passedChecks = 0;
            foreach ((string name, int actual) in results)
            {
                bool passed = actual == sample.Expected;
                if (passed)
                {
                    passedChecks++;
                }

                Console.WriteLine($"{name,-16} 實際 = {actual} => {(passed ? "PASS" : "FAIL")}");
            }

            Console.WriteLine();
            return passedChecks;
        }

        /// <summary>
        /// 描述單一固定案例的輸入與預期輸出。
        /// </summary>
        /// <param name="Name">測試案例名稱。</param>
        /// <param name="N">排成直線的人數。</param>
        /// <param name="Time">枕頭傳遞的秒數。</param>
        /// <param name="Expected">傳遞結束後持有枕頭的人員編號。</param>
        private sealed record SampleCase(string Name, int N, int Time, int Expected);


        /// <summary>
        /// 利用傳遞路徑的固定往返週期，以 O(1) 時間找出最後持有枕頭的人員編號。
        /// 題目輸入為至少 2 人與至少 1 秒的有效整數；每經過完整週期後，位置會回到 1 號。
        /// </summary>
        /// <param name="n">排成直線的人數，限制為 2 到 1000。</param>
        /// <param name="time">枕頭傳遞的秒數，限制為 1 到 1000。</param>
        /// <returns>經過指定秒數後持有枕頭的人員編號，範圍為 1 到 n。</returns>
        /// <remarks>
        /// 完整往返一次需要從 1 走到 n，再從 n 走回 2，共 2 * (n - 1) 秒。
        /// 先將 time 取週期餘數，再判斷目前位於去程或回程即可。
        /// 參考：
        /// https://leetcode.cn/problems/pass-the-pillow/solutions/2451117/di-zhen-tou-by-leetcode-solution-kl5e/
        /// https://leetcode.cn/problems/pass-the-pillow/solutions/2148332/o1-gong-shi-by-endlesscheng-z4xz/
        /// https://leetcode.cn/problems/pass-the-pillow/solutions/2606914/2582-di-zhen-tou-by-stormsunshine-t5fl/
        /// 時間複雜度為 O(1)，額外空間複雜度為 O(1)。
        /// </remarks>
        public static int PassThePillow(int n, int time)
        {
            int cycleLength = 2 * (n - 1);
            int position = time % cycleLength;

            // 去程的餘數 0 到 n - 1 對應人員 1 到 n；其餘位置則沿回程遞減。
            return position < n
                ? position + 1
                : n - (position - (n - 1));
        }

        /// <summary>
        /// 從 1 號開始逐秒模擬枕頭傳遞與方向反轉，找出指定時間後的持有人。
        /// 題目輸入為至少 2 人與至少 1 秒的有效整數，輸出為 1 到 n 之間的人員編號。
        /// </summary>
        /// <param name="n">排成直線的人數，限制為 2 到 1000。</param>
        /// <param name="time">枕頭傳遞的秒數，限制為 1 到 1000。</param>
        /// <returns>經過指定秒數後持有枕頭的人員編號。</returns>
        /// <remarks>
        /// 每次傳遞先依目前方向移動一人；抵達 1 號或 n 號後，下一秒必須改變方向。
        /// 時間複雜度為 O(time)，額外空間複雜度為 O(1)。
        /// </remarks>
        public static int PassThePillow2(int n, int time)
        {
            int currentPerson = 1;
            int direction = 1;

            for (int second = 0; second < time; second++)
            {
                currentPerson += direction;

                // 端點沒有下一位可傳遞，因此下一秒要往相反方向移動。
                if (currentPerson == 1 || currentPerson == n)
                {
                    direction *= -1;
                }
            }

            return currentPerson;
        }

        /// <summary>
        /// 建立一輪完整往返路徑，再以週期索引找出指定時間後的持有人。
        /// 題目輸入為至少 2 人與至少 1 秒的有效整數，輸出為 1 到 n 之間的人員編號。
        /// </summary>
        /// <param name="n">排成直線的人數，限制為 2 到 1000。</param>
        /// <param name="time">枕頭傳遞的秒數，限制為 1 到 1000。</param>
        /// <returns>經過指定秒數後持有枕頭的人員編號。</returns>
        /// <remarks>
        /// 一輪路徑為 [1, 2, ..., n, n - 1, ..., 2]，長度為 2 * (n - 1)。
        /// 這個版本將週期具體化成陣列，適合展示週期觀察，但需要 O(n) 時間與 O(n) 額外空間。
        /// </remarks>
        public static int PassThePillow3(int n, int time)
        {
            int cycleLength = 2 * (n - 1);
            int[] cycle = new int[cycleLength];

            for (int index = 0; index < n; index++)
            {
                cycle[index] = index + 1;
            }

            for (int index = n; index < cycleLength; index++)
            {
                // 回程不重複放入 n，依序填入 n - 1 到 2。
                cycle[index] = cycleLength - index + 1;
            }

            return cycle[time % cycleLength];
        }
    }
}