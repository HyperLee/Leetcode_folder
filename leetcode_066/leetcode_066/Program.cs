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
        /// 66. Plus One
        /// https://leetcode.com/problems/plus-one/description/
        /// 66. 加一
        /// https://leetcode.cn/problems/plus-one/description/
        /// 
        /// 題目要求很簡單就是 + 1
        /// 要區分兩種 case
        /// 1. 除了數字 9 之外數字都單純 + 1
        /// 2. 數字 9, 有進位 問題
        /// 
        /// 進位會 讓原先位置 變成 0
        /// 往上一個位數 要加 1
        /// 
        /// 推薦方法2
        /// 此方法好理解, 也是正常人想不出來方式
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 8, 7, 9 };
            //Console.WriteLine(PlusOne(input));
            //PlusOne(input);

            PlusOne2(input);
            Console.ReadKey();
        }


        /// <summary>
        /// ref:
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


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/plus-one/solutions/4481/java-shu-xue-jie-ti-by-yhhzw/
        /// 
        /// 加一得十进一位个位数为 0 加法运算如不出现进位就运算结束了且进位只会是一。
        /// 只需要判断有没有进位并模拟出它的进位方式，如十位数加 1 个位数置为 0，如此循环直到判断没有再进位就退出循环返回结果。
        /// 然后还有一些特殊情况就是当出现 99、999 之类的数字时，循环到最后也需要进位，出现这种情况时需要手动将它进一位。
        /// 
        /// 簡單說就是 模擬數學計算
        /// 1. 數字非 9 就直接 ++ 然後顯示
        /// 2. 數字為 9 就需要進位(高位進位), 然後原本 index 位置為 0
        /// 2.1 當輸入數字 開頭也為 9 , 此時需要宣告陣列為 digits.Length + 1
        ///     因為要再往上進位, 此時開頭數字要給 1
        ///     
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static int[] PlusOne2(int[] digits)
        {
            // 正常數學計算 由後面往前; 從低位往高位
            for(int i = digits.Length - 1; i >= 0; i--)
            {
                digits[i]++;
                // 數字 + 1 之後, 計算該數字個位數是否為 0
                digits[i] = digits[i] % 10;
                // 非 0 直接輸出, 若是為 0 就有進位問題
                if (digits[i] != 0)
                {
                    Console.WriteLine("-----------------");
                    Console.Write("方法2 沒進位, ans:");
                    foreach (int b in digits)
                    {
                        Console.Write(b);
                    }

                    return digits;
                }
            }

            // 需要額外進位.  ex: 99, 999
            digits = new int[digits.Length + 1];
            // 進位開頭給 1
            digits[0] = 1;

            Console.WriteLine("-----------------");
            Console.Write("方法2 有進位, ans:");
            foreach (int b in digits)
            {
                Console.Write(b);
            }

            return digits;
        }

    }
}
