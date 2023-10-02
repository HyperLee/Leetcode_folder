using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2038
{
    internal class Program
    {
        /// <summary>
        /// 2038. Remove Colored Pieces if Both Neighbors are the Same Color
        /// https://leetcode.com/problems/remove-colored-pieces-if-both-neighbors-are-the-same-color/?envType=daily-question&envId=2023-10-02
        /// 2038. 如果相邻两个颜色均相同则删除当前颜色
        /// https://leetcode.cn/problems/remove-colored-pieces-if-both-neighbors-are-the-same-color/
        /// 
        /// 刪除相鄰的顏色A, B
        /// 1. 頭尾不能刪除
        /// 2. a, b 輪流刪除
        /// 3. 不能刪除時候結束
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string colors = "AAABABBB";
            Console.WriteLine(WinnerOfGame(colors));
            Console.ReadKey();

        }


        /// <summary>
        /// https://leetcode.cn/problems/remove-colored-pieces-if-both-neighbors-are-the-same-color/solutions/1355114/ru-guo-xiang-lin-liang-ge-yan-se-jun-xia-rfbk/
        /// 
        /// 我们可以分别计算出 Alice 和 Bob 的操作数。
        /// 当 Alice 的操作数大于 Bob 的操作数时，Alice 获胜；否则，Bob 获胜。
        /// </summary>
        /// <param name="colors"></param>
        /// <returns></returns>
        public static bool WinnerOfGame(string colors)
        {
            // 統計A, B次數. 誰大誰贏
            int[] freq = { 0, 0 };
            char cur = 'c';
            int cnt = 0;

            foreach (char c in colors) 
            {
                if(c != cur)
                {
                    cur = c;
                    cnt = 1;
                }
                else
                {
                    cnt += 1;

                    if(cnt >= 3)
                    {
                        // 更新 freq[]裡面數值
                        freq[cur - 'A'] += 1;
                    }
                }
            }

            return freq[0] > freq[1];
        }
    }
}
