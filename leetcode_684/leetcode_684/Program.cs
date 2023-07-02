using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_684
{
    internal class Program
    {
        /// <summary>
        /// 684. Redundant Connection
        /// https://leetcode.com/problems/redundant-connection/
        /// 684. 冗余连接
        /// https://leetcode.cn/problems/redundant-connection/
        /// 
        /// 本題目要求 , node 有很多連結
        /// 當刪除一些連接(也就是邊) 還是可以每個 node 都有聯結的
        /// 情況下. 也可理解為 刪除 冗余 or 多餘 的邊
        /// 
        /// 
        /// 輸入array 為 不規則陣列
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/arrays/jagged-arrays
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] inputArray = new int[][]
            {
                new int[] { 1, 2 },
                new int[] { 1, 3 },
                new int[] { 2, 3 }
            };

            //Console.WriteLine(FindRedundantConnection(inputArray));
            FindRedundantConnection(inputArray);

            Console.ReadKey();
        }


        /// <summary>
        /// 併查集 理論
        /// https://leetcode.cn/problems/redundant-connection/solution/rong-yu-lian-jie-by-leetcode-solution-pks2/
        /// 
        /// wiki資料, 此資料結構 需要多加研究 多看. 非常不熟悉
        /// https://zh.wikipedia.org/zh-tw/%E5%B9%B6%E6%9F%A5%E9%9B%86
        /// </summary>
        /// <param name="edges"></param>
        /// <returns></returns>
        public static int[] FindRedundantConnection(int[][] edges)
        {
            int n = edges.Length;
            int[] parent = new int[n + 1];
            
            for (int i = 1; i <= n; i++)
            {
                parent[i] = i;
            }

            for (int i = 0; i < n; i++)
            {
                int[] edge = edges[i];
                int node1 = edge[0], node2 = edge[1];
                
                if (find(parent, node1) != find(parent, node2))
                {
                    union(parent, node1, node2);
                }
                else
                {
                    Console.WriteLine($"[{node1},{node2}]");
                    return edge;
                }

            }

            return new int[0];

        }


        /// <summary>
        /// 合併：將兩個集合合併為一個。
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        public static void union(int[] parent, int index1, int index2)
        {
            parent[find(parent, index1)] = find(parent, index2);

        }


        /// <summary>
        /// 查詢：查詢某個元素屬於哪個集合，通常是返回集合內的一個「代表元素」。
        /// 這個操作是為了判斷兩個元素是否在同一個集合之中。
        /// 
        /// 路徑壓縮最佳化
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int find(int[] parent, int index)
        {
            if (parent[index] != index)
            {
                parent[index] = find(parent, parent[index]);
            }

            return parent[index];

        }


    }
}
