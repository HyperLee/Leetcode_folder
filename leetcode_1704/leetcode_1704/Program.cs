namespace leetcode_1704;

class Program
{
    /// <summary>
    /// 1704. Determine if String Halves Are Alike
    /// https://leetcode.com/problems/determine-if-string-halves-are-alike/description/
    /// 1704. 判断字符串的两半是否相似
    /// https://leetcode.cn/problems/determine-if-string-halves-are-alike/description/
    /// 
    /// 繁體中文（題目描述）：
    /// 給定一個長度為偶數的字串 s。將字串分成兩個等長的子字串，令 a 為前半部、b 為後半部。
    /// 如果兩個字串中元音字母（'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U'）的數量相同，則稱它們「相似」。
    /// 注意 s 可能包含大小寫字母。
    /// 若 a 與 b 相似，回傳 true，否則回傳 false。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();

        // 測試範例 1: "book" -> a = "bo", b = "ok", 元音: a=1(o), b=1(o) -> true
        string test1 = "book";
        Console.WriteLine($"測試 1: \"{test1}\" -> {program.HalvesAreAlike(test1)}"); // 預期: true

        // 測試範例 2: "textbook" -> a = "text", b = "book", 元音: a=1(e), b=2(o,o) -> false
        string test2 = "textbook";
        Console.WriteLine($"測試 2: \"{test2}\" -> {program.HalvesAreAlike(test2)}"); // 預期: false

        // 測試範例 3: "MerryChristmas" -> 前半 "MerryCh", 後半 "ristmas"
        string test3 = "MerryChristmas";
        Console.WriteLine($"測試 3: \"{test3}\" -> {program.HalvesAreAlike(test3)}");

        // 測試範例 4: "AbCdEfGh" -> 測試大小寫混合
        string test4 = "AbCdEfGh";
        Console.WriteLine($"測試 4: \"{test4}\" -> {program.HalvesAreAlike(test4)}");
    }

    /// <summary>
    /// 判斷字串的兩半是否「相似」。
    /// 
    /// <para>
    /// <b>解題思路：計數法</b><br/>
    /// 根據題目定義，若兩個字串含有相同數目的元音字母，則稱它們「相似」。<br/>
    /// 因此，我們只需將字串分成前後兩半，分別統計元音字母的個數，最後比較是否相等即可。
    /// </para>
    /// 
    /// <para>
    /// <b>時間複雜度：</b>O(n)，其中 n 為字串長度，需要遍歷整個字串一次。<br/>
    /// <b>空間複雜度：</b>O(1)，只使用常數額外空間。
    /// </para>
    /// </summary>
    /// <param name="s">輸入的偶數長度字串</param>
    /// <returns>若前半部與後半部的元音字母數量相同，回傳 true；否則回傳 false</returns>
    /// <example>
    /// <code>
    /// HalvesAreAlike("book");      // 回傳 true，前半 "bo" 有 1 個元音，後半 "ok" 有 1 個元音
    /// HalvesAreAlike("textbook");  // 回傳 false，前半 "text" 有 1 個元音，後半 "book" 有 2 個元音
    /// </code>
    /// </example>
    public bool HalvesAreAlike(string s)
    {
        // 將字串分成前後兩半
        string a = s.Substring(0, s.Length / 2);  // 前半部分
        string b = s.Substring(s.Length / 2);     // 後半部分

        // 定義所有元音字母（包含大小寫）
        string vowels = "aeiouAEIOU";

        // 初始化兩個計數器，分別記錄前後兩半的元音數量
        int sum1 = 0;
        int sum2 = 0;

        // 統計前半部分的元音字母數量
        foreach (char c in a)
        {
            if (vowels.Contains(c))
            {
                sum1++;
            }
        }

        // 統計後半部分的元音字母數量
        foreach (char c in b)
        {
            if (vowels.Contains(c))
            {
                sum2++;
            }
        }

        // 比較兩半的元音數量是否相等
        return sum1 == sum2;
    }
}
