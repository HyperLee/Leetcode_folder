using System.Diagnostics;

namespace leetcode_1443;

class Program
{
    /// <summary>
    /// 1443. Minimum Time to Collect All Apples in a Tree
    /// https://leetcode.com/problems/minimum-time-to-collect-all-apples-in-a-tree/description/
    /// 1443. 收集樹上所有蘋果的最少時間
    /// https://leetcode.cn/problems/minimum-time-to-collect-all-apples-in-a-tree/description/
    ///
    /// 題目（繁體中文）：
    /// 給定一棵由 n 個節點（編號 0 到 n-1）組成的無向樹，部分節點上有蘋果。
    /// 每走過一條邊需花費 1 秒。從節點 0 出發並最終回到節點 0，
    /// 求收集樹上所有蘋果所需的最短時間（秒）。
    /// 樹的邊由 edges 陣列給出，edges[i] = [ai, bi] 表示 ai 與 bi 之間存在邊。
    /// hasApple 為布林陣列，hasApple[i] = true 表示節點 i 有蘋果，否則沒有。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");
    }

    IList<int>[] adjacentNodes;
    int[] parents;
    bool[] visited;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="n"></param>
    /// <param name="edges"></param>
    /// <param name="hasApple"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="n"></param>
    /// <param name="edges"></param>
    /// <param name="hasApple"></param>
    /// <returns></returns>
    public int MinTime(int n, int[][] edges, IList<bool> hasApple)
    {
        adjacentNodes = new IList<int>[n];
        for(int i = 0; i < n; i++)
        {
            adjacentNodes[i] = new List<int>();
        }

        foreach(int[] edge in edges)
        {
            int node0 = edge[0];
            int node1 = edge[1];
            adjacentNodes[node0].Add(node1);
            adjacentNodes[node1].Add(node0);
        }

        parents = new int[n];
        Array.Fill(parents, -1);
        DFS(0, -1);
        visited = new bool[n];
        visited[0] = true;
        int time = 0;
        for(int i = 0; i < n; i++)
        {
            if(hasApple[i])
            {
                time += GetTime(i);
            }
        }
        return time;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <param name="parent"></param> <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <param name="parent"></param>
    public void DFS(int node, int parent)
    {
        IList<int> neighbors = adjacentNodes[node];
        foreach(int neighbor in neighbors)
        {
            if(neighbor == parent)
            {
                continue;
            }
            parents[neighbor] = node;
            DFS(neighbor, node);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public int GetTime(int node)
    {
        int time = 0;
        while(!visited[node])
        {
            visited[node] = true;
            node = parents[node];
            time += 2;
        }
        return time;
    }
}
