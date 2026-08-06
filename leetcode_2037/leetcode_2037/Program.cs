namespace leetcode_2037
{
    internal class Program
    {
        /// <summary>
        /// 2037. Minimum Number of Moves to Seat Everyone
        /// https://leetcode.com/problems/minimum-number-of-moves-to-seat-everyone/description/?envType=daily-question&envId=2024-06-13
        /// 2037. 使每位学生都有座位的最少移动次数
        /// https://leetcode.cn/problems/minimum-number-of-moves-to-seat-everyone/description/
        /// </summary>
        /// <remarks>
        /// 主要進入點會執行六組固定案例，比較排序貪婪與計數雙指標兩種解法，
        /// 並以 Expected、Actual 與 PASS/FAIL 顯示驗證結果。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用。</param>
        private static void Main(string[] args)
        {
            bool allPassed = RunSamples();
            Environment.ExitCode = allPassed ? 0 : 1;
        }

        /// <summary>
        /// 執行六組符合題目限制的固定案例，分別驗證排序貪婪與計數雙指標解法。
        /// 每個解法都取得獨立的輸入副本，避免排序造成的修改影響另一個解法。
        /// </summary>
        /// <returns>十二項答案檢查全部通過時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
        private static bool RunSamples()
        {
            (string Name, int[] Seats, int[] Students, int Expected)[] cases =
            {
                ("1. 官方範例一", new[] { 3, 1, 5 }, new[] { 2, 7, 4 }, 4),
                ("2. 官方範例二", new[] { 4, 1, 5, 9 }, new[] { 1, 3, 2, 6 }, 7),
                ("3. 官方重複位置範例", new[] { 2, 2, 6, 6 }, new[] { 1, 3, 2, 6 }, 4),
                ("4. 最小輸入", new[] { 1 }, new[] { 1 }, 0),
                ("5. 已配對但順序不同", new[] { 2, 1, 2 }, new[] { 2, 2, 1 }, 0),
                ("6. 重複值與位置上下界", new[] { 1, 1, 1 }, new[] { 100, 100, 100 }, 297)
            };

            int passedChecks = 0;
            const int checksPerCase = 2;
            int totalChecks = cases.Length * checksPerCase;

            foreach ((string name, int[] seats, int[] students, int expected) in cases)
            {
                passedChecks += RunCase(name, seats, students, expected);
            }

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項測試通過");
            return passedChecks == totalChecks;
        }

        /// <summary>
        /// 將單一案例交給兩種解法，顯示原始輸入、預期答案、實際答案與驗證結果。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="seats">座位位置。</param>
        /// <param name="students">學生位置。</param>
        /// <param name="expected">人工推導的預期最少移動次數。</param>
        /// <returns>本案例通過的解法數量，範圍為零到二。</returns>
        private static int RunCase(string name, int[] seats, int[] students, int expected)
        {
            Console.WriteLine($"案例：{name}");
            Console.WriteLine($"Input：seats = {FormatArray(seats)}, students = {FormatArray(students)}");

            int sortingActual = MinMovesToSeat((int[])seats.Clone(), (int[])students.Clone());
            int countingActual = MinMovesToSeat2((int[])seats.Clone(), (int[])students.Clone());
            bool sortingPassed = sortingActual == expected;
            bool countingPassed = countingActual == expected;

            Console.WriteLine("解法一：MinMovesToSeat（排序貪婪）");
            Console.WriteLine($"Expected：{expected}");
            Console.WriteLine($"Actual：{sortingActual}");
            Console.WriteLine($"Result：{(sortingPassed ? "PASS" : "FAIL")}");
            Console.WriteLine("解法二：MinMovesToSeat2（計數雙指標）");
            Console.WriteLine($"Expected：{expected}");
            Console.WriteLine($"Actual：{countingActual}");
            Console.WriteLine($"Result：{(countingPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return (sortingPassed ? 1 : 0) + (countingPassed ? 1 : 0);
        }

        /// <summary>
        /// 將整數陣列格式化為穩定的方括號字串，供測試輸出與 README 範例使用。
        /// </summary>
        /// <param name="values">要格式化的整數陣列。</param>
        /// <returns>格式為 <c>[value1, value2, ...]</c> 的字串。</returns>
        private static string FormatArray(int[] values)
        {
            return $"[{string.Join(", ", values)}]";
        }

        /// <summary>
        /// 將座位與學生位置分別原地排序，再把第 <c>i</c> 位學生配給第 <c>i</c> 個座位，
        /// 以避免配對路徑交叉並得到最少移動次數。輸入需符合題目限制：兩陣列長度相同且非空，
        /// 每個位置介於 1 到 100。方法會修改兩個輸入陣列，回傳所有配對距離的總和；
        /// 時間複雜度為 O(n log n)，排序所需額外空間依執行環境而定。
        /// </summary>
        /// <remarks>
        /// 參考：
        /// https://leetcode.cn/problems/minimum-number-of-moves-to-seat-everyone/solutions/2037615/shi-mei-wei-xue-sheng-du-you-zuo-wei-de-oll4i/
        /// https://leetcode.cn/problems/minimum-number-of-moves-to-seat-everyone/solutions/2625721/2037-shi-mei-wei-xue-sheng-du-you-zuo-we-g2ib/
        /// </remarks>
        /// <param name="seats">要原地排序的座位位置陣列。</param>
        /// <param name="students">要原地排序的學生位置陣列。</param>
        /// <returns>讓每位學生各自坐到一個座位所需的最少移動次數。</returns>
        public static int MinMovesToSeat(int[] seats, int[] students)
        {
            Array.Sort(seats);
            Array.Sort(students);

            int moves = 0;
            for (int i = 0; i < seats.Length; i++)
            {
                // 排序後依序配對可消除交叉路徑；交換任何交叉配對都不會增加總距離。
                moves += Math.Abs(seats[i] - students[i]);
            }

            return moves;
        }

        /// <summary>
        /// 統計位置 1 到 100 各有多少座位與學生，再以兩個位置指標由小到大批次配對。
        /// 輸入需符合題目限制：兩陣列長度相同且非空，每個位置介於 1 到 100。
        /// 方法不修改輸入陣列，回傳所有配對距離的最小總和；若位置範圍大小為 k，
        /// 時間複雜度為 O(n+k)，額外空間為 O(k)。
        /// </summary>
        /// <param name="seats">座位位置陣列。</param>
        /// <param name="students">學生位置陣列。</param>
        /// <returns>讓每位學生各自坐到一個座位所需的最少移動次數。</returns>
        public static int MinMovesToSeat2(int[] seats, int[] students)
        {
            const int maximumPosition = 100;
            int[] seatCounts = new int[maximumPosition + 1];
            int[] studentCounts = new int[maximumPosition + 1];

            foreach (int seat in seats)
            {
                seatCounts[seat]++;
            }

            foreach (int student in students)
            {
                studentCounts[student]++;
            }

            int seatPosition = 1;
            int studentPosition = 1;
            int remainingPairs = seats.Length;
            int moves = 0;

            while (remainingPairs > 0)
            {
                while (seatCounts[seatPosition] == 0)
                {
                    seatPosition++;
                }

                while (studentCounts[studentPosition] == 0)
                {
                    studentPosition++;
                }

                // 一次配對兩個目前最小位置的可用數量，等價於排序後逐項配對。
                int matchedCount = Math.Min(seatCounts[seatPosition], studentCounts[studentPosition]);
                moves += matchedCount * Math.Abs(seatPosition - studentPosition);
                seatCounts[seatPosition] -= matchedCount;
                studentCounts[studentPosition] -= matchedCount;
                remainingPairs -= matchedCount;
            }

            return moves;
        }
    }
}