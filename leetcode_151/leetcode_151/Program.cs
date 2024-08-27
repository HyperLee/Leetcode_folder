using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace leetcode_151
{
    class Program
    {
        /// <summary>
        /// 151. Reverse Words in a String
        /// https://leetcode.com/problems/reverse-words-in-a-string/
        /// 151. 反转字符串中的单词
        /// https://leetcode.cn/problems/reverse-words-in-a-string/
        /// 
        /// 要去除連續空白
        /// 單字與單字間 要空白隔開
        /// 反向排序
        /// 
        /// 本題目反轉是 輸入的字串 "順序"
        /// 不是 字串 "文字" 反轉
        /// 需要注意
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string a = " the sky is blue";
            Console.WriteLine(ReverseWords(a));
            Console.ReadKey();
        }

        /// <summary>
        /// https://leetcode.com/problems/reverse-words-in-a-string/discuss/737908/C-solutions
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string reverseWords1(string s)
        {
            s = s.Trim();
            String[] split = s.Split();
            // 正規畫 去除 兩個以上空白
            Regex replaceSpace = new Regex(@"\s{2,}", RegexOptions.IgnoreCase);

            s = replaceSpace.Replace(s, "").Trim();

            StringBuilder sb = new StringBuilder();
            //從後面取
            for (int i = split.Length - 1; i >= 0; i--)
            {
                if (sb.Length > 0)
                {
                    //if(i)
                    //前面正則已經把空白去掉了,這邊依照題目要求補上
                    sb.Append(" ");
                }
                sb.Append(split[i]);
            }

            string aa = "";
            aa = sb.ToString();

            aa = replaceSpace.Replace(aa, " ").Trim();

            return aa;
        }

        /// <summary>
        /// 大致上解法概念
        /// 1. 使用 StringBuilder 儲存結果
        /// 2. 去除空白
        /// 3. 計算字串長度
        /// 4. 將字串加入 sb 裡面
        /// 4.1 sb 為空就直接 Append
        /// 4.2 sb 不為空就用插入的方式加入字串,
        ///     插入位置為 0, 原先已加入者會往後
        ///     最後要加上一個空白, 區隔字串
        /// 5. 輸出結果 字串與字串之間要一個空白區隔, 與輸入順序反轉    
        /// 
        /// 
        /// StringBuilder Insert(int index, string value)
        /// index：指定要插入字串或數據的位置。
        /// value：要插入的字串或對象。
        /// 
        /// ' ' => char
        /// " " => string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ReverseWords(string s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ')
                {
                    // 去除空白
                    continue;
                }

                // 字串起始位置
                int start = i;
                while (i < s.Length && s[i] != ' ')
                {
                    // 字串結束位置; 計算該字串長度
                    i++;
                }

                // 加入字串: 結束位置 - 起始位置
                if (sb.Length == 0)
                {
                    // sb 為空, 就加入
                    sb.Append(s.Substring(start, i - start));
                }
                else
                {
                    // 新字串插入位置 0, 原先已加入者會往後, 最後加上一個空白區隔
                    sb.Insert(0, s.Substring(start, i - start) + ' ');
                }
            }

            return sb.ToString();
        }

    }
}
