using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1971
{
    internal class Program
    {
        /// <summary>
        /// leetcode 1971
        /// https://leetcode.com/problems/find-if-path-exists-in-graph/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
        }


        /// <summary>
        /// https://leetcode.cn/problems/find-if-path-exists-in-graph/solution/xun-zhao-tu-zhong-shi-fou-cun-zai-lu-jin-d0q0/
        /// 深度優先 DFS
        /// 
        /// https://leetcode.com/problems/find-if-path-exists-in-graph/solutions/2715942/find-if-path-exists-in-graph/
        /// </summary>
        /// <param name="n"></param>
        /// <param name="edges"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public bool ValidPath(int n, int[][] edges, int source, int destination)
        {
            IList<int>[] adj = new IList<int>[n];
            for (int i = 0; i < n; i++)
            {
                adj[i] = new List<int>();
            }
            foreach (int[] edge in edges)
            {
                int x = edge[0], y = edge[1];
                adj[x].Add(y);
                adj[y].Add(x);
            }
            bool[] visited = new bool[n];
            return DFS(source, destination, adj, visited);
        }

        public bool DFS(int source, int destination, IList<int>[] adj, bool[] visited)
        {
            if (source == destination)
            {
                return true;
            }
            visited[source] = true;
            foreach (int next in adj[source])
            {
                if (!visited[next] && DFS(next, destination, adj, visited))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
