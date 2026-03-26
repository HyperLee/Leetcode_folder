namespace leetcode_947;

class Program
{
    /// <summary>
    /// 947. Most Stones Removed with Same Row or Column
    /// https://leetcode.com/problems/most-stones-removed-with-same-row-or-column/description/
    /// 947. 移除最多的同行或同列石頭
    /// https://leetcode.cn/problems/most-stones-removed-with-same-row-or-column/description/
    /// 
    /// On a 2D plane, we place n stones at some integer coordinate points.
    /// Each coordinate point may have at most one stone.
    /// A stone can be removed if it shares either the same row or the same column
    /// as another stone that has not been removed.
    /// Given an array stones of length n where stones[i] = [xi, yi] represents
    /// the location of the ith stone, return the largest possible number of stones
    /// that can be removed.
    /// 
    /// 在二維平面上，我們在整數座標點上放置了 n 顆石頭。
    /// 每個座標點最多只能有一顆石頭。
    /// 如果一顆石頭與另一顆尚未被移除的石頭共享同一行或同一列，則該石頭可以被移除。
    /// 給定一個長度為 n 的陣列 stones，其中 stones[i] = [xi, yi] 表示第 i 顆石頭的位置，
    /// 請回傳最多可以移除的石頭數量。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new();

        // 範例 1：stones = [[0,0],[0,1],[1,0],[1,2],[2,1],[2,2]]，預期輸出：5
        int[][] stones1 =
        [
            [0, 0], [0, 1], [1, 0], [1, 2], [2, 1], [2, 2]
        ];
        Console.WriteLine($"範例 1：{program.RemoveStones(stones1)}"); // 5

        // 範例 2：stones = [[0,0],[0,2],[1,1],[2,0],[2,2]]，預期輸出：3
        int[][] stones2 =
        [
            [0, 0], [0, 2], [1, 1], [2, 0], [2, 2]
        ];
        Console.WriteLine($"範例 2：{program.RemoveStones(stones2)}"); // 3

        // 範例 3：stones = [[0,0]]，預期輸出：0
        int[][] stones3 = [[0, 0]];
        Console.WriteLine($"範例 3：{program.RemoveStones(stones3)}"); // 0

