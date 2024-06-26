using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace _1535
{
    internal class Program
    {
        /// <summary>
        /// 1535. Find the Winner of an Array Game
        /// https://leetcode.com/problems/find-the-winner-of-an-array-game/?envType=daily-question&envId=2023-11-05
        /// 1535. 找出数组游戏的赢家
        /// https://leetcode.cn/problems/find-the-winner-of-an-array-game/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 2, 1, 3, 5, 4, 6, 7 };
            int k = 2;

            Console.WriteLine(GetWinner(input, k));
            Console.ReadKey();
        }


        /// <summary>
        /// 官方解法
        /// https://leetcode.cn/problems/find-the-winner-of-an-array-game/solutions/371344/zhao-chu-shu-zu-you-xi-de-ying-jia-by-leetcode-sol/
        /// 本專案採用解法:
        /// https://leetcode.cn/problems/find-the-winner-of-an-array-game/solutions/995312/1535-zhao-chu-shu-zu-you-xi-de-ying-jia-bg8fv/
        /// 
        /// 用 prev 表示上一回合游戏中取得胜利的整数，
        /// 用 consecutive 表示该整数取得连续胜利的回合数。
        /// 第一回合游戏在 arr[0] 和 arr[1] 之间进行，第一回合游戏之后，prev 为 arr[0]
        /// 和 arr[1] 中的较大值，consecutive 的值为 1。
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int GetWinner(int[] arr, int k)
        {
            int prev = Math.Max(arr[0], arr[1]);
            
            // 第一回合結束,直接把最大的取出來即可
            if(k == 1)
            {
                return prev;
            }

            // 已經跑過第一回合,所以從一開始累計
            int consecutive = 1;
            int maxNum = prev;
            int lenght = arr.Length;

            // 第二回合開始比較
            for(int i = 2; i < lenght; i++)
            {
                int curr = arr[i];

                if(prev > curr)
                {
                    consecutive++;

                    if(consecutive == k)
                    {
                        // 達到題目要求回和數
                        return prev;
                    }

                }
                else
                {
                    // 比較輸了,那就把prev替換為curr, 且consecutive從1開始累計
                    prev = curr;
                    consecutive = 1;
                }

                // 找出最大數值者, 最大才有辦法連贏
                maxNum = Math.Max(prev, curr);
            }

            return maxNum;

        }

    }
}
