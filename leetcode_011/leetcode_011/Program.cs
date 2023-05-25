using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_011
{
    internal class Program
    {
        /// <summary>
        /// leetcode_011 Container With Most Water
        /// https://leetcode.com/problems/container-with-most-water/
        /// 
        /// 盛最多水的容器
        /// https://leetcode.cn/problems/container-with-most-water/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] height = new int[] { 1, 8, 6, 2, 5, 4, 8, 3, 7 };

            Console.WriteLine(MaxArea(height));
            Console.ReadKey();
        }


        /// <summary>
        /// https://ithelp.ithome.com.tw/articles/10228493
        /// https://leetcode.cn/problems/container-with-most-water/solution/container-with-most-water-shuang-zhi-zhen-fa-yi-do/
        /// https://leetcode.cn/problems/container-with-most-water/solution/sheng-zui-duo-shui-de-rong-qi-by-leetcode-solution/
        /// 
        /// 面積公式: S(i,j) = min(h[i], h[j]) × (j − i)
        /// 
        /// Math.Min(height[left], height[right]) 
        /// =>要取Min, 如果是Max會造成水位溢出
        /// 因兩邊不等高. 水當然會溢出.
        /// 兩邊高度一致水位才不會溢出
        /// 
        /// (right - left) 是為了找出面積 底長
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public static int MaxArea(int[] height)
        {
            int length = height.Length; // 輸入之height總數量(筆數)
            int left = 0; // 左邊 index
            int right = length - 1; // 右邊 index, index: 0開始
            int max = 0; // 最大儲存量

            while (left < right)
            {
                // 這兩行可縮成下面寫法.但是兩行比較好理解
                // 先求面積 長度 * 高度
                int area = Math.Min(height[left], height[right]) * (right - left);
                // 找出最大面積
                max = Math.Max(max, area);

                //濃縮寫法
                //max = Math.Max(max, Math.Min(height[left], height[right]) * (right - left));

                if (height[left] < height[right])
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }
            return max;
        }

    }
}
