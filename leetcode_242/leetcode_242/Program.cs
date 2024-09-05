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
            Console.WriteLine("方法1: " + IsAnagram(s, t));
            Console.WriteLine("方法2: " + IsAnagram2(s, t));
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
        /// 
        /// 在 C# 中，SequenceEqual 是用于比较两个序列（如数组、列表等）是否相等的 LINQ 方法。它会逐个比较两个序列中的元素，检查它们的长度是否相同，且每个位置的元素是否相等。
        /// 使用场景：
        /// 比较两个数组是否相等。
        /// 比较两个列表是否包含相同的元素（顺序和内容必须相同）。
        /// 
        /// 如果两个序列的长度不同，或者任意位置的元素不同，SequenceEqual 返回 false，否则返回 true。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsAnagram(string s, string t)
        {
            // 這個其實可以不用寫, 但是先做判斷,
            // 可以節省時間
            if(s.Length != t.Length)
            {
                return false;
            }

            char[] a = s.ToCharArray();
            char[] b = t.ToCharArray();

            // 排序英文順序 asc
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
        /// Dictionary 統計 字串 s 中 出現字母 與 出現頻率
        /// 然後去 t 中相同就扣除
        /// 出現負數代表 不同
        /// 
        ///  s 中沒出現, t 出現的 直接預設就給負數
        ///  
        /// 此方法也不太需要預先排序了
        /// 畢竟都枚舉 統計 char 與 次數了
        /// 
        /// 輸入的是字串但是比對的是 char
        /// 所以要把 string 轉 char[]
        /// 
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

            Dictionary<char, int> dic = new Dictionary<char, int>();
            char[] a = s.ToCharArray();
            char[] b = t.ToCharArray();

            // 統計字串 s 出現的字母與頻率
            foreach(char c in a) 
            {
                if(dic.ContainsKey(c)) 
                {
                    dic[c]++;
                }
                else
                {
                    dic.Add(c, 1);
                }
            }

            // s 與 t 比對, char 相同就扣除.
            // s 沒有 t 才有, 直接給負數
            foreach(char c in b)
            {
                if(dic.ContainsKey(c))
                {
                    dic[c]--;
                }
                else
                {
                    // s 中沒出現, t 出現的直接預設就給負數
                    dic.Add(c, -1);
                }
            }

            // 找出負數就是不同
            foreach(var item in dic)
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
