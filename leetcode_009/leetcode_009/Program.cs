using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_009
{
    class Program
    {
        /// <summary>
        /// 9. Palindrome Number
        /// https://leetcode.com/problems/palindrome-number/
        /// 
        /// 9. 回文数
        /// https://leetcode.cn/problems/palindrome-number/description/
        /// 
        /// 輸入是整數 int
        /// 给你一个整数 x ，如果 x 是一个回文整数，返回 true ；否则，返回 false 。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int s = 11;

            Console.WriteLine("方法1: " + IsPalindrome(s));
            Console.WriteLine("方法2: " + IsPalindrome2(s));
            Console.WriteLine("方法3: " + IsPalindrome3(s));
            Console.ReadKey();
        }


        /// <summary>
        /// 宣告一個 字串 暫存
        /// 把原先輸入的 int, 反轉輸入
        /// 在跟原先的輸入對比
        /// 是否相同
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static bool IsPalindrome(int x)
        {
            string s_input = x.ToString();
            string res = "";

            // 反轉存入 res
            for (int i = s_input.Length - 1; i >= 0; i--)
            {
                res += s_input[i];
            }

            // res 與原先輸入 x 字串 比對
            if (res == s_input)
            {
                // 相同
                return true;
            }
            else
            {
                // 不同
                return false;
            }

        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/palindrome-number/solutions/281686/hui-wen-shu-by-leetcode-solution/
        /// 官方解法 
        /// 方法一：反转一半数字
        /// 將一半的位數反轉比對
        /// 不用整串全部反轉
        /// 由于整个过程我们不断将原始数字除以 10，然后给反转后的数字乘上 10，
        /// 所以，当原始数字小于或等于反转后的数字时，就意味着我们已经处理了一半位数的数字了。
        /// 
        /// 
        /// 當結尾是 0, 迴文開頭也必須是 0
        /// 但是只有 0 才能達成這條件
        /// 所以當結尾是 0, 但是 輸入 int 開頭非 0
        /// 就是錯誤, 不是迴文
        /// 
        /// -123  負數不會是迴文, 因為有 負號(只有開頭有, 結尾不會有負號)
        /// 
        /// % => 取出低位數字
        /// / => 輸入 int 右移 一位
        /// revertedNumber * 10 + x % 10; => 反轉後的數字
        /// 
        /// sample:
        /// 对于数字 1221，如果执行 1221 % 10，我们将得到最后一位数字 1，要得到倒数第二位数字，我们可以先通过除以 10 把最后
        /// 一位数字从 1221 中移除，1221 / 10 = 122，再求出上一步结果除以 10 的余数，122 % 10 = 2，就可以得到倒数第二位数字。
        /// 如果我们把最后一位数字乘以 10，再加上倒数第二位数字，1 * 10 + 2 = 12，就得到了我们想要的反转后的数字。如果继续这个
        /// 过程，我们将得到更多位数的反转数字。
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static bool IsPalindrome2(int x)
        {
            // 特殊情况：
            // 如上所述，当 x < 0 时，x 不是回文数。
            // 同样地，如果数字的最后一位是 0，为了使该数字为回文，
            // 则其第一位数字也应该是 0
            // 只有 0 满足这一属性
            if (x < 0 || (x % 10 == 0 && x != 0))
            {
                return false;
            }

            int revertedNumber = 0;
            while (x > revertedNumber)
            {
                // revertedNumber 取反轉後數字
                revertedNumber = revertedNumber * 10 + x % 10;
                // 原始數 x 右移
                x /= 10;
            }

            // 当数字长度为奇数时，我们可以通过 revertedNumber/10 去除处于中位的数字。
            // 例如，当输入为 12321 时，在 while 循环的末尾我们可以得到 x = 12，revertedNumber = 123，
            // 由于处于中位的数字不影响回文（它总是与自己相等），所以我们可以简单地将其去除。
            return x == revertedNumber || x == revertedNumber / 10;
        }


        /// <summary>
        /// 雙指針
        /// 左右對比
        /// 相同就縮小範圍,
        /// 不同就是錯誤
        /// 
        /// memo: -123  負數不會是迴文, 因為有 負號(只有開頭有, 結尾不會有負號)
        /// 
        /// int 沒辦法直接取長度, 要轉字串
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static bool IsPalindrome3(int x)
        {
            // 負數不會是 迴文
            if(x < 0)
            {
                return false;
            }

            int left = 0;
            int right = x.ToString().Length - 1;
            // 轉字串 來比對
            string input = x.ToString();

            while(left < right)
            {
                if (input[left] == input[right])
                {
                    // 比對相同, 就縮小左右距離
                    left++;
                    right--;
                }
                else
                {
                    // 不同就是錯誤
                    return false;
                }
            }

            return true;
        }

    }
}
