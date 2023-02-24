using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_448
{
    internal class Program
    {
        /// <summary>
        /// leetcode 448 Find All Numbers Disappeared in an Array
        /// https://leetcode.com/problems/find-all-numbers-disappeared-in-an-array/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] array = { 4, 3, 2, 7, 8, 2, 3, 1 };

            //Console.WriteLine(FindDisappearedNumbers(array));
            FindDisappearedNumbers(array);
            Console.ReadKey();
        }


        /// <summary>
        /// https://ithelp.ithome.com.tw/articles/10225673
        /// 
        /// list 基本用法 知識
        /// https://www.ruyut.com/2021/12/c-list.html
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static IList<int> FindDisappearedNumbers(int[] nums)
        {
            List<int> result = new List<int>();
            int numsLength = nums.Length;

            for (int i = 0; i < numsLength; i++)
            {
                int index = Math.Abs(nums[i]) - 1;
                if (nums[index] > 0)
                {
                    nums[index] = -nums[index];
                }
            }

            for (int i = 0; i < numsLength; i++)
            {
                if (nums[i] > 0)
                {
                    result.Add(i + 1);
                }
            }

            // List 輸出
            foreach (var str in result)
            {
                Console.WriteLine("欠缺: " + str);
            }

            return result;
        }

    }
}
