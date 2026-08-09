namespace leetcode_1334
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 1334. Find the City With the Smallest Number of Neighbors at a Threshold Distance
        /// https://leetcode.com/problems/find-the-city-with-the-smallest-number-of-neighbors-at-a-threshold-distance/description/
        ///
        /// There are n cities numbered from 0 to n - 1. Given the array edges where edges[i] = [from_i, to_i, weight_i]
        /// represents a bidirectional and weighted edge between cities from_i and to_i, and given the integer distanceThreshold.
        ///
        /// Return the city with the smallest number of cities that are reachable through some path and whose distance is at most
        /// distanceThreshold. If there are multiple such cities, return the city with the greatest number.
        ///
        /// Notice that the distance of a path connecting cities i and j is equal to the sum of the edges' weights along that path.
        ///
        /// Example 1:
        /// Official illustration: https://assets.leetcode.com/uploads/2024/08/23/problem1334example1.png
        /// Input: n = 4, edges = [[0,1,3],[1,2,1],[1,3,4],[2,3,1]], distanceThreshold = 4
        /// Output: 3
        /// Explanation: The figure above describes the graph.
        /// The neighboring cities at a distanceThreshold = 4 for each city are:
        /// City 0 -&gt; [City 1, City 2]
        /// City 1 -&gt; [City 0, City 2, City 3]
        /// City 2 -&gt; [City 0, City 1, City 3]
        /// City 3 -&gt; [City 1, City 2]
        /// Cities 0 and 3 have 2 neighboring cities at a distanceThreshold = 4, but we have to return city 3 since it has
        /// the greatest number.
        ///
        /// Example 2:
        /// Official illustration: https://assets.leetcode.com/uploads/2024/08/23/problem1334example0.png
        /// Input: n = 5, edges = [[0,1,2],[0,4,8],[1,2,3],[1,4,2],[2,3,1],[3,4,1]], distanceThreshold = 2
        /// Output: 0
        /// Explanation: The figure above describes the graph.
        /// The neighboring cities at a distanceThreshold = 2 for each city are:
        /// City 0 -&gt; [City 1]
        /// City 1 -&gt; [City 0, City 4]
        /// City 2 -&gt; [City 3, City 4]
        /// City 3 -&gt; [City 2, City 4]
        /// City 4 -&gt; [City 1, City 2, City 3]
        /// The city 0 has 1 neighboring city at a distanceThreshold = 2.
        ///
        /// Constraints:
        /// - 2 &lt;= n &lt;= 100
        /// - 1 &lt;= edges.length &lt;= n * (n - 1) / 2
        /// - edges[i].length == 3
        /// - 0 &lt;= from_i &lt; to_i &lt; n
        /// - 1 &lt;= weight_i, distanceThreshold &lt;= 10^4
        /// - All pairs (from_i, to_i) are distinct.
        /// </para>
        /// <para>
        /// 1334. 閾值距離內鄰居最少的城市
        /// https://leetcode.cn/problems/find-the-city-with-the-smallest-number-of-neighbors-at-a-threshold-distance/description/
        ///
        /// 有 n 個城市，編號從 0 到 n - 1。給定陣列 edges，其中 edges[i] = [from_i, to_i, weight_i]
        /// 表示城市 from_i 與 to_i 之間的一條雙向加權邊，另給定整數 distanceThreshold。
        ///
        /// 回傳經由某條路徑可達，且距離不超過 distanceThreshold 的城市數量最少之城市。
        /// 如果有多個這樣的城市，回傳編號最大的城市。
        ///
        /// 請注意，連接城市 i 與 j 的路徑距離，等於該路徑上所有邊權重的總和。
        ///
        /// 範例 1：
        /// 官方示意圖：https://assets.leetcode.com/uploads/2024/08/23/problem1334example1.png
        /// 輸入：n = 4, edges = [[0,1,3],[1,2,1],[1,3,4],[2,3,1]], distanceThreshold = 4
        /// 輸出：3
        /// 解釋：上圖描述了此圖形。
        /// 每個城市在 distanceThreshold = 4 時的鄰近城市如下：
        /// 城市 0 -&gt; [城市 1, 城市 2]
        /// 城市 1 -&gt; [城市 0, 城市 2, 城市 3]
        /// 城市 2 -&gt; [城市 0, 城市 1, 城市 3]
        /// 城市 3 -&gt; [城市 1, 城市 2]
        /// 城市 0 與 3 都有 2 個距離不超過 distanceThreshold = 4 的鄰近城市，但必須回傳城市 3，
        /// 因為它的編號較大。
        ///
        /// 範例 2：
        /// 官方示意圖：https://assets.leetcode.com/uploads/2024/08/23/problem1334example0.png
        /// 輸入：n = 5, edges = [[0,1,2],[0,4,8],[1,2,3],[1,4,2],[2,3,1],[3,4,1]], distanceThreshold = 2
        /// 輸出：0
        /// 解釋：上圖描述了此圖形。
        /// 每個城市在 distanceThreshold = 2 時的鄰近城市如下：
        /// 城市 0 -&gt; [城市 1]
        /// 城市 1 -&gt; [城市 0, 城市 4]
        /// 城市 2 -&gt; [城市 3, 城市 4]
        /// 城市 3 -&gt; [城市 2, 城市 4]
        /// 城市 4 -&gt; [城市 1, 城市 2, 城市 3]
        /// 城市 0 在 distanceThreshold = 2 時有 1 個鄰近城市。
        ///
        /// 限制條件：
        /// - 2 &lt;= n &lt;= 100
        /// - 1 &lt;= edges.length &lt;= n * (n - 1) / 2
        /// - edges[i].length == 3
        /// - 0 &lt;= from_i &lt; to_i &lt; n
        /// - 1 &lt;= weight_i, distanceThreshold &lt;= 10^4
        /// - 所有 (from_i, to_i) 組合均不相同。
        /// </para>
        /// </summary>
        /// <remarks>
        /// 執行固定案例，逐一比較 Floyd-Warshall、鄰接矩陣 Dijkstra 與優先佇列 Dijkstra 的結果。
        /// </remarks>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            (int N, int[][] Edges, int DistanceThreshold, int Expected)[] cases =
            [
                (4, [[0, 1, 3], [1, 2, 1], [1, 3, 4], [2, 3, 1]], 4, 3),
                (5, [[0, 1, 2], [0, 4, 8], [1, 2, 3], [1, 4, 2], [2, 3, 1], [3, 4, 1]], 2, 0),
                (2, [[0, 1, 10000]], 10000, 1),
                (4, [[0, 1, 5]], 4, 3),
                (4, [[0, 1, 10], [0, 2, 1], [1, 2, 1], [1, 3, 1]], 2, 3),
                (5, [[0, 1, 1], [1, 2, 1]], 1, 4),
                (5, [[0, 1, 2], [1, 2, 2], [2, 3, 2], [3, 4, 2]], 2, 4),
                (100, [[0, 1, 10000]], 10000, 99)
            ];

            int passedChecks = 0;
            int totalChecks = cases.Length * 3;

            for (int i = 0; i < cases.Length; i++)
            {
                passedChecks += RunTestCase(i + 1, cases[i]);
            }

            Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
            Environment.ExitCode = passedChecks == totalChecks ? 0 : 1;
        }


        /// <summary>
        /// 執行單一測試案例，分別呼叫三種最短路徑解法並輸出預期值、實際值與通過狀態。
        /// 輸入案例必須符合題目限制；回傳三種解法中結果正確的檢查數量。
        /// </summary>
        /// <param name="caseNumber">從 1 開始顯示的案例編號。</param>
        /// <param name="testCase">包含城市數、邊、距離閾值與手動推導預期值的測試資料。</param>
        /// <returns>本案例通過的檢查數量，範圍為 0 到 3。</returns>
        private static int RunTestCase(
            int caseNumber,
            (int N, int[][] Edges, int DistanceThreshold, int Expected) testCase)
        {
            int floydWarshallActual = FindTheCity(testCase.N, testCase.Edges, testCase.DistanceThreshold);
            int matrixDijkstraActual = FindTheCity2(testCase.N, testCase.Edges, testCase.DistanceThreshold);
            int priorityQueueDijkstraActual = FindTheCity3(testCase.N, testCase.Edges, testCase.DistanceThreshold);
            bool floydWarshallPassed = floydWarshallActual == testCase.Expected;
            bool matrixDijkstraPassed = matrixDijkstraActual == testCase.Expected;
            bool priorityQueueDijkstraPassed = priorityQueueDijkstraActual == testCase.Expected;

            Console.WriteLine(
                $"Case {caseNumber}: n = {testCase.N}, edges = {FormatEdges(testCase.Edges)}, " +
                $"distanceThreshold = {testCase.DistanceThreshold}");
            Console.WriteLine($"Expected: {testCase.Expected}");
            Console.WriteLine($"FindTheCity Actual: {floydWarshallActual} => {(floydWarshallPassed ? "PASS" : "FAIL")}");
            Console.WriteLine($"FindTheCity2 Actual: {matrixDijkstraActual} => {(matrixDijkstraPassed ? "PASS" : "FAIL")}");
            Console.WriteLine($"FindTheCity3 Actual: {priorityQueueDijkstraActual} => {(priorityQueueDijkstraPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return Convert.ToInt32(floydWarshallPassed)
                + Convert.ToInt32(matrixDijkstraPassed)
                + Convert.ToInt32(priorityQueueDijkstraPassed);
        }


        /// <summary>
        /// 將邊陣列格式化成穩定且容易比對的二維陣列文字；輸入為合法的三欄邊資料，輸出僅供主控台展示。
        /// </summary>
        /// <param name="edges">每列依序包含起點、終點與權重的邊陣列。</param>
        /// <returns>例如 <c>[[0, 1, 3], [1, 2, 1]]</c> 的文字。</returns>
        private static string FormatEdges(int[][] edges)
        {
            return $"[{string.Join(", ", edges.Select(edge => $"[{string.Join(", ", edge)}]"))}]";
        }


        /// <summary>
        /// Floyd-Warshall 算法是解決任意兩點間的最短路徑的一種演算法。
        /// https://zh.wikipedia.org/zh-tw/Floyd-Warshall%E7%AE%97%E6%B3%95
        /// https://hackmd.io/@fdhscpp110/shortest_path
        /// 
        /// k 為中間點, 考慮是否經過點 k 能夠縮短 i 和 j 之間的路徑。
        /// 當 (i, j) >= (i, k) + (k, j) 時候
        /// (i, j) = (i, k) + (k, j)
        /// 簡單說就是原本 i 走道 j
        /// 現在是透過第三方城市 k
        /// 使得 i 到 k + k 到 j
        /// 會比原先 i, j 直達距離還要短
        /// 
        /// 
        /// ref:
        /// https://leetcode.cn/problems/find-the-city-with-the-smallest-number-of-neighbors-at-a-threshold-distance/solutions/2524814/yu-zhi-ju-chi-nei-lin-ju-zui-shao-de-che-i73c/
        /// https://leetcode.cn/problems/find-the-city-with-the-smallest-number-of-neighbors-at-a-threshold-distance/solutions/2525946/dai-ni-fa-ming-floyd-suan-fa-cong-ji-yi-m8s51/
        /// https://leetcode.cn/problems/find-the-city-with-the-smallest-number-of-neighbors-at-a-threshold-distance/solutions/2526052/gong-shui-san-xie-han-gai-suo-you-cun-tu-svq7/
        /// https://leetcode.cn/problems/find-the-city-with-the-smallest-number-of-neighbors-at-a-threshold-distance/solutions/1966076/by-stormsunshine-ksol/
        /// 
        /// map 一開始會給 預設值 int.MaxValue / 2
        /// int.MaxValue / 2:  除法用意是 防止加法溢出
        /// 之後會再把 edges 填入 map 裡面
        /// 
        /// 題目要求是取 distanceThreshold 內 可以到達的最少城市, 
        /// 所以更新 ans 的 if 要取 小於判斷
        /// 一開始的 map 資料填入是雙向圖
        /// 所以取資料時候不用擔心, 可以取道最大的城市編號
        /// 
        /// 
        /// --------------------------------------------------------------------
        /// 還有 Dijkstra 也可以處理
        /// 不過兩者有些微差異
        /// 不能檢查 負環
        /// </summary>
        /// <remarks>
        /// 輸入需符合題目限制：城市數為 2 到 100、邊為不重複的雙向正權重邊，且距離閾值為正整數。
        /// 此方法建立所有城市間的最短距離，不會修改 <paramref name="edges"/>。
        /// </remarks>
        /// <param name="n">城市總數；城市編號介於 0 與 <paramref name="n"/> - 1。</param>
        /// <param name="edges">每列依序包含起點、終點與正權重的雙向邊。</param>
        /// <param name="distanceThreshold">判定另一座城市可達的最大路徑距離。</param>
        /// <returns>閾值內可達城市最少的城市；若同票則回傳編號最大者。</returns>
        public static int FindTheCity(int n, int[][] edges, int distanceThreshold)
        {
            int[][] distances = CreateDistanceMatrix(n, edges);

            for (int intermediate = 0; intermediate < n; intermediate++)
            {
                for (int from = 0; from < n; from++)
                {
                    for (int to = 0; to < n; to++)
                    {
                        // 允許 intermediate 作為中繼點後，保留較短的路徑。
                        distances[from][to] = Math.Min(
                            distances[from][to],
                            distances[from][intermediate] + distances[intermediate][to]);
                    }
                }
            }

            return SelectCity(distances, distanceThreshold);
        }


        /// <summary>
        /// 使用鄰接矩陣，從每座城市各執行一次 Dijkstra 以計算閾值內的可達城市數量。
        /// 每輪線性尋找尚未確定且距離最小的城市，適合展示不依賴優先佇列的單源最短路徑流程。
        /// 輸入需符合題目的正權重雙向圖限制；回傳可達城市最少且同票時編號最大的城市，不修改輸入。
        /// </summary>
        /// <param name="n">城市總數；城市編號介於 0 與 <paramref name="n"/> - 1。</param>
        /// <param name="edges">每列依序包含起點、終點與正權重的雙向邊。</param>
        /// <param name="distanceThreshold">判定另一座城市可達的最大路徑距離。</param>
        /// <returns>閾值內可達城市最少的城市；若同票則回傳編號最大者。</returns>
        public static int FindTheCity2(int n, int[][] edges, int distanceThreshold)
        {
            const int infinity = int.MaxValue / 2;
            int[][] graph = CreateDistanceMatrix(n, edges);
            int minimumReachableCount = int.MaxValue;
            int answer = -1;

            for (int source = 0; source < n; source++)
            {
                int[] distances = new int[n];
                bool[] visited = new bool[n];
                Array.Fill(distances, infinity);
                distances[source] = 0;

                for (int step = 0; step < n; step++)
                {
                    int current = -1;
                    for (int city = 0; city < n; city++)
                    {
                        if (!visited[city] && (current == -1 || distances[city] < distances[current]))
                        {
                            current = city;
                        }
                    }

                    if (current == -1 || distances[current] == infinity)
                    {
                        break;
                    }

                    visited[current] = true;

                    for (int neighbor = 0; neighbor < n; neighbor++)
                    {
                        if (visited[neighbor] || graph[current][neighbor] == infinity)
                        {
                            continue;
                        }

                        // Dijkstra 的鬆弛步驟：嘗試以 current 作為較短路徑的前一站。
                        int candidateDistance = distances[current] + graph[current][neighbor];
                        distances[neighbor] = Math.Min(distances[neighbor], candidateDistance);
                    }
                }

                int reachableCount = CountReachableCities(distances, source, distanceThreshold);
                if (reachableCount <= minimumReachableCount)
                {
                    minimumReachableCount = reachableCount;
                    answer = source;
                }
            }

            return answer;
        }


        /// <summary>
        /// 使用鄰接表與優先佇列，從每座城市各執行一次 Dijkstra 以計算閾值內的可達城市數量。
        /// 優先處理目前距離最短的候選城市，並跳過已被更短路徑取代的過期項目，適合稀疏圖。
        /// 輸入需符合題目的正權重雙向圖限制；回傳可達城市最少且同票時編號最大的城市，不修改輸入。
        /// </summary>
        /// <param name="n">城市總數；城市編號介於 0 與 <paramref name="n"/> - 1。</param>
        /// <param name="edges">每列依序包含起點、終點與正權重的雙向邊。</param>
        /// <param name="distanceThreshold">判定另一座城市可達的最大路徑距離。</param>
        /// <returns>閾值內可達城市最少的城市；若同票則回傳編號最大者。</returns>
        public static int FindTheCity3(int n, int[][] edges, int distanceThreshold)
        {
            const int infinity = int.MaxValue / 2;
            List<(int To, int Weight)>[] graph = CreateAdjacencyList(n, edges);
            int minimumReachableCount = int.MaxValue;
            int answer = -1;

            for (int source = 0; source < n; source++)
            {
                int[] distances = new int[n];
                Array.Fill(distances, infinity);
                distances[source] = 0;

                PriorityQueue<int, int> queue = new();
                queue.Enqueue(source, 0);

                while (queue.TryDequeue(out int current, out int currentDistance))
                {
                    // 同一城市可能多次入列；只處理仍等於目前最佳距離的項目。
                    if (currentDistance != distances[current])
                    {
                        continue;
                    }

                    foreach ((int neighbor, int weight) in graph[current])
                    {
                        int candidateDistance = currentDistance + weight;
                        if (candidateDistance >= distances[neighbor])
                        {
                            continue;
                        }

                        distances[neighbor] = candidateDistance;
                        queue.Enqueue(neighbor, candidateDistance);
                    }
                }

                int reachableCount = CountReachableCities(distances, source, distanceThreshold);
                if (reachableCount <= minimumReachableCount)
                {
                    minimumReachableCount = reachableCount;
                    answer = source;
                }
            }

            return answer;
        }


        /// <summary>
        /// 建立雙向圖的距離矩陣，將自身距離設為 0、沒有直接邊的距離設為安全的無限大值。
        /// 輸入為題目允許的不重複正權重邊；回傳可供 Floyd-Warshall 或矩陣 Dijkstra 使用的新矩陣。
        /// </summary>
        /// <param name="n">城市總數。</param>
        /// <param name="edges">每列依序包含起點、終點與權重的雙向邊。</param>
        /// <returns>大小為 <paramref name="n"/> × <paramref name="n"/> 的距離矩陣。</returns>
        private static int[][] CreateDistanceMatrix(int n, int[][] edges)
        {
            const int infinity = int.MaxValue / 2;
            int[][] distances = new int[n][];

            for (int city = 0; city < n; city++)
            {
                distances[city] = new int[n];
                Array.Fill(distances[city], infinity);
                distances[city][city] = 0;
            }

            foreach (int[] edge in edges)
            {
                int from = edge[0];
                int to = edge[1];
                int weight = edge[2];
                distances[from][to] = weight;
                distances[to][from] = weight;
            }

            return distances;
        }


        /// <summary>
        /// 建立每條邊皆存入兩個方向的加權鄰接表；輸入為題目允許的雙向邊，輸出為每座城市的鄰居清單。
        /// </summary>
        /// <param name="n">城市總數。</param>
        /// <param name="edges">每列依序包含起點、終點與權重的雙向邊。</param>
        /// <returns>索引代表城市、元素代表相鄰城市與邊權重的鄰接表。</returns>
        private static List<(int To, int Weight)>[] CreateAdjacencyList(int n, int[][] edges)
        {
            List<(int To, int Weight)>[] graph = new List<(int To, int Weight)>[n];

            for (int city = 0; city < n; city++)
            {
                graph[city] = [];
            }

            foreach (int[] edge in edges)
            {
                int from = edge[0];
                int to = edge[1];
                int weight = edge[2];
                graph[from].Add((to, weight));
                graph[to].Add((from, weight));
            }

            return graph;
        }


        /// <summary>
        /// 從完整的最短距離矩陣選出閾值內鄰居最少的城市，並在同票時保留編號較大的城市。
        /// </summary>
        /// <param name="distances">所有城市對之間的最短距離矩陣。</param>
        /// <param name="distanceThreshold">判定另一座城市可達的最大距離。</param>
        /// <returns>符合題目選擇規則的城市編號。</returns>
        private static int SelectCity(int[][] distances, int distanceThreshold)
        {
            int minimumReachableCount = int.MaxValue;
            int answer = -1;

            for (int city = 0; city < distances.Length; city++)
            {
                int reachableCount = CountReachableCities(distances[city], city, distanceThreshold);

                // 由小到大走訪並在相等時更新，最後自然保留編號最大的城市。
                if (reachableCount <= minimumReachableCount)
                {
                    minimumReachableCount = reachableCount;
                    answer = city;
                }
            }

            return answer;
        }


        /// <summary>
        /// 統計指定來源城市在距離閾值內可達的其他城市數量；不把來源城市本身計入結果。
        /// </summary>
        /// <param name="distances">指定來源到各城市的最短距離。</param>
        /// <param name="source">必須從統計中排除的來源城市編號。</param>
        /// <param name="distanceThreshold">判定另一座城市可達的最大距離。</param>
        /// <returns>最短距離小於等於閾值的其他城市數量。</returns>
        private static int CountReachableCities(int[] distances, int source, int distanceThreshold)
        {
            int reachableCount = 0;

            for (int city = 0; city < distances.Length; city++)
            {
                if (city != source && distances[city] <= distanceThreshold)
                {
                    reachableCount++;
                }
            }

            return reachableCount;
        }
    }
}