using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_189
{
    internal class Program
    {
        /// <summary>
        /// 189. Rotate Array
        /// https://leetcode.com/problems/rotate-array/?envType=study-plan-v2&envId=top-interview-150
        /// 189. 轮转数组
        /// https://leetcode.cn/problems/rotate-array/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 1, 2, 3, 4, 5, 6, 7 };
            int k = 3;

            Rotate(input, k);
            Console.ReadKey();
        }



        /// <summary>
        /// 官方解法
        /// https://leetcode.cn/problems/rotate-array/solutions/551039/xuan-zhuan-shu-zu-by-leetcode-solution-nipk/
        /// 方法三：数组翻转
        /// ***
        /// 该方法基于如下的事实：当我们将数组的元素向右移动 k 次后，尾部 k mod n
        /// 个元素会移动至数组头部，其余元素向后移动 k mod n 个位置。
        /// 
        /// 1.將原始輸入陣列反轉
        /// 2.翻转 [0,k  mod  n − 1] 区间的元素 =>   前k個元素 反轉回去成原始輸入順序
        /// 3. [k  mod  n, n − 1] 区间的元素 => 後n - k 元素 反轉回去成原始輸入順序
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        public static void Rotate(int[] nums, int k)
        {
            // 計算移動k之後,原始陣列中的結尾位置
            int distance = k % nums.Length;

            // 輸入陣列資料全部反轉
            reverse(nums, 0, nums.Length - 1);

            // 前k個元素反轉為 輸入順序
            reverse(nums, 0, distance - 1);

            // n - k 之後(k 之後的)的元素反轉回去輸入順序
            reverse(nums, distance, nums.Length - 1); 

            foreach(var value in nums)
            {
                Console.Write(value + ", ");
            }

        }


        /// <summary>
        /// 資料反轉
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static void reverse(int[] nums, int start, int end)
        {
            while(start < end)
            {
                int temp = nums[start];
                nums[start] = nums[end];
                nums[end] = temp;

                start++;
                end--;
            }

        }

    }
}
