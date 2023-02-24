using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1527
{
    internal class Program
    {
        /// <summary>
        /// leetcode 1527  Count Odd Numbers in an Interval Range
        /// https://leetcode.com/problems/count-odd-numbers-in-an-interval-range/
        /// 
        /// 統計 範圍內有多少個 奇數
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int low = 8;
            int high = 10;
            Console.WriteLine(CountOdds(low, high));
            Console.ReadKey();
        }

        /// <summary>
        /// 利用 % 2 !=0 來判斷是不是奇數
        /// 不為0 就是 奇數 然後 result ++
        /// </summary>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        public static int CountOdds(int low, int high)
        {
            int result = 0;
            //int x = 1;
            for(int i = low; i <= high; i++)
            {
                if (i % 2 != 0)
                {
                    result++;
                }

                // 利用 邏輯 and 判斷
                //if((i & x) == 1)
                //{
                //    result++;
                //}
            }

            return result;
        }


    }
}
