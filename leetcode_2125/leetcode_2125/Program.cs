using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2125
{
    internal class Program
    {
        /// <summary>
        /// 2125. Number of Laser Beams in a Bank
        /// https://leetcode.com/problems/number-of-laser-beams-in-a-bank/?envType=daily-question&envId=2024-01-03
        /// 2125. 银行中的激光束数量
        /// https://leetcode.cn/problems/number-of-laser-beams-in-a-bank/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] input = { "011001", "000000", "010100", "001000" };
            Console.WriteLine(NumberOfBeams(input));
            Console.ReadKey();

        }



        /// <summary>
        /// 解法:
        /// https://leetcode.cn/problems/number-of-laser-beams-in-a-bank/solutions/2580993/2125-yin-xing-zhong-de-ji-guang-shu-shu-h257u/
        /// 
        /// 对任意两个安全设备而言，如果同时 满足下面两个条件，则二者之间存在 一个 激光束
        /// 1. 两个设备位于两个 不同行 ：r1 和 r2 ，其中 r1 < r2 。
        /// 2. 满足 r1 < i < r2 的 所有 行 i ，都 没有安全设备 。
        /// 
        /// 簡單說 每一行 計算 雷射裝置數量
        /// 再來 不同行之間用乘法相乘
        /// 依照此方法計算每兩兩行數量(不為空的行)
        /// 由上至下
        /// 
        /// 本來以為要用二進位 計算
        /// 結果不用
        /// 
        /// </summary>
        /// <param name="bank"></param>
        /// <returns></returns>
        public static int NumberOfBeams(string[] bank)
        {
            int laser = 0;
            int prevcount = 0;
            // m: 行, n: 列
            int m = bank.Length, n = bank[0].Length;

            for(int i = 0; i < m; i++)
            {
                int currcount = 0;
                for(int j = 0; j < n; j++)
                {
                    if (bank[i][j] == '1')
                    {
                        // 找到雷射 就++
                        currcount++;
                    }
                }

                if (currcount > 0)
                {
                    // 計算雷射數量
                    laser += prevcount * currcount;
                    prevcount = currcount;
                }

            }

            return laser;

        }

    }
}
