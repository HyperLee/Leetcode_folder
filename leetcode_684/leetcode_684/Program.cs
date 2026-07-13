using System;
using System.Linq;

namespace leetcode_684;

internal static class Program
{
    /// <summary>
    /// 684. Redundant Connection
    /// 684. 冗餘連線
    /// https://leetcode.com/problems/redundant-connection/
    /// https://leetcode.cn/problems/redundant-connection/
    /// A tree with one extra undirected edge is given; return the final input edge that creates a cycle.
    /// 給定一棵多了一條無向邊的樹；回傳輸入順序中最後造成環的邊。
    /// </summary>
    private static void Main()
    {
        RedundantConnectionCase[] cases =
        [
            new("Official example 1", "[[1,2], [1,3], [2,3]]", [[1, 2], [1, 3], [2, 3]], [2, 3]),
            new("Official example 2", "[[1,2], [2,3], [3,4], [1,4], [1,5]]", [[1, 2], [2, 3], [3, 4], [1, 4], [1, 5]], [1, 4]),
            new("Minimum reordered cycle", "[[3,1], [1,2], [2,3]]", [[3, 1], [1, 2], [2, 3]], [2, 3]),
            new("Long tree prefix", "[[1,2], [2,3], [3,4], [4,5], [2,5]]", [[1, 2], [2, 3], [3, 4], [4, 5], [2, 5]], [2, 5]),
            new("Preserve answer direction", "[[2,1], [3,2], [4,3], [4,1]]", [[2, 1], [3, 2], [4, 3], [4, 1]], [4, 1]),
            new("Branches meet in a cycle", "[[1,2], [1,3], [2,4], [3,5], [4,5]]", [[1, 2], [1, 3], [2, 4], [3, 5], [4, 5]], [4, 5]),
            new("Forest becomes one cycle", "[[1,2], [3,4], [2,3], [1,4]]", [[1, 2], [3, 4], [2, 3], [1, 4]], [1, 4]),
            new("100000-node spot check", "chain 1..100000 plus [1,100000]", CreateLargeChainWithCycle(100000), [1, 100000])
        ];

        CaseResult[] results = cases.Select(RunCase).ToArray();
        foreach (CaseResult result in results)
        {
            Console.WriteLine($"Case: {result.Name}");
            Console.WriteLine($"Input: {result.InputDescription}");
            Console.WriteLine($"Expected: {FormatEdge(result.Expected)}");
            Console.WriteLine($"Actual: {FormatEdge(result.Actual)}");
            Console.WriteLine($"Result: {(result.Passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        int passedCount = results.Count(result => result.Passed);
        Console.WriteLine($"Summary: {passedCount}/{results.Length} checks passed.");
        if (passedCount != results.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    private static CaseResult RunCase(RedundantConnectionCase testCase)
    {
        int[] actual = FindRedundantConnection(testCase.Edges);
        bool passed = testCase.Expected.SequenceEqual(actual);
        return new CaseResult(testCase.Name, testCase.InputDescription, testCase.Expected, actual, passed);
    }

    /// <summary>
    /// 對有效的節點編號與邊數皆為 <c>n</c> 的無向圖，依輸入順序找出第一條形成環的邊，並保留該邊原本的端點方向回傳。
    /// </summary>
    public static int[] FindRedundantConnection(int[][] edges)
    {
        int[] parent = new int[edges.Length + 1];
        int[] componentSize = new int[edges.Length + 1];
        for (int node = 1; node <= edges.Length; node++)
        {
            parent[node] = node;
            componentSize[node] = 1;
        }

        foreach (int[] edge in edges)
        {
            if (!Union(parent, componentSize, edge[0], edge[1]))
            {
                return edge;
            }
        }

        return Array.Empty<int>();
    }

    /// <summary>
    /// 尋找節點所屬集合的代表元，並以路徑壓縮將沿途節點直接連到代表元。
    /// </summary>
    private static int Find(int[] parent, int node)
    {
        if (parent[node] != node)
        {
            parent[node] = Find(parent, parent[node]);
        }

        return parent[node];
    }

    /// <summary>
    /// 依集合大小合併兩個節點的集合；若已屬於同一集合則回傳 <see langword="false"/>，否則完成合併並回傳 <see langword="true"/>。
    /// </summary>
    private static bool Union(int[] parent, int[] componentSize, int firstNode, int secondNode)
    {
        int firstRoot = Find(parent, firstNode);
        int secondRoot = Find(parent, secondNode);
        if (firstRoot == secondRoot)
        {
            return false;
        }

        // 代表元維持較大集合，避免樹高不必要地增加。
        if (componentSize[firstRoot] < componentSize[secondRoot])
        {
            (firstRoot, secondRoot) = (secondRoot, firstRoot);
        }

        parent[secondRoot] = firstRoot;
        // 只有代表元保存合併後的集合大小。
        componentSize[firstRoot] += componentSize[secondRoot];
        return true;
    }

    private static int[][] CreateLargeChainWithCycle(int nodeCount)
    {
        int[][] edges = new int[nodeCount][];
        for (int node = 1; node < nodeCount; node++)
        {
            edges[node - 1] = [node, node + 1];
        }

        edges[^1] = [1, nodeCount];
        return edges;
    }

    private static string FormatEdge(int[] edge) => $"[{string.Join(',', edge)}]";

    private sealed record RedundantConnectionCase(string Name, string InputDescription, int[][] Edges, int[] Expected);

    private sealed record CaseResult(string Name, string InputDescription, int[] Expected, int[] Actual, bool Passed);
}
