namespace leetcode_310
{
    internal class Program
    {
        /// <summary>
        /// 310. Minimum Height Trees
        /// https://leetcode.com/problems/minimum-height-trees/description/
        /// <para>
        /// A tree is an undirected graph in which any two vertices are connected by exactly one path. In other words, any connected graph without simple cycles is a tree.
        ///
        /// Given a tree of n nodes labeled from 0 to n - 1 and an array of n - 1 edges, where edges[i] = [ai, bi] represents an undirected edge between ai and bi, you may choose any node as the root. When x is chosen as the root, the resulting tree has height h. Among all possible rooted trees, those with minimum height min(h) are called minimum height trees (MHTs).
        ///
        /// Return a list of all MHT root labels in any order.
        ///
        /// The height of a rooted tree is the number of edges on the longest downward path from the root to a leaf.
        ///
        /// Example 1:
        /// Image: https://assets.leetcode.com/uploads/2020/09/01/e1.jpg
        /// Input: n = 4, edges = [[1,0],[1,2],[1,3]]
        /// Output: [1]
        /// Explanation: The tree has height 1 when rooted at node 1, which is the only MHT.
        ///
        /// Example 2:
        /// Image: https://assets.leetcode.com/uploads/2020/09/01/e2.jpg
        /// Input: n = 6, edges = [[3,0],[3,1],[3,2],[3,4],[5,4]]
        /// Output: [3,4]
        ///
        /// Constraints:
        /// - 1 &lt;= n &lt;= 2 * 10^4
        /// - edges.length == n - 1
        /// - 0 &lt;= ai, bi &lt; n
        /// - ai != bi
        /// - All pairs (ai, bi) are distinct.
        /// - The input is guaranteed to be a tree with no repeated edges.
        /// </para>
        /// <para>
        /// 310. 最小高度樹
        /// https://leetcode.cn/problems/minimum-height-trees/description/
        ///
        /// 樹是一種無向圖，其中任意兩個頂點之間恰好只有一條路徑。換句話說，任何沒有簡單環的連通圖都是樹。
        ///
        /// 給定一棵含 n 個節點、標號從 0 到 n - 1 的樹，以及含 n - 1 條邊的陣列 edges，其中 edges[i] = [ai, bi] 表示 ai 與 bi 之間有一條無向邊。你可以選擇任意節點作為根。當選擇 x 作為根時，所得樹的高度為 h。在所有可能的有根樹中，高度最小 min(h) 的樹稱為最小高度樹（MHT）。
        ///
        /// 以任意順序回傳所有 MHT 的根節點標號。
        ///
        /// 有根樹的高度是從根到葉節點的最長向下路徑所包含的邊數。
        ///
        /// 範例 1：
        /// 圖片：https://assets.leetcode.com/uploads/2020/09/01/e1.jpg
        /// 輸入：n = 4, edges = [[1,0],[1,2],[1,3]]
        /// 輸出：[1]
        /// 解釋：以節點 1 為根時樹高為 1，且它是唯一的 MHT。
        ///
        /// 範例 2：
        /// 圖片：https://assets.leetcode.com/uploads/2020/09/01/e2.jpg
        /// 輸入：n = 6, edges = [[3,0],[3,1],[3,2],[3,4],[5,4]]
        /// 輸出：[3,4]
        ///
        /// 限制條件：
        /// - 1 &lt;= n &lt;= 2 * 10^4
        /// - edges.length == n - 1
        /// - 0 &lt;= ai, bi &lt; n
        /// - ai != bi
        /// - 所有 (ai, bi) 配對都不相同。
        /// - 輸入保證是一棵沒有重複邊的樹。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行六組固定範例，涵蓋單一中心與雙中心等樹形，並逐案比對預期及實際結果。
        /// 範例輸入皆符合節點編號從 0 開始、邊數為 n - 1，且所有邊構成一棵樹的條件；
        /// 執行完成後輸出每案 PASS/FAIL 與通過總數。
        /// </summary>
        private static void RunSamples()
        {
            SampleCase[] samples =
            [
                new(
                    "單一節點",
                    1,
                    [],
                    [0]),
                new(
                    "雙節點",
                    2,
                    [[0, 1]],
                    [0, 1]),
                new(
                    "四節點星狀樹",
                    4,
                    [[1, 0], [1, 2], [1, 3]],
                    [1]),
                new(
                    "六節點題目範例",
                    6,
                    [[3, 0], [3, 1], [3, 2], [3, 4], [5, 4]],
                    [3, 4]),
                new(
                    "五節點長鏈",
                    5,
                    [[0, 1], [1, 2], [2, 3], [3, 4]],
                    [2]),
                new(
                    "六節點長鏈",
                    6,
                    [[0, 1], [1, 2], [2, 3], [3, 4], [4, 5]],
                    [2, 3])
            ];

            int passed = 0;

            for (int i = 0; i < samples.Length; i++)
            {
                if (RunSample(i + 1, samples[i]))
                {
                    passed++;
                }
            }

            Console.WriteLine($"總結：{passed}/{samples.Length} 筆測試通過");
        }

