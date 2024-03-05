using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1750
{
    internal class Program
    {
        /// <summary>
        /// 1750. Minimum Length of String After Deleting Similar Ends
        /// https://leetcode.com/problems/minimum-length-of-string-after-deleting-similar-ends/description/?envType=daily-question&envId=2024-03-05
        /// 1750. 删除字符串两端相同字符后的最短长度
        /// https://leetcode.cn/problems/minimum-length-of-string-after-deleting-similar-ends/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string input = "aabccabba";
            Console.WriteLine(MinimumLength(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 雙指針
        /// https://leetcode.cn/problems/minimum-length-of-string-after-deleting-similar-ends/solutions/2033813/shan-chu-zi-fu-chuan-liang-duan-xiang-to-biep/
        /// 
        /// 這題型一看直覺就是雙指針
        /// 指向一左一右
        /// 往內靠攏
        /// 計算出長度
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int MinimumLength(string s)
        {
            int n = s.Length;
            int left = 0;
            int right = n - 1;

            while(left < right && s[left] == s[right])
            {
                char _char = s[left];

                while(left <= right && s[left] == _char)
                {
                    // char相同就往右縮小範圍
                    left++;
                }

                while(left <= right && s[right] == _char)
                {
                    // char相同就往左縮小範圍
                    right--;
                }
            }

            return right - left + 1;
        }

    }
}
