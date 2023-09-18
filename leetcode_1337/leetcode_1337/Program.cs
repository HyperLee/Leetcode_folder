using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1337
{
    internal class Program
    {
        /// <summary>
        /// 1337. The K Weakest Rows in a Matrix
        /// https://leetcode.com/problems/the-k-weakest-rows-in-a-matrix/?envType=daily-question&envId=2023-09-18
        /// 1337. 矩阵中战斗力最弱的 K 行
        /// https://leetcode.cn/problems/the-k-weakest-rows-in-a-matrix/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] jaggedArray2 =
            {
                new int[] { 1, 1, 0, 0, 0 },
                new int[] { 1, 1, 1, 1, 0 },
                new int[] { 1, 0, 0, 0, 0 },
                new int[] { 1, 1, 0, 0, 0 },
                new int[] { 1, 1, 1, 1, 1 }
            };

            int k = 3;

            //Console.WriteLine(KWeakestRows(jaggedArray2, k));

            var res = KWeakestRows(jaggedArray2, k);

            foreach(var value in res)
            {
                Console.Write(value + ", ");
            }

            Console.ReadKey();
        }


        /// <summary>
        /// 不規則陣列 
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/arrays#jagged-arrays
        /// 
        /// https://leetcode.cn/problems/the-k-weakest-rows-in-a-matrix/solutions/89198/fang-zhen-zhong-zhan-dou-li-zui-ruo-de-k-xing-by-y/
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int[] KWeakestRows(int[][] mat, int k)
        {
            // key: 行, value: 每一行加總數量
            Dictionary<int, int> dic = new Dictionary<int, int>();

            // 計算每一行(左右)軍人數量
            for(int i = 0; i < mat.Length; i++)
            {
                dic.Add(i, mat[i].Sum());
            }

            //用value排序取出相對的key. 再依據題目要求取出k個數量 陣列輸出
            return dic.OrderBy(t => t.Value).Select(t => t.Key).Take(k).ToArray();
        }


    }
}