        /// <summary>
        /// 執行單一最小高度樹案例。由於題目允許答案採任意順序，
        /// 比對前會排序預期與實際根節點，最後回傳該案例是否通過。
        /// </summary>
        /// <param name="number">從 1 開始顯示的案例編號。</param>
        /// <param name="sample">包含節點數、無向邊及預期根節點的案例資料。</param>
        /// <returns>排序後的實際結果與預期結果相同時為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
        private static bool RunSample(int number, SampleCase sample)
        {
            int[] expected = sample.Expected.Order().ToArray();
            int[] actual = FindMinHeightTrees(sample.NodeCount, sample.Edges)
                .Order()
                .ToArray();
            bool isPassed = expected.SequenceEqual(actual);

            Console.WriteLine($"案例 {number}：{sample.Name}");
            Console.WriteLine($"輸入：n = {sample.NodeCount}, edges = {FormatEdges(sample.Edges)}");
            Console.WriteLine($"Expected: {FormatNodes(expected)}");
            Console.WriteLine($"Actual:   {FormatNodes(actual)}");
            Console.WriteLine($"結果：{(isPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return isPassed;
        }

        /// <summary>
        /// 將邊集合格式化成易於閱讀的二維陣列字串，供範例輸入輸出使用。
        /// 輸入為每列恰含兩個節點編號的邊集合，輸出格式如 <c>[[1, 0], [1, 2]]</c>。
        /// </summary>
        /// <param name="edges">要格式化的無向邊集合。</param>
        /// <returns>使用方括號表示的邊集合字串。</returns>
        private static string FormatEdges(int[][] edges)
        {
            return $"[{string.Join(", ", edges.Select(edge => $"[{edge[0]}, {edge[1]}]"))}]";
        }

        /// <summary>
        /// 將節點編號集合格式化成易於閱讀的一維陣列字串。
        /// 輸入可為空或包含任意數量節點，輸出格式如 <c>[3, 4]</c>。
        /// </summary>
        /// <param name="nodes">要格式化的節點編號集合。</param>
        /// <returns>使用方括號表示的節點集合字串。</returns>
        private static string FormatNodes(IEnumerable<int> nodes)
        {
            return $"[{string.Join(", ", nodes)}]";
        }

        /// <summary>
        /// 找出指定樹的所有最小高度樹根節點。
        /// 解法先以鄰接表表示無向樹，再用兩次 DFS 找出樹的直徑端點與完整路徑；
        /// 輸入必須是節點編號介於 0 到 n - 1、邊數為 n - 1 的合法樹，
        /// 輸出為直徑中間的一個或兩個節點。
        /// </summary>
        /// <param name="n">樹的節點數量，至少為 1。</param>
        /// <param name="edges">樹中的無向邊，每條邊包含兩個相異的合法節點編號。</param>
        /// <returns>所有可形成最小高度樹的根節點編號，數量至多為兩個。</returns>
        public static IList<int> FindMinHeightTrees(int n, int[][] edges)
        {
            IList<int> result = new List<int>();
            if (n == 1)
            {
                result.Add(0);
                return result;
            }

            IList<int>[] adjacencyList = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                adjacencyList[i] = new List<int>();
            }

            // 每條無向邊都必須同時記錄兩個方向，DFS 才能從任意節點走訪整棵樹。
            foreach (int[] edge in edges)
            {
                adjacencyList[edge[0]].Add(edge[1]);
                adjacencyList[edge[1]].Add(edge[0]);
            }

            int[] parent = new int[n];
            Array.Fill(parent, -1);

            // 任意節點的最遠點是某個直徑端點；再從該端點搜尋即可找到另一端。
            int firstEndpoint = FindLongestNode(0, parent, adjacencyList);
            int secondEndpoint = FindLongestNode(firstEndpoint, parent, adjacencyList);

            IList<int> diameterPath = new List<int>();
            parent[firstEndpoint] = -1;

            // 第二次 DFS 留下的父節點關係可由另一端點一路回溯出完整直徑。
            while (secondEndpoint != -1)
            {
                diameterPath.Add(secondEndpoint);
                secondEndpoint = parent[secondEndpoint];
            }

            int pathNodeCount = diameterPath.Count;
            if (pathNodeCount % 2 == 0)
            {
                result.Add(diameterPath[pathNodeCount / 2 - 1]);
            }

            result.Add(diameterPath[pathNodeCount / 2]);
            return result;
        }

