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
        Console.WriteLine("Hello, World!");
    }

    public static string[] MORSE = {".-", "-...", "-.-.", "-..", ".", "..-.", "--.",
                                    "....", "..", ".---", "-.-", ".-..", "--", "-.",
                                    "---", ".--.", "--.-", ".-.", "...", "-", "..-",
                                    "...-", ".--", "-..-", "-.--", "--.."};

    /// <summary>
    /// 我们将数组 words 中的每个单词按照莫尔斯密码表转换为摩尔斯码
    /// ，并加入哈希集合中，最终的答案即为哈希集合中元素的个数。
    /// 
    /// 
    /// HashSet中的元素唯一性
    /// 如果你向 HashSet 中插入重复的元素，它的内部会忽视这次操作而不像别的集合一样抛出异常
    /// 
    /// HashSet 只能包含唯一的元素，它的内部结构也为此做了专门的优化，值得注意的是，HashSet 也可以
    /// 存放单个的 null 值，可以得出这么一个结论：如何你想拥有一个具有唯一值的集合，那么 HashSet 就是
    /// 你最好的选择，何况它还具有超高的检索性能。
    /// 
    /// 透過hash儲存 不重複資料 最後計算count即可
    /// </summary>
    /// <param name="words"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="words"></param>
    /// <returns></returns>
    public int UniqueMorseRepresentations(string[] words)
    {
        ISet<string> seen = new HashSet<string>();
        foreach(var word in words)
        {
            StringBuilder sb = new StringBuilder();
            foreach(var c in word)
            {
                sb.Append(MORSE[c - 'a']);
            }
            seen.Add(sb.ToString());
        }
        return seen.Count;
    }
}
