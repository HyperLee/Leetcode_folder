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
                 new int[]{ 1, 3, 5, 7 },
                 new int[]{ 10, 11, 16, 20 },
                 new int[]{ 23, 30, 34, 60}
            };
            int target = 3;

            Console.WriteLine(SearchMatrix(input, target));
            
            Console.ReadKey();

        }



        /// <summary>
        /// 二分法
        /// https://leetcode.cn/problems/search-a-2d-matrix/solution/by-stormsunshine-gay8/
        /// https://leetcode.cn/problems/search-a-2d-matrix/solutions/2783931/liang-chong-fang-fa-er-fen-cha-zhao-pai-39d74/
        /// 
        /// 有趣,很有趣
        /// 原來二分法也可以應用在陣列上
        /// 以前都覺得只能用在 string 上找前後
        /// 
        /// m 行 n 列的矩阵, m 與 n 類似高與寬 意思
        /// 大小是遞增 => 由左至右 由上至下
        /// 每一行的開頭, 會比前一行的尾數還要大
        /// 
        /// 猜測是因為上述說明所以才用 n 來計算
        /// 把陣列攤平就是 m * n 
        /// row = mid / n 與 column = mid % n 取法 是重點
        /// 
        /// 第二個聯結有比較詳細說明
        /// 把輸入的陣列給攤平
        /// a = [1,3,5,7,10,11,16,20,23,30,34,60]
        /// 并不需要真的拼成一个长为 m * n 的数组 a，而是将 a[i] 转换成矩阵中的行号和列号。
        /// 例如示例 1，i = 9 对应的 a[i] = 30，
        /// 由于矩阵有 n = 4 列，
        /// a[i] 在 i / n = 2 行
        ///         i mod n = 1 列
        ///         
        /// 由於攤平了所以用 n 來定位出在哪個位置
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SearchMatrix(int[][] matrix, int target)
        {
            // 幾組陣列; 高; 行(上下)
            int m = matrix.Length;
            // 每個陣列有多少個; 寬; 列(左右)
            int n = matrix[0].Length;
            int low = 0;
            int high = m * n - 1;

            while (low <= high)
            {
                int mid = low + (high - low) / 2;
                // 上下, 取出第幾行
                int row = mid / n;
                // 左右, 取出第幾列
                int column = mid % n;

                // show value
                //int temp = matrix[row][column];

                if (matrix[row][column] == target)
                {
                    return true;
                }
                else if (matrix[row][column] > target)
                {
                    // 數字太大就縮小 high
                    high = mid - 1;
                }
                else
                {
                    // 數字太小就擴大 low
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
