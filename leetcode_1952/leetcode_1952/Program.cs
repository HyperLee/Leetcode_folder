using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1952
{
    internal class Program
    {
        /// <summary>
        /// 1952. Three Divisors
        /// https://leetcode.com/problems/three-divisors/
        /// 1952. 三除数
        /// https://leetcode.cn/problems/three-divisors/
        /// 
        /// 又是一個數學題,
        /// 要有觀念可能比較好解
        /// 不然一時還真看不懂解法
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int input = 4;
            Console.WriteLine(IsThree(input));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/three-divisors/solution/san-chu-shu-by-leetcode-solution-z1b4/
        /// 
        /// x = n / x，此时新增的正除数数目为 1
        /// x != n / x，此时新增的正除数数目为 2。
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsThree(int n)
        {
            int cnt = 0;

            for(int i = 1; i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    if (i != n / i)
                    {
                        // 此时 i 与 n / i 为不同整数
                        // 推測 +2 為 1與n本身
                        cnt += 2;
                    }
                    else
                    {
                        // 此时 i 与 n / i 相等
                        // 其餘能夠整除者
                        cnt += 1;
                    }
                }

                if(cnt == 3)
                {
                    return true;
                }

            }

            return cnt == 3;
        }


    }
}
