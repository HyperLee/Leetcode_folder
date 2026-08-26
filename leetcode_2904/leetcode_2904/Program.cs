using System.Runtime.InteropServices.Marshalling;

namespace leetcode_2904;

class Program
{
    /// <summary>
    /// English:
    ///
    /// 2904. Shortest and Lexicographically Smallest Beautiful String
    /// https://leetcode.com/problems/shortest-and-lexicographically-smallest-beautiful-string/description
    ///
    /// Given a binary string s and a positive integer k.
    ///
    /// A substring of s is beautiful if the number of 1's in it is exactly k.
    ///
    /// Let len be the length of the shortest beautiful substring.
    ///
    /// Return the lexicographically smallest beautiful substring of string s with length equal to len. If s doesn't contain a beautiful substring, return an empty string.
    ///
    /// A string a is lexicographically larger than a string b (of the same length) if in the first position where they differ, a has a character strictly larger than the corresponding character in b.
    ///
    /// For example, "abcd" is lexicographically larger than "abcc" because the first position they differ is at the fourth character, and d is greater than c.
    ///
    /// Traditional Chinese（繁體中文）：
    ///
    /// 2904. 最短且字典序最小的美丽子字符串
    /// https://leetcode.cn/problems/shortest-and-lexicographically-smallest-beautiful-string/description
    ///
    /// 給定一個二進位字串 s 和一個正整數 k。
    ///
    /// 如果子字串中 1 的數量恰好等於 k，則稱該子字串為美麗子字串。
    ///
    /// 令 len 為最短美麗子字串的長度。
    ///
    /// 請回傳 s 中長度等於 len 的字典序最小美麗子字串。如果 s 中不存在美麗子字串，請回傳空字串。
    ///
    /// 若兩個長度相同的字串 a 和 b 在第一個不同的位置上，a 對應字元的字典序嚴格大於 b 對應字元，則稱 a 的字典序大於 b。
    ///
    /// 例如，因為兩個字串第一個不同的位置在第四個字元，且 d 大於 c，所以 "abcd" 的字典序大於 "abcc"。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 解法一:枚舉
    /// 题目要求我们在二进制字符串 s 中找到包含 k 个 1 的最短且字典序最小的字符串。
    /// 假设 s 的长度为 n。注意到题目给定的字符串长度范围较小，在 102 内，所以我们可以用 O(n3) 时间复杂度的算法来解决这个问题。
    /// 假设最短字符串的长度为 m，我们在 s 中枚举所有长度为 m 的子字符串，判断其中是否有 k 个 1，并返回字典序最小的字符串。m 的范围为 [k,n]。
    /// </summary>
    /// <param name="s"></param>
    /// <param name="k"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public string ShortestBeautifulSubstring(string s, int k)
    {
        for (int m = k; m <= s.Length; m++) 
        {
            string ans = "";
            for (int i = m; i <= s.Length; i++) 
            {
                string t = s.Substring(i - m, m);
                if ((ans.Length == 0 || string.CompareOrdinal(t, ans) < 0) && t.Count(c => c == '1') == k) 
                {
                    ans = t;
                }
            }
            if (ans.Length > 0) 
            {
                return ans;
            }
        }
        return "";
    }

    /// <summary>
    /// 解法二:滑動視窗
    /// 我们可以维护一个滑动窗口，当窗口中的 1 数量大于 k 或窗口端点处的字符是 0，就可以缩小窗口，从而找到最短的子字符串。
    /// </summary>
    /// <param name="s"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public string ShortestBeautifulSubstring2(string s, int k)
    {
        if(s.Count(c => c == '1') < k) return "";

        string ans = s;
        int cnt = 0;
        int left = 0;
        for(int right = 0; right < s.Length; right++)
        {
            cnt += s[right] - '0';
            while(cnt > k || s[left] == '0')
            {
                cnt -= s[left++] - '0';
            }

            if(cnt == k)
            {
                string t = s.Substring(left, right - left + 1);
                if(t.Length < ans.Length || t.Length == ans.Length && string.CompareOrdinal(t, ans) < 0)
                {
                    ans = t;
                }
            }
        }
        return ans;
    }
}
