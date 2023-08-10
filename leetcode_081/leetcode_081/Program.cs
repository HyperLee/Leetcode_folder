using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_081
{
    internal class Program
    {
        /// <summary>
        /// 81. Search in Rotated Sorted Array II
        /// https://leetcode.com/problems/search-in-rotated-sorted-array-ii/description/
        /// 81. 搜索旋转排序数组 II
        /// https://leetcode.cn/problems/search-in-rotated-sorted-array-ii/
        /// 
        /// 寫此題之前 先參考 題型Ｉ
        /// 33. Search in Rotated Sorted Array
        /// https://leetcode.com/problems/search-in-rotated-sorted-array/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 2, 5, 6, 0, 0, 1, 2 };
            int target = 0;

            Console.WriteLine(Search(input, target));
            Console.ReadKey();
        }


        /// <summary>
        /// 33. Search in Rotated Sorted Array
        /// 衍伸題目
        /// 簡單說 題目33是求出 target在nums的第nums[i]位置
        /// 
        /// 本題目81
        /// 簡單說是求出target是否存在nums中
        /// 有回傳true　沒有回傳false
        /// 
        /// 應該有更有效率得方法
        /// 看討論區都是說 二分法
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool Search(int[] nums, int target)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == target)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
