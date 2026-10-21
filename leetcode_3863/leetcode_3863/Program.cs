using System.Reflection.Metadata.Ecma335;

namespace leetcode_3863;

class Program
{
    /// <summary>
    /// 3863. Minimum Operations to Sort a String
    /// https://leetcode.com/problems/minimum-operations-to-sort-a-string/description/
    /// 3863. 將字串排序所需的最少操作次數
    /// https://leetcode.cn/problems/minimum-operations-to-sort-a-string/description/
    /// <para>Original English prompt:</para>
    /// <para>Given a string <c>s</c> consisting of lowercase English letters.</para>
    /// <para>In one operation, you can select any <b>substring</b> of <c>s</c> that is <b>not</b> the entire string and <b>sort</b> it in <b>non-descending alphabetical</b> order.</para>
    /// <para>Return the minimum number of operations required to make <c>s</c> sorted in <b>non-descending</b> order. If it is not possible, return -1.</para>
    /// <para>繁體中文翻譯：</para>
    /// <para>給定一個由小寫英文字母組成的字串 <c>s</c>。</para>
    /// <para>一次操作中，你可以選擇 <c>s</c> 的任意一個不是整個字串的子字串，並將該子字串依字母順序非遞減排序。</para>
    /// <para>請回傳使 <c>s</c> 依字母順序非遞減排序所需的最少操作次數。若無法完成，回傳 -1。</para>
    /// <para>此進入點執行固定的範例資料，逐筆核對 MinOperations 的實際結果與預期結果。</para>
    /// </summary>
    /// <param name="args">命令列參數；範例程式不讀取此參數。</param>
    static void Main(string[] args)
    {
        (string Input, int Expected)[] examples =
        {
            ("abc", 0),
            ("dog", 1),
            ("card", 2),
            ("dcba", 3),
            ("gf", -1)
        };
        Program solution = new Program();

        foreach ((string input, int expected) in examples)
        {
            int actual = solution.MinOperations(input);
            if (actual != expected)
            {
                throw new InvalidOperationException(
                    $"輸入 \"{input}\" 預期結果為 {expected}，實際結果為 {actual}。");
            }

            Console.WriteLine($"{input} -> {actual}");
        }
    }

    /// <summary>
    /// 使用分類討論求出將字串排序所需的最少操作次數。
    /// 透過一次掃描同時檢查字串是否已排序，並找出最小與最大字元，再依極值位置判斷答案。
    /// 輸入為非空且只包含小寫英文字母的字串；無法排序時回傳 -1。
    /// </summary>
    /// <param name="s">長度介於 1 到 100,000，且只包含小寫英文字母的字串。</param>
    /// <returns>排序所需的最少操作次數；若無法排序則回傳 -1。</returns>
    public int MinOperations(string s)
    {
        int n = s.Length;
        char mx = 'a', mn = 'z';
        bool sorted = true;

        // 一次掃描同時取得全域極值，並利用相鄰逆序判斷字串是否已排序。
        for (int i = 0; i < n; i++)
        {
            if (s[i] > mx)
            {
                mx = s[i];
            }

            if (s[i] < mn)
            {
                mn = s[i];
            }

            if (i > 0 && s[i - 1] > s[i])
            {
                sorted = false;
            }
        }

        if (sorted)
        {
            return 0;
        }

        // 長度為 2 時，合法子字串只能是單一字元，無法改變兩字元的順序。
        if (n == 2)
        {
            return -1;
        }

        // 最小值已在左端或最大值已在右端，排序其餘部分即可完成。
        if (s[0] == mn || s[n - 1] == mx)
        {
            return 1;
        }

        // 內部已有全域極值時，可先將它移到正確端點，再排序剩餘區段。
        for (int i = 1; i < n - 1; i++)
        {
            if (s[i] == mn || s[i] == mx)
            {
                return 2;
            }
        }

        // 極值分處兩端且都不在正確端點時，需要三次合法子字串排序。
        return 3;
    }
}