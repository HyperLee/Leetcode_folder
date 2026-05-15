namespace leetcode_028;

internal static class Program
{
    /// <summary>
    /// https://leetcode.com/problems/find-the-index-of-the-first-occurrence-in-a-string/description/
    /// 28. Find the Index of the First Occurrence in a String
    /// Given two strings needle and haystack, return the index of the first occurrence of needle in haystack,
    /// or -1 if needle is not part of haystack.
    ///
    /// 28. 找出字串中第一個匹配項的索引
    /// 給定兩個字串 needle 和 haystack，回傳 needle 在 haystack 中第一次出現的索引；
    /// 如果 needle 不是 haystack 的一部分，則回傳 -1。
    /// https://leetcode.cn/problems/find-the-index-of-the-first-occurrence-in-a-string/description/
    /// </summary>
    /// <param name="args"></param>
    private static void Main(string[] args)
    {
        RunDemoCases();
    }

    /// <summary>
    /// 執行固定測資並輸出兩種解法的結果。用途是讓主程式可直接驗證字串匹配是否正確；
    /// 解題概念是讓暴力法與 KMP 在同一組符合題目限制的輸入上同步比對；
    /// 輸入為程式內建的 haystack、needle 與預期答案，輸出為每筆測資的索引結果與 PASS 或 FAIL 狀態。
    /// </summary>
    private static void RunDemoCases()
    {
        (string Haystack, string Needle, int Expected)[] demoCases =
        [
            ("sadbutsad", "sad", 0),
            ("leetcode", "leeto", -1),
            ("aaaaa", "bba", -1),
            ("mississippi", "issip", 4),
        ];

        Console.WriteLine("LeetCode 28 - Find the Index of the First Occurrence in a String");
        Console.WriteLine();

        for (int caseIndex = 0; caseIndex < demoCases.Length; caseIndex++)
        {
            (string haystack, string needle, int expected) = demoCases[caseIndex];
            int bruteForceResult = StrStrBruteForce(haystack, needle);
            int kmpResult = StrStrKmp(haystack, needle);

            PrintDemoCaseResult(caseIndex + 1, haystack, needle, expected, bruteForceResult, kmpResult);
        }
    }

    /// <summary>
    /// 將單筆測資的輸入、預期值與兩種解法結果格式化輸出。用途是集中管理示範輸出格式；
    /// 解題概念是把演算法驗證和顯示責任分開；
    /// 輸入需提供題目字串、預期索引與實際索引，輸出為主控台上的可讀示範資訊。
    /// </summary>
    /// <param name="caseNumber">目前輸出的測資編號，從 1 開始遞增。</param>
    /// <param name="haystack">要被搜尋的主字串，預期符合題目限制的非空小寫英文字串。</param>
    /// <param name="needle">要匹配的目標字串，預期符合題目限制的非空小寫英文字串。</param>
    /// <param name="expected">題目預期應回傳的第一個匹配起始索引，若不存在則為 -1。</param>
    /// <param name="bruteForceResult">暴力法計算出的索引結果。</param>
    /// <param name="kmpResult">KMP 解法計算出的索引結果。</param>
    private static void PrintDemoCaseResult(
        int caseNumber,
        string haystack,
        string needle,
        int expected,
        int bruteForceResult,
        int kmpResult)
    {
        string bruteForceStatus = bruteForceResult == expected ? "PASS" : "FAIL";
        string kmpStatus = kmpResult == expected ? "PASS" : "FAIL";

        Console.WriteLine($"Case {caseNumber}: haystack = \"{haystack}\", needle = \"{needle}\", expected = {expected}");
        Console.WriteLine($"Brute force: {bruteForceResult} ({bruteForceStatus})");
        Console.WriteLine($"KMP: {kmpResult} ({kmpStatus})");
        Console.WriteLine();
    }

