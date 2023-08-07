using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_074
{
    internal class Program
    {
        /// <summary>
        /// 74. Search a 2D Matrix
        /// https://leetcode.com/problems/search-a-2d-matrix/
        /// 74. 搜索二维矩阵
        /// https://leetcode.cn/problems/search-a-2d-matrix/
        /// 
        /// 二維陣列 + 二分法
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] input = new int[][]
            {
                 new int[]{ 1,3,5,7 },
                 new int[]{ 10, 11, 16, 20 },
                 new int[]{ 23,30,34,60}
            };
            int target = 3;
            Console.WriteLine(SearchMatrix(input, target));
            Console.ReadKey();

        }



        /// <summary>
        /// 二分法
        /// https://leetcode.cn/problems/search-a-2d-matrix/solution/by-stormsunshine-gay8/
        /// 
        /// 有趣,很有趣
        /// 原來二分法也可以應用在陣列上
        /// 以前都覺得只能用在string 上找前後
        /// 
        /// m 行 n 列的矩阵, m與n 類似高與寬 意思
        /// 大小是遞增 => 由左至右 由上至下
        /// 每一行的開頭, 會比前一行的尾數還要大
        /// 
        /// row = mid / n 與 column = mid % n 取法 是重點
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SearchMatrix(int[][] matrix, int target)
        {
            // 幾組陣列; 高; 行
            int m = matrix.Length;
            // 每個陣列有多少個; 寬; 列
            int n = matrix[0].Length;
            int low = 0;
            int high = m * n - 1;

            while (low <= high)
            {
                int mid = low + (high - low) / 2;
                // 上下
                int row = mid / n;
                // 左右
                int column = mid % n;

                int temp = matrix[row][column];

                if (matrix[row][column] == target)
                {
                    return true;
                }
                else if (matrix[row][column] > target)
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }

            }

            return false;

        }


        /// <summary>
        /// 此方法 應該算是 最無腦 最簡單的
        /// 試試看 暴力法兩個for 迴圈
        /// 就怕 超時而已
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SearchMatrix2(int[][] matrix, int target)
        {
            int m = matrix.Length;
            int n = matrix[0].Length;

            for(int i = 0; i < m; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    if (matrix[i][j] == target)
                    {
                        return true;
                    }
                }
            }

            return false;
        }



    }
}
