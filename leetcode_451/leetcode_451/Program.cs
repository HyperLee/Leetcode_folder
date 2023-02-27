using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_451
{
    class Program
    {
        /// <summary>
        /// leetcode_451. Sort Characters By Frequency
        /// 根据字符出现频率排序
        /// https://leetcode.com/problems/sort-characters-by-frequency/
        /// https://leetcode-cn.com/problems/sort-characters-by-frequency/
        /// 
        /// leetcode 451
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "";
            s = "abb";

            Console.Write(FrequencySort(s));
            Console.ReadKey();
        }


        /// <summary>
        /// https://ithelp.ithome.com.tw/articles/10220133
        /// 利用Dictionary計算次數
        /// 排序 由大至小   題目要求
        /// 根據排序結果 顯示
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FrequencySort(string s)
        {
            Dictionary<char, int> map = new Dictionary<char, int>();
            char[] cs = s.ToCharArray();

            for (int i = 0; i < cs.Length; i++)
            {
                if (map.ContainsKey(cs[i]))
                {
                    // 以下兩種寫法都可以,一樣意思
                    //map[cs[i]] = map[cs[i]] + 1;
                    map[cs[i]]++;
                }
                else
                {
                    map.Add(cs[i], 1);
                }
            }

            // 降冪排序
            var sort = map.OrderByDescending(x => x.Value);
            StringBuilder result = new StringBuilder();

            // 外層是key
            foreach (var item in sort)
            {
                // 取每個key對應value次數
                for (int i = 0; i < item.Value; i++)
                {
                    // 把key 塞到 result裡面
                    result = result.Append(item.Key);
                }
            }

            return result.ToString();
        }


    }
}
