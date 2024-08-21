using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_045
{
    internal class Program
    {
        /// <summary>
        /// 45. Jump Game II
        /// https://leetcode.com/problems/jump-game-ii/
        /// 
        /// 45. 跳跃游戏 II
        /// https://leetcode.cn/problems/jump-game-ii/description/
        /// 
        /// 簡單說就是求出走到結尾
        /// 所需要的最少次數
        /// 
        /// 個人偏好 方法二
        /// 反向推導 解法
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = { 2, 3, 1, 1, 4 };

            Console.WriteLine("方法1: " + Jump(nums));
            Console.WriteLine("方法2: " + Jump2(nums));

            Console.ReadKey();
        }


        /// <summary>
        /// method2
        /// 
        /// https://leetcode.cn/problems/jump-game-ii/solution/tiao-yue-you-xi-ii-by-leetcode-solution/
        /// 
        ///  思想就一句话：每次在上次能跳到的范围（end）内选择一个能跳的最远的位置（也就是能跳到maxPosition位置
        ///  的点）作为下次的起跳点 ！
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int Jump(int[] nums)
        {
            int length = nums.Length;
            // 上次跳躍可達範圍右邊界（下次的最右起跳點）
            int end = 0;
            // 目前能跳到的最遠位置
            int maxPosition = 0;
            // 跳躍次數
            int steps = 0;

            /*
                每个end其实相当于跳下一步的点  集合中有几个end就最少需要跳几步
                因此不能遍历到nums.length - 1 因为前面的点跳到最后一个点 到达就行了 最后一个点不能成为起跳点
                如果i可以到达length - 1 例如[2, 3, 1, 1, 2, 2, 2] 正好最后end = length - 1 也就是i = length - 1时 
                res会 + 1 会多算一个起跳点 从而使得结果错误
            */
            // 这里有个小细节，因为是起跳的时候就 + 1 了，如果最后一次跳跃刚好到达了最后一个位置，那么遍历到最后一个位置的时候就会再次起跳，这是不允许的，因此不能遍历最后一个位置
            for (int i = 0; i < length - 1; i++)
            {
                // 找能跳的最遠的
                maxPosition = Math.Max(maxPosition, i + nums[i]); // i是nums[]第幾個位置, + nums[i]是 第i個位置在陣列裡面所蘊含的數值, 組合出來就是他可以走到最遠的距離

                if (i == end) //遇到边界，就更新边界，并且步数加一  // 到达上次跳跃能到达的右边界了
                {
                    end = maxPosition; // 目前能跳到的最远位置变成了下次起跳位置的有边界
                    steps++; // 进入下一次跳跃
                }
            }
            return steps;
        }

        /// <summary>
        /// ref:
        /// 方法1
        /// 反向查找出發位置
        /// https://leetcode.cn/problems/jump-game-ii/solution/xiang-xi-tong-su-de-si-lu-fen-xi-duo-jie-fa-by-10/
        /// 
        /// 結尾位置固定為已知
        /// 所以我們只要求出 出發位置即可
        /// 反向推導
        /// 結尾位置前的 跳躍位置
        /// 如果有好幾個位置都能跳躍到結尾
        /// 那就考慮 距離結尾最遠的那個位置
        /// 也就是能一次跳最遠的距離優先選擇
        /// 
        /// 反向推導, 所以跳躍位置會越來往前靠(縮短)
        /// 
        /// 反向的是 position 位置
        /// i 是從起始位置往 position 找
        /// 找到能跳到 position 位置的 i
        /// 也就是找到能跳最遠的
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int Jump2(int[] nums)
        {
            // 这里有个小细节，因为是起跳的时候就 + 1 了，如果最后一次跳跃刚好到达了最后一个位置，那么遍历到最后一个位置的时候就会再次起跳，这是不允许的，因此不能遍历最后一个位置
            // 要找的位置
            int position = nums.Length - 1; 
            // 跳躍次數累計
            int steps = 0;

            // 是否到了第 0 個位置, 反向推導(尾端 往 開頭找). 所以開頭就是截止條件
            while (position > 0) 
            {
                for (int i = 0; i < position; i++)
                {
                    // i + nums[i] => 能跳躍的距離
                    if (i + nums[i] >= position)
                    {
                        // 更新要找的位置
                        position = i;  
                        steps++;
                        
                        break;
                    }
                }
            }
            return steps;
        }

    }
}
