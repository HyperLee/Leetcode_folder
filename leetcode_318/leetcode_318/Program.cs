using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_318
{
    internal class Program
    {
        /// <summary>
        /// leetcode_318 Maximum Product of Word Lengths
        /// https://leetcode.com/problems/maximum-product-of-word-lengths/
        /// 
        /// Given a string array words, return the maximum value of length(word[i]) * length(word[j]) 
        /// where the two words do not share common letters. If no such two words exist, return 0.
        /// 
        /// 本題目其實看不懂想表達意思
        /// 什麼事 share common letters?
        /// 共用字母 但是 最短的 單字 又不一定是
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] words = { "a", "ab", "abc", "d", "cd", "bcd", "abcd" };
            Console.WriteLine(MaxProduct(words));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/maximum-product-of-word-lengths/solution/zui-da-dan-ci-chang-du-cheng-ji-by-leetc-lym9/
        /// 方法一：位运算
        /// 
        /// https://leetcode.cn/problems/maximum-product-of-word-lengths/solutions/1105955/gong-shui-san-xie-jian-dan-wei-yun-suan-cqtxq/?envType=daily-question&envId=Invalid+Date
        /// 根据题意进行模拟即可，利用每个 words[i] 只有小写字母，且只需要区分两字符是否有字母重复。
        /// 取出的兩個字母, 不能有重複的字元
        /// 
        /// masks
        /// 位掩码（BitMask），是”位（Bit）“和”掩码（Mask）“的组合词。
        /// https://www.cnblogs.com/Ada-CN/p/16381928.html
        /// https://segmentfault.com/a/1190000039239875
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public static int MaxProduct(string[] words)
        {
            int length = words.Length;
            int[] masks = new int[length];

            for (int i = 0; i < length; i++)
            {
                String word = words[i];
                int wordLength = word.Length;
                // |= => or 邏輯意思. 其中一個為一 就唯一
                for (int j = 0; j < wordLength; j++)
                {
                    masks[i] |= 1 << (word[j] - 'a');
                }
            }

            int maxProd = 0;
            // 題目要求, 找出兩兩長度相乘之後 最長者
            for (int i = 0; i < length; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    // == 0 代表 倆倆沒有重複
                    if ((masks[i] & masks[j]) == 0)
                    {
                        maxProd = Math.Max(maxProd, words[i].Length * words[j].Length);
                    }
                }
            }

            return maxProd;

        }

    }
}
