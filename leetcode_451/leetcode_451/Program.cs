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
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FrequencySort(string s)
        {
            var map = new Dictionary<char, int>();
            var cs = s.ToCharArray();
            for (int i = 0; i < cs.Length; i++)
            {
                if (map.ContainsKey(cs[i]))
                {
                    map[cs[i]] = map[cs[i]] + 1;
                }
                else
                {
                    map.Add(cs[i], 1);
                }
            }
            var sort = map.OrderByDescending(x => x.Value);
            StringBuilder result = new StringBuilder();
            foreach (var item in sort)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    result = result.Append(item.Key);
                }
            }
            return result.ToString();
        }


    }
}
