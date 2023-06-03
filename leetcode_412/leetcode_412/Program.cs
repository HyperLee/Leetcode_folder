using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_412
{
    internal class Program
    {
        /// <summary>
        /// leetcode 412. Fizz Buzz
        /// https://leetcode.com/problems/fizz-buzz/
        /// 412. Fizz Buzz
        /// https://leetcode.cn/problems/fizz-buzz/solution/fizz-buzz-by-leetcode-solution-s0s5/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int n = 15;
            Console.WriteLine(FizzBuzz(n));
            Console.ReadKey();
        }


        /// <summary>
        /// 輸入的 n 從 1　開始
        /// 但是　我用array 從 0 開始
        /// 故 output[length - 1]  要減掉 1
        /// 不然會出錯
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static IList<string> FizzBuzz(int n)
        {
            string[] output = new string[n];
            for (int i = 1; i <= n; i++)
            {
                if ((i % 3 == 0) && (i % 5 == 0))
                {
                    // 3 && 5 倍數 替換
                    output[i - 1] = "FizzBuzz";
                }
                else if(i % 3 == 0)
                {
                    // 3 的倍數
                    output[i - 1] = "Fizz";
                }
                else if(i % 5 == 0)
                {
                    // 5 的倍數
                    output[i - 1] = "Buzz";
                }
                else
                {
                    // 其餘顯示第i序號
                    output[i - 1] = i.ToString();
                }
            }

            foreach (string s in output) 
            {
                Console.Write(s + ",");
            }
            Console.WriteLine();

            return output;
        }


    }
}
