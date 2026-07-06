namespace leetcode_318;

class Program
{
    /// <summary>
    /// 318. Maximum Product of Word Lengths
    /// https://leetcode.com/problems/maximum-product-of-word-lengths/description/
    /// 318. 最大單字長度乘積
    /// https://leetcode.cn/problems/maximum-product-of-word-lengths/description/
    ///
    /// Given a string array words, return the maximum value of length(word[i]) * length(word[j]) where the two words do not share common letters. If no such two words exist, return 0.
    ///
    /// 給定一個字串陣列 words，回傳 length(word[i]) * length(word[j]) 的最大值，其中這兩個單字不共享任何相同字母。若不存在這樣的兩個單字，回傳 0。
    /// </summary>
    /// <remarks>
    /// 進入點會執行固定測資，並同時驗證兩種位元遮罩解法的輸出。
    /// </remarks>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        RunSamples();
    }

    /// <summary>
    /// 執行 LeetCode 318 的固定範例測資，逐筆比較預期值與兩種解法的實際輸出。
    /// 測資包含官方範例、無可配對情境、相同 mask 壓縮情境，以及單字涵蓋全部 26 個字母的邊界情境。
    /// 輸出結果可直接複製到 README 作為範例執行紀錄。
    /// </summary>
    private static void RunSamples()
    {
        Program solution = new Program();
        (string[] Words, int Expected)[] samples =
        [
            (["abcw", "baz", "foo", "bar", "xtfn", "abcdef"], 16),
            (["a", "ab", "abc", "d", "cd", "bcd", "abcd"], 4),
            (["a", "aa", "aaa", "aaaa"], 0),
            (["ab", "aabb", "cd"], 8),
            (["abcdefghijklmnopqrstuvwxyz", "abc", "bcd"], 0)
        ];

        int passed = 0;
        int total = samples.Length * 2;

        Console.WriteLine("LeetCode 318 - Maximum Product of Word Lengths");

        for (int i = 0; i < samples.Length; i++)
        {
            string[] words = samples[i].Words;
            int expected = samples[i].Expected;
            int firstResult = solution.MaxProduct(words);
            int secondResult = solution.MaxProduct2(words);

            if (firstResult == expected)
            {
                passed++;
            }

            if (secondResult == expected)
            {
                passed++;
            }

            Console.WriteLine($"案例 {i + 1}: words = {FormatWords(words)}");
            Console.WriteLine($"  Expected: {expected}");
            Console.WriteLine($"  MaxProduct  Result: {firstResult} ({(firstResult == expected ? "PASS" : "FAIL")})");
            Console.WriteLine($"  MaxProduct2 Result: {secondResult} ({(secondResult == expected ? "PASS" : "FAIL")})");

            if (i < samples.Length - 1)
            {
                Console.WriteLine();
            }
        }

        Console.WriteLine($"總結：{passed}/{total} 項驗證通過");
    }

    /// <summary>
    /// 方法一：使用陣列保存每個單字的 26-bit 字母遮罩，再枚舉所有單字配對。
    /// 輸入需符合題目條件：每個字串只包含小寫英文字母。當兩個遮罩做 AND 後為 0，
    /// 代表兩個單字沒有共用字母，此時回傳所有合法配對中的最大長度乘積；若沒有合法配對則回傳 0。
    /// </summary>
    /// <param name="words">只包含小寫英文字母的單字陣列。</param>
    /// <returns>兩個不共享字母的單字長度乘積最大值；若不存在合法配對則為 0。</returns>
    public int MaxProduct(string[] words)
    {
        int length = words.Length;
        int[] masks = new int[length];

        for (int i = 0; i < length; i++)
        {
            masks[i] = BuildLetterMask(words[i]);
        }

        int maxProduct = 0;

        for (int i = 0; i < length; i++)
        {
            for (int j = i + 1; j < length; j++)
            {
                // AND 結果為 0 表示兩個單字的字母集合沒有交集，可以計算長度乘積。
                if ((masks[i] & masks[j]) == 0)
                {
                    maxProduct = Math.Max(maxProduct, words[i].Length * words[j].Length);
                }
            }
        }

        return maxProduct;
    }

    /// <summary>
    /// 方法二：先將相同字母集合的單字壓縮成同一個 mask，並只保存該 mask 對應的最大單字長度。
    /// 輸入需符合題目條件：每個字串只包含小寫英文字母。壓縮後再比較不同 mask 是否不相交，
    /// 回傳可由兩個不共享字母的單字形成的最大長度乘積；若沒有合法配對則回傳 0。
    /// </summary>
    /// <param name="words">只包含小寫英文字母的單字陣列。</param>
    /// <returns>兩個不共享字母的單字長度乘積最大值；若不存在合法配對則為 0。</returns>
    public int MaxProduct2(string[] words)
    {
        Dictionary<int, int> maxLengthByMask = new Dictionary<int, int>();

        foreach (string word in words)
        {
            int mask = BuildLetterMask(word);

            // 相同 mask 代表字母集合完全相同，只保留較長的單字即可。
            if (!maxLengthByMask.TryGetValue(mask, out int currentLength) || word.Length > currentLength)
            {
                maxLengthByMask[mask] = word.Length;
            }
        }

        int maxProduct = 0;

        foreach (KeyValuePair<int, int> first in maxLengthByMask)
        {
            foreach (KeyValuePair<int, int> second in maxLengthByMask)
            {
                if ((first.Key & second.Key) == 0)
                {
                    maxProduct = Math.Max(maxProduct, first.Value * second.Value);
                }
            }
        }

        return maxProduct;
    }

    /// <summary>
    /// 將只包含小寫英文字母的單字轉成 26-bit 字母遮罩。
    /// 第 0 bit 代表 a，第 25 bit 代表 z；同一個字母重複出現仍只會讓對應 bit 保持為 1。
    /// </summary>
    /// <param name="word">只包含小寫英文字母的單字。</param>
    /// <returns>表示單字字母集合的整數位元遮罩。</returns>
    private static int BuildLetterMask(string word)
    {
        int mask = 0;

        foreach (char letter in word)
        {
            mask |= 1 << (letter - 'a');
        }

        return mask;
    }

    /// <summary>
    /// 將測資單字陣列格式化成穩定的 console 顯示字串，方便 README 精準記錄實際輸出。
    /// </summary>
    /// <param name="words">要顯示的單字陣列。</param>
    /// <returns>形如 ["abc", "def"] 的單行字串。</returns>
    private static string FormatWords(string[] words)
    {
        string[] quotedWords = new string[words.Length];

        for (int i = 0; i < words.Length; i++)
        {
            quotedWords[i] = $"\"{words[i]}\"";
        }

        return $"[{string.Join(", ", quotedWords)}]";
    }
}
