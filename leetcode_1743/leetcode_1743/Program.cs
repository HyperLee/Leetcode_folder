using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1743
{
    internal class Program
    {
        /// <summary>
        /// 1743. Restore the Array From Adjacent Pairs
        /// https://leetcode.com/problems/restore-the-array-from-adjacent-pairs/?envType=daily-question&envId=2023-11-10
        /// 1743. 从相邻元素对还原数组
        /// https://leetcode.cn/problems/restore-the-array-from-adjacent-pairs/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
        }


        /// <summary>
        /// 官方解法:
        /// https://leetcode.cn/problems/restore-the-array-from-adjacent-pairs/solutions/894557/cong-xiang-lin-yuan-su-dui-huan-yuan-shu-v55t/
        /// 
        /// </summary>
        /// <param name="adjacentPairs"></param>
        /// <returns></returns>
        public int[] RestoreArray(int[][] adjacentPairs)
        {
            Dictionary<int, IList<int>> dictionary = new Dictionary<int, IList<int>>();
            foreach (int[] adjacentpair in adjacentPairs)
            {
                if (!dictionary.ContainsKey(adjacentpair[0]))
                {
                    dictionary.Add(adjacentpair[0], new List<int>());
                }

                if (!dictionary.ContainsKey(adjacentpair[1]))
                {
                    dictionary.Add(adjacentpair[1], new List<int>());
                }

                dictionary[adjacentpair[0]].Add(adjacentpair[1]);
                dictionary[adjacentpair[1]].Add(adjacentpair[0]);
            }

            int n = adjacentPairs.Length + 1;
            int[] ret = new int[n];
            foreach(KeyValuePair<int, IList<int>> pair in dictionary)
            {
                int e = pair.Key;
                IList<int> adj = pair.Value;
                if(adj.Count == 1)
                {
                    ret[0] = e;
                    break;
                }
            }

            ret[1] = dictionary[ret[0]][0];
            for(int i = 2; i < n; i++)
            {
                IList<int> adj = dictionary[ret[i - 1]];
                ret[i] = ret[i - 2] == adj[0] ? adj[1] : adj[0];
            }

            return ret;
        }

    }
}
