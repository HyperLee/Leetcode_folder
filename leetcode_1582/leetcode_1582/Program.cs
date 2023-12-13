using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1582
{
    internal class Program
    {
        /// <summary>
        /// 1582. Special Positions in a Binary Matrix
        /// https://leetcode.com/problems/special-positions-in-a-binary-matrix/?envType=daily-question&envId=2023-12-13
        /// 1582. 二进制矩阵中的特殊位置
        /// https://leetcode.cn/problems/special-positions-in-a-binary-matrix/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // [][]:不規則陣列, 這邊題目輸入是2維陣列
            int[][] input = { new int[] { 1, 0, 0}, new int[] { 0, 1, 0 }, new int[] { 0, 0, 1} };

            Console.WriteLine(NumSpecial(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 官方解法 1:
        /// https://leetcode.cn/problems/special-positions-in-a-binary-matrix/solutions/1796614/er-jin-zhi-ju-zhen-zhong-de-te-shu-wei-z-kan4/
        /// 
        /// 簡單說 特殊位置 就是 [i, j] = 1. 
        /// 然後所處的 i 其餘位置 都為 0
        /// 同理 所處的 j 其餘 位置 都為 0
        /// 
        /// 這解法就是 分別把 i , j 位置加總合為1 
        /// 即是題目要求的特殊位置定義
        /// 
        /// 陣列知識
        /// https://sweetkikibaby.pixnet.net/blog/post/191310453
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/arrays#jagged-arrays
        /// 
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public static int NumSpecial(int[][] mat)
        {
            // row = 行 , column = 列
            int m = mat.Length, n = mat[0].Length;
            int[] rowsum = new int[m];
            int[] colsum = new int[n];

            // 分別加總計算i, j總和
            for(int i = 0; i < m; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    rowsum[i] += mat[i][j];
                    colsum[j] += mat[i][j];
                }
            }

            int res = 0;
            for(int i = 0; i < m; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    // 找出 輸入的mat裡面總共有多少個 特殊位置
                    if (mat[i][j] == 1 && rowsum[i] == 1 && colsum[j] == 1)
                    {
                        res++;
                    }
                }
            }

            return res;

        }

    }
}
