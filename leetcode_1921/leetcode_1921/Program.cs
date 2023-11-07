using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1921
{
    internal class Program
    {
        /// <summary>
        /// 1921. Eliminate Maximum Number of Monsters     Eliminate: 排除;殲滅;清除
        /// https://leetcode.com/problems/eliminate-maximum-number-of-monsters/
        /// 1921. 消灭怪物的最大数量
        /// https://leetcode.cn/problems/eliminate-maximum-number-of-monsters/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] dist = { 1, 3, 4 };
            int[] speed = { 1, 1, 1 };

            Console.WriteLine(EliminateMaximum(dist, speed));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/eliminate-maximum-number-of-monsters/solutions/857961/xiao-mie-guai-wu-de-zui-da-shu-liang-by-0ou2p/
        /// 
        /// 将我方的攻击时间序列和排序后的怪物到达时间依次进行比较
        /// 当第一次出现到达时间小于等于攻击时间，即表示怪物到达城市我方会输掉游戏。
        /// 在比较时，因为我方的攻击时间为整数，因此可以将怪物到达时间向上取整，可以达到避免浮点数误差的效果。
        /// </summary>
        /// <param name="dist"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public static int EliminateMaximum(int[] dist, int[] speed)
        {
            int n = dist.Length;
            int[] arrivalTimes = new int[n];
            for(int i = 0; i < n; i++)
            {
                // 最後 +１　避免除法出現浮點數,弄成整數
                arrivalTimes[i] = ( (dist[i] - 1) / speed[i] ) + 1;
            }

            Array.Sort(arrivalTimes); 
            // 第0分鐘就可以開始攻擊, 所以迴圈從0開始.
            // 攻擊時間都是整數, 所以用迴圈跑沒問題
            for(int i = 0; i < n; i++)
            {
                // 到達時間 <= 攻擊時間. 就是失敗 怪物闖入
                if (arrivalTimes[i] <= i)
                {
                    // 回傳可以打到第幾隻
                    return i;
                }
            }

            // 全部都能攻擊
            return n;

        }

    }
}
