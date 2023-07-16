using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 優化遞迴方式
{
    internal class Program
    {
        /// <summary>
        /// 70. Climbing Stairs
        /// https://leetcode.com/problems/climbing-stairs/submissions/
        /// 70. 爬楼梯
        /// https://leetcode.cn/problems/climbing-stairs/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int n = 3;
            Console.WriteLine("total step:" + ClimbStairs2(n));
            Console.WriteLine("total step:" + ClimbStairs3(n));
            Console.ReadKey();
        }


        /// <summary>
        /// 方法2
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int ClimbStairs2(int n)
        {
            {
                if (n == 1)
                    return 1;
                if (n == 2)
                    return 2;
                return ClimbStairs2(n - 1) + ClimbStairs2(n - 2);
            }

        }


        /// <summary>
        /// 方法3
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int ClimbStairs3(int n)
        {
            if (n == 1)
            {
                return 1;
            }

            if (n == 2)
            {
                return 2;
            }

            int result = 0;
            int pre = 1;
            int next = 2;
            for (int i = 2; i < n; i++)
            {
                result = pre + next;
                pre = next;
                next = result;
            }

            return result;
        }


    }
}
