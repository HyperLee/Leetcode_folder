using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1423
{
    internal class Program
    {
        /// <summary>
        /// 1423. Maximum Points You Can Obtain from Cards
        /// https://leetcode.com/problems/maximum-points-you-can-obtain-from-cards/
        /// 1423. 可获得的最大点数
        /// https://leetcode.cn/problems/maximum-points-you-can-obtain-from-cards/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = new int[] { 1, 2, 3, 4, 5, 6, 1 };
            int k = 3;

            Console.WriteLine(MaxScore(input, k));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/maximum-points-you-can-obtain-from-cards/solution/ke-huo-de-de-zui-da-dian-shu-by-leetcode-7je9/
        /// 
        /// https://leetcode.cn/problems/maximum-points-you-can-obtain-from-cards/solution/jian-dan-de-hua-dong-chuang-kou-he-kuai-1go5h/
        /// 
        /// 要求: 要求出連續k加總個為最大值
        /// 
        /// 因全部總和為sum 是固定的
        /// 故只要找出 總量n - 加總數量 k 總和為最小
        /// 就代表為 最大和
        /// 反向思考
        /// 最大扣除 最小 
        /// </summary>
        /// <param name="cardPoints"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int MaxScore(int[] cardPoints, int k)
        {
            int n = cardPoints.Length;
            // 滑动窗口大小为 n-k
            int len = n - k;
            int sum = 0, curr = 0;

            // 全部總和
            for(int i = 0; i < n; i++)
            {
                sum += cardPoints[i];
            }

            // 選前 n-k 作為为初始值
            for (int i = 0; i < len; i++)
            {
                curr += cardPoints[i];
            }

            int min = curr;

            for(int i = len; i < n; i++)
            {
                // 滑动窗口每向右移动一格，增加从右侧进入窗口的元素值，并减少从左侧离开窗口的元素值
                curr = curr + cardPoints[i] - cardPoints[i - len];
                min = Math.Min(min, curr);
            }

            // 總和 扣除 最小 就是最大和
            return sum - min;
        }

    }
}
