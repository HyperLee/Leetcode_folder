using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_005
{
    internal class Program
    {
        /// <summary>
        /// leetcode 005
        /// https://leetcode.com/problems/longest-palindromic-substring/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "babad";
            Console.WriteLine(LongestPalindrome(s));
            Console.ReadKey();
        }


        /// <summary>
        /// https://blog.csdn.net/wf824284257/article/details/104548244
        /// 解法一: 暴力法
        /// 
        /// 使用 3层循环 来依次对所有子串进行检查，将最长的子串最为最终结果返回。
        /// 下面代码中，我们检查i到j的子串是否是回文串，如果是 且长度大于当前结果result的长度，
        /// 就将result更新为i到j的子串。
        /// 执行结果
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string LongestPalindrome(string s)
        {
            string result = "";
            int n = s.Length;

            for(int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    //檢查s[i]到s[j]是否是回文, 如果是且長度大於result長度就更新他
                    int p = i, q = j;
                    bool isPalindromic = true;
                    while(p < q)
                    {
                        if (s[p++] != s[q--])
                        {
                            isPalindromic = false; 
                            break;
                        }
                    }

                    if(isPalindromic)
                    {
                        int len = j - i + 1;
                        if(len > result.Length)
                        {
                            result = s.Substring(i, len);
                        }
                    }
                }
            }
            return result;
        }


        /// <summary>
        /// https://www.freesion.com/article/2998935289/
        /// 思路在于遍历找到各个可以作为中点的单个字母或两个相同的字母，
        /// 然后利用ExpendCenter方法来获取最长的结果并和最终结果比较，取最长。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        //public static string LongestPalindrome2(string s)
        //{
        //    string result = "";
        //    int n = s.Length;
        //    int end = 2 * n - 1;
        //    for(int i = 0; i < end; i++)
        //    {
        //        double mid = i / 2.0;

        //    }
        //}



    }
}
