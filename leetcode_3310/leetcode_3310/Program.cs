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
        Console.WriteLine("Hello, World!");
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
    /// </summary>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <param name="invocations"></param>
    /// <returns></returns>
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
    /// DFS 遍历图，标记所有可疑方法
    /// </summary>
    /// <param name="node"></param>
    /// <param name="graph"></param>
    /// <param name="isSuspicious"></param> <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <param name="graph"></param>
    /// <param name="isSuspicious"></param>
    private void DFS(int node, List<int>[] graph, bool[] isSuspicious)
    {
        isSuspicious[node] = true;
        foreach(int neighbor in graph[node])
        {
            // 避免重複走訪及無限遞迴
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
    /// </summary>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <param name="invocations"></param>
    /// <returns></returns>
    public IList<int> RemainingMethods2(int n, int k, int[][] invocations)
    {
        List<int>[] edges = new List<int>[n];
        for (int i = 0; i < n; i++) {
            edges[i] = new List<int>();
        }
        int[] inDegree = new int[n];

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

                if (!suspicious[v]) {
                    queue.Enqueue(v);
                    suspicious[v] = true;
                }
            }
        }

        bool canRemoveAll = true;
        List<int> remaining = new List<int>();

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
