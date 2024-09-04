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
        /// 3. Longest Substring Without Repeating Characters
        /// https://leetcode.com/problems/longest-substring-without-repeating-characters/
        /// 3. 无重复字符的最长子串
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
        /// 如果 list 集合中没有重覆出现字符，则 right 不断往右滑即 right++ 並將當前字符添加到集合中
        /// ，如果出现重覆字符則 left++ 缩小窗口。
        /// 直到滿足題目需求
        /// 
        /// 從左往右把字母加進去
        /// 如果遇到相同重複就把最左邊扣除,
        /// 右邊繼續往前進
        /// 直到跑完整個輸入字串為止
        /// 
        /// list 可以取代 HashSet
        /// 因加入時候就會判斷是否存在, 
        /// 已存在就會去除
        /// 故不使用 HashSet 也無礙
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int LengthOfLongestSubstring2(string s)
        {
            if(s.Length == 0)
            {
                return 0;
            }

            // 紀錄每個 char 是否存在
            List<char> letter = new List<char>();
            // 初始化左右指针，預設 0 開始
            int left = 0, right = 0;
            int length = s.Length;
            // count 紀錄每次移動後字串長度
            int count = 0, max = 0;

            while (right < length)
            {
                // 右指針 char 未重複
                if (!letter.Contains(s[right]))
                {
                    // 將該 char 加入 list
                    letter.Add(s[right]);
                    // 右指針右移
                    right++;
                    count++;
                }
                else
                {
                    // 右指針重複, 左指針開始右移.直到不含重複 char (即左指針移動到重複 char(左) 的右邊一位)
                    // 去除 list 中當前左指針 char
                    letter.Remove(s[left]);
                    // 左指針右移
                    left++;
                    count--;
                }
                max = Math.Max(max, count);
            }

            return max;
        }


    }
}
