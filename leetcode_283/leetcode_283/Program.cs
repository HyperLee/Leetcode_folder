using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_283
{
    class Program
    {
        /// <summary>
        /// https://leetcode.com/problems/move-zeroes/
        /// https://leetcode-cn.com/problems/move-zeroes/solution/yi-dong-ling-by-leetcode-solution/
        /// leetcode 283
        /// 给定一个数组 nums，编写一个函数将所有 0 移动到数组的末尾，同时保持非零元素的相对顺序。
        /// 请注意 ，必须在不复制数组的情况下原地对数组进行操作。
        /// Note that you must do this in-place without making a copy of the array.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] array1 = { 0, 1, 3};
            MoveZeroes(array1);

            Console.ReadKey();
        }


        /// <summary>
        /// https://ithelp.ithome.com.tw/articles/10221042
        /// </summary>
        /// <param name="nums"></param>
        public static void MoveZeroes(int[] nums)
        {
            int pointer = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] != 0)
                {
                    if (pointer != i)
                    {
                        nums[pointer] = nums[i];
                        nums[i] = 0;
                    }
                    pointer++;
                }
            }

            // 輸出至Console, 不輸出就註解for loop
            for (int i = 0; i < nums.Length; i++)
            {
                Console.Write(nums[i] + ", ");
            }


        }


    }
}
