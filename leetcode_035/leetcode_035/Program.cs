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
        /// 35. Search Insert Position
        /// https://leetcode.com/problems/search-insert-position/description/
        /// 
        /// 35. 搜索插入位置
        /// https://leetcode.cn/problems/search-insert-position/description/
        /// 
        /// 找出 target 在陣列中的 第幾個 index
        /// 要是找不到就回傳可以插入的 index 位置
        /// 輸入的陣列不會重複且已經過排序
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = new int[] { 1, 3, 5, 6 };
            int target = 5;

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
                // + low => 防止溢出 等同于 (left + right) / 2
                int mid = low + (high - low) / 2;
                if(nums[mid] == target)
                {
                    return mid;
                }
                else if(nums[mid] > target)
                {
                    // 數字太大, high 往左縮小
                    high = mid - 1;
                }
                else
                {
                    // 數字太小, low 往右放大
                    low = mid + 1;
                }
            }

            return low;

        }


    }
}
