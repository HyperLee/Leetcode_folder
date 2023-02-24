using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_947
{
    internal class Program
    {
        /// <summary>
        /// https://leetcode.com/problems/most-stones-removed-with-same-row-or-column/
        /// leetcode 947
        /// https://leetcode.cn/problems/most-stones-removed-with-same-row-or-column/
        /// 
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/arrays/jagged-arrays
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //int[][] stone = new int[][] { { 0 }, { 0 } };

            //Console.WriteLine(RemoveStones(stone));
        }

        /// <summary>
        /// https://leetcode.cn/problems/most-stones-removed-with-same-row-or-column/solution/guan-fang-bing-cha-ji-dai-ma-by-zy4889-d9co/
        /// </summary>
        /// <param name="stones"></param>
        /// <returns></returns>
        public static int RemoveStones(int[][] stones)
        {
            UnionFind0 unionFind = new UnionFind0();

            foreach (int[] stone in stones)
            {
                unionFind.Union(stone[0] + 10001, stone[1]);//用加法区分行列
            }
            return stones.Length - unionFind.Count;
        }

        private class UnionFind0
        {
            public int Count { get; private set; }//圈数量
            Dictionary<int, int> parent;
            public UnionFind0()
            {
                parent = new Dictionary<int, int>();
                Count = 0;
            }
            public int Find(int x)
            {
                if (!parent.ContainsKey(x)) { parent[x] = x; Count++; }//新值
                if (x != parent[x]) parent[x] = Find(parent[x]);
                return parent[x];
            }
            public void Union(int x, int y)//联合两个值
            {
                int rootX = Find(x);//找根索引
                int rootY = Find(y);//找根索引
                if (rootX == rootY) return;
                parent[rootX] = rootY;//直接x并入y?
                Count--;
            }
        }


    }
}
