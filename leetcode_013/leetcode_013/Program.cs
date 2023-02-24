using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_013
{
    class Program
    {
        /// <summary>
        /// https://www.itread01.com/content/1543415356.html
        /// leetcode_013
        /// Roman to Integer
        /// https://leetcode.com/problems/roman-to-integer/ 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string str = "IV";
            int res = RomanToInt(str);
            Console.WriteLine(res);
            Console.ReadKey();

        }


        /// <summary>
        /// https://www.796t.com/content/1543415356.html
        /// 
        /// 首先，分別將單個羅馬數和其所對應的整數存入字典中。
        /// 其次，對於輸入的羅馬數，將其看作字串。設定目前數為0，開始遍歷，根據規律，
        /// 從第一個字元到倒數第二個字元，每個字元在字典中的值與後一個字元比較
        /// ，若前者大於後者，說明是類似於IV一樣的，需要用目前的數減
        /// 去這個值。否則，用目前的數加上這個值。若迴圈到最後一個字元
        /// ，則其在字典中的值直接相加，直到迴圈結束。
        /// 最後，返回結果。
        /// 時間複雜度：O(n)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int RomanToInt(string str)
        {
            int res = 0;
            Dictionary<char, int> dic = new Dictionary<char, int> { { 'I', 1 }, { 'V', 5 }, { 'X', 10 }, { 'L', 50 }, { 'C', 100 }, { 'D', 500 }, { 'M', 1000 } };
            for (int i = 0; i < str.Length; ++i)
            {
                int val = dic[str[i]];
                if (i == str.Length - 1 || dic[str[i + 1]] <= dic[str[i]])
                {
                    res += val;
                }
                else
                {
                    //若前者大於後者，說明是類似於IV一樣的，需要用目前的數減
                    //去這個值
                    res -= val;
                }
            }
            return res;
        }
    }
}
