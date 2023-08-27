using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace leetcode_205
{
    internal class Program
    {
        /// <summary>
        /// 205. Isomorphic Strings
        /// https://leetcode.com/problems/isomorphic-strings/description/
        /// 205. 同构字符串
        /// https://leetcode.cn/problems/isomorphic-strings/
        /// 
        /// 我偏好Dictionary方法
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "egg";
            string t = "add";
            Console.WriteLine(IsIsomorphic(s, t));
            Console.ReadKey();

        }


        /// <summary>
        /// 此解法非常有趣
        /// 值得參考學習
        /// 來源:
        /// https://leetcode.cn/problems/isomorphic-strings/solutions/34208/c-ji-bai-liao-9231-de-ti-jiao-shi-yong-zi-fu-shou-/
        /// 使用字符首次出现的等价序列
        /// 
        /// 例子： paper 与 title paper中各字母首次出现下标的序列为: [0, 1, 0, 3, 4]， 
        /// title中各字母首次出现下标的序列为: [0, 1, 0, 3, 4] 因为下标数组一致，所以
        /// 两字符串同构。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>

        public static bool IsIsomorphic(string s, string t)
        {
            List<int> indexS = new List<int>();
            List<int> indexT = new List<int>();

            for(int i = 0; i < s.Length; i++)
            {
                indexS.Add(s.IndexOf(s[i]));
            }

            for(int j = 0; j < t.Length; j++)
            {
                indexT.Add(t.IndexOf(t[j]));
            }

            return indexS.SequenceEqual(indexT);

        }


        /// <summary>
        /// https://leetcode.cn/problems/isomorphic-strings/solutions/537648/shi-yong-cde-dictionarygou-zao-zi-fu-zhi-ot8u/
        /// 參考別人
        /// 
        /// 我自己的有點問題
        /// 我是想統計s 與t 個出現幾種字母以及該字母出現次數
        /// 本來打算用Dictionary做
        /// 但是 想不出怎麼做會更有效率
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsIsomorphic2(string s, string t)
        {
            Dictionary<char, char> pairs = new Dictionary<char, char>();
            for (int i = 0; i < s.Length; i++)
            {
                if (!pairs.ContainsKey(s[i]))
                {
                    if (pairs.ContainsValue(t[i]))
                    {
                        return false;
                    }
                    else
                    {
                        pairs.Add(s[i], t[i]);
                    }
                }
                else
                {
                    string _a = t[i].ToString();
                    string _c = s[i].ToString();
                    string _b = pairs[s[i]].ToString();

                    if (t[i] != pairs[s[i]])
                    {
                        return false;
                    }
                }
            }

            return true;

        }


    }
}
