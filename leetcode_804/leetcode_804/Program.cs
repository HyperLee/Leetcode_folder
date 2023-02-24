using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_804
{
    internal class Program
    {
        /// <summary> 
        /// leetcode 804 唯一摩尔斯密码词 Unique Morse Code Words
        /// https://leetcode.com/problems/unique-morse-code-words/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] input = { "a", "a" };
            int a = 0;
            a = UniqueMorseRepresentations(input);
            Console.WriteLine(a);
            Console.ReadKey();

        }

        public static string[] MORSE = {".-", "-...", "-.-.", "-..", ".", "..-.", "--.",
                                      "....", "..", ".---", "-.-", ".-..", "--", "-.",
                                      "---", ".--.", "--.-", ".-.", "...", "-", "..-",
                                      "...-", ".--", "-..-", "-.--", "--.."};

        /// <summary>
        /// 我们将数组 words\textit{words}words 中的每个单词按照莫尔斯密码表转换为摩尔斯码
        /// ，并加入哈希集合中，最终的答案即为哈希集合中元素的个数。
        /// 
        /// 
        /// HashSet中的元素唯一性
        /// 如果你向 HashSet 中插入重复的元素，它的内部会忽视这次操作而不像别的集合一样抛出异常
        /// 
        /// HashSet 只能包含唯一的元素，它的内部结构也为此做了专门的优化，值得注意的是，HashSet 也可以
        /// 存放单个的 null 值，可以得出这么一个结论：如何你想拥有一个具有唯一值的集合，那么 HashSet 就是
        /// 你最好的选择，何况它还具有超高的检索性能。
        /// 
        /// 透過hash儲存 不重複資料 最後計算count即可
        /// https://segmentfault.com/a/1190000038565893
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public static int UniqueMorseRepresentations(string[] words)
        {
            ISet<string> seen = new HashSet<string>();
            foreach (string word in words)
            {
                StringBuilder code = new StringBuilder();
                foreach (char c in word)
                {
                    code.Append(MORSE[c - 'a']);
                }
                seen.Add(code.ToString());
            }
            return seen.Count;
        }

    }
}
