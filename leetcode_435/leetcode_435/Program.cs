using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_435
{
    internal class Program
    {
        /// <summary>
        /// 435. Non-overlapping Intervals
        /// https://leetcode.com/problems/non-overlapping-intervals/
        /// 435. 无重叠区间
        /// https://leetcode.cn/problems/non-overlapping-intervals/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

        }


        /// <summary>
        /// https://leetcode.cn/problems/non-overlapping-intervals/solution/by-stormsunshine-0uxp/
        /// 
        /// </summary>
        /// <param name="intervals"></param>
        /// <returns></returns>
        public int EraseOverlapIntervals(int[][] intervals)
        {
            Array.Sort(intervals, (a, b) => a[1] - b[1]);
            int eraseCount = 0;
            int end = intervals[0][1];
            int length = intervals.Length;
            for (int i = 1; i < length; i++)
            {
                if (intervals[i][0] >= end)
                {
                    end = intervals[i][1];
                }
                else
                {
                    eraseCount++;
                }
            }
            return eraseCount;

        }

    }
}
