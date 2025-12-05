using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace leetcode_151;

class Program
{
    /// <summary>
    /// 151. Reverse Words in a String
    /// https://leetcode.com/problems/reverse-words-in-a-string/description/?envType=study-plan-v2&envId=leetcode-75
    /// 151. 反轉字串中的單詞
    /// https://leetcode.cn/problems/reverse-words-in-a-string/description/
    /// 
    /// 
    /// </summary>
    /// <param name="args"></param>
    /// <summary>
    /// 給定一個輸入字串 s，請反轉字串中的單詞順序。
    /// 「單詞」由連續的非空格字元組成。s 中的單詞至少會被一個空格分隔。
    /// 回傳一個字串，該字串為按反序排列的單詞，並以單一空格連接。
    /// 注意 s 可能包含前導或尾隨空格，或兩個單詞之間有多個空格。回傳的字串中單詞間只應保留單一空格，且不要包含額外的空格。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料: 一組用來驗證兩個方法行為是否相同的測試
        var testCases = new Dictionary<string, string>()
        {
            {"the sky is blue", "blue is sky the"},
            {"  hello world  ", "world hello"},
            {"a good   example", "example good a"},
            {"    ", ""},
            {"one", "one"}
        };

        var instance = new Program();
        Console.WriteLine("Testing ReverseWords (manual parser) and ReverseWords_2 (Split + join):\n");
        foreach (var kv in testCases)
        {
            string input = kv.Key;
            string expected = kv.Value;

            string out1 = instance.ReverseWords(input);
            string out2 = instance.ReverseWords_2(input);

            Console.WriteLine($"Input: '{input}'");
            Console.WriteLine($"  ReverseWords  => {FormatForPrint(out1)}  | Expected: {FormatForPrint(expected)}  | OK: {out1 == expected}");
            Console.WriteLine($"  ReverseWords_2 => {FormatForPrint(out2)}  | Expected: {FormatForPrint(expected)}  | OK: {out2 == expected}");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    /// <summary>
    /// 反轉字串中的單詞。
    /// <para>解題說明：</para>
    /// <para>使用手動掃描字串的方式，從左到右解析每個單詞並以 "先出現者插入於字首" 的方式累加到 StringBuilder 中，
    /// 可在一次遍歷中完成單詞擷取、忽略多餘空白並反轉順序，避免額外的 split/regex 開銷。</para>
    /// <example>
    /// 輸入: "  hello world  " => 回傳: "world hello"
    /// </example>
    /// </summary>
    /// <param name="s">輸入字串，可能含前後空白或連續多個空白。</param>
    /// <returns>反轉後且單詞之間以單一空格分隔的字串。</returns>
    public string ReverseWords(string s)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == ' ')
            {
                // 去除空白
                continue;
            }

            // 字串起始位置
            int start = i;
            while (i < s.Length && s[i] != ' ')
            {
                // 字串結束位置; 計算該字串長度
                i++;
            }

            // 加入字串: 結束位置 - 起始位置
            if (sb.Length == 0)
            {
                // sb 為空, 就加入
                sb.Append(s.Substring(start, i - start));
            }
            else
            {
                // 新字串插入位置 0, 原先已加入者會往後, 最後加上一個空白區隔
                sb.Insert(0, s.Substring(start, i - start) + ' ');
            }
        }

        return sb.ToString();        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    /// <summary>
    /// 以 Split 將原始字串拆成單詞，並反向串接回傳結果。
    /// <para>解題說明：</para>
    /// <para>比較簡潔的做法是使用內建的 Split() 來取得所有非空白的單詞 (預設會以空白分隔並過濾掉空元素)，
    /// 再把字串陣列反向並以單一空格連接。此法程式較短，但會分配一個陣列與額外字串，時間/空間上與手動解析不同。</para>
    /// </summary>
    /// <param name="s">輸入字串，可能含前後空白或連續多個空白。</param>
    /// <returns>反轉後且單詞之間以單一空格分隔的字串。</returns>
    public string ReverseWords_2(string s)
    {
        s = s.Trim();
        String[] split = s.Split();
        // 正規化 去除 兩個以上空白
        Regex replaceSpace = new Regex(@"\s{2,}", RegexOptions.IgnoreCase);

        // 注意：若想要保留單詞之間的單一空格，應把兩個以上空白替換成單一空白 " " 而不是空字串 ""。
        // 但由於我們在此已先使用 Split() 取得單詞陣列，下面這行是多餘的；
        // 若要讓 replace 出現在後面組合，應該把 replace 的替換目標改為 " "。
        s = replaceSpace.Replace(s, "").Trim();

        StringBuilder sb = new StringBuilder();
        // 從字串陣列後端取來組合反轉順序
        for (int i = split.Length - 1; i >= 0; i--)
        {
            if (sb.Length > 0)
            {
                //if(i)
                //前面正則已經把空白去掉了,這邊依照題目要求補上
                sb.Append(" ");
            }
            sb.Append(split[i]);
        }

        string aa = "";
        aa = sb.ToString();

        // 針對字串結果再次正規化: 將連續空白轉為單一空白，再 Trim
        aa = replaceSpace.Replace(aa, " ").Trim();

        return aa;        
    }

    /// <summary>
    /// 格式化輸出內容以供 Console 顯示：
    /// - 若為空字串, 顯示 (empty)
    /// - 否則以單引號包起來, 例如 'value'
    /// </summary>
    /// <param name="s">要格式化的字串</param>
    /// <returns>供 Console 顯示的字串</returns>
    private static string FormatForPrint(string s)
    {
        if (string.IsNullOrEmpty(s)) return "(empty)";
        return $"'{s}'";
    }
}
