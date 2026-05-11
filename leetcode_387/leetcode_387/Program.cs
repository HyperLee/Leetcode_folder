namespace leetcode_387;

class Program
{
    /// <summary>
    /// 387. First Unique Character in a String
    /// https://leetcode.com/problems/first-unique-character-in-a-string/description/
    /// 387. 字符串中的第一个唯一字符
    /// https://leetcode.cn/problems/first-unique-character-in-a-string/description/
    /// 
    /// Given a string s, find the first non-repeating character in it and return its index. If it does not exist, return -1.
    /// 給定一個字串 s，找出其中第一個不重複的字元，並回傳它的索引。如果不存在，則回傳 -1。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        (string Input, int Expected)[] testCases = new (string Input, int Expected)[]
        {
            ("leetcode", 0),
            ("loveleetcode", 2),
            ("aabb", -1),
            ("", -1),
        };
        (string Name, Func<string, int> Method)[] methods = new (string Name, Func<string, int> Method)[]
        {
            (nameof(FirstUniqChar), solution.FirstUniqChar),
            (nameof(FirstUniqChar2), solution.FirstUniqChar2),
            (nameof(FirstUniqChar3), solution.FirstUniqChar3),
            (nameof(FirstUniqChar4), solution.FirstUniqChar4),
        };

        foreach ((string input, int expected) in testCases)
        {
            Console.WriteLine($"Input: \"{input}\", Expected: {expected}");

            foreach ((string name, Func<string, int> method) in methods)
            {
                int actual = method(input);
                string result = actual == expected ? "PASS" : "FAIL";
                Console.WriteLine($"  {name}: {actual} ({result})");
            }

            Console.WriteLine();
        }
    }

    /// <summary>
    /// 使用字典記錄每個字元第一次出現的索引，並在遇到重複字元時改以 -1 標記。
    /// 輸入需為非 null 字串，題目限制為小寫英文字母；輸出為第一個不重複字元的索引，不存在時回傳 -1。
    /// 解題概念是先掃描一次建立「字元到索引或重複標記」的對照，再依原字串順序找出第一個索引未被標記為 -1 的字元。
    /// </summary>
    /// <param name="s">要檢查的字串。</param>
    /// <returns>第一個唯一字元的索引；若所有字元都重複或字串為空，回傳 -1。</returns>
    public int FirstUniqChar(string s)
    {
        Dictionary<char, int> indexes = new Dictionary<char, int>();
        for (int i = 0; i < s.Length; i++)
        {
            char ch = s[i];

            // 重複字元不需要保留原索引，直接以 -1 表示不可作為答案。
            if (indexes.ContainsKey(ch))
            {
                indexes[ch] = -1;
            }
            else
            {
                indexes.Add(ch, i);
            }
        }

        for (int i = 0; i < s.Length; i++)
        {
            if (indexes[s[i]] != -1)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// 逐一檢查每個字元的第一次出現位置與最後一次出現位置是否相同。
    /// 輸入需為非 null 字串，題目限制為小寫英文字母；輸出為第一個不重複字元的索引，不存在時回傳 -1。
    /// 解題概念是若 <see cref="string.IndexOf(char)"/> 與 <see cref="string.LastIndexOf(char)"/> 相等，代表該字元只出現一次。
    /// </summary>
    /// <param name="s">要檢查的字串。</param>
    /// <returns>第一個唯一字元的索引；若所有字元都重複或字串為空，回傳 -1。</returns>
    public int FirstUniqChar2(string s)
    {
        for (int i = 0; i < s.Length; i++)
        {
            char ch = s[i];

            // 第一次與最後一次出現位置相同，表示目前字元在整個字串中唯一。
            if (s.IndexOf(ch) == s.LastIndexOf(ch))
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// 使用 ASCII 頻率陣列統計每個字元出現次數，再依原字串順序找出第一個頻率為 1 的字元。
    /// 輸入需為非 null 字串，題目限制為小寫英文字母，因此可安全使用長度 128 的 ASCII 陣列；輸出為第一個不重複字元的索引，不存在時回傳 -1。
    /// 解題概念是以字元的 ASCII code 作為陣列索引，先累計次數，再二次掃描確認答案位置。
    /// </summary>
    /// <param name="s">要檢查的字串。</param>
    /// <returns>第一個唯一字元的索引；若所有字元都重複或字串為空，回傳 -1。</returns>
    public int FirstUniqChar3(string s)
    {
        int[] frequencies = new int[128];
        for (int i = 0; i < s.Length; i++)
        {
            frequencies[s[i]]++;
        }

        // 第二次掃描保留原字串順序，第一個頻率為 1 的位置就是答案。
        for (int i = 0; i < s.Length; i++)
        {
            if (frequencies[s[i]] == 1)
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// 使用字典統計每個字元的出現次數，再依原字串順序找出第一個頻率為 1 的字元。
    /// 輸入需為非 null 字串，題目限制為小寫英文字母；輸出為第一個不重複字元的索引，不存在時回傳 -1。
    /// 解題概念與頻率陣列相同，但以 <see cref="Dictionary{TKey,TValue}"/> 儲存次數，避免依賴固定字元編碼範圍。
    /// </summary>
    /// <param name="s">要檢查的字串。</param>
    /// <returns>第一個唯一字元的索引；若所有字元都重複或字串為空，回傳 -1。</returns>
    public int FirstUniqChar4(string s)
    {
        Dictionary<char, int> frequency = new Dictionary<char, int>();
        for (int i = 0; i < s.Length; i++)
        {
            char ch = s[i];

            if (frequency.ContainsKey(ch))
            {
                frequency[ch]++;
            }
            else
            {
                frequency[ch] = 1;
            }
        }

        for (int i = 0; i < s.Length; i++)
        {
            if (frequency[s[i]] == 1)
            {
                return i;
            }
        }

        return -1;
    }
}