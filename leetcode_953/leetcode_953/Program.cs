namespace leetcode_953;

class Program
{
    /// <summary>
    /// 953. Verifying an Alien Dictionary
    /// https://leetcode.com/problems/verifying-an-alien-dictionary/description/
    /// 953. 驗證外星語詞典
    /// https://leetcode.cn/problems/verifying-an-alien-dictionary/description/
    ///
    /// In an alien language, surprisingly, they also use English lowercase letters,
    /// but possibly in a different order. The order of the alphabet is some permutation of lowercase letters.
    /// Given a sequence of words written in the alien language, and the order of the alphabet,
    /// return true if and only if the given words are sorted lexicographically in this alien language.
    ///
    /// 在外星語言中，令人驚訝的是，他們也使用英文小寫字母，但可能以不同的順序排列。
    /// 字母順序是小寫字母的某種排列。
    /// 給定一個用外星語言撰寫的單詞序列，以及字母的順序，
    /// 當且僅當給定的單詞在此外星語言中按字典序排序時，返回 true。
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 一次遍歷
    /// 
    /// 比對輸入的string 要依據 題目給的 order 排序
    /// 前一個字母跟後一個字母 要依據 order 排序
    /// 
    /// 題目給的範例需要詳細閱讀與觀察
    /// case1, 2: 比较每个单词出现的第一个不同的字母，如果index小于后面的单词，就不再比较 return false
    /// case3: 如果没出现不同字母&&前一个单词的长度比后一个单词长度大，返回false
    /// </summary>
    /// <param name="words"></param>
    /// <param name="order"></param>
    /// <returns></returns>
    public bool IsAlienSorted(string[] words, string order)
    {
        int[] index = new int[26];

        // 把題目的字典順序大小轉成index陣列，index[0] = a, index[1] = b, index[2] = c, ...
        for(int i = 0; i < order.Length; i++)
        {
            index[order[i] - 'a'] = i;
        }

        // 依序檢查第 i 與 i - 1 字典順序大小
        // i > i - 1  return true;
        // i < i - 1  return false; 
        for (int i = 1; i < words.Length; i++)
        {
            bool valid = false;

            // j 要在 word 長度範圍內, 不能超出
            for (int j = 0; j < words[i - 1].Length && j < words[i].Length; j++)
            {
                int prev = index[words[i - 1][j] - 'a'];
                int curr = index[words[i][j] - 'a'];

                if (prev < curr)
                {
                    valid = true;
                    break;
                }
                else if (prev > curr)
                {
                    return false;
                }
            }

            if (!valid)
            {
                // 當 輸入的字母 前面的比後面的長度還要長
                // 直接回傳 false; 
                if (words[i - 1].Length > words[i].Length)
                {
                    return false;
                }
            }
        }

        return true;        
    }
}
