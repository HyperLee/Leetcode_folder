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
    /// <param name="args">命令列參數；本範例不使用此參數。</param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        (string Name, string Word1, string Word2, bool Expected)[] testCases =
        [
            ("Example 1", "abc", "bca", true),
            ("Example 2", "a", "aa", false),
            ("Example 3", "cabbba", "abbccc", true),
            ("Character set mismatch", "uau", "ssx", false)
        ];

        foreach ((string name, string word1, string word2, bool expected) in testCases)
        {
            RunTestCase(solution, name, word1, word2, expected);
        }
    }

    /// <summary>
    /// 執行一組「判定兩個字串是否接近」範例，分別呼叫兩種解法並列印預期結果、
    /// 實際結果與 PASS/FAIL。輸入應為長度 1 到 100000、且只含小寫英文字母的字串；
    /// 此方法只輸出比較結果，不回傳資料。
    /// </summary>
    /// <param name="solution">包含兩種解法的 <see cref="Program"/> 實例。</param>
    /// <param name="name">顯示在主控台上的案例名稱。</param>
    /// <param name="word1">第一個待比較的非空小寫英文字母字串。</param>
    /// <param name="word2">第二個待比較的非空小寫英文字母字串。</param>
    /// <param name="expected">此組輸入是否應被判定為接近字串。</param>
    private static void RunTestCase(Program solution, string name, string word1, string word2, bool expected)
    {
        bool dictionaryResult = solution.CloseStrings(word1, word2);
        bool countingResult = solution.CloseStrings2(word1, word2);

        Console.WriteLine($"{name}: word1 = \"{word1}\", word2 = \"{word2}\", Expected = {expected}");
        Console.WriteLine($"  CloseStrings:  Actual = {dictionaryResult} ({(dictionaryResult == expected ? "PASS" : "FAIL")})");
        Console.WriteLine($"  CloseStrings2: Actual = {countingResult} ({(countingResult == expected ? "PASS" : "FAIL")})");
        Console.WriteLine();
    }

    /// <summary>
    /// 使用 Dictionary 統計兩個字串中各字元的出現次數，再確認字串長度相同、
    /// 出現過的字元集合相同，且排序後的字元頻率集合相同。輸入必須是長度
    /// 1 到 100000、且只包含小寫英文字母的非空字串；符合接近字串條件時回傳
    /// <see langword="true"/>，否則回傳 <see langword="false"/>。
    /// </summary>
    /// <param name="word1">第一個待比較的非空小寫英文字母字串。</param>
    /// <param name="word2">第二個待比較的非空小寫英文字母字串。</param>
    /// <returns>兩個字串可透過題目允許的操作互相轉換時為 <see langword="true"/>；否則為 <see langword="false"/>。</returns>
    public bool CloseStrings(string word1, string word2)
    {
        // 交換與字元名稱互換都不會改變字串長度，因此長度不同時一定不接近。
        if(word1.Trim().Length != word2.Trim().Length)
        {
            return false;
        }

        // Dictionary 的 key 是字元，value 是該字元的出現頻率。
        Dictionary<char, int> dic1 = new Dictionary<char, int>();
        foreach(char value in word1.ToCharArray())
        {
            if(dic1.ContainsKey(value))
            {
                dic1[value]++;
            }
            else
            {
                dic1.Add(value, 1);
            }
        }

        Dictionary<char, int> dic2 = new Dictionary<char, int>();
        foreach(char value in word2.ToCharArray())
        {
            if(dic2.ContainsKey(value))
            {
                dic2[value]++;
            }
            else
            {
                dic2.Add(value, 1);
            }
        }

        List<int> list1 = new List<int>();
        List<int> list2 = new List<int>();

        foreach(var kvp in dic1)
        {
            // 操作 2 只能交換兩個已存在的字元名稱，不能創造另一邊沒有的字元。
            if(!dic2.ContainsKey(kvp.Key))
            {
                return false;
            }
        }

        foreach(var kvp in dic1)
        {
            list1.Add(kvp.Value);
        }

        list1.Sort();

        foreach(var kvp in dic2)
        {
            list2.Add(kvp.Value);
        }

        list2.Sort();

        // 操作 2 可以重新指派頻率所屬的字元；排序後只需比較兩邊的頻率多重集合。
        for(int i = 0; i < list1.Count; i++)
        {
            if(list1[i] != list2[i])
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 使用固定長度 26 的計數陣列記錄小寫英文字母頻率，先確認兩個字串出現過的
    /// 字元集合相同，再排序並比較完整頻率陣列。輸入必須是長度 1 到 100000、且只
    /// 包含小寫英文字母的非空字串；符合接近字串條件時回傳 <see langword="true"/>，
    /// 否則回傳 <see langword="false"/>。
    /// </summary>
    /// <param name="word1">第一個待比較的非空小寫英文字母字串。</param>
    /// <param name="word2">第二個待比較的非空小寫英文字母字串。</param>
    /// <returns>兩個字串可透過題目允許的操作互相轉換時為 <see langword="true"/>；否則為 <see langword="false"/>。</returns>
    public bool CloseStrings2(string word1, string word2)
    {
        int[] count1 = new int[26];
        int[] count2 = new int[26];
        foreach(char c in word1)
        {
            count1[c - 'a']++;
        }

        foreach(char c in word2)
        {
            count2[c - 'a']++;
        }

        for(int i = 0; i < 26; i++)
        {
            // 同一個字母必須在兩邊同時出現，才能利用操作 2 重新分配頻率。
            if(count1[i] > 0 && count2[i] == 0 || count1[i] == 0 && count2[i] > 0)
            {
                return false;
            }
        }

        Array.Sort(count1);
        Array.Sort(count2);

        // 排序會忽略頻率原本屬於哪個字母，只比較兩邊可重新指派的頻率是否一致。
        return Enumerable.SequenceEqual(count1, count2);
    }
}
