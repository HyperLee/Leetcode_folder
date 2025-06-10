namespace leetcode_3442;

class Program
{
    /// <summary>
    /// 3442. Maximum Difference Between Even and Odd Frequency I
    /// https://leetcode.com/problems/maximum-difference-between-even-and-odd-frequency-i/description/?envType=daily-question&envId=2025-06-10
    /// 3442. 奇偶频次间的最大差值 I
    /// https://leetcode.cn/problems/maximum-difference-between-even-and-odd-frequency-i/description/?envType=daily-question&envId=2025-06-10
    /// 題目描述:
    /// 給定一個只包含小寫英文字母的字串 s。
    /// 請找出滿足以下條件的最大差值 diff = freq(a1) - freq(a2)：
    /// a1 在字串中出現的頻次為奇數。
    /// a2 在字串中出現的頻次為偶數。
    /// 返回這個最大差值。
    /// 
    /// 題目要求出 奇數 - 偶數 = 最大值
    /// 要求出最大所以 奇數要盡可能的找出最大值，偶數要盡可能的找出最小值。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        string[] testCases = new string[]
        {
            "aabbcc",    // 預期: 2-2=0
            "abcabcabc", // 預期: 3-0=3
            "aabbccd",   // 預期: 2-2=0
            "aabbccdde", // 預期: 2-2=0
            "a",         // 預期: 1-0=1
            "",          // 預期: 0-0=0
            "zzzzzz",    // 預期: 6-0=6
            "aabbbbcc",   // 預期: 4-2=2
            "mmsmsym"     // 預期 1-4 = -1
        };

        var prog = new Program();
        foreach (var s in testCases)
        {
            int res1 = prog.MaxDifference(s);
            int res2 = prog.MaxDifference2(s);
            Console.WriteLine($"測試字串: '{s}' | 方法一: {res1} | 方法二: {res2}");
        }
    }

    /// <summary>
    /// 解法一：
    /// 使用長度為 26 的陣列記錄每個字母的出現次數。
    /// 步驟：
    /// 1. 遍歷字串，統計每個字母的頻次。
    /// 2. 找出所有奇數頻次的最大值 oddMax，偶數頻次的最小值 evenMin。
    /// 3. 回傳 oddMax - evenMin。
    /// - 若沒有奇數頻次或偶數頻次，則視為 0。
    /// 複雜度：
    /// - 時間複雜度 O(n)，n 為字串長度。
    /// - 空間複雜度 O(1)，因為字母數固定為 26。
    /// 優點：效能最佳，程式碼簡潔。
    /// 缺點：僅適用於字母範圍固定的情境。
    /// </summary>
    /// <param name="s">輸入字串</param>
    /// <returns>最大差值</returns>
    public int MaxDifference(string s)
    {
        // 建立長度為 26 的陣列，記錄每個字母的出現次數
        int[] freq = new int[26];
        foreach (char c in s)
        {
            freq[c - 'a']++; // 統計每個字母的頻次
        }

        int oddMax = int.MinValue; // 奇數頻次最大值
        int evenMin = int.MaxValue; // 偶數頻次最小值
        foreach (int count in freq)
        {
            if (count == 0) continue; // 跳過未出現的字母
            if (count % 2 == 0)
            {
                // 更新偶數頻次的最小值
                evenMin = Math.Min(evenMin, count);
            }
            else
            {
                // 更新奇數頻次的最大值
                oddMax = Math.Max(oddMax, count);
            }
        }
        // 若沒有奇數或偶數頻次，視為 0
        if (oddMax == int.MinValue) oddMax = 0;
        if (evenMin == int.MaxValue) evenMin = 0;
        // 回傳最大差值
        return oddMax - evenMin;
    }

    /// <summary>
    /// 解法二：
    /// 使用 Dictionary 記錄每個字母的出現次數。
    /// 步驟：
    /// 1. 遍歷字串，統計每個字母的頻次（支援字母範圍不固定）。
    /// 2. 找出所有奇數頻次的最大值 oddMax，偶數頻次的最小值 evenMin。
    /// 3. 回傳 oddMax - evenMin。
    /// - 若沒有奇數頻次或偶數頻次，則視為 0。
    /// 複雜度：
    /// - 時間複雜度 O(n)，n 為字串長度。
    /// - 空間複雜度 O(1)（若字母種類有限），否則 O(k)，k 為不同字母數。
    /// 優點：可擴充性高，適用於字母範圍不固定。
    /// 缺點：效能略低於陣列，程式碼稍長。
    /// </summary>
    /// <param name="s">輸入字串</param>
    /// <returns>最大差值</returns>
    public int MaxDifference2(string s)
    {
        // 使用 Dictionary 記錄每個字母的出現次數
        Dictionary<int, int> freq = new Dictionary<int, int>();
        foreach (char c in s)
        {
            int index = c - 'a';
            if (freq.ContainsKey(index))
            {
                freq[index]++; // 已存在則累加
            }
            else
            {
                freq[index] = 1; // 首次出現設為 1
            }
        }

        int oddMax = int.MinValue; // 奇數頻次最大值
        int evenMin = int.MaxValue; // 偶數頻次最小值
        foreach (var kvp in freq)
        {
            if (kvp.Value == 0) continue; // 跳過未出現的字母
            if (kvp.Value % 2 == 0)
            {
                // 更新偶數頻次的最小值
                evenMin = Math.Min(evenMin, kvp.Value);
            }
            else
            {
                // 更新奇數頻次的最大值
                oddMax = Math.Max(oddMax, kvp.Value);
            }
        }
        // 若沒有奇數或偶數頻次，視為 0
        if (oddMax == int.MinValue) oddMax = 0;
        if (evenMin == int.MaxValue) evenMin = 0;
        // 回傳最大差值
        return oddMax - evenMin;
    }
}
