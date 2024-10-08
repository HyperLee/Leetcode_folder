using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_033
{
    internal class Program
    {
        /// <summary>
        /// 33. Search in Rotated Sorted Array
        /// https://leetcode.com/problems/search-in-rotated-sorted-array/
        /// 33. 搜索旋转排序数组
        /// https://leetcode.cn/problems/search-in-rotated-sorted-array/
        /// 
        /// 偏好方法一: 迴圈直接做完就好
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 4, 5, 6, 7, 0, 1, 2 };

            Console.WriteLine(Search(input, 0));
            Console.ReadKey();

        }


        /// <summary>
        /// 方法一
        /// 單純用迴圈跑過一輪 比對
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int Search(int[] nums, int target)
        {

            for(int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == target)
                {
                    return i;
                }
            }

            return -1;
        }


        /// <summary>
        /// 方法二
        /// 
        /// ref:
        /// https://leetcode.cn/problems/search-in-rotated-sorted-array/solutions/1987503/by-endlesscheng-auuh/
        /// 
        /// 試試看 二分法
        /// 輸入的 nums[] 有旋轉過, 所以原先的 遞增順序
        /// 會被切割成左右兩塊.
        /// 
        /// 使用兩次二分
        /// 抓出 target 坐落在 nums[] 的左邊或是右邊
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int Search2(int[] nums, int target)
        {
            int n = nums.Length;
            int i = findMin(nums);

            // target 在第一段
            if(target > nums[n - 1])
            {
                // 開區間 (-1, i)
                return lowerBound(nums, -1, i, target);
            }

            // target 在第二段
            // 開區間 (i - 1, n)
            return lowerBound(nums, i - 1, n, target);
        }


        /// <summary>
        /// 153. 寻找旋转排序数组中的最小值
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int findMin(int[] nums)
        {
            int n = nums.Length;
            int left = -1;
            // 開區間 (-1, n - 1)
            int right = n - 1;

            // 開區間 不為空
            while(left + 1 < right)
            {
                int mid = left + (right - left) / 2;
                if (nums[mid] < nums[n - 1])
                {
                    right = mid;
                }
                else
                {
                    left = mid;
                }
            }

            return right;
        }


        /// <summary>
        /// 有序数组中找 target 的下标
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int lowerBound(int[] nums, int left, int right, int target)
        {
            // 開區間不為空
            // 循環不變量:
            // nums[left] < target
            // nums[right] >= target
            while (left + 1 < right)
            {
                int mid = left + (right - left) / 2;
                if (nums[mid] < target)
                {
                    // 範圍縮小到 (mid, right)
                    left = mid;
                }
                else
                {
                    // 範圍縮小到 (left, mid)
                    right = mid;
                }
            }

            return nums[right] == target ? right : -1;
        }
    }
}
