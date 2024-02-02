using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1291
{
    internal class Program
    {
        /// <summary>
        /// 1291. Sequential Digits
        /// https://leetcode.com/problems/sequential-digits/description/?envType=daily-question&envId=2024-02-02
        /// 1291. 顺次数
        /// https://leetcode.cn/problems/sequential-digits/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int low = 100;
            int high = 300;

            var value = SequentialDigits(low, high);
            foreach(var res in value)
            {
                Console.Write(res + ", ");
            }

            Console.ReadKey();

        }


        /// <summary>
        /// 題目要求
        /// 10 <= low <= high <= 10^9
        /// 最多位數就是9個
        /// 所以迴圈從1跑到9
        /// 
        /// </summary>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        public static IList<int> SequentialDigits(int low, int high)
        {
            List<int> ans = new List<int>();

            // 高位數 左邊
            for (int i = 1; i <= 9; i++)
            {
                int num = i;

                // 低位數 右邊; j = i + 1 <題目要求,每一位上的数字都比前一位上的数字大 1 的整数>
                for (int j = i + 1; j <= 9; j++)
                {
                    // 左至右 or 右至左 差距是十倍. 十進位
                    // 題目要求後面位數要大於前面  j > i
                    num = num * 10 + j;

                    // 需要符合題目區間
                    if(num >= low && num <= high)
                    {
                        ans.Add(num);
                    }
                }
            }

            // 排序小至大 輸出
            ans.Sort();

            return ans;
        }

    }
}
