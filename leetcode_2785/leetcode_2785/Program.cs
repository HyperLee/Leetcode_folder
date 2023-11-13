using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2785
{
    internal class Program
    {
        /// <summary>
        /// 2785. Sort Vowels in a String
        /// https://leetcode.com/problems/sort-vowels-in-a-string/
        /// 2785. 将字符串中的元音字母排序
        /// https://leetcode.cn/problems/sort-vowels-in-a-string/
        /// 
        /// a, e, i, o, u 為母音
        /// 其餘皆不是
        /// 母音須按照ascii 順序遞增排序
        /// 其餘非母音者 位置不變
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "loba";
            Console.WriteLine(SortVowels(s));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/sort-vowels-in-a-string/solutions/2363464/ji-shu-pai-xu-tong-pai-xu-lai-pai-xu-yua-0ihb/
        /// https://leetcode.com/problems/sort-vowels-in-a-string/solutions/4281258/c-solution-for-sort-vowels-in-a-string-problem/
        /// 
        /// 1.找出母音 放到 List裡面存放
        /// 2.把母音部分排序
        /// 3.放回去, 非母音位置不變, 母音位置要替換原先輸入字串的順序
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string SortVowels(string s)
        {
            char[] chars = s.ToCharArray();
            List<char> Vowels = new List<char>();

            // 抓出母音, 放入 Vowels
            foreach (char c in chars) 
            {
                if(IsVowels(c))
                {
                    Vowels.Add(c);
                }
            }

            // 題目要求 母音排序.ASCII由小至大
            Vowels.Sort();

            // 放回chars, 非母音位置不動, 母音按照ascii順序 塞入
            int vowelidx = 0;
            for(int i = 0; i < chars.Length; i++)
            {
                if (IsVowels(chars[i]))
                {
                    chars[i] = Vowels[vowelidx++];
                }
            }

            return new string(chars);
        }


        /// <summary>
        /// 判斷是不是母音
        /// 輸入資料: 統一 一律轉小寫, 因題目有說大小寫都有
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsVowels(char c)
        {
            char lowerC = char.ToLower(c);
            return lowerC == 'a' || lowerC =='e' || lowerC == 'i' || lowerC =='o' || lowerC =='u';
        }

    }
}
