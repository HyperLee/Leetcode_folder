using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_345
{
    internal class Program
    {
        /// <summary>
        /// 345. Reverse Vowels of a String
        /// https://leetcode.com/problems/reverse-vowels-of-a-string/?envType=study-plan-v2&envId=leetcode-75
        /// 345. 反转字符串中的元音字母
        /// https://leetcode.cn/problems/reverse-vowels-of-a-string/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "hello";
            Console.WriteLine(ReverseVowels(s));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/reverse-vowels-of-a-string/solutions/944385/fan-zhuan-zi-fu-chuan-zhong-de-yuan-yin-2bmos/
        /// 
        /// 一開始直接指到 需要交換的位置, 或是邊界
        /// 
        /// 字串s 轉成char[] 取出每個字母來比對
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ReverseVowels(string s)
        {
            int n = s.Length;
            int i = 0, j = s.Length - 1;
            char[] arr = s.ToCharArray();

            while (i < j) 
            {
                // 直接跑到需要交換的位置, 或是邊界
                while(i < n && !IsVowel(arr[i]))
                {
                    i++;
                }
                // 直接跑到需要交換的位置, 或是邊界
                while (j > 0 && !IsVowel(arr[j]))
                {
                    j--;
                }

                if(i < j)
                {
                    swap(arr, i, j);
                    i++;
                    j--;
                }
            }

            return new string(arr);
        }

        /// <summary>
        /// 判斷是不是 母音
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool IsVowel(char ch)
        {
            return "aeiouAEIOU".IndexOf(ch) >= 0;
        }


        /// <summary>
        /// 母音交換
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public static void swap(char[] arr, int i, int j)
        {
            char tmp = arr[i];
            arr[i] = arr[j];
            arr[j] = tmp;
        }

    }
}
