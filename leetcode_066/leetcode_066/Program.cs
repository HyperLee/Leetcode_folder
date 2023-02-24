using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_066
{
    internal class Program
    {
        /// <summary>
        /// leetcode 066
        /// https://leetcode.com/problems/plus-one/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 9,9 };
            //Console.WriteLine(PlusOne(input));
            PlusOne(input);
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/plus-one/solution/jia-yi-by-leetcode-solution-2hor/
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static int[] PlusOne(int[] digits)
        {
            int n = digits.Length;
            for (int i = n - 1; i >= 0; --i)
            {
                // 不為9
                if (digits[i] != 9)
                {
                    ++digits[i];
                    for (int j = i + 1; j < n; ++j)
                    {
                        // 為9的 進為之後 後面都要給0
                        digits[j] = 0;
                    }
                    foreach(int a in digits)
                    {
                        Console.WriteLine(a);
                    }
                    return digits;
                }
            }

            // digits 中所有的元素均为 9
            int[] ans = new int[n + 1];
            ans[0] = 1;
            foreach (int b in ans)
            {
                Console.WriteLine(b);
            }
            return ans;

        }

    }
}
