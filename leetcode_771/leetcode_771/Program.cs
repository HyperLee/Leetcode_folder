using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_771
{
    class Program
    {
        /// <summary>
        /// https://leetcode-cn.com/problems/jewels-and-stones/solution/bao-shi-yu-shi-tou-by-leetcode-solution/
        /// https://leetcode.com/problems/jewels-and-stones/
        /// 
        /// leetcode 771 Jewels and Stones
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string j = "";
            j = "aA";
            string s = "";
            s = "aAAbbbb";

            Console.WriteLine(NumJewelsInStones(j, s));
            Console.ReadKey();
        }


        /// <summary>
        /// https://ithelp.ithome.com.tw/articles/10220065
        /// 
        /// J 含有key word 字串, 每個字母都為獨立. 
        /// S 完整要查詢的字串
        /// 
        /// 查找j在s裡面有幾個 key word, 大小寫視為不同
        /// </summary>
        /// <param name="J"></param>
        /// <param name="S"></param>
        /// <returns></returns>
        public static int NumJewelsInStones(string J, string S)
        {
            int cnt = 0;
            for (int i = 0; i < S.Length; i++)
            {
                // method 1
                //if (J.IndexOf(S[i]) > -1)
                //{
                //    cnt++;
                //}

                // method 2
                if (J.Contains(S[i]))
                {
                    cnt++;
                }

            }
            return cnt;
        }


    }
}
