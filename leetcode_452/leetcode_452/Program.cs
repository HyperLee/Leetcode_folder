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

            Console.WriteLine(FindMinArrowShots(jaggedArray2));
            Console.ReadKey();
        }


        /// <summary>
        /// c# 解法
        /// https://leetcode.cn/problems/minimum-number-of-arrows-to-burst-balloons/solution/by-stormsunshine-r1u2/
        /// 
        /// 官方解法
        /// https://leetcode.cn/problems/minimum-number-of-arrows-to-burst-balloons/solution/yong-zui-shao-shu-liang-de-jian-yin-bao-qi-qiu-1-2/
        /// 
        /// 不規則陣列 int[][] 宣告 以及 輸出 方式
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/arrays/jagged-arrays
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

            /*// 輸出 array sort 結果
            for (int i = 0; i < points.Length; i++)
            {
                System.Console.Write("Element({0}): ", i);

                for (int j = 0; j < points[i].Length; j++)
                {
                    System.Console.Write("{0}{1}", points[i][j], j == (points[i].Length - 1) ? "" : " ");
                }
                System.Console.WriteLine();
            }
            */

            int count = 1;
            int arrow = points[0][1];
            int length = points.Length; // 幾組陣列數目
            for (int i = 1; i < length; i++)
            {
                int[] point = points[i];
                if (point[0] > arrow)
                {
                    arrow = point[1];
                    count++;
                }
            }
            return count;

        }

    }
}
