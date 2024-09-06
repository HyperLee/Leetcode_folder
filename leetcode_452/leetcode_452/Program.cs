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
        /// 452. Minimum Number of Arrows to Burst Balloons
        /// https://leetcode.com/problems/minimum-number-of-arrows-to-burst-balloons/
        /// 
        /// 452. 用最少数量的箭引爆气球
        /// https://leetcode.cn/problems/minimum-number-of-arrows-to-burst-balloons/description/
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
        /// 1.先排序, points 的結束座標遞增來排序
        /// 2.氣球座標重疊就往結尾座標小的那個位置射出箭矢, 即可射破多個
        /// 2.1 找出氣球重疊最大的交集, 這樣就可以省下浪費箭矢
        /// 2.2 沒有新的交集就射出箭矢
        /// 3.沒有重疊只能單獨射出箭矢
        /// 
        /// 贪心策略的正确性说明如下。
        /// 1. 每次射出箭时，都是在尚未引爆的气球中选择结束坐标最小的气球，从该气球的结束坐标处射出箭，
        ///    该做法可以确保结束坐标最小的气球被引爆，且可以被引爆的气球数最大化。
        /// 2. 只有当已经射出的箭无法引爆更多的气球时，才射出新的箭，
        ///    因此每次射出的箭都必不可少，如果减少射出的箭数则一定不能引爆所有气球。
        ///    
        /// 这道题可以这么理解，后一个区间如果和前面某个区间有交集，说明它并不会贡献一次新的射击，因为可以和其他区间一块射击。
        /// 这样我们就可以按照区间右端点排序，这样距离当前区间左端点最近的右端点就是前一个区间的右端点
        /// 只有当前区间左端点大于前一个区间右端点时才不相交，所以需要新增一次射击
        /// 
        /// 只要维护一个气球公共区间（交集），若是当前气球共这个公共区间有交集，则更新公共区间；
        /// 若没有交集，则可射出一箭引爆公共区间所涉及的那些气球，并将公共区间置为当前气球的区间继续遍历完即可。
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static int FindMinArrowShots(int[][] points)
        {
            // 排序氣球位置, 依據 points 結束位置(右邊界)遞增排序
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
            
            // 累計從 1 開始,第一支箭能射破第一個位置(重疊區間)
            int count = 1;
            // 第一支箭矢預設位置 就是排序後的第一個(結束位置最小的那一個)
            int arrow = points[0][1];
            // 幾組陣列數目
            int length = points.Length;
            
            for (int i = 0; i < length; i++)
            {
                int[] point = points[i];

                // 氣球起始位置(左邊界)比箭矢所在位置還要大, 就需要移動箭矢
                // 反之就是有交集, 可以合併一起射擊
                if (point[0] > arrow)
                {
                    // 無法射破,需要移動箭矢位置
                    // 將氣球結束位置(右邊界)當作箭矢新的所在位置
                    arrow = point[1];
                    count++;
                }
            }

            return count;

        }

    }
}
