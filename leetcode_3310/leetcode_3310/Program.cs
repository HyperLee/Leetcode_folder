namespace leetcode_3310;

class Program
{
    /// <summary>
    /// https://leetcode.com/problems/remove-methods-from-project/description/
    /// 3310. Remove Methods From Project
    /// https://leetcode.cn/problems/remove-methods-from-project/description/
    /// 3310. 移除可疑的方法
    ///
    /// English:
    /// You are maintaining a project that has n methods numbered from 0 to n - 1.
    ///
    /// You are given two integers n and k, and a 2D integer array invocations, where invocations[i] = [ai, bi] indicates that method ai invokes method bi.
    ///
    /// There is a known bug in method k. Method k, along with any method invoked by it, either directly or indirectly, are considered suspicious and we aim to remove them.
    ///
    /// A group of methods can only be removed if no method outside the group invokes any methods within it.
    ///
    /// Return an array containing all the remaining methods after removing all the suspicious methods. You may return the answer in any order. If it is not possible to remove all the suspicious methods, none should be removed.
    ///
    /// 繁體中文：
    /// 你正在維護一個共有 n 個方法的專案，這些方法的編號從 0 到 n - 1。
    ///
    /// 給定兩個整數 n 和 k，以及一個二維整數陣列 invocations，其中 invocations[i] = [ai, bi] 表示方法 ai 會呼叫方法 bi。
    ///
    /// 方法 k 已知存在錯誤。方法 k，以及所有由它直接或間接呼叫的方法，都被視為可疑方法，我們希望移除它們。
    ///
    /// 只有在群組外沒有任何方法呼叫群組內的方法時，才能移除一組方法。
    ///
    /// 請回傳移除所有可疑方法後的所有剩餘方法。答案可以按照任意順序回傳。如果無法移除所有可疑方法，則不移除任何方法。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new();
        (string Name, int N, int K, int[][] Invocations, int[] Expected)[] testCases =
        [
            (
                "Example 1 - external callers block removal",
                4,
                1,
                new int[][]
                {
                    new[] { 1, 2 },
                    new[] { 0, 1 },
                    new[] { 3, 2 }
                },
                new[] { 0, 1, 2, 3 }
            ),
            (
                "Example 2 - suspicious group can be removed",
                5,
                0,
                new int[][]
                {
                    new[] { 1, 2 },
                    new[] { 0, 2 },
                    new[] { 0, 1 },
                    new[] { 3, 4 }
                },
                new[] { 3, 4 }
            ),
            (
                "Example 3 - every method is suspicious",
                3,
                2,
                new int[][]
                {
                    new[] { 1, 2 },
                    new[] { 0, 1 },
                    new[] { 2, 0 }
                },
                Array.Empty<int>()
            ),
            (
                "Boundary - no invocations",
                5,
                2,
                Array.Empty<int[]>(),
                new[] { 0, 1, 3, 4 }
            ),
            (
                "Boundary - suspicious cycle has an external caller",
                4,
                2,
                new int[][]
                {
                    new[] { 2, 3 },
                    new[] { 3, 2 },
                    new[] { 0, 2 }
                },
                new[] { 0, 1, 2, 3 }
            )
        ];

        int passed = 0;
        foreach ((string name, int n, int k, int[][] invocations, int[] expected) in testCases)
        {
            passed += RunTestCase(solution, name, n, k, invocations, expected);
        }

