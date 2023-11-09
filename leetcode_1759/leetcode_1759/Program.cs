using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1759
{
    internal class Program
    {
        /// <summary>
        /// 1759. Count Number of Homogenous Substrings
        /// https://leetcode.com/problems/count-number-of-homogenous-substrings/?envType=daily-question&envId=2023-11-09
        /// 1759. 统计同质子字符串的数目
        /// https://leetcode.cn/problems/count-number-of-homogenous-substrings/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "bb";
            Console.WriteLine(CountHomogenous(s));
            Console.ReadKey();
        }


        /// <summary>
        /// 官方:
        /// https://leetcode.cn/problems/count-number-of-homogenous-substrings/solutions/2031869/tong-ji-tong-gou-zi-zi-fu-chuan-de-shu-m-tw5m/
        /// 
        /// 應該會有更好的方好
        /// 感覺應該可以用 二分法 or滑動視窗　之類概念
        /// 之後嘗試看看
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int CountHomogenous(string s)
        {
            // 題目要求, 最後要取 mod
            const int Mod = 1000000007;
            long res = 0;
            char prev = s[0];
            int cnt = 0;

            foreach (char c in s) 
            {
                if(c == prev)
                {
                    // 字符串 相同,就累加計數
                    cnt++;
                }
                else
                {
                    // 當連續字符串 中斷就計算該長度組合, 這邊是計算前一個 c == prev 的字符
                    res += (long)(cnt + 1) * cnt / 2;

                    // 新的字符串開始; 從頭開始 cnt 從1 開始
                    cnt = 1;
                    // prev 設為新的 字符
                    prev = c;
                }

            }

            // 遇到連續相同 直到s結束都沒中斷. 就計算長度組合
            res += (long)(cnt + 1) * cnt / 2;

            return (int)(res % Mod);

        }

    }
}
