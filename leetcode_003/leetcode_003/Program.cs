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
        /// leetcode 003 Longest Substring Without Repeating Characters
        /// https://leetcode.com/problems/longest-substring-without-repeating-characters/
        /// 无重复字符的最长子串
        /// https://leetcode.cn/problems/longest-substring-without-repeating-characters/
        /// 滑動視窗 解題觀念
        /// 
        /// 類似題目:
        /// leetcode 3, 30, 76, 159, 209, 239, 567, 632, 727
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "abcabcbb";

            Console.WriteLine(LengthOfLongestSubstring2(s));
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
                // 是否已存在
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
                    // 不存在
                    map[s[r]] = true;
                    r++;
                }
            }

            max = Math.Max(max, r - l);
            return max;
        }


        /// <summary>
        /// 滑動視窗, 左右指針概念
        /// 
        /// https://leetcode.cn/problems/longest-substring-without-repeating-characters/solution/hua-dong-chuang-kou-cban-by-seerjjj/
        /// https://leetcode.cn/problems/longest-substring-without-repeating-characters/solution/wu-zhong-fu-zi-fu-de-zui-chang-zi-chuan-rqmpw/
        /// https://leetcode.cn/problems/longest-substring-without-repeating-characters/solution/hua-dong-chuang-kou-by-powcai/
        /// 
        /// 滑動視窗 解題觀念:
        /// 如果list集合中没有重覆出现字符，则right不断往右滑即right++并將當前字符添加到集合中
        /// ，如果出现重覆字符則left++缩小窗口。
        /// 直到滿足題目需求
        /// 
        /// 從左往右把字母加進去
        /// 如果遇到相同重複就把最左邊扣除,
        /// 右邊繼續往前進
        /// 直到跑完整個輸入字串為止
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int LengthOfLongestSubstring2(string s)
        {
            if(s.Length == 0)
            {
                return 0;
            }

            //HashSet<char> letter = new HashSet<char>();// 哈希集合，记录每个字符是否出现过
            List<char> letter = new List<char>();
            int left = 0, right = 0;//初始化左右指针，指向字符串首尾字符
            int length = s.Length;
            int count = 0, max = 0;//count记录每次指针移动后的子串长度

            while (right < length)
            {
                if (!letter.Contains(s[right]))//右指针字符未重覆
                {
                    letter.Add(s[right]);//将该字符添加进集合
                    right++;//右指针继续右移
                    count++;
                }
                else//右指针字符重覆，左指针开始右移，直到不含重覆字符（即左指针移动到重覆字符(左)的右边一位）
                {
                    letter.Remove(s[left]);//去除集合中当前左指针字符
                    left++;//左指针右移
                    count--;
                }
                max = Math.Max(max, count);
            }

            return max;
        }


    }
}
