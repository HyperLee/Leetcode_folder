using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1079
{
    internal class Program
    {
        /// <summary>
        /// 1079. Letter Tile Possibilities
        /// https://leetcode.com/problems/letter-tile-possibilities/
        /// 1079. 活字印刷
        /// https://leetcode.cn/problems/letter-tile-possibilities/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string input = "AAB";
            Console.WriteLine(NumTilePossibilities(input));
            Console.ReadKey();

        }


        /// <summary>
        /// 官方解法
        /// 方法一：回溯
        /// https://leetcode.cn/problems/letter-tile-possibilities/solution/huo-zi-yin-shua-by-leetcode-solution-e49s/
        /// 
        /// https://leetcode.cn/problems/letter-tile-possibilities/solution/by-stormsunshine-oo7t/
        /// 
        /// 大概想法是 排序 + DFS, 去除重覆的
        /// 目前還不是很熟
        /// </summary>
        /// <param name="tiles"></param>
        /// <returns></returns>
        public static int NumTilePossibilities(string tiles)
        {
            IDictionary<char, int> count = new Dictionary<char, int>();

            // 統計 輸入字串 每個字的次數
            foreach (char t in tiles)
            {
                if (count.ContainsKey(t))
                {
                    count[t]++;
                }
                else
                {
                    count.Add(t, 1);
                }
            }

            ISet<char> tile = new HashSet<char>(count.Keys);

            // 最后我们返回搜索到的所有字符串，因为题目要求返回非空字符串的数目，所以结果还要减一。
            return DFS(tiles.Length, count, tile) - 1;
        }


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="count"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        private static int DFS(int i, IDictionary<char, int> count, ISet<char> tile)
        {
            if (i == 0)
            {
                return 1;
            }
            
            //DisplaySet(tile);
            
            int res = 1;
            foreach (char t in tile)
            {
                if (count[t] > 0)
                {
                    count[t]--;
                    res += DFS(i - 1, count, tile);
                    count[t]++;
                }
            }

            Console.WriteLine("final res:" + res);

            return res;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tile"></param>
        private static void DisplaySet(ISet<char> tile)
        {
            Console.Write("{");
            foreach (char t in tile)
            {
                Console.Write(" {0}", t);
            }
            Console.WriteLine(" }");
        }


    }
}