    /// <summary>
    /// 使用暴力比對找出 needle 在 haystack 中第一次出現的位置。用途是提供最直接的基礎解法；
    /// 解題概念是從 haystack 的每個可能起點逐字比較 needle；
    /// 輸入預設為符合題目限制的字串，輸出為第一個匹配起始索引，若找不到則回傳 -1，時間複雜度為 O(n * m)。
    /// </summary>
    /// <param name="haystack">要被搜尋的主字串，通常長度大於等於 needle。</param>
    /// <param name="needle">要尋找的目標字串，長度至少為 1；若傳入空字串則回傳 0。</param>
    /// <returns>needle 第一次出現的起始索引；若不存在則回傳 -1。</returns>
    public static int StrStrBruteForce(string haystack, string needle)
    {
        if (needle.Length == 0)
        {
            return 0;
        }

        int haystackLength = haystack.Length;
        int needleLength = needle.Length;

        // 只需要檢查還放得下 needle 的起點，避免無效的尾端比對。
        for (int haystackStart = 0; haystackStart + needleLength <= haystackLength; haystackStart++)
        {
            bool isMatch = true;

            for (int needleIndex = 0; needleIndex < needleLength; needleIndex++)
            {
                // 一旦某個字元不同，這個起點就不可能成功，直接換下一個起點。
                if (haystack[haystackStart + needleIndex] != needle[needleIndex])
                {
                    isMatch = false;
                    break;
                }
            }

            if (isMatch)
            {
                return haystackStart;
            }
        }

        return -1;
    }

    /// <summary>
    /// 使用 KMP 演算法找出 needle 在 haystack 中第一次出現的位置。用途是提供線性時間的進階解法；
    /// 解題概念是先建立最長前後綴表，失敗時重用已匹配資訊而不是整段重來；
    /// 輸入預設為符合題目限制的字串，輸出為第一個匹配起始索引，若找不到則回傳 -1，時間複雜度為 O(n + m)。
    /// </summary>
    /// <param name="haystack">要被搜尋的主字串，可視為文字串。</param>
    /// <param name="needle">要尋找的目標字串，可視為模式串；若傳入空字串則回傳 0。</param>
    /// <returns>needle 第一次出現的起始索引；若不存在則回傳 -1。</returns>
    public static int StrStrKmp(string haystack, string needle)
    {
        if (needle.Length == 0)
        {
            return 0;
        }

        int[] longestPrefixSuffix = BuildLps(needle);
        int haystackIndex = 0;
        int needleIndex = 0;

        while (haystackIndex < haystack.Length)
        {
            if (haystack[haystackIndex] == needle[needleIndex])
            {
                haystackIndex++;
                needleIndex++;

                if (needleIndex == needle.Length)
                {
                    return haystackIndex - needleIndex;
                }

                continue;
            }

            if (needleIndex > 0)
            {
                // 失敗時退回上一個可延續的前後綴長度，避免 haystackIndex 重新從下一格開始。
                needleIndex = longestPrefixSuffix[needleIndex - 1];
                continue;
            }

            haystackIndex++;
        }

        return -1;
    }

    /// <summary>
    /// 建立 KMP 所需的最長前後綴表。用途是記錄每個位置之前可重用的匹配長度；
    /// 解題概念是對 pattern 自身做前綴與後綴的最長相等長度預處理；
    /// 輸入為模式字串，輸出為等長整數陣列，其中每格代表該位置結尾的最長共同前後綴長度。
    /// </summary>
    /// <param name="pattern">KMP 的模式字串，通常為題目的 needle。</param>
    /// <returns>對應 pattern 的 LPS 陣列，用來在匹配失敗時決定下一個比較位置。</returns>
    private static int[] BuildLps(string pattern)
    {
        int[] longestPrefixSuffix = new int[pattern.Length];
        int prefixLength = 0;
        int patternIndex = 1;

        while (patternIndex < pattern.Length)
        {
            if (pattern[patternIndex] == pattern[prefixLength])
            {
                prefixLength++;
                longestPrefixSuffix[patternIndex] = prefixLength;
                patternIndex++;
                continue;
            }

            if (prefixLength > 0)
            {
                // 回退到上一段可延續的前後綴長度，繼續嘗試接上目前字元。
                prefixLength = longestPrefixSuffix[prefixLength - 1];
                continue;
            }

            longestPrefixSuffix[patternIndex] = 0;
            patternIndex++;
        }

        return longestPrefixSuffix;
    }
}
