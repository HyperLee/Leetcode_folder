using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_387
{
    class Program
    {
        /// <summary>
        /// leetcode 387  First Unique Character in a String
        /// https://leetcode.com/problems/first-unique-character-in-a-string/
        /// 
        /// Q: 
        /// Given a string s, find the first non-repeating character in it and return its index. 
        /// If it does not exist, return -1.
        /// 
        /// 给定一个字符串 s ，找到 它的第一个不重复的字符，并返回它的索引 。如果不存在，则返回 -1 。
        /// 字串長度0開始 找出 最先不重複的英文字
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "";
            s = "aaabc";
            Console.WriteLine("Version1: " + FirstUniqChar(s));
            //Console.WriteLine("Version2: " + FirstUniqChar2(s));
            Console.ReadKey();
        }


        /// <summary>
        /// https://ithelp.ithome.com.tw/articles/10222391
        /// method1
        /// 
        /// 這題要找出整個字串裡，第一個只出現一次的 char 的 index。
        /// 1. 宣告一個 int[] array, size = 128
        /// 2. 統計每個 char 的個數 (array[(int)char]) 
        ///    ==> ascii 位置 數量累加
        ///    如 a:97,  aa=>97位置數量會是2
        /// 3. 再用另一個 for 迴圈去查找有沒有個數為 1 的char (array[s[i]] == 1)
        ///    若 有 ，則回傳 index
        ///    若 無 ，則回傳 -1
        ///    
        /// 根據 char ascii code
        /// A-Z 為 65 ~ 90
        /// a-z 為 97 ~ 122
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int FirstUniqChar(string s)
        {
            int[] array = new int[128];
            // 先把輸入轉成ascii 塞入array裡面
            for (int i = 0; i < s.Length; i++)
            {
                array[s[i]]++;
            }
            // 從array裡面由0開始找出第一個 次數為1的就是 題目要求
            for (int i = 0; i < s.Length; i++)
            {
                //int aa = 0;
                //aa = array[s[i]];
                if (array[s[i]] == 1)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// method2
        /// 
        /// 就是逐一判斷每個 char 的 IndexOf 及 LastIndexOf 是不是一樣，若 一樣 就代表它是整個 string 的 唯一值 啦！
        /// 不過這樣的寫法時間複雜度 (Time Complexity) 是 O(n^2) 唷！
        /// 因為 Index.Of 與 LastIndexOf 底層也是用迴圈 去查找。
        /// 
        /// IndexOf : 指定字串在原字串中第一次出現的位置
        /// LastlndexOf : 指定字串在查找的字串中最後一次出現的位置
        /// 
        ///  IndexOf、LastIndexOf都是返回一個位置，是個整數值；找不到都返回-1；
        /// IndexOf是從左向右查，LastIndexOf是從右向左查，
        /// 不管是IndexOf還是LastIndexOf，索引序列都是從左到右的(起始值是0)
        /// 
        /// 兩個相等 就是 字串裡面唯一
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int FirstUniqChar2(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                var c = s[i];
                if (s.IndexOf(c) == s.LastIndexOf(c))
                    return i;
            }
            return -1;
        }


    }
}
