using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_228
{
    internal class Program
    {
        /// <summary>
        /// leetcode_228 Summary Ranges
        /// https://leetcode.com/problems/summary-ranges/
        /// 228. 汇总区间
        /// https://leetcode.cn/problems/summary-ranges/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = new int[] { 0, 1, 2, 4, 5, 7 };
            Console.WriteLine(SummaryRanges(input));
            Console.ReadKey();

        }


        /// <summary>
        /// 目前方法 邊界 也就是 最後一個 會被忽略
        /// 有bug
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>

        public static IList<string> SummaryRanges(int[] nums)
        {
            List<string> list = new List<string>();

            int length = nums.Length;
            int cnt = 0, end = 0;

            for (int i = 1; i < length; i++)
            {
                
                if (nums[i] - nums[i - 1] == 1)
                {
                    cnt++;
                    end = i;
                }
                else if(cnt > 0)
                {
                    int start = i - cnt - 1;
                    list.Add(nums[start] + "->" + nums[end]);
                    cnt = 0;
                }
                else
                {
                    cnt = 0;
                    list.Add(nums[i].ToString());
                }

            }

            foreach(var value in list)
            {
                Console.WriteLine(value.ToString());
            }

            return list;
        }


    }
}
