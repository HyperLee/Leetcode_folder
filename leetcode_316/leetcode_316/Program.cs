using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_316
{
    class Program
    {
        /// <summary>
        /// leetcode 316
        /// https://leetcode.com/problems/remove-duplicate-letters/
        /// 
        /// https://leetcode-cn.com/problems/remove-duplicate-letters/solution/qu-chu-zhong-fu-zi-mu-by-leetcode-soluti-vuso/
        /// 去中文版 力扣 看官方解法 下面討埨 會有訊息 可用
        /// 
        /// 值得一提的是，ch - 'a'这种形式直接将26个不同小写字母变成索引，a是nums[0],b是nums[1]诸如此类
        /// 。同理要是想把字符串中是数字的字符转换成整型，只要ch - '0' 就可以得到对应的整数。
        /// 
        /// 題目:
        /// 给你一个字符串 s ，请你去除字符串中重复的字母，使得每个字母只出现一次。
        /// 需保证 返回结果的字典序最小（要求不能打乱其他字符的相对位置）。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "bcabc"; //"cbacdcbc"

            /* // test code
            var count = new int[26]; // 每個字母最後一次出現的位置
            for (int i = 0; i < s.Length; i++)
            {
                count[s[i] - 'a']++;
            }

            // 輸出字母26個字 每個字母次數
            for (int j = 0; j < 26; j++)
            {
                Console.WriteLine(" item: " + j + ", times: " + count[j]);
            }
            */

            Console.WriteLine(RemoveDuplicateLetters(s));
            //Console.WriteLine(RemoveDuplicateLetters2(s));
            Console.ReadKey();

        }



        /// <summary>
        /// https://blog.csdn.net/sinat_42483341/article/details/118633872
        /// method 1
        /// 
        /// https://leetcode.cn/problems/remove-duplicate-letters/solution/ti-jie-yao-jia-ru-de-zi-fu-du-jin-liang-0ajr0/
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveDuplicateLetters(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            var count = new int[26]; // 每個字母最後一次出現的位置
            for (int i = 0; i < s.Length; i++)
            {
                count[s[i] - 'a']++;// 統計每次字母次數
            }

            var sb = new StringBuilder();
            var hs = new HashSet<char>();
            for (int i = 0; i < s.Length; i++)
            {
                count[s[i] - 'a']--;
                if (hs.Contains(s[i]))
                {
                    continue;
                }   

                // ascii字典順序 有重複 且大的在前面小的在後面, 不是遞增順序狀況 ex: cbacb
                // 需保证 返回结果的字典序最小（要求不能打乱其他字符的相对位置）使得每个字母只出现一次
                // 後面進入的 會去檢查上述行為, 抓到就 移除
                while (sb.Length > 0 && sb[sb.Length - 1] > s[i] && count[sb[sb.Length - 1] - 'a'] > 0)
                {
                    hs.Remove(sb[sb.Length - 1]);
                    sb.Length--;
                }
                hs.Add(s[i]);
                sb.Append(s[i]);
            }
            return sb.ToString();

        }


        /// <summary>
        /// method2
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveDuplicateLetters2(string s)
        {
            var charAndCount = new int[256];

            foreach (var c in s)
            {
                charAndCount[c]++;
            }

            var isVisited = new HashSet<char>();
            var charStack = new Stack<char>();

            foreach (var c in s)
            {
                if (!isVisited.Contains(c))
                {
                    while (charStack.Any())
                    {
                        var preChar = charStack.Peek();

                        if (charAndCount[preChar] > 0 && preChar > c)
                        {
                            var popped = charStack.Pop();
                            isVisited.Remove(popped);
                        }
                        else
                        {
                            break;
                        }
                    }

                    isVisited.Add(c);
                    charStack.Push(c);
                }

                charAndCount[c]--;
            }

            var result = new List<char>();

            //  Any<TSource>(IEnumerable<TSource>) 	判斷序列是否包含任何項目。
            while (charStack.Any())
            {
                result.Add(charStack.Pop());
            }

            result.Reverse();

            return string.Join("", result);
        }


    }
}
