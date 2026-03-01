using System.Text;

namespace leetcode_804;

class Program
{
    /// <summary>
    /// 804. Unique Morse Code Words
    /// https://leetcode.com/problems/unique-morse-code-words/description/
    /// 804. 唯一摩尔斯密码词
    /// https://leetcode.cn/problems/unique-morse-code-words/description/
    ///
    /// Problem Description (English):
    /// International Morse Code defines a standard encoding where each letter is mapped to a series of dots and dashes.
    /// For convenience, the full table for the 26 letters of the English alphabet is given below:
    /// [".-","-...","-.-.","-..",".","..-.","--.","....","..",".---","-.-",".-..","--","-.","---",".--.","--.-",".-.","...","-","..-","...-",".--","-..-","-.--","--.."]
    /// Given an array of strings words where each word can be written as a concatenation of the Morse code of each letter.
    /// For example, "cab" can be written as "-.-..--...", which is the concatenation of "-.-.", ".-", and "-...".
    /// We will call such a concatenation the transformation of a word.
    /// Return the number of different transformations among all words we have.
    ///
    /// 題目描述 (繁體中文)：
    /// 國際摩爾斯密碼定義了一套標準編碼，其中每個字母對應到一系列的點和線。
    /// 為了方便，以下給出了 26 個英文字母的完整對照表：
    /// [".-","-...","-.-.","-..",".","..-.","--.","....","..",".---","-.-",".-..","--","-.","---",".--.","--.-",".-.","...","-","..-","...-",".--","-..-","-.--","--.."]
    /// 給定一個字串數組 words，每個單詞都可以寫成對應字母摩爾斯密碼的連接。
    /// 例如，"cab" 可以寫成 "-.-..--..."，即 "-.-."、".-" 和 "-..." 的連接。
    /// 我們稱這樣的連接為單詞的轉換形式。
    /// 請返回我們擁有的所有單詞中，不同轉換形式的數量。
    ///
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試案例 1：LeetCode 官方範例
        // 預期輸出：2
        // "gin"  -> "--....-."  
        // "zen"  -> "--....-."  (與 gin 相同)
        // "tit"  -> "-......-"
        // "maps" -> "--...-..."
        // 共有 2 種不同的轉換形式
        string[] words1 = ["gin", "zen", "tit", "maps"];
        int result1 = solution.UniqueMorseRepresentations(words1);
        Console.WriteLine($"測試案例 1：{result1}"); // 預期：2

        // 測試案例 2：只有一個單詞
        // "a" -> ".-"，共 1 種
        string[] words2 = ["a"];
        int result2 = solution.UniqueMorseRepresentations(words2);
        Console.WriteLine($"測試案例 2：{result2}"); // 預期：1

        // 測試案例 3：所有單詞摩爾斯碼不同
        // "abc" -> ".--.-."，"xyz" -> "-..--.--.."，共 2 種
        string[] words3 = ["abc", "xyz"];
        int result3 = solution.UniqueMorseRepresentations(words3);
        Console.WriteLine($"測試案例 3：{result3}"); // 預期：2
    }

    public static string[] MORSE = {".-", "-...", "-.-.", "-..", ".", "..-.", "--.",
                                    "....", "..", ".---", "-.-", ".-..", "--", "-.",
                                    "---", ".--.", "--.-", ".-.", "...", "-", "..-",
                                    "...-", ".--", "-..-", "-.--", "--.."};

    /// <summary>
    /// 解題思路：使用 HashSet 去除重複摩爾斯密碼字串
    ///
    /// 核心概念：
    ///   將每個單詞的每個字母依照 MORSE 對照表逐一拼接，
    ///   形成該單詞完整的摩爾斯密碼字串（稱為「轉換形式」）。
    ///   再利用 HashSet 的唯一性特性——插入重複值時會自動忽略——
    ///   收集所有不重複的轉換形式，最終回傳集合的元素個數。
    ///
    /// 出發點：
    ///   不同單詞可能產生相同的摩爾斯密碼（例如 "gin" 與 "zen" 都是 "--....-."），
    ///   因此直接對字串做去重即可，無需排序或複雜比較。
    ///
    /// 時間複雜度：O(S)，S 為所有單詞字元總數；空間複雜度：O(S)。
    ///
    /// HashSet 特性補充：
    ///   - 元素唯一：插入重複元素時靜默忽略，不拋出例外。
    ///   - 支援存放單一 null 值。
    ///   - 內部以雜湊結構實作，查詢與插入平均 O(1)，效能極佳。
    ///
    /// <example>
    /// <code>
    /// var sol = new Program();
    /// "gin"->"--....-." , "zen"->"--....-." , "tit"->"-....." , "maps"->"--...-..."
    /// int result = sol.UniqueMorseRepresentations(["gin", "zen", "tit", "maps"]); // 2
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="words">由小寫英文字母組成的單詞陣列。</param>
    /// <returns>所有單詞中不同摩爾斯密碼轉換形式的數量。</returns>
    public int UniqueMorseRepresentations(string[] words)
    {
        // 使用 HashSet 自動去除重複的摩爾斯密碼轉換形式
        ISet<string> seen = new HashSet<string>();

        foreach (var word in words)
        {
            // 逐字母查表，拼接出整個單詞的摩爾斯密碼字串
            StringBuilder sb = new StringBuilder();
            foreach (var c in word)
            {
                // c - 'a' 利用 ASCII 差值取得 MORSE 陣列的對應索引（'a'=0, 'b'=1, ...）
                sb.Append(MORSE[c - 'a']);
            }

            // 加入集合；若已存在相同字串，HashSet 會自動忽略，確保唯一性
            seen.Add(sb.ToString());
        }

        // 集合元素個數即為不同轉換形式的數量
        return seen.Count;
    }
}
