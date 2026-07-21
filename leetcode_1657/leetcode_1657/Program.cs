namespace leetcode_1657;

class Program
{
    /// <summary>
    /// 1657. Determine if Two Strings Are Close
    /// https://leetcode.com/problems/determine-if-two-strings-are-close/description/
    /// 1657. 判定兩個字串是否接近
    /// https://leetcode.cn/problems/determine-if-two-strings-are-close/description/
    ///
    /// English:
    /// Two strings are considered close if you can attain one from the other using the following operations:
    ///
    /// Operation 1: Swap any two existing characters.
    /// For example, abcde -> aecdb
    ///
    /// Operation 2: Transform every occurrence of one existing character into another existing character,
    /// and do the same with the other character.
    /// For example, aacabb -> bbcbaa (all a's turn into b's, and all b's turn into a's)
    ///
    /// You can use the operations on either string as many times as necessary.
    ///
    /// Given two strings, word1 and word2, return true if word1 and word2 are close, and false otherwise.
    ///
    /// 繁體中文：
    /// 如果可以使用下列操作，將一個字串變成另一個字串，則認為這兩個字串是接近的：
    ///
    /// 操作 1：交換任意兩個既有字元。
    /// 例如：abcde -> aecdb
    ///
    /// 操作 2：將某個既有字元的每一次出現全部轉換成另一個既有字元，
    /// 同時也將另一個字元的每一次出現全部轉換成前一個字元。
    /// 例如：aacabb -> bbcbaa（所有 a 變成 b，所有 b 變成 a）
    ///
    /// 你可以視需要，對任一字串使用任意次數的上述操作。
    ///
    /// 給定兩個字串 word1 和 word2，如果 word1 和 word2 是接近的，回傳 true；否則回傳 false。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
