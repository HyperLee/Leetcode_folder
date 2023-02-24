using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1071
{
    internal class Program
    {
        /// <summary>
        /// leetcode 1071
        /// https://leetcode.com/problems/greatest-common-divisor-of-strings/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string str1 = "ABABAB";
            string str2 = "ABAB";
            Console.WriteLine(GcdOfStrings(str1, str2));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/greatest-common-divisor-of-strings/solution/zi-fu-chuan-de-zui-da-gong-yin-zi-by-leetcode-solu/
        /// 枚舉 拼接出
        /// 
        /// 從str1 與str2 取出 短的長度
        /// 因題目說最常公因數
        /// 所以將最短的長度 拿去整除 str1 與str2
        /// 找出 一個能夠將兩者 共同整除 的 長度
        /// 所以須由大至小找出來 用for loop找
        /// 找出能將str1與str2整除的長度之後
        /// 再把這長度 拿去擷取 字串出來
        /// 再把這字串 串長 跟str1與str2比對
        /// 是否相同
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public static string GcdOfStrings(string str1, string str2)
        {
            int len1 = str1.Length, len2 = str2.Length;

            // 從兩字串中 取出 較短的字串長度出來, 找出最常共同字串長度(由大至小縮減找出來)
            for(int i = Math.Min(len1, len2); i >= 1; i--)
            {
                // 長度需要能夠 % 兩字串為0 才是兩者共同公因數長度
                if(len1 % i == 0 && len2 % i == 0)
                {
                    string x = str1.Substring(0, i);
                    // 比對 字串是否相同
                    if(check(x, str1) && check(x, str2))
                    {
                        return x;
                    }
                }
            }
            return "";
        }

        public static bool check(string t, string s)
        {
            int lenx = s.Length / t.Length;
            StringBuilder ans = new StringBuilder();
            for(int i = 1; i <= lenx; i++)
            {
                ans.Append(t);
            }
            return ans.ToString().Equals(s);
        }


    }
}