        /// <summary>
        /// 從指定起點以 DFS 計算到所有節點的距離，並找出距離最遠的節點。
        /// 輸入的鄰接表必須表示一棵連通樹；方法同時更新父節點陣列，
        /// 回傳值可作為直徑端點，父節點資料則可供後續重建路徑。
        /// </summary>
        /// <param name="u">DFS 的起始節點。</param>
        /// <param name="parent">記錄各節點在本次走訪中的父節點。</param>
        /// <param name="adj">以鄰接表表示的無向樹。</param>
        /// <returns>距離起點最遠的節點編號。</returns>
        public static int FindLongestNode(int u, int[] parent, IList<int>[] adj)
        {
            int[] distance = new int[adj.Length];
            Array.Fill(distance, -1);
            distance[u] = 0;

            DFS(u, distance, parent, adj);

            int maxDistance = 0;
            int farthestNode = u;

            for (int i = 0; i < distance.Length; i++)
            {
                if (distance[i] > maxDistance)
                {
                    maxDistance = distance[i];
                    farthestNode = i;
                }
            }

            return farthestNode;
        }

        /// <summary>
        /// 從目前節點遞迴走訪尚未拜訪的相鄰節點，並記錄其距離與父節點。
        /// 輸入的距離陣列以 -1 表示尚未拜訪；執行完成後可取得起點到各節點的距離，
        /// 並透過父節點陣列回溯 DFS 路徑。
        /// </summary>
        /// <param name="u">目前正在走訪的節點。</param>
        /// <param name="dist">起點到每個節點的距離，-1 代表尚未拜訪。</param>
        /// <param name="parent">每個已拜訪節點的父節點。</param>
        /// <param name="adj">以鄰接表表示的無向樹。</param>
        public static void DFS(int u, int[] dist, int[] parent, IList<int>[] adj)
        {
            foreach (int neighbor in adj[u])
            {
                if (dist[neighbor] >= 0)
                {
                    continue;
                }

                dist[neighbor] = dist[u] + 1;
                parent[neighbor] = u;
                DFS(neighbor, dist, parent, adj);
            }
        }

        /// <summary>
        /// 表示一組可執行範例，包含案例名稱、合法樹輸入與預期的最小高度樹根節點。
        /// </summary>
        /// <param name="Name">案例顯示名稱。</param>
        /// <param name="NodeCount">樹的節點數量。</param>
        /// <param name="Edges">構成樹的無向邊。</param>
        /// <param name="Expected">預期的最小高度樹根節點。</param>
        private sealed record SampleCase(string Name, int NodeCount, int[][] Edges, int[] Expected);
    }
}
