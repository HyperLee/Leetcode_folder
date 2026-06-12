namespace leetcode_187;

class Program
{
    /// <summary>
    /// 187. Repeated DNA Sequences
    /// https://leetcode.com/problems/repeated-dna-sequences/description/
    /// 187. 重複的 DNA 序列
    /// https://leetcode.cn/problems/repeated-dna-sequences/description/
    ///
    /// The DNA sequence is composed of a series of nucleotides abbreviated as 'A', 'C', 'G', and 'T'.
    ///
    /// For example, "ACGAATTCCG" is a DNA sequence.
    /// When studying DNA, it is useful to identify repeated sequences within the DNA.
    ///
    /// Given a string s that represents a DNA sequence, return all the 10-letter-long sequences
    /// (substrings) that occur more than once in a DNA molecule. You may return the answer in any order.
    ///
    /// DNA 序列是由一連串以 'A'、'C'、'G' 與 'T' 縮寫表示的核苷酸所組成。
    ///
    /// 例如，"ACGAATTCCG" 是一個 DNA 序列。
    /// 在研究 DNA 時，找出 DNA 中重複出現的序列是很有幫助的。
    ///
    /// 給定一個代表 DNA 序列的字串 s，請回傳所有在 DNA 分子中出現超過一次、
    /// 長度為 10 的序列（子字串）。你可以以任意順序回傳答案。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solver = new Program();
        solver.RunSamples();
    }

    private const int SequenceLength = 10;

    private static readonly Dictionary<char, int> BinaryEncodingMap = new Dictionary<char, int>
    {
        ['A'] = 0,
        ['C'] = 1,
        ['G'] = 2,
        ['T'] = 3
    };

    /// <summary>
    /// 執行固定測資，驗證兩種解法都能找出所有重複出現且長度為 10 的 DNA 片段。
    /// 輸入資料為題目代表性案例，輸出為每組案例的預期答案、實際答案與比對結果。
    /// </summary>
    private void RunSamples()
    {
        (string Input, string[] Expected)[] samples =
        [
            ("AAAAACCCCCAAAAACCCCCCAAAAAGGGTTT", ["AAAAACCCCC", "CCCCCAAAAA"]),
            ("AAAAAAAAAAAAA", ["AAAAAAAAAA"]),
            ("ACGTACGT", [])
        ];

        Console.WriteLine("Repeated DNA Sequences Sample Runner");
        Console.WriteLine();

        PrintSolutionResults(
            "Solution 1 - Dictionary Counting",
            samples,
            FindRepeatedDnaSequences);

        PrintSolutionResults(
            "Solution 2 - Bitmask Sliding Window",
            samples,
            FindRepeatedDnaSequences2);
    }

    /// <summary>
    /// 逐一執行指定解法，將輸入、預期輸出與實際結果印成可直接閱讀的驗證報告。
    /// 輸入必須符合題目條件，輸出會標示每個案例是否與預期答案一致。
    /// </summary>
    /// <param name="title">本次要展示的解法名稱。</param>
    /// <param name="samples">要執行的測資與其預期答案。</param>
    /// <param name="solver">實際解題函式。</param>
    private void PrintSolutionResults(
        string title,
        IEnumerable<(string Input, string[] Expected)> samples,
        Func<string, IList<string>> solver)
    {
        Console.WriteLine(title);

        int index = 1;
        foreach((string Input, string[] Expected) sample in samples)
        {
            IList<string> actual = solver(sample.Input);
            bool isMatch = HasSameSequences(sample.Expected, actual);

            Console.WriteLine($"Case {index}");
            Console.WriteLine($"Input   : {sample.Input}");
            Console.WriteLine($"Expected: {FormatSequences(sample.Expected)}");
            Console.WriteLine($"Actual  : {FormatSequences(actual)}");
            Console.WriteLine($"Pass    : {isMatch}");
            Console.WriteLine();

            index++;
        }
    }

    /// <summary>
    /// 解法一：使用字典統計每個長度為 10 的子字串出現次數。
    /// 輸入必須是只包含 A、C、G、T 的 DNA 字串；輸出為所有出現超過一次的長度 10 子字串。
    /// 核心概念是枚舉所有固定長度視窗，並在子字串第二次出現時加入答案，避免重複收錄。
    /// </summary>
    /// <param name="s">只包含 A、C、G、T 的 DNA 序列字串。</param>
    /// <returns>所有重複出現的長度 10 DNA 序列，順序可任意。</returns>
    public IList<string> FindRepeatedDnaSequences(string s)
    {
        IList<string> result = new List<string>();
        Dictionary<string, int> counts = new Dictionary<string, int>();
        int length = s.Length;

        for(int i = 0; i <= length - SequenceLength; i++)
        {
            string sequence = s.Substring(i, SequenceLength);

            if(!counts.ContainsKey(sequence))
            {
                counts.Add(sequence, 1);
            }
            else
            {
                counts[sequence]++;
            }

            // 只有在第二次遇到相同片段時才加入答案，可避免第三次之後重複收錄。
            if(counts[sequence] == 2)
            {
                result.Add(sequence);
            }
        }

        return result;
    }

    /// <summary>
    /// 解法二：使用 2-bit 編碼、滑動視窗與位元運算壓縮每個長度為 10 的子字串。
    /// 輸入必須是只包含 A、C、G、T 的 DNA 字串；輸出為所有出現超過一次的長度 10 子字串。
    /// 核心概念是將每個字元映射成 2 個 bit，讓固定長度視窗能以 O(1) 更新編碼並完成重複判斷。
    /// </summary>
    /// <param name="s">只包含 A、C、G、T 的 DNA 序列字串。</param>
    /// <returns>所有重複出現的長度 10 DNA 序列，順序可任意。</returns>
    public IList<string> FindRepeatedDnaSequences2(string s)
    {
        IList<string> result = new List<string>();
        int length = s.Length;

        if(length < SequenceLength)
        {
            return result;
        }

        int encodedWindow = 0;
        for(int i = 0; i < SequenceLength - 1; i++)
        {
            encodedWindow = (encodedWindow << 2) | BinaryEncodingMap[s[i]];
        }

        Dictionary<int, int> counts = new Dictionary<int, int>();
        int mask = (1 << (SequenceLength * 2)) - 1;

        for(int i = 0; i <= length - SequenceLength; i++)
        {
            // 左移 2 位騰出空間給新字元，再用 mask 保留視窗內對應的低 20 位。
            encodedWindow = ((encodedWindow << 2) | BinaryEncodingMap[s[i + SequenceLength - 1]]) & mask;

            if(!counts.ContainsKey(encodedWindow))
            {
                counts.Add(encodedWindow, 1);
            }
            else
            {
                counts[encodedWindow]++;
            }

            if(counts[encodedWindow] == 2)
            {
                result.Add(s.Substring(i, SequenceLength));
            }
        }

        return result;
    }

    /// <summary>
    /// 比較兩組 DNA 片段是否代表相同答案集合。
    /// 輸入可以是任意順序的結果集合；輸出為是否在排序後完全一致。
    /// </summary>
    /// <param name="expected">預期答案集合。</param>
    /// <param name="actual">實際輸出集合。</param>
    /// <returns>若兩組結果內容相同則為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
    private static bool HasSameSequences(IEnumerable<string> expected, IEnumerable<string> actual)
    {
        string[] normalizedExpected = expected.OrderBy(sequence => sequence).ToArray();
        string[] normalizedActual = actual.OrderBy(sequence => sequence).ToArray();

        return normalizedExpected.SequenceEqual(normalizedActual);
    }

    /// <summary>
    /// 將 DNA 片段集合格式化為主控台可閱讀的字串。
    /// 輸入為任意字串集合；輸出為以中括號包住的逗號分隔表示法。
    /// </summary>
    /// <param name="sequences">要格式化的 DNA 片段集合。</param>
    /// <returns>適合顯示在主控台與 README 的序列文字。</returns>
    private static string FormatSequences(IEnumerable<string> sequences)
    {
        string[] orderedSequences = sequences.OrderBy(sequence => sequence).ToArray();
        return $"[{string.Join(", ", orderedSequences.Select(sequence => $"\"{sequence}\""))}]";
    }
}
