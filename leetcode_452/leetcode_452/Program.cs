using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_452
{
    internal class Program
    {
        /// <summary>
        /// leetcode 452
        /// https://leetcode.com/problems/minimum-number-of-arrows-to-burst-balloons/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] jaggedArray2 = new int[][]
            {
            new int[] {10, 16 },
            new int[] { 2, 8 },
            new int[] { 1, 6 },
            new int[] { 7, 12 }
            };

            Console.WriteLine("Output: " + FindMinArrowShots(jaggedArray2));
            Console.ReadKey();
        }


        /// <summary>
        /// 解法建議看下列網址說明
        /// 
        /// c# 解法
        /// https://leetcode.cn/problems/minimum-number-of-arrows-to-burst-balloons/solution/by-stormsunshine-r1u2/
        /// 
        /// 官方解法
        /// https://leetcode.cn/problems/minimum-number-of-arrows-to-burst-balloons/solution/yong-zui-shao-shu-liang-de-jian-yin-bao-qi-qiu-1-2/
        /// 
        /// 不規則陣列 int[][] 宣告 以及 輸出 方式
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/arrays/jagged-arrays
        /// 
        /// 1.先排序, points的結束座標遞增來排序
        /// 2.氣球座標重疊就往結尾座標小的那個位置射出箭矢, 即可射破多個
        /// 3.沒有重疊只能單獨射出箭矢
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static int FindMinArrowShots(int[][] points)
        {
            Array.Sort(points, (a, b) => {
                if (a[1] > b[1])
                {
                    return 1;
                }
                else if (a[1] < b[1])
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            });

            // 輸出 array sort 結果 //////////////////////////////////////////////////////////
            for (int i = 0; i < points.Length; i++)
            {
                System.Console.Write("array sort Element({0}): ", i);

                for (int j = 0; j < points[i].Length; j++)
                {
                    System.Console.Write("{0}{1}", points[i][j], j == (points[i].Length - 1) ? "" : " ");
                }
                System.Console.WriteLine();
            }
            ///////////////////////////////////////////////////////////////////////////////////
            
            // 累計從1開始,第一支箭能射破第一個位置
            int count = 1;
            // 第一支箭矢預設位置 就是排序後的第一個(結束位置最小的那一個)
            int arrow = points[0][1];
            // 幾組陣列數目
            int length = points.Length;
            
            for (int i = 1; i < length; i++)
            {
                int[] point = points[i];
                if (point[0] > arrow)
                {
                    // 無法射破,需要移動箭矢位置
                    arrow = point[1];
                    count++;
                }
            }

            return count;

        }

    }
}
