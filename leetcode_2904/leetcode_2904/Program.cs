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
}
