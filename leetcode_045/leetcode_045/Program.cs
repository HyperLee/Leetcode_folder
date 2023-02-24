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
        /// leetcode 045
        /// 
        /// https://leetcode.com/problems/jump-game-ii/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = { 2, 3, 1, 1, 4 };

            Console.WriteLine(Jump(nums));
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
            int end = 0; // 上次跳跃可达范围右边界（下次的最右起跳点）
            int maxPosition = 0; // 目前能跳到的最远位置
            int steps = 0; // 跳跃次数
            for (int i = 0; i < length - 1; i++)
            {
                //找能跳的最远的
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
        /// https://leetcode.cn/problems/jump-game-ii/solution/xiang-xi-tong-su-de-si-lu-fen-xi-duo-jie-fa-by-10/
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int Jump2(int[] nums)
        {
            int position = nums.Length - 1; //要找的位置
            int steps = 0;
            while (position > 0) //是否到了第 0 个位置
            {
                for (int i = 0; i < position; i++)
                {
                    if (i + nums[i] >= position)
                    {
                        position = i;  //更新要找的位置
                        steps++;
                        break;
                    }
                }
            }
            return steps;
        }

    }
}
