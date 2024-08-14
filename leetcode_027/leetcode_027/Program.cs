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
        /// 27. Remove Element
        /// https://leetcode.com/problems/remove-element/
        /// 
        /// 27. 移除元素
        /// https://leetcode.cn/problems/remove-element/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = { 3, 2, 2, 3 };
            int val = 3;

            //Console.WriteLine(RemoveElement(nums, val));
            RemoveElement(nums, val);
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/remove-element/solution/yi-chu-yuan-su-by-leetcode-solution-svxi/
        /// 
        /// 输入：nums = [3,2,2,3], val = 3
        /// 输出：2, nums = [2,2]
        /// 解释：函数应该返回新的长度 2, 并且 nums 中的前两个元素均为 2。
        /// 你不需要考虑数组中超出新长度后面的元素。
        /// 例如，函数返回的新长度为 2 
        /// ，而 nums = [2,2,3,3] 或 nums = [2,2,0,0]，也会被视作正确答案。
        ///
        /// 右指針指向下一個要比對的位置
        /// 左指針指向下一個要替換的位置
        /// 
        /// 原地替換
        /// 把 != val 的 element 往前移動
        /// = val 往後
        /// 最終 回傳 前 k 個 數量
        /// 也是新的 nums[k] 的 index 位置
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
                    // replace value
                    nums[left] = nums[right];
                    left++;
                }
            }

            Console.WriteLine("長度: " + left);
            Console.WriteLine();

            Console.Write("nums[]: [");
            foreach (var value in nums)
            {
                Console.Write(value + ", ");
            }
            Console.Write("]");

            return left;

        }


    }
}
