using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_055
{
    internal class Program
    {
        /// <summary>
        /// 55. Jump Game
        /// https://leetcode.com/problems/jump-game/
        /// 55. 跳跃游戏
        /// https://leetcode.cn/problems/jump-game/description/
        /// 
        /// 計算能不能走完路程, 走到底 or 超過都可以
        /// 可以就回傳 true
        /// 不行就回傳 false
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] num = new int[] { 2, 3, 1, 1, 4 };

            Console.WriteLine("方法1: " + CanJump(num));
            Console.WriteLine("方法2: " + CanJump2(num));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/jump-game/solution/tiao-yue-you-xi-by-leetcode-solution/
        /// DP 貪婪演算法
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static bool CanJump(int[] nums)
        {
            int n = nums.Length;
            // 最右邊位置
            int rightmost = 0; 

            for (int i = 0; i < n; i++)
            {
                // 當前位置 不能大於 最右邊之位置 否則就是失敗
                if (i <= rightmost)
                {
                    // 取出可走到隻最右邊位置;  i + nums[i] 原本第 i 位置 + i 這個蘊含可以走的數值
                    rightmost = Math.Max(rightmost, i + nums[i]);

                    // 可以走到最右邊之數值 比 總長度大或等於 就代表可以走完
                    if (rightmost >= n - 1)
                    {
                        return true;
                    }
                }
            }

            return false;

        }


        /// <summary>
        /// https://ithelp.ithome.com.tw/articles/10228245
        /// 方法2
        /// 其實概念都差不多
        /// 
        /// 宣告 int length 為 array 的長度
        /// 判斷 length 為 0 時，return false
        /// 宣告 int max 用於記錄 nums[i] 可以走的 最大步數
        /// 宣告 for 迴圈逐一判斷 nums[i] 
        ///     if(i > max) return false; 表示當前的 位置 已經 超過 可以走的步數
        ///     if(max > length) return true; 表示 最大步數 可以走超過最後一個 位置
        ///     max = Math.Max(max, nums[i] + i); 
        ///         比較出 目前最大步數 max 與 當前可以走的步數 + 位置 index 的最大的數字
        ///         即為 最大步數
        ///         
        /// 若 for 迴圈結束後，沒有 return false，代表這個 array 是有辦法走到最後的，因此
        /// return true;
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static bool CanJump2(int[] nums)
        {
            int n = nums.Length;
            if (n == 0)
            {
                return false;
            }

            int max = 0;
            for (int i = 0; i < n; i++)
            {
                if (i > max)
                {
                    // 走不完, false
                    return false;
                }

                if (max > n)
                {
                    // 提早走完, return
                    return true;
                }

                // 找出能走的最大步數
                max = Math.Max(max, nums[i] + i);
            }

            // 走到array最後才走完 return
            return true;
        }


    }
}
