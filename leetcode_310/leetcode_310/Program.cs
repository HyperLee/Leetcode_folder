namespace leetcode_310
{
    internal class Program
    {
        /// <summary>
        /// 310. Minimum Height Trees
        /// https://leetcode.com/problems/minimum-height-trees/description/?envType=daily-question&envId=2024-04-23
        /// 310. 最小高度树
        /// https://leetcode.cn/problems/minimum-height-trees/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int n = 4;
            int[][] input = new int[][]
            {
                 new int[]{ 1, 0 },
                 new int[]{ 1, 2 },
                 new int[]{ 1, 3 }
            };

            var res = FindMinHeightTrees(n, input);
            foreach (int i in res) 
            {
                Console.WriteLine(i);
            }
            Console.ReadKey();
        }



        /// <summary>
        /// ref: 官方 DFS
        /// https://leetcode.cn/problems/minimum-height-trees/solutions/1395249/zui-xiao-gao-du-shu-by-leetcode-solution-6v6f/
        /// 
        /// https://leetcode.cn/problems/minimum-height-trees/solutions/1961777/by-stormsunshine-fv74/
        /// https://leetcode.cn/problems/minimum-height-trees/solutions/242910/zui-rong-yi-li-jie-de-bfsfen-xi-jian-dan-zhu-shi-x/
        /// </summary>
        /// <param name="n"></param>
        /// <param name="edges"></param>
        /// <returns></returns>
        public static IList<int> FindMinHeightTrees(int n, int[][] edges)
        {
            IList<int> ans = new List<int>();
            if(n == 1)
            {
                // 只有一個node, 直接回傳最短距離 0
                ans.Add(0);
                return ans;
            }

            // 相鄰的node
            IList<int>[] adj = new List<int>[n];
            for(int i = 0; i < n; i++)
            {
                adj[i] = new List<int>();
            }

            // 無向圖, 邊走向  [ai, bi]
            foreach (int[] edge in edges)
            {
                // 前[ai]
                adj[edge[0]].Add(edge[1]);
                // 後[bi]
                adj[edge[1]].Add(edge[0]);
            }

            int[] parent = new int[n];
            
            // 初始化 都塞 -1
            Array.Fill(parent, -1);
            
            // 找出與節點 0 最遠節點 x
            int x = FindLongestNode(0, parent, adj);
            
            // 找出與節點 x 最遠節點 y
            int y = FindLongestNode(x, parent, adj);
            
            // 求出節點 x 到 y 路徑
            IList<int> path = new List<int>();
            parent[x] = -1;

            while (y != -1)
            {
                path.Add(y);
                y = parent[y];
            }
            
            int m = path.Count;
            if (m % 2 == 0)
            {
                ans.Add(path[m / 2 - 1]);
            }
            
            ans.Add(path[m / 2]);

            return ans;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="parent"></param>
        /// <param name="adj"></param>
        /// <returns></returns>
        public static int FindLongestNode(int u, int[] parent, IList<int>[] adj)
        {
            int n = adj.Length;
            int[] dist = new int[n];
            Array.Fill(dist, -1);
            dist[u] = 0;
            DFS(u, dist, parent, adj);
            int maxdist = 0;
            int node = -1;

            for(int i = 0; i < n; i++)
            {
                if (dist[i] > maxdist)
                {
                    maxdist = dist[i];
                    node = i;
                }
            }

            return node;
        }

        /// <summary>
        /// DFS
        /// </summary>
        /// <param name="u"></param>
        /// <param name="dist"></param>
        /// <param name="parent"></param>
        /// <param name="adj"></param>
        public static void DFS(int u, int[] dist, int[] parent, IList<int>[] adj)
        {
            foreach(int v in adj[u])
            {
                if (dist[v] < 0)
                {
                    dist[v] = dist[u] + 1;
                    parent[v] = u;
                    DFS(v, dist, parent, adj);
                }
            }
        }
    }
}
