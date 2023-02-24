using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1443
{
    internal class Program
    {
        //IList<int>[] adjacentNodes;
        //int[] parents;
        //bool[] visited;

        /// <summary>
        /// leetcode 1443
        /// https://leetcode.com/problems/minimum-time-to-collect-all-apples-in-a-tree/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int n = 7;

            // 不規則陣列
            int[][] edges = new int[][]
            {
                new int[] { 0, 1 },
                new int[] { 0, 2 },
                new int[] { 1, 4 },
                new int[] { 1, 5 },
                new int[] { 2, 3 },
                new int[] { 2, 6 }
            };

            bool[] visited = { false, false, true, false, true, true, false };

            Console.WriteLine(MinTime(n, edges, visited));
            Console.ReadKey();

        }

        #region method1
        /*
        /// <summary>
        /// https://leetcode.cn/problems/minimum-time-to-collect-all-apples-in-a-tree/solution/by-stormsunshine-gqlr/
        /// DFS
        /// </summary>
        /// <param name="n"></param>
        /// <param name="edges"></param>
        /// <param name="hasApple"></param>
        /// <returns></returns>
        public int MinTime(int n, int[][] edges, IList<bool> hasApple)
        {
            adjacentNodes = new IList<int>[n];
            for (int i = 0; i < n; i++)
            {
                adjacentNodes[i] = new List<int>();
            }
            foreach (int[] edge in edges)
            {
                int node0 = edge[0], node1 = edge[1];
                adjacentNodes[node0].Add(node1);
                adjacentNodes[node1].Add(node0);
            }
            parents = new int[n];
            Array.Fill(parents, -1);
            DFS(0, -1);
            visited = new bool[n];
            visited[0] = true;
            int time = 0;
            for (int i = 0; i < n; i++)
            {
                if (hasApple[i])
                {
                    time += GetTime(i);
                }
            }
            return time;
        }

        public void DFS(int node, int parent)
        {
            IList<int> adjacent = adjacentNodes[node];
            foreach (int next in adjacent)
            {
                if (next == parent)
                {
                    continue;
                }
                parents[next] = node;
                DFS(next, node);
            }
        }

        public int GetTime(int node)
        {
            int time = 0;
            while (!visited[node])
            {
                visited[node] = true;
                node = parents[node];
                time += 2;
            }
            return time;
        }
        */
        #endregion




        /// <summary>
        /// method2
        /// https://leetcode.com/problems/minimum-time-to-collect-all-apples-in-a-tree/solutions/624051/c-solution/?q=c%23&orderBy=most_relevant
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <param name="edges"></param>
        /// <param name="hasApple"></param>
        /// <returns></returns>
        public static int MinTime(int n, int[][] edges, IList<bool> hasApple)
        {
            var graph = new Dictionary<int, HashSet<int>>();
            for (int i = 0; i < n; i++)
                graph[i] = new HashSet<int>();
            foreach (var edge in edges)
            {
                int u = edge[0], v = edge[1];
                graph[u].Add(v);
                graph[v].Add(u);
            }

            return DFS(graph, 0, hasApple, new bool[n])[1];
        }
        

        private static int[] DFS(Dictionary<int, HashSet<int>> graph, int current, IList<bool> hasApple, bool[] visited)
        {
            int[] result = new int[2];
            if (hasApple[current]) result[0] = 1;
            visited[current] = true;
            foreach (var next in graph[current])
            {
                if (!visited[next])
                {
                    var nextResult = DFS(graph, next, hasApple, visited);
                    if (nextResult[0] == 1)
                    {
                        result[0] = 1;
                        result[1] += nextResult[1] + 2;
                    }
                }
            }

            return result;
        }

    }
}
