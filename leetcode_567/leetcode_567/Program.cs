using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_567
{
    internal class Program
    {
        /// <summary>
        /// leetcode 567
        /// https://leetcode.com/problems/permutation-in-string/
        /// 给你两个字符串 s1 和 s2 ，写一个函数来判断 s2 是否包含 s1 的排列。
        /// 果是，返回 true ；否则，返回 false 。
        /// 换句话说，s1 的排列之一是 s2 的 子串 。
        /// 
        /// 在s2中找出一組 子字串 與 s1排序(順序前後可換,但是字母與次數需相同)相同即可
        /// 
        /// method2 方法效能較佳
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s1 = "ab";
            string s2 = "cab";
            Console.WriteLine(CheckInclusion2(s1, s2));
            Console.ReadKey();
        }


        /// <summary>
        /// method1
        /// https://leetcode.cn/problems/permutation-in-string/solution/zi-fu-chuan-de-pai-lie-by-leetcode-q6tp/
        /// 
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool CheckInclusion(string s1, string s2)
        {
            char[] pattern = s1.ToCharArray();
            char[] text = s2.ToCharArray();

            int pLen = s1.Length;
            int tLen = s2.Length;

            int[] pFreq = new int[26];
            int[] winFreq = new int[26];

            // 統計有哪些 英文字母 以及 出現次數
            for (int i = 0; i < pLen; i++)
            {
                pFreq[pattern[i] - 'a']++;
            }

            int pCount = 0;
            for (int i = 0; i < 26; i++)
            {
                if (pFreq[i] > 0)
                {
                    pCount++;
                }
            }

            int left = 0;
            int right = 0;
            // 当滑动窗口中的某个字符个数与 s1 中对应相等的时候才计数
            int winCount = 0;
            while (right < tLen)
            {
                if (pFreq[text[right] - 'a'] > 0)
                {
                    winFreq[text[right] - 'a']++;
                    if (winFreq[text[right] - 'a'] == pFreq[text[right] - 'a'])
                    {
                        winCount++;
                    }
                }
                right++;

                while (pCount == winCount)
                {
                    if (right - left == pLen)
                    {
                        return true;
                    }
                    if (pFreq[text[left] - 'a'] > 0)
                    {
                        winFreq[text[left] - 'a']--;
                        if (winFreq[text[left] - 'a'] < pFreq[text[left] - 'a'])
                        {
                            winCount--;
                        }
                    }
                    left++;
                }
            }
            return false;
        }

        /// <summary>
        /// method2
        /// https://leetcode.cn/problems/permutation-in-string/solution/by-stormsunshine-xkxt/
        /// 效能比較好
        /// 
        /// 如果 s1​ 的长度大于 s2​ 的长度，则 s2​ 一定不包含 s1​ 的排列，返回 false。
        /// 分兩部分判斷
        /// 比如 
        /// s1 長度 5,
        /// s2 長度 12
        /// 一開始先判斷 s1長度(長度5, 也就是前五個) 有無相符
        /// 有 就是是相同
        /// 要是找不到 就找
        /// 後半部 也就是s2從長度6開始 到s2長度截止 部分
        /// 有無相符
        /// 每次往右一步, 原先左邊的就要少一個.  一進一出 往右走
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool CheckInclusion2(string s1, string s2)
        {
            int length1 = s1.Length, length2 = s2.Length;
            if (length1 > length2)
            {
                return false;
            }
            int[] counts1 = new int[26];
            int[] counts2 = new int[26];

            // 如果 s2​ 的首个子字符串中的每个字符的出现次数与 s1 中的每个字符的出现次数相同，则 s2​
            // 的首个子字符串即为 s1​ 的排列，返回 true
            for (int i = 0; i < length1; i++)
            {
                char c1 = s1[i];
                counts1[c1 - 'a']++;
                char c2 = s2[i];
                counts2[c2 - 'a']++;
            }
            if (CheckEqual(counts1, counts2))
            {
                return true;
            }

            // 前面已經把s1長度判斷完畢,後面剩下s2後半部
            // 判斷s1長度停止後 s2後半部
            // 如果 s2​ 的首个子字符串不是 s1​ 的排列，则需要继续遍历其余的子字符串并判断是否为 s1​ 的排列。
            // 每次将子字符串的下标范围向右移动一位，则有一个字符移出子字符串，有一个字符移入子字符串，
            // 每次往右走一個長度, 增加一個 就減少一個
            for (int i = length1; i < length2; i++)
            {
                // 扣除先前
                char prev = s2[i - length1];
                counts2[prev - 'a']--;
                // 增加後面新的
                char curr = s2[i];
                counts2[curr - 'a']++;
                if (CheckEqual(counts1, counts2))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 比對兩兩是否相同
        /// </summary>
        /// <param name="counts1"></param>
        /// <param name="counts2"></param>
        /// <returns></returns>
        public static bool CheckEqual(int[] counts1, int[] counts2)
        {
            for (int i = 0; i < 26; i++)
            {
                if (counts1[i] != counts2[i])
                {
                    return false;
                }
            }
            return true;
        }

    }
}
