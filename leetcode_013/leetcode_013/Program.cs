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
        /// 13. Roman to Integer
        /// https://leetcode.com/problems/roman-to-integer/ 
        /// 13. 罗马数字转整数
        /// https://leetcode.cn/problems/roman-to-integer/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string str = "IX";
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
        /// ，若前者小於後者，說明是類似於IV一樣的，需要用目前的數減去這個值。
        /// 否則，用目前的數加上這個值。
        /// 若迴圈到最後一個字元，則其在字典中的值直接相加，直到迴圈結束。
        /// 最後，返回結果。
        /// 時間複雜度：O(n)
        /// 
        /// 要小心羅馬數字有前後問題
        /// 如下:
        /// IV: 4  => -=
        ///  V: 5
        /// VI: 6  => +=
        /// 並非像是阿拉伯數字一致性讀取即可, 會出錯
        /// 
        /// 羅馬數字前後判讀方式:
        /// 前面 < 後面 => 減法
        /// 前面 > 後面 => 加法
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int RomanToInt(string str)
        {
            int res = 0;
            // 儲存 羅馬字 總共七種符號
            Dictionary<char, int> dic = new Dictionary<char, int> { { 'I', 1 }, { 'V', 5 }, { 'X', 10 }, { 'L', 50 }, { 'C', 100 }, { 'D', 500 }, { 'M', 1000 } };

            for (int i = 0; i < str.Length; i++)
            {
                int val = dic[str[i]];
                // 每個字元在字典中的值與後一個字元比較
                if (i == str.Length - 1 || dic[str[i + 1]] <= dic[str[i]])
                {
                    // 前面比後面大
                    res += val;
                }
                else
                {
                    // 後面比前面大，說明是類似於IV一樣的，需要用目前的數減去這個值
                    res -= val;
                }
            }
            return res;
        }
    }
}
