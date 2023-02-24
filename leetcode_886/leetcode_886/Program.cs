using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_886
{
    internal class Program
    {
        /// <summary>
        /// leetcode 886
        /// https://leetcode.com/problems/possible-bipartition/
        /// 
        /// int[][] ==> 不規則陣列
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/arrays/jagged-arrays
        /// 
        /// 邏輯互斥 OR 運算子   ^
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/operators/boolean-logical-operators#logical-exclusive-or-operator-
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            //int a = 3;
            //Console.WriteLine(2 ^ 1); //邏輯互斥 OR 運算子

            int[][] dislikes = new int[][]
            {
                //new int[] { 1,2 },
                //new int[] { 1,3 },
                //new int[] { 2,4 }

                new int[] { 1,2 }
            };
            int n = 3;

            Console.WriteLine(PossibleBipartition(n, dislikes));

            Console.ReadKey();
        }


        /// <summary>
        /// 深度優先
        /// https://leetcode.cn/problems/possible-bipartition/solution/ke-neng-de-er-fen-fa-by-leetcode-solutio-guo7/
        /// </summary>
        /// <param name="n"></param>
        /// <param name="dislikes"></param>
        /// <returns></returns>
        public static bool PossibleBipartition(int n, int[][] dislikes)
        {
            int[] color = new int[n + 1]; // 每个人对应的颜色
            IList<int>[] g = new IList<int>[n + 1];
            for (int i = 0; i <= n; ++i)
            {
                g[i] = new List<int>();
            }
            foreach (int[] p in dislikes) //给每一个人做一张仇人表
            {
                g[p[0]].Add(p[1]);
                g[p[1]].Add(p[0]);
            }
            for (int i = 1; i <= n; ++i) //顺序地给每个人染色
            {
                if (color[i] == 0 && !DFS(i, 1, color, g))
                {
                    //这个节点无论如何都会与仇人共色
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 这里3^nowcolor进行染色分组，0表示未分组，1表示分组1，2表示分组2 在进行使用时，采用异或， 3（11）异或1（01）得到2（10），3（11）异或2（10）得到1（01）
        /// 
        /// DFS的任务：
        /// 将color中的curnode染色成nowcolor，同时将curnode的仇人们染成反色
        /// 如果方案存在，返回true，不存在返回false
        /// </summary>
        /// <param name="curnode"></param>
        /// <param name="nowcolor"></param>
        /// <param name="color"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        public static bool DFS(int curnode, int nowcolor, int[] color, IList<int>[] g)
        {
            color[curnode] = nowcolor;  //给节点染色
            foreach (int nextnode in g[curnode]) //g[curnode]表示这个节点的仇家们
            {
                if (color[nextnode] != 0 && color[nextnode] == color[curnode])
                {
                    //仇家已经被染色，而且和节点颜色相同
                    return false;
                }
                if (color[nextnode] == 0 && !DFS(nextnode, 3 ^ nowcolor, color, g))
                {
                    //虽然仇家没有染色，但是不可能将全部仇家然成反色(3 ^ nowcolor)
                    return false;
                }
            }
            return true;
        }


    }
}
