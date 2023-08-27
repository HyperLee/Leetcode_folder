using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_202
{
    internal class Program
    {
        /// <summary>
        /// 202. Happy Number
        /// https://leetcode.com/problems/happy-number/?envType=study-plan-v2&envId=top-interview-150
        /// 202. 快乐数
        /// https://leetcode.cn/problems/happy-number/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int n = 19;

            Console.WriteLine(IsHappy(n));
            Console.ReadKey();
        }


        /// <summary>
        /// 參考來源:
        /// 官方解法
        /// https://leetcode.cn/problems/happy-number/solutions/224894/kuai-le-shu-by-leetcode-solution/
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsHappy(int n)
        {
            HashSet<int> seen = new HashSet<int>();

            // 遇到1就停止以及 不要是已出現過的數字避免迴圈循環不停(避免邁入無限循環)
            while (n != 1 && !seen.Contains(n))
            {
                seen.Add(n);
                n = getNext(n);
            }
            return n == 1;
        }


        /// <summary>
        /// 用來計算 n 每個位置上的數字平方和
        /// 題目要求
        /// 
        /// d = n % 10  => 取最右邊位數的數值
        /// n = n / 10  => 取下一個位數的值 ( or 進位)
        /// 
        /// 把n每個位數 拆開
        /// 拆開之後 每個位數 取平方和加總
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static int getNext(int n)
        {
            int totalSum = 0;
            while (n > 0)
            {
                // 取值
                int d = n % 10;
                // 進位
                n = n / 10;
                // 取平方加總
                totalSum += d * d;
            }

            return totalSum;

        }


    }
}
