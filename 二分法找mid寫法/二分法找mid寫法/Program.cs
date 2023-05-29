using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 二分法找mid寫法
{
    internal class Program
    {
        /// <summary>
        /// https://blog.csdn.net/m0_50086696/article/details/123353057
        /// 二分法找mid 
        /// 兩種寫法 
        /// 測試
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int left = 0;
            int right = 11;

            Method1(left, right);
            Method2(left, right);

            Console.ReadKey();
        }


        /// <summary>
        /// mid = (left + right) / 2;
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public static void Method1(int left, int right)
        {
            int mid = 0;
            mid = (left + right) / 2;

            Console.WriteLine("Method1: " + mid);

        }


        /// <summary>
        ///  mid = (right - left) / 2 + left;
        ///  此方法通常是用來防止 溢位 使用
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public static void Method2(int left, int right)
        {
            int mid = 0;
            mid = (right - left) / 2 + left;

            Console.WriteLine("Method2: " + mid);

        }
    }
}
