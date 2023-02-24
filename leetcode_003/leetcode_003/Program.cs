using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_003
{
    internal class Program
    {
        /// <summary>
        /// leetcode 003
        /// https://leetcode.com/problems/longest-substring-without-repeating-characters/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s =  "abc";

            Console.WriteLine(LengthOfLongestSubstring(s));
            Console.ReadKey();
        }


        /// <summary>
        /// 这道题关键在于利用 BitArray来记录字母是否首次出现。时间复杂度O(n)
        /// https://blog.csdn.net/qq_39643935/article/details/78169424
        /// 
        /// 
        /// https://leetcode.cn/problems/longest-substring-without-repeating-characters/solution/wu-zhong-fu-zi-fu-de-zui-chang-zi-chuan-by-leetc-2/
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int LengthOfLongestSubstring(string s)
        {
            int max = 0;
            BitArray map = new BitArray(256, false);
            int l = 0, r = 0;
            int n = s.Length;

            while(r < n)
            {
                if (map[s[r]])
                {
                    max = Math.Max(max, r - l);
                    while (s[l] != s[r])
                    {
                        map[s[l]] = false;
                        l++;
                    }
                    l++;
                    r++;
                }
                else
                {
                    map[s[r]] = true;
                    r++;
                }
            }

            max = Math.Max(max, r - l);
            return max;
        }


    }
}
