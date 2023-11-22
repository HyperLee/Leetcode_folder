using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1424
{
    internal class Program
    {
        /// <summary>
        /// 1424. Diagonal Traverse II
        /// https://leetcode.com/problems/diagonal-traverse-ii/?envType=daily-question&envId=2023-11-22
        /// 
        /// 1424. 对角线遍历 II
        /// https://leetcode.cn/problems/diagonal-traverse-ii/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //int[,] input = { { 1, 2, 3}, { 4, 5, 6}, { 7, 8, 9} };
            //Console.WriteLine(FindDiagonalOrder(input));

        }


        /// <summary>
        /// 解法:
        /// https://leetcode.cn/problems/diagonal-traverse-ii/solutions/1530737/by-stormsunshine-5pl9/
        /// 
        /// 需要多加思考一下
        /// 第一次解這種類型題目
        /// 不是很清楚關鍵以及訣竅在哪裡
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int[] FindDiagonalOrder(IList<IList<int>> nums)
        {
            IList<int[]> list = new List<int[]>();
            int row = nums.Count;

            for(int i = 0; i < row; i++)
            {
                IList<int> rowList = nums[i];
                int cols = rowList.Count;

                for(int j = 0; j < cols; j++)
                {
                    int num = rowList[j];
                    list.Add(new int[] { i + j, j, num});
                }
            }

            ((List<int[]>)list).Sort((a, b) => 
            {
                if (a[0] != b[0])
                {
                    return a[0] - b[0];
                }
                else
                {
                    return a[1] - b[1];
                }
            });

            int size = list.Count;
            int[] order = new int[size];
            
            for(int i = 0; i < size; i++)
            {
                order[i] = list[i][2];
            }

            return order;
        }

    }
}
