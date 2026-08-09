namespace leetcode_2285
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 2285. Maximum Total Importance of Roads
        /// https://leetcode.com/problems/maximum-total-importance-of-roads/description/
        ///
        /// You are given integer n, the number of cities numbered 0 through n - 1, and roads where roads[i] = [a_i,b_i] is a bidirectional road. Assign every city a distinct integer value from 1 through n. A road's importance is the sum of its endpoint values. Return the maximum possible total importance after an optimal assignment.
        ///
        /// Images: https://assets.leetcode.com/uploads/2022/04/07/ex1drawio.png and https://assets.leetcode.com/uploads/2022/04/07/ex2drawio.png
        ///
        /// Example 1:
        /// Input: n = 5, roads = [[0,1],[1,2],[2,3],[0,2],[1,3],[2,4]]
        /// Output: 43
        /// Explanation: Assign values [2,4,5,3,1]. The road importances are 2 + 4 = 6, 4 + 5 = 9, 5 + 3 = 8, 2 + 5 = 7, 4 + 3 = 7, and 5 + 1 = 6. Their total is 6 + 9 + 8 + 7 + 7 + 6 = 43, and no greater total is possible.
        ///
        /// Example 2:
        /// Input: n = 5, roads = [[0,3],[2,4],[1,3]]
        /// Output: 20
        /// Explanation: Assign values [4,3,2,5,1]. The road importances are 4 + 5 = 9, 2 + 1 = 3, and 3 + 5 = 8. Their total is 9 + 3 + 8 = 20, and no greater total is possible.
        ///
        /// Constraints:
        /// - 2 &lt;= n &lt;= 5 * 10^4
        /// - 1 &lt;= roads.length &lt;= 5 * 10^4
        /// - roads[i].length == 2
        /// - 0 &lt;= a_i, b_i &lt;= n - 1
        /// - a_i != b_i
        /// - There are no duplicate roads.
        /// </para>
        /// <para>
        /// 2285. 道路的最大總重要性
        /// https://leetcode.cn/problems/maximum-total-importance-of-roads/description/
        ///
        /// 給定整數 n，表示編號 0 到 n - 1 的城市數量，以及 roads，其中 roads[i] = [a_i,b_i] 表示一條雙向道路。為每個城市指派 1 到 n 中互不相同的整數值；道路的重要性是兩端城市值之和。回傳最佳指派後可能得到的最大總重要性。
        ///
        /// 圖片：https://assets.leetcode.com/uploads/2022/04/07/ex1drawio.png 與 https://assets.leetcode.com/uploads/2022/04/07/ex2drawio.png
        ///
        /// 範例 1：
        /// 輸入：n = 5, roads = [[0,1],[1,2],[2,3],[0,2],[1,3],[2,4]]
        /// 輸出：43
        /// 說明：指派值 [2,4,5,3,1]。各道路重要性為 2 + 4 = 6、4 + 5 = 9、5 + 3 = 8、2 + 5 = 7、4 + 3 = 7、5 + 1 = 6；總和為 6 + 9 + 8 + 7 + 7 + 6 = 43，且無法得到更大的總和。
        ///
        /// 範例 2：
        /// 輸入：n = 5, roads = [[0,3],[2,4],[1,3]]
        /// 輸出：20
        /// 說明：指派值 [4,3,2,5,1]。各道路重要性為 4 + 5 = 9、2 + 1 = 3、3 + 5 = 8；總和為 9 + 3 + 8 = 20，且無法得到更大的總和。
        ///
        /// 限制條件：
        /// - 2 &lt;= n &lt;= 5 * 10^4
        /// - 1 &lt;= roads.length &lt;= 5 * 10^4
        /// - roads[i].length == 2
        /// - 0 &lt;= a_i, b_i &lt;= n - 1
        /// - a_i != b_i
        /// - 不存在重複道路。
        /// </para>
        /// </summary>
        /// <remarks>
        /// 主要進入點會執行六組固定案例，比較排序城市度數與度數頻率桶兩種貪心解法，
        /// 並以 Expected、Actual 與 PASS/FAIL 顯示驗證結果。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用。</param>
        private static void Main(string[] args)
        {
            bool allPassed = RunSamples();
            Environment.ExitCode = allPassed ? 0 : 1;
        }

        /// <summary>
        /// 執行六組符合題目限制的固定道路案例，分別驗證排序城市度數與度數頻率桶解法。
        /// 每組案例都以人工推導的最大總重要性檢查兩個公開方法的輸出。
        /// </summary>
        /// <returns>十二項答案檢查全部通過時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
        private static bool RunSamples()
        {
            (string Name, int CityCount, int[][] Roads, long Expected)[] cases =
            {
                (
                    "1. 官方範例一",
                    5,
                    new[]
                    {
                        new[] { 0, 1 },
                        new[] { 1, 2 },
                        new[] { 2, 3 },
                        new[] { 0, 2 },
                        new[] { 1, 3 },
                        new[] { 2, 4 }
                    },
                    43),
                (
                    "2. 官方範例二",
                    5,
                    new[]
                    {
                        new[] { 0, 3 },
                        new[] { 2, 4 },
                        new[] { 1, 3 }
                    },
                    20),
                ("3. 最小合法圖", 2, new[] { new[] { 0, 1 } }, 3),
                (
                    "4. 星狀圖",
                    5,
                    new[]
                    {
                        new[] { 0, 1 },
                        new[] { 0, 2 },
                        new[] { 0, 3 },
                        new[] { 0, 4 }
                    },
                    30),
                (
                    "5. 含孤立城市的稀疏圖",
                    6,
                    new[]
                    {
                        new[] { 0, 1 },
                        new[] { 1, 2 },
                        new[] { 2, 3 }
                    },
                    29),
                (
                    "6. 完全圖且所有城市同度數",
                    4,
                    new[]
                    {
                        new[] { 0, 1 },
                        new[] { 0, 2 },
                        new[] { 0, 3 },
                        new[] { 1, 2 },
                        new[] { 1, 3 },
                        new[] { 2, 3 }
                    },
                    30)
            };

            int passedChecks = 0;
            const int checksPerCase = 2;
            int totalChecks = cases.Length * checksPerCase;

            foreach ((string name, int cityCount, int[][] roads, long expected) in cases)
            {
                passedChecks += RunCase(name, cityCount, roads, expected);
            }

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項測試通過");
            return passedChecks == totalChecks;
        }

        /// <summary>
        /// 將單一合法道路圖交給兩種貪心解法，並顯示輸入、預期答案、實際答案與驗證結果。
        /// </summary>
        /// <param name="name">案例名稱。</param>
        /// <param name="cityCount">城市數量，範圍為 2 到 50000。</param>
        /// <param name="roads">不含自環與重複道路的雙向道路陣列。</param>
        /// <param name="expected">人工推導的最大總重要性。</param>
        /// <returns>本案例通過的解法數量，範圍為零到二。</returns>
        private static int RunCase(string name, int cityCount, int[][] roads, long expected)
        {
            Console.WriteLine($"案例：{name}");
            Console.WriteLine($"Input：n = {cityCount}, roads = {FormatRoads(roads)}");

            long sortingActual = MaximumImportance(cityCount, roads);
            long bucketActual = MaximumImportance2(cityCount, roads);
            bool sortingPassed = sortingActual == expected;
            bool bucketPassed = bucketActual == expected;

            Console.WriteLine("解法一：MaximumImportance（排序城市度數）");
            Console.WriteLine($"Expected：{expected}");
            Console.WriteLine($"Actual：{sortingActual}");
            Console.WriteLine($"Result：{(sortingPassed ? "PASS" : "FAIL")}");
            Console.WriteLine("解法二：MaximumImportance2（度數頻率桶）");
            Console.WriteLine($"Expected：{expected}");
            Console.WriteLine($"Actual：{bucketActual}");
            Console.WriteLine($"Result：{(bucketPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return (sortingPassed ? 1 : 0) + (bucketPassed ? 1 : 0);
        }

        /// <summary>
        /// 將道路陣列格式化為穩定的巢狀方括號字串，供測試輸出與 README 範例使用。
        /// </summary>
        /// <param name="roads">每個元素都包含兩個城市編號的道路陣列。</param>
        /// <returns>格式為 <c>[[a1,b1],[a2,b2],...]</c> 的字串。</returns>
        private static string FormatRoads(int[][] roads)
        {
            return $"[{string.Join(",", roads.Select(static road => $"[{road[0]},{road[1]}]"))}]";
        }

        /// <summary>
        /// 統計合法無向圖中每座城市的道路數，將度數由小到大排序，再依序配對重要性 1 到 n。
        /// 城市每連接一條道路，其重要性就會在總和中出現一次，因此高度數城市應配到較大的重要性。
        /// 輸入必須符合題目限制，方法不修改 <paramref name="roads"/>，並回傳所有道路可達成的最大總重要性；
        /// 時間複雜度為 O(n log n + m)，額外空間複雜度為 O(n)，其中 m 為道路數量。
        /// </summary>
        /// <remarks>
        /// 參考：
        /// https://leetcode.cn/problems/maximum-total-importance-of-roads/solutions/1523886/by-endlesscheng-9p6y/
        /// https://leetcode.cn/problems/maximum-total-importance-of-roads/solutions/2636473/2285-dao-lu-de-zui-da-zong-zhong-yao-xin-p3zl/
        /// https://leetcode.cn/problems/maximum-total-importance-of-roads/solutions/1523888/by-shou-hu-zhe-t-li9y/
        /// </remarks>
        /// <param name="n">城市數量，城市編號必須介於 0 到 <paramref name="n"/> - 1。</param>
        /// <param name="roads">每個元素都包含兩個不同城市編號，且不含重複道路的雙向道路陣列。</param>
        /// <returns>將 1 到 <paramref name="n"/> 各使用一次後，所有道路重要性的最大總和。</returns>
        public static long MaximumImportance(int n, int[][] roads)
        {
            long[] cityDegrees = new long[n];

            for (int i = 0; i < roads.Length; i++)
            {
                // 每條無向道路會讓兩端城市各在總重要性中貢獻一次自身權重。
                cityDegrees[roads[i][0]]++;
                cityDegrees[roads[i][1]]++;
            }

            Array.Sort(cityDegrees);

            long totalImportance = 0;
            for (int i = 0; i < n; i++)
            {
                // 同序配對可讓高度數乘上較大權重；若反向配對，交換兩個權重不會得到更大的總和。
                totalImportance += cityDegrees[i] * (i + 1);
            }

            return totalImportance;
        }

        /// <summary>
        /// 統計合法無向圖中每座城市的道路數，再以度數頻率桶取代排序，從低度數到高度數分配重要性 1 到 n。
        /// 因簡單圖的城市度數必介於 0 到 n - 1，可線性掃描所有度數並讓高度數取得較大重要性。
        /// 輸入必須符合題目限制，方法不修改 <paramref name="roads"/>，並回傳所有道路可達成的最大總重要性；
        /// 時間複雜度為 O(n + m)，額外空間複雜度為 O(n)，其中 m 為道路數量。
        /// </summary>
        /// <param name="n">城市數量，城市編號必須介於 0 到 <paramref name="n"/> - 1。</param>
        /// <param name="roads">每個元素都包含兩個不同城市編號，且不含重複道路的雙向道路陣列。</param>
        /// <returns>將 1 到 <paramref name="n"/> 各使用一次後，所有道路重要性的最大總和。</returns>
        public static long MaximumImportance2(int n, int[][] roads)
        {
            int[] cityDegrees = new int[n];
            for (int i = 0; i < roads.Length; i++)
            {
                cityDegrees[roads[i][0]]++;
                cityDegrees[roads[i][1]]++;
            }

            int[] degreeFrequencies = new int[n];
            foreach (int degree in cityDegrees)
            {
                degreeFrequencies[degree]++;
            }

            long totalImportance = 0;
            int importance = 1;

            for (int degree = 0; degree < degreeFrequencies.Length; degree++)
            {
                // 頻率桶依度數遞增展開，等同排序結果，但省去 O(n log n) 的比較排序。
                for (int city = 0; city < degreeFrequencies[degree]; city++)
                {
                    totalImportance += (long)degree * importance;
                    importance++;
                }
            }

            return totalImportance;
        }
    }
}