using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_027
{
    internal class Program
    {
        /// <summary>
        /// leetcode 027
        /// https://leetcode.com/problems/remove-element/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = {3,2,2,3 };
            int val = 3;

            Console.WriteLine(RemoveElement(nums, val));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/remove-element/solution/yi-chu-yuan-su-by-leetcode-solution-svxi/
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int RemoveElement(int[] nums, int val)
        {
            int n = nums.Length;
            int left = 0;
            for (int right = 0; right < n; right ++)
            {
                if (nums[right] != val)
                {
                    nums[left] = nums[right];
                    left++;
                }
            }
            return left;

        }


    }
}
