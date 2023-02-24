using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_035
{
    internal class Program
    {
        /// <summary>
        /// leetcode 035  Search Insert Position
        /// https://leetcode.com/problems/search-insert-position/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = new int[] { 11, 33, 55, 66 };
            int target = 77;

            Console.WriteLine(SearchInsert(nums, target));
            Console.ReadKey();

        }


        /// <summary>
        /// https://leetcode.cn/problems/search-insert-position/solution/by-stormsunshine-lws7/
        /// 方法2
        /// 
        /// https://leetcode.cn/problems/search-insert-position/solution/35sou-suo-cha-ru-wei-zhi-xiao-bai-yi-don-tnsm/
        /// https://leetcode.cn/problems/search-insert-position/solution/sou-suo-cha-ru-wei-zhi-by-leetcode-solution/
        /// 
        /// 
        /// 詳細解說
        /// https://leetcode.cn/problems/search-insert-position/solution/by-carlsun-2-2dlr/
        /// 二分法第一种写法
        /// 
        /// low = 左區間 left
        /// high = 右區間 right
        /// 兩邊區間夾擊
        /// 
        /// 二分法 wiki 資料
        /// https://zh.wikipedia.org/zh-tw/%E4%BA%8C%E5%88%86%E6%90%9C%E5%B0%8B%E6%BC%94%E7%AE%97%E6%B3%95
        /// int mid = start + (end - start) / 2;    //直接平均可能會溢位，所以用此算法
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int SearchInsert(int[] nums, int target)
        {
            int low = 0, high = nums.Length - 1;
            while (low <= high)
            {
                // 防止溢出 等同于(left + right)/2
                int mid = low + ((high - low) / 2);
                if (nums[mid] == target)
                {
                    return mid;
                }
                else if (nums[mid] > target)
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }
            return low;

        }


    }
}
