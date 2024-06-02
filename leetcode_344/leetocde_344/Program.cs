using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetocde_344
{
    class Program
    {
        /// <summary>
        /// 344. Reverse String
        /// https://leetcode.com/problems/reverse-string/?envType=daily-question&envId=2024-06-02
        /// 344. 反转字符串
        /// https://leetcode.cn/problems/reverse-string/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            char[] arr = new char[6];
            arr[0] = 'h';
            arr[1] = 'a';
            arr[2] = 'n';
            arr[3] = 'k';
            arr[4] = 's';
            arr[5] = '0';

            //Console.WriteLine(ReverseString(arr));
            ReverseString(arr);

            Console.ReadKey();

        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/reverse-string/solutions/439034/fan-zhuan-zi-fu-chuan-by-leetcode-solution/
        /// https://leetcode.cn/problems/reverse-string/solutions/2376290/ji-chong-bu-tong-de-xie-fa-pythonjavacgo-9trb/
        /// https://leetcode.cn/problems/reverse-string/solutions/1765738/by-stormsunshine-n3w4/
        /// 
        /// 雙指針方式
        /// left < right 時候停止 迴圈
        /// 
        /// </summary>
        /// <param name="s"></param>
        public static void ReverseString(char[] s)
        {
            int n = s.Length;
            for (int left = 0, right = n - 1; left < right; left++, right--)
            {
                char tmp = s[left];
                s[left] = s[right];
                s[right] = tmp;

                //Console.WriteLine(s);
            }
            Console.WriteLine(s);

        }
    }
}
