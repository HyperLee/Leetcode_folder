using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2616
{
    internal class Program
    {
        /// <summary>
        /// 2616. Minimize the Maximum Difference of Pairs
        /// https://leetcode.com/problems/minimize-the-maximum-difference-of-pairs/
        /// 
        /// 2616. 最小化数对的最大差值
        /// https://leetcode.cn/problems/minimize-the-maximum-difference-of-pairs/
        /// 
        /// 
        /// 本題似乎可以參考 
        /// 2560 打家劫舍 IV 
        /// https://leetcode.cn/problems/house-robber-iv/description/
        /// 198. 打家劫舍
        /// https://leetcode.cn/problems/house-robber/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 10, 1, 2, 7, 1, 3 };
            int p = 2;

            Console.WriteLine(MinimizeMax(input, p));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/minimize-the-maximum-difference-of-pairs/solution/er-fen-da-an-tan-xin-by-endlesscheng-dlxv/
        /// https://leetcode.cn/problems/minimize-the-maximum-difference-of-pairs/solution/er-fen-tan-xin-han-xiang-xi-zheng-ming-b-21b5/
        /// 查看說明後發現:
        /// 排序後 變成找相鄰的, 答案不會變
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int MinimizeMax(int[] nums, int p)
        {
            Array.Sort(nums);
            int result = 0;
            int pairs = 0;
            for(int i = 0; i < nums.Length - 1; i++)
            {
                int curr = 0;
                curr = nums[i + 1] - nums[i];
                pairs++;
                result = Math.Min(result, curr);
            }

            return result;
        }


    }
}