        Console.WriteLine();
        Console.WriteLine("--- 解法二：並查集 — 預先初始化 + 按秩合併 ---");
        Console.WriteLine($"範例 1：{program.RemoveStones2(stones1)}"); // 5
        Console.WriteLine($"範例 2：{program.RemoveStones2(stones2)}"); // 3
        Console.WriteLine($"範例 3：{program.RemoveStones2(stones3)}"); // 0
    }

    /// <summary>
    /// 解法一：並查集（Union-Find） — 懶初始化
    /// 解題思路：
    /// 將二維座標平面上的石頭視為圖的頂點，若兩顆石頭同行或同列，則它們之間存在一條邊。
    /// 根據移除規則，一個連通分量中的所有石頭最終只需保留一顆，
    /// 因此「最多可移除的石頭數 = 石頭總數 - 連通分量個數」。
    ///
    /// 使用並查集（Union-Find）將每顆石頭的「行座標」與「列座標」合併，
    /// 代表所有共享同一行或同一列的石頭屬於同一個連通分量。
    /// 為了在一維並查集中區分行與列，將行座標加上 10001 進行映射（因為 0 <= x, y <= 10^4）。
    /// </summary>
    /// <param name="stones">石頭座標陣列，stones[i] = [xi, yi]</param>
    /// <returns>最多可以移除的石頭數量</returns>
    /// <example>
    /// <code>
    /// int[][] stones = [[0,0],[0,1],[1,0],[1,2],[2,1],[2,2]];
    /// RemoveStones(stones); // 回傳 5
    /// </code>
    /// </example>
    public int RemoveStones(int[][] stones)
    {
        UnionFind0 unionFind = new UnionFind0();

        foreach (int[] stone in stones)
        {
            // 將行座標 (stone[0]) 加上 10001，與列座標 (stone[1]) 區分開來，
            // 然後在並查集中合併，使同行或同列的石頭歸屬同一個連通分量
            unionFind.Union(stone[0] + 10001, stone[1]);
        }

        // 石頭總數減去連通分量個數，即為最多可移除的石頭數
        return stones.Length - unionFind.Count;
    }

    /// <summary>
    /// 並查集（Union-Find）資料結構。
    /// 使用 Dictionary 作為底層儲存，支援動態新增元素（懶初始化）。
    /// 每當遇到新元素時自動建立節點並增加連通分量計數；
    /// 合併兩個不同連通分量時減少計數。
    /// 透過路徑壓縮（Path Compression）優化 Find 操作的效率。
    /// </summary>
    private class UnionFind0
    {
        /// <summary>目前連通分量的個數</summary>
        public int Count { get; private set; }

        /// <summary>parent[x] 記錄節點 x 的父節點，根節點的父節點為自身</summary>
        private readonly Dictionary<int, int> parent;

        /// <summary>
        /// 初始化並查集，起始時無任何元素，連通分量數為 0。
        /// </summary>
        public UnionFind0()
        {
            Count = 0;
            parent = new Dictionary<int, int>();
        }

        /// <summary>
        /// 尋找節點 x 的根節點（代表元素）。
        /// 若 x 尚未存在於並查集中，則自動建立新節點（懶初始化），
        /// 並將連通分量數加 1。
        /// 使用遞迴式路徑壓縮：在查找過程中，將沿途所有節點直接指向根節點，
        /// 使後續查詢接近 O(1)。
        /// </summary>
        /// <param name="x">欲查找根節點的元素</param>
        /// <returns>x 所屬連通分量的根節點</returns>
        public int Find(int x)
        {
            // 懶初始化：若 x 是新元素，將自己設為根節點，連通分量數 +1
            if (!parent.ContainsKey(x))
            {
                parent[x] = x;
                Count++;
            }

            // 路徑壓縮：遞迴查找根節點，並將 x 的父節點直接指向根節點
            if (parent[x] != x)
            {
                parent[x] = Find(parent[x]);
            }

            return parent[x];
        }

        /// <summary>
        /// 合併節點 x 與節點 y 所屬的連通分量。
        /// 若 x 與 y 已在同一連通分量中則不做任何操作；
        /// 否則將 x 的根節點掛到 y 的根節點下，並將連通分量數減 1。
        /// </summary>
        /// <param name="x">第一個欲合併的元素</param>
        /// <param name="y">第二個欲合併的元素</param>
        public void Union(int x, int y)
        {
            // 分別找到 x 和 y 的根節點
            int rootX = Find(x);
            int rootY = Find(y);

            // 若根節點不同，代表屬於不同連通分量，進行合併
            if (rootX != rootY)
            {
                // 將 x 的根節點指向 y 的根節點，完成合併
                parent[rootX] = rootY;
                Count--;
            }
        }
    }    

    /// <summary>
    /// 解題思路（解法二）：
    /// 與解法一相同的核心邏輯：最多可移除石頭數 = 石頭總數 - 連通分量個數。
    ///
    /// 不同之處在於並查集的實作方式：
    /// - 將每顆石頭視為一個節點（索引 0 ~ n-1），使用陣列預先初始化。
    /// - 透過 Dictionary 將同行或同列的石頭分組，再將同組石頭合併。
    /// - 同時使用路徑壓縮與按秩合併兩種優化，達到最佳均攤時間複雜度 O(α(n))。
    /// </summary>
    /// <param name="stones">石頭座標陣列，stones[i] = [xi, yi]</param>
    /// <returns>最多可以移除的石頭數量</returns>
    /// <example>
    /// <code>
    /// int[][] stones = [[0,0],[0,1],[1,0],[1,2],[2,1],[2,2]];
    /// RemoveStones2(stones); // 回傳 5
    /// </code>
    /// </example>
    public int RemoveStones2(int[][] stones)
    {
        int n = stones.Length;
        UnionFind1 unionFind = new UnionFind1(n);

        // 使用 Dictionary 將同行或同列的石頭索引分組
        Dictionary<int, List<int>> rowGroups = new();
        Dictionary<int, List<int>> colGroups = new();

        for (int i = 0; i < n; i++)
        {
            int row = stones[i][0];
            int col = stones[i][1];

            if (!rowGroups.ContainsKey(row))
            {
                rowGroups[row] = new List<int>();
            }
            rowGroups[row].Add(i);

            if (!colGroups.ContainsKey(col))
            {
                colGroups[col] = new List<int>();
            }
            colGroups[col].Add(i);
        }

        // 將同行的石頭合併至同一連通分量
        foreach (List<int> group in rowGroups.Values)
        {
            for (int i = 1; i < group.Count; i++)
            {
                unionFind.Union(group[0], group[i]);
            }
        }

        // 將同列的石頭合併至同一連通分量
        foreach (List<int> group in colGroups.Values)
        {
            for (int i = 1; i < group.Count; i++)
            {
                unionFind.Union(group[0], group[i]);
            }
        }

        // 石頭總數減去連通分量個數，即為最多可移除的石頭數
        return n - unionFind.Count;
    }

    /// <summary>
    /// 並查集（Union-Find）資料結構（解法二）。
    /// 使用陣列預先初始化，支援路徑壓縮與按秩合併兩種優化。
    /// 均攤時間複雜度為 O(α(n))，其中 α 為反阿克曼函式。
    /// </summary>
    private class UnionFind1
    {
        /// <summary>目前連通分量的個數</summary>
        public int Count { get; private set; }

        /// <summary>parent[x] 記錄節點 x 的父節點，根節點的父節點為自身</summary>
        private readonly int[] parent;

        /// <summary>rank[x] 記錄以 x 為根的樹的秩（近似高度），用於按秩合併</summary>
        private readonly int[] rank;

        /// <summary>
        /// 初始化並查集，建立 n 個獨立的集合（元素 0 ~ n-1）。
        /// </summary>
        /// <param name="n">元素總數</param>
        public UnionFind1(int n)
        {
            Count = n;
            parent = new int[n];
            rank = new int[n];
            for (int i = 0; i < n; i++)
            {
                parent[i] = i;
            }
        }

        /// <summary>
        /// 查找元素 x 的根節點（代表元素），使用遞迴式路徑壓縮。
        /// </summary>
        /// <param name="x">欲查找根節點的元素</param>
        /// <returns>x 所屬連通分量的根節點</returns>
        public int Find(int x)
        {
            if (parent[x] != x)
            {
                parent[x] = Find(parent[x]);
            }

            return parent[x];
        }

        /// <summary>
        /// 合併元素 x 與元素 y 所屬集合，使用按秩合併優化。
        /// 將較矮的樹掛到較高的樹下方，避免樹高度持續增長。
        /// </summary>
        /// <param name="x">第一個欲合併的元素</param>
        /// <param name="y">第二個欲合併的元素</param>
        /// <returns>是否實際執行了合併（false 表示已在同一集合）</returns>
        public bool Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);
            if (rootX == rootY)
            {
                return false;
            }

            // 按秩合併：將較矮的樹掛到較高的樹下
            if (rank[rootX] < rank[rootY])
            {
                parent[rootX] = rootY;
            }
            else if (rank[rootX] > rank[rootY])
            {
                parent[rootY] = rootX;
            }
            else
            {
                parent[rootY] = rootX;
                rank[rootX]++;
            }

            Count--;
            return true;
        }
    }


}
