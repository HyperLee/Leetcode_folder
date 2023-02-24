using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_342
{
    internal class Program
    {
        /// <summary>
        /// leetcode 342  Power of Four
        /// 4的幂
        /// https://leetcode.com/problems/power-of-four/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int n = 16;
            Console.WriteLine("method: " + IsPowerOfFour(n));
            Console.WriteLine("method2:" + IsPowerOfFour2(n));
            Console.WriteLine("method3:" + IsPowerOfFour3(n));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/power-of-four/solution/4de-mi-by-leetcode-solution-b3ya/
        /// method1
        /// mask=(10101010101010101010101010101010)2​
        /// 題目定義32位元
        /// mask & n = 0 
        /// 邏輯運算 直接用and 為0就是 冪次方
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsPowerOfFour(int n)
        {
            return n > 0 && (n & (n - 1)) == 0 && (n & 0xaaaaaaaa) == 0;
        }


        /// <summary>
        /// method2
        /// n與n-1 & 後為0
        /// 如果 n 是 4 的幂，那么它可以表示成 4的n次方 的形式，我们可以发现它除以 3 的余数一定为 1
        /// 4 mod 3 == 1
        /// 且n > 0
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsPowerOfFour2(int n)
        {
            return n > 0 && (n & (n - 1)) == 0 && n % 3 == 1;
        }

        /// <summary>
        /// 有趣解法
        /// 但是會超時 邏輯運篹比較快速
        /// https://leetcode.cn/problems/power-of-four/solution/dai-ma-jian-ji-yi-chong-huan-bu-cuo-de-j-c5pw/
        /// 负数和零 不是4的幂数
        /// 1 是 任何数的幂数
        /// NUM % 4 如果不为 0 肯定不是
        /// 循环遍历查找。如果NUM不等于i*4；肯定不是
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsPowerOfFour3(int n)
        {
            if (n < 0) return false;
            if (n == 1) return true;
            if (n % 4 != 0) return false;
            for(int i = 4; i <=n; i = i * 4)
            {
                if(n == i)
                {
                    return true;
                }
            }
            return false;

        }


    }
}
