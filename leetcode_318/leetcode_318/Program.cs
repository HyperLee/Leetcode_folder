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
        /// 
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
                for (int j = 0; j < wordLength; j++)
                {
                    masks[i] |= 1 << (word[j] - 'a');
                }
            }
            int maxProd = 0;
            for (int i = 0; i < length; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
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
