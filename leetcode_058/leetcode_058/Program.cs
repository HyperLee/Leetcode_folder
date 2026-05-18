namespace leetcode_058;

class Program
{
    /// <summary>
    /// 58. Length of Last Word
    /// https://leetcode.com/problems/length-of-last-word/description/
    /// 58. 最後一個單字的長度
    /// https://leetcode.cn/problems/length-of-last-word/description/
    /// 
    /// English:
    /// Given a string s consisting of words and spaces, return the length of the last word in the string.
    /// A word is a maximal substring consisting of non-space characters only.
    ///
    /// 繁體中文:
    /// 給定一個由單字與空格組成的字串 s，回傳字串中最後一個單字的長度。
    /// 單字是只由非空格字元組成的最大子字串。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public int LengthOfLastWord(string s)
    {
        int index = s.Length - 1;
        // 字串最後空白部分先過濾, 只留下結尾部分是純文字結尾
        while(s[index] == ' ')
        {
            index--;
        }

        int wordLength = 0;
        // 反向(右至左)遍歷字串 s, 且不為空
        while(index >= 0 && s[index] != ' ')
        {
            wordLength++;
            index--;
        }
        return wordLength;
    }
}
