using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1576
{
    internal class Program
    {
        /// <summary>
        /// leetcode 1576. Replace All ?'s to Avoid Consecutive Repeating Characters
        /// 替换所有的问号
        /// https://leetcode.com/problems/replace-all-s-to-avoid-consecutive-repeating-characters/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string input = "ab?";

            Console.WriteLine(ModifyString(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 參考此寫法
        /// https://leetcode.com/problems/replace-all-s-to-avoid-consecutive-repeating-characters/solutions/837923/c/?q=c%23&orderBy=most_relevant
        /// 
        /// 宣告 左右用意在於 避免i溢位
        /// 當超過就要用 null 代替
        /// 
        /// 題目雖然是說替代,但是只要不連續即可
        /// 既然這樣那只要關注 i - 1, i, i + 1
        /// 三個那只需要三個文字即可
        /// 不需要跑完全部英文26個字母 下去找尋
        /// 
        /// (char?)null  ==> string 賦予 null 的寫法
        /// 直接用string.empty 會錯
        /// 
        /// 本題須注意的是 溢位 與 string給null 的用法
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ModifyString(string s)
        {
            int n = s.Length;
            char[] arr = s.ToCharArray();
            for(int i = 0; i < n; i++)
            {
                // 避免有 arr[i - 1] < 0 的溢位問題
                var left = i > 0 ? arr[i - 1] : (char?)null;

                // 避免有 arr[i + 1] 超過 n.Length 的溢位問題
                var right = i < n - 1 ? arr[i + 1] : (char?)null;
                
                if (arr[i] == '?')
                {
                    if (left != 'a' && right != 'a')
                    {
                        arr[i] = 'a';
                    }
                    else if (left != 'b' && right  != 'b')
                    {
                        arr[i] = 'b';
                    }
                    else if (left != 'c' && right != 'c')
                    {
                        arr[i] = 'c';
                    }

                }
            }

            return new String(arr);
        }

    }
}
