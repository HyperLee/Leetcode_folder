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
        Program solution = new Program();
        (string Input, int Expected)[] testCases =
        [
            ("Hello World", 5),
            ("   fly me   to   the moon  ", 4),
            ("luffy is still joyboy", 6),
            ("a", 1),
            ("Today is a nice day   ", 3),
        ];

        foreach ((string input, int expected) in testCases)
        {
            int actual = solution.LengthOfLastWord(input);
            string result = actual == expected ? "PASS" : "FAIL";

            Console.WriteLine($"Input: \"{input}\" | Expected: {expected} | Actual: {actual} | {result}");
        }
    }

    /// <summary>
    /// 計算字串中最後一個單字的長度。解題概念是從字串尾端往前掃描，先略過尾端空格，
    /// 再統計最後一段連續非空格字元的長度。輸入需符合題目限制：字串長度至少為 1，
    /// 且至少包含一個由非空格字元組成的單字。輸出為最後一個單字的字元數。
    /// </summary>
    /// <param name="s">由英文字母與空格組成，且至少包含一個單字的字串。</param>
    /// <returns>最後一個單字的長度。</returns>
    public int LengthOfLastWord(string s)
    {
        int index = s.Length - 1;

        // 從尾端略過所有空格，定位到最後一個單字的結尾。
        while (s[index] == ' ')
        {
            index--;
        }

        int wordLength = 0;

        // 往左累計連續非空格字元，直到遇到空格或字串開頭。
        while (index >= 0 && s[index] != ' ')
        {
            wordLength++;
            index--;
        }
        return wordLength;
    }
}
