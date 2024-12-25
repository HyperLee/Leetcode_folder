using System.Text;

namespace leetcode_1071
{
    internal class Program
    {
        /// <summary>
        /// 1071. Greatest Common Divisor of Strings
        /// https://leetcode.com/problems/greatest-common-divisor-of-strings/
        /// 
        /// 1071. 字符串的最大公因子
        /// https://leetcode.cn/problems/greatest-common-divisor-of-strings/description/
        /// 
        /// 对于字符串 s 和 t，只有在 s = t + t + t + ... + t + t（t 自身连接 1 次或多次）时，我们才认定 “t 能除尽 s”。
        /// 给定两个字符串 str1 和 str2 。返回 最长字符串 x，要求满足 x 能除尽 str1 且 x 能除尽 str2 。
        /// 
        /// 最大公因數（英語：highest common factor，hcf）也稱最大公約數（英語：greatest common divisor，gcd）是數學詞彙，
        /// 指能夠整除多個非零整數的最大正整數。例如8和12的最大公因數為4。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string str1 = "ABABAB";
            string str2 = "ABAB";
            Console.WriteLine("res: " + GcdOfStrings(str1, str2));
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/greatest-common-divisor-of-strings/solution/zi-fu-chuan-de-zui-da-gong-yin-zi-by-leetcode-solu/
        /// 约数，最大公约数
        /// 辗转相除法
        /// 枚舉 拼接出答案
        /// 
        /// 從 str1 與 str2 取出 短的長度
        /// 因題目說最長公因數
        /// 所以將最短的長度 拿去整除 str1 與 str2
        /// 找出 一個能夠將兩者 共同整除 的 長度
        /// 所以須由大至小找出來用 for loop 找
        /// 找出能將 str1 與 str2 整除的長度之後
        /// 再把這長度拿去擷取字串出來
        /// 再把這字串長跟 str1 與 str2 比對
        /// 是否相同
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public static string GcdOfStrings(string str1, string str2)
        {
            int len1 = str1.Length, len2 = str2.Length;
            // 找出公因數字串
            string GCDWord = str1.Substring(0, GCD(len1, len2));
            // 公因數字串與原輸入字串比對
            if (check(GCDWord, str1) && check(GCDWord, str2))
            {
                return GCDWord;    
            }

            return "";
        }


        /// <summary>
        /// 公因數字串與原字串比對是否相同
        /// 或是 如題目所說
        /// 原字串能經由切割很多次公因數字串組合而成
        /// s = t + t + t + ... + t + t
        /// 
        /// 比對方式, 這兩種寫法都是相同意思
        /// return ans.ToString() == s;
        /// return ans.ToString().Equals(s);
        /// </summary>
        /// <param name="t">公因數字串</param>
        /// <param name="s">字串原始</param>
        /// <returns></returns>
        public static bool check(string t, string s)
        {
            // 公因數字串在原始字串需要跑幾輪
            int lenx = s.Length / t.Length;

            StringBuilder ans = new StringBuilder();
            for (int i = 1; i <= lenx; i++)
            {
                ans.Append(t);
            }

            // 回傳方式, 這兩種寫法都可以
            return ans.ToString() == s;
            //return ans.ToString().Equals(s);
        }


        /// <summary>
        /// GCD 最大公因數長度 計算
        /// 辗转相除法求得两个字符串长度的最大公约数 gcd(len1​,len2​)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int GCD(int a, int b)
        {
            int remainder = a % b;
            // 輾轉相除法
            while(remainder != 0)
            {
                a = b;
                b = remainder;
                remainder = a % b;
            }

            return b;
        }
    }
}
