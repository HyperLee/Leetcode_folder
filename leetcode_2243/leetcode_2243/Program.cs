using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2243
{
    internal class Program
    {
        /// <summary>
        /// leetcode 2243 Calculate Digit Sum of a String
        /// https://leetcode.com/problems/calculate-digit-sum-of-a-string/
        /// 2243. 计算字符串的数字和
        /// https://leetcode.cn/problems/calculate-digit-sum-of-a-string/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "11111222223";
            int k = 3;

            Console.WriteLine(DigitSum(s, k));
            Console.ReadKey();


        }


        /// <summary>
        /// https://leetcode.com/problems/calculate-digit-sum-of-a-string/solutions/2935351/iterate-and-sum/
        /// </summary>
        /// <param name="s"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static string DigitSum(string s, int k)
        {
            while(s.Length > k)
            {
                StringBuilder sb = new StringBuilder();
                int j = k;
                int ans = 0;

                // 步驟一: 每k個為一輪 加總
                for(int i = 0; i < s.Length; i++)
                {
                    ans += Convert.ToInt32(s[i].ToString());

                    if(i == j - 1)
                    {
                        sb.Append(ans.ToString());
                        ans = 0;
                        j += k;
                    }
                }

                // 步驟二: 每k個一輪之後 剩餘不足k個數量 做加總
                if(j > s.Length && (s.Length % k != 0))
                {
                    sb.Append(ans.ToString());
                    ans = 0;
                    j += k;
                }

                // 統計上述兩步驟之後 在繼續下一輪
                // 只要 長度 > k 就要一直做
                s = sb.ToString();
            }

            return s;
        }


    }
}
