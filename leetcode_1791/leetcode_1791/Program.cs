namespace leetcode_1791
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 1791. Find Center of Star Graph
        /// https://leetcode.com/problems/find-center-of-star-graph/description/
        ///
        /// There is an undirected star graph with n nodes labeled from 1 to n. A star graph has one center node and exactly n - 1 edges connecting the center to every other node.
        ///
        /// You are given a 2D integer array edges, where edges[i] = [u_i, v_i] represents an edge between u_i and v_i. Return the center of the star graph.
        ///
        /// Image: https://assets.leetcode.com/uploads/2021/02/24/star_graph.png
        ///
        /// Example 1:
        /// Input: edges = [[1,2],[2,3],[4,2]]
        /// Output: 2
        /// Explanation: Node 2 is connected to every other node, so it is the center.
        ///
        /// Example 2:
        /// Input: edges = [[1,2],[5,1],[1,3],[1,4]]
        /// Output: 1
        ///
        /// Constraints:
        /// - 3 &lt;= n &lt;= 10^5
        /// - edges.length == n - 1
        /// - edges[i].length == 2
        /// - 1 &lt;= u_i, v_i &lt;= n
        /// - u_i != v_i
        /// - edges represents a valid star graph.
        /// </para>
        /// <para>
        /// 1791. 找出星型圖的中心節點
        /// https://leetcode.cn/problems/find-center-of-star-graph/description/
        ///
        /// 有一個由 n 個節點組成的無向星型圖，節點編號為 1 到 n。星型圖只有一個中心節點，並且恰有 n - 1 條邊將中心連接到其餘每個節點。
        ///
        /// 給定二維整數陣列 edges，其中 edges[i] = [u_i, v_i] 表示 u_i 與 v_i 之間有一條邊。回傳此星型圖的中心節點。
        ///
        /// 圖片：https://assets.leetcode.com/uploads/2021/02/24/star_graph.png
        ///
        /// 範例 1：
        /// 輸入：edges = [[1,2],[2,3],[4,2]]
        /// 輸出：2
        /// 說明：節點 2 與其他每個節點相連，因此它是中心。
        ///
        /// 範例 2：
        /// 輸入：edges = [[1,2],[5,1],[1,3],[1,4]]
        /// 輸出：1
        ///
        /// 限制條件：
        /// - 3 &lt;= n &lt;= 10^5
        /// - edges.length == n - 1
        /// - edges[i].length == 2
        /// - 1 &lt;= u_i, v_i &lt;= n
        /// - u_i != v_i
        /// - edges 表示一個有效的星型圖。
        /// </para>
        /// </summary>
        /// <remarks>
        /// 執行五組固定案例，比較三種尋找中心點的解法，並驗證所有解法都不會修改輸入陣列。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用。</param>
        private static void Main(string[] args)
        {
            int passedChecks = 0;
            int totalChecks = 0;

            AddResult(RunCase(
                "官方範例一",
                new[] { new[] { 1, 2 }, new[] { 2, 3 }, new[] { 4, 2 } },
                2));
            AddResult(RunCase(
                "官方範例二",
                new[] { new[] { 1, 2 }, new[] { 5, 1 }, new[] { 1, 3 }, new[] { 1, 4 } },
                1));
            AddResult(RunCase(
                "最小星型圖：中心在第一欄",
                new[] { new[] { 2, 1 }, new[] { 2, 3 } },
                2));
            AddResult(RunCase(
                "最小星型圖：中心在第二欄",
                new[] { new[] { 1, 3 }, new[] { 2, 3 } },
                3));
            AddResult(RunCase(
                "邊順序與中心位置交錯",
                new[]
                {
                    new[] { 6, 3 },
                    new[] { 1, 3 },
                    new[] { 3, 5 },
                    new[] { 2, 3 },
                    new[] { 3, 4 }
                },
                3));

            Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
            Environment.ExitCode = passedChecks == totalChecks ? 0 : 1;

            void AddResult((int Passed, int Total) result)
            {
                passedChecks += result.Passed;
                totalChecks += result.Total;
            }
        }

        /// <summary>
        /// 執行一組合法星型圖測試資料，依序驗證三種主要解法的答案與輸入保持不變契約。
        /// </summary>
        /// <param name="name">顯示於主控台的案例名稱。</param>
        /// <param name="edges">描述合法星型圖的邊集合，每條邊包含兩個不同節點。</param>
        /// <param name="expected">預期的中心節點編號。</param>
        /// <returns>本案例通過的檢查數與總檢查數。</returns>
        private static (int Passed, int Total) RunCase(string name, int[][] edges, int expected)
        {
            (string Name, Func<int[][], int> Solver)[] solutions =
            {
                (nameof(FindCenter), FindCenter),
                (nameof(FindCenter2), FindCenter2),
                (nameof(FindCenter3), FindCenter3)
            };
            int passedChecks = 0;

            Console.WriteLine($"Case: {name}");
            Console.WriteLine($"Edges: {FormatEdges(edges)}");
            Console.WriteLine($"Expected: {expected}");

            foreach ((string solutionName, Func<int[][], int> solver) in solutions)
            {
                int[][] solutionInput = CloneEdges(edges);
                int actual = solver(solutionInput);
                bool resultPassed = actual == expected;
                bool inputPreserved = EdgesEqual(solutionInput, edges);
                bool solutionPassed = resultPassed && inputPreserved;

                passedChecks += resultPassed ? 1 : 0;
                passedChecks += inputPreserved ? 1 : 0;
                Console.WriteLine(
                    $"{solutionName,-11} Actual: {actual} | Input preserved: {inputPreserved} | "
                    + (solutionPassed ? "PASS" : "FAIL"));
            }

            Console.WriteLine();
            return (passedChecks, solutions.Length * 2);
        }

        /// <summary>
        /// 深層複製星型圖的邊集合，讓每種解法取得互不影響的合法輸入資料。
        /// </summary>
        /// <param name="edges">要複製的邊集合。</param>
        /// <returns>外層與每條內層邊陣列皆為新實例的副本。</returns>
        private static int[][] CloneEdges(int[][] edges)
        {
            return edges.Select(edge => (int[])edge.Clone()).ToArray();
        }

        /// <summary>
        /// 比較兩份邊集合的維度、順序與節點值是否完全相同。
        /// </summary>
        /// <param name="left">第一份邊集合。</param>
        /// <param name="right">第二份邊集合。</param>
        /// <returns>兩份邊集合內容完全相同時為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
        private static bool EdgesEqual(int[][] left, int[][] right)
        {
            return left.Length == right.Length
                && left.Zip(right, (leftEdge, rightEdge) => leftEdge.SequenceEqual(rightEdge)).All(equal => equal);
        }

        /// <summary>
        /// 將邊集合格式化為容易閱讀且可重複比對的巢狀方括號表示法。
        /// </summary>
        /// <param name="edges">要顯示的邊集合。</param>
        /// <returns>例如 <c>[[1, 2], [2, 3], [4, 2]]</c> 的字串。</returns>
        private static string FormatEdges(int[][] edges)
        {
            return $"[{string.Join(", ", edges.Select(edge => $"[{string.Join(", ", edge)}]"))}]";
        }

        /// <summary>
        /// 以字典統計每個節點出現在邊集合中的次數，找出合法星型圖的中心節點。輸入必須描述
        /// 一個至少三個節點的合法星型圖，每條邊必須包含兩個不同節點；方法不修改輸入，並回傳
        /// 唯一出現在全部 <paramref name="edges"/> 中的節點編號。
        /// </summary>
        /// <remarks>
        /// 中心點與其餘每個節點相連，因此出現次數必定等於邊數。時間複雜度為 O(n)，字典所需的
        /// 額外空間複雜度為 O(n)。
        /// </remarks>
        /// <param name="edges">合法星型圖的邊集合；每條邊由兩個節點編號組成。</param>
        /// <returns>星型圖的中心節點編號。</returns>
        public static int FindCenter(int[][] edges)
        {
            Dictionary<int, int> nodeOccurrences = new();

            foreach (int[] edge in edges)
            {
                foreach (int node in edge)
                {
                    nodeOccurrences[node] = nodeOccurrences.GetValueOrDefault(node) + 1;
                }
            }

            // 中心節點是唯一出現在每一條邊的節點，因此其累計次數會等於總邊數。
            foreach (KeyValuePair<int, int> occurrence in nodeOccurrences)
            {
                if (occurrence.Value == edges.Length)
                {
                    return occurrence.Key;
                }
            }

            // 題目保證輸入是合法星型圖；此回傳值只作為契約遭破壞時的防禦性結果。
            return -1;
        }

        /// <summary>
        /// 比較前兩條邊的端點，直接找出合法星型圖的中心節點。輸入必須描述至少三個節點的
        /// 合法星型圖，因此至少存在兩條邊；方法不修改輸入，並回傳兩條邊唯一共有的節點編號。
        /// </summary>
        /// <remarks>
        /// 星型圖任意兩條邊都共享中心節點，只需固定次數的比較。時間與額外空間複雜度皆為 O(1)。
        /// </remarks>
        /// <param name="edges">合法星型圖的邊集合；至少包含兩條邊。</param>
        /// <returns>前兩條邊共有的中心節點編號。</returns>
        public static int FindCenter2(int[][] edges)
        {
            // 第一條邊的第一個端點若也出現在第二條邊，它就是中心；否則另一端點必為中心。
            return edges[0][0] == edges[1][0] || edges[0][0] == edges[1][1] ? edges[0][0] : edges[0][1];
        }

        /// <summary>
        /// 以第一條邊的兩個端點作為候選，逐邊驗證並找出合法星型圖的中心節點。輸入必須描述
        /// 一個至少三個節點的合法星型圖，每條邊必須包含兩個不同節點；方法不修改輸入，並回傳
        /// 唯一存在於每一條邊的節點編號。
        /// </summary>
        /// <remarks>
        /// 若第一個候選沒有出現在某條邊，依合法星型圖契約，第一條邊的另一端點必定是中心。
        /// 時間複雜度為 O(n)，額外空間複雜度為 O(1)。
        /// </remarks>
        /// <param name="edges">合法星型圖的邊集合；每條邊由兩個節點編號組成。</param>
        /// <returns>通過全部邊驗證的中心節點編號。</returns>
        public static int FindCenter3(int[][] edges)
        {
            int firstCandidate = edges[0][0];

            foreach (int[] edge in edges)
            {
                if (edge[0] != firstCandidate && edge[1] != firstCandidate)
                {
                    // 第一個候選遭任一條邊排除後，另一個候選依題目保證必定出現在所有邊中。
                    return edges[0][1];
                }
            }

            return firstCandidate;
        }
    }
}