        Console.WriteLine($"Summary: {passed}/{testCases.Length * 2} passed.");
    }

    /// <summary>
    /// 執行一組固定圖案例，分別呼叫 DFS 與 BFS 兩種解法，並以排序後的節點集合比較結果。
    /// 輸入必須符合題目限制：方法數量介於 1 到 100000，k 是合法方法編號，且 invocation
    /// 的端點都落在方法範圍內。此 runner 會列印預期結果、兩種實際結果與 PASS/FAIL，並回傳
    /// 通過的解法數量。
    /// </summary>
    /// <param name="solution">包含兩種解法的 <see cref="Program"/> 實例。</param>
    /// <param name="name">顯示在主控台上的案例名稱。</param>
    /// <param name="n">專案中的方法總數。</param>
    /// <param name="k">已知有 bug 的起始方法編號。</param>
    /// <param name="invocations">有向呼叫邊，每條邊表示來源方法呼叫目標方法。</param>
    /// <param name="expected">此案例預期留下的方法編號；順序不影響比較。</param>
    /// <returns>兩種解法中通過驗證的數量，範圍為 0 到 2。</returns>
    private static int RunTestCase(
        Program solution,
        string name,
        int n,
        int k,
        int[][] invocations,
        int[] expected)
    {
        IList<int> dfsResult = solution.RemainingMethods(n, k, CloneInvocations(invocations));
        IList<int> bfsResult = solution.RemainingMethods2(n, k, CloneInvocations(invocations));
        bool dfsPassed = MethodsMatch(dfsResult, expected);
        bool bfsPassed = MethodsMatch(bfsResult, expected);

        Console.WriteLine($"{name}: Expected = {FormatMethods(expected)}");
        Console.WriteLine($"  RemainingMethods:  Actual = {FormatMethods(dfsResult)} ({(dfsPassed ? "PASS" : "FAIL")})");
        Console.WriteLine($"  RemainingMethods2: Actual = {FormatMethods(bfsResult)} ({(bfsPassed ? "PASS" : "FAIL")})");
        Console.WriteLine();

        return (dfsPassed ? 1 : 0) + (bfsPassed ? 1 : 0);
    }

    /// <summary>
    /// 比較兩組方法編號集合。題目允許答案以任意順序回傳，因此比較前會先排序。
    /// </summary>
    /// <param name="actual">解法實際回傳的方法編號。</param>
    /// <param name="expected">案例預期的方法編號。</param>
    /// <returns>兩組集合內容相同時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
    private static bool MethodsMatch(IEnumerable<int> actual, IEnumerable<int> expected)
    {
        return actual.OrderBy(method => method).SequenceEqual(expected.OrderBy(method => method));
    }

    /// <summary>
    /// 複製 invocation 的每條邊，讓兩種解法在 runner 中使用彼此獨立的輸入資料。
    /// </summary>
    /// <param name="invocations">原始有向呼叫邊。</param>
    /// <returns>內容相同但內層陣列獨立的新 invocation 陣列。</returns>
    private static int[][] CloneInvocations(int[][] invocations)
    {
        return invocations.Select(edge => edge.ToArray()).ToArray();
    }

    /// <summary>
    /// 將方法編號集合格式化成升冪的主控台文字，方便閱讀與 README transcript 對照。
    /// </summary>
    /// <param name="methods">要格式化的方法編號集合。</param>
    /// <returns>例如 <c>[0, 1, 2]</c> 的文字結果。</returns>
    private static string FormatMethods(IEnumerable<int> methods)
    {
        return $"[{string.Join(", ", methods.OrderBy(method => method))}]";
    }

    /// <summary>
    /// 题意
    /// 一个项目有 n 个方法，其中第 k 个方法有 bug。可能是第 k 个方法自己的 bug，也可能是第 k 个方法调用的更底层的方法有 bug。
    /// 你需要删除所有可能有 bug 的方法。如果删除后无法编译（剩余的方法调用了删除的方法），那么返回数组 [0,1,2,⋯,n−1]。
    /// 如果可以正常删除，返回剩余的方法编号。
    /// 
    /// 思路
    /// 1. 从 k 开始 DFS 图，标记所有可能有 bug 的方法（节点）。题目把这些方法叫做可疑方法。
    /// 2. 遍历 invocations，如果存在从「非可疑方法」到「可疑方法」的边，则删除后无法编译，返回数组 [0,1,2,⋯,n−1]。
    /// 3. 否则可以正常删除，把非可疑方法加入答案。
    /// 注意：图中可能有环，为避免 DFS 无限递归下去，只需 DFS 没有访问过（没有被标记）的节点。
    ///
    /// 本方法先建立有向鄰接表，再從 k 遞迴找出所有可疑方法，最後掃描呼叫邊判斷是否有
    /// 非可疑方法呼叫可疑方法。輸入必須符合題目限制：1 <= n <= 100000、0 <= k < n，
    /// 且每條邊的端點都落在 [0, n - 1]。若可疑方法能被安全移除，回傳其餘方法；否則回傳全部方法。
    /// </summary>
    /// <param name="n">專案中的方法數量。</param>
    /// <param name="k">已知有 bug 的起始方法編號。</param>
    /// <param name="invocations">有向呼叫邊陣列，<c>[a, b]</c> 表示方法 a 呼叫方法 b。</param>
    /// <returns>可移除可疑方法時回傳剩餘方法；若存在外部呼叫則回傳所有方法。</returns>
    public IList<int> RemainingMethods(int n, int k, int[][] invocations)
    {
        var graph = new List<int>[n];

        for(int i = 0; i < n; i++)
        {
            graph[i] = new List<int>();
        }

        foreach(int[] edge in invocations)
        {
            graph[edge[0]].Add(edge[1]);
        }

        // 標記所有可疑方法
        bool[] isSuspicious = new bool[n];
        DFS(k, graph, isSuspicious);

        // 檢查是否存在「非可疑方法 -> 可疑方法」的邊
        foreach(int[] edge in invocations)
        {
            if(!isSuspicious[edge[0]] && isSuspicious[edge[1]])
            {
                // 無法移除可疑方法，回傳所有方法
                var res = new List<int>(n);
                for(int i = 0; i < n; i++)
                {
                    res.Add(i);
                }
                return res;
            }
        }

        // 移除所有可疑方法
        var ans = new List<int>();
        for(int i = 0; i < n; i++)
        {
            if(!isSuspicious[i])
            {
                ans.Add(i);
            }
        }
        return ans;
    }

    /// <summary>
    /// 從指定節點沿著有向邊遞迴探索，標記所有可直接或間接到達的可疑方法。
    /// graph 必須以方法編號為索引，isSuspicious 用來記錄已訪問節點，避免圖中的環造成重複走訪或無限遞迴。
    /// </summary>
    /// <param name="node">目前要探索的方法編號。</param>
    /// <param name="graph">以來源方法編號索引的有向鄰接表。</param>
    /// <param name="isSuspicious">長度為 n 的訪問標記陣列，會直接更新可疑方法狀態。</param>
    private void DFS(int node, List<int>[] graph, bool[] isSuspicious)
    {
        isSuspicious[node] = true;
        foreach(int neighbor in graph[node])
        {
            // 只遞迴尚未標記的節點，讓 DFS 能安全處理重疊路徑與有向環。
            if(!isSuspicious[neighbor])
            {
                DFS(neighbor, graph, isSuspicious);
            }
        }
    }

    /// <summary>
    /// 解法二: 搜索
    /// 给定的 invocations 数组实际上定义了一个有向图。给定节点 k，将这个图中的节点 k 自身以及通过节点 k 能到达的节点称为
    /// 「可疑方法」。按题意，我们实际上需要判断是否存在调用了「可疑方法」的普通方法。也就是说，是否不存在从普通节点连向
    /// 「可疑方法」的边，只有满足这个条件时，才能移除所有「可疑方法」。
    /// 
    /// 首先，我们需要找出所有的「可疑方法」。以节点 k 作为起点，使用深度优先搜索或广度优先搜索，在图上不重复地遍历即可。
    /// 然后我们需要判断是否还有其他节点可以到达这些节点，此时有两种思路：
    /// - 统计每个节点的入度，在遍历的时候将目标节点的入度减一，相当于移除这次遍历所用的边。等找到所有的「可疑方法」后，
    /// 节点的入度就代表连向该节点的普通节点数量。此时遍历所有的「可疑方法」，如果某个节点的入度不为 0，说明还有外部的
    /// 节点可以到达「可疑方法」。
    /// - 遍历 invocations，如果存在从某个普通节点指向「可疑方法」的边，则说明存在能到达「可疑方法」的其他节点。我们可以
    /// 使用哈希表来快速判断节点是否属于「可疑方法」集合
    /// 最后按题意，分为两种情况处理：
    /// 如果没有其他任何节点能达到这些「可疑方法」，那么就返回移除这些节点后剩余的节点。
    /// 否则就返回全部节点。
    ///
    /// 本方法使用 BFS 找出可疑集合，同時將可疑來源所使用的邊從入度中扣除；BFS 結束後，
    /// 可疑節點仍保有的入度只代表來自非可疑方法的外部呼叫。輸入必須符合題目限制：
    /// 1 <= n <= 100000、0 <= k < n，且 invocation 端點皆在合法方法範圍內；成功時回傳非可疑方法，
    /// 失敗時回傳全部方法。
    ///
    /// </summary>
    /// <param name="n">專案中的方法數量。</param>
    /// <param name="k">已知有 bug 的起始方法編號。</param>
    /// <param name="invocations">有向呼叫邊陣列，<c>[a, b]</c> 表示方法 a 呼叫方法 b。</param>
    /// <returns>可移除可疑方法時回傳剩餘方法；若存在外部呼叫則回傳所有方法。</returns>
    public IList<int> RemainingMethods2(int n, int k, int[][] invocations)
    {
        List<int>[] edges = new List<int>[n];
        for (int i = 0; i < n; i++) {
            edges[i] = new List<int>();
        }
        int[] inDegree = new int[n];

        // 先記錄完整入度；BFS 處理可疑來源時會扣除可疑區域內的邊，留下外部呼叫數量。
        foreach (var inv in invocations) {
            edges[inv[0]].Add(inv[1]);
            inDegree[inv[1]]++;
        }

        Queue<int> queue = new Queue<int>();
        queue.Enqueue(k);
        bool[] suspicious = new bool[n];
        suspicious[k] = true;

        while (queue.Count > 0) {
            int u = queue.Dequeue();
            foreach (int v in edges[u]) {
                inDegree[v]--;

                // 每個節點只入隊一次；即使圖中有環，也只會標記一次並終止。
                if (!suspicious[v]) {
                    queue.Enqueue(v);
                    suspicious[v] = true;
                }
            }
        }

        bool canRemoveAll = true;
        List<int> remaining = new List<int>();

        // 可疑節點仍有入度，表示有未刪除的方法呼叫它，整個可疑集合不能移除。
        for (int i = 0; i < n; i++) {
            if (suspicious[i] && inDegree[i] > 0) {
                canRemoveAll = false;
                break;
            } else if (!suspicious[i]) {
                remaining.Add(i);
            }
        }

        if (!canRemoveAll) {
            List<int> allNodes = new List<int>(n);
            for (int i = 0; i < n; i++) {
                allNodes.Add(i);
            }
            return allNodes;
        }

        return remaining;
    }
}
