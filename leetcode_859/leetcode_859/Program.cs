using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_859
{
    internal class Program
    {
        /// <summary>
        /// 859. Buddy Strings
        /// https://leetcode.com/problems/buddy-strings/
        /// 859. 亲密字符串
        /// https://leetcode.cn/problems/buddy-strings/
        /// 
        /// 題目好像沒明確說
        /// 但是看個解法都只能交換一次
        /// 意即只能有兩處位置不同
        /// 超過就 false
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "ab";
            string goal = "ba";
            Console.WriteLine(BuddyStrings(s, goal));
            Console.ReadKey();

        }


        /// <summary>
        /// 官方解法
        /// https://leetcode.cn/problems/buddy-strings/solution/qin-mi-zi-fu-chuan-by-leetcode-solution-yyis/
        /// 
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="goal"></param>
        /// <returns></returns>
        public static bool BuddyStrings(string s, string goal)
        {
            // 兩輸入字串必須相同長度
            if (s.Length != goal.Length)
            {
                return false;
            }

            // s 與 goal 輸入字串相同case
            if (s.Equals(goal))
            {
                // 統計 各字母 出現次數 or 頻率
                int[] count = new int[26];

                for (int i = 0; i < s.Length; i++)
                {
                    count[s[i] - 'a']++;
                    // 有字母出現超過一次 就會有重覆的文字
                    // 故交換重覆就會相同
                    if (count[s[i] - 'a'] > 1)
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                // 兩輸入 字串不同 case
                int first = -1, second = -1;

                for (int i = 0; i < goal.Length; i++)
                {
                    // 找出僅有兩個相同位置,但是字母不同的情況下
                    // 一個為 first, 另一個為second
                    // 可以使得兩位置可以做swap
                    if (s[i] != goal[i])
                    {
                        if (first == -1)
                        {
                            first = i;
                        }
                        else if (second == -1)
                        {
                            second = i;
                        }
                        else
                        {
                            // 超過兩個即太多不相同.
                            return false;
                        }
                    }

                }

                // second為 -1, 代表first不是 -1. 以及兩字串交換後需要相同
                return (second != -1 && s[first] == goal[second] && s[second] == goal[first]);
            }

        }


        /// <summary>
        /// 解法2
        /// https://leetcode.cn/problems/buddy-strings/solution/gong-shui-san-xie-jian-dan-zi-fu-chuan-m-q056/
        /// 
        /// 1.当 s 与 goal 长度 或 词频不同，必然不为亲密字符；
        /// ==>長度要相同 這是必須
        /// 頻率也要相同
        /// 因為 交換過後 兩邊要一致
        /// 代表 原始順序可以不同
        /// 但是 長度以及 出現字母的數量都要相同
        /// 
        /// 2.当「s 与 goal 不同的字符数量为 2（能够相互交换）」或「s 与 goal 不同的字符数量为 0
        /// ，但同时 s 中有出现数量超过 2 的字符（能够相互交换）」时，两者必然为亲密字符。
        /// ==> 前段為 可以swap好幾次.
        /// ==> 後段為 單一字母頻率很多次. 即視為自己交換就好
        /// ex: s:abb  goal:abb 
        /// =>b交換就好 交換過也相同
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="goal"></param>
        /// <returns></returns>
        public bool buddyStrings2(String s, String goal)
        {
            int n = s.Length;
            int m = goal.Length;

            // 長度要一致
            if(n != m)
            {
                return false;
            }

            // 統計兩輸入字串字母頻率
            int[] count1 = new int[26], count2 = new int[26];
            int sum = 0;

            for(int i = 0; i < n; i++)
            {
                int a = s[i] - 'a', b = goal[i] - 'a';
                count1[a]++;
                count2[b]++;

                // 統計有多少個位置不同 字母數量
                if(a != b)
                {
                    sum++;
                }

            }

            bool ok = false;
            for(int i = 0; i < 26; i++)
            {
                if (count1[i] != count2[i])
                {
                    // 兩邊輸入字串 字母出現頻率要一致
                    return false;
                }

                if (count1[i] > 1)
                {
                    // 相同字母頻率超過一
                    ok = true;
                }
            }

            // 有兩處可交換, 或是 相同字母頻率超過一
            return sum == 2 || (sum == 0 && ok);

        }


    }
}
