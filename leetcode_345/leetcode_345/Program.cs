namespace leetcode_345;

class Program
{
    /// <summary>
    /// 345. Reverse Vowels of a String
    /// https://leetcode.com/problems/reverse-vowels-of-a-string/description/
    /// 345. 反转字符串中的元音字母
    /// https://leetcode.cn/problems/reverse-vowels-of-a-string/description/
    ///
    /// Given a string s, reverse only all the vowels in the string and return it.
    /// The vowels are 'a', 'e', 'i', 'o', and 'u', and they can appear in both lower and upper cases, more than once.
    ///
    /// 給定一個字串 s，請只反轉字串中的所有母音字母，並回傳反轉後的結果。
    /// 母音包含 'a'、'e'、'i'、'o' 與 'u'，它們可能以大小寫形式出現，且可重複出現多次。
    /// </summary>
    /// <remarks>
    /// 主程式會呼叫 <see cref="RunSamples"/>，以固定測資展示雙指標解法在官方案例、大小寫混合案例與無母音案例下的輸出結果。
    /// </remarks>
    /// <param name="args">命令列參數；本範例程式不需要使用。</param>
    static void Main(string[] args)
    {
        RunSamples();
    }

    /// <summary>
    /// 執行固定範例測資，列出輸入字串、預期結果、實際結果與 PASS/FAIL，方便直接透過主控台驗證解法。
    /// </summary>
    /// <remarks>
    /// 這個 repo 沒有獨立測試專案，因此使用 <c>RunSamples</c> 作為可執行的驗證入口，並讓 README 的輸出區塊直接對齊實際執行結果。
    /// </remarks>
    private static void RunSamples()
    {
        List<SampleCase> samples =
        [
            new SampleCase("官方範例 1 - 大小寫母音交錯", "IceCreAm", "AceCreIm"),
            new SampleCase("官方範例 2 - 一般英文單字", "leetcode", "leotcede"),
            new SampleCase("大小寫混合 - 兩端母音互換", "Aa", "aA"),
            new SampleCase("沒有母音 - 字串保持不變", "bcdfg", "bcdfg"),
            new SampleCase("重複母音與中間子音 - 驗證指標跳過流程", "queueing", "qieueung")
        ];

        Program solution = new Program();
        int passedCount = 0;

        Console.WriteLine("LeetCode 345 - Reverse Vowels of a String");
        Console.WriteLine("解法：雙指標從左右往中間收斂，只在兩端都停在母音時交換");
        Console.WriteLine();

        for (int i = 0; i < samples.Count; i++)
        {
            SampleCase sample = samples[i];
            string actual = solution.ReverseVowels(sample.Input);
            bool passed = actual == sample.Expected;

            if (passed)
            {
                passedCount++;
            }

            Console.WriteLine($"案例 {i + 1}：{sample.Description}");
            Console.WriteLine($"輸入：\"{sample.Input}\"");
            Console.WriteLine($"預期：\"{sample.Expected}\"");
            Console.WriteLine($"實際：\"{actual}\" => {(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        Console.WriteLine($"總結：{passedCount}/{samples.Count} 筆測試通過");
    }

    /// <summary>
    /// 使用雙指標反轉字串中的所有母音字母，保留其他非母音字元的相對位置不變。
    /// </summary>
    /// <remarks>
    /// 解題概念是讓左指標與右指標分別向中間收斂，略過所有非母音字元，只有當兩側都停在母音時才交換。
    /// 輸入字串需符合題目限制：長度至少為 1，內容由可列印 ASCII 字元組成；回傳結果會是與輸入等長的新字串。
    /// </remarks>
    /// <param name="s">要處理的原始字串，可能包含大小寫母音與任意非母音可列印 ASCII 字元。</param>
    /// <returns>只反轉母音順序後的新字串；所有非母音字元會保留在原本的位置。</returns>
    public string ReverseVowels(string s)
    {
        char[] characters = s.ToCharArray();
        int left = 0;
        int right = characters.Length - 1;

        while (left < right)
        {
            // 左指標負責找下一個待交換的母音；遇到子音時直接略過，避免做無意義的交換。
            while (left < right && !IsVowel(characters[left]))
            {
                left++;
            }

            // 右指標同樣只停在母音，讓兩端在一次線性掃描內完成配對。
            while (left < right && !IsVowel(characters[right]))
            {
                right--;
            }

            if (left < right)
            {
                // 只有兩端都鎖定母音時才交換，交換後同步內縮即可維持 O(n) 時間複雜度。
                Swap(characters, left, right);
                left++;
                right--;
            }
        }

        return new string(characters);
    }

    /// <summary>
    /// 判斷單一字元是否為題目定義中的母音字母，包含大小寫。
    /// </summary>
    /// <remarks>
    /// 題目只把 <c>a</c>、<c>e</c>、<c>i</c>、<c>o</c>、<c>u</c> 以及它們的大寫型態視為母音。
    /// </remarks>
    /// <param name="character">要判斷的單一字元。</param>
    /// <returns>若 <paramref name="character"/> 是母音則回傳 <c>true</c>；否則回傳 <c>false</c>。</returns>
    private static bool IsVowel(char character)
    {
        return "aeiouAEIOU".IndexOf(character) >= 0;
    }

    /// <summary>
    /// 交換字元陣列中兩個指定索引位置的字元，供雙指標在找到一對母音後原地互換。
    /// </summary>
    /// <param name="characters">由原始字串轉出的可變動字元陣列。</param>
    /// <param name="left">左側母音所在索引。</param>
    /// <param name="right">右側母音所在索引。</param>
    private static void Swap(char[] characters, int left, int right)
    {
        char temp = characters[left];
        characters[left] = characters[right];
        characters[right] = temp;
    }

    /// <summary>
    /// 表示一筆可執行範例，包含案例說明、輸入字串與預期輸出字串。
    /// </summary>
    /// <param name="Description">案例目的或這筆測資要覆蓋的行為。</param>
    /// <param name="Input">要傳入 <see cref="ReverseVowels"/> 的原始字串。</param>
    /// <param name="Expected">反轉所有母音後預期得到的字串。</param>
    private sealed record SampleCase(string Description, string Input, string Expected);
}
