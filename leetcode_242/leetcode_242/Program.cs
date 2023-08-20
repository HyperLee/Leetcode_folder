using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_242
{
    internal class Program
    {
        /// <summary>
        /// 242. Valid Anagram
        /// https://leetcode.com/problems/valid-anagram/
        /// 242. 有效的字母异位词
        /// https://leetcode.cn/problems/valid-anagram/
        /// 
        /// 比對兩輸入字串是否相同
        /// 1.出現字母
        /// 2.每次字母出現的頻率(次數)
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "anagram";
            string t = "nagaram";
            Console.WriteLine(IsAnagram2(s, t));
            Console.ReadKey();

        }


        /// <summary>
        /// 方法1 
        /// 最直覺方式
        /// 直接使用內建api
        /// 
        /// 1.排序
        /// 2.排序後兩字串比對
        /// 
        /// 使用 SequenceEqual 比對
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsAnagram(string s, string t)
        {
            if(s.Length != t.Length)
            {
                return false;
            }

            char[] a = s.ToCharArray();
            char[] b = t.ToCharArray();

            Array.Sort(a);
            Array.Sort(b);

            if(a.SequenceEqual(b))
            {
                return true;
            }
            else
            {
                return false;
            }


        }


        /// <summary>
        /// Dictionary 統計 字串s中 出現字母 與 出現頻率
        /// 然後去 t 中相同就扣除
        /// 出現負數代表 不同
        /// 
        ///  s中沒出現, t出現的 直接預設就給負數
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsAnagram2(string s, string t)
        {
            if(s.Length != t.Length)
            {
                return false;
            }

            Dictionary<char, int> dic1 = new Dictionary<char, int>();
            char[] a = s.ToCharArray();
            char[] b = t.ToCharArray();

            // 統計 字串s 出現的字母與頻率
            foreach(char c in a) 
            {
                if(dic1.ContainsKey(c)) 
                {
                    dic1[c]++;
                }
                else
                {
                    dic1.Add(c, 1);
                }
            }

            // s與t比對, 相同就扣除.
            // s沒有t才有, 直接給負數
            foreach(char c in b)
            {
                if(dic1.ContainsKey(c))
                {
                    dic1[c]--;
                }
                else
                {
                    // s中沒出現, t出現的 直接預設就給負數
                    dic1.Add(c, -1);
                }
            }

            // 找出負數就是不同
            foreach(var item in dic1)
            {
                if(item.Value < 0)
                {
                    return false;
                }
            }

            return true;

        }


    }
}
