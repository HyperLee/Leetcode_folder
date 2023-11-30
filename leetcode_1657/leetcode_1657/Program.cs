using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1657
{
    internal class Program
    {
        /// <summary>
        /// 1657. Determine if Two Strings Are Close
        /// https://leetcode.com/problems/determine-if-two-strings-are-close/
        /// 1657. 确定两个字符串是否接近
        /// https://leetcode.cn/problems/determine-if-two-strings-are-close/?envType=daily-question&envId=Invalid%20Date
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string word1 = "abc";
            string word2 = "bca";
            Console.WriteLine(CloseStrings(word1, word2));
            Console.ReadKey();
        }



        /// <summary>
        /// 題目敘述很複雜
        /// 但是其實把握下面敘述重點即可
        /// 
        /// word1, word2 長度要一致
        /// word1, word2 出現的char要一致(相同)
        /// word1, word2 每個char出現的頻率(次數)要一致
        /// </summary>
        /// <param name="word1"></param>
        /// <param name="word2"></param>
        /// <returns></returns>
        public static bool CloseStrings(string word1, string word2)
        {
            // 長度不一致 就錯誤
            if(word1.Trim().Length != word2.Trim().Length)
            {
                return false;
            }

            // 統計 word1
            // key: char, Value:char的頻率
            Dictionary<char, int> dic1 = new Dictionary<char, int>();
            foreach(char value in word1.ToCharArray())
            {
                if(dic1.ContainsKey(value))
                {
                    dic1[value]++;
                }
                else
                {
                    dic1.Add(value, 1);
                }
            }

            // 統計 word2
            // key: char, Value:char的頻率
            Dictionary<char, int> dic2 = new Dictionary<char, int>();
            foreach (char value in word2.ToCharArray()) 
            {
                if (dic2.ContainsKey(value))
                {
                    dic2[value]++;
                }
                else
                {
                    dic2.Add(value, 1);
                }
            }

            List<int> list1 = new List<int>();
            List<int> list2 = new List<int>();

            foreach(var kvp in dic1)
            {
                // 比對 兩個輸入的char是否相同一致 不然就是錯誤
                if(!dic2.ContainsKey(kvp.Key))
                {
                    return false;
                }
            }

            foreach(var kvp in dic1) 
            {
                // word1 char頻率
                list1.Add(kvp.Value);
            }

            // 排序
            list1.Sort();

            foreach(var kvp in dic2)
            {
                // word2 char頻率
                list2.Add(kvp.Value);
            }

            // 排序 
            list2.Sort();

            // 兩者長度一定要相同, 否則第一個判斷就回傳錯誤了
            for(int i = 0; i < list1.Count; i++)
            {
                // 比對 是否 頻率相同, 不同就是錯
                if (list1[i] != list2[i])
                {
                    return false;
                }
            }

            return true;
        }

    }
}
