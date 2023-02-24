using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetocde_344
{
    class Program
    {
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
        /// https://leetcode.com/problems/reverse-string/submissions/
        /// leetcode _344
        /// 
        /// https://leetcode-cn.com/problems/reverse-string/solution/fan-zhuan-zi-fu-chuan-by-leetcode-solution/
        /// 官方解法
        /// </summary>
        /// <param name="s"></param>
        public static void ReverseString(char[] s)
        {
            int n = s.Length;
            for (int left = 0, right = n - 1; left < right; ++left, --right)
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
