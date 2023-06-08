using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1351
{
    internal class Program
    {
        /// <summary>
        /// leetcode 1351 Count Negative Numbers in a Sorted Matrix
        /// https://leetcode.com/problems/count-negative-numbers-in-a-sorted-matrix/
        /// 1351. 统计有序矩阵中的负数
        /// https://leetcode.cn/problems/count-negative-numbers-in-a-sorted-matrix/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] input = new int[][]
            {
                 new int[]{ 4,3,2,-1 },
                 new int[]{ 3,2,1,-1 },
                 new int[]{ 1,1,-1,-2 },
                 new int[]{ -1,-1,-2,-3 }
            };

            //CountNegatives(input);
            Console.WriteLine(CountNegatives2(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static int CountNegatives(int[][] grid)
        {
            int cnt = 0;

            // 輸出 陣列資料
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] < 0)
                    {
                        cnt++;
                    }

                }
            }

            return cnt;
        }


        public static int CountNegatives2(int[][] grid)
        {
            Array.Sort(grid);


            int cnt = 0;

            // 輸出 陣列資料
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] < 0)
                    {
                        cnt++;
                    }
                    else
                    {
                        return cnt;
                    }

                }
            }

            return cnt;

        }


    }
}
