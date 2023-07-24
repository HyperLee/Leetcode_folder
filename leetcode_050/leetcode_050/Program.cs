using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_050
{
    internal class Program
    {
        /// <summary>
        /// 50. Pow(x, n)
        /// https://leetcode.com/problems/powx-n/
        /// 50. Pow(x, n)
        /// https://leetcode.cn/problems/powx-n/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            double x = 2.00000;
            int n = -2;

            Console.WriteLine(MyPow(x, n));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/powx-n/solution/by-stormsunshine-zmxx/
        /// 實作 Math.Pow(x, n);
        /// 
        /// 先偷懶用內建
        /// 再找時間實作
        /// </summary>
        /// <param name="x"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double MyPow(double x, int n)
        {
            if(n == 0)
            {
                return 1;
            }

            if(x == 0)
            {
                return 0;
            }

            double a = 0.0;
            
            if (n > 0)
            {
                
                a = Math.Pow(x, n);
                
            }
            else
            {
                a = Math.Pow(x, n);
            }

            return a;
        }
    }
}
