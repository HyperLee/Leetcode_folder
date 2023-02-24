using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_58
{
    internal class Program
    {
        /// <summary>
        /// leetcode 058
        /// https://leetcode.com/problems/length-of-last-word/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "a b     ";

            Console.WriteLine(LengthOfLastWord(s));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/length-of-last-word/solution/zui-hou-yi-ge-dan-ci-de-chang-du-by-leet-51ih/
        /// 從後面往前面找
        /// 从最后一个字母开始继续反向遍历字符串，直到遇到空格或者到达字符串的起始位置。遍历到的每个字母都
        /// 是最后一个单词中的字母，因此遍历到的字母数量即为最后一个单词的长度。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int LengthOfLastWord(string s)
        {
            int index = s.Length - 1;
            // 字串最後空白部分先扣除, 只留下結尾部分是有文字開始
            while (s[index] == ' ')
            {
                index--;
            }
            int wordLength = 0;
            while (index >= 0 && s[index] != ' ')
            {
                wordLength++;
                index--;
            }
            return wordLength;

        }

    }
}
