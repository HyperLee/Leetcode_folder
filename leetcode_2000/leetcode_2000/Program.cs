using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2000
{
    internal class Program
    {
        /// <summary>
        /// leetcode 2000 Reverse Prefix of Word
        /// https://leetcode.com/problems/reverse-prefix-of-word/description/
        /// 2000. 反转单词前缀
        /// https://leetcode.cn/problems/reverse-prefix-of-word/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string word = "abcdefd";
            char ch = 'd';
            Console.WriteLine(ReversePrefix(word, ch));
            Console.ReadKey();
        }


        /// <summary>
        /// 1. 先取出 第一個 ch的位置
        /// 2. 反轉原先字串
        /// 3. 把ch之後的文字接回去(從ch下一個文字開始, 直到字串結尾)
        /// </summary>
        /// <param name="word"></param>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static string ReversePrefix(string word, char ch)
        {
            int tempi = -1;
            char[]  word2 = word.ToCharArray();

            // 找出第一個ch位置
            // break用意是 避免拿到第一個以外的位置
            for(int i = 0; i < word.Length; i++)
            {
                if (word2[i] == ch)
                {
                    tempi = i;
                    break;
                }
            }

            // 反轉
            StringBuilder sb = new StringBuilder();
            for(int i = tempi; i >= 0; i--)
            {
                sb.Append(word[i]);
            }

            // 接回ch之後文字
            string cut2 = "";
            cut2 = word.Substring(tempi + 1, word.Length - tempi - 1);
            sb.Append(cut2);

            return sb.ToString();
        }


        /// <summary>
        /// https://leetcode.cn/problems/reverse-prefix-of-word/solution/fan-zhuan-dan-ci-qian-zhui-by-leetcode-s-ruaj/
        /// 
        /// 直接反轉,
        /// 首先找出第一個char的位置
        /// 找到之後 從index = 0 開始 反轉到 char 位置
        /// 後續就直接接上
        /// 
        /// 
        /// 上面方法1 是分兩階段擷取與組合
        /// 本方法2, 只需要從index 0 開始 到 index char 位置 反轉而已
        /// 不需要分兩階段擷取計算長度
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static string ReversePrefix2(string word, char ch)
        {
            int index = word.IndexOf(ch);
            if (index >= 0)
            {
                char[] arr = word.ToCharArray();
                int left = 0, right = index;
                while (left < right)
                {
                    char temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;
                    left++;
                    right--;
                }
                word = new string(arr);
            }
            return word;
        }


    }
}
