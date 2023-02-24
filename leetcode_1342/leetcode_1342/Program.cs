using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1342
{
    internal class Program
    {
        /// <summary>
        /// leetcode 1342  Number of Steps to Reduce a Number to Zero
        /// 将数字变成 0 的操作次数
        /// https://leetcode.com/problems/number-of-steps-to-reduce-a-number-to-zero/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int num = 8;
            Console.WriteLine("將" + num + "變成0所需要次數: " + NumberOfSteps(num));
            Console.ReadKey();
        }

        /// <summary>
        /// https://blog.csdn.net/qq_40902709/article/details/106728283
        /// 给你一个非负整数 num ，请你返回将它变成 0 所需要的步数。
        /// 如果当前数字是偶数，你需要把它除以 2 ；否则，减去 1 。
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int NumberOfSteps(int num)
        {
            int flag = 0;

            // while直到數值不為0 才停止
            while( num != 0)
            {
                if(num % 2 ==0)
                {
                    num /= 2;
                }
                else
                {
                    num--;
                }
                flag++;
            }
            return flag;
        }
    }
}
